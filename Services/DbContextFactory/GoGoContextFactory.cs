using System;

using Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Services.DbContextFactory
{
    public class GoGoContextFactory : IGoGoContextFactory
    {
        private readonly DbContext _context;

        public GoGoContextFactory(IOptions<DbStringKey> connectionStringKey)
        {
            _context = new GoGoDbContext(connectionStringKey.Value.KeyValue);
        }


        public DbContext Create()
        {
            return _context;
        }
    }
}