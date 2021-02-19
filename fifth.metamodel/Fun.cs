namespace Fifth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public static class Fun
    {
        public static FuncWrapper AsFun<T>(this T x) => Wrap(() => x);

        public static FuncWrapper Wrap(this Delegate d)
        {
            var method = d.Method;
            var parameters = method.GetParameters();
            var formalParams = parameters.Select(p => Expression.Parameter(p.ParameterType, p.Name))
                .ToArray();
            var call = Expression.Call(null, method, formalParams);
            return new FuncWrapper(parameters.Select(p => p.ParameterType).ToList(), method.ReturnType, Expression.Lambda(call, formalParams).Compile(), method.MetadataToken);
        }

        public static FuncWrapper Wrap<R>(Func<R> fn) =>
            new FuncWrapper(new List<Type> { }, typeof(R), f: fn);

        public static FuncWrapper Wrap<T1, R>(Func<T1, R> fn) =>
            new FuncWrapper(new List<Type> { typeof(T1) }, typeof(R), f: fn);

        public static FuncWrapper Wrap<T1, T2, R>(Func<T1, T2, R> fn) =>
            new FuncWrapper(new List<Type> { typeof(T1), typeof(T2) }, typeof(R), f: fn);

        public static FuncWrapper Wrap<T1, T2, T3, R>(Func<T1, T2, T3, R> fn) =>
            new FuncWrapper(new List<Type> { typeof(T1), typeof(T2), typeof(T3) }, typeof(R), f: fn);

        public static FuncWrapper Wrap<T1, T2, T3, T4, R>(Func<T1, T2, T3, T4, R> fn) =>
            new FuncWrapper(new List<Type> { typeof(T1), typeof(T2), typeof(T3), typeof(T4) }, typeof(R), f: fn);

        public static FuncWrapper Wrap<T1, T2, T3, T4, T5, R>(Func<T1, T2, T3, T4, T5, R> fn) =>
            new FuncWrapper(new List<Type> { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) }, typeof(R), f: fn);
    }
}
