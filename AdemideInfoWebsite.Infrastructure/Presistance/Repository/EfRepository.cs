using AdemideInfoWebsite.Application.Abstractions;
using AdemideInfoWebsite.Domain.Entities;
using AdemideInfoWebsite.Infrastructure.Presistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text; 

namespace AdemideInfoWebsite.Infrastructure.Presistance.Repository;

//MainRepository class to provide repository methods for other repositories. It can be used to implement common repository methods that can be shared across different repositories.
public sealed class EfRepository<T>(MainDbContext dbContext) : IRepository<T> where T : Entity
{
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
     
    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        dbContext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);

    public Task<List<T>> ListAsync(Func<IQueryable<T>, IQueryable<T>>? shape = null, CancellationToken cancellationToken = default)
    {
        var query = shape?.Invoke(dbContext.Set<T>()) ?? dbContext.Set<T>();
        return query.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default) =>
        await dbContext.Set<T>().AddAsync(entity, cancellationToken);

    //update entity
    public void Update(T entity) => dbContext.Set<T>().Update(entity);

    public void Remove(T entity, CancellationToken cancellationToken = default) => dbContext.Set<T>().Remove(entity);

    //count entities
    public Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default) =>
        predicate is null
            ? dbContext.Set<T>().CountAsync(cancellationToken)
            : dbContext.Set<T>().CountAsync(predicate, cancellationToken);
}

