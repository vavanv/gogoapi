using System;

using Microsoft.EntityFrameworkCore;

namespace Services.DbContextFactory
{
    public interface IGoGoContextFactory
    {
        DbContext Create();
    }
}