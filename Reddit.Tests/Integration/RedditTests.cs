using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Reddit.Things.API;
using Reddit.Things;

namespace Reddit.Tests.Integration
{
    public class RedditTests
    {
        private Reddit _reddit;

        public RedditTests ()
        {
            _reddit = Constants.GetReddit();
        }

        [Fact]
        public void LoginTest ()
        {
            var actual = _reddit.Login(Constants.UserName, Constants.Password);

            Assert.Equal(actual.Name, Constants.UserName);
            Assert.Equal(actual.Kind, Kind.Account);
            Assert.Equal(actual.ID, Constants.UserID);
        }

        [Fact]
        public void rTest ()
        {
            string subredditName = "testjswrapper";

            var actual = _reddit.r(subredditName);

            Assert.Equal(actual.Name, subredditName);
            Assert.True(actual.Links.Count > 0);
        }

        [Fact]
        public void GetUserTest ()
        {
            var actual = _reddit.GetUser(Constants.UserName);

            Assert.Equal(actual.Name, Constants.UserName);
            Assert.Equal(actual.ID, Constants.UserID);
            Assert.Equal(actual.Kind, Kind.Account);
        }
    }
}