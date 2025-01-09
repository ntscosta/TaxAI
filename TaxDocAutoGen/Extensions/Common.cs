using System.Xml;
using System.Xml.Schema;

namespace TaxAI.TaxDocAutoGen.Extensions
{
    public static class Common
    {
        public static string ToPascalCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            // Separa as palavras por espaços, hífens ou underscores
            string[] words = str.Split(new[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);

            // Capitaliza a primeira letra de cada palavra e junta todas as palavras
            string result = string.Concat(words.Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));

            return result;
        }
        public static string ToPascalCasePreservingUpper(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            char[] chars = str.ToCharArray();
            bool newWord = true;

            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsLetter(chars[i]))
                {
                    if (newWord)
                    {
                        chars[i] = char.ToUpper(chars[i]);
                        newWord = false;
                    }
                    else
                    {
                        chars[i] = char.ToLower(chars[i]);
                    }
                }
                else
                {
                    newWord = true; // Iniciamos uma nova palavra após encontrar um não-caractere
                }
            }

            return new string(chars);
        }

        public static string ItensToString(this XmlSchemaAnnotation annotation)
        {
            return string.Join("\n", annotation?.Items
                    .OfType<XmlSchemaDocumentation>()
                    .Select(d => d.Markup != null ? string.Join("", d.Markup.OfType<XmlNode>().Select(n => n.InnerText)) : "") ?? [""]);
        }
    }
}
