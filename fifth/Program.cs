namespace Fifth
{
    using System;
    using System.IO;
    using Runtime;

    // ReSharper disable once UnusedType.Global
    internal class Program
    {
        private static void Main(string fileName)
        {
            var s = new FifthRuntime().Execute(File.ReadAllText(fileName));
            if (!string.IsNullOrWhiteSpace(s))
            {
                Console.Write(s);
            }
        }
    }
}
