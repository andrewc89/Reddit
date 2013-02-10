
namespace Reddit.Things.API.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class SortBy
    {
        internal readonly string Arg;

        public static readonly SortBy Best = new SortBy("best");
        public static readonly SortBy Top = new SortBy("top");        
        public static readonly SortBy New = new SortBy("new");
        public static readonly SortBy Hot = new SortBy("hot");
        public static readonly SortBy Controversial = new SortBy("controversial");
        public static readonly SortBy Old = new SortBy("old");       

        private SortBy (string Arg)
        {
            this.Arg = Arg;
        }
    }
}
