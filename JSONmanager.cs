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
        static string _defaultPath = @"f:\RS\Exchange\jsonDefault\";
        static string _defaultURL = "https://storage.googleapis.com/osb-exchange/summary.json";
        private static string _configPath = @"config/defaultPath.txt";
        private static string _configURL = @"config/jsonURL.txt";

        private static void LoadStrings()
        {
            LoadTextFromFile(_defaultPath);
            LoadURLFromFile(_defaultURL);
        }

        public static void SaveJson()
        {
            LoadStrings();

            using (var client = new WebClient())
            {
                client.DownloadFile(_defaultURL, $@"{_defaultPath}{GetCurrentTimePrefix()}_summary.json");
            }
        }

        private static void WriteTest()
        {
            string fileToCopy = @"f:\RS\Exchange\test.json";
            File.Copy(fileToCopy, _defaultPath + Path.GetFileName(fileToCopy));
        }

        private static void LoadTextFromFile(string target)
        {
            if (File.Exists(_configPath))
            {
                target = File.ReadAllText(_configPath);

                if (!string.IsNullOrWhiteSpace(target) && Directory.Exists(target))
                {
                    _defaultPath = target;
                }
            }
        }

        private static void LoadURLFromFile(string target)
        {
            if (File.Exists(_configURL))
            {
                target = File.ReadAllText(_configURL);

                if (!string.IsNullOrWhiteSpace(target) && CheckURLValid(target))
                {
                    _defaultURL = target;
                }
            }
        }

        private static string GetCurrentTimePrefix()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH-mm");
        }

        private static bool CheckURLValid(this string source) => Uri.TryCreate(source, UriKind.Absolute, out Uri uriResult) && uriResult.Scheme == Uri.UriSchemeHttps;
    }
}
