using AdemideInfoWebsite.Application.Abstractions;
using AdemideInfoWebsite.Domain.Entities;
using AdemideInfoWebsite.Infrastructure.Presistance.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Infrastructure.Presistance.Repository;

public sealed class UnitOfWork(MainDbContext dbContext) : IUnitOfWork
{
    public IRepository<T> Repository<T>() where T : Entity => new EfRepository<T>(dbContext);
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}
