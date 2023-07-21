using System;
namespace MagicVilla_VillaAPI.models.Dto
{
	public class LoginResponseDTO
	{
        //public LocalUser User { get; set; } --> we dont need 'LocalUser' anymore because i have table(role,pass)
        public UserDTO User { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
	}
}

