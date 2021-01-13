using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NS.WebApp.MVC.Models
{
    public class UserLoginResponseModel
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
        
        [JsonPropertyName("expiresIn")]
        public double ExpiresIn { get; set; }
        
        [JsonPropertyName("userToken")]
        public UserTokenModel UserToken { get; set; }
        
        [JsonPropertyName("responseResult")]
        public ResponseResultModel ResponseResult { get; set; }
    }
}