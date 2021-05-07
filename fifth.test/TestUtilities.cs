namespace Fifth.Test
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    public static class TestUtilities
    {
        public static async Task<string> CopyEmbeddedResourceToTempFile(Type t, string fullName)
        {
            var p = Path.GetTempFileName();
            using var s = File.OpenWrite(p);
            using var sw = new StreamWriter(s);
            var content = await LoadEmbeddedResourceToString(t, fullName);
            await sw.WriteAsync(content.ToCharArray());
            return p;
        }

        public static Task<string> CopyTestResourceToTempFile(string fullName)
            => CopyEmbeddedResourceToTempFile(typeof(TestUtilities), fullName);

        public static Task<string> LoadEmbeddedResourceToString(Type t, string fullName)
        {
            var resourceStream = t.Assembly.GetManifestResourceStream(fullName);
            using var reader = new StreamReader(resourceStream, Encoding.UTF8);
            return reader.ReadToEndAsync();
        }

        public static TempFile LoadTestResource(string fullName)
        {
            var stream = typeof(TestUtilities).Assembly
                                              .GetManifestResourceStream(fullName);
            var sr = new StreamReader(stream);
            return new TempFile(sr);
        }
    }
}
