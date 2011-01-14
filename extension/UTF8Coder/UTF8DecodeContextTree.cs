namespace XNMD
{
    using System;

    public class UTF8DecodeContextTree
    {
        public Node root = new Node();

        public bool Add(Node currentNode, UTF8DecodeContext context, TreeStatus t)
        {
            if (t == TreeStatus.Valid)
            {
                currentNode.center = new Node();
                currentNode.center.value = context;
                currentNode.left = null;
                currentNode.right = null;
            }
            else if (t == TreeStatus.InvalidByte)
            {
                currentNode.right = new Node();
                currentNode.right.value = context;
            }
            else if (t == TreeStatus.InvalidSequence)
            {
                currentNode.left = new Node();
                currentNode.left.value = context;
            }
            return true;
        }
    }
}
