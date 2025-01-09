using TaxAI.TaxDocAutoGen.XsdProcessing;
using System.Xml.Schema;

namespace TaxAI.TaxDocAutoGen.Tests
{
    public class SchemaReaderTests
    {
        [Fact]
        public void LoadSchema_WithNFeXsd_ShouldLoadAllElements()
        {
            // Arrange
            string basePath = Path.Combine(@"D:\repo\AuditCommunity\Documentation\LeiauteEFD\Schemas\NFe\PL_009k_NT2023_001_v120");
            string nfeSchemaPath = Path.Combine(basePath, "procNFe_v4.00.xsd");

            var schemaReader = new SchemaReader();

            // Act
            var schemaSet = schemaReader.LoadSchema(nfeSchemaPath);

            // Assert
            Assert.NotNull(schemaSet);
            Assert.True(schemaSet.Count > 0, "SchemaSet não contém esquemas carregados.");

            var schemas = schemaSet.Schemas();
            Assert.NotEmpty(schemas);

            // Verifica se os elementos estão presentes
            bool hasProcElement = false, hasNFeElement = false, hasIdeElement = false;

            foreach (XmlSchema schema in schemas)
            {
                TraverseSchema(schema.Items, ref hasProcElement, ref hasNFeElement, ref hasIdeElement);
            }
                
            // Asserções finais
            Assert.True(hasProcElement, "Elemento 'nfeProc' não encontrado no esquema.");
            Assert.True(hasNFeElement, "Elemento 'NFe' não encontrado no esquema.");
            Assert.True(hasIdeElement, "Elemento 'ide' não encontrado no esquema.");
        }

        private void TraverseSchema(XmlSchemaObjectCollection items, ref bool hasProcElement, ref bool hasNFeElement, ref bool hasIdeElement)
        {
            foreach (XmlSchemaObject item in items)
            {
                if (item is XmlSchemaElement element)
                {
                    Console.WriteLine($"Elemento encontrado: {element.Name}");

                    // Verificar o elemento atual
                    switch (element.Name)
                    {
                        case "nfeProc":
                            hasProcElement = true;
                            break;
                        case "NFe":
                            hasNFeElement = true;
                            break;
                        case "ide":
                            hasIdeElement = true;
                            break;
                    }

                    // Caso o elemento possua um tipo complexo, continuar a busca nos filhos
                    if (element.ElementSchemaType is XmlSchemaComplexType complexType && complexType.Particle is XmlSchemaParticle particle)
                    {
                        TraverseParticle(particle, ref hasProcElement, ref hasNFeElement, ref hasIdeElement);
                    }
                }
            }
        }

        private void TraverseParticle(XmlSchemaParticle particle, ref bool hasProcElement, ref bool hasNFeElement, ref bool hasIdeElement)
        {
            if (particle is XmlSchemaGroupBase groupBase)
            {
                // Se for um grupo (sequence, choice, all), percorrer os itens dentro dele
                TraverseSchema(groupBase.Items, ref hasProcElement, ref hasNFeElement, ref hasIdeElement);
            }
        }

    }

}
