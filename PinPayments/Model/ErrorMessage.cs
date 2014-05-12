using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class ErrorMessage
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "param")]
        public string Param { get; set; }
    }
}