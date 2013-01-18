
namespace Reddit.Things.API.Enums
{    
    public sealed class Messages
    {
        public readonly string Arg;

        public static readonly Messages Inbox = new Messages("inbox");
        public static readonly Messages New = new Messages("unread");
        public static readonly Messages Sent = new Messages("sent");

        private Messages (string Messages)
        {
            this.Arg = Messages;
        }
    }
}
