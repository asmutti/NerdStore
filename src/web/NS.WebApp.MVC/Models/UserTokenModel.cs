using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NS.WebApp.MVC.Models
{
    public class UserTokenModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("claims")]
        public IEnumerable<UserClaimModel> Claims { get; set; }
    }
}