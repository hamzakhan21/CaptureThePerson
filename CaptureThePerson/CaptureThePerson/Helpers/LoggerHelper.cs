using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureThePerson.Helpers
{
    internal class LoggerHelper
    {
        //public static void Log(string _event)
        //{
        //    try
        //    {
        //        SystemLogging log = new SystemLogging();
        //        log.Activity = _event;
        //        log.LogTime = DateTime.Now;
        //        log.BranchID = 1;
        //        log.UserID = 1016;
        //        log.Save();
        //    }
        //    catch
        //    {
        //        DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Log");
        //        if (directory.Exists)
        //        {
        //            string fileName = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + ".txt";
        //            string filePath = directory.FullName + "\\" + fileName;
        //            FileInfo file = new FileInfo(filePath);
        //            using (StreamWriter writer = file.Exists ? new StreamWriter(filePath, true) : File.CreateText(filePath))
        //            {
        //                writer.WriteLine(_event);
        //                writer.WriteLine("At: " + DateTime.Now);
        //                writer.WriteLine("************************************************");
        //                writer.Flush();
        //                writer.Close();
        //            }
        //        }
        //    }
        //}

        //public static void ErrorLog(Exception ex, string path, string AdditionalDetails = "")
        //{
        //    try
        //    {
        //        TblErrorLog objUsrErorFedBck = new TblErrorLog();
        //        objUsrErorFedBck.ErrorX = AdditionalDetails + " | " + ex.Message;
        //        objUsrErorFedBck.StrackTrace = ex.StackTrace;
        //        objUsrErorFedBck.ErrorPath = path;
        //        objUsrErorFedBck.Save();
        //    }
        //    catch
        //    {
        //        DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\ExceptionLog");
        //        if (directory.Exists)
        //        {
        //            string fileName = "Error-Log-" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + ".txt";
        //            string filePath = directory.FullName + "\\" + fileName;
        //            FileInfo file = new FileInfo(filePath);
        //            using (StreamWriter writer = file.Exists ? new StreamWriter(filePath, true) : File.CreateText(filePath))
        //            {
        //                writer.WriteLine(ex.Message);
        //                writer.WriteLine("At: " + DateTime.Now);
        //                writer.WriteLine("************************************************");
        //                writer.Flush();
        //                writer.Close();
        //            }
        //        }
        //    }
        //}

        public static void LogInFile(string _event, string fileName = "BP_Log", string folder = "")
        {

            try
            {
                DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\FileLog");
                if (directory.Exists)
                {
                    if (folder != "")
                    {
                        if (!new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\FileLog\" + folder).Exists)
                            Directory.CreateDirectory(directory.FullName + "\\" + folder);

                        folder = directory.FullName + "\\" + folder + "\\";
                    }
                    else
                        folder = directory.FullName + "\\";
                    string filePath = folder + fileName + ".txt";
                    FileInfo file = new FileInfo(filePath);
                    using (StreamWriter writer = file.Exists ? new StreamWriter(filePath, true) : File.CreateText(filePath))
                    {
                        writer.WriteLine(_event + " -- At: " + DateTime.Now);
                        writer.Flush();
                        writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(_event + " -- At: " + DateTime.Now);
                //Console.WriteLine(ex.Message);
                try
                {
                    LogInFile(ex.Message + "|" + fileName + "|" + _event, "FileWriteLog");
                }
                catch (Exception ex1)
                {
                    Console.WriteLine(ex1.Message + " -- " + folder + " -- " + fileName);
                }
                //LogInFile(_event, fileName, folder); //Retry
            }
        }

    }
}
