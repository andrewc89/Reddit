
namespace Reddit.Things.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Extensions;
    using SimpleJSON;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Link : Thing
    {        
        public string Domain { get; set; }
        public Thing BannedBy { get; set; }
        public string MediaEmbed { get; set; }
        public string SubRedditName { get; set; }
        public string SelfTextHtml { get; set; }
        public string SelfText { get; set; }
        public int Likes { get; set; }
        public string LinkFlairText { get; set; }
        public bool Clicked { get; set; }
        public string Title { get; set; }
        public int NumComments { get; set; }
        public int Score { get; set; }
        public Thing ApprovedBy { get; set; }
        public bool Over18 { get; set; }
        public bool Hidden { get; set; }
        public string Thumbnail { get; set; }
        public Thing Subreddit { get; set; }
        public bool Edited { get; set; }
        public string LinkFlairCSSClass { get; set; }
        public string AuthorFlairCSSClass { get; set; }
        public int Downvotes { get; set; }
        public bool Saved { get; set; }
        public bool IsSelf { get; set; }
        public string Permalink { get; set; }
        public DateTime Created { get; set; }
        public string Url { get; set; }
        public string AuthorFlairText { get; set; }
        public string Author { get; set; }
        public DateTime CreatedUTC { get; set; }
        public string Media { get; set; }
        public string NumReports { get; set; }
        public int Upvotes { get; set; }
        public List<Comment> Comments { get; set; }

        public static Link Create (JObject Json)
        {
            var Temp = new Link();            

            Temp.ID = Json["id"].StringValue;
            Temp.Kind = Kind.Link;
            Temp.Domain = Json["domain"].StringValue;
            //Temp.BannedBy = null;
            //Temp.MediaEmbed = null;
            Temp.SubRedditName = Json["subreddit"].StringValue;
            Temp.SelfTextHtml = Json["selftext_html"].StringValue;
            Temp.SelfText = Json["selftext"].StringValue;
            //Temp.Likes = Json["likes"].IntValue;
            Temp.LinkFlairText = Json["link_flair_text"].StringValue;            
            Temp.Clicked = Json["clicked"].BooleanValue;
            Temp.Title = Json["title"].StringValue;
            Temp.NumComments = Json["num_comments"].IntValue;
            Temp.Score = Json["score"].IntValue;
            //Temp.ApprovedBy = null;
            Temp.Over18 = Json["over_18"].BooleanValue;
            Temp.Hidden = Json["hidden"].BooleanValue;
            Temp.Thumbnail = Json["thumbnail"].StringValue;
            Temp.Subreddit = new Thing { Kind = Kind.Subreddit, ID = Json["subreddit_id"].StringValue };
            Temp.Edited = Json["edited"].BooleanValue;
            Temp.LinkFlairCSSClass = Json["link_flair_css_class"].StringValue;
            Temp.AuthorFlairCSSClass = Json["author_flair_css_class"].StringValue;
            Temp.Downvotes = Json["downs"].IntValue;
            Temp.Saved = Json["saved"].BooleanValue;
            Temp.IsSelf = Json["is_self"].BooleanValue;
            Temp.Permalink = Json["permalink"].StringValue;
            Temp.Created = Json["created"].DoubleValue.ToDateTime();
            Temp.CreatedUTC = Json["created_utc"].DoubleValue.ToDateTime();
            Temp.Url = Json["url"].StringValue;
            Temp.AuthorFlairText = Json["author_flair_text"].StringValue;
            Temp.Author = Json["author"].StringValue;
            //Temp.Media = null;
            //Temp.NumReports = null;
            Temp.Upvotes = Json["ups"].IntValue;

            return Temp;
        }

        public static Link ByID (string Input)
        {
            return Link.Create(SimpleJSON.JSONDecoder.Decode(Input)["data"]["children"][0]);
        }

        public static Link ByUrl (string Input)
        {
            var Json = SimpleJSON.JSONDecoder.Decode(Input);
            var Temp =  Link.Create(Json[0]["data"]["children"][0]["data"]);
            Temp.Comments = new List<Comment>();
            foreach (var Comment in Json[1]["data"]["children"].ArrayValue)
            {
                Temp.Comments.Add(API.Comment.Create(Comment["data"]));
            }
            return Temp;
        }
    }
}
