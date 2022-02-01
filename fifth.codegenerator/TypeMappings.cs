using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifth.CodeGeneration
{
    using PrimitiveTypes;

    public static class TypeMappings
    {
        public static bool HasMapping(TypeId fifthTid)
            => DotnetPrimitiveNames.ContainsKey(fifthTid);
        public static string ToDotnetType(TypeId fifthTid)
            => DotnetPrimitiveNames[fifthTid];
        public static string ToBoxedDotnetType(TypeId fifthTid)
            => DotnetBoxedTypes[fifthTid];

        public static readonly Dictionary<TypeId, string> DotnetPrimitiveNames = new()
        {
            {PrimitiveBool.Default.TypeId, "bool"},
            {PrimitiveChar.Default.TypeId, "char"},
            {PrimitiveDate.Default.TypeId, "System.DateTimeOffset"},
            {PrimitiveDecimal.Default.TypeId, "decimal"},
            {PrimitiveDouble.Default.TypeId, "float64"},
            {PrimitiveFloat.Default.TypeId, "float32"},
            {PrimitiveInteger.Default.TypeId, "int32"},
            {PrimitiveLong.Default.TypeId, "int64"},
            {PrimitiveShort.Default.TypeId, "int16"},
            {PrimitiveString.Default.TypeId, "string"}
        };

        public static readonly Dictionary<TypeId, string> DotnetBoxedTypes = new()
        {
            {PrimitiveBool.Default.TypeId, "System.Boolean"},
            {PrimitiveChar.Default.TypeId, "System.Char"},
            {PrimitiveDate.Default.TypeId, "System.DateTimeOffset"},
            {PrimitiveDecimal.Default.TypeId, "System.Decimal"},
            {PrimitiveDouble.Default.TypeId, "System.Double"},
            {PrimitiveFloat.Default.TypeId, "System.Single"},
            {PrimitiveInteger.Default.TypeId, "System.Int32"},
            {PrimitiveLong.Default.TypeId, "System.Int64"},
            {PrimitiveShort.Default.TypeId, "System.Int16"},
            {PrimitiveString.Default.TypeId, "System.String"}
        };
    }
}
