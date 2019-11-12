using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Services.Entities
{
    public class Cache : BaseEntity
    {
        public int Type { get; set; }
        public string Code { get; set; }
        public string Data { get; set; }
    }

    public class CacheConfig : IEntityTypeConfiguration<Cache>
    {
        public void Configure(EntityTypeBuilder<Cache> entity)
        {
            entity.ToTable("Cache");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Code).HasMaxLength(20);
        }
    }
}