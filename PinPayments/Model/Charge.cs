using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class Charge
    {
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "ip_address")]
        public string IpAddress { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "capture")]
        public bool Capture { get; set; }

        [DataMember(Name = "card")]
        public Card Card { get; set; }

        [DataMember(Name = "card_token")]
        public string CardToken { get; set; }

        [DataMember(Name = "customer_token")]
        public string CustomerToken { get; set; }
    }
}
