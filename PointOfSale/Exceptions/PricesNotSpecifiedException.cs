using System;
using System.Runtime.Serialization;

namespace PointOfSale.Exceptions
{
    [Serializable]
    public class PricesNotSpecifiedException : Exception
    {
        public PricesNotSpecifiedException()
        {
        }

        public PricesNotSpecifiedException(string message) : base(message)
        {
        }

        public PricesNotSpecifiedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PricesNotSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}