using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class CustomerUpdate
    {
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "card_token")]
        public string CardToken { get; set; }

        [DataMember(Name = "card")]
        public Card Card { get; set; }
    }
}