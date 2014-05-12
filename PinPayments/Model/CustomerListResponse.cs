using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class CustomerListResponse
    {
        [DataMember(Name = "response")]
        public List<Customer> Customers { get; set; }

        [DataMember(Name = "pagination")]
        public Pagination Pages { get; set; }
    }
}