using ast;
using Fifth.LangProcessingPhases;
using FluentAssertions;

namespace ast_tests;

public class TypeAnnotationPipelineTests
{
    [Fact]
    public void Annotate_ShouldRunInjectedStagesInOrder_AndExposeSharedErrors()
    {
        var order = new List<string>();
        var stage1 = new TestStage("first", order, addError: true);
        var stage2 = new TestStage("second", order);
        var pipeline = new TypeAnnotationPipeline([stage1, stage2]);

        pipeline.Annotate(new Int32LiteralExp { Value = 1 });

        order.Should().Equal(["first", "second"]);
        pipeline.Errors.Should().ContainSingle();
        pipeline.Errors[0].Message.Should().Be("stage-first-error");
    }

    private sealed class TestStage(string name, ICollection<string> order, bool addError = false) : ITypeAnnotationStage
    {
        public AstThing Apply(AstThing ast, TypeAnnotationContext context)
        {
            order.Add(name);
            if (addError)
            {
                context.Errors.Add(new TypeCheckingError(
                    $"stage-{name}-error",
                    "",
                    0,
                    0,
                    [],
                    TypeCheckingSeverity.Error));
            }

            return ast;
        }
    }
}
