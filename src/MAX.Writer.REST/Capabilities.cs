using System;
using Vipr.Core.CodeModel.Vocabularies.Capabilities;

namespace Max.Writer.Rest
{
    public class Capabilities
    {
        public bool Insertable { get { return InsertCapability.Insertable; } }
        public bool Updatable { get { return UpdateCapability.Updatable; } }
        public bool Deletable { get { return DeleteCapability.Deletable; } }
        public bool Expandable { get { return ExpandCapability.Expandable; } }
        

        private OdcmInsertCapability InsertCapability;
        private OdcmUpdateCapability UpdateCapability;
        private OdcmDeleteCapability DeleteCapability;
        private OdcmExpandCapability ExpandCapability;

        internal void Add(OdcmCapability capability)
        {
            if (capability.TermName.Contains("InsertRestrictions"))
            {
                InsertCapability = (OdcmInsertCapability)capability;
            }
            else if (capability.TermName.Contains("UpdateRestrictions"))
            {
                UpdateCapability = (OdcmUpdateCapability)capability;
            }
            else if (capability.TermName.Contains("DeleteRestrictions"))
            {
                DeleteCapability = (OdcmDeleteCapability)capability;
            }
            else if (capability.TermName.Contains("Expand"))
            {
                ExpandCapability = (OdcmExpandCapability)capability;
            } else
            {
                Console.WriteLine("Unhandled capability");
            }
        }
    }
}