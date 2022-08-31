# Parameter Declaration and Destructuring Semantics

## The Grammar

```antlr
function_declaration: name=function_name args=function_args COLON result_type=function_type body=function_body ;

function_body : block ;

function_name : identifier_chain ;

function_type : IDENTIFIER ;

function_args : OPENPAREN formal_parameters? CLOSEPAREN ;

formal_parameters : paramdecl (COMMA paramdecl)* ;

paramdecl : param_name COLON param_type ( variable_constraint?  | destructuring_decl )?  ;

param_name : IDENTIFIER ;

param_type : identifier_chain ;

destructuring_decl : OPENBRACE bindings+=destructure_binding ( COMMA bindings+=destructure_binding )* CLOSEBRACE ;

destructure_binding : param_name COLON param_name destructuring_decl?  ;
```

## The AST

```csharp
/*FunctionDefinition*/new AstNodeSpec()
{
    Name = "FunctionDefinition",
    Parent = "ScopeAstNode, IFunctionDefinition",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "ParameterDeclarations", type: "ParameterDeclarationList", isCollection: false, ignoreDuringVisit: false),
        new PropertySpec(name: "Body", type: "Block", isCollection: false, ignoreDuringVisit: false),
        new PropertySpec(name: "Typename", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "IsEntryPoint", type: "bool", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "ReturnType", type: "TypeId", isCollection: false, ignoreDuringVisit: true)
    }
},
/*ParameterDeclarationList*/new AstNodeSpec()
{
    Name = "ParameterDeclarationList",
    Parent = "AstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "ParameterDeclarations", type: "ParameterDeclaration", isCollection: true, ignoreDuringVisit: false, interfaceName: "IParameterListItem")
    }
},        
/*ParameterDeclaration*/new AstNodeSpec()
{
    Name = "ParameterDeclaration",
    Parent = "TypedAstNode, IParameterListItem",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "ParameterName", type: "Identifier"),
        new PropertySpec(name: "TypeName", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "Constraint", type: "Expression"),
        new PropertySpec(name: "DestructuringDecl", type: "DestructuringDeclaration")
    }
},
/*DestructuringDeclaration*/new AstNodeSpec()
{
    Name = "DestructuringDeclaration",
    Parent = "AstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Bindings", type: "DestructuringBinding", isCollection: true, ignoreDuringVisit: false)
    }
},
/*DestructuringBinding*/new AstNodeSpec()
{
    Name = "DestructuringBinding",
    Parent = "AstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Varname", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "Propname", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "DestructuringDecl", type: "DestructuringDeclaration")
    }
},
```

## Code Sample

```csharp
class VitalStatistics {
    Height: float;
    Age: float;
    Mass: float;    
}

class Person {
    Name: string;
    Vitals: VitalStatistics;
}

calculate_bmi(
    p: Person {
        name : Name,
        v : Vitals {
            age: Age | age > 60,
            height: Height ,
            mass: Mass
        }
    }) : float {
    print ("calculating the BMI for " + name);
    return mass / (height * height);
}

 main():void {
    eric: Person = new Person{
        Name = 'Eric Morecombe',
        Vitals = new VitalStatistics{
            Height = 1.93,
            Age = 65,
            Mass = 100
        }
    };
    print(calculate_bmi(eric));
}
```

## Discussion

Both the grammar and the AST are recursively defined.  The same applies to the
destructuring parama decl itself.

The resolution of variables, properties and parameters presumably requires the
execution of a recursive operation in reverse order - working back up the AST
hierarchy.

### Worked Example

To resolve, and desugar the bindings in the parameter `p` of function `calculate_bmi` you must:

1. starting at outermost parameter `p`, resolve it's type - found type `Person`.
2. `Person` is the scope/context in which property definitions will take place.
3. Now move on to the `DestructuringDeclaration` of the parameter, enumerate the bindings
4. The first is `name` which is bound to property `Name` which is of type `string`.  
   So, `name` can be converted into the following statement:
   `name: string = p.Name;`
5. Next we come to `v` which is bound to `Vitals` of type `VitalStatistics`.
   So, `v` becomes a similar statement:
   `v: VitalStatistics = p.Vitals;`
6. Binding `v` has it's own `DestructuringDeclaration`, so we need to drill into
   that.  The current binding is of type `VitalStatistics` so that becomes the
   naming scope for property bindings going forward.  The scope for declaration
   statements becomes `v`.  Enumerate the bindings of `v`.
7. First we have `age` bound to `Age` of type `float`.  It will translate into a
   statement of the form.  

   ```csharp
   age: float = v.Age;
   if(!(age > 60)) throw new FunctionBindingConstraintViolation("age > 60");
   ```

8. Next we have `height`, bound to `Height` of type `float`.
   `height: float = v.Height;`
9. Finally, we reach `mass`, bound to `Mass` of type `float`.
   `mass: float = v.Mass;`

So, the end result of the operation is:

```csharp
calculate_bmi(p: Person) : float 
{
    name: string = p.Name;
    v: VitalStatistics = p.Vitals;
    age: float = v.Age;
    if(!(age > 60)) 
        throw new FunctionBindingConstraintViolation("age > 60");
    height: float = v.Height;
    mass: float = v.Mass;
    print ("calculating the BMI for " + name);
    return mass / (height * height);
}
```

At this point there is no destructuring and there are just a few variable
definitions and assignments.  The achieve this simplification, you need to have
the following:

1. A Stack of the naming and variable scope being resolved against
2. A list of statements to prepend to the body of the function.

