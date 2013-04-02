using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Reddit.Things.API;
using Reddit.Things.API.Enums;
using Reddit.Things;

namespace Reddit.Tests.Integration
{
    [TestFixture]
    class LinkTests
    {
        private Link _link;

        public LinkTests ()
        {
            _link = Constants.GetLink();
        }

        [Test]
        public void GetCommentsTest ()
        {
            var actual = _link.GetComments();

            Assert.That(actual.Count > 0);
        }

        [Test]
        public void CommentTest ()
        {
            var actual = _link.Comment("testing from Reddit.Tests.Integration.LinkTests");

            Assert.That(actual.Kind, Is.EqualTo(Kind.Comment));
        }

        [Test]
        public void EditContentTest ()
        {
            string newContent = "updating content from LinkTests at " + DateTime.Now.ToString();
            
            _link.EditContent(newContent);

            Assert.That(_link.SelfContent, Is.EqualTo(newContent));
        }

        [Test]
        public void GetAuthorTest ()
        {
            var actual = _link.Author;

            Assert.That(actual.Kind, Is.EqualTo(Kind.Account));
            Assert.That(actual.ID, Is.EqualTo(Constants.UserID));
            Assert.That(actual.Name, Is.EqualTo(Constants.UserName));   
        }

        [Test]
        public void GetSubredditTest ()
        {
            var actual = _link.Subreddit;

            Assert.That(actual.Kind, Is.EqualTo(Kind.Subreddit));
            Assert.That(actual.ID, Is.EqualTo(Constants.SubredditID));
            Assert.That(actual.Links.Exists(x => x.ID == Constants.LinkID));
        }
    }
}
