using System.Drawing;
using System;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using System.Threading;

namespace CaptureThePerson.Helpers
{

    public static class CaptureHelper
    {
        public static VideoCaptureDevice Device = null;

        public static string FileName = "";
        public static bool CapturePerson(string _FileName)
        {
            try
            {
                FilterInfoCollection WebcamColl;
                FileName = _FileName;

                WebcamColl = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                //if you have connected with one more camera choose index as you want 
                Device = new VideoCaptureDevice(WebcamColl[2].MonikerString);
                Device.Start();
                Device.NewFrame += new NewFrameEventHandler(Device_NewFrame);
                Thread.Sleep(TimeSpan.FromSeconds(30));

                string path = @"C:\Dropbox" + @"\" + FileName + ".jpg";
                LoggerHelper.LogInFile("Checking file against path:"+path, "Checking");

                FileInfo fileInfo = new FileInfo(path);
                LoggerHelper.LogInFile( "file is saved "+ fileInfo.Exists +"Against Path"+ fileInfo.FullName, "Checking");

                return fileInfo.Exists; 
            }
            catch (Exception ex)
            {
                LoggerHelper.LogInFile("Exception:" + ex.Message, "ErrorLog");
                return false;
            }
        }
        public static void Device_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Image img = (Bitmap)eventArgs.Frame.Clone();
            string fileName = FileName;
            fileName = fileName + ".jpg";
            string path = @"C:\Dropbox";

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
                directoryInfo.Create();

            img.Save(path + @"\" + fileName);
            Device.SignalToStop();
        }
    }
}
