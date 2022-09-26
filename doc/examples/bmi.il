// Type: Program
// Assembly: ConsoleApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1025BDF9-2340-40BF-84D2-248488C5A7E6
// Location: /Users/aabs/Projects/ConsoleApp1/ConsoleApp1/bin/Debug/net6.0/ConsoleApp1.dll
// Sequence point data from /Users/aabs/Projects/ConsoleApp1/ConsoleApp1/bin/Debug/net6.0/ConsoleApp1.pdb

.class private auto ansi beforefieldinit
  Program
    extends [System.Runtime]System.Object
{
  .custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()
    = (01 00 00 00 )

  .method private hidebysig static void
    '<Main>$'(
      string[] args
    ) cil managed
  {
    .entrypoint
    .maxstack 5
    .locals init (
      [0] class ConsoleApp1.Person eric
    )

    IL_0000: nop

    // [12 1 - 12 36]
    IL_0001: ldstr        "Hello, World!"
    IL_0006: call         void [System.Console]System.Console::WriteLine(string)
    IL_000b: nop

    // [13 1 - 21 3]
    IL_000c: newobj       instance void ConsoleApp1.Person::.ctor()
    IL_0011: dup
    IL_0012: ldstr        "Eric"
    IL_0017: callvirt     instance void ConsoleApp1.Person::set_Name(string)
    IL_001c: nop
    IL_001d: dup
    IL_001e: newobj       instance void ConsoleApp1.VitalStatistics::.ctor()
    IL_0023: dup
    IL_0024: ldc.r8       1.93
    IL_002d: callvirt     instance void ConsoleApp1.VitalStatistics::set_Height(float64)
    IL_0032: nop
    IL_0033: dup
    IL_0034: ldc.r8       123.4
    IL_003d: callvirt     instance void ConsoleApp1.VitalStatistics::set_Weight(float64)
    IL_0042: nop
    IL_0043: callvirt     instance void ConsoleApp1.Person::set_Vitals(class ConsoleApp1.VitalStatistics)
    IL_0048: nop
    IL_0049: stloc.0      // eric

    // [22 1 - 22 40]
    IL_004a: ldloc.0      // eric
    IL_004b: call         float64 Program::'<<Main>$>g__calculate_bmi|0_0'(class ConsoleApp1.Person)
    IL_0050: call         void [System.Console]System.Console::WriteLine(float64)
    IL_0055: nop
    IL_0056: ret

  } // end of method Program::'<Main>$'

  .method public hidebysig specialname rtspecialname instance void
    .ctor() cil managed
  {
    .maxstack 8

    IL_0000: ldarg.0      // this
    IL_0001: call         instance void [System.Runtime]System.Object::.ctor()
    IL_0006: nop
    IL_0007: ret

  } // end of method Program::.ctor

  .method assembly hidebysig static float64
    '<<Main>$>g__calculate_bmi|0_0'(
      class ConsoleApp1.Person p
    ) cil managed
  {
    .custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor([in] unsigned int8)
      = (01 00 01 00 00 ) // .....
      // unsigned int8(1) // 0x01
    .custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()
      = (01 00 00 00 )
    .maxstack 3
    .locals init (
      [0] class ConsoleApp1.VitalStatistics vitals,
      [1] float64 height,
      [2] float64 weight,
      [3] float64 V_3
    )

    // [5 1 - 5 2]
    IL_0000: nop

    // [6 4 - 6 26]
    IL_0001: ldarg.0      // p
    IL_0002: callvirt     instance class ConsoleApp1.VitalStatistics ConsoleApp1.Person::get_Vitals()
    IL_0007: stloc.0      // vitals

    // [7 4 - 7 31]
    IL_0008: ldloc.0      // vitals
    IL_0009: callvirt     instance float64 ConsoleApp1.VitalStatistics::get_Height()
    IL_000e: stloc.1      // height

    // [8 4 - 8 31]
    IL_000f: ldloc.0      // vitals
    IL_0010: callvirt     instance float64 ConsoleApp1.VitalStatistics::get_Weight()
    IL_0015: stloc.2      // weight

    // [9 4 - 9 38]
    IL_0016: ldloc.2      // weight
    IL_0017: ldloc.1      // height
    IL_0018: ldloc.1      // height
    IL_0019: mul
    IL_001a: div
    IL_001b: stloc.3      // V_3
    IL_001c: br.s         IL_001e

    // [10 1 - 10 2]
    IL_001e: ldloc.3      // V_3
    IL_001f: ret

  } // end of method Program::'<<Main>$>g__calculate_bmi|0_0'
} // end of class Program
