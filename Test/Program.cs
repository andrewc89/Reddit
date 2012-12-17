using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reddit;
using Reddit.Things;

namespace Test
{
    class Program
    {
        static void Main (string[] args)
        {
            var Reddit = new Reddit.Reddit("testing C# reddit API wrapper by /u/GrammarNazism, https://www.github.com/theyshookhands/Reddit");
            Console.WriteLine(Reddit.Login("testjswrapper", "testjswrapper"));

            var Me = Reddit.GetUser("testjswrapper");
            Console.WriteLine(Me.Name + " --> " + Me.HasMail.ToString());
        }
    }
}