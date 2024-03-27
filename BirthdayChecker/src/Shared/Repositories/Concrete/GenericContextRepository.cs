using Microsoft.EntityFrameworkCore;
using Shared.Repositories.Abstract;
using System.Linq.Expressions;

namespace Shared.Repositories.Concrete;

public class GenericContextRepository<T, TDbContext> : IGenericContextRepository<T, TDbContext>
    where T : class
    where TDbContext : DbContext
{
    private readonly TDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericContextRepository(TDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet.AsNoTracking();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Where(expression).AsNoTracking();
    }

    public async Task<T> Save(T entity)
    {
        var inserted = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return inserted.Entity;
    }

    public async Task<T> Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
