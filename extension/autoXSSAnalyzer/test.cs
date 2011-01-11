using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Casaba;

namespace CasabaSecurity.Web.x5s
{
    class test
    {
        public static void Main(string[] rgs)
        {
            UAEngine xa = new UAEngine();
            UAUserInterface form1 = new UAUserInterface(xa);
            Application.Run(form1);
        }
    }
}
