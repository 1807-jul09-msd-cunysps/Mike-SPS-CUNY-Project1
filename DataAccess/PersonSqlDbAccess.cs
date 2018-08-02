using System;
using System.Collections.Generic;
using Models.Person;
using System.Data;              // ADO.NET lib
using System.Data.SqlClient;    // Client in ADO.NET library

namespace DataAccess
{
    public static class PersonSqlDbAccess
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);

        private static string connectionString = "Data Source=rev-training-mc-dbs.database.windows.net;" +   // SQL Server
                                                 "Initial Catalog=rev-training-mc-contacts-db;" +            // SQL DB
                                                 "Persist Security Info=True;" +                             // Security
                                                 "MultipleActiveResultSets=True;" +                          // MARS
                                                 "User ID=;" +                                       // User name
                                                 "Password=";                                       // Password

        public static int Add(PersonModel person)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlTransaction transaction = connection.BeginTransaction(); // Create transaction
                SqlCommand command = connection.CreateCommand();            // Create command
                command.Transaction = transaction;                          // Assign transaction to command

                try
                {
                    // INSERT for person
                    command.CommandText = $"INSERT INTO Person VALUES (" +
                                          $"'{person.Firstname}', " +
                                          $"'{person.Lastname}'," +
                                          $"'{person.Age}'," +
                                          $"'{person.Gender}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // Retrieve Person ID for person we just submitted
                    int PersonId;
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        PersonId = Int32.Parse(reader[0].ToString());
                    }
                    else
                    {
                        throw new Exception("Failed to ExecuteReader for PersonId");
                    }
                    reader.Close();

                    // INSERT for ContactInfo
                    command.CommandText = $"INSERT INTO ContactInfo VALUES (" +
                                          $"{PersonId}," +
                                          $"'{person.ContactInfo.FK_Country}'," +
                                          $"'{person.ContactInfo.Number}'," +
                                          $"'{person.ContactInfo.Ext}'," +
                                          $"'{person.ContactInfo.Email}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // INSERT for Address
                    command.CommandText = "INSERT INTO Address VALUES (" +
                                          $"{PersonId}, " +
                                          $"'{person.Address.AddrLine1}', " +
                                          $"'{person.Address.AddrLine2}', " +
                                          $"'{person.Address.City}', " +
                                          $"'{person.Address.State}', " +
                                          $"'{person.Address.FK_Country}', " +
                                          $"'{person.Address.Zipcode}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // Commit transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    logger.Info($"PersonSqlDbAccess.Add threw: {ex.Message}");
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
            return -1;
        }

        public static void Update(PersonModel newInfo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlTransaction transaction = connection.BeginTransaction(); // Create transaction
                SqlCommand command = connection.CreateCommand();            // Create command
                command.Transaction = transaction;                          // Assign transaction to command

                try
                {
                    // UPDATE for person
                    command.CommandText = $"UPDATE Person " +
                                          $"SET Firstname = '{newInfo.Firstname}', " +
                                          $"Lastname = '{newInfo.Lastname}', " +
                                          $"Age = '{newInfo.Age}', " +
                                          $"Gender = '{newInfo.Gender}' " +
                                          $"WHERE Id = {newInfo.Id};";
                    command.ExecuteNonQuery();

                    // UPDATE for phone
                    command.CommandText = $"UPDATE ContactInfo " +
                                          $"SET FK_Country = '{newInfo.ContactInfo.FK_Country}', " +
                                          $"Number = '{newInfo.ContactInfo.Number}', " +
                                          $"Ext = '{newInfo.ContactInfo.Ext}', " +
                                          $"Email = '{newInfo.ContactInfo.Email}' " +
                                          $"WHERE FK_Person = {newInfo.Id};";
                    command.ExecuteNonQuery();

                    // UPDATE for address
                    command.CommandText = $"UPDATE Address " +
                                          $"SET AddrLine1 = '{newInfo.Address.AddrLine1}', " +
                                          $"AddrLine2 = '{newInfo.Address.AddrLine2}', " +
                                          $"City = '{newInfo.Address.City}', " +
                                          $"State = '{newInfo.Address.State}', " +
                                          $"FK_Country = '{newInfo.Address.FK_Country}', " +
                                          $"Zipcode = '{newInfo.Address.Zipcode}' " +
                                          $"WHERE FK_Person = {newInfo.Id};";
                    command.ExecuteNonQuery();

                    // Commit transaction
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    logger.Info($"DataAccessADOSQL.DataAccess.Update: {ex.Message}");
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
        }

        public static void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlTransaction transaction = connection.BeginTransaction(); // Create transaction
                SqlCommand command = connection.CreateCommand();            // Create command
                command.Transaction = transaction;                          // Assign transaction to command

                try
                {
                    // DELETE for Phone
                    command.CommandText = $"DELETE FROM ContactInfo WHERE phone.FK_Person = {id};";
                    command.ExecuteNonQuery();
                    // DELETE for Address
                    command.CommandText = $"DELETE FROM Address WHERE address.FK_Person = {id};";
                    command.ExecuteNonQuery();
                    // DELETE for Person
                    command.CommandText = $"DELETE FROM person WHERE person.id = {id};";
                    command.ExecuteNonQuery();
                    // Commit transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    logger.Info($"PersonSqlDbAccess.Delete threw: {ex.Message}");
                    try
                    {
                        transaction.Rollback();                             // Roll back if exception
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }

        public static PersonModel GetPersonById(int id)
        {
            // Person to return
            PersonModel p = null;
            // SQL interaction
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlCommand command = connection.CreateCommand();            // Create command

                try
                {
                    // Search firstname, lastname, zipcode, city, and phone number for query            
                    command.CommandText = $"SELECT * FROM Person AS p " +
                                          $"LEFT JOIN ContactInfo AS c ON p.ID = c.FK_Person " +
                                          $"LEFT JOIN address AS a ON p.ID = a.FK_Person " +
                                          $"WHERE LOWER(p.Firstname) = '{id}' " +
                                          $";";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        p = new PersonModel()
                        {
                            Id = Int32.Parse(reader[0].ToString()),
                            Firstname = reader[1].ToString(),
                            Lastname = reader[2].ToString(),
                            Age = Int32.Parse(reader[3].ToString()),
                            Gender = reader[4].ToString(),
                            ContactInfo = new ContactInfoModel
                            {
                                Id = Int32.Parse(reader[5].ToString()),
                                FK_Person = Int32.Parse(reader[6].ToString()),
                                FK_Country = Int32.Parse(reader[7].ToString()),
                                Number = reader[8].ToString(),
                                Ext = reader[9].ToString(),
                                Email = reader[10].ToString()
                            },
                            Address = new AddressModel
                            {
                                Id = Int32.Parse(reader[11].ToString()),
                                FK_Person = Int32.Parse(reader[12].ToString()),
                                AddrLine1 = reader[13].ToString(),
                                AddrLine2 = reader[14].ToString(),
                                City = reader[15].ToString(),
                                State = reader[16].ToString(),
                                FK_Country = Int32.Parse(reader[17].ToString()),
                                Zipcode = reader[18].ToString()
                            }
                        };
                    }
                    reader.Close();
                    if (p == null)
                    {
                        throw new Exception($"No Person exists with ID: {id}");
                    }
                }
                catch (Exception ex)
                {
                    logger.Info($"PersonSqlDbAccess.GetPersonById threw:  {ex.Message}");
                }
            }
            return p;
        }

        public static List<PersonModel> Search(string s)
        {
            string query = s.ToLower();
            // List to return query results
            List<PersonModel> results = new List<PersonModel>();
            // SQL interaction
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlCommand command = connection.CreateCommand();            // Create command

                try
                {
                    // Search firstname, lastname, zipcode, city, and phone number for query            
                    command.CommandText = $"SELECT * FROM Person AS p " +
                                          $"LEFT JOIN ContactInfo AS c ON p.ID = c.FK_Person " +
                                          $"LEFT JOIN address AS a ON p.ID = a.FK_Person " +
                                          $"WHERE LOWER(p.Firstname)    = '{query}' " +
                                          $"OR LOWER(p.Lastname)        = '{query}' " +
                                          $"OR LOWER(p.Age)             = '{query}' " +
                                          $"OR LOWER(p.Gender)          = '{query}' " +
                                          $"OR LOWER(c.Number)          = '{query}' " +
                                          $"OR LOWER(c.Ext)             = '{query}' " +
                                          $"OR LOWER(c.Email)           = '{query}' " +
                                          $"OR LOWER(a.AddrLine1)       = '{query}' " +
                                          $"OR LOWER(a.AddrLine2)       = '{query}' " +
                                          $"OR LOWER(a.City)            = '{query}' " +
                                          $"OR LOWER(a.State)           = '{query}' " +
                                          $"OR LOWER(a.Zipcode)         = '{query}' " +
                                          $";";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PersonModel p = new PersonModel()
                        {
                            Id = Int32.Parse(reader[0].ToString()),
                            Firstname = reader[1].ToString(),
                            Lastname = reader[2].ToString(),
                            Age = Int32.Parse(reader[3].ToString()),
                            Gender = reader[4].ToString(),
                            ContactInfo = new ContactInfoModel
                            {
                                Id = Int32.Parse(reader[5].ToString()),
                                FK_Person = Int32.Parse(reader[6].ToString()),
                                FK_Country = Int32.Parse(reader[7].ToString()),
                                Number = reader[8].ToString(),
                                Ext = reader[9].ToString(),
                                Email = reader[10].ToString()
                            },
                            Address = new AddressModel
                            {
                                Id = Int32.Parse(reader[11].ToString()),
                                FK_Person = Int32.Parse(reader[12].ToString()),
                                AddrLine1 = reader[13].ToString(),
                                AddrLine2 = reader[14].ToString(),
                                City = reader[15].ToString(),
                                State = reader[16].ToString(),
                                FK_Country = Int32.Parse(reader[17].ToString()),
                                Zipcode = reader[18].ToString()
                            }
                        };
                        results.Add(p);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    logger.Info($"PersonSqlDbAccess.Search threw:  {ex.Message}");
                }
            }
            return results;
        }

        public static List<PersonModel> GetAll()
        {
            // List to return query results
            List<PersonModel> results = new List<PersonModel>();
            // SQL interaction
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlCommand command = connection.CreateCommand();            // Create command

                try
                {
                    // Search firstname, lastname, zipcode, city, and phone number for query            
                    command.CommandText = $"SELECT * FROM Person AS p" +
                                          $"LEFT JOIN ContactInfo AS c ON p.ID = c.FK_Person " +
                                          $"LEFT JOIN address AS a ON p.ID = a.FK_Person " +
                                          $";";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PersonModel p = new PersonModel()
                        {
                            Id = Int32.Parse(reader[0].ToString()),
                            Firstname = reader[1].ToString(),
                            Lastname = reader[2].ToString(),
                            Age = Int32.Parse(reader[3].ToString()),
                            Gender = reader[4].ToString(),
                            ContactInfo = new ContactInfoModel
                            {
                                Id = Int32.Parse(reader[5].ToString()),
                                FK_Person = Int32.Parse(reader[6].ToString()),
                                FK_Country = Int32.Parse(reader[7].ToString()),
                                Number = reader[8].ToString(),
                                Ext = reader[9].ToString(),
                                Email = reader[10].ToString()
                            },
                            Address = new AddressModel
                            {
                                Id = Int32.Parse(reader[11].ToString()),
                                FK_Person = Int32.Parse(reader[12].ToString()),
                                AddrLine1 = reader[13].ToString(),
                                AddrLine2 = reader[14].ToString(),
                                City = reader[15].ToString(),
                                State = reader[16].ToString(),
                                FK_Country = Int32.Parse(reader[17].ToString()),
                                Zipcode = reader[18].ToString()
                            }
                        };
                        results.Add(p);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    logger.Info($"PersonSqlDbAccess.GetAll threw: {ex.Message}");
                }
            }
            return results;
        }
    }
}
