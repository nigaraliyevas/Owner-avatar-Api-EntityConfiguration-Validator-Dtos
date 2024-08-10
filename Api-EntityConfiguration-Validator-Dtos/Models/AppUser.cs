using Microsoft.AspNetCore.Identity;

namespace Api_EntityConfiguration_Validator_Dtos.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
    }
}
