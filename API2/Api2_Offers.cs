using System.Text.Json.Serialization;

namespace API2
{
    public class Api2_Offers
    {
        [JsonPropertyName("consignee")]
        public string Consignee { get; set; }

        [JsonPropertyName("consignor")]
        public string Consignor { get; set; }

        [JsonPropertyName("cartons")]
        public int[] Cartons { get; set; }
    }

    public class Api2_Output
    {
        [JsonPropertyName("amount")]
        public long Amount { get; set; }
    }
}