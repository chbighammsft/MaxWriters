using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vipr.Core;
using Vipr.Core.CodeModel;

namespace Max.Writer.Rest
{
    internal class DocumentationGenerator
    {
        private DocSet docSet;
        private List<TextFile> FileList;
        private StringBuilder sb;

        public DocumentationGenerator(DocSet docSet)
        {
            this.docSet = docSet;
        }

        internal IEnumerable<TextFile> Generate()
        {
            FileList = new List<TextFile>();

            sb = new StringBuilder();

            foreach (var rootType in docSet.RootTypes.Values)
            {
                Write(rootType);
            }

            foreach (var resourceNamespace in docSet.Namespaces)
            {
                sb.AppendFormat("## {0} namespace ##", resourceNamespace.Name);
                sb.AppendLine();

                Write(resourceNamespace);
            }

            FileList.Add(new TextFile("docSet.md", sb.ToString()));

            return FileList;
        }

        private void Write(Namespace resourceNamespace)
        {
            Write(resourceNamespace.Enums);
            Write(resourceNamespace.Classes);
        }

        private void Write(IEnumerable<MaxClass> classes)
        {
            if (classes.Count() > 0)
            {
                sb.AppendLine(GetBookmark("classes"));
                sb.AppendLine("### Classes ###");
                sb.AppendLine();

                foreach (var resourceClass in classes)
                {
                    if (!docSet.RootTypes.ContainsKey(resourceClass.Name))
                    {
                        Write(resourceClass);
                        sb.AppendLine();
                    }
                }
            }
        }

        private void Write(MaxClass resourceClass)
        {
            sb.AppendLine(GetBookmark(resourceClass.Name));
            sb.AppendFormat("#### {0} class ####", resourceClass.Name);
            sb.AppendLine();

            if (resourceClass.Properties.Count() > 0)
            {
                sb.AppendLine(GetPropertyHeader());
                foreach (var property in resourceClass.Properties)
                {
                    sb.AppendFormat("|{0} | {1} | {2} |", property.Name, GetPropertyType(property), property.Description);
                    sb.AppendLine();
                }
            }
        }

        private void Write(IEnumerable<OdcmEnum> enums)
        {
            if (enums.Count() > 0)
            {
                sb.AppendLine(GetBookmark("enumerations"));
                sb.AppendLine("### Enumerations ###");
                sb.AppendLine();

                foreach (var enumeration in enums)
                {
                    Write(enumeration);
                    sb.AppendLine();
                }
            }
        }

        private void Write(OdcmEnum enumeration)
        {
            sb.AppendLine(GetBookmark(enumeration.Name));
            sb.AppendFormat("#### {0} enumeration ####", enumeration.Name);
            sb.AppendLine();
            sb.AppendLine("| Value | Name |");
            sb.AppendLine("|----- |----- |");
            foreach (var enumMember in enumeration.Members)
            {
                Write(enumMember);
            }
        }

        private void Write(OdcmEnumMember enumMember)
        {
            sb.AppendFormat("| {0} | {1} |", enumMember.Value, enumMember.Name);
            sb.AppendLine();
        }

        private void Write(RootType rootType)
        {
            sb.AppendLine(GetBookmark(rootType.TypeName));
            sb.AppendFormat("# {0} #", rootType.TypeName);
            sb.AppendLine();

            // Write service URLs for the entity. If there are multiple references
            // to the same root object (like the "Me" and "Users" endpoints in the
            // Exchange service) this will write the correct URLs.
            var entities = docSet.Entities.Where(e => e.TypeName == rootType.TypeName);
            foreach (var e in entities.OrderBy(e => e.IsCollection))
            {
                WriteURLs(e);
            }
            // Now write the properties and navigation properties for the root objects.
            // Since all of the entities in the collection share the same root, we
            // only need to send one, so we send the first.
            var entity = entities.First();

            Write(entity.Capabilities);
            Write(entity);

            sb.AppendLine();
        }

        private void Write(Capabilities capabilities)
        {
            sb.AppendLine("### Capabilities ###");
            sb.AppendLine("| Insert | Update | Delete | Expand |");
            sb.AppendLine("|----- |----- |----- |----- |");
            sb.AppendFormat("| {0} | {1} | {2} | {3} |", 
                capabilities.Insertable, capabilities.Updatable, 
                capabilities.Deletable, capabilities.Expandable);
            sb.AppendLine();
        }

        private string GetBookmark(string bookmarkName)
        {
            return string.Format("<a name=\"bk_{0}\"></a>", bookmarkName);
        }

        private void WriteURLs(Entity entity)
        {
            if (entity.IsCollection)
            {
                sb.AppendFormat(ConfigurationService.Settings.ServiceURLFormat + "/[identifier]", entity.Name);
                sb.AppendLine(); sb.AppendLine();
                sb.AppendLine("or");
                sb.AppendLine();
                sb.AppendFormat(ConfigurationService.Settings.ServiceURLFormat + "('[identifier]')", entity.Name);
                sb.AppendLine();
            }
            else
            {
                sb.AppendFormat(ConfigurationService.Settings.ServiceURLFormat, entity.Name);
                sb.AppendLine(); sb.AppendLine();
                sb.AppendLine("or"); sb.AppendLine();
            }
        }

        private void Write(Entity entity)
        {
            if (entity.Properties.Count() > 0)
            {
                sb.AppendLine(GetPropertyHeader());
                foreach (var property in entity.Properties)
                {
                    Write(property);
                }
            }
            if (entity.NavigationProperties.Count() > 0)
            {
                sb.AppendLine(GetPropertyHeader(true));
                foreach (var property in entity.NavigationProperties)
                {
                    Write(property);
                }
            }
        }

        private void Write(Property property)
        {
            sb.AppendFormat("| {0} | {1} | {2} |", property.Name, GetPropertyType(property), property.Description);
            sb.AppendLine();
        }

        private string GetPropertyType(Property property)
        {
            if (property.IsComplex)
            {
                return string.Format("[{0}](bk_{0})", property.Type);
            }
            else
            {
                return property.Type;
            }
        }

        private string GetPropertyHeader()
        {
            return GetPropertyHeader(false);
        }

        private string GetPropertyHeader(bool isNavigation)
        {
            var hsb = new StringBuilder();

            if (isNavigation)
                hsb.AppendLine("## Navigation properties ##");
            else
                sb.AppendLine("## Properties ##");

            hsb.AppendLine("| Name | Type | Description |");
            hsb.Append("|----- |----- |----- |");

            return hsb.ToString();
        }
    }
}