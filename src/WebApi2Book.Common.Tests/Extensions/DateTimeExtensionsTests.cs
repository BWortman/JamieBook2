// DateTimeExtensionsTests.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi2Book.Common.Extensions;

namespace WebApi2Book.Common.Tests.Extensions
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void ToUrlFriendlyDate_converts_date()
        {
            const string expected = "2013-02-13";
            var actual = DateTime.Parse(expected).ToUrlFriendlyDate();
            Assert.AreEqual(expected, actual);
        }
    }
}