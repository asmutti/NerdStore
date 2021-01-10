using System.Text.Json.Serialization;

namespace NS.WebApp.MVC.Models
{
    public class UserClaimModel
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
        
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}