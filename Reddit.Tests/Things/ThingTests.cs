using NUnit.Framework;
using Reddit.Things;

namespace Reddit.Tests.Things
{
    [TestFixture]
    public class ThingTests
    {
        [Test]
        public void GetTest ()
        {
            var expectedThing = new Thing("abc123", Kind.Link);
            string inputString = "t3_abc123";

            var thing = Thing.Get(inputString);

            Assert.That(thing.Kind, Is.EqualTo(expectedThing.Kind));
            Assert.That(thing.ID, Is.EqualTo(expectedThing.ID));
        }

        [Test]
        public void CorrectToString ()
        {
            string expectedString = "t1_a1d3f2";
            var thing = new Thing("a1d3f2", Kind.Comment);

            string output = thing.ToString();

            Assert.That(output, Is.EqualTo(expectedString));
        }
    }
}