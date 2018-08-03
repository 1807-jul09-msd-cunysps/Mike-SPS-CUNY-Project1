using System;
using System.Collections.Generic;
using Models.Person;
using System.Data;              // ADO.NET lib
using System.Data.SqlClient;    // Client in ADO.NET library

namespace DataAccess
{
    public static class CountrySqlDbAccess
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);

        private static string connectionString = "Data Source=rev-training-mc-dbs.database.windows.net;" +   // SQL Server
                                                 "Initial Catalog=rev-training-mc-contacts-db;" +            // SQL DB
                                                 "Persist Security Info=True;" +                             // Security
                                                 "MultipleActiveResultSets=True;" +                          // MARS
                                                 "User ID=revature;" +                                       // User name
                                                 "Password=Password1";                                       // Password

        public static CountryModel GetByISO2(string iso2)
        {
            // Person to return
            CountryModel c = null;
            // SQL interactionCountryModel
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlCommand command = connection.CreateCommand();            // Create command
                try
                {
                    // Search firstname, lastname, zipcode, city, and phone number for query            
                    command.CommandText = $"SELECT * FROM Country WHERE ISO2 = '{iso2}';";

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        c = new CountryModel()
                        {
                            Id = new Guid(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            PhoneCode = reader[2].ToString(),
                            ISO2 = reader[3].ToString(),
                            ISO3 = reader[4].ToString()
                        };
                    }
                    reader.Close();
                    if (c == null)
                    {
                        throw new Exception($"No Country exists with ISO2: {iso2}");
                    }
                }
                catch (Exception ex)
                {
                    logger.Info($"MessageSqlDbAccess.GetById threw:  {ex.Message}");
                }
            }
            return c;
        }
    }
}
