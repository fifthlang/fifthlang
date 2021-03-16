namespace Fifth.Parser.LangProcessingPhases
{
    using System.Collections.Generic;
    using System.Linq;
    using AST;
    using PrimitiveTypes;

    public class TypeAnnotatorVisitor : BaseAstVisitor
    {
        private readonly Stack<IAstNode> currentFunctionDef = new();

        public override void EnterFloatValueExpression(FloatValueExpression ctx)
            => ctx["type"] = PrimitiveFloat.Default;

        public override void EnterFunctionDefinition(FunctionDefinition ctx) => currentFunctionDef.Push(ctx);

        public override void EnterIdentifierExpression(IdentifierExpression identifierExpression)
        {
            if (!identifierExpression.HasAnnotation("type"))
            {
                if (currentFunctionDef != null && currentFunctionDef.Count > 0)
                {
                    // look in params to see if the type is known from there
                    if (currentFunctionDef.Peek() is FunctionDefinition funcDef &&
                        funcDef.ParameterDeclarations != null &&
                        funcDef.ParameterDeclarations.ParameterDeclarations.Any(pd =>
                            pd.ParameterName == identifierExpression.Identifier.Value))
                    {
                        var paramDecl = funcDef.ParameterDeclarations.ParameterDeclarations.First(pd =>
                            pd.ParameterName == identifierExpression.Identifier.Value);
                        identifierExpression.Identifier["type"] = paramDecl.ParameterType;
                        identifierExpression["type"] = paramDecl.ParameterType;
                    }
                }
            }
        }

        public override void EnterIntValueExpression(IntValueExpression ctx)
            => ctx["type"] = PrimitiveInteger.Default;

        public override void EnterStringValueExpression(StringValueExpression ctx)
            => ctx["type"] = PrimitiveString.Default;

        public override void LeaveAssignmentStmt(AssignmentStmt ctx)
        {
            // 2. get the scope the var is declared in (should have been done by now)
            if (!ctx.Expression.HasAnnotation("type"))
            {
                throw new TypeCheckingException("Unable to infer type of expression during assignment");
            }

            // 3. annotate the type of the symbol in the symtab
            // 4. annotate the type of the assignment expression
            ctx["type"] = ctx.Expression["type"];
        }

        public override void LeaveBinaryExpression(BinaryExpression ctx)
        {
            var left = ctx.Left;
            AnnotateIfIdentifierInSymtab(left);
            var right = ctx.Right;
            AnnotateIfIdentifierInSymtab(right);

            if (TypeHelpers.TryInferOperationResultType(ctx.Op, left["type"] as IFifthType,
                right["type"] as IFifthType, out var resulttype))
            {
                ctx["type"] = resulttype;
            }
        }

        public override void LeaveFunctionDefinition(FunctionDefinition ctx) => currentFunctionDef.Pop();

        private void AnnotateIfIdentifierInSymtab(Expression expression)
        {
            if (!expression.HasAnnotation("type"))
            {
                if (expression is IdentifierExpression)
                {
                    var id = expression as IdentifierExpression;
                    var astNode = currentFunctionDef.Peek();
                    if (astNode.TryGetAnnotation<Scope>("symtab", out var scope))
                    {
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
                                    if (symtabEntry.Context.TryGetAnnotation<IFifthType>("type", out var symType))
                                    {
                                        fifthType = symType;
                                    }

                                    break;
                            }

                            if (fifthType != null)
                            {
                                expression["type"] = fifthType;
                            }
                        }
                    }
                }
            }
        }
    }
}
