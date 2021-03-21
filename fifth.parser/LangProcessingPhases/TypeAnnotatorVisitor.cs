namespace Fifth.Parser.LangProcessingPhases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AST;
    using PrimitiveTypes;
    using Symbols;

    public class TypeAnnotatorVisitor : BaseAstVisitor
    {
        private readonly Stack<IAstNode> currentFunctionDef = new();

        public override void EnterFloatValueExpression(FloatValueExpression ctx)
            => ctx.FifthType = PrimitiveFloat.Default;

        public override void EnterFunctionDefinition(FunctionDefinition ctx) => currentFunctionDef.Push(ctx);

        public override void EnterIdentifierExpression(IdentifierExpression ctx)
        {
            var scope = ctx.NearestScope();
            if (scope.TryResolve(ctx.Identifier.Value, out var symtabEntry))
            {
                var originTyped = symtabEntry.Context as ITypedAstNode;
                ctx.FifthType = originTyped.FifthType;
                ctx.Identifier.FifthType = originTyped.FifthType;
                return;
            }

            if (currentFunctionDef != null && currentFunctionDef.Count > 0)
            {
                // look in params to see if the type is known from there
                if (currentFunctionDef.Peek() is FunctionDefinition funcDef &&
                    funcDef.ParameterDeclarations != null &&
                    funcDef.ParameterDeclarations.ParameterDeclarations.Any(pd =>
                        pd.ParameterName == ctx.Identifier.Value))
                {
                    var paramDecl = funcDef.ParameterDeclarations.ParameterDeclarations.First(pd =>
                        pd.ParameterName == ctx.Identifier.Value);
                    ctx.Identifier.FifthType = paramDecl.ParameterType;
                    ctx.FifthType = paramDecl.ParameterType;
                }
            }
        }

        public override void EnterVariableReference(VariableReference ctx)
        {
            var scope = ctx.NearestScope();
            if(scope.TryResolve(ctx.Name, out var symtabEntry))
            {
                IFifthType fifthType = null;
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
        }

        public override void EnterIntValueExpression(IntValueExpression ctx)
            => ctx.FifthType = PrimitiveInteger.Default;

        public override void EnterStringValueExpression(StringValueExpression ctx)
            => ctx.FifthType = PrimitiveString.Default;

        public override void LeaveAssignmentStmt(AssignmentStmt ctx)
        { 
            _ = ctx ?? throw new ArgumentNullException(nameof(ctx));
            var rhsType = ctx?.Expression?.FifthType ?? PrimitiveVoid.Default;
            var lhsType = ctx.VariableRef?.FifthType ?? PrimitiveVoid.Default;

            if (lhsType == PrimitiveVoid.Default ||rhsType == PrimitiveVoid.Default)
            {
                throw new TypeCheckingException("Unable to infer type of expression during assignment");
            }
                
            // 2. get the scope the var is declared in (should have been done by now)
            if (lhsType != rhsType)
            {
                throw new TypeCheckingException("Type mismatch in assignment statement");
            }

            // 3. annotate the type of the symbol in the symtab
            // 4. annotate the type of the assignment expression
            ctx.FifthType = lhsType;
        }

        public override void LeaveBinaryExpression(BinaryExpression ctx)
        {
            var left = ctx.Left;
            AnnotateIfIdentifierInSymtab(left);
            var right = ctx.Right;
            AnnotateIfIdentifierInSymtab(right);

            if (TypeHelpers.TryInferOperationResultType(ctx.Op, left.FifthType as IFifthType,
                right.FifthType as IFifthType, out var resulttype))
            {
                ctx.FifthType = resulttype;
            }
        }

        public override void LeaveFunctionDefinition(FunctionDefinition ctx) => currentFunctionDef.Pop();

        private void AnnotateIfIdentifierInSymtab(Expression expression)
        {
            if (expression is ITypedAstNode tn)
            {
                if (tn.FifthType != null && tn.FifthType != PrimitiveVoid.Default)
                {
                    return;
                }
            }
            if (expression is IdentifierExpression id)
            {
                var scope = id.NearestScope();
                if (scope.SymbolTable.TryGetValue(id.Identifier.Value, out var symtabEntry))
                {
                    IFifthType fifthType = null;
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
            }
        }
    }
}
