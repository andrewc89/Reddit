using Reddit.Things;
using Xunit;

namespace Reddit.Tests.Things
{
    public class KindTests
    {
        [Fact]
        public void GetTest()
        {
            string comment = "t3";
            var expectedKind = Kind.Link;

            var kind = Kind.Get(comment);

            Assert.Equal(kind, expectedKind);
        }
    }
}