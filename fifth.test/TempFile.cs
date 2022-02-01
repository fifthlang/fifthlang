namespace Fifth.Test
{
    using System;
    using System.IO;

    public class TempFile : IDisposable
    {
        public TempFile(StreamReader sr)
        {
            Path = System.IO.Path.GetTempFileName();
            using var s = File.OpenWrite(Path);
            using var sw = new StreamWriter(s);
            var content = sr.ReadToEnd();
            sw.Write(content.ToCharArray());
        }

        public string Path { get; }

        public void Dispose()
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
        }
    }
}
