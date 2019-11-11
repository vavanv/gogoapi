using System;

using Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Services.DbConext;

namespace Services.DbContextFactory
{
    public class GoGoContextFactory : IGoGoContextFactory
    {
        private readonly DbContext _context;

        public GoGoContextFactory(IOptions<EnvironmentStringKey> connectionStringKey)
        {
            _context = new GoGoDbContext(connectionStringKey.Value.KeyValue);
        }


        public DbContext GetContext()
        {
            return _context;
        }
    }
}