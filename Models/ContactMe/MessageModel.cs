using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Models.ContactMe
{
    [DataContract]
    public class MessageModel
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public bool WasRead { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Message { get; set; }

        public string Print()
        {
            return $"{FullName} - {Email}\n{Message}";
        }
    }
}
