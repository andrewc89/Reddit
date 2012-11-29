
namespace Reddit.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class DoubleExtensions
    {
        public static DateTime ToDateTime (this double UnixTime)
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dt = dt.AddSeconds(UnixTime).ToLocalTime();
            return dt;
        }
    }
}
