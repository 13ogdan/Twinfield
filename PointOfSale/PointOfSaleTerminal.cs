using PointOfSale.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace PointOfSale
{
    public class PointOfSaleTerminal
    {
        private IReadOnlyDictionary<string, Price> _pricePerProduct;
        private readonly Dictionary<string, uint> _scannedProducts = new Dictionary<string, uint>();

        public void SetPricing(IReadOnlyDictionary<string, Price> prices)
        {
            _pricePerProduct = prices.ToImmutableDictionary();
        }

        public void Scan(string productCode)
        {
            VerifyPrices();

            if (!_pricePerProduct.ContainsKey(productCode))
                throw new ProductPriceNotFoundException(null, productCode);

            if (!_scannedProducts.ContainsKey(productCode))
                _scannedProducts.Add(productCode, 1);
            else
                _scannedProducts[productCode]++;
        }

        public double CalculateTotal()
        {
            VerifyPrices();

            double totalPrice = 0;
            foreach (var productState in _scannedProducts)
            {
                totalPrice += GetTotalProductPrice(productState.Key, productState.Value);
            }

            return totalPrice;
        }

        /// <summary>
        /// Invalidates all scanned products. Can be used for re-scanning or when products were successfuly sold.
        /// </summary>
        public void InvalidateScannedProducts()
        {
            _scannedProducts.Clear();
        }

        /// <summary>
        /// Verifies that prices is initialized. Otherwise throws exception.
        /// </summary>
        /// <exception cref="PricesNotSpecifiedException">Throws exeption when prices are not set.</exception>
        private void VerifyPrices()
        {
            if (_pricePerProduct == null)
                throw new PricesNotSpecifiedException($"Prices is not specified. Use {nameof(SetPricing)} method firstly.");
        }

        private double GetTotalProductPrice(string productCode, uint volume)
        {
            var price = _pricePerProduct[productCode];
            
            if (price is VolumePrice volumePrice && volume >= volumePrice.Volume)
            {
                uint packCount = volume / volumePrice.Volume;
                uint restCount = volume - packCount * volumePrice.Volume;
                return (packCount * volumePrice.PackPrice) + restCount * volumePrice.PriceForUnit;
            }
            else
            {
                return volume * price.PriceForUnit;
            }
        }
    }
}
