using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class CustomerResponse
    {
        [DataMember(Name = "response")]
        public Customer Customer { get; set; }
    }
}