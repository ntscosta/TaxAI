using System.Xml.Schema;
using TaxAI.TaxDocAutoGen.Extensions;

namespace TaxAI.TaxDocAutoGen.XsdProcessing
{
    public class PropertyMappingInfo
    {
        public PropertyMappingInfo(XmlSchemaElement element)
        {
            Name = (element.Name ?? element.QualifiedName.Name).ToPascalCase();
            XsdMapping = XsdTypeMapping.Mapping(element);
        }
        public PropertyMappingInfo(XmlSchemaAttribute attribute)
        {
            Name = (attribute.Name ?? attribute.QualifiedName.Name).ToPascalCase();
            XsdMapping = XsdTypeMapping.Mapping(attribute);
        }

        public string Name { get; set; }
        public XsdTypeMapping XsdMapping { get; }
        public TypeMappingInfo? TypeMapping { get; }

        public override string? ToString()
        {
            return Name;
        }
    }
}
