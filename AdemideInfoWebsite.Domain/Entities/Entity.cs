using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(1); 
}
