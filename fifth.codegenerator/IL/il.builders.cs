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

        public AssemblyDeclarationBuilder WithPrimeModule(fifth.metamodel.metadata.il.ModuleDeclaration value){
            Model.PrimeModule = value;
            return this;
        }

        public AssemblyDeclarationBuilder WithAssemblyReferences(List<fifth.metamodel.metadata.il.AssemblyReference> value){
            Model.AssemblyReferences = value;
            return this;
        }

        public AssemblyDeclarationBuilder AddingItemToAssemblyReferences(fifth.metamodel.metadata.il.AssemblyReference value){
            Model.AssemblyReferences.Add(value);
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
    public partial class BoolLiteralBuilder : BaseBuilder<BoolLiteralBuilder,fifth.metamodel.metadata.il.BoolLiteral>
    {
        public BoolLiteralBuilder()
        {
            Model = new();
        }

        public BoolLiteralBuilder WithValue(System.Boolean value){
            Model.Value = value;
            return this;
        }

    }
    public partial class ByteLiteralBuilder : BaseBuilder<ByteLiteralBuilder,fifth.metamodel.metadata.il.ByteLiteral>
    {
        public ByteLiteralBuilder()
        {
            Model = new();
        }

        public ByteLiteralBuilder WithValue(System.Byte value){
            Model.Value = value;
            return this;
        }

    }
    public partial class CharLiteralBuilder : BaseBuilder<CharLiteralBuilder,fifth.metamodel.metadata.il.CharLiteral>
    {
        public CharLiteralBuilder()
        {
            Model = new();
        }

        public CharLiteralBuilder WithValue(System.Char value){
            Model.Value = value;
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

        public ClassDefinitionBuilder WithNamespace(System.String value){
            Model.Namespace = value;
            return this;
        }

        public ClassDefinitionBuilder WithBaseClasses(List<fifth.metamodel.metadata.il.ClassDefinition> value){
            Model.BaseClasses = value;
            return this;
        }

        public ClassDefinitionBuilder AddingItemToBaseClasses(fifth.metamodel.metadata.il.ClassDefinition value){
            Model.BaseClasses.Add(value);
            return this;
        }
        public ClassDefinitionBuilder WithParentAssembly(fifth.metamodel.metadata.il.AssemblyDeclaration value){
            Model.ParentAssembly = value;
            return this;
        }

        public ClassDefinitionBuilder WithVisibility(fifth.metamodel.metadata.il.MemberAccessability value){
            Model.Visibility = value;
            return this;
        }

    }
    public partial class DateOnlyLiteralBuilder : BaseBuilder<DateOnlyLiteralBuilder,fifth.metamodel.metadata.il.DateOnlyLiteral>
    {
        public DateOnlyLiteralBuilder()
        {
            Model = new();
        }

        public DateOnlyLiteralBuilder WithValue(System.DateOnly value){
            Model.Value = value;
            return this;
        }

    }
    public partial class DateTimeOffsetLiteralBuilder : BaseBuilder<DateTimeOffsetLiteralBuilder,fifth.metamodel.metadata.il.DateTimeOffsetLiteral>
    {
        public DateTimeOffsetLiteralBuilder()
        {
            Model = new();
        }

        public DateTimeOffsetLiteralBuilder WithValue(System.DateTimeOffset value){
            Model.Value = value;
            return this;
        }

    }
    public partial class DecimalLiteralBuilder : BaseBuilder<DecimalLiteralBuilder,fifth.metamodel.metadata.il.DecimalLiteral>
    {
        public DecimalLiteralBuilder()
        {
            Model = new();
        }

        public DecimalLiteralBuilder WithValue(System.Decimal value){
            Model.Value = value;
            return this;
        }

    }
    public partial class DoubleLiteralBuilder : BaseBuilder<DoubleLiteralBuilder,fifth.metamodel.metadata.il.DoubleLiteral>
    {
        public DoubleLiteralBuilder()
        {
            Model = new();
        }

        public DoubleLiteralBuilder WithValue(System.Double value){
            Model.Value = value;
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
    public partial class FieldDefinitionBuilder : BaseBuilder<FieldDefinitionBuilder,fifth.metamodel.metadata.il.FieldDefinition>
    {
        public FieldDefinitionBuilder()
        {
            Model = new();
        }

        public FieldDefinitionBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public FieldDefinitionBuilder WithTheType(fifth.metamodel.metadata.il.TypeReference value){
            Model.TheType = value;
            return this;
        }

        public FieldDefinitionBuilder WithParentClass(fifth.metamodel.metadata.il.ClassDefinition value){
            Model.ParentClass = value;
            return this;
        }

        public FieldDefinitionBuilder WithAssociatedProperty(fifth.metamodel.metadata.il.PropertyDefinition value){
            Model.AssociatedProperty = value;
            return this;
        }

        public FieldDefinitionBuilder WithTypeOfMember(fifth.metamodel.metadata.il.MemberType value){
            Model.TypeOfMember = value;
            return this;
        }

        public FieldDefinitionBuilder WithIsStatic(System.Boolean value){
            Model.IsStatic = value;
            return this;
        }

        public FieldDefinitionBuilder WithIsFinal(System.Boolean value){
            Model.IsFinal = value;
            return this;
        }

        public FieldDefinitionBuilder WithIsVirtual(System.Boolean value){
            Model.IsVirtual = value;
            return this;
        }

        public FieldDefinitionBuilder WithIsStrict(System.Boolean value){
            Model.IsStrict = value;
            return this;
        }

        public FieldDefinitionBuilder WithIsAbstract(System.Boolean value){
            Model.IsAbstract = value;
            return this;
        }

        public FieldDefinitionBuilder WithIsSpecialName(System.Boolean value){
            Model.IsSpecialName = value;
            return this;
        }

        public FieldDefinitionBuilder WithHideBySig(System.Boolean value){
            Model.HideBySig = value;
            return this;
        }

        public FieldDefinitionBuilder WithVisibility(fifth.metamodel.metadata.il.MemberAccessability value){
            Model.Visibility = value;
            return this;
        }

    }
    public partial class FloatLiteralBuilder : BaseBuilder<FloatLiteralBuilder,fifth.metamodel.metadata.il.FloatLiteral>
    {
        public FloatLiteralBuilder()
        {
            Model = new();
        }

        public FloatLiteralBuilder WithValue(System.Single value){
            Model.Value = value;
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
    public partial class IntLiteralBuilder : BaseBuilder<IntLiteralBuilder,fifth.metamodel.metadata.il.IntLiteral>
    {
        public IntLiteralBuilder()
        {
            Model = new();
        }

        public IntLiteralBuilder WithValue(System.Int32 value){
            Model.Value = value;
            return this;
        }

    }
    public partial class LongLiteralBuilder : BaseBuilder<LongLiteralBuilder,fifth.metamodel.metadata.il.LongLiteral>
    {
        public LongLiteralBuilder()
        {
            Model = new();
        }

        public LongLiteralBuilder WithValue(System.Int64 value){
            Model.Value = value;
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
    public partial class MemberRefBuilder : BaseBuilder<MemberRefBuilder,fifth.metamodel.metadata.il.MemberRef>
    {
        public MemberRefBuilder()
        {
            Model = new();
        }

        public MemberRefBuilder WithTarget(fifth.metamodel.metadata.il.MemberTarget value){
            Model.Target = value;
            return this;
        }

        public MemberRefBuilder WithClassDefinition(fifth.metamodel.metadata.il.ClassDefinition value){
            Model.ClassDefinition = value;
            return this;
        }

        public MemberRefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public MemberRefBuilder WithSig(fifth.metamodel.metadata.il.MethodSignature value){
            Model.Sig = value;
            return this;
        }

        public MemberRefBuilder WithField(fifth.metamodel.metadata.il.FieldDefinition value){
            Model.Field = value;
            return this;
        }

    }
    public partial class MethodDefinitionBuilder : BaseBuilder<MethodDefinitionBuilder,fifth.metamodel.metadata.il.MethodDefinition>
    {
        public MethodDefinitionBuilder()
        {
            Model = new();
        }

        public MethodDefinitionBuilder WithHeader(fifth.metamodel.metadata.il.MethodHeader value){
            Model.Header = value;
            return this;
        }

        public MethodDefinitionBuilder WithSignature(fifth.metamodel.metadata.il.MethodSignature value){
            Model.Signature = value;
            return this;
        }

        public MethodDefinitionBuilder WithImpl(fifth.metamodel.metadata.il.MethodImpl value){
            Model.Impl = value;
            return this;
        }

        public MethodDefinitionBuilder WithCodeTypeFlags(fifth.metamodel.metadata.il.CodeTypeFlag value){
            Model.CodeTypeFlags = value;
            return this;
        }

        public MethodDefinitionBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public MethodDefinitionBuilder WithTheType(fifth.metamodel.metadata.il.TypeReference value){
            Model.TheType = value;
            return this;
        }

        public MethodDefinitionBuilder WithParentClass(fifth.metamodel.metadata.il.ClassDefinition value){
            Model.ParentClass = value;
            return this;
        }

        public MethodDefinitionBuilder WithAssociatedProperty(fifth.metamodel.metadata.il.PropertyDefinition value){
            Model.AssociatedProperty = value;
            return this;
        }

        public MethodDefinitionBuilder WithTypeOfMember(fifth.metamodel.metadata.il.MemberType value){
            Model.TypeOfMember = value;
            return this;
        }

        public MethodDefinitionBuilder WithIsStatic(System.Boolean value){
            Model.IsStatic = value;
            return this;
        }

        public MethodDefinitionBuilder WithIsFinal(System.Boolean value){
            Model.IsFinal = value;
            return this;
        }

        public MethodDefinitionBuilder WithIsVirtual(System.Boolean value){
            Model.IsVirtual = value;
            return this;
        }

        public MethodDefinitionBuilder WithIsStrict(System.Boolean value){
            Model.IsStrict = value;
            return this;
        }

        public MethodDefinitionBuilder WithIsAbstract(System.Boolean value){
            Model.IsAbstract = value;
            return this;
        }

        public MethodDefinitionBuilder WithIsSpecialName(System.Boolean value){
            Model.IsSpecialName = value;
            return this;
        }

        public MethodDefinitionBuilder WithHideBySig(System.Boolean value){
            Model.HideBySig = value;
            return this;
        }

        public MethodDefinitionBuilder WithVisibility(fifth.metamodel.metadata.il.MemberAccessability value){
            Model.Visibility = value;
            return this;
        }

    }
    public partial class MethodHeaderBuilder : BaseBuilder<MethodHeaderBuilder,fifth.metamodel.metadata.il.MethodHeader>
    {
        public MethodHeaderBuilder()
        {
            Model = new();
        }

        public MethodHeaderBuilder WithFunctionKind(fifth.metamodel.metadata.FunctionKind value){
            Model.FunctionKind = value;
            return this;
        }

        public MethodHeaderBuilder WithIsEntrypoint(System.Boolean value){
            Model.IsEntrypoint = value;
            return this;
        }

    }
    public partial class MethodImplBuilder : BaseBuilder<MethodImplBuilder,fifth.metamodel.metadata.il.MethodImpl>
    {
        public MethodImplBuilder()
        {
            Model = new();
        }

        public MethodImplBuilder WithImplementationFlags(fifth.metamodel.metadata.il.ImplementationFlag value){
            Model.ImplementationFlags = value;
            return this;
        }

        public MethodImplBuilder WithIsManaged(System.Boolean value){
            Model.IsManaged = value;
            return this;
        }

        public MethodImplBuilder WithBody(fifth.metamodel.metadata.il.Block value){
            Model.Body = value;
            return this;
        }

    }
    public partial class MethodRefBuilder : BaseBuilder<MethodRefBuilder,fifth.metamodel.metadata.il.MethodRef>
    {
        public MethodRefBuilder()
        {
            Model = new();
        }

        public MethodRefBuilder WithTarget(fifth.metamodel.metadata.il.MemberTarget value){
            Model.Target = value;
            return this;
        }

        public MethodRefBuilder WithClassDefinition(fifth.metamodel.metadata.il.ClassDefinition value){
            Model.ClassDefinition = value;
            return this;
        }

        public MethodRefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public MethodRefBuilder WithSig(fifth.metamodel.metadata.il.MethodSignature value){
            Model.Sig = value;
            return this;
        }

        public MethodRefBuilder WithField(fifth.metamodel.metadata.il.FieldDefinition value){
            Model.Field = value;
            return this;
        }

    }
    public partial class MethodSignatureBuilder : BaseBuilder<MethodSignatureBuilder,fifth.metamodel.metadata.il.MethodSignature>
    {
        public MethodSignatureBuilder()
        {
            Model = new();
        }

        public MethodSignatureBuilder WithCallingConvention(fifth.metamodel.metadata.il.MethodCallingConvention value){
            Model.CallingConvention = value;
            return this;
        }

        public MethodSignatureBuilder WithNumberOfParameters(System.UInt16 value){
            Model.NumberOfParameters = value;
            return this;
        }

        public MethodSignatureBuilder WithParameterSignatures(List<fifth.metamodel.metadata.il.ParameterSignature> value){
            Model.ParameterSignatures = value;
            return this;
        }

        public MethodSignatureBuilder AddingItemToParameterSignatures(fifth.metamodel.metadata.il.ParameterSignature value){
            Model.ParameterSignatures.Add(value);
            return this;
        }
        public MethodSignatureBuilder WithReturnTypeSignature(fifth.metamodel.metadata.il.TypeReference value){
            Model.ReturnTypeSignature = value;
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

        public ModuleDeclarationBuilder WithClasses(List<fifth.metamodel.metadata.il.ClassDefinition> value){
            Model.Classes = value;
            return this;
        }

        public ModuleDeclarationBuilder AddingItemToClasses(fifth.metamodel.metadata.il.ClassDefinition value){
            Model.Classes.Add(value);
            return this;
        }
        public ModuleDeclarationBuilder WithFunctions(List<fifth.metamodel.metadata.il.MethodDefinition> value){
            Model.Functions = value;
            return this;
        }

        public ModuleDeclarationBuilder AddingItemToFunctions(fifth.metamodel.metadata.il.MethodDefinition value){
            Model.Functions.Add(value);
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
    public partial class ParameterSignatureBuilder : BaseBuilder<ParameterSignatureBuilder,fifth.metamodel.metadata.il.ParameterSignature>
    {
        public ParameterSignatureBuilder()
        {
            Model = new();
        }

        public ParameterSignatureBuilder WithInOut(fifth.metamodel.metadata.il.InOutFlag value){
            Model.InOut = value;
            return this;
        }

        public ParameterSignatureBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public ParameterSignatureBuilder WithTypeReference(fifth.metamodel.metadata.il.TypeReference value){
            Model.TypeReference = value;
            return this;
        }

        public ParameterSignatureBuilder WithIsUDTType(System.Boolean value){
            Model.IsUDTType = value;
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

        public PropertyDefinitionBuilder WithFieldDefinition(fifth.metamodel.metadata.il.FieldDefinition value){
            Model.FieldDefinition = value;
            return this;
        }

        public PropertyDefinitionBuilder WithTheType(fifth.metamodel.metadata.il.TypeReference value){
            Model.TheType = value;
            return this;
        }

        public PropertyDefinitionBuilder WithParentClass(fifth.metamodel.metadata.il.ClassDefinition value){
            Model.ParentClass = value;
            return this;
        }

        public PropertyDefinitionBuilder WithAssociatedProperty(fifth.metamodel.metadata.il.PropertyDefinition value){
            Model.AssociatedProperty = value;
            return this;
        }

        public PropertyDefinitionBuilder WithTypeOfMember(fifth.metamodel.metadata.il.MemberType value){
            Model.TypeOfMember = value;
            return this;
        }

        public PropertyDefinitionBuilder WithIsStatic(System.Boolean value){
            Model.IsStatic = value;
            return this;
        }

        public PropertyDefinitionBuilder WithIsFinal(System.Boolean value){
            Model.IsFinal = value;
            return this;
        }

        public PropertyDefinitionBuilder WithIsVirtual(System.Boolean value){
            Model.IsVirtual = value;
            return this;
        }

        public PropertyDefinitionBuilder WithIsStrict(System.Boolean value){
            Model.IsStrict = value;
            return this;
        }

        public PropertyDefinitionBuilder WithIsAbstract(System.Boolean value){
            Model.IsAbstract = value;
            return this;
        }

        public PropertyDefinitionBuilder WithIsSpecialName(System.Boolean value){
            Model.IsSpecialName = value;
            return this;
        }

        public PropertyDefinitionBuilder WithHideBySig(System.Boolean value){
            Model.HideBySig = value;
            return this;
        }

        public PropertyDefinitionBuilder WithVisibility(fifth.metamodel.metadata.il.MemberAccessability value){
            Model.Visibility = value;
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
    public partial class SByteLiteralBuilder : BaseBuilder<SByteLiteralBuilder,fifth.metamodel.metadata.il.SByteLiteral>
    {
        public SByteLiteralBuilder()
        {
            Model = new();
        }

        public SByteLiteralBuilder WithValue(System.SByte value){
            Model.Value = value;
            return this;
        }

    }
    public partial class ShortLiteralBuilder : BaseBuilder<ShortLiteralBuilder,fifth.metamodel.metadata.il.ShortLiteral>
    {
        public ShortLiteralBuilder()
        {
            Model = new();
        }

        public ShortLiteralBuilder WithValue(System.Int16 value){
            Model.Value = value;
            return this;
        }

    }
    public partial class StringLiteralBuilder : BaseBuilder<StringLiteralBuilder,fifth.metamodel.metadata.il.StringLiteral>
    {
        public StringLiteralBuilder()
        {
            Model = new();
        }

        public StringLiteralBuilder WithValue(System.String value){
            Model.Value = value;
            return this;
        }

    }
    public partial class TimeOnlyLiteralBuilder : BaseBuilder<TimeOnlyLiteralBuilder,fifth.metamodel.metadata.il.TimeOnlyLiteral>
    {
        public TimeOnlyLiteralBuilder()
        {
            Model = new();
        }

        public TimeOnlyLiteralBuilder WithValue(System.TimeOnly value){
            Model.Value = value;
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
    public partial class TypeReferenceBuilder : BaseBuilder<TypeReferenceBuilder,fifth.metamodel.metadata.il.TypeReference>
    {
        public TypeReferenceBuilder()
        {
            Model = new();
        }

        public TypeReferenceBuilder WithNamespace(System.String value){
            Model.Namespace = value;
            return this;
        }

        public TypeReferenceBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public TypeReferenceBuilder WithModuleName(System.String value){
            Model.ModuleName = value;
            return this;
        }

    }
    public partial class UIntLiteralBuilder : BaseBuilder<UIntLiteralBuilder,fifth.metamodel.metadata.il.UIntLiteral>
    {
        public UIntLiteralBuilder()
        {
            Model = new();
        }

        public UIntLiteralBuilder WithValue(System.UInt32 value){
            Model.Value = value;
            return this;
        }

    }
    public partial class ULongLiteralBuilder : BaseBuilder<ULongLiteralBuilder,fifth.metamodel.metadata.il.ULongLiteral>
    {
        public ULongLiteralBuilder()
        {
            Model = new();
        }

        public ULongLiteralBuilder WithValue(System.UInt64 value){
            Model.Value = value;
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
    public partial class UriLiteralBuilder : BaseBuilder<UriLiteralBuilder,fifth.metamodel.metadata.il.UriLiteral>
    {
        public UriLiteralBuilder()
        {
            Model = new();
        }

        public UriLiteralBuilder WithValue(System.Uri value){
            Model.Value = value;
            return this;
        }

    }
    public partial class UShortLiteralBuilder : BaseBuilder<UShortLiteralBuilder,fifth.metamodel.metadata.il.UShortLiteral>
    {
        public UShortLiteralBuilder()
        {
            Model = new();
        }

        public UShortLiteralBuilder WithValue(System.UInt16 value){
            Model.Value = value;
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
