using System;

using Microsoft.EntityFrameworkCore;

namespace Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }
        void SaveChangesAsync();
        void SaveChanges();
    }
}