
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
    public class Message : Thing
    {
        #region Constructor

        public Message () { }

        #endregion

        #region Properties

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
        public string Body { get; set; }
        public string BodyHtml { get; set; }
        public string ContextUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime CreatedUTC { get; set; }
        public string RecipientName { get; set; }
        public Thing FirstMessage { get; set; }
        public bool New { get; set; }
        public Thing Parent { get; set; }
        //public Thing Replies { get; set; }
        public string Subject { get; set; }
        public string SubredditName { get; set; }
        public bool WasComment { get; set; }

        #endregion

        #region Factory

        public static Message Create (SimpleJSON.JObject Json)
        {
            Json = Json["data"];
            var Temp = new Message();

            Temp.AuthorName = Json["author"].StringValue;
            Temp.Body = Json["body"].StringValue;
            Temp.BodyHtml = Json["body_html"].StringValue;
            Temp.ContextUrl = "" + Json["context"].StringValue;
            Temp.Created = Json["created"].DoubleValue.ToDateTime();
            Temp.CreatedUTC = Json["created_utc"].DoubleValue.ToDateTime();
            Temp.RecipientName = Json["dest"].StringValue;
            //Temp.FirstMessage = Thing.Get(Json["first_message"].StringValue);
            Temp.ID = Json["id"].StringValue;
            Temp.Kind = Kind.Message;
            Temp.New = Json["new"].BooleanValue;
            Temp.Parent = Thing.Get(Json["parent_id"].StringValue);
            Temp.Subject = Json["subject"].StringValue;
            Temp.SubredditName = Json["subreddit"].StringValue;
            Temp.WasComment = Json["was_comment"].BooleanValue;

            return Temp;
        }

        #endregion
    }
}
