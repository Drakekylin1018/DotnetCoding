using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using System.Runtime.CompilerServices;

namespace DotnetCoding.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContextClass _dbContext;

        protected GenericRepository(DbContextClass context)
        {
            _dbContext = context;
        }
        
        /// <summary>
        /// Get all items of one type
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get one item base on id for one type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Create one item for one entity
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Insert(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            _dbContext.Set<T>().Add(obj);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Update one item for one entity
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Update(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            _dbContext.Set<T>().Update(obj);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Delete one item for one entity
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var obj = _dbContext.Set<T>().Find(id);

            if (obj != null)
            {
                _dbContext.Set<T>().Remove(obj);
                _dbContext.SaveChanges();
            }
        }
    }
}
