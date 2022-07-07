using System.Xml.Serialization;

namespace API3
{
    [XmlType("xml")]
    public class Api3_Offers
    {
        [XmlElement("source")]
        public string Source { get; set; }

        [XmlElement("destination")]
        public string Destination { get; set; }

        [XmlElement("packages")]
        public Packages Packages { get; set; }
    }

    public class Packages
    {
        [XmlElement("package")]
        public long[] Package { get; set; }
    }

    [XmlType("xml")]
    public class Api3_Output
    {
        [XmlElement("quote")]
        public long Quote { get; set; }
    }
}