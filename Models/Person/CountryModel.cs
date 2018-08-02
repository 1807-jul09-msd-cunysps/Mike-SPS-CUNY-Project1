using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft;

namespace Models.Person
{
    [DataContract]
    public class CountryModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string PhoneCode { get; set; }
        [DataMember]
        public string ISO2 { get; set; }
        [DataMember]
        public string ISO3 { get; set; }
    
        public string Print()
        {
            return $"{Name}-{PhoneCode}-{ISO2}-{ISO3}";
        }
    }
    
}
