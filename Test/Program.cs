using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reddit;
using Reddit.JSON;
using Reddit.Things;

namespace Test
{
    class Program
    {
        static void Main (string[] args)
        {
            var Reddit = new Reddit.Reddit("testing C# reddit API wrapper by /u/GrammarNazism, https://www.github.com/theyshookhands/Reddit");
            Console.WriteLine(Reddit.Login("testjswrapper", "testjswrapper"));
            //var Link = Reddit.GetLink("www.reddit.com/r/testjswrapper/comments/1409co/test/");
            //var Comment = Reddit.PostComment(Link, "testing comment posting again");
            //Reddit.PostComment(Comment, "responding to test comment");

            var Messages = Reddit.GetInbox();
            foreach (var Message in Messages)
            {
                Console.WriteLine(Message.Body);
            }
        }
    }
}
