using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class RefundResponse
    {
        [DataMember(Name = "response")]
        public Refund Refund { get; set; }
    }
}