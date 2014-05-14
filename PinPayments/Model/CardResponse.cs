using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class CardResponse
    {
        [DataMember(Name = "response")]
        public Card Card { get; set; }
    }
}