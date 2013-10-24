using System;
using Xunit;
using Reddit.Extensions;

namespace Reddit.Tests.Extensions
{
    public class DateTimeTests
    {
        [Fact]
        public void ConversionTest ()
        {
            double inputDate = 1364913850;
            var expectedDateTime = new DateTime(2013, 4, 2, 14, 44, 10);

            var actualDateTime = inputDate.ToDateTime().ToUniversalTime();

            Assert.Equal(actualDateTime, expectedDateTime);
        }
    }
}