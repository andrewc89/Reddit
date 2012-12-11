
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
    public class User : Thing
    {
        public bool HasMail { get; set; }
        public string Name { get; set; }
        public bool IsFriend { get; set; }
        public DateTime Created { get; set; }
        public DateTime CreatedUTC { get; set; }
        public int LinkKarma { get; set; }
        public int CommentKarma { get; set; }
        public bool IsGold { get; set; }
        public bool IsMod { get; set; }
        public bool HasModMail { get; set; }

        public static User Create (SimpleJSON.JObject Json)
        {
            var Temp = new User();

            Temp.ID = Json["id"].StringValue;
            Temp.Kind = Kind.Account;
            Temp.HasMail = Json["has_mail"].BooleanValue;
            Temp.Name = Json["name"].StringValue;
            Temp.IsFriend = Json["is_friend"].BooleanValue;
            Temp.Created = Json["created"].DoubleValue.ToDateTime();
            Temp.CreatedUTC = Json["created_utc"].DoubleValue.ToDateTime();
            Temp.LinkKarma = Json["link_karma"].IntValue;
            Temp.CommentKarma = Json["comment_karma"].IntValue;
            Temp.IsGold = Json["is_gold"].BooleanValue;
            Temp.IsMod = Json["is_mod"].BooleanValue;
            Temp.HasModMail = Json["has_mod_mail"].BooleanValue;

            return Temp;
        }

        public static User ByID (string Input)
        {
            throw new NotImplementedException();
        }

        public static User ByUrl (string Input)
        {
            return Create(SimpleJSON.JSONDecoder.Decode(Input)["data"]);
        }
    }
}
