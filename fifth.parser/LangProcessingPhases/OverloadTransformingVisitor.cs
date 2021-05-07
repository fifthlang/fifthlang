namespace Fifth.LangProcessingPhases
{
    using System.Collections.Generic;
    using AST;
    using AST.Visitors;

    /// <summary>
    /// Convert a multi-clause function into simpler form.
    /// </summary>
    public class OverloadTransformingVisitor : BaseAstVisitor
    {
        public override void EnterOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx)
        {
            var combinedFunctions = GenerateCombinedFunctions(ctx);
            base.EnterOverloadedFunctionDefinition(ctx);
        }

        private IList<IFunctionDefinition> GenerateCombinedFunctions(OverloadedFunctionDefinition ctx)
        {
            // . create the new guard function (gf)
            // . create a list of if-statements to store clauses
            // . for each sub function (sf)
            // .    create a list of variable bindings to store pattern matches in
            // .    for each pattern matching
            // .        create a variable declaration
            // .        
            // 2. rename the sub functions to have unique names
            // 3. reduce arg list for sub functions to normal types
            // 4. 
            throw new System.NotImplementedException();
        }
    }
}
