using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Timers;
using System.Net;


namespace TimedStatusLog
{
    class Program
    {
        public static void Main()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 20000;
            aTimer.Enabled = true;
            aTimer.Start();
            File.WriteAllText(@"C:\Users\user\Documents\Visual Studio 2010\LOG\log1.txt", "");

            //Console.WriteLine("Press \'q\' to quit the sample.");
            //while (Console.Read() != 'q') ;
            while (true) ;
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://sarath.freshservice.com");
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Console.WriteLine(Environment.CurrentDirectory);
            File.AppendAllText(@"C:\Users\user\Documents\Visual Studio 2010\LOG\log1.txt", "\n" + DateTime.Now.ToString() + ": Status " + res.StatusCode);
            //Console.WriteLine(DateTime.Now + ": " + res.StatusCode);
            res.Close();
        }
    }
}
