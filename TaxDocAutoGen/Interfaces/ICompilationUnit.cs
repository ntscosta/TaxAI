using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TaxAI.TaxDocAutoGen.Interfaces
{
    public interface ICompilationUnit : ISyntax<CompilationUnitSyntax>
    {
    }
}
