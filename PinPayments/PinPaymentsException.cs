using System;
using PinPayments.Model;

namespace PinPayments
{
    public enum ErrorType
    {
        Unknown,
        CardDeclined,
        InsufficientFunds,
        InvalidCvv,
        InvalidCard,
        ProcessingError,
        SuspectFraud
    }

    public class PinPaymentsException : Exception
    {
        public PinError Error { get; private set; }

        public PinPaymentsException(PinError error, string message)
            : base(message)
        {
            Error = error;
        }

        public PinPaymentsException(PinError error, string message, Exception innerException)
            : base(message, innerException)
        {
            Error = error;
        }
    }
}