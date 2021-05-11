namespace SERGO.SevDesk.API.Models
{
    using System;
    using System.Text.Json.Serialization;

    public class Voucher
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("supplierNameAtSave")]
        public string SupplierNameAtSave { get; set; }

        [JsonPropertyName("costCentre")]
        public CostCentre CostCentre { get; set; }

        [JsonPropertyName("sumNet")]
        public string SumNet { get; set; }

        [JsonPropertyName("sumGross")]
        public string SumGross { get; set; }

        [JsonPropertyName("sumTax")]
        public string SumTax { get; set; }
    }
}
