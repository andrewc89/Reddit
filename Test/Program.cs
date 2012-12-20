using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reddit;
using Reddit.Things;
using System.Threading;
using Reddit.Things.API.Enums;

namespace Test
{
    class Program
    {
        static void Main (string[] args)
        {
            var r = new Reddit.Reddit("testing C# reddit API wrapper by /u/GrammarNazism, https://www.github.com/theyshookhands/Reddit");
            Console.WriteLine(r.Login("", ""));
            var RedditDev = r.GetSubreddit("testjswrapper");
            var TopPosts = RedditDev.Top(From.ThisWeek);
            var Comment = TopPosts.First().Comment("idk what's going on but this is my opinion!");	
            
        }
    }
}