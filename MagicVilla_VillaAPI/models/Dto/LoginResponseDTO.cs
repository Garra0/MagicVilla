using System;
namespace MagicVilla_VillaAPI.models.Dto
{
	public class LoginResponseDTO
	{
		public LocalUser User { get; set; }
		public string Token { get; set; }
	}
}

