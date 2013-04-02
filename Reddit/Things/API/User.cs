using System;
using System.Collections.Generic;
using Reddit.Extensions;
using Reddit.Things.API.Enums;

namespace Reddit.Things.API
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class User : Thing
    {
        #region Properties

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
        private List<Comment> _Comments;
        public List<Comment> Comments
        {
            get
            {
                if (_Comments == null)
                {
                    _Comments = GetComments();
                }
                return _Comments;
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sort">one of: SortBy.Hot, SortBy.New, SortBy.Top, SortBy.Controversial</param>
        /// <param name="From">one of: From.ThisHour, From.Today, From.ThisWeek, From.ThisMonth, From.ThisYear, From.Forever</param>
        /// <returns></returns>
        public List<Comment> GetComments(SortBy Sort = null, From From = null)
        {
            if (Sort == null)
            {
                Sort = SortBy.Hot;
            }
            if (From == null)
            {
                From = Enums.From.AllTime;
            }
            if (Sort == SortBy.Best || Sort == SortBy.Old)
            {
                throw new Exception("Can't apply SortBy.Best or SortBy.Old in this context");
            }
            string Response = Connection.Get("user/" + Name + "/comments.json", "sort=" + Sort.Arg + "&t=" + From.Arg);
            var Comments = new List<Comment>();
            foreach (var Comment in SimpleJSON.JSONDecoder.Decode(Response)["data"]["children"].ArrayValue)
            {
                Comments.Add(API.Comment.Create(Comment["data"]));
            }
            return Comments;
        }

        #endregion

        #region Factories

        internal static User Create (SimpleJSON.JObject Json)
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

        internal static User ByID (string Input)
        {
            throw new NotImplementedException();
        }

        internal static User ByUrl (string Input)
        {
            return Create(SimpleJSON.JSONDecoder.Decode(Input)["data"]);
        }

        #endregion
    }
}