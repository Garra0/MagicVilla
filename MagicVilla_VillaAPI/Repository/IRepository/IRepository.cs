using System;
using MagicVilla_VillaAPI.models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    // T here is give the type for the obj ,
    // because every class have not seem type then T help us here
	public interface IRepository<T> where T : class
	{
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null , string? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task saveAsync();
    }
}

