
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
            var metadata = reddit.r("redditdev").MetaData;
            Console.WriteLine(metadata.PublicDescription);
        }
    }
}