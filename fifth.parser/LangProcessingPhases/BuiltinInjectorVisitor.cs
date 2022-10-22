namespace Fifth.LangProcessingPhases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AST;
    using AST.Builders;
    using AST.Visitors;
    using fifth.metamodel.metadata;
    using fifthlang.system;
    using PrimitiveTypes;
    using TypeSystem;
    using TypeSystem.PrimitiveTypes;
    using Expression = fifth.metamodel.metadata.il.Expression;

    /// <summary>
    /// A visitor that injects builtin things into the AST.
    /// </summary>
    public class BuiltinInjectorVisitor : BaseAstVisitor
    {
        public override void EnterFifthProgram(FifthProgram ctx)
        {
            WrapType(typeof(GlobalBuiltinFunctions));


            // ctx.Functions.Add(new BuiltinFunctionDefinition("print", "void", ("format", "string"), ("value", "string")));
            // ctx.Functions.Add(new BuiltinFunctionDefinition("print", "void", ("value", "string")));
            // ctx.Functions.Add(new BuiltinFunctionDefinition("write", "void", ("value", "string")));
        }

        private void WrapType(Type t)
        {
            if (TypeRegistry.DefaultRegistry.TryLookupType(t, out var itype))
            {
                return;
            }

            var c = ClassDefinitionBuilder.CreateClassDefinition()
                                          .WithName(t.Name)
                                          .WithFields(t.GetFields().Select(fi => WrapField(fi)).ToList())
                                          .WithProperties(t.GetProperties().Select(pi => WrapProperty(pi)).ToList())
                                          .WithFunctions(t.GetMethods(BindingFlags.Public|BindingFlags.Static|BindingFlags.Instance)
                                                          .Select(mi => WrapMethod(mi)).ToList())
                                          .Build();
            var userDefinedType = new UserDefinedType(c);
            TypeRegistry.DefaultRegistry.RegisterType(userDefinedType);
        }

        public FieldDefinition WrapField(FieldInfo fi)
        {
            if (TypeRegistry.DefaultRegistry.TryLookupType(fi.FieldType, out var ft))
            {
                return FieldDefinitionBuilder.CreateFieldDefinition()
                                             .WithName(fi.Name)
                                             .WithTypeName(fi.FieldType.Name)
                                             .Build();
            }

            return default;
        }

        public PropertyDefinition WrapProperty(PropertyInfo pi)
        {
            if (TypeRegistry.DefaultRegistry.TryLookupType(pi.PropertyType, out var ft))
            {
                return PropertyDefinitionBuilder.CreatePropertyDefinition()
                                                .WithName(pi.Name)
                                                .WithTypeName(pi.PropertyType.Name)
                                                .Build();
            }

            return default;
        }

        private IFunctionDefinition WrapMethod(MethodInfo mi)
        {
            if (!TypeRegistry.DefaultRegistry.TryLookupType(mi.ReturnType, out var rettype))
            {
                return default;
            }

            var builder = FunctionDefinitionBuilder.CreateFunctionDefinition()
                                                   .WithName(mi.Name)
                                                   .WithTypename(rettype.Name)
                                                   .WithFunctionKind(FunctionKind.Normal)
                                                   .WithIsEntryPoint(false);

            var pdlb = ParameterDeclarationListBuilder.CreateParameterDeclarationList();
            foreach (var pi in mi.GetParameters())
            {
                if (TypeRegistry.DefaultRegistry.TryLookupType(pi.ParameterType, out var paramtype))
                {
                    pdlb.AddingItemToParameterDeclarations(
                        ParameterDeclarationBuilder.CreateParameterDeclaration()
                                                   .WithParameterName(new Identifier(pi.Name))
                                                   .WithTypeName(paramtype.Name)
                                                   .Build()
                    );
                }
            }

            builder.WithParameterDeclarations(pdlb.Build());

            var argsAsExpressions = mi.GetParameters().Select(pi => (AST.Expression)VariableReferenceBuilder
                .CreateVariableReference()
                .WithName(pi.Name)
                .Build());

            builder.WithBody(BlockBuilder.CreateBlock()
                                         .AddingItemToStatements(
                                             ExpressionStatementBuilder.CreateExpressionStatement()
                                                                       .WithExpression(
                                                                           FuncCallExpressionBuilder
                                                                               .CreateFuncCallExpression()
                                                                               .WithName(mi.Name)
                                                                               .WithActualParameters(
                                                                                   ExpressionListBuilder
                                                                                       .CreateExpressionList()
                                                                                       .WithExpressions(
                                                                                           argsAsExpressions
                                                                                               .ToList())
                                                                                       .Build() // params expression list
                                                                               )
                                                                               .Build() // func call exp
                                                                       ).Build() // expression statement
                                         )
                                         .Build() // block
            );
            return builder.Build();
        }
    }
}
