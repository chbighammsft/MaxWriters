using System.Collections.Generic;
using Vipr.Core.CodeModel;

namespace Max.Writer.Rest
{
    public class MaxClass
    {
        private OdcmClass odcmClass;

        public string Name { get { return odcmClass.Name; } }
        public List<Property> Properties { get; private set; }
        public MaxClass(OdcmClass odcmClass)
        {
            this.odcmClass = odcmClass;

            Properties = new List<Property>();
            foreach (var odcmProperty in odcmClass.Properties)
            {
                Properties.Add(new Property(odcmProperty));
            }

        }
    }
}