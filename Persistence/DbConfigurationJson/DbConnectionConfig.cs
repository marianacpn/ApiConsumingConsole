using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading;

namespace Persistence.DbConfigurationJson
{
    public class DbConnectionConfig
    {
        public string AppSettingsRootPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appSettings.json");
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string SSPI { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public DbConnectionConfig()
        {

        }

        public string GetConnectionString()
        {
            string conn;

            conn = string.Format("data source={0}; initial catalog={1}; persist security info=True; user id={2}; password={3}; MultipleActiveResultSets=True; App=EntityFrameworkCore",
                Server, Database, User, Password);

            return conn;
        }

        public void Configure(string server, string db, string user, string password)
        {
            string jsonAppSettings = File.ReadAllText(AppSettingsRootPath);

            JObject rss = JObject.Parse(jsonAppSettings);

            JObject channel = (JObject)rss["DbConnection"];

            channel["Server"] = server;
            channel["Database"] = db;
            channel["User"] = user;
            channel["Password"] = password;

            File.WriteAllText(AppSettingsRootPath, rss.ToString());

            Thread.Sleep(TimeSpan.FromSeconds(5));
        }

        public bool HasConnectionString()
        {
            return !string.IsNullOrEmpty(Database);
        }

        public string ToJson()
        {
            string format = "{{ \"{0}\": {1} }}";

            string result = string.Format(format, "DbConnection", JsonConvert.SerializeObject(this));

            return result;
        }
    }
}
