using System;

using Microsoft.EntityFrameworkCore;

namespace Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        void SaveChangesAsync();
        void SaveChanges();
        DbContext Context { get; }
    }
}