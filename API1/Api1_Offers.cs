using System.Text.Json.Serialization;

namespace API1
{
    public class Api1_Offers
    {
        [JsonPropertyName("contact_address")]
        public string ContactAddress { get; set; }

        [JsonPropertyName("warehouse_address")]
        public string WarehouseAddress { get; set; }

        [JsonPropertyName("package_dimensions")]
        public int[] PackageDimensions { get; set; }
    }

    public class Api1_Output
    {
        [JsonPropertyName("total")]
        public long Total { get; set; }
    }
}
