namespace Fifth.Parser.LangProcessingPhases
{
    using AST;
    using AST.Visitors;
    using Symbols;
    using TypeSystem;

    public class SymbolTableBuilderVisitor : BaseAstVisitor
    {
        public bool IsInPatternMatcher { get; set; }
        public TypeId PatternMatcherTid { get; set; }
        public override void EnterPropertyDefinition(PropertyDefinition ctx)
        {
            var enclosingScope = ctx.ParentNode.NearestScope();
            enclosingScope.Declare(ctx.Name, SymbolKind.PropertyDefinition, ctx);
        }

        public override void EnterTypeInitParamDecl(TypeInitParamDecl ctx)
        {
            IsInPatternMatcher = true;
            var enclosingScope = ctx.NearestScope();
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(ctx.Pattern.TypeName, out var pmType))
            {
                PatternMatcherTid = pmType.TypeId;
            }
            enclosingScope.Declare(ctx.Name, SymbolKind.PatternMatchingFormalParameter, ctx);
        }

        public override void EnterTypePropertyInit(TypePropertyInit ctx)
        {
            var enclosingScope = ctx.NearestScope();
            if (IsInPatternMatcher && ctx.Value is VariableReference vr)
            {
                // then any unbound variable name on the RHS might be a declaration in disguise...
                if (PatternMatcherTid.Lookup() is UserDefinedType udt)
                {
                    if (udt.Definition.TryResolve(ctx.Name, out var propEntry))
                    {
                        if (!enclosingScope.TryResolve(vr.Name, out var varSte))
                        {
                            // if we get to here then we have an unbound variable and we know the details of the property it's type etc is based on
                            enclosingScope.Declare(ctx.Name, SymbolKind.PatternBinding, propEntry.Context);
                        }

                    }
                }

            }
        }

        public override void LeaveTypeInitParamDecl(TypeInitParamDecl ctx)
        {
            IsInPatternMatcher = false;
            base.LeaveTypeInitParamDecl(ctx);
        }

        public override void LeaveTypePropertyInit(TypePropertyInit ctx)
            => base.LeaveTypePropertyInit(ctx);

        public override void EnterFunctionDefinition(FunctionDefinition ctx)
        {
            // Declare(ctx.Name, SymbolKind.FunctionDeclaration, ctx);
            var enclosingScope = ctx.ParentNode.NearestScope();
            enclosingScope.Declare(ctx.Name, SymbolKind.FunctionDeclaration, ctx);
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(ctx.Typename, out var t))
            {
                ctx.ReturnType = t.TypeId;
            }
        }

        public override void EnterParameterDeclaration(ParameterDeclaration ctx)
            => Declare(ctx.ParameterName.Value, SymbolKind.FormalParameter, ctx);

        public override void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx)
            => Declare(ctx.Name.Value, SymbolKind.VariableDeclaration, ctx);

        private void Declare<T>(string name, SymbolKind kind, T ctx, params (string, object)[] properties)
            where T : AstNode
        {
            var enclosingScope = ctx.NearestScope();
            enclosingScope.Declare(name, kind, ctx, properties);
        }
    }
}
