using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RS485Test
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Test485 test485 = new Test485();
            string portName = "COM11";
            test485.Connect(portName);
            while(true)
            {
                Thread.Sleep(100);
            }
        }
        
    }
}
