using System;
using System.Xml;
using System.Xml.Schema;
using TaxAI.TaxDocAutoGen.Extensions;

namespace TaxAI.TaxDocAutoGen.XsdProcessing
{
    public class XsdTypeMapping
    {
        public XsdTypeMapping(string name, string? documentation = null, int? minOcurs = null, int? maxOcurs = null, XmlSchemaUse? use = null, XsdRestriction? restriction = null)
        {
            Name = name;
            Documentation = documentation;
            MinOcurs = minOcurs;
            MaxOcurs = maxOcurs;
            XsdRestriction = restriction;
            Use = use;
        }

        public string Name {  get; set; }
        public string? Documentation {  get; set; }
        public int? MinOcurs { get; set; }
        public int? MaxOcurs { get; set; }
        public XmlSchemaUse? Use { get; set; }
        public XsdRestriction? XsdRestriction { get; set; }

        public static XsdTypeMapping Mapping(XmlSchemaElement element)
        {
            if(element.Parent is XmlSchemaSequence sequence && sequence.Parent is XmlSchemaSequence sequenceParent)
            {
                element.MinOccurs = sequence.MinOccurs;
                element.MaxOccurs = sequence.MaxOccurs;
            }

            var restriction = (element.ElementSchemaType is XmlSchemaSimpleType e) ? (e.Content is XmlSchemaSimpleTypeRestriction r)? r : null : null;
            var enumerationFacet = restriction?.Facets.OfType<XmlSchemaEnumerationFacet>();
            var lengthFacet = restriction?.Facets.OfType<XmlSchemaLengthFacet>().SingleOrDefault();
            var MinlengthFacet = restriction?.Facets.OfType<XmlSchemaMinLengthFacet>().SingleOrDefault();
            var MaxlengthFacet = restriction?.Facets.OfType<XmlSchemaMaxLengthFacet>().SingleOrDefault();
            var PatternFacet = restriction?.Facets.OfType<XmlSchemaPatternFacet>().SingleOrDefault();

            return new XsdTypeMapping(
                name: (element.SchemaType?.Name ?? element.ElementSchemaType?.Name) ?? "",
                documentation: string.Join("\n", [element.Annotation?.ItensToString(), element.ElementSchemaType?.Annotation?.ItensToString()]),
                minOcurs: int.Parse(element.MinOccurs.ToString()),
                maxOcurs: int.Parse(element.MaxOccurs.ToString()),
                restriction: new XsdRestriction(
                    @base: element.ElementSchemaType?.Name,
                    isEnum: enumerationFacet?.Any() ?? false,
                    enumValues: enumerationFacet?.Select(v => v.Value ?? "").ToList() ?? null,
                    length: lengthFacet?.Value != null? int.Parse(lengthFacet.Value) : null,
                    minLength: MinlengthFacet?.Value != null? int.Parse(MinlengthFacet.Value) : null,
                    maxLength: MaxlengthFacet?.Value != null ? int.Parse(MaxlengthFacet.Value) : null,
                    pattern: PatternFacet?.Value != null ? PatternFacet.Value : null
                    ));
        }
        public static XsdTypeMapping Mapping(XmlSchemaAttribute attribute)
        {
            return new XsdTypeMapping(
                name: attribute.AttributeSchemaType?.Name ?? "",
                documentation: string.Join("\n", attribute.AttributeSchemaType?.Annotation?.Items
                    .OfType<XmlSchemaDocumentation>()
                    .Select(d => d.Markup != null? string.Join("\n", d.Markup.OfType<XmlNode>().Select(n => n.InnerText)) : "") ?? [""]),
                use: attribute.Use,
                minOcurs: 1,
                maxOcurs: 1,
                restriction: new XsdRestriction(
                    attribute.AttributeSchemaType?.Name,
                    pattern: (attribute.AttributeSchemaType?.Content is XmlSchemaSimpleTypeRestriction restriction)? 
                        restriction.Facets.OfType<XmlSchemaPatternFacet>().Select(f => f.Value).SingleOrDefault() : null
                ));
        }
    }
    public class XsdRestriction
    {
        public XsdRestriction(string? @base, bool isEnum = false, List<string>? enumValues = null, int? length = null, int? minLength = null, int? maxLength = null, string? pattern = null)
        {
            Base = @base;
            IsEnum = isEnum;
            EnumValues = enumValues;
            Length = length;
            MinLength = minLength;
            MaxLength = maxLength;
            Pattern = pattern;
        }

        public string? Base { get; set; }
        public bool IsEnum { get; set; }
        public List<string>? EnumValues { get; set; }
        public int? Length { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public string? Pattern { get; set; }
    }
}
