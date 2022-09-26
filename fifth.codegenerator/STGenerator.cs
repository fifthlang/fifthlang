namespace Fifth.CodeGeneration;

using System.Globalization;
using System.IO;
using System.Reflection;

public class STGenerator
{
    public string GetEmbeddedResourceAsString(string resourceName) {
        var assembly = Assembly.GetExecutingAssembly();
        // assembly.GetManifestResourceNames(); // Get all the resources
        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream != null)
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
            return string.Empty;
        }
    }

    public string ProcessResourceTemplate(string resourceName, string[,] parameters)
    {
        string template = GetEmbeddedResourceAsString(resourceName);
        return ProcessTemplate(template, parameters);
    }

    public string ProcessTemplate(string stringTemplate, string[,] parameters)
    {
        TemplateGroup templateGroup = new TemplateGroup('{', '}');
        Template template = new Template(templateGroup, stringTemplate);

        for (int i = 0; i < parameters.GetLength(0);i += 1)
        {
            template.Add(parameters[i, 0], parameters[i, 1]);
        }

        return template.Render(new CultureInfo("nl-NL"), 200);
    }
}
