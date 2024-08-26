using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ConfigureSettingsBusiness: IConfigureSettingsBusinesss
    {

        private readonly IConfiguration _configuration;

        public ConfigureSettingsBusiness(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool UpdateConectionString(ConfigureSettingsDTO configureSettings)
        {
            var appSettingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var json = File.ReadAllText(appSettingsFilePath);
            var jsonObj = JObject.Parse(json);
            string getConnectionString = jsonObj["MSSQLServerSQLConnection"]["MSSQLServerSQLConnectionString"].ToString();
            var conectionStringSplit = SplitConextionString(getConnectionString);
            string catalog = configureSettings.Catalog ?? conectionStringSplit.Catalog;
            string dataSource = configureSettings.DataSource ?? conectionStringSplit.DataSource;
            string userID = configureSettings.UserID ?? conectionStringSplit.UserID;
            string password = configureSettings.Password ?? conectionStringSplit.Password;
            string newConnectionString = $"Data Source={dataSource};Initial Catalog={catalog};User ID={userID};Password={password};Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False";
            jsonObj["MSSQLServerSQLConnection"]["MSSQLServerSQLConnectionString"] = newConnectionString;
            File.WriteAllText(appSettingsFilePath, jsonObj.ToString());
            return true;
        }

        public ConfigureSettingsDTO SplitConextionString(string connectionString)
        {
            var parameters = connectionString.Split(';');
            var dictionary = new Dictionary<string, string>();
            string dataSource = dictionary.ContainsKey("Data Source") ? dictionary["Data Source"] : null;
            string initialCatalog = dictionary.ContainsKey("Initial Catalog") ? dictionary["Initial Catalog"] : null;
            string userId = dictionary.ContainsKey("User ID") ? dictionary["User ID"] : null;
            string password = dictionary.ContainsKey("Password") ? dictionary["Password"] : null;
            return new ConfigureSettingsDTO
            {
                Catalog = initialCatalog,
                UserID =   userId,
                DataSource = dataSource,
                Password = password
            };
        }

        //private string GetConnectionString()
        //{
        //    var appSettingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        //    var json = File.ReadAllText(appSettingsFilePath);
        //    var jsonObj = JObject.Parse(json);
        //    string getConnectionString = jsonObj["ConnectionStrings"]["DefaultConnection"].ToString();
        //    return getConnectionString;
        //}


    }
}
