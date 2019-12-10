using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Services.Entities
{
    public class Stop : BaseEntity
    {
        public string StopId { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ZoneId { get; set; }
        public string Url { get; set; }
        public int Type { get; set; }
        public string ParentStation { get; set; }
        public bool WheelchairBoarding { get; set; }
        public string Code { get; set; }
    }

    public class StopConfig : IEntityTypeConfiguration<Stop>
    {
        public void Configure(EntityTypeBuilder<Stop> entity)
        {
            entity.ToTable("Stops");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            entity.Property(e => e.StopId).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Latitude).IsRequired();
            entity.Property(e => e.Longitude).IsRequired();
            entity.Property(e => e.ZoneId).IsRequired();
            entity.Property(e => e.Url).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.ParentStation).IsRequired(false);
            entity.Property(e => e.WheelchairBoarding).IsRequired();
            entity.Property(e => e.Code).IsRequired(false);
        }
    }
}
