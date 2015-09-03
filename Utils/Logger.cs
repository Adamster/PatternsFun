using System;
using System.Diagnostics;
using System.IO;
using System.Text;

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
            using (var fs = new FileStream(@"C:\Users\" + Environment.UserName + @"\Documents\log.txt", FileMode.Append)
                )
            {
                AddMsgToLog("log saving into file");
                var tmp = LogString.ToString();
                var buffBytes = Encoding.Unicode.GetBytes(tmp);
                fs.Write(buffBytes, 0, buffBytes.Length);
            }
        }

        public static void AddMsgToLog(string msg)
        {
            var preLog = string.Format("{0} {1} | {2} logged at {3} from application launch\n",
                DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), msg, Sw.Elapsed.ToString("g"));
            LogString.Append(preLog);
        }

        public static void WriteLIne(string msg)
        {
            AddMsgToLog(msg);
            Console.WriteLine(msg);
        }
    }
}