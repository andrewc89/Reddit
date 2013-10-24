using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Reddit.Things.API;
using Reddit.Things;

namespace Reddit.Tests.Integration
{
    public class SubredditTests
    {
        private Subreddit _subreddit;

        public SubredditTests ()
        {
            var reddit = Constants.GetReddit();
            _subreddit = Constants.GetSubreddit();
        }

        [Fact]
        public void PostSelfTest ()
        {
            string title = "testing from Reddit.Tests.Integration.SubredditTests";
            string body = "PostSelfTest";
            
            var actual = _subreddit.PostSelf(title, body);

            Assert.Equal(actual.Kind, Kind.Link);
        }

        [Fact]
        public void PostLinkTest ()
        {
            string title = "Reddit API wrapper in C#";

            var actual = _subreddit.PostLink(title, Constants.RandomUrl);

            Assert.Equal(actual.Kind, Kind.Link);
        }

        [Fact]
        public void SubredditMetaTest ()
        {
            var actual = _subreddit.MetaData;

            Assert.Equal(actual.DisplayName, "testjswrapper");
            Assert.Equal(actual.ID, Constants.SubredditID);            
        }
    }
}