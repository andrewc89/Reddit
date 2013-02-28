using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reddit.Things.API.Meta
{
    public class SubredditHeader
    {
        #region Constructor

        public SubredditHeader () { }

        #endregion

        #region Properties

        public string ImageUrl { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public string Title { get; set; }

        #endregion
    }
}