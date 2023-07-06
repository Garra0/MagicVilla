using System;
using System.Linq.Expressions;
using MagicVilla_VillaAPI.models;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
	public interface IVillaRepository : IRepository<Villa>
    {
        // why i have just update function here and i dont have it in the other interface or class?
        // becase this function i cant use it in all classes , every class have difference atts
        Task<Villa> UpdateAsync(Villa entity);
        
    }
}

