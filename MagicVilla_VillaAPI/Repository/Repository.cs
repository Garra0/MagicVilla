using System;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        // as wee see dbSet obj from DbSet then we dont need to use '.villas'
        // T that mean the type by the creater class
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //_db.villaNumbers.Include(u => u.Villa).ToList();
            this.dbSet = _db.Set<T>();
        }

        // we implementation the seem functions from the Interface repository:

        public async Task CreateAsync(T entity)
        {
            // i dont need to use '.villas' because its the
            // same type of this obj 'DbSet' then dbSet enough 
            await dbSet.AddAsync(entity);
            await saveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if(includeProperties != null)
            {
                foreach(var includeProp in
                    includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            //Console.WriteLine(includeProperties);
            foreach (var includeProp in
                    includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await saveAsync();
        }

        public async Task saveAsync()
        {
            await _db.SaveChangesAsync();
        }

    }
}

