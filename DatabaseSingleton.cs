using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducentConsumerProblem
{
    public class DatabaseSingleton
    {
        // Statická proměnná typu SqlConnection, která uchovává instanci připojení k databázi.
        private static SqlConnection conn = null;
        private DatabaseSingleton()
        {

        }
        /// <summary>
        /// Získá instanci připojení k databázi Pokud instance ještě neexistuje vytvoří novou.
        /// </summary>
        /// <returns>Instance třídy SqlConnection představující připojení k databázi.</returns>
        public static SqlConnection GetInstance()
        {
            // Kontroluje, zda instance připojení již existuje.
            if (conn == null)
            {
                // Pokud instance neexistuje vytvoří novou
                SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
                // Nastaví vlastnosti připojení na hodnoty načtené z konfiguračního souboru
                consStringBuilder.UserID = ReadSetting("Name");
                consStringBuilder.Password = ReadSetting("Password");
                consStringBuilder.InitialCatalog = ReadSetting("Database");
                consStringBuilder.DataSource = ReadSetting("DataSource");
                consStringBuilder.ConnectTimeout = 30;
                conn = new SqlConnection(consStringBuilder.ConnectionString);
                // Otevře spojení s databází
                conn.Open();
            }
            // Vrací existující instanci připojení k databázi
            return conn;
        }

        /// <summary>
        /// Uzavře spojení s databází pokud je spojení aktivní
        /// </summary>
        public static void CloseConnection()
        {
            // Kontroluje, zda je spojení s databází aktivní
            if (conn != null)
            {
                // Pokud je spojení aktivní uzavře ho
                conn.Close();
                conn.Dispose();
                // Nastaví proměnnou conn na hodnotu null
                conn = null;
            }
        }

        /// <summary>
        /// Funkce pro čtení hodnoty z konfiguračního souboru aplikace
        /// </summary>
        /// <param name="key">Klíč podle kterého se má hledat hodnota v konfiguraci</param>
        /// <returns>
        /// Hodnota z konfiguračního souboru aplikace
        /// Pokud klíč není nalezen funkce vrátí řetězec "Not Found"
        /// </returns>
        private static string ReadSetting(string key)
        {
            //přečte hodnotu z App.config
            var appSettings = ConfigurationManager.AppSettings;
            //pokud nenajde hodnotu vrítí Not Found
            string result = appSettings[key] ?? "Not Found";
            //vrátí nalezenou hodnotu nebo not found
            return result;
        }
    }
}
