using System.Collections.Generic;
using System.Linq;
using Vipr.Core.CodeModel;

namespace Max.Writer.Rest
{
    internal class Entity
    {
        private OdcmProperty entity;
        private OdcmEntityClass entityClass;

        public Capabilities Capabilities { get; private set; }
        public bool IsCollection { get { return entity.IsCollection; } }
        public string Name { get { return entity.Name; } }
        public List<Property> Properties { get; private set; }
        public List<Property> NavigationProperties { get; private set; }
        public string TypeName { get { return entity.Projection.Type.Name; } }

        public Entity(OdcmProperty entity)
        {
            this.entity = entity;
            this.entityClass = (OdcmEntityClass)entity.Projection.Type;

            Properties = new List<Property>();
            foreach (var property in entityClass.Properties.Where(p => p.IsCollection == false))
            {
                Properties.Add(new Property(property));
            }

            NavigationProperties = new List<Property>();
            foreach (var property in entityClass.Properties.Where(p => p.IsCollection == false))
            {
                NavigationProperties.Add(new Property(property));
            }

            Capabilities = new Capabilities();
            foreach (var capability in entity.Projection.Capabilities)
            {
                Capabilities.Add(capability);
            }
        }
    }
}