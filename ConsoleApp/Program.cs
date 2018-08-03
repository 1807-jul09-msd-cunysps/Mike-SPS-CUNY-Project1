using System;
using System.Collections.Generic;
using BusinessLogic;
using DataAccess;
using Models.ContactMe;
using Models.Person;

namespace ConsoleApp
{
    class Program
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);
        static void Main(string[] args)
        {

            CountryModel c = CountrySqlDbAccess.GetByISO2("US");
            Console.WriteLine(c.Print());

            Console.ReadLine();
        }
    }
}
