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
    public class VillaRepository : Repository<Villa> ,IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
            //  breakdown
            //  return an instance of Villa type
            //  'Task' is a type that represents an asynchronous operation.
            //  It is used to indicate that a method returns a result asynchronously.
            //  The Task type allows you to work with asynchronous code
            //  and perform operations without blocking the execution thread.
            //  async : i think its dont allow to block the I/O (thred) 
        }
    }
}

