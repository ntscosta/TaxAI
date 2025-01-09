using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TaxAI.TaxDocAutoGen.Interfaces
{
    public interface IClass : ISyntax<ClassDeclarationSyntax>
    {
    }
}
