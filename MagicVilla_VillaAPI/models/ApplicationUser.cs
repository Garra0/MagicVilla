using Microsoft.AspNetCore.Identity;

namespace MagicVilla_VillaAPI.models
{
    public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}
