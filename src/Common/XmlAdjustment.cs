using System;
using System.Collections.Generic;
using System.Text;

namespace Parse5.Common
{
    public class XmlAdjustment
    {
        public readonly string prefix;
        public readonly string name;
        public readonly string @namespace;
        public XmlAdjustment(string prefix, string name, string @namespace)
        {
            this.prefix = prefix;
            this.name = name;
            this.@namespace = @namespace;
        }
    }
}
