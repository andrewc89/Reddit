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

        /// <summary>
        /// new Reddit wrapper
        /// </summary>
        /// <param name="UserAgent">your useragent per https://github.com/reddit/reddit/wiki/API#Rules </param>
        public Reddit (string UserAgent) 
        {
            this.Me = new Me();
            this.Connection = new HttpWrapper(this.Me);
            this.Connection.UserAgent = UserAgent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// connection instance
        /// </summary>
        private HttpWrapper Connection { get; set; }       

        /// <summary>
        /// logged in user
        /// </summary>
        private Me Me { get; set; }

        #endregion

        #region Login

        /// <summary>
        /// log in via API
        /// required to make additional requests
        /// </summary>
        /// <param name="UserName">user's username</param>
        /// <param name="Password">user's password</param>
        /// <returns>logged in successfully?</returns>
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

        /// <summary>
        /// checks connection to see if logged in
        /// </summary>
        /// <returns>logged in to API?</returns>
        public bool LoggedIn ()
        {
            return this.Connection.LoggedIn();
        }

        #endregion                

        #region Get

        /// <summary>
        /// internal function to perform get request for JSON info
        /// </summary>
        /// <param name="Url">url including http:// and .json suffix</param>
        /// <returns>response string</returns>
        private string GetJSON (string Url)
        {
            return this.Connection.Get(Url);
        }

        /// <summary>
        /// internal function to get json info of Thing
        /// </summary>
        /// <param name="Thing">reddit Thing</param>
        /// <param name="Comments">comments ? /comments/{thing}.json : /by_id/{thing}.json</param>
        /// <returns>response string</returns>
        private string GetThing (Thing Thing, bool Comments = false)
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

        /// <summary>
        /// gets logged in user's info
        /// </summary>
        /// <returns>API.Me object</returns>
        private Me GetMe ()
        {
            if (!LoggedIn()) throw new NotLoggedInException("You need to be logged in to get your info");
            var Response = GetJSON("http://www.reddit.com/api/me.json");
            this.Me = Me.Create(Response);
            return this.Me;
        }

        #endregion
        
        #region Subreddit

        /// <summary>
        /// gets a subreddit, including links
        /// </summary>
        /// <param name="SubredditName">subreddit name</param>
        /// <returns>API.Subreddit object</returns>
        public Subreddit GetSubreddit (string SubredditName)
        {
            string Response = GetJSON("http://www.reddit.com/r/" + SubredditName + "/.json");
            return Subreddit.Create(Response);
        }

        #endregion

        #region Comment

        /// <summary>
        /// gets a comment
        /// </summary>
        /// <param name="Thing">reddit Thing</param>
        /// <returns>API.Comment object</returns>
        public Comment GetComment (Thing Thing)
        {
            string Response = GetThing(Thing);
            return Comment.Create(Response);
        }

        /// <summary>
        /// gets a comment by id
        /// </summary>
        /// <param name="id">Thing id</param>
        /// <returns>API.Comment object</returns>
        public Comment GetComment (string id)
        {
            return GetComment(Thing.Get(Kind.Comment.ToString() + "_" + id));
        }

        /// <summary>
        /// post a comment in response to a Thing (Link, Comment, Message)
        /// </summary>
        /// <param name="id">Thing id</param>
        /// <param name="kind">Thing kind</param>
        /// <param name="CommentMarkdown">comment text in markdown format</param>
        /// <returns>comment as a Thing</returns>
        public Thing PostComment (string id, Kind kind, string CommentMarkdown)
        {
            return this.PostComment(Thing.Get(kind.ToString() + "_" + id.ToString()), CommentMarkdown);          
        }

        /// <summary>
        /// post a comment in response to a Thing (Link, Comment, Message)
        /// </summary>
        /// <param name="thing">Thing to respond to</param>
        /// <param name="CommentMarkdown">comment text in markdown format</param>
        /// <returns>comment as a Thing</returns>
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

        public Thing EditComment (Thing thing, string CommentMarkdown)
        {
            if (!LoggedIn()) throw new NotLoggedInException("You need to be logged in to edit a comment");
            string PostData = new StringBuilder()
                .Append("thing_id=").Append(thing.ToString())
                .Append("&text=").Append(CommentMarkdown)
                .ToString();
            string Response = this.Connection.Post("http://www.reddit.com/api/editusertext", PostData);
            return thing;
        }

        #endregion

        #region Link

        /// <summary>
        /// get submission (link or self post)
        /// </summary>
        /// <param name="Thing">submission as Thing</param>
        /// <returns>API.Link object</returns>
        public Link GetLink (Thing Thing)
        {
            string Response = GetJSON("http://www.reddit.com/by_id/" + Thing.ToString() + "/.json");
            return Link.ByID(Response);
        }

        public Link GetSelf (Thing Thing)
        {
            return GetLink(Thing);
        }

        /// <summary>
        /// get submission (link or self post) by url
        /// </summary>
        /// <param name="Url">url of submission</param>
        /// <returns>API.Link object</returns>
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
                .Append("&kind=").Append("self")
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

        public Thing EditSelf (Thing thing, string CommentMarkdown)
        {
            return EditComment(thing, CommentMarkdown);
        }

        #endregion

        #region User

        public User GetUser (string UserName)
        {
            string Response = GetJSON("http://www.reddit.com/user/" + UserName + "/about.json");
            return User.Create(SimpleJSON.JSONDecoder.Decode(Response)["data"]);
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