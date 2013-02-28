
namespace Reddit.Things.API
{
    using System;
    using System.Collections.Generic;
    using Extensions;
    using SimpleJSON;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Me : Thing
    {
        #region Constructor

        public Me () 
        {
            this.Kind = Kind.Account;
        }

        #endregion

        #region Properties

        public string ModHash { get; set; }
        public string Name { get; set; }
        public bool HasMail { get; set; }
        public bool HasModMail { get; set; }
        public DateTime Created { get; set; }        
        public DateTime CreatedUTC { get; set; }
        public long LinkKarma { get; set; }
        public long CommentKarma { get; set; }
        public bool IsGold { get; set; }
        public bool IsMod { get; set; }

        #endregion

        #region Public Functions

        public List<Message> Mail (Enums.Messages messages = null)
        {
            if (messages == null)
            {
                messages = Enums.Messages.Inbox;
            }
            string Response = Connection.Get("/message/" + messages.Arg + ".json");
            var Messages = new List<Message>();
            foreach (var Message in SimpleJSON.JSONDecoder.Decode(Response)["data"]["children"].ArrayValue)
            {
                Messages.Add(Things.API.Message.Create(Message));
            }

            return Messages;
        }

        #endregion

        #region Factory

        internal static Me Create (JObject Json)
        {
            var Temp = new Me();            

            Temp.ID = Json["id"].StringValue;
            Temp.ModHash = Json["modhash"].StringValue;
            Temp.Name = Json["name"].StringValue;

            Temp.HasMail = Json["has_mail"].BooleanValue;
            Temp.HasModMail = Json["has_mod_mail"].BooleanValue;

            Temp.Created = Json["created"].DoubleValue.ToDateTime();
            Temp.CreatedUTC = Json["created_utc"].DoubleValue.ToDateTime();

            Temp.LinkKarma = Json["link_karma"].LongValue;
            Temp.CommentKarma = Json["comment_karma"].LongValue;

            Temp.IsGold = Json["is_gold"].BooleanValue;
            Temp.IsMod = Json["is_mod"].BooleanValue;           

            return Temp;
        }

        internal static Me Create (string Input)
        {
            return Me.Create(SimpleJSON.JSONDecoder.Decode(Input)["data"]);
        }

        #endregion
    }
}