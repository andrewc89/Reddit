using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Reddit.Things;
using Reddit.Things.API;
using System.Runtime.Serialization;

namespace Reddit
{
    public class Reddit
    {
        #region Constructor

        public Reddit (string UserAgent) 
        {
            this.Me = new Me();
            this.Connection = new HttpWrapper(this.Me);
            this.Connection.UserAgent = UserAgent;
        }

        #endregion

        #region Properties

        private HttpWrapper Connection { get; set; }       

        private Me Me { get; set; }

        #endregion

        #region Login

        public bool Login (string UserName, string Password)
        {
            var Response = Connection.Post("http://www.reddit.com/api/login", "user=" + UserName + "&passwd=" + Password);
            if (string.IsNullOrEmpty(Response)) return false;
            SimpleJSON.JObject Json = SimpleJSON.JSONDecoder.Decode(Response);
            this.Connection.Cookie = (string)Json["json"]["data"]["cookie"];
            this.Me.ModHash = (string)Json["json"]["data"]["modhash"];  
            GetMe();
            return true;
        }

        public bool LoggedIn ()
        {
            return this.Connection.LoggedIn();
        }

        #endregion        

        #region Get

        public string GetJSON (string Url)
        {
            return this.Connection.Get(Url);
        }

        public string GetThing (Thing Thing, bool Comments = false)
        {
            string Response;
            if (Comments)
            {
                Response = GetJSON("http://www.reddit.com/comments/" + Thing.ToString() + ".json");
            }
            else
            {
                Response = GetJSON("http://www.reddit.com/by_id/" + Thing.ToString() + ".json");
            }
            return Response;
        }

        #endregion

        #region Me

        private Me GetMe ()
        {
            if (!LoggedIn()) throw new NotLoggedInException("You need to be logged in to get your info");
            var Response = GetJSON("http://www.reddit.com/api/me.json");
            this.Me = Me.Create(Response);
            return this.Me;
        }

        #endregion

        #region Subreddit

        public Subreddit GetSubreddit (string SubredditName)
        {
            string Response = GetJSON("http://www.reddit.com/r/" + SubredditName + "/.json");
            return Subreddit.Create(Response);
        }

        #endregion

        #region Comment

        public Comment GetComment (Thing Thing)
        {
            string Response = GetThing(Thing);
            return Comment.Create(Response);
        }

        public Thing PostComment (string id, Kind kind, string CommentMarkdown)
        {
            return this.PostComment(Thing.Get(kind.ToString() + "_" + id.ToString()), CommentMarkdown);          
        }

        public Thing PostComment (Thing thing, string CommentMarkdown)
        {
            if (!LoggedIn()) throw new NotLoggedInException("You need to be logged in to post a comment");
            string PostData = new StringBuilder()
                .Append("thing_id=").Append(thing.ToString())
                .Append("&text=").Append(CommentMarkdown)
                .ToString();
            string Response = this.Connection.Post("http://www.reddit.com/api/comment", PostData);
            return Thing.Get(SimpleJSON.JSONDecoder.Decode(Response)["json"]["data"]["things"].ArrayValue[0]["data"]["id"].StringValue);
        }

        #endregion

        #region Link

        public Link GetLink (Thing Thing)
        {
            string Response = GetJSON("http://www.reddit.com/by_id/" + Thing.ToString() + "/.json");
            return Link.ByID(Response);
        }

        public Link GetLink (string Url)
        {
            string Response = GetJSON(Url + ".json");
            return Link.ByUrl(Response);
        }

        public List<Link> GetUrlSubmissions (string Url)
        {
            string Response = GetJSON("http://www.reddit.com/api/info.json?url=" + Url);
            var Submissions = new List<Link>();
            foreach (var Submission in SimpleJSON.JSONDecoder.Decode(Response)["data"]["children"].ArrayValue)
            {
                Submissions.Add(Link.Create(Submission["data"]));
            }
            return Submissions;
        }

        public Thing PostSelf (string Subreddit, string Title, string ContentMarkdown)
        {
            if (!LoggedIn()) throw new NotLoggedInException("You need to be logged in to post a self post");
            string PostData = new StringBuilder()
                .Append("title=").Append(Title)
                .Append("&text=").Append(ContentMarkdown)
                .Append("&sr=").Append(Subreddit)
                .Append("&kind=").Append("link")
                .ToString();
            var Response = this.Connection.Post("http://www.reddit.com/api/submit", PostData);
            string Link = SimpleJSON.JSONDecoder.Decode(Response)["json"]["data"]["name"].StringValue;
            return Thing.Get(Link);
        }

        public Thing PostLink (string Subreddit, string Title, string Url)
        {
            if (!LoggedIn()) throw new NotLoggedInException("You need to be logged in to post a link");
            string PostData = new StringBuilder()
                .Append("title=").Append(Title)
                .Append("&url=").Append(Url)
                .Append("&sr=").Append(Subreddit)
                .Append("&kind=").Append("link")
                .ToString();
            var Response = this.Connection.Post("http://www.reddit.com/api/submit", PostData);
            string Link = SimpleJSON.JSONDecoder.Decode(Response)["json"]["data"]["name"].StringValue;
            return Thing.Get(Link);
        }

        #endregion

        #region User

        public void GetUser (string UserName)
        {
            var Response = GetJSON("http://www.reddit.com/user/" + UserName + "/about.json");
        }

        #endregion

        #region Messages

        public List<Message> GetInbox ()
        {
            string Response = GetJSON("http://www.reddit.com/message/inbox/.json");
            var Json = SimpleJSON.JSONDecoder.Decode(Response);
            var Messages = new List<Message>();
            foreach (var JMessage in Json["data"]["children"].ArrayValue)
            {
                Messages.Add(Message.Create(JMessage));
            }
            return Messages;
        }

        #endregion
    }

    public class NotLoggedInException : Exception
    {
        public NotLoggedInException ()
        {
        }

        public NotLoggedInException (string Message)
            : base(Message)
        {
        }

        public NotLoggedInException (string Message, Exception InnerException)
            : base(Message, InnerException)
        {
        }

        protected NotLoggedInException (SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
        }
    }
}