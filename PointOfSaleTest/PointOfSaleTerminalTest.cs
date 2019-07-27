using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointOfSale;
using PointOfSale.Exceptions;
using System.Collections.Generic;

namespace PointOfSaleTest
{
    [TestClass]
    public class PointOfSaleTerminalTest
    {
        public readonly PointOfSaleTerminal _terminal = new PointOfSaleTerminal();

        [TestMethod]
        public void Should_ThrowException_When_PricesNotDefined()
        {
            //Arrange
            string productCode = "NotDefinedProduct";

            //Act-Assert
            var ex = Assert.ThrowsException<PricesNotSpecifiedException>(() => _terminal.Scan(productCode));
        }

        [TestMethod]
        public void Should_ThrowException_When_ProductPricesNotSpecified()
        {
            //Arrange
            string productCode = "NotDefinedProduct";
            _terminal.SetPricing(new Dictionary<string, Price>() { { "Product", new Price(1) } });

            //Act-Assert
            var ex = Assert.ThrowsException<ProductPriceNotFoundException>(() => _terminal.Scan(productCode));

            //Assert
            Assert.AreEqual(productCode, ex.ProductCode);
        }

        [DataTestMethod]
        [DataRow("ABCDABA", 13.25)]
        [DataRow("CCCCCCC", 6)]
        [DataRow("ABCD", 7.25)]
        [DataRow("DCAB", 7.25)]
        [DataRow("AAA", 3)]
        [DataRow("AAAA", 4.25)]
        [DataRow("ABABABA", 17)]
        public void Should_CalculateTotal(string products, double expectedTotal)
        {
            //Arrange
            var prices = new Dictionary<string, Price>();
            prices.Add("A", new VolumePrice(1.25, 3, 3));
            prices.Add("B", new Price(4.25));
            prices.Add("C", new VolumePrice(1, 6, 5));
            prices.Add("D", new Price(0.75));
            _terminal.SetPricing(prices);

            //Act
            foreach (var productCode in products)
            {
                _terminal.Scan(productCode.ToString());
            }
            var actualTotal = _terminal.CalculateTotal();

            //Assert
            Assert.AreEqual(expectedTotal, actualTotal);
        }

        [TestMethod]
        public void Should_GiveSameTotal_When_CalculatesTwoTimesInRow()
        {
            //Arrange
            const string ProductCode = "Product";
            var prices = new Dictionary<string, Price>();
            prices.Add(ProductCode, new Price(1));
            _terminal.SetPricing(prices);
            _terminal.Scan(ProductCode);
            _terminal.Scan(ProductCode);
            _terminal.Scan(ProductCode);

            //Act
            var price1 = _terminal.CalculateTotal();
            var price2 = _terminal.CalculateTotal();

            //Arrange 
            Assert.AreEqual(price1, price2);
            Assert.AreEqual(3, price1);
        }
    }
}
