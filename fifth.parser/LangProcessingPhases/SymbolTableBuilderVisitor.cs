namespace Fifth.Parser.LangProcessingPhases
{
    using System.Collections;
    using System.Collections.Generic;
    using AST;
    using AST.Visitors;
    using Symbols;
    using TypeSystem;

    public class SymbolTableBuilderVisitor : BaseAstVisitor
    {
        public Stack<TypeId> PatternMatchers { get; set; } = new Stack<TypeId>();
        public bool IsInPatternMatcher => PatternMatchers.Count > 0;
        public TypeId PatternMatcherTid => IsInPatternMatcher ? PatternMatchers.Peek() : default;
        public override void EnterPropertyDefinition(PropertyDefinition ctx)
        {
            var enclosingScope = ctx.ParentNode.NearestScope();
            enclosingScope.Declare(ctx.Name, SymbolKind.PropertyDefinition, ctx);
        }

        public override void EnterDestructuringParamDecl(DestructuringParamDecl ctx)
        {
            var enclosingScope = ctx.NearestScope();
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(ctx.TypeName, out var typeType))
            {
                PatternMatchers.Push(typeType.TypeId);
            }
            enclosingScope.Declare(ctx.ParameterName.Value, SymbolKind.PatternMatchingFormalParameter, ctx);
        }

        public override void EnterPropertyBinding(PropertyBinding ctx)
        {
            // find, from SymTab of the type of the current pattern match, the property that the binding relates to
            // parent node should be the DestructuringParamDecl, that will link to the type defining the properties
            var enclosingScope = ctx.NearestScope();
            if (ctx.ParentNode is DestructuringParamDecl dpd)
            {
                var baseTypeForPatternMatch = PatternMatcherTid.Lookup() as UserDefinedType;
                if (baseTypeForPatternMatch.Definition.TryResolve(ctx.BoundPropertyName, out var boundPropSte))
                {
                    ctx.BoundProperty = boundPropSte.Context as PropertyDefinition;
                    // now we know what the binding relates to, we can create an ste in the scope, to record the relationship
                    enclosingScope.Declare(ctx.BoundVariableName, SymbolKind.PatternBinding, ctx);
                }
            }
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
                            enclosingScope.Declare(vr.Name, SymbolKind.PatternBinding, propEntry.Context);
                        }

                    }
                }

            }
        }

        public override void LeaveDestructuringParamDecl(DestructuringParamDecl ctx)
        {
            PatternMatchers.Pop();
        }

        public override void EnterBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx)
        {
            var enclosingScope = ctx.ParentNode.NearestScope();
            enclosingScope.Declare(ctx.Name, SymbolKind.BuiltinFunctionDeclaration, ctx);
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(ctx.Typename, out var t))
            {
                ctx.ReturnType = t.TypeId;
            }            
        }

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
