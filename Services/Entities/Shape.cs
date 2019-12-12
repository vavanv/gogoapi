using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Services.Entities
{
    public class Shape : BaseEntity
    {
        public string ShapeId { get; set; }
        public decimal Lon { get; set; }
        public decimal Lat { get; set; }
        public int Sec { get; set; }
    }

    public class ShapeConfig : IEntityTypeConfiguration<Shape>
    {
        public void Configure(EntityTypeBuilder<Shape> entity)
        {
            entity.ToTable("Shapes");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            entity.Property(e => e.ShapeId).IsRequired();
            entity.Property(e => e.Lon).IsRequired();
            entity.Property(e => e.Lat).IsRequired();
            entity.Property(e => e.Sec).IsRequired();
        }
    }
}