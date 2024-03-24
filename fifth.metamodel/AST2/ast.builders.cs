namespace Fifth.Metamodel.AST2;

using System;
using System.Collections.Generic;
using fifth.metamodel.metadata.il;

    public partial class AssemblyDefBuilder : BaseBuilder<AssemblyDefBuilder,fifth.metamodel.metadata.AST.AssemblyDef>
    {
        public AssemblyDefBuilder()
        {
            Model = new();
        }

        public AssemblyDefBuilder WithPublicKeyToken(System.String value){
            Model.PublicKeyToken = value;
            return this;
        }

        public AssemblyDefBuilder WithVersion(System.String value){
            Model.Version = value;
            return this;
        }

        public AssemblyDefBuilder WithAssemblyRefs(LinkedList<fifth.metamodel.metadata.AST.AssemblyRef> value){
            Model.AssemblyRefs = value;
            return this;
        }

        public AssemblyDefBuilder AddingItemToAssemblyRefs(fifth.metamodel.metadata.AST.AssemblyRef value){
            if(Model.AssemblyRefs is null)
                Model.AssemblyRefs = [];
            Model.AssemblyRefs.AddLast(value);
            return this;
        }
        public AssemblyDefBuilder WithClassDefs(LinkedList<fifth.metamodel.metadata.AST.ClassDef> value){
            Model.ClassDefs = value;
            return this;
        }

        public AssemblyDefBuilder AddingItemToClassDefs(fifth.metamodel.metadata.AST.ClassDef value){
            if(Model.ClassDefs is null)
                Model.ClassDefs = [];
            Model.ClassDefs.AddLast(value);
            return this;
        }
        public AssemblyDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public AssemblyDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public AssemblyDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class AssemblyRefBuilder : BaseBuilder<AssemblyRefBuilder,fifth.metamodel.metadata.AST.AssemblyRef>
    {
        public AssemblyRefBuilder()
        {
            Model = new();
        }

        public AssemblyRefBuilder WithPublicKeyToken(System.String value){
            Model.PublicKeyToken = value;
            return this;
        }

        public AssemblyRefBuilder WithVersion(System.String value){
            Model.Version = value;
            return this;
        }

        public AssemblyRefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public AssemblyRefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public AssemblyRefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class AssertionStmtBuilder : BaseBuilder<AssertionStmtBuilder,fifth.metamodel.metadata.AST.AssertionStmt>
    {
        public AssertionStmtBuilder()
        {
            Model = new();
        }

        public AssertionStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public AssertionStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public AssertionStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class AssignmentStmtBuilder : BaseBuilder<AssignmentStmtBuilder,fifth.metamodel.metadata.AST.AssignmentStmt>
    {
        public AssignmentStmtBuilder()
        {
            Model = new();
        }

        public AssignmentStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public AssignmentStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public AssignmentStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class AtomBuilder : BaseBuilder<AtomBuilder,fifth.metamodel.metadata.AST.Atom>
    {
        public AtomBuilder()
        {
            Model = new();
        }

        public AtomBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public AtomBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public AtomBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class BinaryExpBuilder : BaseBuilder<BinaryExpBuilder,fifth.metamodel.metadata.AST.BinaryExp>
    {
        public BinaryExpBuilder()
        {
            Model = new();
        }

        public BinaryExpBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public BinaryExpBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public BinaryExpBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class BlockStmtBuilder : BaseBuilder<BlockStmtBuilder,fifth.metamodel.metadata.AST.BlockStmt>
    {
        public BlockStmtBuilder()
        {
            Model = new();
        }

        public BlockStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public BlockStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public BlockStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class CastExpBuilder : BaseBuilder<CastExpBuilder,fifth.metamodel.metadata.AST.CastExp>
    {
        public CastExpBuilder()
        {
            Model = new();
        }

        public CastExpBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public CastExpBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public CastExpBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class ClassDefBuilder : BaseBuilder<ClassDefBuilder,fifth.metamodel.metadata.AST.ClassDef>
    {
        public ClassDefBuilder()
        {
            Model = new();
        }

        public ClassDefBuilder WithMemberDefs(LinkedList<fifth.metamodel.metadata.AST.MemberDef> value){
            Model.MemberDefs = value;
            return this;
        }

        public ClassDefBuilder AddingItemToMemberDefs(fifth.metamodel.metadata.AST.MemberDef value){
            if(Model.MemberDefs is null)
                Model.MemberDefs = [];
            Model.MemberDefs.AddLast(value);
            return this;
        }
        public ClassDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public ClassDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public ClassDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class ExpStmtBuilder : BaseBuilder<ExpStmtBuilder,fifth.metamodel.metadata.AST.ExpStmt>
    {
        public ExpStmtBuilder()
        {
            Model = new();
        }

        public ExpStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public ExpStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public ExpStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class FieldDefBuilder : BaseBuilder<FieldDefBuilder,fifth.metamodel.metadata.AST.FieldDef>
    {
        public FieldDefBuilder()
        {
            Model = new();
        }

        public FieldDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public FieldDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public FieldDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class ForeachStmtBuilder : BaseBuilder<ForeachStmtBuilder,fifth.metamodel.metadata.AST.ForeachStmt>
    {
        public ForeachStmtBuilder()
        {
            Model = new();
        }

        public ForeachStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public ForeachStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public ForeachStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class ForStmtBuilder : BaseBuilder<ForStmtBuilder,fifth.metamodel.metadata.AST.ForStmt>
    {
        public ForStmtBuilder()
        {
            Model = new();
        }

        public ForStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public ForStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public ForStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class FuncCallExpBuilder : BaseBuilder<FuncCallExpBuilder,fifth.metamodel.metadata.AST.FuncCallExp>
    {
        public FuncCallExpBuilder()
        {
            Model = new();
        }

        public FuncCallExpBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public FuncCallExpBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public FuncCallExpBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class GraphBuilder : BaseBuilder<GraphBuilder,fifth.metamodel.metadata.AST.Graph>
    {
        public GraphBuilder()
        {
            Model = new();
        }

        public GraphBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public GraphBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public GraphBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class GraphNamespaceAliasBuilder : BaseBuilder<GraphNamespaceAliasBuilder,fifth.metamodel.metadata.AST.GraphNamespaceAlias>
    {
        public GraphNamespaceAliasBuilder()
        {
            Model = new();
        }

        public GraphNamespaceAliasBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public GraphNamespaceAliasBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public GraphNamespaceAliasBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class GuardStmtBuilder : BaseBuilder<GuardStmtBuilder,fifth.metamodel.metadata.AST.GuardStmt>
    {
        public GuardStmtBuilder()
        {
            Model = new();
        }

        public GuardStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public GuardStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public GuardStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class IfElseStmtBuilder : BaseBuilder<IfElseStmtBuilder,fifth.metamodel.metadata.AST.IfElseStmt>
    {
        public IfElseStmtBuilder()
        {
            Model = new();
        }

        public IfElseStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public IfElseStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public IfElseStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class InferenceRuleDefBuilder : BaseBuilder<InferenceRuleDefBuilder,fifth.metamodel.metadata.AST.InferenceRuleDef>
    {
        public InferenceRuleDefBuilder()
        {
            Model = new();
        }

        public InferenceRuleDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public InferenceRuleDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public InferenceRuleDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class LambdaDefBuilder : BaseBuilder<LambdaDefBuilder,fifth.metamodel.metadata.AST.LambdaDef>
    {
        public LambdaDefBuilder()
        {
            Model = new();
        }

        public LambdaDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public LambdaDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public LambdaDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class ListBuilder : BaseBuilder<ListBuilder,fifth.metamodel.metadata.AST.List>
    {
        public ListBuilder()
        {
            Model = new();
        }

        public ListBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public ListBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public ListBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class LiteralExpBuilder : BaseBuilder<LiteralExpBuilder,fifth.metamodel.metadata.AST.LiteralExp>
    {
        public LiteralExpBuilder()
        {
            Model = new();
        }

        public LiteralExpBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public LiteralExpBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public LiteralExpBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class MemberAccessExpBuilder : BaseBuilder<MemberAccessExpBuilder,fifth.metamodel.metadata.AST.MemberAccessExp>
    {
        public MemberAccessExpBuilder()
        {
            Model = new();
        }

        public MemberAccessExpBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public MemberAccessExpBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public MemberAccessExpBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class MemberDefBuilder : BaseBuilder<MemberDefBuilder,fifth.metamodel.metadata.AST.MemberDef>
    {
        public MemberDefBuilder()
        {
            Model = new();
        }

        public MemberDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public MemberDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public MemberDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class MemberRefBuilder : BaseBuilder<MemberRefBuilder,fifth.metamodel.metadata.AST.MemberRef>
    {
        public MemberRefBuilder()
        {
            Model = new();
        }

        public MemberRefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public MemberRefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public MemberRefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class MethodDefBuilder : BaseBuilder<MethodDefBuilder,fifth.metamodel.metadata.AST.MethodDef>
    {
        public MethodDefBuilder()
        {
            Model = new();
        }

        public MethodDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public MethodDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public MethodDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class ObjectInstantiationExpBuilder : BaseBuilder<ObjectInstantiationExpBuilder,fifth.metamodel.metadata.AST.ObjectInstantiationExp>
    {
        public ObjectInstantiationExpBuilder()
        {
            Model = new();
        }

        public ObjectInstantiationExpBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public ObjectInstantiationExpBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public ObjectInstantiationExpBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class ParamDefBuilder : BaseBuilder<ParamDefBuilder,fifth.metamodel.metadata.AST.ParamDef>
    {
        public ParamDefBuilder()
        {
            Model = new();
        }

        public ParamDefBuilder WithParameterConstraint(fifth.metamodel.metadata.AST.Expression value){
            Model.ParameterConstraint = value;
            return this;
        }

        public ParamDefBuilder WithDestructureDef(fifth.metamodel.metadata.AST.ParamDestructureDef value){
            Model.DestructureDef = value;
            return this;
        }

        public ParamDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public ParamDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public ParamDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class ParamDestructureDefBuilder : BaseBuilder<ParamDestructureDefBuilder,fifth.metamodel.metadata.AST.ParamDestructureDef>
    {
        public ParamDestructureDefBuilder()
        {
            Model = new();
        }

        public ParamDestructureDefBuilder WithBindings(LinkedList<fifth.metamodel.metadata.AST.PropertyBindingDef> value){
            Model.Bindings = value;
            return this;
        }

        public ParamDestructureDefBuilder AddingItemToBindings(fifth.metamodel.metadata.AST.PropertyBindingDef value){
            if(Model.Bindings is null)
                Model.Bindings = [];
            Model.Bindings.AddLast(value);
            return this;
        }
        public ParamDestructureDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public ParamDestructureDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public ParamDestructureDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class PropertyBindingDefBuilder : BaseBuilder<PropertyBindingDefBuilder,fifth.metamodel.metadata.AST.PropertyBindingDef>
    {
        public PropertyBindingDefBuilder()
        {
            Model = new();
        }

        public PropertyBindingDefBuilder WithIntroducedVariable(fifth.metamodel.metadata.AST.VariableDecl value){
            Model.IntroducedVariable = value;
            return this;
        }

        public PropertyBindingDefBuilder WithReferencedProperty(fifth.metamodel.metadata.AST.PropertyDef value){
            Model.ReferencedProperty = value;
            return this;
        }

        public PropertyBindingDefBuilder WithDestructureDef(fifth.metamodel.metadata.AST.ParamDestructureDef value){
            Model.DestructureDef = value;
            return this;
        }

        public PropertyBindingDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public PropertyBindingDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public PropertyBindingDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class PropertyDefBuilder : BaseBuilder<PropertyDefBuilder,fifth.metamodel.metadata.AST.PropertyDef>
    {
        public PropertyDefBuilder()
        {
            Model = new();
        }

        public PropertyDefBuilder WithBackingField(fifth.metamodel.metadata.AST.FieldDef value){
            Model.BackingField = value;
            return this;
        }

        public PropertyDefBuilder WithGetter(fifth.metamodel.metadata.AST.MethodDef value){
            Model.Getter = value;
            return this;
        }

        public PropertyDefBuilder WithSetter(fifth.metamodel.metadata.AST.MethodDef value){
            Model.Setter = value;
            return this;
        }

        public PropertyDefBuilder WithCtorOnlySetter(System.Boolean value){
            Model.CtorOnlySetter = value;
            return this;
        }

        public PropertyDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public PropertyDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public PropertyDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class RetractionStmtBuilder : BaseBuilder<RetractionStmtBuilder,fifth.metamodel.metadata.AST.RetractionStmt>
    {
        public RetractionStmtBuilder()
        {
            Model = new();
        }

        public RetractionStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public RetractionStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public RetractionStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class ReturnStmtBuilder : BaseBuilder<ReturnStmtBuilder,fifth.metamodel.metadata.AST.ReturnStmt>
    {
        public ReturnStmtBuilder()
        {
            Model = new();
        }

        public ReturnStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public ReturnStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public ReturnStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class StructDefBuilder : BaseBuilder<StructDefBuilder,fifth.metamodel.metadata.AST.StructDef>
    {
        public StructDefBuilder()
        {
            Model = new();
        }

        public StructDefBuilder WithMemberDefs(LinkedList<fifth.metamodel.metadata.AST.MemberDef> value){
            Model.MemberDefs = value;
            return this;
        }

        public StructDefBuilder AddingItemToMemberDefs(fifth.metamodel.metadata.AST.MemberDef value){
            if(Model.MemberDefs is null)
                Model.MemberDefs = [];
            Model.MemberDefs.AddLast(value);
            return this;
        }
        public StructDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public StructDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public StructDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class TripleBuilder : BaseBuilder<TripleBuilder,fifth.metamodel.metadata.AST.Triple>
    {
        public TripleBuilder()
        {
            Model = new();
        }

        public TripleBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public TripleBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public TripleBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class TypeDefBuilder : BaseBuilder<TypeDefBuilder,fifth.metamodel.metadata.AST.TypeDef>
    {
        public TypeDefBuilder()
        {
            Model = new();
        }

        public TypeDefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public TypeDefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public TypeDefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class TypeRefBuilder : BaseBuilder<TypeRefBuilder,fifth.metamodel.metadata.AST.TypeRef>
    {
        public TypeRefBuilder()
        {
            Model = new();
        }

        public TypeRefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public TypeRefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public TypeRefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class UnaryExpBuilder : BaseBuilder<UnaryExpBuilder,fifth.metamodel.metadata.AST.UnaryExp>
    {
        public UnaryExpBuilder()
        {
            Model = new();
        }

        public UnaryExpBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public UnaryExpBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public UnaryExpBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class VarDeclStmtBuilder : BaseBuilder<VarDeclStmtBuilder,fifth.metamodel.metadata.AST.VarDeclStmt>
    {
        public VarDeclStmtBuilder()
        {
            Model = new();
        }

        public VarDeclStmtBuilder WithVariableDecl(fifth.metamodel.metadata.AST.VariableDecl value){
            Model.VariableDecl = value;
            return this;
        }

        public VarDeclStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public VarDeclStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public VarDeclStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class VariableDeclBuilder : BaseBuilder<VariableDeclBuilder,fifth.metamodel.metadata.AST.VariableDecl>
    {
        public VariableDeclBuilder()
        {
            Model = new();
        }

        public VariableDeclBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public VariableDeclBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public VariableDeclBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class VarRefBuilder : BaseBuilder<VarRefBuilder,fifth.metamodel.metadata.AST.VarRef>
    {
        public VarRefBuilder()
        {
            Model = new();
        }

        public VarRefBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public VarRefBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public VarRefBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class VarRefExpBuilder : BaseBuilder<VarRefExpBuilder,fifth.metamodel.metadata.AST.VarRefExp>
    {
        public VarRefExpBuilder()
        {
            Model = new();
        }

        public VarRefExpBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public VarRefExpBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public VarRefExpBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class WhileStmtBuilder : BaseBuilder<WhileStmtBuilder,fifth.metamodel.metadata.AST.WhileStmt>
    {
        public WhileStmtBuilder()
        {
            Model = new();
        }

        public WhileStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public WhileStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public WhileStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
    public partial class WithScopeStmtBuilder : BaseBuilder<WithScopeStmtBuilder,fifth.metamodel.metadata.AST.WithScopeStmt>
    {
        public WithScopeStmtBuilder()
        {
            Model = new();
        }

        public WithScopeStmtBuilder WithType(fifth.metamodel.metadata.AST.TypeMetadata value){
            Model.Type = value;
            return this;
        }

        public WithScopeStmtBuilder WithName(System.String value){
            Model.Name = value;
            return this;
        }

        public WithScopeStmtBuilder WithParent(fifth.metamodel.metadata.AST.AstThing value){
            Model.Parent = value;
            return this;
        }

    }
