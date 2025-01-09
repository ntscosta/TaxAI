using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TaxAI.TaxDocAutoGen.Interfaces
{
    public interface INameSpace : ISyntax<NamespaceDeclarationSyntax>
    {
        const string NameSpace = "TaxAI";
    }
}
