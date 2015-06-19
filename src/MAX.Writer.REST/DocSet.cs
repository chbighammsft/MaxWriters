using System;
using System.Collections.Generic;
using System.Linq;
using Vipr.Core.CodeModel;

namespace Max.Writer.Rest
{
    internal class DocSet
    {
        private OdcmModel model;

        public IEnumerable<Namespace> Namespaces { get; private set; }
        public List<Entity> Entities { get; private set; }
        public Dictionary<string, RootType> RootTypes { get; private set; }

        public DocSet(OdcmModel model)
        {
            this.model = model;

            Namespaces = model.Namespaces.Where(n => !n.Name.Equals("edm", StringComparison.OrdinalIgnoreCase))
                .Select(n => new Namespace(n, model));

            Entities = new List<Entity>();
            RootTypes = new Dictionary<string, RootType>();
            foreach (var entity in model.EntityContainer.Properties)
            {
                Entities.Add(new Entity(entity));
                if (!RootTypes.ContainsKey(entity.Projection.Type.Name))
                {
                    RootTypes.Add(entity.Projection.Type.Name, new RootType(entity));
                }
            }
        }
    }
}