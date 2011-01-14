using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Secsay.xss
{
    /// <summary>
    /// This class is responsible for throttling requests to be sent to the remote
    /// server via Fiddler.
    /// </summary>
    internal sealed class RequestManager
    {
        public const int DEFAULT_DELAY = 1000;                                              // Default delay (ms) prior to sending each batch of requests
        public const int DEFAULT_BATCHSIZE = 25;                                            // Default number of requests to send from the queue after each delay

        public const int MAXIMUM_DELAY = Int32.MaxValue;                                    // Maximum delay (ms)
        public const int MINIMUM_DELAY = 0;                                                 // Minimum delay (ms)

        public const int MAXIMUM_BATCHSIZE = Int32.MaxValue;                                // Maximum batch size
        public const int MINIMUM_BATCHSIZE = 1;                                             // Minimum batch size

        private const int THREAD_SHUTDOWN_DELAY = 1000;                                    // Maximum amount of time to wait for the processing thread to shut down

        private Queue<Secsay.Session> m_outstandingRequests = new Queue<Secsay.Session>();  // Queue of pending requests to be sent
        private ManualResetEvent m_signal = new ManualResetEvent(false);                    // Event used to signal the shutdown of the request manager
        private Thread m_thread;                                                            // Thread responsible for processing the queue of outgoing requests

        private int m_delay = DEFAULT_DELAY;                                                // Time to wait in milliseconds prior to sending each request in the queue
        private int m_batchSize = DEFAULT_BATCHSIZE;                                        // Number of requests to send from the queue after each delay

        /// <summary>
        /// Create an instance of the class which sends requests at the specified interval.
        /// </summary>
        public RequestManager()
        {
            Debug.WriteLine("RequestManager initializing...");
            m_thread = new Thread(new ThreadStart(RequestProcessingThread));
            m_thread.Name = "RequestManage";
            Throttle = true;    // By default, throttling is enabled.
        }

        // TODO: implement Throttle, BatchSize; confirm Delay

        /// <summary>
        /// Number of requests to send from the queue after each delay.
        /// </summary>
        public int BatchSize 
        {
            get
            {
                return m_batchSize;
            }

            set
            {
#if false
                // Throw on invalid range
                if (value > MAXIMUM_BATCHSIZE || value < MINIMUM_BATCHSIZE)
                {
                    throw new ArgumentOutOfRangeException("BatchSize");
                }
#else
                // Validate and correct the batch size
                if (value > MAXIMUM_BATCHSIZE)
                {
                    m_batchSize = MAXIMUM_BATCHSIZE;
                }

                else if (value < MINIMUM_BATCHSIZE)
                {
                    m_batchSize = MINIMUM_BATCHSIZE;
                }

                else
#endif
                m_batchSize = value;
            }
        }

        /// <summary>
        /// Time to wait in milliseconds prior to sending each request in the queue.
        /// </summary>
        public int Delay
        {
            get
            {
                return m_delay;
            }

            set
            {
#if false
                // Throw on invalid range
                if (value > MAXIMUM_DELAY || value < MINIMUM_DELAY)
                {
                    throw new ArgumentOutOfRangeException("Delay");
                }
#else
                // Validate and correct the delay period
                if (value > MAXIMUM_DELAY)
                {
                    m_delay = MAXIMUM_DELAY;
                }

                else if (value < MINIMUM_DELAY)
                {
                    m_delay = MINIMUM_DELAY;
                }

                else
#endif
                m_delay = value;
            }
        }

        /// <summary>
        /// True if request throttling is enabled; False otherwise.
        /// </summary>
        public bool Throttle { get; set; }

        /// <summary>
        /// This method starts the request processing thread.
        /// </summary>
        public void Start()
        {
            // Ensure the thread has not yet been started
            if (m_thread.IsAlive == false)
            {
                lock (m_thread)
                {
                    if (m_thread.IsAlive == false)
                    {
                        Debug.WriteLine("Attempting to start the RequestManager thread...");
                        m_thread.Start();   // Note: this may throw if the thread has already been started
                    }
                }
            }
        }

        /// <summary>
        /// Shutdown the request processing thread.
        /// </summary>
        public void Stop()
        {
            // Ensure the thread is running
            if (m_thread.IsAlive)
            {
                lock (m_thread)
                {
                    if (m_thread.IsAlive)
                    {
                        // If the stop signal has not been raised, attempt to raise it
                        if (m_signal.WaitOne(0) == false)
                        {
                            Debug.WriteLine("Attempting to stop the RequestManager thread...");

                            // Signal the request processing thread to stop, and wait up to THREAD_SHUTDOWN_DELAY (ms) for the thread to exit.
                            m_signal.Set();
                            m_thread.Join(THREAD_SHUTDOWN_DELAY);    // Note: This may throw ThreadStateException if the thread has already been stopped
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method removes any remaining requests from the queue.
        /// </summary>
        public void Clear()
        {
            lock (m_outstandingRequests)
            {
                m_outstandingRequests.Clear();
                Debug.WriteLine(String.Format("Cleared {0} requests.", m_outstandingRequests.Count));
            }
        }

        /// <summary>
        /// This method adds a session to the outgoing queue.
        /// </summary>
        /// <param name="session">The session to enqueue.</param>
        public void Enqueue(Secsay.Session session)
        {
            lock (m_outstandingRequests)
            {
                Debug.WriteLine("Queuing a session...");
                m_outstandingRequests.Enqueue(session);
                Debug.WriteLine(String.Format("{0} sessions queued.", m_outstandingRequests.Count));
            }
        }

        /// <summary>
        /// This method performs the throttled request injection.
        /// </summary>
        private void RequestProcessingThread()
        {
            while (true)
            {
                // If we're throttling requests, pause for the user-specified delay interval.
                // Otherwise, pause for the default delay interval.  If the exit signal has 
                // been raised during this period, exit.
                if (m_signal.WaitOne(Throttle ? Delay : DEFAULT_DELAY,false))
                    break;

                lock (m_outstandingRequests)
                {
                    // Determine the number of requests to inject.  If we're not throttling,
                    // this will be ther number of requests we ultimately inject.
                    int requestsToInject = m_outstandingRequests.Count;

                    // If there are no outstanding requests, resume the wait.
                    if (requestsToInject < 1)
                        continue;

                    // If we're throttling and the number of requests to inject is greater 
                    // than the batch size, inject a maximum of BatchSize requests.
                    else if (Throttle && requestsToInject > BatchSize)
                        requestsToInject = BatchSize;

                    Debug.WriteLine(String.Format("Performing injection of {0} out of {1} total sessions in the queue...", requestsToInject, m_outstandingRequests.Count));

                    // Inject the next batch of requests
                    for (int n = 0; n < requestsToInject; ++n)
                    {
                        Secsay.Session session = m_outstandingRequests.Dequeue();
                        Secsay.xss.FiddlerUtils.CasabaSessionFiddlerInjector(session);
                    }

                    Debug.WriteLine(String.Format("{0} sessions remaining in queue.", m_outstandingRequests.Count));
                }
            }
        }
    }
}
