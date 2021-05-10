namespace Fifth.LangProcessingPhases
{
    using System.Collections.Generic;
    using System.Linq;
    using AST;
    using AST.Visitors;

    /// <summary>
    /// Convert a multi-clause function into simpler form.
    /// </summary>
    public class OverloadTransformingVisitor : BaseAstVisitor
    {
        public override void EnterOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx)
        {
            int clauseCounter = 0;
            var clauses = new List<(Expression, IFunctionDefinition)>();
            foreach (var clause in ctx.OverloadClauses)
            {
                var precondition = GetPrecondition(clause);
                var subClauseFunction = GetSubclauseFunction(clause);
                clauses.Add((precondition, subClauseFunction));
            }

            var guardFunction = GenerateGuardFunction(ctx, clauses);
            var newFunctions = clauses.Select(c => c.Item2);
            newFunctions =newFunctions.Prepend(guardFunction);
            SubstituteFunctionDefinitions((IFunctionCollection) ctx.ParentNode, new[] {ctx}, newFunctions);
        }

        private IFunctionDefinition GenerateGuardFunction(OverloadedFunctionDefinition ctx, List<(Expression, IFunctionDefinition)> clauses)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// transforms the function into a form that can be envoked from a dispatcher guard function
        /// </summary>
        /// <param name="clause">the original function to transform</param>
        /// <returns>a new function</returns>
        private IFunctionDefinition GetSubclauseFunction(IFunctionDefinition clause)
        {
            // get all the bindings
            // create a new function with unique name
            // normalise param list and insert into function
            // add binding var decls to front of body
            // insert body of old function into new function
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// extract the preconditions for entry into a subclause
        /// </summary>
        /// <param name="clause">the original function with preconditions</param>
        /// <returns>an expression combining all preconditions</returns>
        private Expression GetPrecondition(IFunctionDefinition clause)
        {
            var conditions = new Queue<Expression>();
            foreach (var pd in clause.ParameterDeclarations.ParameterDeclarations)
            {
                if (pd is DestructuringParamDecl dpd)
                {
                    foreach (var pb in dpd.PropertyBindings)
                    {
                        if (pb.Constraint != null)
                        {
                            conditions.Enqueue(pb.Constraint);
                        }
                    }
                }
            }

            Expression e = null;
            while (conditions.Count > 0)
            {
                if (e==null)
                {
                    e = conditions.Dequeue();
                }
                else
                {
                    e = new BinaryExpression(e, Operator.And, conditions.Dequeue());
                }
            }

            return e;
        }

        private void SubstituteFunctionDefinitions(IFunctionCollection functionContainer, IEnumerable<IFunctionDefinition> functionsToRemove, IEnumerable<IFunctionDefinition> functionsToAdd)
        {
            foreach (var fd in functionsToRemove)
            {
                functionContainer.RemoveFunction(fd);
            }
            foreach (var fd in functionsToAdd)
            {
                functionContainer.AddFunction(fd);
            }
        }
    }
}
