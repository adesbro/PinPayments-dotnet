using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class Card
    {
        [DataMember(Name = "scheme")]
        public string Scheme { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "number")]
        public string Number { get; set; }

        [DataMember(Name = "expiry_month")]
        public int ExpiryMonth { get; set; }

        [DataMember(Name = "expiry_year")]
        public int ExpiryYear { get; set; }

        [DataMember(Name = "cvc")]
        public string Cvc { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "address_line1")]
        public string AddressLine1 { get; set; }

        [DataMember(Name = "address_line2")]
        public string AddressLine2 { get; set; }

        [DataMember(Name = "address_city")]
        public string AddressCity { get; set; }

        [DataMember(Name = "address_postcode")]
        public string AddressPostcode { get; set; }

        [DataMember(Name = "address_state")]
        public string AddressState { get; set; }

        [DataMember(Name = "address_country")]
        public string AddressCountry { get; set; }
    }
}