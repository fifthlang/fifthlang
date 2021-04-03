using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifth.CodeGeneration
{
    using AST;

    public class CodeGenerator
    {
        public static string GenerateCode(FifthProgram ast)
        {
            var ST = TemplateLoader.LoadTemplates();
            var f = ST.GetInstanceOf("program");
            f.Add("p", ast);
            var s = f.Render();
            return s;
        }
    }
}
