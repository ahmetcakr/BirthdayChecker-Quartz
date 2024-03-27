using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Shared.Services.Abstract;

public interface IGenericContextService<T, TDbContext> where T : class where TDbContext : DbContext
{
    IEnumerable<T> GetAll();
    IEnumerable<T> Where(Expression<Func<T, bool>> expression);
}
