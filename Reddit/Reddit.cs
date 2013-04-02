using Reddit.Things.API;
using Reddit.Exceptions;

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
            Connection.UserAgent = UserAgent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// logged in user
        /// </summary>
        public Me Me { get; set; }

        #endregion

        #region Login

        /// <summary>
        /// log in via API
        /// required to make additional requests
        /// </summary>
        /// <param name="UserName">user's username</param>
        /// <param name="Password">user's password</param>
        /// <returns>logged in successfully?</returns>
        public Me Login (string UserName, string Password)
        {
            var Response = Connection.Post("api/login", "user=" + UserName + "&passwd=" + Password);
            SimpleJSON.JObject Json = SimpleJSON.JSONDecoder.Decode(Response);
            Connection.Cookie = (string)Json["json"]["data"]["cookie"];
            Connection.ModHash = (string)Json["json"]["data"]["modhash"];  
            return GetMe();
        }

        /// <summary>
        /// checks connection to see if logged in
        /// </summary>
        /// <returns>logged in to API?</returns>
        public bool LoggedIn ()
        {
            return Connection.LoggedIn();
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
            var Response = Connection.Get("api/me.json");
            this.Me = Me.Create(Response);
            return this.Me;
        }

        #endregion
        
        #region Subreddit

        /// <summary>
        /// gets a subreddit, including links
        /// </summary>
        /// <param name="SubredditName">subreddit name</param>
        /// <returns>Subreddit wrapper object</returns>
        public Subreddit r (string SubredditName)
        {
            string Response = Connection.Get("r/" + SubredditName + ".json");
            return Subreddit.Create(SubredditName, SimpleJSON.JSONDecoder.Decode(Response)["data"]);
        }

        #endregion

        #region User

        /// <summary>
        /// get a user by his/her username
        /// </summary>
        /// <param name="UserName">user's username</param>
        /// <returns>User wrapper object</returns>
        public User GetUser (string UserName)
        {
            string Response = Connection.Get("user/" + UserName + "/about.json");
            return User.Create(SimpleJSON.JSONDecoder.Decode(Response)["data"]);
        }

        #endregion
    }
}