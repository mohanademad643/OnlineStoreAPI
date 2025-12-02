
using Ecom.Application.Interfaces;
using Ecom.Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecom.Infastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _Context;

        public GenericRepository(AppDbContext Context)
        {
            _Context = Context;
        }

        public async Task AddAsync(T entity)
        {
            await _Context.Set<T>().AddAsync(entity);
            await _Context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        => await _Context.Set<T>().CountAsync();

        public async Task DeleteAsync(int id)
        {
            var entity = await _Context.Set<T>().FindAsync(id);
            _Context.Set<T>().Remove(entity);

             await _Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _Context.Set<T>().AsNoTracking().ToListAsync();
       

        public  async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var Query = _Context.Set<T>().AsQueryable();

            foreach (var item in includes)
            {
                Query = Query.Include(item);
            }
            return await Query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _Context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var Query = _Context.Set<T>().AsQueryable();

            foreach (var item in includes)
            {
                Query = Query.Include(item);
            }
            var entity = await Query.FirstOrDefaultAsync(x=> EF.Property<int>(x, "Id") == id);

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _Context.Entry(entity).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
        }
    }
}
