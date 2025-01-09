using Microsoft.CodeAnalysis.CSharp;
using TaxAI.TaxDocAutoGen.Interfaces;

namespace TaxAI.TaxDocAutoGen.CodeGeneration
{
    public abstract class SyntaxGenerator<T> : ISyntax<T> where T : CSharpSyntaxNode
    {
        public abstract T Generate();
    }
}
