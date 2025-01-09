using System.Xml;
using System.Xml.Schema;

namespace TaxAI.TaxDocAutoGen.XsdProcessing
{
    public class SchemaReader
    {
        public XmlSchemaSet LoadSchema(string schemaPath)
        {
            var schemaSet = new XmlSchemaSet();
            var baseDirectory = Path.GetDirectoryName(schemaPath);

            var rootSchema = GetSchema(schemaPath);
            if (rootSchema != null)
            {
                schemaSet.Add(rootSchema);

                schemaSet.ValidationEventHandler += (sender, args) =>
                {
                    Console.WriteLine($"Erro ao compilar o esquema: {args.Message}");
                };

                FilesIncludes(rootSchema, baseDirectory, schemaSet);

                schemaSet.Compile();
            }

            return schemaSet;
        }
        private static XmlSchema? GetSchema(string schemaFile)
        {
            using Stream stream = File.OpenRead(schemaFile);
            using XmlReader reader = XmlReader.Create(stream, null);
            return XmlSchema.Read(reader, null);
        }
        private void FilesIncludes(XmlSchema xmlSchema, string schemaPath, XmlSchemaSet SchemaSet)
        {
            foreach (var include in xmlSchema.Includes)
            {
                if (include is XmlSchemaInclude schemaInclude)
                {
                    var schema = GetSchema(Path.Combine(schemaPath, schemaInclude.SchemaLocation));
                    SchemaSet.Add(schema);
                    FilesIncludes(schema, schemaPath, SchemaSet);
                }
                if (include is XmlSchemaImport schemaImport)
                {
                    var schema = GetSchema(Path.Combine(schemaPath, schemaImport.SchemaLocation));
                    SchemaSet.Add(schema);
                    FilesIncludes(schema, schemaPath, SchemaSet);
                }
            }
        }
        private void ValidationCallback(object? sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.WriteLine($"Aviso: {e.Message}");
            }
            else
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
        }
    }
}
