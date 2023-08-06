namespace Fifth.Test.metamodel;

using fifth.metamodel.metadata;
using Fifth.TypeSystem;
using PrimitiveTypes;

[TestFixture]
public class OpPrecedenceCalTests
{
    #region setup
    private TypeId boolTid;
    private TypeId shortTid;
    private TypeId intTid;
    private TypeId longTid;
    private TypeId floatTid;
    private TypeId doubleTid;
    private TypeId decimalTid;
    [SetUp]
    public void SetUp()
    {
        TypeRegistry.DefaultRegistry.LoadPrimitiveTypes();
        boolTid = PrimitiveBool.Default.TypeId;
        shortTid = PrimitiveShort.Default.TypeId;
        intTid = PrimitiveInteger.Default.TypeId;
        longTid = PrimitiveLong.Default.TypeId;
        floatTid = PrimitiveFloat.Default.TypeId;
        doubleTid = PrimitiveDouble.Default.TypeId;
        decimalTid = PrimitiveDecimal.Default.TypeId;
    }
    #endregion

    [TestCase(Operator.Add)]
    [TestCase(Operator.Subtract)]
    [TestCase(Operator.Multiply)]
    [TestCase(Operator.Divide)]
    public void CanInferType(Operator op)
    {
        // equal seniority
        TestCanInfer(op, shortTid, shortTid, shortTid);
        TestCanInfer(op, intTid, intTid, intTid);
        TestCanInfer(op, longTid, longTid, longTid);
        TestCanInfer(op, floatTid, floatTid, floatTid);
        TestCanInfer(op, doubleTid, doubleTid, doubleTid);
        TestCanInfer(op, decimalTid, decimalTid, decimalTid);

        // lhs lower seniority
        TestCanInfer(op, shortTid, intTid, intTid);
        TestCanInfer(op, intTid, longTid, longTid);
        TestCanInfer(op, longTid, floatTid, floatTid);
        TestCanInfer(op, floatTid, doubleTid, doubleTid);
        TestCanInfer(op, doubleTid, decimalTid, decimalTid);

        // rhs lower seniority
        TestCanInfer(op, intTid, shortTid, intTid);
        TestCanInfer(op, longTid, intTid, longTid);
        TestCanInfer(op, floatTid, longTid, floatTid);
        TestCanInfer(op, doubleTid, floatTid, doubleTid);
        TestCanInfer(op, decimalTid, doubleTid, decimalTid);

    }

    [TestCase(Operator.LessThan)]
    [TestCase(Operator.GreaterThan)]
    [TestCase(Operator.LessThanOrEqual)]
    [TestCase(Operator.GreaterThanOrEqual)]
    public void CanInferRelational(Operator op)
    {
        // equal seniority
        TestCanInfer(op, shortTid, shortTid, boolTid);
        TestCanInfer(op, intTid, intTid, boolTid);
        TestCanInfer(op, longTid, longTid, boolTid);
        TestCanInfer(op, floatTid, floatTid, boolTid);
        TestCanInfer(op, doubleTid, doubleTid, boolTid);
        TestCanInfer(op, decimalTid, decimalTid, boolTid);

        // lhs lower seniority
        TestCanInfer(op, shortTid, intTid, boolTid);
        TestCanInfer(op, intTid, longTid, boolTid);
        TestCanInfer(op, longTid, floatTid, boolTid);
        TestCanInfer(op, floatTid, doubleTid, boolTid);
        TestCanInfer(op, doubleTid, decimalTid, boolTid);

        // rhs lower seniority
        TestCanInfer(op, intTid, shortTid, boolTid);
        TestCanInfer(op, longTid, intTid, boolTid);
        TestCanInfer(op, floatTid, longTid, boolTid);
        TestCanInfer(op, doubleTid, floatTid, boolTid);
        TestCanInfer(op, decimalTid, doubleTid, boolTid);
    }
    private void TestCanInfer(Operator op, TypeId lhs, TypeId rhs, TypeId expectedTid)
    {
        var (actual, lhsCoercion, rhsCoercion) = OperatorPrecedenceCalculator.GetResultType(op, lhs, rhs);
        Assert.That(actual == expectedTid);
    }

}
