namespace Fifth.Runtime.LangProcessingPhases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AST;
    using PrimitiveTypes;

    /*
    public class EmitBuilder
    {
        private readonly IRuntimeStack runtimeStack;
        internal readonly Queue<StackElement> tmpQueue;

        private EmitBuilder(IRuntimeStack runtimeStack)
        {
            this.runtimeStack = runtimeStack;
            tmpQueue = new Queue<StackElement>();
        }

        public static EmitBuilder Into(IRuntimeStack stack) => new EmitBuilder(stack);

        public EmitBuilder VariableDeclaration(VariableDeclarationStatement variableDeclarationStatement) => this;
    }*/

    public class StackEmitter : IStackEmitter
    {
        /// <summary>
        ///     Adds the wrapped function to the stack
        /// </summary>
        /// <param name="stack">the stack into which elements are emitted</param>
        /// <param name="fs">
        ///     A variable length param array of wrapped functions to be pushed onto the stack
        /// </param>
        public void Emit(IRuntimeStack stack, params StackElement[] fs)
        {
            foreach (var f in fs)
            {
                stack.Push(f);
            }
        }

        public void Operator(IRuntimeStack stack, BinaryExpression ctx)
        {
            var lhsType = LookupType(ctx.Left).GetType();
            var rhsType = LookupType(ctx.Right).GetType();

            if (!TypeHelpers.TryGetOperatorByNameAndTypes(ctx.Op, lhsType, rhsType, out var operatorFunction))
            {
                throw new TypeCheckingException(
                    $"operator {ctx.Op} not supported between {lhsType.Name} and {rhsType.Name}.");
            }

            Emit(stack, new FunctionStackElement(operatorFunction));
        }

        public void Value(IRuntimeStack stack, object v)
            => Emit(stack, WrapValue(v));

        public void VariableReference(IRuntimeStack stack, string v) => Emit(stack, WrapVariableReference(v));

        public void BinaryFunction<T1, T2, TR>(IRuntimeStack stack, Func<T1, T2, TR> f)
            => Emit(stack, WrapBinaryFunction(f));

        public void UnaryFunction<T1, TR>(IRuntimeStack stack, Func<T1, TR> f)
            => Emit(stack, WrapUnaryFunction(f));

        public void MetaFunction(IRuntimeStack stack, Func<IDispatcher, IDispatcher> metafunc)
            => Emit(stack, WrapMetaFunction(metafunc));

        public StackElement WrapVariableReference(string i)
            => new VariableReferenceStackElement(i);

        public StackElement WrapValue(object v)
            => new ValueStackElement(v);

        public StackElement WrapBinaryFunction<T1, T2, TR>(Func<T1, T2, TR> f)
            => new FunctionStackElement(Fun.Wrap(f));

        public StackElement WrapUnaryFunction<T1, TR>(Func<T1, TR> f)
            => new FunctionStackElement(Fun.Wrap(f));

        public StackElement WrapMetaFunction(Func<IDispatcher, IDispatcher> metafunc)
            => new MetaFunctionStackElement(Fun.Wrap(metafunc));

        private IFifthType LookupType(Expression e) => e["type"] as IFifthType;
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
        private readonly AstFunctionDefinition astFunction;
        private readonly RuntimeFunctionDefinition runtimeFunction;

        public FunctionDefinitionEmitter(AST.AstFunctionDefinition astFunction)
        {
            this.astFunction = astFunction;
            runtimeFunction = new RuntimeFunctionDefinition { Name = astFunction.Name };
            if (this.astFunction.ParameterDeclarations != null &&
                this.astFunction.ParameterDeclarations.ParameterDeclarations.Any())
            {
                runtimeFunction.Arguments.AddRange(this.astFunction.ParameterDeclarations.ParameterDeclarations.Select(
                    (p, i) =>
                        new FunctionArgument { ArgOrdinal = i, Name = p.ParameterName, Type = p.ParameterType }));
            }
        }

        public void Emit(IStackEmitter emitter, IActivationFrame frame)
        {
            var ele = new ExpressionListStackEmitter(astFunction.Body);
            ele.Emit(emitter, runtimeFunction);
            frame.Environment.AddFunctionDefinition(runtimeFunction);
            foreach (var kvp in runtimeFunction.Environment.Definitions)
            {
                frame.Environment.AddFunctionDefinition(kvp.Value,kvp.Key);
            }
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
                case AssignmentStmt ae:
                    EmitAssignmentExpression(ae, emitter, frame);
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
                case IfElseExp ifElseExp:
                    EmitIfElseExpression(ifElseExp, emitter, frame);
                    break;
                case WhileExp whileExp:
                    EmitWhileExpression(whileExp, emitter, frame);
                    break;
            }
        }

        private void EmitAssignmentExpression(AssignmentStmt ae, IStackEmitter emitter, IActivationFrame frame)
        {
            new ExpressionStackEmitter(ae.Expression).Emit(emitter, frame);
            emitter.Value(frame.Stack, ae.VariableRef.Name);
            emitter.MetaFunction(frame.Stack, MetaFunction.BindVariable);
        }

        private void EmitIfElseExpression(IfElseExp e, IStackEmitter emitter, IActivationFrame frame)
        {
            // TODO: steps to generate code for IfElseExp:
            //   create new anon function for if part
            //   create new anon function for else part
            //   emit function names 
            //   emit the condition
            //   emit the metafunction to choose which to call
            EmitBlock(e.ElseBlock, emitter, frame);
            EmitBlock(e.IfBlock, emitter, frame);

            var condEmitter = new ExpressionStackEmitter(e.Condition);
            condEmitter.Emit(emitter, frame);

            emitter.MetaFunction(frame.Stack, MetaFunction.BranchIfTrue);
        }
        private void EmitWhileExpression(WhileExp e, IStackEmitter emitter, IActivationFrame frame)
        {
            EmitBlock(e.LoopBlock, emitter, frame);

            var condFunName = Guid.NewGuid().ToString();
            var condFun = new RuntimeFunctionDefinition(frame)
            {
                Name = condFunName,
                Type = PrimitiveBool.Default
            };

            var condEmitter = new ExpressionStackEmitter(e.Condition);
            condEmitter.Emit(emitter, condFun);
            frame.Environment.AddFunctionDefinition(condFun);

            emitter.Value(frame.Stack, condFunName);
            emitter.MetaFunction(frame.Stack, MetaFunction.WhileTrue);
        }

        public void EmitBlock(Block b, IStackEmitter emitter, IActivationFrame frame)
        {
            var blockEmitter = new BlockEmitter(b);
            var functionName = Guid.NewGuid().ToString();
            var fd = new RuntimeFunctionDefinition(frame)
            {
                Name = functionName
            };
            blockEmitter.Emit(emitter, fd);
            //   create new anon function for block
            frame.Environment.AddFunctionDefinition(fd);
            //   emit function name
            emitter.Value(frame.Stack, functionName);
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

        public ExpressionListStackEmitter(Block b) => expressionList = b.Expressions;
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

    public class BlockEmitter : ISpecialFormEmitter
    {
        private readonly IEnumerable<Expression> expressionList;

        public BlockEmitter(Block b) => expressionList = b.Expressions;

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
