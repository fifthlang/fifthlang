﻿using fifth.VirtualMachine;

namespace fifth.Parser.AST
{
    public class ParameterDeclaration
    {
        public string ParameterName { get; set; }
        public IFifthType ParameterType { get; set; }
    }
}