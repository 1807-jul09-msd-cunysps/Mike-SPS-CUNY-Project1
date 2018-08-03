using System;
using System.Runtime.Serialization;

namespace Models.Person
{
    [DataContract]
    public class AddressModel
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public Guid FK_Person { get; set; }
        [DataMember]
        public string AddrLine1 { get; set; }
        [DataMember]
        public string AddrLine2 { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public Guid FK_Country { get; set; }
        [DataMember]
        public string CountryISO2 { get; set; }
        [DataMember]
        public string Zipcode { get; set; }

        public string Print()
        {
            return $"{AddrLine1} {AddrLine2}\n{City}, {State}, {Zipcode}";
        }
    }
}
