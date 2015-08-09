// File: Logger.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 07 08 2015

#region

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

#endregion

namespace Utils
{
    public class Logger
    {
        private static readonly Stopwatch Sw = new Stopwatch();
        private static readonly Logger InstanceLogger = new Logger();
        private static readonly StringBuilder LogString = new StringBuilder();

        static Logger()
        {
        }

        private Logger()
        {
            Sw.Start();
        }

        public static Logger GetLogger()
        {
            return InstanceLogger;
        }

        public void SaveToFile()
        {
            using (FileStream fs = new FileStream("log.txt", FileMode.Append))
            {
                AddMsgToLog("log saving into file");
                string tmp = LogString.ToString();
                byte[] buffBytes = Encoding.Unicode.GetBytes(tmp);
                fs.Write(buffBytes, 0, buffBytes.Length);
            }
        }

        public static void AddMsgToLog(string msg)
        {
            var preLog = string.Format("{0} {1} | {2} logged at {3} from application launch\n",
                DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), msg, Sw.Elapsed.ToString("g"));
            LogString.Append(preLog);
        }
    }
}