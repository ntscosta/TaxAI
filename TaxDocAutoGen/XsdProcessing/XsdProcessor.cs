using System.Xml.Schema;

namespace TaxAI.TaxDocAutoGen.XsdProcessing
{
    public class XsdProcessor
    {
        private readonly SchemaReader _schemaReader;
        private static string[] Namespaces = ["http://www.portalfiscal.inf.br/nfe", "http://www.portalfiscal.inf.br/cte", ""];

        public XsdProcessor(SchemaReader schemaReader)
        {
            _schemaReader = schemaReader ?? throw new ArgumentNullException(nameof(schemaReader));
            ClassElements = new List<ClassMappingInfo>();
        }
        public ICollection<ClassMappingInfo> ClassElements { get; private set; }

        public void ProcessXsd(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("O caminho do arquivo não pode ser vazio ou nulo.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"O arquivo especificado não foi encontrado: {filePath}");

            XmlSchemaSet schemaSet;

            try
            {
                schemaSet = _schemaReader.LoadSchema(filePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao carregar o esquema do arquivo: {filePath}", ex);
            }

            foreach (XmlSchema schema in schemaSet.Schemas())
            {
                ProcessSchema(schema);
            }
        }
        private void ProcessSchema(XmlSchema schema)
        {
            if (schema == null) throw new ArgumentNullException(nameof(schema));

            foreach (XmlSchemaElement element in schema.Elements.Values)
            {
                ProcessElement(element);
            }
        }
        private void ProcessElement(XmlSchemaElement element, XmlSchemaElement? parent = null)
        {
            if (Namespaces.Contains(element.QualifiedName.Namespace) &&
                element.ElementSchemaType is XmlSchemaComplexType complexType)
            {

                if(element.Name == "prod")
                {

                }
                var classMapping = new ClassMappingInfo(element, parent);

                ClassElements.Add(classMapping);
                ProcessComplextype(complexType, element);
            }
        }
        private void ProcessComplextype(XmlSchemaComplexType complexType, XmlSchemaElement? parent = null)
        {
            if (complexType.ContentTypeParticle is XmlSchemaSequence sequence)
            {
                foreach (var item in sequence.Items)
                {
                    if (item is XmlSchemaElement element)
                    {
                        ProcessElement(element, parent);
                    }
                }
            }
            else if (complexType.ContentTypeParticle is XmlSchemaChoice choice)
            {
                foreach (var item in choice.Items)
                {
                    if (item is XmlSchemaElement element)
                    {
                        ProcessElement(element, parent);
                    }
                }
            }
        }
        public static PropertyMappingInfo[] PropertiesForElement(XmlSchemaElement element)
        {
            if (element.ElementSchemaType is XmlSchemaComplexType complexType)
            {
                var properties = Array.Empty<PropertyMappingInfo>();

                if(complexType.Attributes.Count > 0)
                {
                    properties = properties.Concat(complexType.Attributes.OfType<XmlSchemaAttribute>()
                        .Select(a => new PropertyMappingInfo(a))).ToArray();
                }
                if (complexType.Particle is XmlSchemaSequence sequence)
                {
                    return PropertiesForElement(sequence, properties);
                }
                if (complexType.Particle is XmlSchemaChoice choice)
                {
                    return PropertiesForElement(choice, properties);
                }
            }
            return Array.Empty<PropertyMappingInfo>();
        }
        private static PropertyMappingInfo[] PropertiesForElement(XmlSchemaSequence sequence, PropertyMappingInfo[] propertyMappings)
        {
            foreach (var item in sequence.Items)
            {
                if (item is XmlSchemaElement element)
                {
                    propertyMappings = [.. propertyMappings, new PropertyMappingInfo(element)];
                }
                if(item is XmlSchemaSequence innerSequence)
                {
                    propertyMappings = PropertiesForElement(innerSequence, propertyMappings);
                }
                if(item is XmlSchemaChoice choice)
                {
                    propertyMappings = PropertiesForElement(choice, propertyMappings);
                }
            }
            return propertyMappings;
        }
        private static PropertyMappingInfo[] PropertiesForElement(XmlSchemaChoice choice, PropertyMappingInfo[] propertyMappings)
        {
            foreach (var item in choice.Items)
            {
                if (item is XmlSchemaElement element)
                {
                    propertyMappings = [.. propertyMappings, new PropertyMappingInfo(element)];
                }
                if (item is XmlSchemaSequence innerSequence)
                {
                    propertyMappings = PropertiesForElement(innerSequence, propertyMappings);
                }
                if (item is XmlSchemaChoice innerChoice)
                {
                    propertyMappings = PropertiesForElement(innerChoice, propertyMappings);
                }
            }
            return propertyMappings;
        }
    }
}
