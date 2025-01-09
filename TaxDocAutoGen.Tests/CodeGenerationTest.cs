using TaxAI.TaxDocAutoGen.CodeGeneration.Model;
using TaxAI.TaxDocAutoGen.Extensions;
using TaxAI.TaxDocAutoGen.XsdProcessing;

namespace TaxDocAutoGen.Tests
{
    public class CodeGenerationTest
    {
        [Fact]
        public void ModelGeneratorXSDTest()
        {
            // Arrange
            var sr = new SchemaReader();

            var processor = new XsdProcessor(sr);

            // Arrange
            string basePath = Path.Combine(@"D:\repo\AuditCommunity\Documentation\LeiauteEFD\Schemas\NFe\PL_009k_NT2023_001_v120");
            string nfeSchemaPath = Path.Combine(basePath, "procNFe_v4.00.xsd");

            processor.ProcessXsd(nfeSchemaPath);

            foreach (var element in processor.ClassElements)
            {
                var fk = element.ForeignKey;

                var modelGenerator = new ModelUnitGenerator("NFe", element);

                var code = modelGenerator.Generate();

                Console.WriteLine(code);
                
            }
        }
    }
}
