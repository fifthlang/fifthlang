using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifth.AST
{
    public partial class DestructuringParamDecl
    {
        public Expression Constraint { get; set; } // always null?
    }

    public partial class PropertyBinding
    {
    }
}
