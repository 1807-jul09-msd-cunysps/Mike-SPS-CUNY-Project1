using System;
using System.Collections.Generic;
using Models;
using Newtonsoft.Json;
using NLog;

namespace BusinessLogic
{
    public class Serializer
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);

        // JSON Serializer
        public static string PersonModelListToJson(List<PersonModel> contacts, bool formatted)
        {
            string json = "";
            try
            {
                if (formatted)
                    json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
                else
                    json = JsonConvert.SerializeObject(contacts);
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
            }
            return json;
        }

        // JSON Deserializer
        public static List<PersonModel> JsonToPersonModelList(string json)
        {
            List<PersonModel> contacts = new List<PersonModel>();
            try
            {
                contacts = JsonConvert.DeserializeObject<List<PersonModel>>(json);

            }
            catch (Exception e)
            {
                logger.Info(e.Message);
            }
            return contacts;
        }
    }
}
