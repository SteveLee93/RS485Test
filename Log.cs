using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RS485Test
{
    public static class Log
    {
        public enum LogType
        {
            Log_485 = 1,
        }
        public static string LogPath = "C:\\Ablelabs\\Notable_Robot\\Log";

        public static void Start()
        {
            DirectoryInfo di = new DirectoryInfo(LogPath);
            if (di.Exists == false)
            {
                di.Create();
            }
        }

        readonly static object _lockObject = new object();
        public static void WriteLog(LogType logType, string log)
        {
            lock (_lockObject)
            {
                string log_path_date = "\\" + DateTime.Today.ToString("yyyyMMdd") + "\\";
                string time_log = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(DateTime.Today.ToString())) + DateTime.Now.ToString(" HH:mm:ss") + ":" + DateTime.Now.Millisecond.ToString("000") + "\t";
                string log_file_name;
                string file_path;
                switch (logType)
                {
                    case LogType.Log_485: log_file_name = "Log_485.txt"; break;
                    default: log_file_name = "Default.txt"; break;
                }
                file_path = LogPath + log_path_date;
                DirectoryInfo di = new DirectoryInfo(file_path);
                if (di.Exists == false)
                {
                    di.Create();
                }
                file_path += log_file_name;
                try
                {
                    using (StreamWriter sw = new StreamWriter(file_path, append: true))
                    {
                        sw.WriteLine(time_log + log);
                    }
                    Console.WriteLine(time_log + log);
                }
                catch
                {

                }
            }
        }
    }
}
