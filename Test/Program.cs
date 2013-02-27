
using System;
using System.Linq;
using Reddit.Things.API.Enums;

namespace Test
{
    class Program
    {
        static void Main (string[] args)
        {
            var reddit = new Reddit.Reddit("testing C# reddit API wrapper, https://www.github.com/theyshookhands/Reddit");
            reddit.Login("testjswrapper", "testjswrapper");
            var Top = reddit.r("testjswrapper").Top();
            var Comments = Top.First().Comments;
            foreach (var Comment in Comments)
            {
                Console.WriteLine("Comment: " + Comment.Content);
            }
            foreach (var Reply in Comments.First().Comments)
            {
                Console.WriteLine("Reply: " + Reply.Content);
            }
        }
    }
}