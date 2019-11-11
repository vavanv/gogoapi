using System;
using System.Collections.Generic;
using System.Text;

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
