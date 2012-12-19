
namespace Reddit.Things.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Listing
    {
        public string Url { get; set; }
        public Thing Before { get; set; }
        public Thing After { get; set; }
        public List<Thing> Things { get; set; }
    }
}
