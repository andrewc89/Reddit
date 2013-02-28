
namespace Reddit.Things.API
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Extensions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Comment : Thing
    {
        #region Constructor

        public Comment () { }

        #endregion

        #region Properties

        private string LinkId;
        internal Link _Link;
        public Link Link
        {
            get
            {
                if (_Link == null)
                {
                    string Response = Connection.Get("/comments/" + LinkId + ".json");
                    _Link = Link.Create(SimpleJSON.JSONDecoder.Decode(Response)[0]["data"]["children"][0]["data"]);
                }
                return _Link;
            }
        }
        private string ParentId;
        private Comment _Parent;
        public Comment Parent
        {
            get
            {
                return _Parent;
            }
        }
        private string SubredditName;
        private Subreddit _Subreddit;
        public Subreddit Subreddit 
        {
            get 
            {
                if (_Subreddit == null)
                {
                    string Response = Connection.Get("/r/" + SubredditName + ".json");
                    _Subreddit = Subreddit.Create(SubredditName, SimpleJSON.JSONDecoder.Decode(Response)["data"]);
                }
                return _Subreddit;
            }
        }

        //public User BannedBy { get; set; }
        public int Likes { get; set; }
        private List<Comment> _Comments;
        public List<Comment> Comments
        {
            get
            {
                if (_Comments == null || _Comments.Count == 0)
                {
                    // load the comment's replies
                }
                return _Comments;
            }
        }
        public int Gilded { get; set; }
        private string AuthorName;
        private User _Author;
        public User Author
        {
            get
            {
                if (_Author == null)
                {
                    string Response = Connection.Get("/user/" + AuthorName + "/about.json");
                    _Author = User.Create(SimpleJSON.JSONDecoder.Decode(Response)["data"]);
                }
                return _Author;
            }
        }
        //public User ApprovedBy { get; set; }
        private string _Content;
        public string Content
        {
            get { return _Content; }
            set
            {
                _Content = value;
                Edited = true;
                string Response = Connection.Post("/api/editusertext", "text=" + value + "&thing_id=" + this.ToString());
                var Json = SimpleJSON.JSONDecoder.Decode(Response)["json"]["data"]["things"].ArrayValue[0]["data"];
                this.ContentHtml = Json["contentHTML"].StringValue;
            }
        }
        public string ContentHtml { get; set; }
        public bool Edited { get; set; }
        public string AuthorFlairText { get; set; }
        public string AuthorFlairCSSClass { get; set; }
        public int Downvotes { get; set; }        
        public DateTime Created { get; set; }
        public DateTime CreatedUTC { get; set; }
        //public int NumReports { get; set; }
        public int Upvotes { get; set; }

        #endregion

        #region Functions

        public Thing Reply (string CommentMarkdown)
        {
            string PostData = new StringBuilder()
                .Append("thing_id=").Append(this.ToString())
                .Append("&text=").Append(CommentMarkdown)
                .ToString();
            string Response = Connection.Post("/api/comment", PostData);
            return Thing.Get(SimpleJSON.JSONDecoder.Decode(Response)["json"]["data"]["things"].ArrayValue[0]["data"]["id"].StringValue);
        }

        public void Edit (string Content)
        {
            this.Content = Content;
        }

        public bool Delete ()
        {
            string PostData = new StringBuilder()
                .Append("id=").Append(this.ToString())
                .ToString();
            string Response = Connection.Post("http://reddit.com/api/del", PostData);
            return true;
        }

        #endregion

        #region Factory

        internal static Comment Create (SimpleJSON.JObject Json)
        {
            var Temp = new Comment();

            Temp.LinkId = Json["link_id"].StringValue.Remove(0, 3);
            Temp.ID = Json["id"].StringValue;
            Temp.Kind = Kind.Comment;            
            //Temp.BannedBy = Json["banned_by"];            
            Temp.Likes = Json["likes"].IntValue;
            Temp._Comments = new List<Comment>();
            if (Json["replies"].ObjectValue != null && Json["replies"]["data"].ObjectValue != null)
            {
                foreach (var Reply in Json["replies"]["data"]["children"].ArrayValue)
                {
                    var ReplyObj = Comment.Create(Reply["data"]);
                    ReplyObj._Parent = Temp;
                    Temp._Comments.Add(ReplyObj);
                }
            }
            Temp.Gilded = Json["gilded"].IntValue;
            Temp.AuthorName = Json["author"].StringValue;
            Temp.ParentId = Json["parent_id"].StringValue;
            //Temp.ApprovedBy = Json["approved_by"];
            Temp._Content = Json["body"].StringValue;
            Temp.Edited = Json["edited"].BooleanValue;
            Temp.AuthorFlairText = Json["author_flair_text"].StringValue;
            Temp.AuthorFlairCSSClass = Json["author_flair_css_class"].StringValue;
            Temp.Downvotes = Json["downs"].IntValue;
            Temp.ContentHtml = Json["body_html"].StringValue;
            Temp.SubredditName = Json["subreddit"].StringValue;
            Temp.Created = Json["created"].DoubleValue.ToDateTime();
            Temp.CreatedUTC = Json["created_utc"].DoubleValue.ToDateTime();
            //Temp.NumReports = Json["num_reports"];
            Temp.Upvotes = Json["ups"].IntValue;

            return Temp;
        }

        internal static Comment Create (string Input)
        {
            return Create(SimpleJSON.JSONDecoder.Decode(Input)["data"]);
        }

        #endregion
    }
}