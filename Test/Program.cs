
using System;
using System.Linq;
using Reddit.Things.API.Enums;

namespace Test
{
    class Program
    {
        static void Main (string[] args)
        {
            var reddit = new Reddit.Reddit("testing C# reddit API wrapper by /u/GrammarNazism, https://www.github.com/theyshookhands/Reddit");
            reddit.Login("", "");
            var Mail = reddit.Me.Mail();
            foreach (var Message in Mail)
            {
                var author = Message.Author;
            }
        }
    }
}