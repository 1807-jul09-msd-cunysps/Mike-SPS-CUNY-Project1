using System;
using System.Collections.Generic;
using Models.Person;
using Models.ContactMe;
using System.Data;              // ADO.NET lib
using System.Data.SqlClient;    // Client in ADO.NET library

namespace DataAccess
{
    public static class MessageSqlDbAccess
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);

        private static string connectionString = "Data Source=rev-training-mc-dbs.database.windows.net;" +   // SQL Server
                                                 "Initial Catalog=rev-training-mc-messages-db;" +            // SQL DB
                                                 "Persist Security Info=True;" +                             // Security
                                                 "MultipleActiveResultSets=True;" +                          // MARS
                                                 "User ID=;" +                                       // User name
                                                 "Password=";                                       // Password

        public static int Add(MessageModel msg)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlTransaction transaction = connection.BeginTransaction(); // Create transaction
                SqlCommand command = connection.CreateCommand();            // Create command

                try
                {
                    // INSERT for person
                    command.CommandText = $"INSERT INTO Message VALUES (" +
                                          $"'{msg.FullName}', " +
                                          $"'{msg.Email}'," +
                                          $"'{msg.Message}', " +
                                          $"'{false}' " +
                                          ");";
                    command.ExecuteNonQuery();

                    // Retrieve Person ID for person we just submitted
                    int MsgId;
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        MsgId = Int32.Parse(reader[0].ToString());
                    }
                    else
                    {
                        throw new Exception("Failed to ExecuteReader for MsgId");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    logger.Info($"MessageSqlDbAccess.Add threw: {ex.Message}");
                }
            }
            return -1;
        }

        public static void Update(MessageModel newInfo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlTransaction transaction = connection.BeginTransaction(); // Create transaction
                SqlCommand command = connection.CreateCommand();            // Create command

                try
                {
                    command.CommandText = $"UPDATE Message " +
                                          $"SET Read = '{newInfo.Read}', " +
                                          $"FullName = '{newInfo.FullName}', " +
                                          $"Email = '{newInfo.Email}', " +
                                          $"Message = '{newInfo.Message}' " +
                                          $"WHERE Id = {newInfo.Id};";
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    logger.Info($"MessageSqlDbAccess.Update: {ex.Message}");
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

                try
                {
                    command.CommandText = $"DELETE FROM Message WHERE Id = {id};";
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    logger.Info($"MessageSqlDbAccess.Delete threw: {ex.Message}");
                }
            }
        }

        public static MessageModel GetById(int id)
        {
            // Person to return
            MessageModel msg = null;
            // SQL interaction
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlCommand command = connection.CreateCommand();            // Create command
                try
                {
                    // Search firstname, lastname, zipcode, city, and phone number for query            
                    command.CommandText = $"DELETE FROM Message WHERE Id = {id};";

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        msg = new MessageModel()
                        {
                            Id = Int32.Parse(reader[0].ToString()),
                            Read = Boolean.Parse(reader[1].ToString()),
                            FullName = reader[2].ToString(),
                            Email = reader[3].ToString(),
                            Message = reader[4].ToString()
                        };
                    }
                    reader.Close();
                    if (msg == null)
                    {
                        throw new Exception($"No Message exists with ID: {id}");
                    }
                }
                catch (Exception ex)
                {
                    logger.Info($"MessageSqlDbAccess.GetById threw:  {ex.Message}");
                }
            }
            return msg;
        }

        public static List<MessageModel> Search(string s)
        {
            string query = s.ToLower();
            // List to return query results
            List<MessageModel> results = new List<MessageModel>();
            // SQL interaction
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlCommand command = connection.CreateCommand();            // Create command

                try
                {
                    // Search firstname, lastname, zipcode, city, and phone number for query            
                    command.CommandText = $"SELECT * FROM Message AS m " +
                                          $"WHERE LOWER(m.FullName)         = '{query}' " +
                                          $"OR LOWER(m.Email)               = '{query}' " +
                                          $"OR LOWER(m.Message)             = '{query}' " +
                                          $";";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        MessageModel msg = new MessageModel()
                        {
                            Id = Int32.Parse(reader[0].ToString()),
                            Read = Boolean.Parse(reader[1].ToString()),
                            FullName = reader[2].ToString(),
                            Email = reader[3].ToString(),
                            Message = reader[4].ToString()
                        };
                        results.Add(msg);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    logger.Info($"MessageSqlDbAccess.Search threw:  {ex.Message}");
                }
            }
            return results;
        }

        public static List<MessageModel> GetAll()
        {
            // List to return query results
            List<MessageModel> results = new List<MessageModel>();
            // SQL interaction
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlCommand command = connection.CreateCommand();            // Create command

                try
                {
                    command.CommandText = $"SELECT * FROM Message;";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        MessageModel msg = new MessageModel()
                        {
                            Id = Int32.Parse(reader[0].ToString()),
                            Read = Boolean.Parse(reader[1].ToString()),
                            FullName = reader[2].ToString(),
                            Email = reader[3].ToString(),
                            Message = reader[4].ToString()
                        };
                        results.Add(msg);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    logger.Info($"MessageSqlDbAccess.GetAll threw: {ex.Message}");
                }
            }
            return results;
        }
    }
}
