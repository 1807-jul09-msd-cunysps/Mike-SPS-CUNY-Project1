using System.Runtime.Serialization;

namespace Models.Person
{
    [DataContract]
    public class PersonModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public AddressModel Address { get; set; }
        [DataMember]
        public ContactInfoModel ContactInfo { get; set; }

        public PersonModel()
        {
            Address = new AddressModel();
            ContactInfo = new ContactInfoModel();
        }

        public string Print()
        {
            return $"\n{Firstname} {Lastname}\n{Address.Print()}\n{ContactInfo.Print()}";
        }
    }
}
