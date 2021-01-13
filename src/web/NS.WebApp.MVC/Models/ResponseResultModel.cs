using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NS.WebApp.MVC.Models
{
    public class ResponseResultModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("errors")]
        public ResponseErrorMessages Errors { get; set; }
    }
    
    public class ResponseErrorMessages
    {
        [JsonPropertyName("messages")]
        public List<string> Messages { get; set; }
    }
}