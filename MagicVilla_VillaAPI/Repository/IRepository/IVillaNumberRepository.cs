using System;
using MagicVilla_VillaAPI.models;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
	public interface IVillaNumberRepository : IRepository<VillaNumber>
	{
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
    }
}

