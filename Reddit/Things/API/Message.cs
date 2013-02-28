
namespace Reddit.Things.API
{
    using System;
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
        private string FirstMessageId;
        //private Message _FirstMessage;
        //public Message FirstMessage
        //{
        //    get
        //    {
        //        if (_FirstMessage == null)
        //        {

        //        }
        //        return _FirstMessage;
        //    }
        //}
        public bool New { get; set; }
        private string ParentId;
        //private Message _Parent;
        //public Message Parent
        //{
        //    get
        //    {
        //        if (_Parent == null)
        //        {
        //            string Response = Connection.Get("");
        //        }
        //        return _Parent;
        //    }
        //}
        //public Thing Replies { get; set; }
        public string Subject { get; set; }
        public string SubredditName { get; set; }
        public bool WasComment { get; set; }

        #endregion

        #region Factory

        internal static Message Create (SimpleJSON.JObject Json)
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
            Temp.FirstMessageId = Json["first_message"].StringValue.Remove(0 ,3);
            Temp.ID = Json["id"].StringValue;
            Temp.Kind = Kind.Message;
            Temp.New = Json["new"].BooleanValue;
            Temp.ParentId = Json["parent_id"].StringValue.Remove(0, 3);
            Temp.Subject = Json["subject"].StringValue;
            Temp.SubredditName = Json["subreddit"].StringValue;
            Temp.WasComment = Json["was_comment"].BooleanValue;

            return Temp;
        }

        #endregion
    }
}