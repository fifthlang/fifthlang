namespace Fifth
{
    using System;
    using System.Collections.Generic;

    public static class GeneralHelpers
    {
        public static void ForEach<T>(this IEnumerable<T> seq, Action<T> job)
        {
            foreach (var t in seq)
            {
                job(t);
            }
        }

        public static IEnumerable<T> Map<T>(this IEnumerable<T> seq, Func<T, T> fn)
        {
            foreach (var t in seq)
            {
                yield return fn(t);
            }
        }
    }
}
