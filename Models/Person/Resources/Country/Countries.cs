using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data.SqlClient;

namespace Models.Person.Resources.Country
{
    public static class Countries
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);

        public static string CountriesAsJsonFile = "C:\\Users\\Mike\\Documents\\Coding\\Revature\\Mike-SPS-CUNY-Project1\\Models\\Person\\Resources\\Country\\Countries.json";
        
        private static string connectionString = "Data Source=rev-training-mc-dbs.database.windows.net;" +   // SQL Server
                                                 "Initial Catalog=rev-training-mc-contacts-db;" +            // SQL DB
                                                 "Persist Security Info=True;" +                             // Security
                                                 "MultipleActiveResultSets=True;" +                          // MARS
                                                 "User ID=revature;" +                                       // User name
                                                 "Password=Password1";                                       // Password

        public static List<CountryModel> GetCountriesFromJson()
        {
            string json = File.ReadAllText(@CountriesAsJsonFile);
            List<CountryModel> countries = JsonConvert.DeserializeObject<List<CountryModel>>(json);
            //json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            return countries;
        }
        
        public static void PopulateTable()
        {
            List<CountryModel> countryList = GetCountriesFromJson();

            logger.Info($"countryList = {countryList}");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlTransaction transaction = connection.BeginTransaction(); // Create transaction
                SqlCommand command = connection.CreateCommand();            // Create command
                command.Transaction = transaction;                          // Assign transaction to command

                foreach(CountryModel c in countryList)
                {
                    try
                    {
                        // INSERT for countrymodel
                        command.CommandText = $"INSERT INTO Country VALUES (" +
                                              $"'{Guid.NewGuid()}', " + 
                                              $"'{c.Name}', " +
                                              $"'{c.PhoneCode}', " +
                                              $"'{c.ISO2}', " +
                                              $"'{c.ISO3}'" +
                                              ");";
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        logger.Info($"Models.Scripts.Countries.PopulateTable: {ex.Message}");
                        try
                        {
                            // Roll back if exception
                            transaction.Rollback();
                        }
                        catch (Exception exRollback)
                        {
                            Console.WriteLine(exRollback.Message);
                        }
                    }
                }
                // Commit transaction
                transaction.Commit();
            }
        }        
    }
}
