namespace Fifth.TypeSystem
{
    using System;
    using System.Collections.Generic;

    public class FunctionSignature : IFunctionSignature, IEquatable<FunctionSignature>, IEquatable<IFunctionSignature>
    {
        public FunctionSignature(string name, TypeId returnType, TypeId[] formalParameterTypes)
        {
            Name = name;
            ReturnType = returnType;
            FormalParameterTypes = formalParameterTypes;
        }

        public TypeId ReturnType { get; }
        public TypeId[] FormalParameterTypes { get; }
        public string Name { get; }
        public TypeId TypeId { get; set; }
        public TypeId[] GenericTypeParameters { get; }
        public virtual bool Equals(IFunctionSignature? other)
            => AreEqual(this, other);

        public bool Equals(FunctionSignature? other)
            => AreEqual(this, other);

        public override bool Equals(object? obj)
            => GetHashCode() == (obj?.GetHashCode()??0);
            // => AreEqual(this, obj as IFunctionSignature);

        public override int GetHashCode()
        {
            var x = new HashCode();
            foreach (var parameterType in FormalParameterTypes)
            {
                x.Add(parameterType);
            }
            x.Add(Name);
            x.Add(ReturnType);
            return x.ToHashCode();
        }
        public static bool AreEqual(IFunctionSignature x, IFunctionSignature y)
        {
            if ((x == null && y != null) ||(x != null && y == null))
            {
                return false;
            }

            var returnTypesMatch = Equals(x.ReturnType, y.ReturnType);
            if (x.FormalParameterTypes.Length != y.FormalParameterTypes.Length)
            {
                return false;
            }

            var paramTypesMatch = true;
            for (var i = 0; i < x.FormalParameterTypes.Length; i++)
            {
                paramTypesMatch &= Equals(x.FormalParameterTypes[i], y.FormalParameterTypes[i]);
            }

            var nameMatches = x.Name == y.Name;
            var tidMatches = Equals(x.TypeId, y.TypeId);

            if ((x.GenericTypeParameters == null && y.GenericTypeParameters != null) ||(x.GenericTypeParameters != null && y.GenericTypeParameters == null))
            {
                return false;
            }

            if (x.GenericTypeParameters != null && y.GenericTypeParameters != null && x.GenericTypeParameters.Length != y.GenericTypeParameters.Length)
            {
                return false;
            }

            var genericTypeParamsMatch = true;
            for (var i = 0; x.GenericTypeParameters != null &&  i < x.GenericTypeParameters.Length; i++)
            {
                paramTypesMatch &= Equals(x.GenericTypeParameters[i], y.GenericTypeParameters[i]);
            }

            return returnTypesMatch && paramTypesMatch &&
                   nameMatches && tidMatches &&
                   genericTypeParamsMatch;
        }
    }

    public class SignaturesAreEqual : EqualityComparer<IFunctionSignature>
    {
        public override bool Equals(IFunctionSignature x, IFunctionSignature y)
            => FunctionSignature.AreEqual(x, y);

        public override int GetHashCode(IFunctionSignature obj)
        {
            return HashCode.Combine(obj.Name, obj.FormalParameterTypes, obj.ReturnType);
        }
    }
}
