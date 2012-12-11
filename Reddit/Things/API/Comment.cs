
namespace Reddit.Things.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Extensions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Comment : Thing
    {
        public Comment () { }

        public string Content { get; set; }
        public string ContentHtml { get; set; }
        public string ContentText { get; set; }
        public Thing Link { get; set; }
        public Thing Parent { get; set; }
        public Thing Subreddit { get; set; }

        //public User BannedBy { get; set; }
        public int Likes { get; set; }
        public List<Comment> Replies { get; set; }
        public int Gilded { get; set; }
        public string AuthorName { get; set; }
        //public User ApprovedBy { get; set; }
        public string Body { get; set; }
        public string BodyHTML { get; set; }
        public bool Edited { get; set; }
        public string AuthorFlairText { get; set; }
        public string AuthorFlairCSSClass { get; set; }
        public int Downvotes { get; set; }
        public string SubredditName { get; set; }
        public DateTime Created { get; set; }
        public DateTime CreatedUTC { get; set; }
        //public int NumReports { get; set; }
        public int Upvotes { get; set; }

        public static Comment Create (SimpleJSON.JObject Json)
        {
            var Temp = new Comment();

            Temp.ID = Json["id"].StringValue;
            Temp.Kind = Kind.Comment;
            Temp.Subreddit = Thing.Get(Json["subreddit_id"].StringValue);
            //Temp.BannedBy = Json["banned_by"];            
            Temp.Likes = Json["likes"].IntValue;
            Temp.Replies = new List<Comment>();
            if (Json["replies"].ArrayValue != null)
            {
                foreach (var Reply in Json["replies"]["data"]["children"].ArrayValue)
                {
                    Temp.Replies.Add(Comment.Create(Reply));
                }
            }
            Temp.Gilded = Json["gilded"].IntValue;
            Temp.AuthorName = Json["author"].StringValue;
            Temp.Parent = Thing.Get(Json["parent_id"].StringValue);
            //Temp.ApprovedBy = Json["approved_by"];
            Temp.Body = Json["body"].StringValue;
            Temp.Edited = Json["edited"].BooleanValue;
            Temp.AuthorFlairText = Json["author_flair_text"].StringValue;
            Temp.AuthorFlairCSSClass = Json["author_flair_css_class"].StringValue;
            Temp.Downvotes = Json["downs"].IntValue;
            Temp.BodyHTML = Json["body_html"].StringValue;
            Temp.SubredditName = Json["subreddit"].StringValue;
            Temp.Created = Json["created"].DoubleValue.ToDateTime();
            Temp.CreatedUTC = Json["created_utc"].DoubleValue.ToDateTime();
            //Temp.NumReports = Json["num_reports"];
            Temp.Upvotes = Json["ups"].IntValue;

            return Temp;
        }

        public static Comment Create (string Input)
        {
            return Create(SimpleJSON.JSONDecoder.Decode(Input)["data"]);
        }
    }
}
