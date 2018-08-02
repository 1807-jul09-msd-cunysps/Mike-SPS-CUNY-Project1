using System;
using System.Collections.Generic;
using Models;
using DataAccess;
using NLog;

namespace proj0
{
    class Program
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);
        static void Main(string[] args)
        {
            PersonSqlDbAccess.Add(
                new Models.Person.PersonModel
                {
                    Firstname = "Mike",
                    Lastname = "Corso",
                    Age = 32,
                    Gender = "Male",
                    Address = new Models.Person.AddressModel
                    {
                        AddrLine1 = "32 West 32nd St",
                        AddrLine2 = "Room 218",
                        City = "New York",
                        State = "New York",
                        FK_Country = 1,
                        Zipcode = "10003"
                    },
                    ContactInfo = new Models.Person.ContactInfoModel
                    {
                        Number = "1234567890",
                        Ext = "",
                        Email = "mc5262@nyu.edu"
                    }
                }
            );

            Console.ReadLine();
        }
    }
}
