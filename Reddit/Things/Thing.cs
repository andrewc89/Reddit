
namespace Reddit.Things
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Thing
    {
        public Thing () { }

        public Thing (string ID, Kind kind)
        {
            this.ID = ID;
            this.Kind = kind;
        }

        public string ID { get; set; }
        public Kind Kind { get; set; }        

        public static Thing Get (string Thing)
        {
            if (string.IsNullOrEmpty(Thing))
            {
                return new Thing();
            }
            var Split = Thing.Split('_');
            return new Thing
            {
                ID = Split[1],
                Kind = Kind.Get(Split[0])
            };
        }

        public override string ToString ()
        {
            return Kind.ToString() + "_" + ID;
        }
    }
}
