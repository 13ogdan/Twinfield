using PointOfSale.Exceptions;
using System;


namespace PointOfSale
{
    //TODO decide: what is better
    // 1) Inheritance
    // 2) Factory + interfaces
    // 3) Class + fields (ProductPrice with two constructors)
    // Consider: volume can be changed from int to double, new volume price can be added later, prices must be immutable or clonable
    // The best approach when we can change the implemetation easily 
    public class Price
    {
        public Price(double priceForUnit)
        {
            if (priceForUnit <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(priceForUnit), priceForUnit, "Price must be more than 0");

            PriceForUnit = priceForUnit;
        }

        public double PriceForUnit { get; }
    }


    public class VolumePrice : Price
    {
        public VolumePrice(double priceForUnit, uint volume, double volumePrice)
            : base(priceForUnit)
        {
            if (volume <= 1)
                throw new ArgumentOutOfRangeException(nameof(volume), volume, "Volume must be 2 or more");
            if (volumePrice <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(volumePrice), volumePrice, "Price must be more than 0");
            if (volumePrice / volume >= priceForUnit)
                throw new SenselessVolumePriceException(volumePrice, volume, priceForUnit);

            Volume = volume;
            PackPrice = volumePrice;
        }

        public uint Volume { get; }
        public double PackPrice { get; }
    }
}
