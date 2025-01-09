using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TaxAI.TaxDocAutoGen.Interfaces;
using TaxAI.TaxDocAutoGen.XsdProcessing;

namespace TaxAI.TaxDocAutoGen.CodeGeneration.Model
{
    public class ModelPropertyGenerator : IProperty
    {
        private readonly PropertyMappingInfo Property;

        public ModelPropertyGenerator(PropertyMappingInfo property)
        {
            Property = property;
        }

        public PropertyDeclarationSyntax Generate()
        {
            var name = Property.Name;

            return SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("string"), SyntaxFactory.Identifier(name))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                .NormalizeWhitespace();
        }
    }
}
