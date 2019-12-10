using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Services.Entities
{
    public class Trip : BaseEntity
    {
        public string RouteId { get; set; }
        public string ServiceId { get; set; }
        public string TripId { get; set; }
        public string HeadSign { get; set; }
        public string ShortName { get; set; }
        public int DirectionId { get; set; }
        public string BlockId { get; set; }
        public string ShapeId { get; set; }
        public bool WheelchairAccessible { get; set; }
        public bool BikesAllowed { get; set; }
        public string Variant { get; set; }
    }

    public class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> entity)
        {
            entity.ToTable("Trips");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            entity.Property(e => e.RouteId).IsRequired();
            entity.Property(e => e.ServiceId).IsRequired();
            entity.Property(e => e.TripId).IsRequired();
            entity.Property(e => e.HeadSign).IsRequired();
            entity.Property(e => e.ShortName).IsRequired(false);
            entity.Property(e => e.DirectionId).IsRequired();
            entity.Property(e => e.BlockId).IsRequired();
            entity.Property(e => e.ShapeId).IsRequired();
            entity.Property(e => e.WheelchairAccessible).IsRequired();
            entity.Property(e => e.BikesAllowed).IsRequired();
            entity.Property(e => e.Variant).IsRequired();
        }
    }
}
