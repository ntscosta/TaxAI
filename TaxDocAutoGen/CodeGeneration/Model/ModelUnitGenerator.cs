using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Xml.Schema;
using TaxAI.TaxDocAutoGen.Interfaces;
using TaxAI.TaxDocAutoGen.XsdProcessing;

namespace TaxAI.TaxDocAutoGen.CodeGeneration.Model
{
    public class ModelUnitGenerator : SyntaxGenerator<CompilationUnitSyntax>
    {
        private readonly string NameSpace;
        private readonly ClassMappingInfo ClassMapping;

        public ModelUnitGenerator(string nameSpace, ClassMappingInfo classMapping)
        {
            NameSpace = nameSpace;
            ClassMapping = classMapping;
        }

        public override CompilationUnitSyntax Generate()
        {
            IUsing @using = new ModelUsingGenerator();
            INameSpace nameSpace = new ModelNameSpaceGenerator(NameSpace, ClassMapping);

            var usingDirective = @using.Generate();
            var nameSpaceDeclaration = nameSpace.Generate();

            return SyntaxFactory.CompilationUnit()
                .WithUsings([usingDirective])
                .AddMembers(nameSpaceDeclaration)
                .NormalizeWhitespace();
        }
    }
}

