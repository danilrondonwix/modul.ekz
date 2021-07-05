using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modul.ekz
{
    class Class1
    {
        static void Main(string[] args)
        {
            Debug.Listeners.Add(new TextWriterTraceListener(File.CreateText("Log.txt")));
            Debug.AutoFlush = true;
            modulekz Cpit = new modulekz();
            Cpit.Work();
        }
    }
}
