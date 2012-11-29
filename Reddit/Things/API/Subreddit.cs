
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
        public Subreddit ()
        {
            Children = new List<Link>();
        }

        public string ModHash { get; set; }
        public List<Link> Children { get; set; }
        public Thing Before { get; set; }
        public Thing After { get; set; }

        public static Subreddit Create (string Input)
        {
            var Temp = new Subreddit();
            var Json = SimpleJSON.JSONDecoder.Decode(Input)["data"];
            var Children = Json["children"];

            Temp.ModHash = Json["modhash"].StringValue;
            Temp.Before = Thing.Get(Json["before"].StringValue);
            Temp.After = Thing.Get(Json["after"].StringValue);
            foreach (var Link in Children.ArrayValue)
            {
                Temp.Children.Add(API.Link.Create(Link["data"]));
            }

            return Temp;
        }
    }
}
