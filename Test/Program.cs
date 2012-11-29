using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reddit;
using Reddit.JSON;

namespace Test
{
    class Program
    {
        static void Main (string[] args)
        {
            var Reddit = new Reddit.Reddit();
            Console.WriteLine(Reddit.Login("GrammarNazism", "wazzup2"));
            Reddit.GetSubreddit("testjswrapper");
            Reddit.PostLink("testing again", "github.com", "testjswrapper");
        }
    }
}
