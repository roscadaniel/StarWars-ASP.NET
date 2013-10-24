using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarWars.Helpers
{
    public class Logging
    {
        public void Main(string logMessage)
        {
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            mydocpath += "/log.txt";
            using (StreamWriter w = File.AppendText(mydocpath))
            {
                Log(logMessage, w);
                // Close the writer and underlying file.
                w.Close();
            }
            // Open and read the file.
            using (StreamReader r = File.OpenText(mydocpath))
            {
                DumpLog(r);
            }
        }

        public void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
            // Update the underlying file.
            w.Flush();
        }

        public void DumpLog(StreamReader r)
        {
            // While not at the end of the file, read and write lines.
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            r.Close();
        }
    }
}