
namespace Reddit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net;
    using System.IO;
    using Things.API;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class HttpWrapper
    {
        #region Costructor

        public HttpWrapper (Me me)
        {
            this.Me = me;
        }

        #endregion

        #region Properties

        public string UserAgent { get; set; }

        public string Cookie { get; set; }

        public Me Me { get; set; }

        #endregion

        #region Logged in?

        public bool LoggedIn ()
        {
            return !string.IsNullOrEmpty(this.Cookie) && !string.IsNullOrEmpty(this.Me.ModHash);
        }

        #endregion

        #region Get

        public string Get (string Uri)
        {
            if (!Uri.StartsWith("http://"))
            {
                Uri = "http://" + Uri;
            }
            var Request = (HttpWebRequest)WebRequest.Create(Uri);
            Request.UserAgent = this.UserAgent;
            Request.CookieContainer = new CookieContainer();
            Request.CookieContainer.Add(Request.RequestUri, new CookieCollection() { new Cookie("reddit_session", this.Cookie.Replace(",", "%2c")), new Cookie("uh", this.Me.ModHash) });
            Request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            var Response = Request.GetResponse();
            using (var Reader = new StreamReader(Response.GetResponseStream()))
            {
                return Reader.ReadToEnd().Trim();
            }
        }

        #endregion

        #region Post

        public string Post (string Url, string Post)
        {
            var Request = (HttpWebRequest)WebRequest.Create(Url);
            if (!string.IsNullOrEmpty(this.Cookie))
            {
                Request.CookieContainer = new CookieContainer();
                Request.CookieContainer.Add(Request.RequestUri, new CookieCollection() { new Cookie("reddit_session", this.Cookie.Replace(",", "%2c")) });
            }
            var Encoding = new ASCIIEncoding();
            string ModHash = "";
            if (!string.IsNullOrEmpty(this.Me.ModHash))
            {
                ModHash = "&uh=" + this.Me.ModHash;
            }
            var PostData = Encoding.GetBytes(Post + ModHash + "&api_type=json");
            Request.UserAgent = this.UserAgent;
            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.ContentLength = PostData.Length;
            using (var Stream = Request.GetRequestStream())
            {
                Stream.Write(PostData, 0, PostData.Length);
            }

            var Response = (HttpWebResponse)Request.GetResponse();

            if (Response == null)
            {
                return null;
            }
            using (var Reader = new StreamReader(Response.GetResponseStream()))
            {
                return Reader.ReadToEnd().Trim();
            }
        }

        #endregion
    }
}
