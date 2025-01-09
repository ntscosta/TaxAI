using System.Xml.Schema;
using TaxAI.TaxDocAutoGen.Extensions;

namespace TaxAI.TaxDocAutoGen.XsdProcessing
{
    public class ClassMappingInfo
    {
        public ClassMappingInfo(XmlSchemaElement element, XmlSchemaElement? parent = null)
        {
            Name = (element.Name ?? element.QualifiedName.Name).ToPascalCase();
            ForeignKey = parent != null ? new PropertyMappingInfo(parent) : null;
            propertyMappingInfos = XsdProcessor.PropertiesForElement(element);
        }

        public string Name { get; }
        public PropertyMappingInfo? ForeignKey { get; }
        public PropertyMappingInfo[] propertyMappingInfos { get; }
    }
}
