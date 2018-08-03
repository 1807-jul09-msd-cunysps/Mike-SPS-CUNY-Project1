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
                    PersonSqlDbAccess.Add(p);
                    logger.Info($"PersonController.Post successfully added {p.Print()}");
                    return Ok();
                }
                catch(Exception ex)
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
        public IHttpActionResult Put(PersonModel p)
        {
            if (p != null)
            {
                try
                {
                    PersonSqlDbAccess.Update(p);
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
                logger.Info($"PersonController.Post failed to edit {p.Print()}");
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
