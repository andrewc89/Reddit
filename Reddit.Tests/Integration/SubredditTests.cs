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
    class SubredditTests
    {
        private Subreddit _subreddit;

        public SubredditTests ()
        {
            var reddit = Constants.GetReddit();
            _subreddit = Constants.GetSubreddit();
        }

        [Test]
        public void PostSelfTest ()
        {
            string title = "testing from Reddit.Tests.Integration.SubredditTests";
            string body = "PostSelfTest";
            
            var actual = _subreddit.PostSelf(title, body);

            Assert.That(actual.Kind, Is.EqualTo(Kind.Link));
        }

        [Test]
        public void PostLinkTest ()
        {
            string title = "Reddit API wrapper in C#";

            var actual = _subreddit.PostLink(title, Constants.RandomUrl);

            Assert.That(actual.Kind, Is.EqualTo(Kind.Link));
        }

        [Test]
        public void SubredditMetaTest ()
        {
            var actual = _subreddit.MetaData;

            Assert.That(actual.DisplayName, Is.EqualTo("testjswrapper"));
            Assert.That(actual.ID, Is.EqualTo(Constants.SubredditID));            
        }
    }
}