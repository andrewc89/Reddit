
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

        public string ModHash { get; set; }
        public List<Link> Links { get; set; }
        public Thing Before { get; set; }
        public Thing After { get; set; }

        #endregion

        #region Functions

        public List<Link> Hot (int Limit = 50)
        {
            return Links.GetRange(0, Limit);
        }

        public List<Link> Top (int Limit = 50)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Factory

        public static Subreddit Create (string Input)
        {
            var Temp = new Subreddit();
            var Json = SimpleJSON.JSONDecoder.Decode(Input)["data"];
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