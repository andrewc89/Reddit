using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reddit;
using Reddit.Things;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main (string[] args)
        {
            var Reddit = new Reddit.Reddit("testing C# reddit API wrapper by /u/GrammarNazism, https://www.github.com/theyshookhands/Reddit");
            Console.WriteLine(Reddit.Login("GrammarNazism", "wazzup2"));

            var Programming = Reddit.GetSubreddit("programming");
            foreach (var Link in Programming.Links)
            {
                Console.WriteLine(Link.ID + " --> " + Link.Title);
            }
        }
    }
}