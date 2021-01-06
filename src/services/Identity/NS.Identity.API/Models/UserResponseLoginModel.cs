namespace NS.Identity.API.Models
{
    public class UserResponseLoginModel
    {
        public string AccessToken { get; set; }
        
        public double ExpiresIn { get; set; }
        
        public UserTokenModel UserToken { get; set; }
    }
}