using System;
using System.Linq.Expressions;
using MagicVilla_VillaAPI.models;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
	public interface IVillaRepository
	{
        // return all villas
        Task<List<Villa>> GetAll(Expression<Func<Villa, bool>> filter = null);
        // return one villa and use 'noTracking' by this command 'bool tracked=true' 
        Task<Villa> Get(Expression<Func<Villa, bool>> filter = null , bool tracked=true);
        Task Create(Villa entity);
        Task Remove(Villa entity);
        // to save the changes 
        Task save();
    }
}

