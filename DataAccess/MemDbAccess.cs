//using System;
//using System.Collections.Generic;
//using System.Linq;
//using DataAccess;
//using Models.Person;

//namespace DataAccess
//{
//    public static class MemDbAccess
//    {
//        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);

//        public static List<PersonModel> contacts = new List<PersonModel>();

//        public static bool Add(string firstName = null,
//                               string lastName = null,
//                               string houseNum = null,
//                               string street = null,
//                               string city = null,
//                               State state = State.NULL,
//                               Country country = Country.NULL,
//                               string zipcode = null,
//                               string areaCode = null,
//                               string number = null,
//                               string ext = null)
//        {
//            try
//            {
//                // Create PersonModel ID
//                int Pid = (int) DateTime.Now.Ticks;
//                // Create Phone
//                PhoneModel phone = new PhoneModel();
//                phone.Id = (int)DateTime.Now.Ticks;
//                phone.PersonId = Pid;
//                phone.CountryCode = country;
//                phone.AreaCode = areaCode;
//                phone.Number = number;
//                phone.Ext = ext;
//                // Create Address
//                AddressModel addr = new AddressModel();
//                addr.Id = (int)DateTime.Now.Ticks;
//                addr.PersonId = Pid;
//                addr.HouseNum = houseNum;
//                addr.Street = street;
//                addr.City = city;
//                addr.State = state;
//                addr.Country = country;
//                addr.Zipcode = zipcode;
//                // Create PersonModel
//                PersonModel person = new PersonModel();
//                person.Id = Pid;
//                person.Firstname = firstName;
//                person.Lastname = lastName;
//                person.Address = addr;
//                person.Phone = phone;

//                // Add new person to contact list
//                contacts.Add(person);

//                // Update SQL Server
//                Pid = MessageSqlDbAccess.Add(person);
//                person.Id = Pid;
//                phone.PersonId = Pid;
//                addr.PersonId = Pid;

//                logger.Info($"Added new person to DB: {person.Print()}");
//                return true;
//            }
//            catch(Exception e)
//            {
//                logger.Info($"ContactDataAccess.Add: {e.Message}");
//                return false;
//            }
//        }

//        public static bool Update(string firstName = null,
//                                  string lastName = null,
//                                  string houseNum = null,
//                                  string street = null,
//                                  string city = null,
//                                  State state = State.NULL,
//                                  Country country = Country.NULL,
//                                  string zipcode = null,
//                                  string areaCode = null,
//                                  string number = null,
//                                  string ext = null)
//        {
//            try
//            {
//                // Find the user by first name last name
//                var query = (from p in contacts
//                             where p.Firstname == firstName
//                             && p.Lastname == lastName
//                             select p).ToList();
//                // Throw error if more than one result
//                if (query.Count > 1)
//                {
//                    Console.WriteLine($"Error, multiple contacts with the name {firstName} {lastName}");
//                    return false;
//                }
//                // Get PersonModel
//                PersonModel person = query[0];
//                // Edit Phone
//                PhoneModel phone = person.Phone;
//                if (country != Country.NULL) phone.CountryCode = country;
//                if (areaCode != null) phone.AreaCode = areaCode;
//                if (number != null) phone.Number = number;
//                if (ext != null) phone.Ext = ext;
//                // Edit Address
//                AddressModel addr = person.Address;
//                if (houseNum != null) addr.HouseNum = houseNum;
//                if (street != null) addr.Street = street;
//                if (city != null) addr.City = city;
//                if (state != State.NULL) addr.State = state;
//                if (country != Country.NULL) addr.Country = country;

//                // Update SQL DB
//                MessageSqlDbAccess.Update(person);

//                logger.Info($"Edited person in DB: {person.Print()}");
//                return true;
//            }
//            catch (Exception e)
//            {
//                logger.Info(e.Message);
//                return false;
//            }
//        }

//        public static bool Delete(string firstName = null,
//                                  string lastName = null)
//        {
//            try
//            {
//                // Find the user by first name last name
//                var query = (from p in contacts
//                             where p.Firstname == firstName
//                             && p.Lastname == lastName
//                             select p).ToList();
//                // Throw error if more than one result
//                if (query.Count > 1)
//                {
//                    Console.WriteLine($"Error, multiple contacts with the name {firstName} {lastName}");
//                    return false;
//                }
//                // Get PersonModel
//                PersonModel person = query[0];
//                // Remove PersonModel from DB
//                contacts.Remove(person);

//                // Update SQL DB
//                MessageSqlDbAccess.Delete(person.Id);

//                logger.Info($"Deleted person from DB: {person.Print()}");

//                return true;
//            }
//            catch (Exception e)
//            {
//                logger.Info(e.Message);
//                return false;
//            }
//        }

//        public static List<PersonModel> Search(string query)
//        {
//            // List to return query results
//            List<PersonModel> results = new List<PersonModel>();
//            // Search firstname, lastname, zipcode, city, and phone number for query            
//            results = ( from p in contacts
//                        where p.Firstname.Contains(query) ||
//                              p.Lastname.Contains(query) ||
//                              p.Address.City.Contains(query) ||
//                              p.Address.Zipcode.Contains(query) ||
//                              p.Phone.Number.Contains(query)
//                        select p).ToList();
//            // Return query results
//            return results;
//        }
//    }
//}
