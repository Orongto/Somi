using System;
using System.Xml.Serialization;

namespace Nurose.Text
{
    [Serializable]
    public class FontChar
    {
        [XmlAttribute("id")]
        public int ID
        {
            get;
            set;
        }

        [XmlAttribute("x")]
        public int X
        {
            get;
            set;
        }

        [XmlAttribute("y")]
        public int Y
        {
            get;
            set;
        }

        [XmlAttribute("width")]
        public int Width
        {
            get;
            set;
        }

        [XmlAttribute("height")]
        public int Height
        {
            get;
            set;
        }

        [XmlAttribute("xoffset")]
        public int XOffset
        {
            get;
            set;
        }

        [XmlAttribute("yoffset")]
        public int YOffset
        {
            get;
            set;
        }

        [XmlAttribute("xadvance")]
        public int XAdvance
        {
            get;
            set;
        }

        [XmlAttribute("page")]
        public int Page
        {
            get;
            set;
        }

        [XmlAttribute("chnl")]
        public int Channel
        {
            get;
            set;
        }
    }
}
