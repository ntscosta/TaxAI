using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxAI.TaxDocAutoGen.XsdProcessing
{
    public class TypeMappingInfo
    {
        public string CSharpType { get; }
        public string SqlType { get; }
        public string? SqlSize { get; }
        public string? Validation { get; }
        public string? Description { get; }
        public bool IsEnum { get; }
        public bool IsOptional { get; }

        public TypeMappingInfo(
            string cSharpType,
            string sqlType,
            string sqlSize = null,
            string? validation = null,
            string? description = null,
            bool isEnum = false,
            bool isOptional = false)
        {
            CSharpType = cSharpType;
            SqlType = sqlType;
            SqlSize = sqlSize;
            Validation = validation;
            Description = description;
            IsEnum = isEnum;
            IsOptional = isOptional;
        }

        private readonly Dictionary<string, TypeMappingInfo> TypeMappingInfos = new()
        {
            {
                "TVerNFe",
                new(
                    cSharpType: "string",
                    sqlType: "CHAR",
                    sqlSize: "4",
                    validation: @"4\.00")
            }
        };
    }
}
