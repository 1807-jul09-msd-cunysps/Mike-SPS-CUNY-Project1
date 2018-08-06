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

            // 94C748BB - 0E61 - 41E0 - B2DF - 898638FA1AB1
            //DataAccess.PersonSqlDbAccess.Delete(new Guid("facc101e-1fef-444d-99d7-b98743d3d78e"));

            //        CountryModel c = CountrySqlDbAccess.GetByISO2("US");
            //Console.WriteLine(c.Print());

            //Console.ReadLine();
        }
    }
}
