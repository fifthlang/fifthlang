namespace Fifth.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Antlr4.StringTemplate;
    using Microsoft.Extensions.FileProviders;

    public class TemplateLoader
    {
        public static TemplateGroupDirectory LoadTemplates()
        {
            var pathToTemplates = Path.GetFullPath("templates/CIL");
            // var pathToTemplates = Path.Join(Environment.CurrentDirectory, "templates", "CIL");
            return new (pathToTemplates, '<', '>');
        }

        public static Dictionary<string, Template> LoadEmbeddedTemplates()
        {
            var files = new Dictionary<string, Template>();
            var manifestEmbeddedProvider =
                new ManifestEmbeddedFileProvider(typeof(TemplateLoader).Assembly);
            foreach (var fi in manifestEmbeddedProvider.GetDirectoryContents("templates/CIL"))
            {
                using var s = fi.CreateReadStream();
                using var r = new StreamReader(s);
                files[fi.Name] = new Template(r.ReadToEnd());
            }

            return files;
        }
    }
}
