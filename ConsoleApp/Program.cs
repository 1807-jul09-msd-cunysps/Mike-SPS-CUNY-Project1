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

            MessageSqlDbAccess.Update(
                new Models.ContactMe.MessageModel
                {
                    Id = new Guid("F7AB131E-354C-4698-A673-BB27D0D0D64F"),
                    WasRead = true,
                    FullName = "John Corso",
                    Email = "mc5262@nyu.edy",
                    Message = "This is a test message."
                }
            );
            Console.ReadLine();
        }
    }
}
