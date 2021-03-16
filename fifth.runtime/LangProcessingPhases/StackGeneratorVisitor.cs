namespace Fifth.Runtime.LangProcessingPhases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AST;
    using Parser.LangProcessingPhases;
    using ASTFunctionDefinition = AST.FunctionDefinition;
    using RuntimeFunctionDefinition = FunctionDefinition;

    public class StackGeneratorVisitor : BaseAstVisitor
    {
        public IActivationFrame Frame { get; set; }
        public IStackEmitter Emitter { get; set; }

        public override void EnterFifthProgram(FifthProgram ctx)
        {
            var fpe = new FifthProgramEmitter(ctx);
            fpe.Emit(Emitter, Frame);
        }
    }

    public interface ISpecialFormEmitter
    {
        void Emit(IStackEmitter emitter, IActivationFrame frame);
    }

    public class FifthProgramEmitter : ISpecialFormEmitter
    {
        private readonly FifthProgram programAst;

        public FifthProgramEmitter(FifthProgram ast) => programAst = ast;

        public void Emit(IStackEmitter emitter, IActivationFrame frame)
        {
            foreach (var astAlias in programAst.Aliases)
            {
                var aliasEmitter = new AliasEmitter(astAlias);
                aliasEmitter.Emit(emitter, frame);
            }

            foreach (var astFunction in programAst.Functions)
            {
                var funEmitter = new FunctionDefinitionEmitter(astFunction);
                funEmitter.Emit(emitter, frame);
            }
        }
    }

    public class FunctionDefinitionEmitter : ISpecialFormEmitter
    {
        private readonly ASTFunctionDefinition astFunction;
        private readonly RuntimeFunctionDefinition runtimeFunction;

        public FunctionDefinitionEmitter(FunctionDefinition astFunction)
        {
            this.astFunction = astFunction;
            runtimeFunction = new RuntimeFunctionDefinition {Name = astFunction.Name};
            if (this.astFunction.ParameterDeclarations != null &&
                this.astFunction.ParameterDeclarations.ParameterDeclarations.Any())
            {
                runtimeFunction.Arguments.AddRange(this.astFunction.ParameterDeclarations.ParameterDeclarations.Select(
                    (p, i) =>
                        new FunctionArgument {ArgOrdinal = i, Name = p.ParameterName, Type = p.ParameterType}));
            }
        }

        public void Emit(IStackEmitter emitter, IActivationFrame frame)
        {
            var ele = new ExpressionListStackEmitter(astFunction.Body);
            ele.Emit(emitter, runtimeFunction);
            frame.Environment.AddFunctionDefinition(runtimeFunction);
        }
    }

    public class AliasEmitter : ISpecialFormEmitter
    {
        public AliasEmitter(AliasDeclaration astAlias) => throw new NotImplementedException();

        public void Emit(IStackEmitter emitter, IActivationFrame frame)
        {
        }
    }

    public class ExpressionStackEmitter : ISpecialFormEmitter
    {
        private readonly Expression expression;

        public ExpressionStackEmitter(Expression expression) => this.expression = expression;

        public void Emit(IStackEmitter emitter, IActivationFrame frame)
        {
            switch (expression)
            {
                case FuncCallExpression fce:
                    EmitFuncCallExpression(fce, emitter, frame);
                    break;
                case IdentifierExpression ie:
                    EmitIdentifierExpression(ie, emitter, frame);
                    break;
                case BinaryExpression be:
                    EmitBinaryExpression(be, emitter, frame);
                    break;
                case BooleanExpression boole:
                    EmitBooleanExpression(boole, emitter, frame);
                    break;
                case IntValueExpression ie:
                    EmitIntValueExpression(ie, emitter, frame);
                    break;
                case FloatValueExpression fe:
                    EmitFloatValueExpression(fe, emitter, frame);
                    break;
                case StringValueExpression se:
                    EmitStringValueExpression(se, emitter, frame);
                    break;
                case VariableDeclarationStatement vde:
                    EmitVariableDeclarationExpression(vde, emitter, frame);
                    break;
            }
        }

        public void EmitBinaryExpression(BinaryExpression be, IStackEmitter emitter, IActivationFrame frame)
        {
            var lhsEmitter = new ExpressionStackEmitter(be.Left);
            var rhsEmitter = new ExpressionStackEmitter(be.Right);
            lhsEmitter.Emit(emitter, frame);
            rhsEmitter.Emit(emitter, frame);
            emitter.Operator(frame.Stack, be);
        }

        public void EmitBooleanExpression(BooleanExpression be, IStackEmitter emitter, IActivationFrame frame) =>
            emitter.Value(frame.Stack, be.Value);

        public void EmitFloatValueExpression(FloatValueExpression fe, IStackEmitter emitter, IActivationFrame frame) =>
            emitter.Value(frame.Stack, fe.Value);

        public void EmitFuncCallExpression(FuncCallExpression fce, IStackEmitter emitter, IActivationFrame frame)
        {
            if (fce.ActualParameters.Expressions.Any())
            {
                var ele = new ExpressionListStackEmitter(fce.ActualParameters);
                ele.Emit(emitter, frame);
            }

            emitter.Value(frame.Stack, fce.Name);
            emitter.MetaFunction(frame.Stack, MetaFunction.CallFunction);
        }

        public void EmitIdentifierExpression(IdentifierExpression ie, IStackEmitter emitter, IActivationFrame frame)
        {
            emitter.Value(frame.Stack, ie.Identifier.Value);
            emitter.MetaFunction(frame.Stack, MetaFunction.DereferenceVariable);
        }

        public void EmitIntValueExpression(IntValueExpression ie, IStackEmitter emitter, IActivationFrame frame) =>
            emitter.Value(frame.Stack, ie.Value);

        public void EmitStringValueExpression(StringValueExpression se, IStackEmitter emitter,
            IActivationFrame frame) =>
            emitter.Value(frame.Stack, se.Value);

        public void EmitVariableDeclarationExpression(VariableDeclarationStatement vde, IStackEmitter emitter,
            IActivationFrame frame)
        {
            // see /docs/semantic/metafunctions/Metafunction.DeclareVariable.md for semantics

            if (vde.Expression == null)
            {
                // just a bare decl: `int x`
                // format:     [typename, id, \DeclareVariable] => []
                emitter.Value(frame.Stack, vde.TypeName);
                emitter.Value(frame.Stack, vde.Name.Value);
                emitter.MetaFunction(frame.Stack, MetaFunction.DeclareVariable);
            }
            else
            {
                // a compound decl: `int x = expression`
                // format:     [<expression>, id, \Assign, typename, id, \DeclareVariable] => []

                // assign part
                new ExpressionStackEmitter(vde.Expression).Emit(emitter, frame);
                emitter.Value(frame.Stack, vde.Name.Value);
                emitter.MetaFunction(frame.Stack, MetaFunction.BindVariable);
                // decl part
                emitter.Value(frame.Stack, vde.TypeName);
                emitter.Value(frame.Stack, vde.Name.Value);
                emitter.MetaFunction(frame.Stack, MetaFunction.DeclareVariable);
            }
        }
    }

    public class ExpressionListStackEmitter : ISpecialFormEmitter
    {
        private readonly IEnumerable<Expression> expressionList;

        public ExpressionListStackEmitter(ExpressionList el) => expressionList = el.Expressions;

        public void Emit(IStackEmitter emitter, IActivationFrame frame)
        {
            foreach (var e in expressionList.Reverse())
            {
                var ese = new ExpressionStackEmitter(e);
                ese.Emit(emitter, frame);
            }
        }
    }
}
