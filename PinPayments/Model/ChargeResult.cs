using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class ChargeResult : Charge
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "status_message")]
        public string StatusMessage { get; set; }

        [DataMember(Name = "created_at")]
        private string FormattedCreatedAt { get; set; }

        [IgnoreDataMember]
        public DateTime CreatedAt
        {
            get { return DateTime.Parse(FormattedCreatedAt); }
        }

        [DataMember(Name = "error_message")]
        public string ErrorMessage { get; set; }

        [DataMember(Name = "captured")]
        public bool Captured { get; set; }

        [DataMember(Name = "authorisation_expired")]
        public bool AuthorisationExpired { get; set; }

        [DataMember(Name = "transfer")]
        public List<Transfer> Transfers { get; set; }

        [DataMember(Name = "amount_refunded")]
        public int AmountRefunded { get; set; }

        [DataMember(Name = "total_fees")]
        public int? TotalFees { get; set; }

        [DataMember(Name = "merchant_entitlement")]
        public int? MerchantEntitlement { get; set; }

        [DataMember(Name = "refund_pending")]
        public bool RefundPending { get; set; }

        [DataMember(Name = "settlement_currency")]
        public string SettlementCurrency { get; set; }
    }
}