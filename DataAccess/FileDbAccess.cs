//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Runtime.Serialization.Json;
//using System.Text;
//using Newtonsoft.Json;
//using Models;

//namespace DataAccess
//{
//    public class FileDbAccess
//    {
//        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);
//        private const string contactFile = "contacts.json";

//        // Read contacts file and return as List<PersonModel>
//        public static List<PersonModel> GetContacts(string fileName = contactFile)
//        {
//            try
//            {
//                string contactsSerialized = ReadContactListFromFile(fileName);
//                var temp = JSONToPersonModelList(contactsSerialized);
//                List<PersonModel> p = new List<PersonModel>();
//                if (temp != null)
//                    p = new List<PersonModel>(JSONToPersonModelList(contactsSerialized));
//                return p;
//            }
//            catch (Exception e)
//            {
//                logger.Info($"ContactDataIO.GetContacts:{e.Message}");
//                return null;
//            }
//        }

//        // Write contacts to file
//        public static bool WriteContacts(List<PersonModel> p, string fileName = contactFile)
//        {
//            try
//            {
//                string contactsSerialized = PersonModelListToJSON(p);
//                if (contactsSerialized == null)
//                    throw new Exception($"ContactDataIO.cs\nWriteContactsToFile (line 39).\nPersonModelListToJSON(line 53) returned null.");
//                return WriteContactListToFile(contactsSerialized, fileName);
//            }
//            catch (Exception e)
//            {
//                logger.Info(e.Message);
//                return false;
//            }
//        }

//        // JSON Serializer
//        public static string PersonModelListToJSON(List<PersonModel> contacts)
//        {
//            string json = "";
//            try
//            {
//                json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
//            }
//            catch (Exception e)
//            {
//                logger.Info(e.Message);
//            }
//            return json;
//        }

//        // JSON Deserializer
//        public static List<PersonModel> JSONToPersonModelList(string json)
//        {
//            List<PersonModel> contacts = new List<PersonModel>();
//            try
//            {
//                contacts = JsonConvert.DeserializeObject<List<PersonModel>>(json);
//            }
//            catch (Exception e)
//            {
//                logger.Info(e.Message);
//            }
//            return contacts;
//        }

//        // File Write
//        public static bool WriteContactListToFile(string contactsSerialized, string fileName = contactFile)
//        {
//            try
//            {
//                // Overwrite file with updated contact list
//                using (StreamWriter file = new StreamWriter(@fileName))
//                {
//                    file.Write(contactsSerialized);
//                }
//                return true;
//            }
//            catch (Exception e)
//            {
//                logger.Info(e.Message);
//                return false;
//            }
//        }

//        // File Read
//        public static string ReadContactListFromFile(string fileName = contactFile)
//        {
//            try
//            {
//                // Get serialized contact list from file
//                string contactsSerialized = File.ReadAllText(@fileName);
//                return contactsSerialized;
//            }
//            catch (Exception e)
//            {
//                logger.Info(e.Message);
//                return null;
//            }
//        }
//    }
//}
