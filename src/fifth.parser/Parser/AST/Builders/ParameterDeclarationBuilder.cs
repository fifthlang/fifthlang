﻿using Fifth.VirtualMachine;
using System;
using System.Collections.Generic;

namespace Fifth.AST.Builders
{
    public class ParameterDeclarationBuilder : IBuilder<ParameterDeclaration>
    {
        public List<ParameterDeclaration> ParameterDeclarations { get; private set; }

        public string ParameterName { get; private set; }
        public IFifthType ParameterType { get; private set; }
        public string TypeName { get; private set; }

        public static ParameterDeclarationBuilder Start()
        {
            return new ParameterDeclarationBuilder();
        }

        public ParameterDeclaration Build()
        {
            return new ParameterDeclaration
            {
                ParameterType = this.ParameterType,
                ParameterName = this.ParameterName
            };
        }

        public ParameterDeclarationBuilder WithName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        public ParameterDeclarationBuilder WithType(IFifthType type)
        {
            this.ParameterType = type;
            return this;
        }

        public ParameterDeclarationBuilder WithTypeName(string name)
        {
            this.TypeName = name;
            return this;
        }

        public bool IsValid() => throw new System.NotImplementedException();
    }
}
