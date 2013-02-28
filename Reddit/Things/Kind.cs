
namespace Reddit.Things
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Kind
    {
        private readonly int ID;
        private readonly string Name;

        public static readonly Kind Comment = new Kind(1, "Comment");
        public static readonly Kind Account = new Kind(2, "Account");
        public static readonly Kind Link = new Kind(3, "Link");
        public static readonly Kind Message = new Kind(4, "Message");
        public static readonly Kind Subreddit = new Kind(5, "Subreddit");

        private Kind (int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public static Kind Get (string Kind)
        {
            switch (Kind)
            {
                case "t1":
                    return Comment;
                case "t2":
                    return Account;
                case "t3":
                    return Link;
                case "t4":
                    return Message;
                case "t5":
                    return Subreddit;
                default:
                    return null;
            }
        }

        public override string ToString ()
        {
            return "t" + this.ID.ToString();
        }
    }
}