using Reddit.Things;
using Reddit.Things.API;
using System;
using Xunit;

namespace Reddit.Tests.Integration
{
    public class LinkTests
    {
        private Link _link;

        public LinkTests()
        {
            _link = Constants.GetLink();
        }

        [Fact]
        public void GetCommentsTest()
        {
            var actual = _link.GetComments();

            Assert.True(actual.Count > 0);
        }

        [Fact]
        public void CommentTest()
        {
            var actual = _link.Comment("testing from Reddit.Tests.Integration.LinkTests");

            Assert.Equal(actual.Kind, Kind.Comment);
        }

        [Fact]
        public void EditContentTest()
        {
            string newContent = "updating content from LinkTests at " + DateTime.Now.ToString();

            _link.EditContent(newContent);

            Assert.Equal(_link.SelfContent, newContent);
        }

        [Fact]
        public void GetAuthorTest()
        {
            var actual = _link.Author;

            Assert.Equal(actual.Kind, Kind.Account);
            Assert.Equal(actual.ID, Constants.UserID);
            Assert.Equal(actual.Name, Constants.UserName);
        }

        [Fact]
        public void GetSubredditTest()
        {
            var actual = _link.Subreddit;

            Assert.Equal(actual.Kind, Kind.Subreddit);
            Assert.Equal(actual.ID, Constants.SubredditID);
            Assert.True(actual.Links.Exists(x => x.ID == Constants.LinkID));
        }
    }
}