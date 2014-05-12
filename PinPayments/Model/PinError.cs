using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PinPayments.Model
{
    [DataContract]
    public class PinError
    {
        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "error_description")]
        public string ErrorDescription { get; set; }

        [DataMember(Name = "messages")]
        public List<ErrorMessage> Messages { get; set; }

        [IgnoreDataMember]
        public ErrorType ErrorType
        {
            get { return ToErrorType(Error); }
        }

        private static ErrorType ToErrorType(string error)
        {
            ErrorType result;

            switch (error)
            {
                case "card_declined":
                    result = ErrorType.CardDeclined;
                    break;

                case "insufficient_funds":
                    result = ErrorType.InsufficientFunds;
                    break;

                case "invalid_cvv":
                    result = ErrorType.InvalidCvv;
                    break;

                case "invalid_card":
                    result = ErrorType.InvalidCard;
                    break;

                case "processing_error":
                    result = ErrorType.ProcessingError;
                    break;

                case "suspected_fraud":
                    result = ErrorType.SuspectFraud;
                    break;

                default:
                    result = ErrorType.Unknown;
                    break;
            }

            return result;
        }
    }
}