using System;
using TemplateDemo.Core.Interfaces;

namespace TemplateDemo.Core.Entities
{
    public class EntityBase : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public EntityBase()
        {
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
        }
    }
}