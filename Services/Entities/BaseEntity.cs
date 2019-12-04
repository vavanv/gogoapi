using System;

namespace Services.Entities
{
    public class BaseEntity : IEntity
    {
        public long Id { get; set; }
    }
}