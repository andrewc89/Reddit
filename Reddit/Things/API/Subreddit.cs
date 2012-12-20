
namespace Reddit.Things.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Subreddit : Thing
    {
        #region Constructor

        public Subreddit ()
        {
            Links = new List<Link>();
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public string ModHash { get; set; }
        public List<Link> Links { get; set; }
        public Thing Before { get; set; }
        public Thing After { get; set; }

        #endregion

        #region Submit

        public Thing PostSelf (string Title, string ContentMarkdown)
        {
            string PostData = new StringBuilder()
                .Append("title=").Append(Title)
                .Append("&text=").Append(ContentMarkdown)
                .Append("&sr=").Append(Name)
                .Append("&kind=").Append("self")
                .ToString();
            return Post(PostData);
        }

        public Thing PostLink (string Title, string Url)
        {
            string PostData = new StringBuilder()
                .Append("title=").Append(Title)
                .Append("&url=").Append(Url)
                .Append("&sr=").Append(Name)
                .Append("&kind=").Append("link")
                .ToString();
            return Post(PostData);
        }

        private Thing Post (string PostData)
        {
            string Response = Connection.Post("/api/submit", PostData);
            string Link = SimpleJSON.JSONDecoder.Decode(Response)["json"]["data"]["name"].StringValue;
            return Thing.Get(Link);
        }

        #endregion

        #region Listings

        public List<Link> Hot (int Limit = 50)
        {
            return Links.GetRange(0, Limit);
        }

        public List<Link> New (Enums.Sort New, int Limit = 50)
        {
            string Args = "limit=" + Limit + "&sort=" + New.Arg;
            return Sorted("new", Args);
        }

        public List<Link> Top (Enums.From from, int Limit = 50)
        {
            string Args = "limit=" + Limit + "&t=" + from.Arg;
            return Sorted("top", Args);
        }

        public List<Link> Controversial (Enums.From from, int Limit = 50)
        {
            string Args = "limit=" + Limit + "&t=" + from.Arg;
            return Sorted("controversial", Args);
        }

        #endregion        

        #region Private Functions

        private List<Link> Sorted (string Sort, string Args)
        {
            string Response = Connection.Get("/r/" + Name + "/" + Sort + "/.json", Args);
            var Links = new List<Link>();
            foreach (var Link in SimpleJSON.JSONDecoder.Decode(Response)["data"]["children"].ArrayValue)
            {
                Links.Add(API.Link.Create(Link["data"]));
            }
            return Links;
        }

        #endregion

        #region Factory

        public static Subreddit Create (string Name, SimpleJSON.JObject Json)
        {
            var Temp = new Subreddit();
            Temp.Name = Name;

            var Children = Json["children"];            
            Temp.Kind = Kind.Subreddit;
            if (Children.ArrayValue.Count > 0)
            {
                Temp.ID = Children.ArrayValue[0]["data"]["subreddit_id"].StringValue.Split('_')[1];
            }

            Temp.ModHash = Json["modhash"].StringValue;
            Temp.Before = Thing.Get(Json["before"].StringValue);
            Temp.After = Thing.Get(Json["after"].StringValue);

            foreach (var Link in Children.ArrayValue)
            {
                Temp.Links.Add(API.Link.Create(Link["data"]));
            }

            return Temp;
        }

        #endregion
    }
}