using System;
using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class Customer
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "created_at")]
        private string FormattedCreatedAt { get; set; }

        [IgnoreDataMember]
        public DateTime CreatedAt
        {
            get { return DateTime.Parse(FormattedCreatedAt); }
        }

        [DataMember(Name = "card")]
        public Card Card { get; set; }
    }
}
