using System;
using System.Runtime.Serialization;

namespace PinPayments.Model
{
    public enum SortField
    {
        None,
        CreatedAt,
        Description,
        Amount
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }

    [DataContract]
    public class ChargeSearch
    {
        [DataMember(Name = "query")]
        public string Query { get; set; }

        [DataMember(Name = "start_date")]
        public DateTime? StartDate { get; set; }

        [DataMember(Name = "end_date")]
        public DateTime? EndDate { get; set; }

        [IgnoreDataMember]
        public SortField SortField { get; set; }

        [DataMember(Name = "sort")]
        public string Sort
        {
            get
            {
                switch (SortField)
                {
                    case SortField.Amount:
                        return "amount";

                    case SortField.CreatedAt:
                        return "created_at";

                    case SortField.Description:
                        return "description";

                    default:
                        return null;
                }
            }
        }

        [IgnoreDataMember]
        public SortDirection SortDirection { get; set; }

        [DataMember(Name = "direction")]
        public string Direction
        {
            get { return (SortDirection == SortDirection.Ascending) ? "1" : "-1"; }
        }
    }
}