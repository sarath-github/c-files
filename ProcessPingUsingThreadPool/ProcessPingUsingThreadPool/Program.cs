using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
namespace ProcessPingUsingThreadPool
{
    class Program
    {
        private static int count;
        private static ManualResetEvent[] done_event = new ManualResetEvent[5];//Manual reset events will allow us to do changes manually...
        static void Main(string[] args)
        {
            int set_count = 0;
            count = 0;//count is for Creating our own thread IDs'
            ThreadPool.SetMinThreads(1, 5);
            ThreadPool.SetMaxThreads(5, 5);
            while (count < 256)
            {
                for (int i = 0; i < 5; i++)
                {
                    done_event[i] = new ManualResetEvent(false);
                    ThreadPool.QueueUserWorkItem(threadFunc,i);
                }
                WaitHandle.WaitAll(done_event);
                Console.WriteLine("Set {0} is completed...", ++set_count);
            }
        }
        private static void threadFunc(object event_no)
        {
            Process p = new Process();
            int local_count = count++;
            p.StartInfo.FileName = "ping";
            p.StartInfo.Arguments = "192.168.1." + local_count.ToString();
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            if (output.IndexOf("(0%") != (Int32)(-1))
            {
                Console.WriteLine("192.168.1." + local_count + " is alive");
            }
            else
            {
                Console.WriteLine("192.168.1." + local_count + " is not alive");
            }
            done_event[(int)event_no].Set();
        }
    }
}
