using System.Linq.Expressions;

namespace Models.Application.Interfaces;

public interface IRepositorio<T> where T : class 
{
    public  Task<bool> Create(T  entity);

    public  Task<IEnumerable<T>> Get(Expression<Func<T, bool>>? filter = null, 
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "" );

    public Task<T?> GetById(string id);

    public Task<bool> Update( T entity);

    public Task<bool> Delete( T entity);


}
