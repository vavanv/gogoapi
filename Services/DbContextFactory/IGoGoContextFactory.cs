using System;

using Microsoft.EntityFrameworkCore;

namespace Services.DbConext
{
    public interface IGoGoContextFactory
    {
        DbContext GetContext();
    }
}