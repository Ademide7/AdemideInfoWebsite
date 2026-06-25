using AdemideInfoWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Abstractions;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : Entity;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
