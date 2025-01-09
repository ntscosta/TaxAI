using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Xml.Schema;
using TaxAI.TaxDocAutoGen.Interfaces;
using TaxAI.TaxDocAutoGen.XsdProcessing;

namespace TaxAI.TaxDocAutoGen.CodeGeneration.Model
{
    public class ModelClassGenerator : SyntaxGenerator<ClassDeclarationSyntax>, IClass
    {
        private ClassMappingInfo ClassMapping;
        public ModelClassGenerator(ClassMappingInfo classMapping)
        {
            ClassMapping = classMapping;
        }

        public override ClassDeclarationSyntax Generate()
        {
            var fk = Array.Empty<PropertyDeclarationSyntax>();
            if (ClassMapping.ForeignKey != null)
            {
                ClassMapping.ForeignKey.Name = ClassMapping.ForeignKey.Name + "Id";
                var mp = new ModelPropertyGenerator(ClassMapping.ForeignKey);
                fk = [mp.Generate()];
            }

            var propertiesSyntax = ClassMapping.propertyMappingInfos.Select(x =>
            {
                IProperty property = new ModelPropertyGenerator(x);
                return property.Generate();
            }).ToArray();

            return SyntaxFactory.ClassDeclaration(ClassMapping.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("Entity")))
                .AddMembers(fk)
                .AddMembers(propertiesSyntax)
                .NormalizeWhitespace();
        }
    }
}
