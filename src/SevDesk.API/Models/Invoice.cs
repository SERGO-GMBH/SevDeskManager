﻿namespace SERGO.SevDesk.API.Models
{
    using System;
    using System.Text.Json.Serialization;

    public class Invoice
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("invoiceNumber")]
        public string InvoiceNumber { get; set; }

        [JsonPropertyName("invoiceDate")]
        public string InvoiceDate { get; set; }

        [JsonPropertyName("costCentre")]
        public CostCentre CostCentre { get; set; }

        [JsonPropertyName("invoiceType")]
        public string InvoiceType { get; set; }


        [JsonPropertyName("addressName")]
        public string AddressName { get; set; }


        [JsonPropertyName("sumNet")]
        public string SumNet { get; set; }


        [JsonPropertyName("sumGross")]
        public string SumGross { get; set; }


        [JsonPropertyName("sumTax")]
        public string SumTax { get; set; }
    }
}
