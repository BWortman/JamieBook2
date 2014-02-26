// IntExtensionsTests.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi2Book.Common.Extensions;

namespace WebApi2Book.Common.Tests.Extensions
{
    [TestClass]
    public class IntExtensionsTests
    {
        [TestMethod]
        public void GetBoundedValue_no_change_to_int_value()
        {
            const int val = 44;
            const int minVal = 1;
            const int maxVal = 4000;

            var boundedVal = val.GetBoundedValue(minVal, maxVal);

            Assert.AreEqual(val, boundedVal);
        }

        [TestMethod]
        public void GetBoundedValue_caps_int_value()
        {
            const int val = 44;
            const int minVal = 1;
            const int maxVal = 40;

            var boundedVal = val.GetBoundedValue(minVal, maxVal);

            Assert.AreEqual(maxVal, boundedVal);
        }

        [TestMethod]
        public void GetBoundedValue_floors_int_value()
        {
            const int val = -44;
            const int minVal = 1;
            const int maxVal = 40;

            var boundedVal = val.GetBoundedValue(minVal, maxVal);

            Assert.AreEqual(minVal, boundedVal);
        }

        [TestMethod]
        public void GetBoundedValue_defaults_nullableInt_value()
        {
            int? val = null;
            const int minVal = 1;
            const int maxVal = 40;
            const int defaultVal = 33;

            var boundedVal = val.GetBoundedValue(defaultVal, minVal, maxVal);

            Assert.AreEqual(defaultVal, boundedVal);
        }

        [TestMethod]
        public void GetBoundedValue_floor_only_defaults_nullableInt_value()
        {
            int? val = null;
            const int minVal = 1;
            const int defaultVal = 33;

            var boundedVal = val.GetBoundedValue(defaultVal, minVal);

            Assert.AreEqual(defaultVal, boundedVal);
        }

        [TestMethod]
        public void GetBoundedValue_floor_only_floors_nullableInt_value()
        {
            int? val = -22;
            const int minVal = 1;
            const int defaultVal = 33;

            var boundedVal = val.GetBoundedValue(defaultVal, minVal);

            Assert.AreEqual(minVal, boundedVal);
        }

        [TestMethod]
        public void GetBoundedValue_floor_only_no_change_to_nullableInt_value()
        {
            int? val = 16;
            const int minVal = 1;
            const int defaultVal = 33;

            var boundedVal = val.GetBoundedValue(defaultVal, minVal);

            Assert.AreEqual(val, boundedVal);
        }
    }
}