using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Globalization;
using System.Xml.Serialization;

namespace Nurose.Text
{
    [Serializable]
    public class FontInfo
    {
        [XmlAttribute("face")]
        public string Face
        {
            get;
            set;
        }

        [XmlAttribute("size")]
        public int Size
        {
            get;
            set;
        }

        [XmlAttribute("bold")]
        public int Bold
        {
            get;
            set;
        }

        [XmlAttribute("italic")]
        public int Italic
        {
            get;
            set;
        }

        [XmlAttribute("charset")]
        public string CharSet
        {
            get;
            set;
        }

        [XmlAttribute("unicode")]
        public int Unicode
        {
            get;
            set;
        }

        [XmlAttribute("stretchH")]
        public int StretchHeight
        {
            get;
            set;
        }

        [XmlAttribute("smooth")]
        public int Smooth
        {
            get;
            set;
        }

        [XmlAttribute("aa")]
        public int SuperSampling
        {
            get;
            set;
        }

        private Rectangle _Padding;
        [NonSerialized]
        private readonly NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;

        [XmlAttribute("padding")]
        public string Padding
        {
            get
            {
                return _Padding.X + "," + _Padding.Y + "," + _Padding.Width + "," + _Padding.Height;
            }
            set
            {
                Contract.Requires(value != null);
                string[] padding = value.Split(',');
                _Padding = new Rectangle(Convert.ToInt32(padding[0], numberFormat), Convert.ToInt32(padding[1], numberFormat), Convert.ToInt32(padding[2], numberFormat), Convert.ToInt32(padding[3], numberFormat)); ;
            }
        }

        private Point _Spacing;
        [XmlAttribute("spacing")]
        public string Spacing
        {
            get
            {
                return _Spacing.X + "," + _Spacing.Y;
            }
            set
            {
                Contract.Requires(value != null);
                string[] spacing = value.Split(',');
                _Spacing = new Point(Convert.ToInt32(spacing[0], numberFormat), Convert.ToInt32(spacing[1], numberFormat));
            }
        }

        [XmlAttribute("outline")]
        public int OutLine
        {
            get;
            set;
        }
    }

}