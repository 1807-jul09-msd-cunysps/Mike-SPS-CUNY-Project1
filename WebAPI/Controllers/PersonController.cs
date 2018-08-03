using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models.Person;
using BusinessLogic;
using DataAccess;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]   
    public class PersonController : ApiController
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);

        [HttpGet]
        public JsonResult<IEnumerable<PersonModel>> Get()
        {
            return Json<IEnumerable<PersonModel>>(PersonSqlDbAccess.GetAll());
        }

        [HttpGet]
        public JsonResult<PersonModel> Get(Guid Id)
        {
            return Json<PersonModel>(PersonSqlDbAccess.GetPersonById(Id));
        }

        [HttpPost]
        public IHttpActionResult Post(PersonModel p)
        {
            if (p != null)
            {
                try
                {
                    // Convert ISO2 to CountryModel objects
                    p.Address.FK_Country = Guid.Empty;
                    if (p.Address.CountryISO2 != null)
                    {
                        p.Address.FK_Country = CountrySqlDbAccess.GetByISO2(p.Address.CountryISO2).Id;
                    }
                    p.ContactInfo.FK_Country = Guid.Empty;
                    if (p.Address.CountryISO2 != null)
                    {
                        p.ContactInfo.FK_Country = CountrySqlDbAccess.GetByISO2(p.ContactInfo.CountryISO2).Id;
                    }
                    // Create new Guid for Person, Address, ContactInfo
                    p.Id = p.Address.Id = p.ContactInfo.Id = Guid.NewGuid();

                    PersonSqlDbAccess.Add(p);
                    logger.Info($"PersonController.Post successfully added {p.Print()}");
                    return Ok();
                }
                catch (Exception ex)
                {
                    logger.Info($"PersonController.Post threw error {ex.Message} trying to add {p.Print()}");
                    return BadRequest();
                }
            }
            else
            {
                logger.Info($"PersonController.Post failed to add {p.Print()}");
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult Put(Guid Id, [FromBody] PersonModel p)
        {
            if (p != null)
            {
                try
                {
                    PersonModel original = PersonSqlDbAccess.GetPersonById(Id);

                    if (p.Firstname != null) original.Firstname = p.Firstname;
                    if (p.Lastname != null) original.Lastname = p.Lastname;
                    if (p.Age != null) original.Age = p.Age;
                    if (p.Gender != null) original.Gender = p.Gender;

                    if (p.Address.AddrLine1 != null) original.Address.AddrLine1 = p.Address.AddrLine1;
                    if (p.Address.AddrLine2 != null) original.Address.AddrLine2 = p.Address.AddrLine2;
                    if (p.Address.City != null) original.Address.City = p.Address.City;
                    if (p.Address.State != null) original.Address.State = p.Address.State;
                    if (p.Address.CountryISO2 != null) original.Address.FK_Country = CountrySqlDbAccess.GetByISO2(p.Address.CountryISO2).Id;
                    if (p.Address.Zipcode != null) original.Address.Zipcode = p.Address.Zipcode;

                    if (p.ContactInfo.CountryISO2 != null) original.ContactInfo.FK_Country = CountrySqlDbAccess.GetByISO2(p.ContactInfo.CountryISO2).Id;
                    if (p.ContactInfo.Number != null) original.ContactInfo.Number = p.ContactInfo.Number;
                    if (p.ContactInfo.Ext != null) original.ContactInfo.Ext = p.ContactInfo.Ext;
                    if (p.ContactInfo.Email != null) original.ContactInfo.Email = p.ContactInfo.Email;

                    PersonSqlDbAccess.Update(original);
                    logger.Info($"PersonController.Put successfully edited {p.Print()}");
                    return Ok();
                }
                catch (Exception ex)
                {
                    logger.Info($"PersonController.Put threw error {ex.Message} trying to edit {p.Print()}");
                    return BadRequest();
                }
            }
            else
            {
                logger.Info($"PersonController.Put failed to edit {p.Print()}");
                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid Id)
        {
            try
            {
                PersonSqlDbAccess.Delete(Id);
                logger.Info($"PersonController.Delete successfully deleted pid {Id}");
                return Ok();
            }
            catch (Exception ex)
            {
                logger.Info($"PersonController.Delete threw error {ex.Message} trying to delete {Id}");
                return BadRequest();
            }
        }
    }
}
