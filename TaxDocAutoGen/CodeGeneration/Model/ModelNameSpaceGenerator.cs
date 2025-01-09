using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Xml.Schema;
using TaxAI.TaxDocAutoGen.Interfaces;
using TaxAI.TaxDocAutoGen.XsdProcessing;

namespace TaxAI.TaxDocAutoGen.CodeGeneration.Model
{
    public class ModelNameSpaceGenerator : SyntaxGenerator<NamespaceDeclarationSyntax>, INameSpace
    {
        private readonly string NameSpace;
        private readonly ClassMappingInfo ClassMapping;

        public ModelNameSpaceGenerator(string nameSpace, ClassMappingInfo classMapping)
        {
            NameSpace = nameSpace;
            ClassMapping = classMapping;
        }

        public override NamespaceDeclarationSyntax Generate()
        {
            IClass @class = new ModelClassGenerator(ClassMapping);
            var classDeclaration = @class.Generate();
            var nameSpace = SyntaxFactory.ParseName($"{INameSpace.NameSpace}.Domain.Model.{NameSpace}");
            return SyntaxFactory.NamespaceDeclaration(nameSpace)
                .WithMembers([classDeclaration])
                .NormalizeWhitespace();
        }
    }
}
