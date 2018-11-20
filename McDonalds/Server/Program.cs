using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace Server
{
    class Program
    {
        private static ServerBinder binder;

        static void Main(string[] args)
        {
            binder = new ServerBinder();
            binder.StartListening();
        }
    }
}
