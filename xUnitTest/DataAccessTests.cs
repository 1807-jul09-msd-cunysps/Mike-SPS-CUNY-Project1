using Xunit;
using Xunit.Abstractions;
using System;
using DataAccess;
using Models.Person;
using System.Collections.Generic;

namespace xUnitTest
{

    public class DataAccessTests
    {
        private readonly ITestOutputHelper output;

        public DataAccessTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test_PersonSqlDbCRUD()
        {
            Guid[] guids = new Guid[] { new Guid("F7AB131E-354C-4698-A673-BB27D0D0D64F"),
                                        new Guid("92115905-9A48-4A17-9521-E35560F74674") };

            /* Create */

            // DB before changes
            List<PersonModel> before = PersonSqlDbAccess.GetAll();
            // Changes
            PersonSqlDbAccess.Add(
                new Models.Person.PersonModel
                {
                    Id = guids[0],
                    Firstname = "Mike",
                    Lastname = "Corso",
                    Age = 32,
                    Gender = "Male",
                    Address = new Models.Person.AddressModel
                    {
                        Id = Guid.NewGuid(),
                        AddrLine1 = "32 West 32nd St",
                        AddrLine2 = "Room 218",
                        City = "New York",
                        State = "New York",
                        FK_Country = new Guid("DC9E0EF0-EC74-4E4D-A7DA-00754C3D2616"),
                        Zipcode = "10003"
                    },
                    ContactInfo = new Models.Person.ContactInfoModel
                    {
                        Id = Guid.NewGuid(),
                        FK_Country = new Guid("DC9E0EF0-EC74-4E4D-A7DA-00754C3D2616"),
                        Number = "1234567890",
                        Ext = "",
                        Email = "mc5262@nyu.edu"
                    }
                }
            );
            PersonSqlDbAccess.Add(
                new Models.Person.PersonModel
                {
                    Id = guids[1],
                    Firstname = "Samantha",
                    Lastname = "Corso",
                    Age = 25,
                    Gender = "Female",
                    Address = new Models.Person.AddressModel
                    {
                        Id = Guid.NewGuid(),
                        AddrLine1 = "31 West 32nd St",
                        AddrLine2 = "Room 217",
                        City = "New York",
                        State = "New York",
                        FK_Country = new Guid("DC9E0EF0-EC74-4E4D-A7DA-00754C3D2616"),
                        Zipcode = "10004"
                    },
                    ContactInfo = new Models.Person.ContactInfoModel
                    {
                        Id = Guid.NewGuid(),
                        FK_Country = new Guid("DC9E0EF0-EC74-4E4D-A7DA-00754C3D2616"),
                        Number = "349667890",
                        Ext = "",
                        Email = "sc5262@nyu.edu"
                    }
                }
            );
            // DB after changes
            List<PersonModel> after = PersonSqlDbAccess.GetAll();
            // Assert
            Assert.True(after.Count - before.Count == 2);

            /* Update */

            // DB before changes
            before = PersonSqlDbAccess.Search("John");
            // Changes
            PersonSqlDbAccess.Update(
                new Models.Person.PersonModel
                {
                    Id = guids[0],
                    Firstname = "John",
                    Lastname = "Corso",
                    Age = 32,
                    Gender = "Male",
                    Address = new Models.Person.AddressModel
                    {
                        Id = Guid.NewGuid(),
                        AddrLine1 = "32 West 32nd St",
                        AddrLine2 = "Room 218",
                        City = "New York",
                        State = "New York",
                        FK_Country = new Guid("DC9E0EF0-EC74-4E4D-A7DA-00754C3D2616"),
                        Zipcode = "10003"
                    },
                    ContactInfo = new Models.Person.ContactInfoModel
                    {
                        Id = Guid.NewGuid(),
                        FK_Country = new Guid("DC9E0EF0-EC74-4E4D-A7DA-00754C3D2616"),
                        Number = "1234567890",
                        Ext = "",
                        Email = "mc5262@nyu.edu"
                    }
                }
            );
            // After changes
            after = PersonSqlDbAccess.Search("John");
            // Assert
            Assert.True(after.Count - before.Count == 1);

            /* Delete */
            
            // Changes
            PersonSqlDbAccess.Delete(guids[0]);
            PersonSqlDbAccess.Delete(guids[1]);
            // Assert
            Assert.True(PersonSqlDbAccess.GetPersonById(guids[0]) == null);
            Assert.True(PersonSqlDbAccess.GetPersonById(guids[1]) == null);
        }
    }
}
