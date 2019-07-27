using System;
using System.Runtime.Serialization;

namespace PointOfSale.Exceptions
{
    [Serializable]
    public class ProductPriceNotFoundException : Exception
    {
        private string _defaultMessage;

        public ProductPriceNotFoundException(string message, string productCode) : this(message)
        {
            ProductCode = productCode;
        }

        public ProductPriceNotFoundException(string message) : base(message) { }

        public ProductPriceNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        protected ProductPriceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public string ProductCode { get; }

        public override string Message
        {
            get
            {
                return base.Message ?? GetDefaultMessage();
            }
        }

        private string GetDefaultMessage()
        {
            if (_defaultMessage != null)
                return _defaultMessage;
            
            _defaultMessage = "Cannot find price for product";
            if (ProductCode != null)
                _defaultMessage += $" :{ProductCode}";

            return _defaultMessage;
        }
    }
}
