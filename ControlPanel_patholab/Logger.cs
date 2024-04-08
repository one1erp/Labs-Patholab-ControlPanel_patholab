using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ControlPanel_patholab
{

    public static class Logger1
    {
        // Create a writer and open the file:
        private static StreamWriter log;
        private static string path;
        static Logger1()
        {
             path = "C:\\ControlPanel_program\\Log\\"+ DateTime.Now.ToString("dd-MM-yy hh_mm_ss") + ".log";

            if (!File.Exists(path))

            {
                log = new StreamWriter(path);
            }
            else
            {
                log = File.AppendText(path);
            }

        }
        public static void Log(string strLogText)
        {

            // Write to the file:
            log.WriteLine(DateTime.Now);
            log.WriteLine(strLogText);
            log.WriteLine();
        }

        public static void Close()
        {
         
            if (log != null) log.Close();
        }
    }
}