using System;
namespace MagicVilla_VillaAPI.models.Dto
{
	public class LoginResponseDTO
	{
        //public LocalUser User { get; set; } --> we dont need 'LocalUser' anymore because i have table(role,pass)
        public UserDTO User { get; set; }
        // we will use the Role from the Token 
        public string Token { get; set; }
	}
}

