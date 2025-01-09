using Microsoft.CodeAnalysis.CSharp;

namespace TaxAI.TaxDocAutoGen.Interfaces
{
    public interface ISyntax<out T> where T : CSharpSyntaxNode
    {
        T Generate();
    }
}
