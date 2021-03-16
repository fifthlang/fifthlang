using System;

namespace fifth
{
    using System.Diagnostics;
    using System.IO;
    using Fifth.Runtime;

    class Program
    {
        static int Main(string fileName)
        {
            var result = new FifthRuntime().Execute(File.ReadAllText(fileName));
            Debug.WriteLine(result);
            return result;
        }

    }
}
