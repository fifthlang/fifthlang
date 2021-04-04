namespace Fifth.Parser.LangProcessingPhases
{
    using System;
    using System.Collections.Generic;
    using AST;
    using AST.Visitors;
    using PrimitiveTypes;
    using Symbols;
    using TypeSystem;

    public class TypeAnnotatorVisitor : BaseAstVisitor
    {
        private readonly Stack<IAstNode> currentFunctionDef = new();

        public override void EnterFloatValueExpression(FloatValueExpression ctx)
            => ctx.TypeId = PrimitiveFloat.Default.TypeId;

        public override void EnterFuncCallExpression(FuncCallExpression ctx)
        {
            var scope = ctx.NearestScope();
            var t = TypeChecker.Infer(scope, ctx);
            ctx.TypeId = t;
        }

        public override void EnterFunctionDefinition(FunctionDefinition ctx) => currentFunctionDef.Push(ctx);

        public override void EnterIdentifierExpression(IdentifierExpression ctx)
        {
            var scope = ctx.NearestScope();
            var type = TypeChecker.Infer(scope, ctx);
            if (type != null)
            {
                ctx.TypeId = type;
                ctx.Identifier.TypeId = type;
            }
        }

        public override void EnterIntValueExpression(IntValueExpression ctx)
            => ctx.TypeId = PrimitiveInteger.Default.TypeId;

        public override void EnterParameterDeclaration(ParameterDeclaration ctx)
        {
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(ctx.TypeName, out var paramType))
            {
                ctx.TypeId = paramType.TypeId;
            }
        }

        public override void EnterStringValueExpression(StringValueExpression ctx)
            => ctx.TypeId = PrimitiveString.Default.TypeId;

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
                        fifthType = vd.TypeId;
                        break;

                    case SymbolKind.FunctionDeclaration:
                    case SymbolKind.FormalParameter:
                    case SymbolKind.VariableReference:
                    case SymbolKind.FunctionReference:
                    default:
                        if (symtabEntry.Context is TypedAstNode tan)
                        {
                            fifthType = tan.TypeId;
                        }

                        break;
                }

                if (fifthType != null)
                {
                    ctx.TypeId = fifthType;
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
            var rhsType = ctx?.Expression?.TypeId ??
                          throw new TypeCheckingException("Unable to infer type of expression during assignment");
            var lhsType = ctx.VariableRef?.TypeId ??
                          throw new TypeCheckingException("Unable to infer type of expression during assignment");

            // 3. annotate the type of the symbol in the symtab
            // 4. annotate the type of the assignment expression
            ctx.TypeId = lhsType;
        }

        public override void LeaveBinaryExpression(BinaryExpression ctx)
        {
            var scope = ctx.NearestScope();
            ctx.TypeId = TypeChecker.Infer(scope, ctx);
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
                                fifthType = vd.TypeId;
                                break;

                            case SymbolKind.FunctionDeclaration:
                            case SymbolKind.FormalParameter:
                            case SymbolKind.VariableReference:
                            case SymbolKind.FunctionReference:
                            default:
                                if (symtabEntry.Context is TypedAstNode tan)
                                {
                                    fifthType = tan.TypeId;
                                }

                                break;
                        }

                        if (fifthType != null)
                        {
                            id.TypeId = fifthType;
                        }
                    }

                    break;
                case ITypedAstNode tn:
                    if (tn.TypeId != null)
                    {
                    }

                    break;
            }
        }
    }
}
