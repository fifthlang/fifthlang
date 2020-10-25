namespace Fifth.LangProcessingPhases
{
    using System;
    using Antlr4.Runtime.Misc;
    using Fifth.AST;
    using Fifth.AST.Builders;
    using Fifth.VirtualMachine;
    using static FifthParser;

    public class AstBuilderListener : FifthBaseListener, IBuilder<ProgramDefinition>
    {
        private ProgramBuilder programBuilder = null;
        private FunctionBuilder functionBuilder = null;
        private ExpressionListBuilder expressionListBuilder = null;
        private ParameterDeclarationBuilder parameterDeclarationBuilder = null;
        public AstBuilderListener() { }
        public ProgramDefinition Build() => this.programBuilder.Build();
        public bool IsValid() => throw new System.NotImplementedException();

        #region Fifth
        public override void EnterFifth([NotNull] FifthParser.FifthContext context)
        {
            this.programBuilder = ProgramBuilder.Start();
            base.EnterFifth(context);
        }
        #endregion

        #region Function_declaration
        public override void EnterFunction_declaration([NotNull] FifthParser.Function_declarationContext context)
        {
            this.functionBuilder = FunctionBuilder.Start();
            base.EnterFunction_declaration(context);
        }
        public override void ExitFunction_declaration([NotNull] FifthParser.Function_declarationContext context)
        {
            _ = this.programBuilder.WithFunction(this.functionBuilder.Build());
            this.functionBuilder = null;
            base.ExitFunction_declaration(context);
        }
        #endregion

        #region Function_name
        public override void EnterFunction_name([NotNull] FifthParser.Function_nameContext context)
        {
            _ = this.functionBuilder.WithName(context.GetText());
            base.EnterFunction_name(context);
        }
        #endregion

        #region Parameter_declaration

        public override void EnterParameter_declaration([NotNull] Parameter_declarationContext context)
        {
            this.parameterDeclarationBuilder = ParameterDeclarationBuilder.Start();

            var varType = context.GetChild<Parameter_typeContext>(0);
            var varName = context.children[1];
            var typename = varType.GetText();

            _ = this.parameterDeclarationBuilder
                .WithName(varName.GetText())
                .WithTypeName(typename);
            if(TypeHelpers.IsBuiltinType(typename))
            {
                _ = this.parameterDeclarationBuilder.WithType(TypeHelpers.LookupBuiltinType(varType.GetText()));
            }
            base.EnterParameter_declaration(context);
        }

        #endregion
        #region Function Body
        public override void EnterFunction_body([NotNull] Function_bodyContext context)
        {

            base.EnterFunction_body(context);
        }

        #endregion
    }
}
