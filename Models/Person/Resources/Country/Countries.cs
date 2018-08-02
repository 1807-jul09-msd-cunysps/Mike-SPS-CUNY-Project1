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

        public static string CountriesAsJsonFile = "C:\\Users\\Mike\\Desktop\\Revature\\Mike-SPS-CUNY-Project0\\Models\\Resources\\Countries.json";

        
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
        
        public static void CreateTable()
        {
            string createCountryTable = $"CREATE TABLE Country(" +
                                 $"Id INT PRIMARY KEY IDENTITY(1,1)," +
                                 $"[Name] VARCHAR(255)," +
                                 $"[PhoneCode] VARCHAR(15)," +
                                 $"[ISO2] VARCHAR(2)," +
                                 $"[ISO3] VARCHAR(3)" +
                                 $");";
            
            // SQL connection object
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);               // Define connection
                connection.Open();                                              // Open connection
                try
                {
                    SqlCommand command = new SqlCommand(createCountryTable,     // Define command
                        connection);         
                    command.ExecuteNonQuery();                                  // Send command
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"InitTables(): Failed to create table with: '{createCountryTable}'");
                    Console.WriteLine($"\n{ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static void PopulateTable()
        {
            List<CountryModel> countryList = GetCountriesFromJson();

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
                        command.CommandText = "INSERT INTO Country VALUES (" +
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
