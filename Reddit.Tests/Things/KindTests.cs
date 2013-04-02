using NUnit.Framework;
using Reddit.Things;

namespace Reddit.Tests.Things
{
    [TestFixture]
    public class KindTests
    {
        [Test]
        public void GetTest ()
        {
            string comment = "t3";
            var expectedKind = Kind.Link;

            var kind = Kind.Get(comment);

            Assert.That(kind, Is.EqualTo(expectedKind));
        }
    }
}