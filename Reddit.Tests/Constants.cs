using Reddit.Things.API;
using System;

namespace Reddit.Tests
{
    public static class Constants
    {
        private static Reddit reddit;
        private static Subreddit subreddit;
        private static Link link;

        public static string UserAgent = "testing https://github.com/theyshookhands/Reddit";
        public static string UserName = "testjswrapper";
        public static string Password = "testjswrapper";
        public static string UserID = "9nfm5";
        public static string SubredditID = "2vo7n";
        public static string LinkID = "1p4z0s";
        public static string RandomUrl = "http://example.com/test?datetime=" + DateTime.Now.ToString("o");

        public static Reddit GetReddit()
        {
            if (reddit == null)
            {
                reddit = new Reddit(UserAgent);
                reddit.Login("testjswrapper", "testjswrapper");
            }
            return reddit;
        }

        public static Subreddit GetSubreddit()
        {
            if (subreddit == null)
            {
                subreddit = GetReddit().r("testjswrapper");
            }
            return subreddit;
        }

        public static Link GetLink()
        {
            if (link == null)
            {
                link = GetSubreddit().Links.Find(x => x.ID == LinkID);
            }
            return link;
        }
    }
}