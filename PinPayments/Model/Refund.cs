using System;
using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class Refund
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "success")]
        public string Success { get; set; }

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "charge")]
        public string ChargeToken { get; set; }

        [DataMember(Name = "created_at")]
        private string FormattedCreatedAt { get; set; }

        [IgnoreDataMember]
        public DateTime CreatedAt
        {
            get { return DateTime.Parse(FormattedCreatedAt); }
        }

        [DataMember(Name = "error_message")]
        public string ErrorMessage { get; set; }

        [DataMember(Name = "status_message")]
        public string StatusMessage { get; set; }
    }
}