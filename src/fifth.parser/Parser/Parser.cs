using fifth.Parser.AST;
using fifth.Parser.AST.Builders;


using System;

namespace Deleteme {

public class Parser {
	public const int _EOF = 0;
	public const int _ident = 1;
	public const int _use = 2;
	public const int _string = 3;
	public const int _float = 4;
	public const int _int = 5;
	public const int _endexpr = 6;
	public const int _sepexpr = 7;
	public const int maxT = 11;

	const bool _T = true;
	const bool _x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;

public ProgramBuilder astBuilder;
	FunctionBuilder funcBuilder;
	ParameterListBuilder parameterListBuilder;
	ExpressionListBuilder expressionListBuilder;
	ParameterDeclarationBuilder parameterDeclarationBuilder;

/*--------------------------------------------------------------------------*/


	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
	}

	void SynErr (int n) {
		if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public void SemErr (string msg) {
		if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
		errDist = 0;
	}
	
	void Get () {
		for (;;) {
			t = la;
			la = scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = t;
		}
	}
	
	void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	void ExpectWeak (int n, int follow) {
		if (la.kind == n) Get();
		else {
			SynErr(n);
			while (!StartOf(follow)) Get();
		}
	}


	bool WeakSeparator(int n, int syFol, int repFol) {
		int kind = la.kind;
		if (kind == n) {Get(); return true;}
		else if (StartOf(repFol)) {return false;}
		else {
			SynErr(n);
			while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind])) {
				Get();
				kind = la.kind;
			}
			return StartOf(syFol);
		}
	}

	
	void Ident(out string name) {
		Expect(1);
		name = t.val; 
	}

	void Int(out int value) {
		Expect(5);
		value = Int32.Parse(t.val); 
	}

	void String(out string value) {
		Expect(3);
		value = t.val; 
	}

	void Float(out float value) {
		Expect(4);
		value = Single.Parse(t.val); 
	}

	void Expression(out Expression expression) {
		string stringValue; 
		int intValue; 
		float floatValue; 
		expression = null;
		
		if (la.kind == 5) {
			Int(out intValue);
			expression = new LiteralExpression<int>(intValue); 
		} else if (la.kind == 4) {
			Float(out floatValue);
			expression = new LiteralExpression<float>(floatValue); 
		} else if (la.kind == 3) {
			String(out stringValue);
			expression = new LiteralExpression<string>(stringValue); 
		} else SynErr(12);
	}

	void ExpressionList() {
		Expression expression;
		expressionListBuilder = ExpressionListBuilder.Start(); 
		
		Expression(out expression);
		expressionListBuilder.WithExpression(expression); 
		while (la.kind == 7) {
			Get();
			ExpressionList();
		}
	}

	void ModuleName() {
		Expect(1);
	}

	void TypeImport() {
		Expect(2);
		ModuleName();
		Expect(6);
	}

	void TypeImports() {
		TypeImport();
		while (la.kind == 2) {
			TypeImport();
		}
	}

	void ParameterDeclaration() {
		string typeName, paramName; 
		parameterDeclarationBuilder = ParameterDeclarationBuilder.Start(); 
		
		Ident(out typeName);
		parameterDeclarationBuilder.WithTypeName(typeName); 
		Ident(out paramName);
		parameterDeclarationBuilder.WithName(paramName); 
	}

	void ParameterDeclarations() {
		ParameterDeclaration();
		parameterListBuilder.WithParameter(parameterDeclarationBuilder.Build()); 
		while (la.kind == 7) {
			Get();
			ParameterDeclarations();
		}
	}

	void ParameterDeclarationList() {
		parameterListBuilder = ParameterListBuilder.Start(); 
		Expect(8);
		if (la.kind == 1) {
			ParameterDeclarations();
		}
		Expect(9);
	}

	void FunctionName() {
		string funcName; 
		Ident(out funcName);
		funcBuilder.WithName(funcName);
	}

	void FunctionDefinition() {
		funcBuilder = FunctionBuilder.Start(); 
		FunctionName();
		ParameterDeclarationList();
		funcBuilder.WithParameters( parameterListBuilder.Build() );
		Expect(10);
		ExpressionList();
		Expect(6);
	}

	void FunctionDefinitions() {
		FunctionDefinition();
		astBuilder.WithFunction(funcBuilder.Build()); 
		while (la.kind == 1) {
			FunctionDefinitions();
		}
	}

	void Fifth() {
		astBuilder = ProgramBuilder.Start(); 
		TypeImports();
		FunctionDefinitions();
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		Fifth();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x}

	};
} // end Parser


public class Errors {
	public int count = 0;                                    // number of errors detected
	public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
	public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text

	public virtual void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "ident expected"; break;
			case 2: s = "use expected"; break;
			case 3: s = "string expected"; break;
			case 4: s = "float expected"; break;
			case 5: s = "int expected"; break;
			case 6: s = "endexpr expected"; break;
			case 7: s = "sepexpr expected"; break;
			case 8: s = "\"(\" expected"; break;
			case 9: s = "\")\" expected"; break;
			case 10: s = "\"=>\" expected"; break;
			case 11: s = "??? expected"; break;
			case 12: s = "invalid Expression"; break;

			default: s = "error " + n; break;
		}
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}

	public virtual void SemErr (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}
	
	public virtual void SemErr (string s) {
		errorStream.WriteLine(s);
		count++;
	}
	
	public virtual void Warning (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
	}
	
	public virtual void Warning(string s) {
		errorStream.WriteLine(s);
	}
} // Errors


public class FatalError: Exception {
	public FatalError(string m): base(m) {}
}


}
