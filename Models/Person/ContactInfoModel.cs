using System.Runtime.Serialization;

namespace Models.Person
{
    [DataContract]
    public class ContactInfoModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int FK_Person { get; set; }
        [DataMember]
        public int FK_Country { get; set; }
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
