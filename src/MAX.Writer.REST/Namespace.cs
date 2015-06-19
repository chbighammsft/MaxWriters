using System.Collections.Generic;
using System.Linq;
using Vipr.Core.CodeModel;

namespace Max.Writer.Rest
{
    public class Namespace
    {
        private OdcmModel model;
        private OdcmNamespace n;

        public IEnumerable<OdcmEnum> Enums { get { return n.Enums; } }
        public IEnumerable<OdcmClass> OdcmClasses { get { return n.Classes; } }
        public List<MaxClass> Classes { get; private set; }
        public string Name { get { return n.Name; } }

        public Namespace(OdcmNamespace n, OdcmModel model)
        {
            this.n = n;
            this.model = model;

            Classes = new List<MaxClass>();
            foreach (var odcmClass in n.Classes.Where(c => c.Kind == OdcmClassKind.Complex))
            {
                Classes.Add(new MaxClass(odcmClass));
            }

        }
    }
}
