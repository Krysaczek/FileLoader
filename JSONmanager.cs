namespace FileLoader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public static class JSONmanager
    {
        private static string _defaultPath = @"f:\RS\Exchange\jsonDefault\";
        private static string _defaultURL = "https://storage.googleapis.com/osb-exchange/summary.json";
        private static string _configPath = @"config/defaultPath.txt";
        private static string _configURL = @"config/jsonURL.txt";

        private static void LoadStrings()
        {
            _defaultPath = LoadPathFromFile();
            _defaultURL = LoadURLFromFile();
        }

        public static void SaveJson()
        {
            LoadStrings();

            using (var client = new WebClient())
            {
                Logger.Log($"{GetCurrentTimePrefix()}: Saving"); // refactor with try catch
                client.DownloadFile(_defaultURL, $@"{_defaultPath}{GetCurrentTimePrefix()}_summary.json");
            }
        }

        public static string LoadPathFromFile()
        {
            if (File.Exists(_configPath))
            {
                var temp = File.ReadAllText(_configPath);

                if (!string.IsNullOrWhiteSpace(temp) && Directory.Exists(temp))
                {
                    return temp;
                }
            }

            return _defaultPath;
        }

        private static string LoadURLFromFile()
        {
            if (File.Exists(_configURL))
            {
                var temp = File.ReadAllText(_configURL);

                if (!string.IsNullOrWhiteSpace(temp) && CheckURLValid(temp))
                {
                    return temp;
                }
            }

            return _defaultURL;
        }

        private static string GetCurrentTimePrefix()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH-mm");
        }

        private static bool CheckURLValid(this string source) => Uri.TryCreate(source, UriKind.Absolute, out Uri uriResult) && uriResult.Scheme == Uri.UriSchemeHttps;
    }
}
