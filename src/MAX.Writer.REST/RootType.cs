using Vipr.Core.CodeModel;

namespace Max.Writer.Rest
{
    internal class RootType
    {
        private OdcmProperty entity;

        public string Name { get { return entity.Name; } }
        public string TypeName { get { return entity.Projection.Type.Name; } }

        public RootType(OdcmProperty entity)
        {
            this.entity = entity;
        }
    }
}