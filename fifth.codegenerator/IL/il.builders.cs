namespace Fifth.CodeGeneration.IL;

using System;
using System.Collections.Generic;
using CodeGeneration.IL;
using fifth.metamodel.metadata.il;


    public partial class AssemblyDeclarationBuilder : BaseBuilder<AssemblyDeclarationBuilder,fifth.metamodel.metadata.il.AssemblyDeclaration>
    {
        public AssemblyDeclarationBuilder()
        {
            Model = new();
        }

        public AssemblyDeclarationBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public AssemblyDeclarationBuilder WithVersion(fifth.metamodel.metadata.il.Version value){
            Model.Version = value;
            return this;
        }

        public AssemblyDeclarationBuilder WithProgram(fifth.metamodel.metadata.il.ProgramDefinition value){
            Model.Program = value;
            return this;
        }

    }
    public partial class AssemblyReferenceBuilder : BaseBuilder<AssemblyReferenceBuilder,fifth.metamodel.metadata.il.AssemblyReference>
    {
        public AssemblyReferenceBuilder()
        {
            Model = new();
        }

        public AssemblyReferenceBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public AssemblyReferenceBuilder WithPublicKeyToken(System.String value){
            Model.PublicKeyToken = value;
            return this;
        }

        public AssemblyReferenceBuilder WithVersion(fifth.metamodel.metadata.il.Version value){
            Model.Version = value;
            return this;
        }

    }
    public partial class ClassDefinitionBuilder : BaseBuilder<ClassDefinitionBuilder,fifth.metamodel.metadata.il.ClassDefinition>
    {
        public ClassDefinitionBuilder()
        {
            Model = new();
        }

        public ClassDefinitionBuilder WithFields(List<fifth.metamodel.metadata.il.FieldDefinition> value){
            Model.Fields = value;
            return this;
        }

        public ClassDefinitionBuilder AddingItemToFields(fifth.metamodel.metadata.il.FieldDefinition value){
            Model.Fields.Add(value);
            return this;
        }
        public ClassDefinitionBuilder WithProperties(List<fifth.metamodel.metadata.il.PropertyDefinition> value){
            Model.Properties = value;
            return this;
        }

        public ClassDefinitionBuilder AddingItemToProperties(fifth.metamodel.metadata.il.PropertyDefinition value){
            Model.Properties.Add(value);
            return this;
        }
        public ClassDefinitionBuilder WithMethods(List<fifth.metamodel.metadata.il.MethodDefinition> value){
            Model.Methods = value;
            return this;
        }

        public ClassDefinitionBuilder AddingItemToMethods(fifth.metamodel.metadata.il.MethodDefinition value){
            Model.Methods.Add(value);
            return this;
        }
        public ClassDefinitionBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public ClassDefinitionBuilder WithVisibility(fifth.metamodel.metadata.il.ILVisibility value){
            Model.Visibility = value;
            return this;
        }

        public ClassDefinitionBuilder WithBaseClassName(System.String value){
            Model.BaseClassName = value;
            return this;
        }

    }
    public partial class UnaryExpressionBuilder : BaseBuilder<UnaryExpressionBuilder,fifth.metamodel.metadata.il.UnaryExpression>
    {
        public UnaryExpressionBuilder()
        {
            Model = new();
        }

        public UnaryExpressionBuilder WithOp(System.String value){
            Model.Op = value;
            return this;
        }

        public UnaryExpressionBuilder WithExp(fifth.metamodel.metadata.il.Expression value){
            Model.Exp = value;
            return this;
        }

    }
    public partial class BinaryExpressionBuilder : BaseBuilder<BinaryExpressionBuilder,fifth.metamodel.metadata.il.BinaryExpression>
    {
        public BinaryExpressionBuilder()
        {
            Model = new();
        }

        public BinaryExpressionBuilder WithOp(System.String value){
            Model.Op = value;
            return this;
        }

        public BinaryExpressionBuilder WithLHS(fifth.metamodel.metadata.il.Expression value){
            Model.LHS = value;
            return this;
        }

        public BinaryExpressionBuilder WithRHS(fifth.metamodel.metadata.il.Expression value){
            Model.RHS = value;
            return this;
        }

    }
    public partial class TypeCastExpressionBuilder : BaseBuilder<TypeCastExpressionBuilder,fifth.metamodel.metadata.il.TypeCastExpression>
    {
        public TypeCastExpressionBuilder()
        {
            Model = new();
        }

        public TypeCastExpressionBuilder WithTargetTypeName(System.String value){
            Model.TargetTypeName = value;
            return this;
        }

        public TypeCastExpressionBuilder WithTargetTypeCilName(System.String value){
            Model.TargetTypeCilName = value;
            return this;
        }

        public TypeCastExpressionBuilder WithExpression(fifth.metamodel.metadata.il.Expression value){
            Model.Expression = value;
            return this;
        }

    }
    public partial class FuncCallExpBuilder : BaseBuilder<FuncCallExpBuilder,fifth.metamodel.metadata.il.FuncCallExp>
    {
        public FuncCallExpBuilder()
        {
            Model = new();
        }

        public FuncCallExpBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public FuncCallExpBuilder WithArgs(List<fifth.metamodel.metadata.il.Expression> value){
            Model.Args = value;
            return this;
        }

        public FuncCallExpBuilder AddingItemToArgs(fifth.metamodel.metadata.il.Expression value){
            Model.Args.Add(value);
            return this;
        }
        public FuncCallExpBuilder WithReturnType(System.String value){
            Model.ReturnType = value;
            return this;
        }

        public FuncCallExpBuilder WithClassDefinition(fifth.metamodel.metadata.il.ClassDefinition value){
            Model.ClassDefinition = value;
            return this;
        }

        public FuncCallExpBuilder WithArgTypes(List<System.String> value){
            Model.ArgTypes = value;
            return this;
        }

        public FuncCallExpBuilder AddingItemToArgTypes(System.String value){
            Model.ArgTypes.Add(value);
            return this;
        }
    }
    public partial class MemberAccessExpressionBuilder : BaseBuilder<MemberAccessExpressionBuilder,fifth.metamodel.metadata.il.MemberAccessExpression>
    {
        public MemberAccessExpressionBuilder()
        {
            Model = new();
        }

        public MemberAccessExpressionBuilder WithLhs(fifth.metamodel.metadata.il.Expression value){
            Model.Lhs = value;
            return this;
        }

        public MemberAccessExpressionBuilder WithRhs(fifth.metamodel.metadata.il.Expression value){
            Model.Rhs = value;
            return this;
        }

    }
    public partial class VariableReferenceExpressionBuilder : BaseBuilder<VariableReferenceExpressionBuilder,fifth.metamodel.metadata.il.VariableReferenceExpression>
    {
        public VariableReferenceExpressionBuilder()
        {
            Model = new();
        }

        public VariableReferenceExpressionBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public VariableReferenceExpressionBuilder WithSymTabEntry(System.Object value){
            Model.SymTabEntry = value;
            return this;
        }

        public VariableReferenceExpressionBuilder WithIsParameterReference(System.Boolean value){
            Model.IsParameterReference = value;
            return this;
        }

        public VariableReferenceExpressionBuilder WithOrdinal(System.Int32 value){
            Model.Ordinal = value;
            return this;
        }

    }
    public partial class FieldDefinitionBuilder : BaseBuilder<FieldDefinitionBuilder,fifth.metamodel.metadata.il.FieldDefinition>
    {
        public FieldDefinitionBuilder()
        {
            Model = new();
        }

        public FieldDefinitionBuilder WithTypeName(System.String value){
            Model.TypeName = value;
            return this;
        }

        public FieldDefinitionBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public FieldDefinitionBuilder WithVisibility(fifth.metamodel.metadata.il.ILVisibility value){
            Model.Visibility = value;
            return this;
        }

    }
    public partial class IfStatementBuilder : BaseBuilder<IfStatementBuilder,fifth.metamodel.metadata.il.IfStatement>
    {
        public IfStatementBuilder()
        {
            Model = new();
        }

        public IfStatementBuilder WithConditional(fifth.metamodel.metadata.il.Expression value){
            Model.Conditional = value;
            return this;
        }

        public IfStatementBuilder WithIfBlock(fifth.metamodel.metadata.il.Block value){
            Model.IfBlock = value;
            return this;
        }

        public IfStatementBuilder WithElseBlock(fifth.metamodel.metadata.il.Block value){
            Model.ElseBlock = value;
            return this;
        }

    }
    public partial class MethodDefinitionBuilder : BaseBuilder<MethodDefinitionBuilder,fifth.metamodel.metadata.il.MethodDefinition>
    {
        public MethodDefinitionBuilder()
        {
            Model = new();
        }

        public MethodDefinitionBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public MethodDefinitionBuilder WithReturnType(System.String value){
            Model.ReturnType = value;
            return this;
        }

        public MethodDefinitionBuilder WithParameters(List<fifth.metamodel.metadata.il.ParameterDeclaration> value){
            Model.Parameters = value;
            return this;
        }

        public MethodDefinitionBuilder AddingItemToParameters(fifth.metamodel.metadata.il.ParameterDeclaration value){
            Model.Parameters.Add(value);
            return this;
        }
        public MethodDefinitionBuilder WithVisibility(fifth.metamodel.metadata.il.ILVisibility value){
            Model.Visibility = value;
            return this;
        }

        public MethodDefinitionBuilder WithBody(fifth.metamodel.metadata.il.Block value){
            Model.Body = value;
            return this;
        }

        public MethodDefinitionBuilder WithFunctionKind(fifth.metamodel.metadata.FunctionKind value){
            Model.FunctionKind = value;
            return this;
        }

    }
    public partial class BlockBuilder : BaseBuilder<BlockBuilder,fifth.metamodel.metadata.il.Block>
    {
        public BlockBuilder()
        {
            Model = new();
        }

        public BlockBuilder WithStatements(List<fifth.metamodel.metadata.il.Statement> value){
            Model.Statements = value;
            return this;
        }

        public BlockBuilder AddingItemToStatements(fifth.metamodel.metadata.il.Statement value){
            Model.Statements.Add(value);
            return this;
        }
    }
    public partial class ModuleDeclarationBuilder : BaseBuilder<ModuleDeclarationBuilder,fifth.metamodel.metadata.il.ModuleDeclaration>
    {
        public ModuleDeclarationBuilder()
        {
            Model = new();
        }

        public ModuleDeclarationBuilder WithFileName(System.String value){
            Model.FileName = value;
            return this;
        }

    }
    public partial class ParameterDeclarationBuilder : BaseBuilder<ParameterDeclarationBuilder,fifth.metamodel.metadata.il.ParameterDeclaration>
    {
        public ParameterDeclarationBuilder()
        {
            Model = new();
        }

        public ParameterDeclarationBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public ParameterDeclarationBuilder WithTypeName(System.String value){
            Model.TypeName = value;
            return this;
        }

        public ParameterDeclarationBuilder WithIsUDTType(System.Boolean value){
            Model.IsUDTType = value;
            return this;
        }

    }
    public partial class ProgramDefinitionBuilder : BaseBuilder<ProgramDefinitionBuilder,fifth.metamodel.metadata.il.ProgramDefinition>
    {
        public ProgramDefinitionBuilder()
        {
            Model = new();
        }

        public ProgramDefinitionBuilder WithTargetAsmFileName(System.String value){
            Model.TargetAsmFileName = value;
            return this;
        }

        public ProgramDefinitionBuilder WithClasses(List<fifth.metamodel.metadata.il.ClassDefinition> value){
            Model.Classes = value;
            return this;
        }

        public ProgramDefinitionBuilder AddingItemToClasses(fifth.metamodel.metadata.il.ClassDefinition value){
            Model.Classes.Add(value);
            return this;
        }
        public ProgramDefinitionBuilder WithFunctions(List<fifth.metamodel.metadata.il.MethodDefinition> value){
            Model.Functions = value;
            return this;
        }

        public ProgramDefinitionBuilder AddingItemToFunctions(fifth.metamodel.metadata.il.MethodDefinition value){
            Model.Functions.Add(value);
            return this;
        }
    }
    public partial class PropertyDefinitionBuilder : BaseBuilder<PropertyDefinitionBuilder,fifth.metamodel.metadata.il.PropertyDefinition>
    {
        public PropertyDefinitionBuilder()
        {
            Model = new();
        }

        public PropertyDefinitionBuilder WithTypeName(System.String value){
            Model.TypeName = value;
            return this;
        }

        public PropertyDefinitionBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public PropertyDefinitionBuilder WithVisibility(fifth.metamodel.metadata.il.ILVisibility value){
            Model.Visibility = value;
            return this;
        }

        public PropertyDefinitionBuilder WithOwningClass(fifth.metamodel.metadata.il.ClassDefinition value){
            Model.OwningClass = value;
            return this;
        }

    }
    public partial class VariableAssignmentStatementBuilder : BaseBuilder<VariableAssignmentStatementBuilder,fifth.metamodel.metadata.il.VariableAssignmentStatement>
    {
        public VariableAssignmentStatementBuilder()
        {
            Model = new();
        }

        public VariableAssignmentStatementBuilder WithOrdinal(System.Int32? value){
            Model.Ordinal = value;
            return this;
        }

        public VariableAssignmentStatementBuilder WithLHS(System.String value){
            Model.LHS = value;
            return this;
        }

        public VariableAssignmentStatementBuilder WithRHS(fifth.metamodel.metadata.il.Expression value){
            Model.RHS = value;
            return this;
        }

    }
    public partial class VariableDeclarationStatementBuilder : BaseBuilder<VariableDeclarationStatementBuilder,fifth.metamodel.metadata.il.VariableDeclarationStatement>
    {
        public VariableDeclarationStatementBuilder()
        {
            Model = new();
        }

        public VariableDeclarationStatementBuilder WithOrdinal(System.Int32? value){
            Model.Ordinal = value;
            return this;
        }

        public VariableDeclarationStatementBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public VariableDeclarationStatementBuilder WithTypeName(System.String value){
            Model.TypeName = value;
            return this;
        }

        public VariableDeclarationStatementBuilder WithInitialisationExpression(fifth.metamodel.metadata.il.Expression value){
            Model.InitialisationExpression = value;
            return this;
        }

    }
    public partial class ReturnStatementBuilder : BaseBuilder<ReturnStatementBuilder,fifth.metamodel.metadata.il.ReturnStatement>
    {
        public ReturnStatementBuilder()
        {
            Model = new();
        }

        public ReturnStatementBuilder WithExp(fifth.metamodel.metadata.il.Expression value){
            Model.Exp = value;
            return this;
        }

    }
    public partial class VersionBuilder : BaseBuilder<VersionBuilder,fifth.metamodel.metadata.il.Version>
    {
        public VersionBuilder()
        {
            Model = new();
        }

        public VersionBuilder WithMajor(System.Int32 value){
            Model.Major = value;
            return this;
        }

        public VersionBuilder WithMinor(System.Int32? value){
            Model.Minor = value;
            return this;
        }

        public VersionBuilder WithBuild(System.Int32? value){
            Model.Build = value;
            return this;
        }

        public VersionBuilder WithPatch(System.Int32? value){
            Model.Patch = value;
            return this;
        }

    }
    public partial class WhileStatementBuilder : BaseBuilder<WhileStatementBuilder,fifth.metamodel.metadata.il.WhileStatement>
    {
        public WhileStatementBuilder()
        {
            Model = new();
        }

        public WhileStatementBuilder WithConditional(fifth.metamodel.metadata.il.Expression value){
            Model.Conditional = value;
            return this;
        }

        public WhileStatementBuilder WithLoopBlock(fifth.metamodel.metadata.il.Block value){
            Model.LoopBlock = value;
            return this;
        }

    }
    public partial class ExpressionStatementBuilder : BaseBuilder<ExpressionStatementBuilder,fifth.metamodel.metadata.il.ExpressionStatement>
    {
        public ExpressionStatementBuilder()
        {
            Model = new();
        }

        public ExpressionStatementBuilder WithExpression(fifth.metamodel.metadata.il.Expression value){
            Model.Expression = value;
            return this;
        }

    }
