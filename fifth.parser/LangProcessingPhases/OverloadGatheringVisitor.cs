namespace Fifth.LangProcessingPhases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AST;
    using AST.Visitors;
    using TypeSystem;

    public class OverloadGatheringVisitor : BaseAstVisitor
    {
        public override void EnterFifthProgram(FifthProgram ctx)
            => Gather(ctx);

        public override void EnterClassDefinition(ClassDefinition ctx)
            => Gather(ctx);

        void Gather(IFunctionCollection ctx)
        {
            var overloads = GatherOverloads(ctx);
            foreach (var fs in overloads)
            {
                if (fs.Value.Count > 1)
                {
                    var overloadedFunction = TransformOverloadGroup(fs.Key, fs.Value);
                    SubstituteFunctionDefinitions(ctx, fs.Value, overloadedFunction);
                }
            }
        }

        private IDictionary<IFunctionSignature, List<IFunctionDefinition>> GatherOverloads(IFunctionCollection classDefinition)
        {
            var result = new Dictionary<IFunctionSignature, List<IFunctionDefinition>>();
            foreach (var f in classDefinition.Functions)
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

        private void SubstituteFunctionDefinitions(IFunctionCollection classDefinition, List<IFunctionDefinition> fdg, IFunctionDefinition combinedFunction)
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
