namespace Fifth.LangProcessingPhases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AST;
    using AST.Visitors;
    using TypeSystem;

    public class DesugaringVisitor : BaseAstVisitor
    {
        public override void EnterFifthProgram(FifthProgram ctx)
        {
            var overloads = GatherOverloads(ctx.Functions);
            foreach (var fs in overloads)
            {
                if (fs.Value.Count > 1)
                {
                    var overloadedFunction = TransformOverloadGroup(fs.Key, fs.Value);
                    SubstituteFunctionDefinitions(ctx, fs.Value, overloadedFunction);
                }
            }
        }

        public override void EnterClassDefinition(ClassDefinition ctx)
        {
            var overloads = GatherOverloads(ctx.Functions);
            var newFunctionList = new List<IFunctionDefinition>();
            foreach (var fs in overloads)
            {
                if (fs.Value.Count > 1)
                {
                    var overloadedFunction = TransformOverloadGroup(fs.Key, fs.Value);
                    SubstituteFunctionDefinitions(ctx, fs.Value, overloadedFunction);
                }
                else
                {
                    newFunctionList.AddRange(fs.Value);
                }
            }
        }

        public override void LeaveExpressionList(ExpressionList ctx)
        {
            /*
            for (var i = 0; i < ctx.Expressions.Count - 1; i++)
            {
                if (ctx.Expressions[i] is FuncCallExpression fce)
                {
                    var x = new AssignmentStmt(ctx, fce.TypeId);
                    var variableReference = new VariableReference(ctx, null)
                    {
                        Name = "__discard__", ParentNode = fce.ParentNode
                    };
                    x.VariableRef = variableReference;
                    x.Expression = fce;
                    fce.ParentNode = x;
                    ctx.Expressions[i] = x;
                }
            }
        */
        }

        private IDictionary<IFunctionSignature, List<IFunctionDefinition>> GatherOverloads(IEnumerable<IFunctionDefinition> funcs)
        {
            var result = new Dictionary<IFunctionSignature, List<IFunctionDefinition>>();
            foreach (var f in funcs)
            {
                var signature = f.GetFunctionSignature();
                if (!result.ContainsKey(signature))
                {
                    result[signature] = new List<IFunctionDefinition>();
                }

                result[signature].Add(f);
            }

            return result;
        }

        private void SubstituteFunctionDefinitions(ClassDefinition classDefinition, List<IFunctionDefinition> fdg, IFunctionDefinition combinedFunction)
        {
            foreach (var fd in fdg)
            {
                classDefinition.RemoveFunction(fd);
            }
            classDefinition.AddFunction(combinedFunction);
        }
        private void SubstituteFunctionDefinitions(FifthProgram classDefinition, List<IFunctionDefinition> fdg, IFunctionDefinition combinedFunction)
        {
            foreach (var fd in fdg)
            {
                classDefinition.RemoveFunction(fd);
            }
            classDefinition.AddFunction(combinedFunction);
        }

        /// <summary>
        /// Combine all the different clauses for a set of overloaded functions into a OverloadedFunctionDefinition (which can be further transformed presently)
        /// </summary>
        /// <param name="functionSignature">the common signature on which this overload group is based</param>
        /// <param name="functionDefinitions">the original functions</param>
        /// <returns></returns>
        private IFunctionDefinition TransformOverloadGroup(IFunctionSignature functionSignature,
            List<IFunctionDefinition> functionDefinitions)
        {
            var orderedFuns = functionDefinitions.OrderBy(fd => fd.Line).ToList();
            var result = new OverloadedFunctionDefinition(orderedFuns, functionSignature)
                .CameFromSameSourceLocation(orderedFuns.First())
                .HasSameParentAs(functionDefinitions.First());
            return result;
        }
    }
}
