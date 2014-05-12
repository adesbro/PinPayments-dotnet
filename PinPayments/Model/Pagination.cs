using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class Pagination
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "current")]
        public int Current { get; set; }

        [DataMember(Name = "previous")]
        public int? Previous { get; set; }

        [DataMember(Name = "next")]
        public int? Next { get; set; }

        [DataMember(Name = "pages")]
        public int Pages { get; set; }

        [DataMember(Name = "per_page")]
        public int PerPage { get; set; }
    }
}