using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class ChargeResponse
    {
        [DataMember(Name = "response")]
        public ChargeResult Charge { get; set; }
    }
}