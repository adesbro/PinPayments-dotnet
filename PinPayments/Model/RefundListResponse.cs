using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class RefundListResponse
    {
        [DataMember(Name = "response")]
        public List<Refund> Refunds { get; set; }

        [DataMember(Name = "pagination")]
        public Pagination Pages { get; set; }
    }
}
