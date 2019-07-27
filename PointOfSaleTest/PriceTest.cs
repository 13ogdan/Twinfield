using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointOfSale;
using PointOfSale.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleTest
{
    [TestClass]
    public class PriceTest
    {
        [DataTestMethod]
        [DataRow(0d)]
        [DataRow(-1d)]
        [DataRow(double.NegativeInfinity)]
        public void Should_ThrowException_When_PriceIsNegative(double invalidPrice)
        {
            //Act/Assert
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(()=> new Price(invalidPrice));

            //Assert
            Assert.AreEqual(invalidPrice, ex.ActualValue);
        }
    }

    [TestClass]
    public class VolumePriceTest
    {
        [DataTestMethod]
        [DataRow(0d)]
        [DataRow(-1d)]
        [DataRow(double.NegativeInfinity)]
        public void Should_ThrowException_When_PriceIsNegative(double invalidPrice)
        {
            //Act/Assert
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new VolumePrice(1, 2, invalidPrice));

            //Assert
            Assert.AreEqual(invalidPrice, ex.ActualValue);
        }

        [DataTestMethod]
        [DataRow((uint)0)]
        [DataRow((uint)1)]
        public void Should_ThrowException_When_VolumeIsNegative(uint invalidVolume)
        {
            //Act/Assert
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new VolumePrice(1.25, invalidVolume, 3));

            //Assert
            Assert.AreEqual(invalidVolume, ex.ActualValue);
        }

        [TestMethod]
        public void Should_ThrowException_When_PriceForPackUseless()
        {
            //Act/Assert
            var ex = Assert.ThrowsException<SenselessVolumePriceException>(() => new VolumePrice(1, 3, 3));

            //Assert
            Assert.AreEqual(3, ex.VolumePrice);
            Assert.AreEqual(1, ex.PriceForUnit);
            Assert.AreEqual((uint)3, ex.Volume);
        }
    }
}
