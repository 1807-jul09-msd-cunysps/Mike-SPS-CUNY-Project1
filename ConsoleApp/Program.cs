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
            //Models.Person.Resources.Country.Countries.PopulateTable();


            //PersonSqlDbAccess.Update(
            //    new Models.Person.PersonModel
            //    {
            //        Id = new Guid("D221499C-1239-44AB-8D15-C3D386CAC98C"),
            //        Firstname = "John",
            //        Lastname = "Corso",
            //        Age = 32,
            //        Gender = "Male",
            //        Address = new Models.Person.AddressModel
            //        {
            //            Id = Guid.NewGuid(),
            //            AddrLine1 = "32 West 32nd St",
            //            AddrLine2 = "Room 218",
            //            City = "New York",
            //            State = "New York",
            //            FK_Country = new Guid("DC9E0EF0-EC74-4E4D-A7DA-00754C3D2616"),
            //            Zipcode = "10003"
            //        },
            //        ContactInfo = new Models.Person.ContactInfoModel
            //        {
            //            Id = Guid.NewGuid(),
            //            FK_Country = new Guid("DC9E0EF0-EC74-4E4D-A7DA-00754C3D2616"),
            //            Number = "1234567890",
            //            Ext = "",
            //            Email = "mc5262@nyu.edu"
            //        }
            //    }
            //);

            //PersonSqlDbAccess.Delete(new Guid("A46C1A56-E477-4B60-B754-5E78AA2E1919"));

            //PersonSqlDbAccess.GetPersonById(new Guid("0C9DD546-145D-4BF3-956C-060FBC3807B5"));

            List<PersonModel> people = PersonSqlDbAccess.Search("John");
            foreach (PersonModel p in people)
            {
                Console.WriteLine(p.Print());
            }

            Console.ReadLine();
        }
    }
}
