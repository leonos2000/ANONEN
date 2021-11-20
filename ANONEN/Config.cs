using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ANONEN
{
    class Config
    {
        public class Data
        {
            public string driversLocation { get; set; }
            public string appsLocation { get; set; }
            public List<AppData> apps { get; set; }
            public List<PresetData> presets { get; set; }
        }
        public class AppData
        {
            public string title { get; set; }
            public string description { get; set; }
            public string execPath { get; set; }
            public string args { get; set; }
        }
        public class PresetData
        {
            public string name { get; set; }
            public string description { get; set; }
            public List<string> apps { get; set; }
        }

        public static Data all;

        public static void loadConfig(string configPath = "config.json")
        {
            all = JsonSerializer.Deserialize<Data>(File.ReadAllText(configPath));
        }
    }
}
