namespace Fifth.Parser.LangProcessingPhases
{
    using System;
    using System.Collections.Generic;
    using AST;
    using PrimitiveTypes;
    using Symbols;
    using TypeSystem;

    public class TypeAnnotatorVisitor : BaseAstVisitor
    {
        private readonly Stack<IAstNode> currentFunctionDef = new();

        public override void EnterFloatValueExpression(FloatValueExpression ctx)
            => ctx.FifthType = PrimitiveFloat.Default.TypeId;

        public override void EnterFuncCallExpression(FuncCallExpression ctx)
        {
            var scope = ctx.NearestScope();
            var t = TypeChecker.Infer(scope, ctx);
            ctx.FifthType = t;
        }

        public override void EnterFunctionDefinition(FunctionDefinition ctx) => currentFunctionDef.Push(ctx);

        public override void EnterIdentifierExpression(IdentifierExpression ctx)
        {
            var scope = ctx.NearestScope();
            var type = TypeChecker.Infer(scope, ctx);
            if (type != null)
            {
                ctx.FifthType = type;
                ctx.Identifier.FifthType = type;
            }
        }

        public override void EnterIntValueExpression(IntValueExpression ctx)
            => ctx.FifthType = PrimitiveInteger.Default.TypeId;

        public override void EnterParameterDeclaration(ParameterDeclaration ctx)
        {
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(ctx.TypeName, out var paramType))
            {
                ctx.FifthType = paramType.TypeId;
            }
        }

        public override void EnterStringValueExpression(StringValueExpression ctx)
            => ctx.FifthType = PrimitiveString.Default.TypeId;

        public override void EnterVariableReference(VariableReference ctx)
        {
            var scope = ctx.NearestScope();
            if (scope.TryResolve(ctx.Name, out var symtabEntry))
            {
                TypeId fifthType = null;
                switch (symtabEntry.SymbolKind)
                {
                    case SymbolKind.VariableDeclaration:
                        var vd = symtabEntry.Context as VariableDeclarationStatement;
                        fifthType = vd.FifthType;
                        break;

                    case SymbolKind.FunctionDeclaration:
                    case SymbolKind.FormalParameter:
                    case SymbolKind.VariableReference:
                    case SymbolKind.FunctionReference:
                    default:
                        if (symtabEntry.Context is TypedAstNode tan)
                        {
                            fifthType = tan.FifthType;
                        }

                        break;
                }

                if (fifthType != null)
                {
                    ctx.FifthType = fifthType;
                }
            }
            else
            {
                // maybe it's to discard
                if (ctx.Name == "__discard__")
                {
                }
            }
        }

        public override void LeaveAssignmentStmt(AssignmentStmt ctx)
        {
            _ = ctx ?? throw new ArgumentNullException(nameof(ctx));
            var rhsType = ctx?.Expression?.FifthType ??
                          throw new TypeCheckingException("Unable to infer type of expression during assignment");
            var lhsType = ctx.VariableRef?.FifthType ??
                          throw new TypeCheckingException("Unable to infer type of expression during assignment");

            // 3. annotate the type of the symbol in the symtab
            // 4. annotate the type of the assignment expression
            ctx.FifthType = lhsType;
        }

        public override void LeaveBinaryExpression(BinaryExpression ctx)
        {
            var scope = ctx.NearestScope();
            ctx.FifthType = TypeChecker.Infer(scope, ctx);
        }

        public override void LeaveFunctionDefinition(FunctionDefinition ctx) => currentFunctionDef.Pop();

        private void AnnotateIfIdentifierInSymtab(Expression expression)
        {
            switch (expression)
            {
                case IdentifierExpression id:
                    var scope = id.NearestScope();
                    if (scope.SymbolTable.TryGetValue(id.Identifier.Value, out var symtabEntry))
                    {
                        TypeId fifthType = null;
                        switch (symtabEntry.SymbolKind)
                        {
                            case SymbolKind.VariableDeclaration:
                                var vd = symtabEntry.Context as VariableDeclarationStatement;
                                fifthType = vd.FifthType;
                                break;

                            case SymbolKind.FunctionDeclaration:
                            case SymbolKind.FormalParameter:
                            case SymbolKind.VariableReference:
                            case SymbolKind.FunctionReference:
                            default:
                                if (symtabEntry.Context is TypedAstNode tan)
                                {
                                    fifthType = tan.FifthType;
                                }

                                break;
                        }

                        if (fifthType != null)
                        {
                            id.FifthType = fifthType;
                        }
                    }

                    break;
                case ITypedAstNode tn:
                    if (tn.FifthType != null)
                    {
                    }

                    break;
            }
        }
    }
}
