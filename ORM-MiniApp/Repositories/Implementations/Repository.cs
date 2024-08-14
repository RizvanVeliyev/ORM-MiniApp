using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Contexts;
using ORM_MiniApp.Models.Common;
using ORM_MiniApp.Repositories.Interfaces;
using System.Linq.Expressions;

namespace ORM_MiniApp.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly AppDbContext _appDbContext;


        public Repository()
        {
            _appDbContext = new AppDbContext();
        }
        public async Task CreateAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAllAsync(params string[] includes)
        {
            var query = _appDbContext.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }


            var result = await query.ToListAsync();


            return result;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = _appDbContext.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }


            var result = await query.ToListAsync();


            return result;
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = _appDbContext.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }


            var result = await query.FirstOrDefaultAsync(predicate);

            return result;
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _appDbContext.Set<T>().AnyAsync(predicate);

            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
        }
    }
}
