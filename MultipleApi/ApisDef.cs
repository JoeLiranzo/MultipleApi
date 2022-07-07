using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MultipleApi.ApisDef
{
    //Definitions for client, to consume differents APIs.
    public struct Api1_Input
    {
        public string contact_address { get; set; }

        public string warehouse_address { get; set; }

        public long[] package_dimensions { get; set; }
    }

    public struct Api1_Output
    {
        public long total;
    }

    public struct Api2_Input
    {
        public string consignee { get; set; }

        public string consignor { get; set; }

        public long[] cartons { get; set; }
    }

    public struct Api2_Output
    {
        public long amount { get; set; }
    }

    [XmlType("xml")]
    public struct Api3_Input
    {
        [XmlElement("source")]
        public string source { get; set; }

        [XmlElement("destination")]
        public string destination { get; set; }

        [XmlElement("packages")]
        public packages packages { get; set; }

    }

    public struct packages
    {
        [XmlElement("package")]
        public long[] package { get; set; }
    }

    [XmlType("xml")]
    public struct Api3_Output
    {
        [XmlElement("quote")]
        public long quote { get; set; }
    }
}
