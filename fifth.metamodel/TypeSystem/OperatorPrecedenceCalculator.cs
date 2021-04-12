namespace Fifth.TypeSystem
{
    using System;
    using Fifth.PrimitiveTypes;
    using PrimitiveTypes;

    public static class OperatorPrecedenceCalculator
    {
        /// <summary>
        ///     Work out the result of an operation, and indicate what coercions if any are required to perform it
        /// </summary>
        /// <param name="op">the operation to perform</param>
        /// <param name="lhs">the tid of the lhs operand</param>
        /// <param name="rhs">the tid of the rhs operand</param>
        /// <returns>tuple (result tid, coercion type for lhs, coercion type for rhs)</returns>
        public static (TypeId, TypeId?, TypeId?) GetResultType(Operator? op, TypeId lhs, TypeId rhs)
        {
            if (!op.HasValue)
            {
                throw new ArgumentNullException(nameof(op));
            }

            return GetResultType(op.Value, lhs, rhs);
        }
        public static (TypeId, TypeId?, TypeId?) GetResultType(Operator op, TypeId lhs, TypeId rhs)
        {
            if (IsRelational(op))
            {
                return (PrimitiveBool.Default.TypeId, null, null);
            }

            if (lhs == rhs)
            {
                return (lhs, null, null);
            }

            if (TypeRegistry.DefaultRegistry.TryGetType(lhs, out var lhsType) &&
                TypeRegistry.DefaultRegistry.TryGetType(rhs, out var rhsType))
            {
                if (lhsType is PrimitiveNumeric lhsNum && rhsType is PrimitiveNumeric rhsNum)
                {
                    if (lhsNum.Seniority > rhsNum.Seniority)
                    {
                        return (lhs, null, lhs);
                    }

                    return (rhs, rhs, null);
                }
            }

            throw new TypeCheckingException("could not resolve numerical types ofr coercion");
        }

        private static bool IsRelational(Operator op)
            => op switch
            {
                Operator.And => true,
                Operator.Or => true,
                Operator.Not => true,
                Operator.Nand => true,
                Operator.Nor => true,
                Operator.Xor => true,
                Operator.Equal => true,
                Operator.NotEqual => true,
                Operator.LessThan => true,
                Operator.GreaterThan => true,
                Operator.LessThanOrEqual => true,
                Operator.GreaterThanOrEqual => true,
                _ => false
            };

        private static bool IsNumerical(Operator op)
            => !IsRelational(op);
    }
}
