using System;
using System.Linq.Expressions;
using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.models;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Repository
{
    // class to implementation the IVillaRepository interface
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // we implementation the seem functions from the Interface repository:

        public async Task CreateAsync(Villa entity)
        {
            await _db.Villas.AddAsync(entity);
            await saveAsync();
        }

        public async Task<Villa> GetAsync(Expression<Func<Villa,bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> query = _db.Villas;
            if(!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa,bool>> filter = null)
        {
            IQueryable<Villa> query = _db.Villas;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(Villa entity)
        {
            _db.Villas.Remove(entity);
            await saveAsync();
        }

        public async Task saveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Villa entity)
        {
            _db.Villas.Update(entity);
            await saveAsync();
        }
    }
}

