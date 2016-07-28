using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Net;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace MyNewService5
{
    public partial class MyNewService5 : ServiceBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MyNewService5));
        int count;
        public MyNewService5()
        {
            count = 0;
            InitializeComponent();
        }

        private System.Timers.Timer timer;

        protected override void OnStart(string[] args)
        {
           // BasicConfigurator.Configure();
            this.timer = new System.Timers.Timer(20000D);  // 30000 milliseconds = 30 seconds
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            this.timer.Start();
            //File.WriteAllText(@"C:\Users\user\Documents\Visual Studio 2010\LOG\log1.txt", "");
           // File.Create(@"C:\Users\user\Documents\Visual Studio 2010\LOG\log1.txt");
            log.Info("Starting application.");
        }

        protected override void OnStop()
        {
            this.timer.Stop();
            this.timer = null;
            log.Info("Exiting application.");
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            count++;
            String ress = "";
            /*var ping = new System.Net.NetworkInformation.Ping();
                // Console.WriteLine(Environment.CurrentDirectory);
            var result = ping.Send("https://sarath.freshservice.com");

            if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
                ress = "Error...";
            else
                ress = "OK";*/
            HttpWebResponse res = null;

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://sarath.freshservice.com");
                res = (HttpWebResponse)req.GetResponse();
                ress = res.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                log.Error("website not alive and error is: "+ex.ToString());
                //File.AppendAllText(@"C:\Users\user\Documents\Visual Studio 2010\LOG\log1.txt", "Error: "+ex.ToString());
            }
            finally
            {
                res.Close();
            }
                //File.Create(@"C:\Users\user\Documents\Visual Studio 2010\LOG\log1.txt");
                File.AppendAllText(@"C:\Users\user\Documents\Visual Studio 2010\LOG\log1.txt", " " + DateTime.Now.ToString() + ": Status " + ress + "\n");
                //Console.WriteLine(DateTime.Now + ": " + res.StatusCode);
                log.Debug("status: " + ress);
                /*this.timer = new System.Timers.Timer(30000D);  // 30000 milliseconds = 30 seconds
                this.timer.AutoReset = true;
                this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
                this.timer.Start();*/
     
               // File.AppendAllText(@"C:\Users\user\Documents\Visual Studio 2010\LOG\log1.txt", "Error in https://sarath.freshservice.com :" + ee.ToString());
                if (count == 10)
                {
                    this.timer.Stop();
                    this.timer.Enabled = false;
                }     
        }
        private string get_res()
        {
           /* HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://sarath.freshservice.com");
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            String ress = (string)res.StatusCode.ToString();
            res.Close();
            return ress;*/
          /*  var ping = new System.Net.NetworkInformation.Ping();

            var result = ping.Send("www.google.com");

            if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
                return "Error...";
            else
                return result.ToString();*/
            return "Test...";
        
        }
    }
}
