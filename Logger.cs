using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader
{
    public static class Logger
    {
        static string _defaultPath = @"f:\RS\Exchange\jsonDefault\";
        private static string _configPath = @"config/defaultPath.txt";

        public static void Log(string message)
        {
            _defaultPath = JSONmanager.LoadPathFromFile();
            _defaultPath = Path.GetFullPath(Path.Combine(_defaultPath, ".."));

            var logFile = _defaultPath + @"\log.txt";
            if (!File.Exists(logFile))
            {
                using (var file = File.Create(logFile))
                {
                }
            }

            File.AppendAllText(logFile, message + Environment.NewLine);
        }
    }
}
