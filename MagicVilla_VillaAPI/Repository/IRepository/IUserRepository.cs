using System;
using MagicVilla_VillaAPI.models;
using MagicVilla_VillaAPI.models.Dto;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
	public interface IUserRepository
	{
		bool IsUniqueUser(string username);
		Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
		Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
	}
}

