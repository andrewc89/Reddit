
namespace Reddit.Things.API.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class From
    {
        public readonly string Arg;

        public static readonly From ThisYear = new From("year");
        public static readonly From ThisMonth = new From("month");
        public static readonly From ThisWeek = new From("week");
        public static readonly From Today = new From("day");
        public static readonly From ThisHour = new From("hour");

        private From (string Name)
        {
            this.Arg = Name;
        }
    }
}
