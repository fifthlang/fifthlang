//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from src/fifth.parser/Parser/grammar/FifthParser.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="FifthParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public interface IFifthParserListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.fifth"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFifth([NotNull] FifthParser.FifthContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.fifth"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFifth([NotNull] FifthParser.FifthContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.alias"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAlias([NotNull] FifthParser.AliasContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.alias"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAlias([NotNull] FifthParser.AliasContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtom([NotNull] FifthParser.AtomContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtom([NotNull] FifthParser.AtomContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] FifthParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] FifthParser.BlockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.equation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEquation([NotNull] FifthParser.EquationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.equation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEquation([NotNull] FifthParser.EquationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] FifthParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] FifthParser.ExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.formal_parameters"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFormal_parameters([NotNull] FifthParser.Formal_parametersContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.formal_parameters"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFormal_parameters([NotNull] FifthParser.Formal_parametersContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.function_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction_declaration([NotNull] FifthParser.Function_declarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.function_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction_declaration([NotNull] FifthParser.Function_declarationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.function_args"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction_args([NotNull] FifthParser.Function_argsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.function_args"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction_args([NotNull] FifthParser.Function_argsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.function_body"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction_body([NotNull] FifthParser.Function_bodyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.function_body"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction_body([NotNull] FifthParser.Function_bodyContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.function_call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction_call([NotNull] FifthParser.Function_callContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.function_call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction_call([NotNull] FifthParser.Function_callContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.function_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction_name([NotNull] FifthParser.Function_nameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.function_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction_name([NotNull] FifthParser.Function_nameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.iri"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIri([NotNull] FifthParser.IriContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.iri"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIri([NotNull] FifthParser.IriContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.module_import"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterModule_import([NotNull] FifthParser.Module_importContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.module_import"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitModule_import([NotNull] FifthParser.Module_importContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.module_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterModule_name([NotNull] FifthParser.Module_nameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.module_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitModule_name([NotNull] FifthParser.Module_nameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.multiplying_expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultiplying_expression([NotNull] FifthParser.Multiplying_expressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.multiplying_expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultiplying_expression([NotNull] FifthParser.Multiplying_expressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.packagename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPackagename([NotNull] FifthParser.PackagenameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.packagename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPackagename([NotNull] FifthParser.PackagenameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.pow_expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPow_expression([NotNull] FifthParser.Pow_expressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.pow_expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPow_expression([NotNull] FifthParser.Pow_expressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.parameter_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParameter_declaration([NotNull] FifthParser.Parameter_declarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.parameter_declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParameter_declaration([NotNull] FifthParser.Parameter_declarationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.parameter_type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParameter_type([NotNull] FifthParser.Parameter_typeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.parameter_type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParameter_type([NotNull] FifthParser.Parameter_typeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.parameter_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParameter_name([NotNull] FifthParser.Parameter_nameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.parameter_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParameter_name([NotNull] FifthParser.Parameter_nameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.q_function_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterQ_function_name([NotNull] FifthParser.Q_function_nameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.q_function_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitQ_function_name([NotNull] FifthParser.Q_function_nameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.qvarname"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterQvarname([NotNull] FifthParser.QvarnameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.qvarname"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitQvarname([NotNull] FifthParser.QvarnameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.q_type_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterQ_type_name([NotNull] FifthParser.Q_type_nameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.q_type_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitQ_type_name([NotNull] FifthParser.Q_type_nameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRelop([NotNull] FifthParser.RelopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRelop([NotNull] FifthParser.RelopContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.scientific"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterScientific([NotNull] FifthParser.ScientificContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.scientific"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitScientific([NotNull] FifthParser.ScientificContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.signed_atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSigned_atom([NotNull] FifthParser.Signed_atomContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.signed_atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSigned_atom([NotNull] FifthParser.Signed_atomContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] FifthParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] FifthParser.StatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.type_initialiser"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType_initialiser([NotNull] FifthParser.Type_initialiserContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.type_initialiser"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType_initialiser([NotNull] FifthParser.Type_initialiserContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.type_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType_name([NotNull] FifthParser.Type_nameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.type_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType_name([NotNull] FifthParser.Type_nameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.type_property_init"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType_property_init([NotNull] FifthParser.Type_property_initContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.type_property_init"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType_property_init([NotNull] FifthParser.Type_property_initContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.var_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVar_name([NotNull] FifthParser.Var_nameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.var_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVar_name([NotNull] FifthParser.Var_nameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ihier_part"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIhier_part([NotNull] FifthParser.Ihier_partContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ihier_part"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIhier_part([NotNull] FifthParser.Ihier_partContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.iri_reference"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIri_reference([NotNull] FifthParser.Iri_referenceContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.iri_reference"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIri_reference([NotNull] FifthParser.Iri_referenceContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.absolute_iri"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAbsolute_iri([NotNull] FifthParser.Absolute_iriContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.absolute_iri"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAbsolute_iri([NotNull] FifthParser.Absolute_iriContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.irelative_ref"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIrelative_ref([NotNull] FifthParser.Irelative_refContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.irelative_ref"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIrelative_ref([NotNull] FifthParser.Irelative_refContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.irelative_part"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIrelative_part([NotNull] FifthParser.Irelative_partContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.irelative_part"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIrelative_part([NotNull] FifthParser.Irelative_partContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.iauthority"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIauthority([NotNull] FifthParser.IauthorityContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.iauthority"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIauthority([NotNull] FifthParser.IauthorityContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.iuserinfo"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIuserinfo([NotNull] FifthParser.IuserinfoContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.iuserinfo"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIuserinfo([NotNull] FifthParser.IuserinfoContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ihost"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIhost([NotNull] FifthParser.IhostContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ihost"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIhost([NotNull] FifthParser.IhostContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ireg_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIreg_name([NotNull] FifthParser.Ireg_nameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ireg_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIreg_name([NotNull] FifthParser.Ireg_nameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ipath"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIpath([NotNull] FifthParser.IpathContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ipath"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIpath([NotNull] FifthParser.IpathContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ipath_abempty"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIpath_abempty([NotNull] FifthParser.Ipath_abemptyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ipath_abempty"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIpath_abempty([NotNull] FifthParser.Ipath_abemptyContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ipath_absolute"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIpath_absolute([NotNull] FifthParser.Ipath_absoluteContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ipath_absolute"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIpath_absolute([NotNull] FifthParser.Ipath_absoluteContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ipath_noscheme"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIpath_noscheme([NotNull] FifthParser.Ipath_noschemeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ipath_noscheme"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIpath_noscheme([NotNull] FifthParser.Ipath_noschemeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ipath_rootless"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIpath_rootless([NotNull] FifthParser.Ipath_rootlessContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ipath_rootless"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIpath_rootless([NotNull] FifthParser.Ipath_rootlessContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ipath_empty"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIpath_empty([NotNull] FifthParser.Ipath_emptyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ipath_empty"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIpath_empty([NotNull] FifthParser.Ipath_emptyContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.isegment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIsegment([NotNull] FifthParser.IsegmentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.isegment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIsegment([NotNull] FifthParser.IsegmentContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.isegment_nz"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIsegment_nz([NotNull] FifthParser.Isegment_nzContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.isegment_nz"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIsegment_nz([NotNull] FifthParser.Isegment_nzContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.isegment_nz_nc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIsegment_nz_nc([NotNull] FifthParser.Isegment_nz_ncContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.isegment_nz_nc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIsegment_nz_nc([NotNull] FifthParser.Isegment_nz_ncContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ipchar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIpchar([NotNull] FifthParser.IpcharContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ipchar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIpchar([NotNull] FifthParser.IpcharContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.iquery"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIquery([NotNull] FifthParser.IqueryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.iquery"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIquery([NotNull] FifthParser.IqueryContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ifragment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfragment([NotNull] FifthParser.IfragmentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ifragment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfragment([NotNull] FifthParser.IfragmentContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.iunreserved"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIunreserved([NotNull] FifthParser.IunreservedContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.iunreserved"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIunreserved([NotNull] FifthParser.IunreservedContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.scheme"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterScheme([NotNull] FifthParser.SchemeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.scheme"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitScheme([NotNull] FifthParser.SchemeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.port"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPort([NotNull] FifthParser.PortContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.port"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPort([NotNull] FifthParser.PortContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ip_literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIp_literal([NotNull] FifthParser.Ip_literalContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ip_literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIp_literal([NotNull] FifthParser.Ip_literalContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ip_v_future"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIp_v_future([NotNull] FifthParser.Ip_v_futureContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ip_v_future"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIp_v_future([NotNull] FifthParser.Ip_v_futureContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ip_v6_address"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIp_v6_address([NotNull] FifthParser.Ip_v6_addressContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ip_v6_address"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIp_v6_address([NotNull] FifthParser.Ip_v6_addressContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.h16"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterH16([NotNull] FifthParser.H16Context context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.h16"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitH16([NotNull] FifthParser.H16Context context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ls32"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLs32([NotNull] FifthParser.Ls32Context context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ls32"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLs32([NotNull] FifthParser.Ls32Context context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.ip_v4_address"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIp_v4_address([NotNull] FifthParser.Ip_v4_addressContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.ip_v4_address"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIp_v4_address([NotNull] FifthParser.Ip_v4_addressContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.dec_octet"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDec_octet([NotNull] FifthParser.Dec_octetContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.dec_octet"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDec_octet([NotNull] FifthParser.Dec_octetContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.pct_encoded"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPct_encoded([NotNull] FifthParser.Pct_encodedContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.pct_encoded"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPct_encoded([NotNull] FifthParser.Pct_encodedContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.unreserved"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnreserved([NotNull] FifthParser.UnreservedContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.unreserved"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnreserved([NotNull] FifthParser.UnreservedContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.reserved"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReserved([NotNull] FifthParser.ReservedContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.reserved"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReserved([NotNull] FifthParser.ReservedContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.gen_delims"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGen_delims([NotNull] FifthParser.Gen_delimsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.gen_delims"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGen_delims([NotNull] FifthParser.Gen_delimsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.sub_delims"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSub_delims([NotNull] FifthParser.Sub_delimsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.sub_delims"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSub_delims([NotNull] FifthParser.Sub_delimsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.alpha"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAlpha([NotNull] FifthParser.AlphaContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.alpha"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAlpha([NotNull] FifthParser.AlphaContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.hexdig"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterHexdig([NotNull] FifthParser.HexdigContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.hexdig"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitHexdig([NotNull] FifthParser.HexdigContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.digit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDigit([NotNull] FifthParser.DigitContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.digit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDigit([NotNull] FifthParser.DigitContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="FifthParser.non_zero_digit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNon_zero_digit([NotNull] FifthParser.Non_zero_digitContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="FifthParser.non_zero_digit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNon_zero_digit([NotNull] FifthParser.Non_zero_digitContext context);
}
