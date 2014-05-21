using System;
using System.Runtime.Serialization;

namespace PinPayments.Model
{
    public class Transfer
    {
        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "paid_at")]
        public string FormattedPaidAt { get; set; }

        [IgnoreDataMember]
        public DateTime PaidAt
        {
            get { return DateTime.Parse(FormattedPaidAt); }
        }
        
        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}