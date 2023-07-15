using System;
using MagicVilla_Web.models.Dto;

namespace MagicVilla_web.models.Dto
{
	public class LoginResponseDTO
	{
		public UserDTO User { get; set; }
		public string Token { get; set; }
	}
}

