using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace cmd2
{
    class Program
    {
        private static System.Object lockthis1 = new System.Object();
        private static System.Object lockthis2 = new System.Object();
        static void Main(string[] args)
        {
            int i;
            string res = null;
            // Start the child process.
            Thread[] set1 = new Thread[10]; 
            Thread[] set2 = new Thread[10];
            for (i = 0; i < 10; i++)
            {

                set1[i] = new Thread(new ThreadStart(() => { res = threadFunc1(i); }));
                set1[i].Start();
            }
            for (i = 10; i < 20; i++)
            {
                set1[i-10] = new Thread(new ThreadStart(() => {res = threadFunc2(i);}));
                set1[i-10].Start();
            }

        }
        private static string threadFunc1(int n)
        {
            lock (lockthis1)
            {
                Process p = new Process();
                // Redirect the output stream of the child process.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = "ping";
                p.StartInfo.Arguments = "192.168.1." + n.ToString();
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                if (output.IndexOf("(0%") != (Int32)(-1))
                {
                    Console.WriteLine("192.168.1."+n+" is alive");
                    return "alive";
                }
                else
                {
                    Console.WriteLine("192.168.1." + n + " is not alive");
                    return "not alive";
                }
            }
         }
        private static string threadFunc2(int n)
        {
            lock (lockthis2)
            {
                Process p = new Process();
                // Redirect the output stream of the child process.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = "ping";
                p.StartInfo.Arguments = "192.168.1." + n.ToString();
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                if (output.IndexOf("(0%") != (Int32)(-1))
                {
                    Console.WriteLine("192.168.1." + n + " is alive");
                    return "alive";
                }
                else
                {
                    Console.WriteLine("192.168.1." + n + " is not alive");
                    return "not alive";
                }
            }
        }
    }
}
