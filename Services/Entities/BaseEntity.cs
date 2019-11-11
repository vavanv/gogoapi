using System;

namespace Services.Entities
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}