namespace Fifth.TypeSystem
{
    using System;
    using System.Collections.Generic;
    using fifth.metamodel.metadata;

    public class FunctionSignature : IFunctionSignature, IEquatable<FunctionSignature>, IEquatable<IFunctionSignature>
    {
        public FunctionSignature(string name, TypeId returnType, TypeId[] formalParameterTypes)
        {
            Name = name;
            ReturnType = returnType;
            FormalParameterTypes = formalParameterTypes;
        }

        public TypeId[] FormalParameterTypes { get; }
        public TypeId[] GenericTypeParameters { get; }
        public string Name { get; }
        public string Namespace { get; }
        public TypeId ReturnType { get; }
        public TypeId TypeId { get; set; }

        public static bool AreEqual(IFunctionSignature x, IFunctionSignature y)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            if (y is null)
            {
                throw new ArgumentNullException(nameof(y));
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

            if ((x.GenericTypeParameters == null && y.GenericTypeParameters != null) || (x.GenericTypeParameters != null && y.GenericTypeParameters == null))
            {
                return false;
            }

            if (x.GenericTypeParameters != null && y.GenericTypeParameters != null && x.GenericTypeParameters.Length != y.GenericTypeParameters.Length)
            {
                return false;
            }

            var genericTypeParamsMatch = true;
            for (var i = 0; x.GenericTypeParameters != null && i < x.GenericTypeParameters.Length; i++)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                paramTypesMatch &= Equals(x.GenericTypeParameters[i], y.GenericTypeParameters[i]);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            return returnTypesMatch && paramTypesMatch &&
                   nameMatches && tidMatches &&
                   genericTypeParamsMatch;
        }

        public virtual bool Equals(IFunctionSignature other)
        {
            return AreEqual(this, other);
        }

        public bool Equals(FunctionSignature other)
        {
            return AreEqual(this, other);
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == (obj?.GetHashCode() ?? 0);
        }

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
    }

    public class SignaturesAreEqual : EqualityComparer<IFunctionSignature>
    {
        public override bool Equals(IFunctionSignature x, IFunctionSignature y)
        {
            return FunctionSignature.AreEqual(x, y);
        }

        public override int GetHashCode(IFunctionSignature obj)
        {
            return HashCode.Combine(obj.Name, obj.FormalParameterTypes, obj.ReturnType);
        }
    }
}
