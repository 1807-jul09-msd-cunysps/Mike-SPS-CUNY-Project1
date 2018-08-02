using System.Runtime.Serialization;

namespace Models.Person
{
    [DataContract]
    public class AddressModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int FK_Person { get; set; }
        [DataMember]
        public string AddrLine1 { get; set; }
        [DataMember]
        public string AddrLine2 { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public int FK_Country { get; set; }
        [DataMember]
        public string Zipcode { get; set; }

        public string Print()
        {
            return $"{AddrLine1} {AddrLine2}\n{City}, {State}, {Zipcode}";
        }
    }
}
