using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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
        public float SumNet { get; set; }

        [JsonPropertyName("sumGross")]
        public float SumGross { get; set; }

        [JsonPropertyName("sumTax")]
        public float SumTax { get; set; }
    }
}
