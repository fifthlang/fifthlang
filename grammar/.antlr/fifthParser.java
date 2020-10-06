// Generated from /Users/aabs/dev/by-technology/dotnet/fifthlang/grammar/fifth.g4 by ANTLR 4.8
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class fifthParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.8", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		OpenParen=1, CloseParen=2, Comma=3, LambdaSep=4, Use=5, Plus=6, Minus=7, 
		Times=8, Divide=9, Percent=10, Power=11, EQ=12, NEQ=13, GT=14, LT=15, 
		GEQ=16, LEQ=17, And=18, Or=19, Semicolon=20, Identifier=21, IdStart=22, 
		IdPart=23, TimeInterval=24, Letter=25, Digit=26, PositiveDigit=27, Nat=28, 
		String=29, Float=30, Int=31, Exp=32, Ws=33, VARIABLE=34, ScientificNumber=35;
	public static final int
		RULE_fifth = 0, RULE_function_declaration = 1, RULE_function_args = 2, 
		RULE_function_body = 3, RULE_function_name = 4, RULE_expression_list = 5, 
		RULE_equation = 6, RULE_expression = 7, RULE_multiplying_expression = 8, 
		RULE_pow_expression = 9, RULE_relop = 10, RULE_signed_atom = 11, RULE_function_call = 12, 
		RULE_variable = 13, RULE_atom = 14, RULE_scientific = 15, RULE_formal_parameters = 16, 
		RULE_parameter_declaration = 17, RULE_parameter_type = 18, RULE_parameter_name = 19, 
		RULE_module_import = 20;
	private static String[] makeRuleNames() {
		return new String[] {
			"fifth", "function_declaration", "function_args", "function_body", "function_name", 
			"expression_list", "equation", "expression", "multiplying_expression", 
			"pow_expression", "relop", "signed_atom", "function_call", "variable", 
			"atom", "scientific", "formal_parameters", "parameter_declaration", "parameter_type", 
			"parameter_name", "module_import"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'('", "')'", "','", "'=>'", "'use'", "'+'", "'-'", "'*'", "'/'", 
			"'%'", "'^'", "'=='", "'!='", "'>'", "'<'", "'>='", "'<='", "'&&'", "'||'", 
			"';'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "OpenParen", "CloseParen", "Comma", "LambdaSep", "Use", "Plus", 
			"Minus", "Times", "Divide", "Percent", "Power", "EQ", "NEQ", "GT", "LT", 
			"GEQ", "LEQ", "And", "Or", "Semicolon", "Identifier", "IdStart", "IdPart", 
			"TimeInterval", "Letter", "Digit", "PositiveDigit", "Nat", "String", 
			"Float", "Int", "Exp", "Ws", "VARIABLE", "ScientificNumber"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "fifth.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public fifthParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class FifthContext extends ParserRuleContext {
		public List<Module_importContext> module_import() {
			return getRuleContexts(Module_importContext.class);
		}
		public Module_importContext module_import(int i) {
			return getRuleContext(Module_importContext.class,i);
		}
		public List<Function_declarationContext> function_declaration() {
			return getRuleContexts(Function_declarationContext.class);
		}
		public Function_declarationContext function_declaration(int i) {
			return getRuleContext(Function_declarationContext.class,i);
		}
		public FifthContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_fifth; }
	}

	public final FifthContext fifth() throws RecognitionException {
		FifthContext _localctx = new FifthContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_fifth);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(45);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Use) {
				{
				{
				setState(42);
				module_import();
				}
				}
				setState(47);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(51);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Identifier) {
				{
				{
				setState(48);
				function_declaration();
				}
				}
				setState(53);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Function_declarationContext extends ParserRuleContext {
		public Function_nameContext function_name() {
			return getRuleContext(Function_nameContext.class,0);
		}
		public Function_argsContext function_args() {
			return getRuleContext(Function_argsContext.class,0);
		}
		public Function_bodyContext function_body() {
			return getRuleContext(Function_bodyContext.class,0);
		}
		public TerminalNode Semicolon() { return getToken(fifthParser.Semicolon, 0); }
		public Function_declarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_declaration; }
	}

	public final Function_declarationContext function_declaration() throws RecognitionException {
		Function_declarationContext _localctx = new Function_declarationContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_function_declaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(54);
			function_name();
			setState(55);
			function_args();
			setState(56);
			function_body();
			setState(57);
			match(Semicolon);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Function_argsContext extends ParserRuleContext {
		public TerminalNode OpenParen() { return getToken(fifthParser.OpenParen, 0); }
		public TerminalNode CloseParen() { return getToken(fifthParser.CloseParen, 0); }
		public Formal_parametersContext formal_parameters() {
			return getRuleContext(Formal_parametersContext.class,0);
		}
		public Function_argsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_args; }
	}

	public final Function_argsContext function_args() throws RecognitionException {
		Function_argsContext _localctx = new Function_argsContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_function_args);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(59);
			match(OpenParen);
			setState(61);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Identifier) {
				{
				setState(60);
				formal_parameters();
				}
			}

			setState(63);
			match(CloseParen);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Function_bodyContext extends ParserRuleContext {
		public TerminalNode LambdaSep() { return getToken(fifthParser.LambdaSep, 0); }
		public Expression_listContext expression_list() {
			return getRuleContext(Expression_listContext.class,0);
		}
		public Function_bodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_body; }
	}

	public final Function_bodyContext function_body() throws RecognitionException {
		Function_bodyContext _localctx = new Function_bodyContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_function_body);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(65);
			match(LambdaSep);
			setState(66);
			expression_list();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Function_nameContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(fifthParser.Identifier, 0); }
		public Function_nameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_name; }
	}

	public final Function_nameContext function_name() throws RecognitionException {
		Function_nameContext _localctx = new Function_nameContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_function_name);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(68);
			match(Identifier);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Expression_listContext extends ParserRuleContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(fifthParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(fifthParser.Comma, i);
		}
		public Expression_listContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression_list; }
	}

	public final Expression_listContext expression_list() throws RecognitionException {
		Expression_listContext _localctx = new Expression_listContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_expression_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(70);
			expression();
			setState(75);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(71);
				match(Comma);
				setState(72);
				expression();
				}
				}
				setState(77);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class EquationContext extends ParserRuleContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public RelopContext relop() {
			return getRuleContext(RelopContext.class,0);
		}
		public EquationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_equation; }
	}

	public final EquationContext equation() throws RecognitionException {
		EquationContext _localctx = new EquationContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_equation);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(78);
			expression();
			setState(79);
			relop();
			setState(80);
			expression();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionContext extends ParserRuleContext {
		public List<Multiplying_expressionContext> multiplying_expression() {
			return getRuleContexts(Multiplying_expressionContext.class);
		}
		public Multiplying_expressionContext multiplying_expression(int i) {
			return getRuleContext(Multiplying_expressionContext.class,i);
		}
		public List<TerminalNode> Plus() { return getTokens(fifthParser.Plus); }
		public TerminalNode Plus(int i) {
			return getToken(fifthParser.Plus, i);
		}
		public List<TerminalNode> Minus() { return getTokens(fifthParser.Minus); }
		public TerminalNode Minus(int i) {
			return getToken(fifthParser.Minus, i);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	}

	public final ExpressionContext expression() throws RecognitionException {
		ExpressionContext _localctx = new ExpressionContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(82);
			multiplying_expression();
			setState(87);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Plus || _la==Minus) {
				{
				{
				setState(83);
				_la = _input.LA(1);
				if ( !(_la==Plus || _la==Minus) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(84);
				multiplying_expression();
				}
				}
				setState(89);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Multiplying_expressionContext extends ParserRuleContext {
		public List<Pow_expressionContext> pow_expression() {
			return getRuleContexts(Pow_expressionContext.class);
		}
		public Pow_expressionContext pow_expression(int i) {
			return getRuleContext(Pow_expressionContext.class,i);
		}
		public List<TerminalNode> Times() { return getTokens(fifthParser.Times); }
		public TerminalNode Times(int i) {
			return getToken(fifthParser.Times, i);
		}
		public List<TerminalNode> Divide() { return getTokens(fifthParser.Divide); }
		public TerminalNode Divide(int i) {
			return getToken(fifthParser.Divide, i);
		}
		public Multiplying_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_multiplying_expression; }
	}

	public final Multiplying_expressionContext multiplying_expression() throws RecognitionException {
		Multiplying_expressionContext _localctx = new Multiplying_expressionContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_multiplying_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(90);
			pow_expression();
			setState(95);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Times || _la==Divide) {
				{
				{
				setState(91);
				_la = _input.LA(1);
				if ( !(_la==Times || _la==Divide) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(92);
				pow_expression();
				}
				}
				setState(97);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Pow_expressionContext extends ParserRuleContext {
		public List<Signed_atomContext> signed_atom() {
			return getRuleContexts(Signed_atomContext.class);
		}
		public Signed_atomContext signed_atom(int i) {
			return getRuleContext(Signed_atomContext.class,i);
		}
		public List<TerminalNode> Power() { return getTokens(fifthParser.Power); }
		public TerminalNode Power(int i) {
			return getToken(fifthParser.Power, i);
		}
		public Pow_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pow_expression; }
	}

	public final Pow_expressionContext pow_expression() throws RecognitionException {
		Pow_expressionContext _localctx = new Pow_expressionContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_pow_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(98);
			signed_atom();
			setState(103);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Power) {
				{
				{
				setState(99);
				match(Power);
				setState(100);
				signed_atom();
				}
				}
				setState(105);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class RelopContext extends ParserRuleContext {
		public TerminalNode EQ() { return getToken(fifthParser.EQ, 0); }
		public TerminalNode GT() { return getToken(fifthParser.GT, 0); }
		public TerminalNode LT() { return getToken(fifthParser.LT, 0); }
		public RelopContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_relop; }
	}

	public final RelopContext relop() throws RecognitionException {
		RelopContext _localctx = new RelopContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_relop);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(106);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << EQ) | (1L << GT) | (1L << LT))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Signed_atomContext extends ParserRuleContext {
		public TerminalNode Plus() { return getToken(fifthParser.Plus, 0); }
		public Signed_atomContext signed_atom() {
			return getRuleContext(Signed_atomContext.class,0);
		}
		public TerminalNode Minus() { return getToken(fifthParser.Minus, 0); }
		public Function_callContext function_call() {
			return getRuleContext(Function_callContext.class,0);
		}
		public AtomContext atom() {
			return getRuleContext(AtomContext.class,0);
		}
		public Signed_atomContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_signed_atom; }
	}

	public final Signed_atomContext signed_atom() throws RecognitionException {
		Signed_atomContext _localctx = new Signed_atomContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_signed_atom);
		try {
			setState(114);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,7,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(108);
				match(Plus);
				setState(109);
				signed_atom();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(110);
				match(Minus);
				setState(111);
				signed_atom();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(112);
				function_call();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(113);
				atom();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Function_callContext extends ParserRuleContext {
		public Function_nameContext function_name() {
			return getRuleContext(Function_nameContext.class,0);
		}
		public TerminalNode OpenParen() { return getToken(fifthParser.OpenParen, 0); }
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public TerminalNode CloseParen() { return getToken(fifthParser.CloseParen, 0); }
		public List<TerminalNode> Comma() { return getTokens(fifthParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(fifthParser.Comma, i);
		}
		public Function_callContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_call; }
	}

	public final Function_callContext function_call() throws RecognitionException {
		Function_callContext _localctx = new Function_callContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_function_call);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(116);
			function_name();
			setState(117);
			match(OpenParen);
			setState(118);
			expression();
			setState(123);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(119);
				match(Comma);
				setState(120);
				expression();
				}
				}
				setState(125);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(126);
			match(CloseParen);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class VariableContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(fifthParser.Identifier, 0); }
		public VariableContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_variable; }
	}

	public final VariableContext variable() throws RecognitionException {
		VariableContext _localctx = new VariableContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_variable);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(128);
			match(Identifier);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AtomContext extends ParserRuleContext {
		public ScientificContext scientific() {
			return getRuleContext(ScientificContext.class,0);
		}
		public VariableContext variable() {
			return getRuleContext(VariableContext.class,0);
		}
		public TerminalNode OpenParen() { return getToken(fifthParser.OpenParen, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode CloseParen() { return getToken(fifthParser.CloseParen, 0); }
		public AtomContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_atom; }
	}

	public final AtomContext atom() throws RecognitionException {
		AtomContext _localctx = new AtomContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_atom);
		try {
			setState(136);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ScientificNumber:
				enterOuterAlt(_localctx, 1);
				{
				setState(130);
				scientific();
				}
				break;
			case Identifier:
				enterOuterAlt(_localctx, 2);
				{
				setState(131);
				variable();
				}
				break;
			case OpenParen:
				enterOuterAlt(_localctx, 3);
				{
				setState(132);
				match(OpenParen);
				setState(133);
				expression();
				setState(134);
				match(CloseParen);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ScientificContext extends ParserRuleContext {
		public TerminalNode ScientificNumber() { return getToken(fifthParser.ScientificNumber, 0); }
		public ScientificContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_scientific; }
	}

	public final ScientificContext scientific() throws RecognitionException {
		ScientificContext _localctx = new ScientificContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_scientific);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(138);
			match(ScientificNumber);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Formal_parametersContext extends ParserRuleContext {
		public List<Parameter_declarationContext> parameter_declaration() {
			return getRuleContexts(Parameter_declarationContext.class);
		}
		public Parameter_declarationContext parameter_declaration(int i) {
			return getRuleContext(Parameter_declarationContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(fifthParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(fifthParser.Comma, i);
		}
		public Formal_parametersContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_formal_parameters; }
	}

	public final Formal_parametersContext formal_parameters() throws RecognitionException {
		Formal_parametersContext _localctx = new Formal_parametersContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_formal_parameters);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(140);
			parameter_declaration();
			setState(145);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(141);
				match(Comma);
				setState(142);
				parameter_declaration();
				}
				}
				setState(147);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Parameter_declarationContext extends ParserRuleContext {
		public List<TerminalNode> Identifier() { return getTokens(fifthParser.Identifier); }
		public TerminalNode Identifier(int i) {
			return getToken(fifthParser.Identifier, i);
		}
		public Parameter_declarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameter_declaration; }
	}

	public final Parameter_declarationContext parameter_declaration() throws RecognitionException {
		Parameter_declarationContext _localctx = new Parameter_declarationContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_parameter_declaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(148);
			match(Identifier);
			setState(149);
			match(Identifier);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Parameter_typeContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(fifthParser.Identifier, 0); }
		public Parameter_typeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameter_type; }
	}

	public final Parameter_typeContext parameter_type() throws RecognitionException {
		Parameter_typeContext _localctx = new Parameter_typeContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_parameter_type);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(151);
			match(Identifier);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Parameter_nameContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(fifthParser.Identifier, 0); }
		public Parameter_nameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameter_name; }
	}

	public final Parameter_nameContext parameter_name() throws RecognitionException {
		Parameter_nameContext _localctx = new Parameter_nameContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_parameter_name);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(153);
			match(Identifier);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Module_importContext extends ParserRuleContext {
		public TerminalNode Use() { return getToken(fifthParser.Use, 0); }
		public TerminalNode Identifier() { return getToken(fifthParser.Identifier, 0); }
		public TerminalNode Semicolon() { return getToken(fifthParser.Semicolon, 0); }
		public Module_importContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_module_import; }
	}

	public final Module_importContext module_import() throws RecognitionException {
		Module_importContext _localctx = new Module_importContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_module_import);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(155);
			match(Use);
			setState(156);
			match(Identifier);
			setState(157);
			match(Semicolon);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3%\u00a2\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\3\2\7\2.\n\2\f\2\16\2\61\13\2"+
		"\3\2\7\2\64\n\2\f\2\16\2\67\13\2\3\3\3\3\3\3\3\3\3\3\3\4\3\4\5\4@\n\4"+
		"\3\4\3\4\3\5\3\5\3\5\3\6\3\6\3\7\3\7\3\7\7\7L\n\7\f\7\16\7O\13\7\3\b\3"+
		"\b\3\b\3\b\3\t\3\t\3\t\7\tX\n\t\f\t\16\t[\13\t\3\n\3\n\3\n\7\n`\n\n\f"+
		"\n\16\nc\13\n\3\13\3\13\3\13\7\13h\n\13\f\13\16\13k\13\13\3\f\3\f\3\r"+
		"\3\r\3\r\3\r\3\r\3\r\5\ru\n\r\3\16\3\16\3\16\3\16\3\16\7\16|\n\16\f\16"+
		"\16\16\177\13\16\3\16\3\16\3\17\3\17\3\20\3\20\3\20\3\20\3\20\3\20\5\20"+
		"\u008b\n\20\3\21\3\21\3\22\3\22\3\22\7\22\u0092\n\22\f\22\16\22\u0095"+
		"\13\22\3\23\3\23\3\23\3\24\3\24\3\25\3\25\3\26\3\26\3\26\3\26\3\26\2\2"+
		"\27\2\4\6\b\n\f\16\20\22\24\26\30\32\34\36 \"$&(*\2\5\3\2\b\t\3\2\n\13"+
		"\4\2\16\16\20\21\2\u009a\2/\3\2\2\2\48\3\2\2\2\6=\3\2\2\2\bC\3\2\2\2\n"+
		"F\3\2\2\2\fH\3\2\2\2\16P\3\2\2\2\20T\3\2\2\2\22\\\3\2\2\2\24d\3\2\2\2"+
		"\26l\3\2\2\2\30t\3\2\2\2\32v\3\2\2\2\34\u0082\3\2\2\2\36\u008a\3\2\2\2"+
		" \u008c\3\2\2\2\"\u008e\3\2\2\2$\u0096\3\2\2\2&\u0099\3\2\2\2(\u009b\3"+
		"\2\2\2*\u009d\3\2\2\2,.\5*\26\2-,\3\2\2\2.\61\3\2\2\2/-\3\2\2\2/\60\3"+
		"\2\2\2\60\65\3\2\2\2\61/\3\2\2\2\62\64\5\4\3\2\63\62\3\2\2\2\64\67\3\2"+
		"\2\2\65\63\3\2\2\2\65\66\3\2\2\2\66\3\3\2\2\2\67\65\3\2\2\289\5\n\6\2"+
		"9:\5\6\4\2:;\5\b\5\2;<\7\26\2\2<\5\3\2\2\2=?\7\3\2\2>@\5\"\22\2?>\3\2"+
		"\2\2?@\3\2\2\2@A\3\2\2\2AB\7\4\2\2B\7\3\2\2\2CD\7\6\2\2DE\5\f\7\2E\t\3"+
		"\2\2\2FG\7\27\2\2G\13\3\2\2\2HM\5\20\t\2IJ\7\5\2\2JL\5\20\t\2KI\3\2\2"+
		"\2LO\3\2\2\2MK\3\2\2\2MN\3\2\2\2N\r\3\2\2\2OM\3\2\2\2PQ\5\20\t\2QR\5\26"+
		"\f\2RS\5\20\t\2S\17\3\2\2\2TY\5\22\n\2UV\t\2\2\2VX\5\22\n\2WU\3\2\2\2"+
		"X[\3\2\2\2YW\3\2\2\2YZ\3\2\2\2Z\21\3\2\2\2[Y\3\2\2\2\\a\5\24\13\2]^\t"+
		"\3\2\2^`\5\24\13\2_]\3\2\2\2`c\3\2\2\2a_\3\2\2\2ab\3\2\2\2b\23\3\2\2\2"+
		"ca\3\2\2\2di\5\30\r\2ef\7\r\2\2fh\5\30\r\2ge\3\2\2\2hk\3\2\2\2ig\3\2\2"+
		"\2ij\3\2\2\2j\25\3\2\2\2ki\3\2\2\2lm\t\4\2\2m\27\3\2\2\2no\7\b\2\2ou\5"+
		"\30\r\2pq\7\t\2\2qu\5\30\r\2ru\5\32\16\2su\5\36\20\2tn\3\2\2\2tp\3\2\2"+
		"\2tr\3\2\2\2ts\3\2\2\2u\31\3\2\2\2vw\5\n\6\2wx\7\3\2\2x}\5\20\t\2yz\7"+
		"\5\2\2z|\5\20\t\2{y\3\2\2\2|\177\3\2\2\2}{\3\2\2\2}~\3\2\2\2~\u0080\3"+
		"\2\2\2\177}\3\2\2\2\u0080\u0081\7\4\2\2\u0081\33\3\2\2\2\u0082\u0083\7"+
		"\27\2\2\u0083\35\3\2\2\2\u0084\u008b\5 \21\2\u0085\u008b\5\34\17\2\u0086"+
		"\u0087\7\3\2\2\u0087\u0088\5\20\t\2\u0088\u0089\7\4\2\2\u0089\u008b\3"+
		"\2\2\2\u008a\u0084\3\2\2\2\u008a\u0085\3\2\2\2\u008a\u0086\3\2\2\2\u008b"+
		"\37\3\2\2\2\u008c\u008d\7%\2\2\u008d!\3\2\2\2\u008e\u0093\5$\23\2\u008f"+
		"\u0090\7\5\2\2\u0090\u0092\5$\23\2\u0091\u008f\3\2\2\2\u0092\u0095\3\2"+
		"\2\2\u0093\u0091\3\2\2\2\u0093\u0094\3\2\2\2\u0094#\3\2\2\2\u0095\u0093"+
		"\3\2\2\2\u0096\u0097\7\27\2\2\u0097\u0098\7\27\2\2\u0098%\3\2\2\2\u0099"+
		"\u009a\7\27\2\2\u009a\'\3\2\2\2\u009b\u009c\7\27\2\2\u009c)\3\2\2\2\u009d"+
		"\u009e\7\7\2\2\u009e\u009f\7\27\2\2\u009f\u00a0\7\26\2\2\u00a0+\3\2\2"+
		"\2\r/\65?MYait}\u008a\u0093";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}