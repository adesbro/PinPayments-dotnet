using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class ChargeListResponse
    {
        [DataMember(Name = "response")]
        public List<ChargeResult> Charges { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "pagination")]
        public Pagination Pages { get; set; }
    }
}