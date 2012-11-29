
namespace Reddit.Things.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Comment : Thing
    {
        public Comment () { }

        public Thing ID { get; set; }
        public string Content { get; set; }
        public string ContentHtml { get; set; }
        public string ContentText { get; set; }
        public Thing Link { get; set; }
        public Thing Parent { get; set; }
    }
}
