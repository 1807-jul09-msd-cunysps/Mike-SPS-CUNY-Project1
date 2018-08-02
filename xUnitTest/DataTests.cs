//using Xunit;
//using Xunit.Abstractions;
//using System.Collections.Generic;
//using Models;
//using DataAccess;

//namespace xUnitTest
//{

//    public class DataAccessTests
//    {
//        private readonly ITestOutputHelper output;
//        public static string testFileName = "test-contacts.dat";
//        public static List<PersonModel> testContacts = new List<PersonModel>();

//        public DataAccessTests(ITestOutputHelper output)
//        {
//            this.output = output;
//        }

//        string[] firstnames = { "Mike", "Kate", "Sally" };
//        string[] lastNames = { "Powell", "Sanders", "Smith" };
//        string[] houseNums = { "123", "456", "789" };
//        string[] streets = { "Main", "Broad", "Madison" };
//        string[] cities = { "Miami", "Chesapeake", "Columbus" };
//        State[] states = { State.FL, State.MD, State.OH };
//        Country[] countries = { Country.Australia, Country.India, Country.Pakistan };
//        string[] zipcodes = { "43564", "12o23", "34567" };
//        string[] areaCodes = { "321", "654", "987" };
//        string[] numbers = { "123456", "234567", "6948747" };
//        string[] exts = { "001", "002", "003" };

//        [Fact]
//        public void TestAdd()
//        {
//            //
//            // Add three people to contacts
//            for (int i = 0; i < firstnames.Length; i++)
//            {
//                MemDbAccess.Add(firstName: firstnames[i],
//                                      lastName: lastNames[i],
//                                      houseNum: houseNums[i],
//                                      street: streets[i],
//                                      city: cities[i],
//                                      state: states[i],
//                                      country: countries[i],
//                                      zipcode: zipcodes[i],
//                                      areaCode: areaCodes[i],
//                                      number: numbers[i],
//                                      ext: exts[i]);
//            }

//            // Debug output for Add
//            output.WriteLine($"Contacts after Add:");
//            foreach (PersonModel p in MemDbAccess.contacts)
//                output.WriteLine(p.Print());

//            // Assert 3 Person were added
//            Assert.Equal(3, MemDbAccess.contacts.Count);
//        }

//        [Fact]
//        public void TestEdit()
//        {
//            // Assert Edit Mike Powell
//            Assert.True(MemDbAccess.Update(firstName: "Mike",
//                                          lastName: "Powell",
//                                          houseNum: null,
//                                          street: "999",
//                                          city: null,
//                                          state: State.NULL,
//                                          country: Country.UK,
//                                          zipcode: "99999",
//                                          areaCode: "999",
//                                          number: "9999999",
//                                          ext: null));

//            // Debug output for Update
//            output.WriteLine($"\nContacts after Update 'Mike Powell':");
//            foreach (PersonModel p in MemDbAccess.contacts)
//                output.WriteLine(p.Print());
//        }

//        [Fact]
//        public void TestSearch()
//        {
//            List<PersonModel> results = new List<PersonModel>();
//            // Queries to test first name, last name, city, zip code, and phone number
//            string[] queries = new string[] { "Mike", "Smith", "Miami", "43564", "6948747" };
//            foreach (string q in queries)
//            {
//                // Search for query string
//                results = MemDbAccess.Search(q);
//                // Debug output for Query
//                output.WriteLine($"\nSearch results with query '{q}':");
//                foreach (PersonModel p in results)
//                    output.WriteLine(p.Print());
//                // Assert one return
//                Assert.Single(results);
//            }
//        }

//        [Fact]
//        public void TestDelete()
//        {
//            //
//            // Delete Kate Sanders from contacts
//            MemDbAccess.Delete(firstName: "Kate", lastName: "Sanders");

//            // Debug output for Delete
//            output.WriteLine($"\nContacts after Deleting Kate Powell:");
//            foreach (PersonModel p in MemDbAccess.contacts)
//                output.WriteLine(p.Print());

//            // Assert Delete
//            Assert.Equal(2, MemDbAccess.contacts.Count);
//        }

//        [Fact]
//        public void TestSerialization()
//        {
//            //
//            // Serialization Test
//            string json = FileDbAccess.PersonModelListToJSON(MemDbAccess.contacts);
//            testContacts = FileDbAccess.JSONToPersonModelList(json);

//            // Compare List<Person>s
//            string listAsStringPreConversions = "", listAsStringPostConversions = "";
//            for (int i = 0; i < testContacts.Count; i++)
//            {
//                listAsStringPreConversions += MemDbAccess.contacts[i].Print();
//                listAsStringPostConversions += testContacts[i].Print();
//            }

//            // Debug output for serialization
//            output.WriteLine($"\nSerialization Test");
//            output.WriteLine($"\nlistAsStringPreConversions = \n{listAsStringPreConversions}");
//            output.WriteLine($"\nlistAsStringPostConversions = \n{listAsStringPostConversions}");

//            // Serialization Assert
//            Assert.True(listAsStringPreConversions == listAsStringPostConversions);
//        }

//        [Fact]
//        public void TestFileIO()
//        { 
//            //
//            // File IO Test
//            bool writeTest = FileDbAccess.WriteContacts(MemDbAccess.contacts, testFileName);
//            testContacts = FileDbAccess.GetContacts(testFileName);

//            output.WriteLine($"ContactDataAccess.contacts:");
//            foreach (PersonModel p in MemDbAccess.contacts)
//            {
//                output.WriteLine(p.Print());
//            }

//            output.WriteLine($"\ntestContacts:");
//            foreach (PersonModel p in testContacts)
//            {
//                output.WriteLine(p.Print());
//            }

//            // Compare List<Person>s
//            string listAsStringPreConversions = "", listAsStringPostConversions = "";
//            for (int i = 0; i < testContacts.Count; i++)
//            {
//                listAsStringPreConversions += MemDbAccess.contacts[i].Print();
//                listAsStringPostConversions += testContacts[i].Print();
//            }
//            output.WriteLine($"\nFile IO Test");
//            output.WriteLine($"\nlistAsStringPreConversions = \n{listAsStringPreConversions}");
//            output.WriteLine($"\nlistAsStringPostConversions = \n{listAsStringPostConversions}");

//            // File IO Assert
//            Assert.True(listAsStringPreConversions == listAsStringPostConversions);
//        }
//    }
//}
