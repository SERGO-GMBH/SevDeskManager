namespace SERGO.SevDesk.API.Models
{
    using System.Text.Json.Serialization;
    using System.Collections.Generic;

    public class RequestResponseList<T>
    {
        [JsonPropertyName("objects")]
        public List<T> Objects { get; set; }
    }

}