namespace Fifth.CodeGeneration.IL;

using System.Text;

public partial class AssemblyDeclarationBuilder
{
    public override string Build()
    {
        var sb = new StringBuilder();
        sb.AppendLine($@".assembly {Model.Name}
            {{
              .custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 )
              .custom instance void [System.Runtime]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                                               63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

              // --- The following custom attribute is added automatically, do not uncomment -------
              //  .custom instance void [System.Runtime]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [System.Runtime]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 )

              .custom instance void [System.Runtime]System.Runtime.Versioning.TargetFrameworkAttribute::.ctor(string) = ( 01 00 18 2E 4E 45 54 43 6F 72 65 41 70 70 2C 56   // ....NETCoreApp,V
                                                                                                                          65 72 73 69 6F 6E 3D 76 35 2E 30 01 00 54 0E 14   // ersion=v5.0..T..
                                                                                                                          46 72 61 6D 65 77 6F 72 6B 44 69 73 70 6C 61 79   // FrameworkDisplay
                                                                                                                          4E 61 6D 65 00 )                                  // Name.
              .hash algorithm 0x00008004
              .ver {WriteVersion(Model.Version)}
            }}");
        sb.AppendLine(ModuleDeclarationBuilder.Create(Model.PrimeModule).Build());
        return sb.ToString();
    }

}
