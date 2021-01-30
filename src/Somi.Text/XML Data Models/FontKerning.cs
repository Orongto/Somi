using System;
using System.Xml.Serialization;

namespace Nurose.Text
{
    [Serializable]
    public class FontKerning
    {
        [XmlAttribute("first")]
        public int First
        {
            get;
            set;
        }

        [XmlAttribute("second")]
        public int Second
        {
            get;
            set;
        }

        [XmlAttribute("amount")]
        public int Amount
        {
            get;
            set;
        }
    }
}
