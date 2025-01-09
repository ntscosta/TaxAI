using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TaxAI.TaxDocAutoGen.Interfaces;

namespace TaxAI.TaxDocAutoGen.CodeGeneration.Model
{
    public class ModelUsingGenerator : SyntaxGenerator<UsingDirectiveSyntax>, IUsing
    {
        public override UsingDirectiveSyntax Generate()
        {
            NameSyntax nameSyntax = SyntaxFactory.ParseName("");
            return SyntaxFactory.UsingDirective(nameSyntax).NormalizeWhitespace();
        }
    }
}
