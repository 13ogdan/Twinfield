using System;
using System.Runtime.Serialization;

namespace PointOfSale.Exceptions
{
    [Serializable]
    public class SenselessVolumePriceException : Exception
    {
        public SenselessVolumePriceException(double volumePrice, uint volume, double priceForUnit):this()
        {
            VolumePrice = volumePrice;
            Volume = volume;
            PriceForUnit = priceForUnit;
        }

        public SenselessVolumePriceException() : this("Volume price must be profitable comparing to price for unit.")
        {
        }

        public SenselessVolumePriceException(string message) : base(message)
        {
        }

        public SenselessVolumePriceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SenselessVolumePriceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public double VolumePrice { get; }
        public uint Volume { get; }
        public double PriceForUnit { get; }
    }
}