namespace Fifth.AST
{
    using System;
    using PrimitiveTypes;
    using Visitors;

    public class FloatValueExpression : LiteralExpression<float>
    {
        public FloatValueExpression(float value)
            : base(value, PrimitiveFloat.Default.TypeId)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFloatValueExpression(this);
            visitor.LeaveFloatValueExpression(this);
        }
    }
    public class LongValueExpression : LiteralExpression<long>
    {
        public LongValueExpression(long value)
            : base(value, PrimitiveLong.Default.TypeId)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterLongValueExpression(this);
            visitor.LeaveLongValueExpression(this);
        }
    }
    public class ShortValueExpression : LiteralExpression<short>
    {
        public ShortValueExpression(short value)
            : base(value, PrimitiveShort.Default.TypeId)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterShortValueExpression(this);
            visitor.LeaveShortValueExpression(this);
        }
    }
    public class DoubleValueExpression : LiteralExpression<double>
    {
        public DoubleValueExpression(double value)
            : base(value, PrimitiveDouble.Default.TypeId)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterDoubleValueExpression(this);
            visitor.LeaveDoubleValueExpression(this);
        }
    }
    public class DecimalValueExpression : LiteralExpression<decimal>
    {
        public DecimalValueExpression(decimal value)
            : base(value, PrimitiveDecimal.Default.TypeId)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterDecimalValueExpression(this);
            visitor.LeaveDecimalValueExpression(this);
        }
    }
    public class DateValueExpression : LiteralExpression<DateTimeOffset>
    {
        public DateValueExpression(DateTimeOffset value)
            : base(value, PrimitiveDate.Default.TypeId)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterDateValueExpression(this);
            visitor.LeaveDateValueExpression(this);
        }
    }
}
