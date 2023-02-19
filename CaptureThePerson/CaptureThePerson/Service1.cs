using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CaptureThePerson.Helpers;

namespace CaptureThePerson
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread.Sleep(TimeSpan.FromMinutes(1));
            string Filename = "Image_" + DateTime.Now.ToString("yyyy MM dd HH mm ss");
            while (!CaptureHelper.CapturePerson(Filename))
            {
              LoggerHelper.LogInFile("File not Saved FileName:"+ Filename,"PictureSavingLog");
            }
            

        }

        protected override void OnStop()
        {
        }
    }
}
