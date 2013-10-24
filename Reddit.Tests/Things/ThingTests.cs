using Reddit.Things;
using Xunit;

namespace Reddit.Tests.Things
{
    public class ThingTests
    {
        [Fact]
        public void GetTest()
        {
            var expectedThing = new Thing("abc123", Kind.Link);
            string inputString = "t3_abc123";

            var thing = Thing.Get(inputString);

            Assert.Equal(thing.Kind, expectedThing.Kind);
            Assert.Equal(thing.ID, expectedThing.ID);
        }

        [Fact]
        public void CorrectToString()
        {
            string expectedString = "t1_a1d3f2";
            var thing = new Thing("a1d3f2", Kind.Comment);

            string output = thing.ToString();

            Assert.Equal(output, expectedString);
        }
    }
}