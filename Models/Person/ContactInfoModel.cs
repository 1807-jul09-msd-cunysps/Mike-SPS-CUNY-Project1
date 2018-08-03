using System;
using System.Runtime.Serialization;

namespace Models.Person
{
    [DataContract]
    public class ContactInfoModel
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public Guid FK_Person { get; set; }
        [DataMember]
        public Guid FK_Country { get; set; }
        [DataMember]
        public string CountryISO2 { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string Ext { get; set; }
        [DataMember]
        public string Email { get; set; }

        public string Print()
        {
            return $"{Number}" + ((Ext != "") ? $": {Ext}" : "") + $" {Email}";
        }
    }
}
