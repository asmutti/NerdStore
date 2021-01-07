using System.Collections.Generic;

namespace NS.Identity.API.Models
{
    public class UserTokenModel
    {
        public string Id { get; set; }
        
        public string Email { get; set; }
        
        public IEnumerable<UserClaimModel> Claims { get; set; }
    }
}