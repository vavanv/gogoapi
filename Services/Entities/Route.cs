using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Services.Entities
{
    public class Route : BaseEntity
    {
        public string RouteId { get; set; }
        public string AgencyId { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public int Type { get; set; }
        public string Color { get; set; }
        public string TextColor { get; set; }

    }

    public class RouteConfig : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> entity)
        {
            entity.ToTable("Routes");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            entity.Property(e => e.RouteId).IsRequired();
            entity.Property(e => e.AgencyId).IsRequired();
            entity.Property(e => e.ShortName).IsRequired();
            entity.Property(e => e.LongName).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Color).IsRequired();
            entity.Property(e => e.TextColor).IsRequired();
        }
    }
}
