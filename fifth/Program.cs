namespace Fifth
{
    using System.IO;
    using Fifth.Runtime;

    // ReSharper disable once UnusedType.Global
    internal class Program
    {
        private static int Main(string fileName) => new FifthRuntime().Execute(File.ReadAllText(fileName));
    }
}
