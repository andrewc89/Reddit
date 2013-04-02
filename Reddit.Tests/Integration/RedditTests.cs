using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Reddit.Things.API;
using Reddit.Things;

namespace Reddit.Tests.Integration
{
    [TestFixture]
    class RedditTests
    {
        private Reddit _reddit;

        public RedditTests ()
        {
            _reddit = Constants.GetReddit();
        }

        [Test]
        public void LoginTest ()
        {
            var actual = _reddit.Login(Constants.UserName, Constants.Password);

            Assert.That(actual.Name, Is.EqualTo(Constants.UserName));
            Assert.That(actual.Kind, Is.EqualTo(Kind.Account));
            Assert.That(actual.ID, Is.EqualTo(Constants.UserID));
        }

        [Test]
        public void rTest ()
        {
            string subredditName = "testjswrapper";

            var actual = _reddit.r(subredditName);

            Assert.That(actual.Name, Is.EqualTo(subredditName));
            Assert.That(actual.Links.Count > 0);
        }

        [Test]
        public void GetUserTest ()
        {
            var actual = _reddit.GetUser(Constants.UserName);

            Assert.That(actual.Name, Is.EqualTo(Constants.UserName));
            Assert.That(actual.ID, Is.EqualTo(Constants.UserID));
            Assert.That(actual.Kind, Is.EqualTo(Kind.Account));
        }
    }
}