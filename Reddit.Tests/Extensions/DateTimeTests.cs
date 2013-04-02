using System;
using NUnit.Framework;
using Reddit.Extensions;

namespace Reddit.Tests.Extensions
{
    [TestFixture]
    class DateTimeTests
    {
        [Test]
        public void ConversionTest ()
        {
            double inputDate = 1364913850;
            var expectedDateTime = new DateTime(2013, 4, 2, 14, 44, 10);

            var actualDateTime = inputDate.ToDateTime().ToUniversalTime();

            Assert.That(actualDateTime, Is.EqualTo(expectedDateTime));
        }
    }
}