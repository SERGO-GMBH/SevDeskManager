namespace SERGO.SevDesk.API.Models
{
    using System.Text.Json.Serialization;

    public class CostCentre
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("objectName")]
        public string ObjectName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(this.Name) ? string.Empty : this.Number + " - " + this.Name;
        }
    }
}
