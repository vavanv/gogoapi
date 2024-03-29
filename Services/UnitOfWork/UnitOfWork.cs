﻿using System;

using Microsoft.EntityFrameworkCore;

using Services.DbContextFactory;

namespace Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;

        public UnitOfWork(IGoGoContextFactory contextFactory)
        {
            Context = contextFactory.Create();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DbContext Context { get; }

        public void SaveChangesAsync()
        {
            Context?.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            Context?.SaveChanges();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    Context.Dispose();

            _disposed = true;
        }
    }
}