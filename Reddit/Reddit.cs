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

        public Reddit () 
        {
            this.Me = new Me();
            this.Connection = new HttpWrapper(this.Me);
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

        #region GetJSON

        public string GetJSON (string Url)
        {
            return this.Connection.Get(Url);
        }

        public void GetJSON (Thing Thing, bool Comments = false)
        {            
            string Url;
            if (Comments)
            {
                Url = "http://www.reddit.com/comments/" + Thing.ToString() + ".json";
            }
            else
            {
                Url = "http://www.reddit.com/by_id/" + Thing.ToString() + ".json";
            }
            GetJSON(Url);
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

        public void GetSubreddit (string Subreddit)
        {
            var Response = GetJSON("http://www.reddit.com/r/" + Subreddit + "/.json");            
        }

        #endregion

        #region Comment

        public void PostComment (string id, Kind kind, string CommentMarkdown)
        {
            if (!LoggedIn()) throw new NotLoggedInException("You need to be logged in to post a comment");
            string PostData = new StringBuilder()
                .Append("thing_id=").Append(id)
                .Append("&text=").Append(CommentMarkdown)
                //.Append("&uh=").Append(this.Me.ModHash)
                .ToString();
            var Response = this.Connection.Post("http://www.reddit.com/api/comment", PostData);
        }

        public void PostComment (Thing thing, string Comment)
        {
            this.PostComment(thing.ID, thing.Kind, Comment);
        }        

        #endregion

        #region Link

        public Thing PostLink (string Title, string Url, string Subreddit)
        {
            if (!LoggedIn()) throw new NotLoggedInException("You need to be logged in to post a link");
            string PostData = new StringBuilder()
                .Append("title=").Append(Title)
                .Append("&url=").Append(Url)
                .Append("&sr=").Append(Subreddit)
                .Append("&kind=").Append("link")
                //.Append("&uh=").Append(this.Me.ModHash)
                .ToString();
            var Response = this.Connection.Post("http://www.reddit.com/api/submit", PostData);
            string Link = SimpleJSON.JSONDecoder.Decode(Response)["json"]["data"]["name"].StringValue;
            Console.WriteLine(Link);
            return Thing.Get(Link);
        }

        #endregion

        #region User

        public void GetUser (string UserName)
        {
            var Response = GetJSON("http://www.reddit.com/user/" + UserName + "/.json");
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