// Generated from /Users/aabs/dev/by-technology/dotnet/fifthlang/src/fifth.parser/Parser/grammar/FifthParser.g4 by ANTLR 4.8
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class FifthParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.8", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		ALIAS=1, AS=2, ELSE=3, IF=4, NEW=5, WITH=6, RETURN=7, USE=8, ASSIGN=9, 
		CLOSEBRACE=10, CLOSEPAREN=11, COMMA=12, DIVIDE=13, DOT=14, EQ=15, LAMBDASEP=16, 
		MINUS=17, OPENBRACE=18, OPENPAREN=19, PLUS=20, TIMES=21, PERCENT=22, POWER=23, 
		NEQ=24, GT=25, LT=26, GEQ=27, LEQ=28, AND=29, OR=30, NOT=31, SEMICOLON=32, 
		URICONSTANT=33, IDENTIFIER=34, IDSTART=35, IDPART=36, TIMEINTERVAL=37, 
		LETTER=38, DIGIT=39, POSITIVEDIGIT=40, NAT=41, STRING=42, FLOAT=43, INT=44, 
		EXP=45, WS=46, VARIABLE=47, ScientificNumber=48;
	public static final int
		RULE_exp = 0, RULE_fifth = 1, RULE_alias = 2, RULE_exp_list = 3, RULE_formal_parameters = 4, 
		RULE_function_declaration = 5, RULE_function_args = 6, RULE_function_body = 7, 
		RULE_function_call = 8, RULE_function_name = 9, RULE_packagename = 10, 
		RULE_module_import = 11, RULE_parameter_declaration = 12, RULE_block = 13, 
		RULE_parameter_type = 14, RULE_parameter_name = 15, RULE_qvarname = 16, 
		RULE_statement = 17, RULE_type_initialiser = 18, RULE_type_name = 19, 
		RULE_type_property_init = 20, RULE_var_name = 21, RULE_variable = 22;
	private static String[] makeRuleNames() {
		return new String[] {
			"exp", "fifth", "alias", "exp_list", "formal_parameters", "function_declaration", 
			"function_args", "function_body", "function_call", "function_name", "packagename", 
			"module_import", "parameter_declaration", "block", "parameter_type", 
			"parameter_name", "qvarname", "statement", "type_initialiser", "type_name", 
			"type_property_init", "var_name", "variable"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'alias'", "'as'", "'else'", "'if'", "'new'", "'with'", "'return'", 
			"'use'", "'='", "'}'", "')'", "','", "'/'", "'.'", "'=='", "'=>'", "'-'", 
			"'{'", "'('", "'+'", "'*'", "'%'", "'^'", "'!='", "'>'", "'<'", "'>='", 
			"'<='", "'&&'", "'||'", "'!'", "';'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "ALIAS", "AS", "ELSE", "IF", "NEW", "WITH", "RETURN", "USE", "ASSIGN", 
			"CLOSEBRACE", "CLOSEPAREN", "COMMA", "DIVIDE", "DOT", "EQ", "LAMBDASEP", 
			"MINUS", "OPENBRACE", "OPENPAREN", "PLUS", "TIMES", "PERCENT", "POWER", 
			"NEQ", "GT", "LT", "GEQ", "LEQ", "AND", "OR", "NOT", "SEMICOLON", "URICONSTANT", 
			"IDENTIFIER", "IDSTART", "IDPART", "TIMEINTERVAL", "LETTER", "DIGIT", 
			"POSITIVEDIGIT", "NAT", "STRING", "FLOAT", "INT", "EXP", "WS", "VARIABLE", 
			"ScientificNumber"
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
	public String getGrammarFileName() { return "FifthParser.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public FifthParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ExpContext extends ParserRuleContext {
		public TerminalNode INT() { return getToken(FifthParser.INT, 0); }
		public TerminalNode FLOAT() { return getToken(FifthParser.FLOAT, 0); }
		public TerminalNode STRING() { return getToken(FifthParser.STRING, 0); }
		public QvarnameContext qvarname() {
			return getRuleContext(QvarnameContext.class,0);
		}
		public Function_nameContext function_name() {
			return getRuleContext(Function_nameContext.class,0);
		}
		public TerminalNode OPENPAREN() { return getToken(FifthParser.OPENPAREN, 0); }
		public TerminalNode CLOSEPAREN() { return getToken(FifthParser.CLOSEPAREN, 0); }
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public TerminalNode NOT() { return getToken(FifthParser.NOT, 0); }
		public TerminalNode NEW() { return getToken(FifthParser.NEW, 0); }
		public Type_initialiserContext type_initialiser() {
			return getRuleContext(Type_initialiserContext.class,0);
		}
		public TerminalNode AND() { return getToken(FifthParser.AND, 0); }
		public TerminalNode PLUS() { return getToken(FifthParser.PLUS, 0); }
		public TerminalNode MINUS() { return getToken(FifthParser.MINUS, 0); }
		public TerminalNode TIMES() { return getToken(FifthParser.TIMES, 0); }
		public TerminalNode DIVIDE() { return getToken(FifthParser.DIVIDE, 0); }
		public ExpContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_exp; }
	}

	public final ExpContext exp() throws RecognitionException {
		return exp(0);
	}

	private ExpContext exp(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExpContext _localctx = new ExpContext(_ctx, _parentState);
		ExpContext _prevctx = _localctx;
		int _startState = 0;
		enterRecursionRule(_localctx, 0, RULE_exp, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(73);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,2,_ctx) ) {
			case 1:
				{
				setState(47);
				match(INT);
				}
				break;
			case 2:
				{
				setState(48);
				match(FLOAT);
				}
				break;
			case 3:
				{
				setState(49);
				match(STRING);
				}
				break;
			case 4:
				{
				setState(50);
				qvarname();
				}
				break;
			case 5:
				{
				setState(51);
				function_name();
				setState(52);
				match(OPENPAREN);
				setState(54);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NEW) | (1L << OPENPAREN) | (1L << NOT) | (1L << IDENTIFIER) | (1L << STRING) | (1L << FLOAT) | (1L << INT))) != 0)) {
					{
					setState(53);
					exp(0);
					}
				}

				setState(56);
				match(CLOSEPAREN);
				}
				break;
			case 6:
				{
				setState(58);
				qvarname();
				setState(59);
				match(OPENPAREN);
				setState(61);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NEW) | (1L << OPENPAREN) | (1L << NOT) | (1L << IDENTIFIER) | (1L << STRING) | (1L << FLOAT) | (1L << INT))) != 0)) {
					{
					setState(60);
					exp(0);
					}
				}

				setState(63);
				match(CLOSEPAREN);
				}
				break;
			case 7:
				{
				setState(65);
				match(OPENPAREN);
				setState(66);
				exp(0);
				setState(67);
				match(CLOSEPAREN);
				}
				break;
			case 8:
				{
				setState(69);
				match(NOT);
				setState(70);
				exp(2);
				}
				break;
			case 9:
				{
				setState(71);
				match(NEW);
				setState(72);
				type_initialiser();
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(92);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,4,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(90);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
					case 1:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(75);
						if (!(precpred(_ctx, 14))) throw new FailedPredicateException(this, "precpred(_ctx, 14)");
						setState(76);
						match(AND);
						setState(77);
						exp(15);
						}
						break;
					case 2:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(78);
						if (!(precpred(_ctx, 13))) throw new FailedPredicateException(this, "precpred(_ctx, 13)");
						setState(79);
						match(PLUS);
						setState(80);
						exp(14);
						}
						break;
					case 3:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(81);
						if (!(precpred(_ctx, 12))) throw new FailedPredicateException(this, "precpred(_ctx, 12)");
						setState(82);
						match(MINUS);
						setState(83);
						exp(13);
						}
						break;
					case 4:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(84);
						if (!(precpred(_ctx, 11))) throw new FailedPredicateException(this, "precpred(_ctx, 11)");
						setState(85);
						match(TIMES);
						setState(86);
						exp(12);
						}
						break;
					case 5:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(87);
						if (!(precpred(_ctx, 10))) throw new FailedPredicateException(this, "precpred(_ctx, 10)");
						setState(88);
						match(DIVIDE);
						setState(89);
						exp(11);
						}
						break;
					}
					} 
				}
				setState(94);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,4,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class FifthContext extends ParserRuleContext {
		public List<Module_importContext> module_import() {
			return getRuleContexts(Module_importContext.class);
		}
		public Module_importContext module_import(int i) {
			return getRuleContext(Module_importContext.class,i);
		}
		public List<AliasContext> alias() {
			return getRuleContexts(AliasContext.class);
		}
		public AliasContext alias(int i) {
			return getRuleContext(AliasContext.class,i);
		}
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
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
		enterRule(_localctx, 2, RULE_fifth);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(98);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==USE) {
				{
				{
				setState(95);
				module_import();
				}
				}
				setState(100);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(104);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==ALIAS) {
				{
				{
				setState(101);
				alias();
				}
				}
				setState(106);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(110);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,7,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(107);
					statement();
					}
					} 
				}
				setState(112);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,7,_ctx);
			}
			setState(116);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==IDENTIFIER) {
				{
				{
				setState(113);
				function_declaration();
				}
				}
				setState(118);
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

	public static class AliasContext extends ParserRuleContext {
		public TerminalNode ALIAS() { return getToken(FifthParser.ALIAS, 0); }
		public TerminalNode URICONSTANT() { return getToken(FifthParser.URICONSTANT, 0); }
		public TerminalNode AS() { return getToken(FifthParser.AS, 0); }
		public PackagenameContext packagename() {
			return getRuleContext(PackagenameContext.class,0);
		}
		public TerminalNode SEMICOLON() { return getToken(FifthParser.SEMICOLON, 0); }
		public AliasContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_alias; }
	}

	public final AliasContext alias() throws RecognitionException {
		AliasContext _localctx = new AliasContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_alias);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(119);
			match(ALIAS);
			setState(120);
			match(URICONSTANT);
			setState(121);
			match(AS);
			setState(122);
			packagename();
			setState(123);
			match(SEMICOLON);
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

	public static class Exp_listContext extends ParserRuleContext {
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(FifthParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(FifthParser.COMMA, i);
		}
		public Exp_listContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_exp_list; }
	}

	public final Exp_listContext exp_list() throws RecognitionException {
		Exp_listContext _localctx = new Exp_listContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_exp_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(125);
			exp(0);
			setState(130);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(126);
				match(COMMA);
				setState(127);
				exp(0);
				}
				}
				setState(132);
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

	public static class Formal_parametersContext extends ParserRuleContext {
		public List<Parameter_declarationContext> parameter_declaration() {
			return getRuleContexts(Parameter_declarationContext.class);
		}
		public Parameter_declarationContext parameter_declaration(int i) {
			return getRuleContext(Parameter_declarationContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(FifthParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(FifthParser.COMMA, i);
		}
		public Formal_parametersContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_formal_parameters; }
	}

	public final Formal_parametersContext formal_parameters() throws RecognitionException {
		Formal_parametersContext _localctx = new Formal_parametersContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_formal_parameters);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(133);
			parameter_declaration();
			setState(138);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(134);
				match(COMMA);
				setState(135);
				parameter_declaration();
				}
				}
				setState(140);
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
		public TerminalNode SEMICOLON() { return getToken(FifthParser.SEMICOLON, 0); }
		public Function_declarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_declaration; }
	}

	public final Function_declarationContext function_declaration() throws RecognitionException {
		Function_declarationContext _localctx = new Function_declarationContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_function_declaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(141);
			function_name();
			setState(142);
			function_args();
			setState(143);
			function_body();
			setState(144);
			match(SEMICOLON);
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
		public TerminalNode OPENPAREN() { return getToken(FifthParser.OPENPAREN, 0); }
		public TerminalNode CLOSEPAREN() { return getToken(FifthParser.CLOSEPAREN, 0); }
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
		enterRule(_localctx, 12, RULE_function_args);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(146);
			match(OPENPAREN);
			setState(148);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==IDENTIFIER) {
				{
				setState(147);
				formal_parameters();
				}
			}

			setState(150);
			match(CLOSEPAREN);
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
		public TerminalNode LAMBDASEP() { return getToken(FifthParser.LAMBDASEP, 0); }
		public Exp_listContext exp_list() {
			return getRuleContext(Exp_listContext.class,0);
		}
		public Function_bodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_body; }
	}

	public final Function_bodyContext function_body() throws RecognitionException {
		Function_bodyContext _localctx = new Function_bodyContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_function_body);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(152);
			match(LAMBDASEP);
			setState(153);
			exp_list();
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
		public TerminalNode OPENPAREN() { return getToken(FifthParser.OPENPAREN, 0); }
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public TerminalNode CLOSEPAREN() { return getToken(FifthParser.CLOSEPAREN, 0); }
		public List<TerminalNode> COMMA() { return getTokens(FifthParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(FifthParser.COMMA, i);
		}
		public Function_callContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_call; }
	}

	public final Function_callContext function_call() throws RecognitionException {
		Function_callContext _localctx = new Function_callContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_function_call);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(155);
			function_name();
			setState(156);
			match(OPENPAREN);
			setState(157);
			exp(0);
			setState(162);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(158);
				match(COMMA);
				setState(159);
				exp(0);
				}
				}
				setState(164);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(165);
			match(CLOSEPAREN);
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
		public TerminalNode IDENTIFIER() { return getToken(FifthParser.IDENTIFIER, 0); }
		public Function_nameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_name; }
	}

	public final Function_nameContext function_name() throws RecognitionException {
		Function_nameContext _localctx = new Function_nameContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_function_name);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(167);
			match(IDENTIFIER);
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

	public static class PackagenameContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(FifthParser.IDENTIFIER, 0); }
		public PackagenameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_packagename; }
	}

	public final PackagenameContext packagename() throws RecognitionException {
		PackagenameContext _localctx = new PackagenameContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_packagename);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(169);
			match(IDENTIFIER);
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
		public TerminalNode USE() { return getToken(FifthParser.USE, 0); }
		public TerminalNode IDENTIFIER() { return getToken(FifthParser.IDENTIFIER, 0); }
		public TerminalNode SEMICOLON() { return getToken(FifthParser.SEMICOLON, 0); }
		public Module_importContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_module_import; }
	}

	public final Module_importContext module_import() throws RecognitionException {
		Module_importContext _localctx = new Module_importContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_module_import);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(171);
			match(USE);
			setState(172);
			match(IDENTIFIER);
			setState(173);
			match(SEMICOLON);
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
		public List<TerminalNode> IDENTIFIER() { return getTokens(FifthParser.IDENTIFIER); }
		public TerminalNode IDENTIFIER(int i) {
			return getToken(FifthParser.IDENTIFIER, i);
		}
		public Parameter_declarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameter_declaration; }
	}

	public final Parameter_declarationContext parameter_declaration() throws RecognitionException {
		Parameter_declarationContext _localctx = new Parameter_declarationContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_parameter_declaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(175);
			match(IDENTIFIER);
			setState(176);
			match(IDENTIFIER);
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

	public static class BlockContext extends ParserRuleContext {
		public TerminalNode OPENBRACE() { return getToken(FifthParser.OPENBRACE, 0); }
		public TerminalNode CLOSEBRACE() { return getToken(FifthParser.CLOSEBRACE, 0); }
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public BlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_block; }
	}

	public final BlockContext block() throws RecognitionException {
		BlockContext _localctx = new BlockContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_block);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(178);
			match(OPENBRACE);
			setState(182);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << IF) | (1L << NEW) | (1L << WITH) | (1L << RETURN) | (1L << OPENPAREN) | (1L << NOT) | (1L << IDENTIFIER) | (1L << STRING) | (1L << FLOAT) | (1L << INT))) != 0)) {
				{
				{
				setState(179);
				statement();
				}
				}
				setState(184);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(185);
			match(CLOSEBRACE);
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
		public TerminalNode IDENTIFIER() { return getToken(FifthParser.IDENTIFIER, 0); }
		public Parameter_typeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameter_type; }
	}

	public final Parameter_typeContext parameter_type() throws RecognitionException {
		Parameter_typeContext _localctx = new Parameter_typeContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_parameter_type);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(187);
			match(IDENTIFIER);
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
		public TerminalNode IDENTIFIER() { return getToken(FifthParser.IDENTIFIER, 0); }
		public Parameter_nameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameter_name; }
	}

	public final Parameter_nameContext parameter_name() throws RecognitionException {
		Parameter_nameContext _localctx = new Parameter_nameContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_parameter_name);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(189);
			match(IDENTIFIER);
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

	public static class QvarnameContext extends ParserRuleContext {
		public List<Var_nameContext> var_name() {
			return getRuleContexts(Var_nameContext.class);
		}
		public Var_nameContext var_name(int i) {
			return getRuleContext(Var_nameContext.class,i);
		}
		public TerminalNode DOT() { return getToken(FifthParser.DOT, 0); }
		public QvarnameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_qvarname; }
	}

	public final QvarnameContext qvarname() throws RecognitionException {
		QvarnameContext _localctx = new QvarnameContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_qvarname);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(191);
			var_name();
			{
			setState(192);
			match(DOT);
			setState(193);
			var_name();
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

	public static class StatementContext extends ParserRuleContext {
		public QvarnameContext qvarname() {
			return getRuleContext(QvarnameContext.class,0);
		}
		public TerminalNode ASSIGN() { return getToken(FifthParser.ASSIGN, 0); }
		public ExpContext exp() {
			return getRuleContext(ExpContext.class,0);
		}
		public TerminalNode RETURN() { return getToken(FifthParser.RETURN, 0); }
		public TerminalNode IF() { return getToken(FifthParser.IF, 0); }
		public TerminalNode OPENPAREN() { return getToken(FifthParser.OPENPAREN, 0); }
		public TerminalNode CLOSEPAREN() { return getToken(FifthParser.CLOSEPAREN, 0); }
		public List<BlockContext> block() {
			return getRuleContexts(BlockContext.class);
		}
		public BlockContext block(int i) {
			return getRuleContext(BlockContext.class,i);
		}
		public TerminalNode ELSE() { return getToken(FifthParser.ELSE, 0); }
		public TerminalNode WITH() { return getToken(FifthParser.WITH, 0); }
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_statement);
		try {
			setState(218);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,14,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(195);
				qvarname();
				setState(196);
				match(ASSIGN);
				setState(197);
				exp(0);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(199);
				match(RETURN);
				setState(200);
				exp(0);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(201);
				match(IF);
				setState(202);
				match(OPENPAREN);
				setState(203);
				exp(0);
				setState(204);
				match(CLOSEPAREN);
				setState(205);
				block();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(207);
				match(IF);
				setState(208);
				match(OPENPAREN);
				setState(209);
				exp(0);
				setState(210);
				match(CLOSEPAREN);
				setState(211);
				block();
				setState(212);
				match(ELSE);
				setState(213);
				block();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(215);
				match(WITH);
				setState(216);
				statement();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(217);
				exp(0);
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

	public static class Type_initialiserContext extends ParserRuleContext {
		public Type_nameContext type_name() {
			return getRuleContext(Type_nameContext.class,0);
		}
		public TerminalNode OPENBRACE() { return getToken(FifthParser.OPENBRACE, 0); }
		public TerminalNode CLOSEBRACE() { return getToken(FifthParser.CLOSEBRACE, 0); }
		public List<Type_property_initContext> type_property_init() {
			return getRuleContexts(Type_property_initContext.class);
		}
		public Type_property_initContext type_property_init(int i) {
			return getRuleContext(Type_property_initContext.class,i);
		}
		public Type_initialiserContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type_initialiser; }
	}

	public final Type_initialiserContext type_initialiser() throws RecognitionException {
		Type_initialiserContext _localctx = new Type_initialiserContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_type_initialiser);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(220);
			type_name();
			setState(221);
			match(OPENBRACE);
			setState(225);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==IDENTIFIER) {
				{
				{
				setState(222);
				type_property_init();
				}
				}
				setState(227);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(228);
			match(CLOSEBRACE);
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

	public static class Type_nameContext extends ParserRuleContext {
		public QvarnameContext qvarname() {
			return getRuleContext(QvarnameContext.class,0);
		}
		public Type_nameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type_name; }
	}

	public final Type_nameContext type_name() throws RecognitionException {
		Type_nameContext _localctx = new Type_nameContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_type_name);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(230);
			qvarname();
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

	public static class Type_property_initContext extends ParserRuleContext {
		public Var_nameContext var_name() {
			return getRuleContext(Var_nameContext.class,0);
		}
		public TerminalNode ASSIGN() { return getToken(FifthParser.ASSIGN, 0); }
		public ExpContext exp() {
			return getRuleContext(ExpContext.class,0);
		}
		public Type_property_initContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type_property_init; }
	}

	public final Type_property_initContext type_property_init() throws RecognitionException {
		Type_property_initContext _localctx = new Type_property_initContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_type_property_init);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(232);
			var_name();
			setState(233);
			match(ASSIGN);
			setState(234);
			exp(0);
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

	public static class Var_nameContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(FifthParser.IDENTIFIER, 0); }
		public Var_nameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_var_name; }
	}

	public final Var_nameContext var_name() throws RecognitionException {
		Var_nameContext _localctx = new Var_nameContext(_ctx, getState());
		enterRule(_localctx, 42, RULE_var_name);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(236);
			match(IDENTIFIER);
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
		public TerminalNode IDENTIFIER() { return getToken(FifthParser.IDENTIFIER, 0); }
		public VariableContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_variable; }
	}

	public final VariableContext variable() throws RecognitionException {
		VariableContext _localctx = new VariableContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_variable);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(238);
			match(IDENTIFIER);
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

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 0:
			return exp_sempred((ExpContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean exp_sempred(ExpContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 14);
		case 1:
			return precpred(_ctx, 13);
		case 2:
			return precpred(_ctx, 12);
		case 3:
			return precpred(_ctx, 11);
		case 4:
			return precpred(_ctx, 10);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\62\u00f3\4\2\t\2"+
		"\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\3\2\3\2\3"+
		"\2\3\2\3\2\3\2\3\2\3\2\5\29\n\2\3\2\3\2\3\2\3\2\3\2\5\2@\n\2\3\2\3\2\3"+
		"\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\5\2L\n\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3"+
		"\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\7\2]\n\2\f\2\16\2`\13\2\3\3\7\3c\n\3\f"+
		"\3\16\3f\13\3\3\3\7\3i\n\3\f\3\16\3l\13\3\3\3\7\3o\n\3\f\3\16\3r\13\3"+
		"\3\3\7\3u\n\3\f\3\16\3x\13\3\3\4\3\4\3\4\3\4\3\4\3\4\3\5\3\5\3\5\7\5\u0083"+
		"\n\5\f\5\16\5\u0086\13\5\3\6\3\6\3\6\7\6\u008b\n\6\f\6\16\6\u008e\13\6"+
		"\3\7\3\7\3\7\3\7\3\7\3\b\3\b\5\b\u0097\n\b\3\b\3\b\3\t\3\t\3\t\3\n\3\n"+
		"\3\n\3\n\3\n\7\n\u00a3\n\n\f\n\16\n\u00a6\13\n\3\n\3\n\3\13\3\13\3\f\3"+
		"\f\3\r\3\r\3\r\3\r\3\16\3\16\3\16\3\17\3\17\7\17\u00b7\n\17\f\17\16\17"+
		"\u00ba\13\17\3\17\3\17\3\20\3\20\3\21\3\21\3\22\3\22\3\22\3\22\3\23\3"+
		"\23\3\23\3\23\3\23\3\23\3\23\3\23\3\23\3\23\3\23\3\23\3\23\3\23\3\23\3"+
		"\23\3\23\3\23\3\23\3\23\3\23\3\23\3\23\5\23\u00dd\n\23\3\24\3\24\3\24"+
		"\7\24\u00e2\n\24\f\24\16\24\u00e5\13\24\3\24\3\24\3\25\3\25\3\26\3\26"+
		"\3\26\3\26\3\27\3\27\3\30\3\30\3\30\2\3\2\31\2\4\6\b\n\f\16\20\22\24\26"+
		"\30\32\34\36 \"$&(*,.\2\2\2\u00f9\2K\3\2\2\2\4d\3\2\2\2\6y\3\2\2\2\b\177"+
		"\3\2\2\2\n\u0087\3\2\2\2\f\u008f\3\2\2\2\16\u0094\3\2\2\2\20\u009a\3\2"+
		"\2\2\22\u009d\3\2\2\2\24\u00a9\3\2\2\2\26\u00ab\3\2\2\2\30\u00ad\3\2\2"+
		"\2\32\u00b1\3\2\2\2\34\u00b4\3\2\2\2\36\u00bd\3\2\2\2 \u00bf\3\2\2\2\""+
		"\u00c1\3\2\2\2$\u00dc\3\2\2\2&\u00de\3\2\2\2(\u00e8\3\2\2\2*\u00ea\3\2"+
		"\2\2,\u00ee\3\2\2\2.\u00f0\3\2\2\2\60\61\b\2\1\2\61L\7.\2\2\62L\7-\2\2"+
		"\63L\7,\2\2\64L\5\"\22\2\65\66\5\24\13\2\668\7\25\2\2\679\5\2\2\28\67"+
		"\3\2\2\289\3\2\2\29:\3\2\2\2:;\7\r\2\2;L\3\2\2\2<=\5\"\22\2=?\7\25\2\2"+
		">@\5\2\2\2?>\3\2\2\2?@\3\2\2\2@A\3\2\2\2AB\7\r\2\2BL\3\2\2\2CD\7\25\2"+
		"\2DE\5\2\2\2EF\7\r\2\2FL\3\2\2\2GH\7!\2\2HL\5\2\2\4IJ\7\7\2\2JL\5&\24"+
		"\2K\60\3\2\2\2K\62\3\2\2\2K\63\3\2\2\2K\64\3\2\2\2K\65\3\2\2\2K<\3\2\2"+
		"\2KC\3\2\2\2KG\3\2\2\2KI\3\2\2\2L^\3\2\2\2MN\f\20\2\2NO\7\37\2\2O]\5\2"+
		"\2\21PQ\f\17\2\2QR\7\26\2\2R]\5\2\2\20ST\f\16\2\2TU\7\23\2\2U]\5\2\2\17"+
		"VW\f\r\2\2WX\7\27\2\2X]\5\2\2\16YZ\f\f\2\2Z[\7\17\2\2[]\5\2\2\r\\M\3\2"+
		"\2\2\\P\3\2\2\2\\S\3\2\2\2\\V\3\2\2\2\\Y\3\2\2\2]`\3\2\2\2^\\\3\2\2\2"+
		"^_\3\2\2\2_\3\3\2\2\2`^\3\2\2\2ac\5\30\r\2ba\3\2\2\2cf\3\2\2\2db\3\2\2"+
		"\2de\3\2\2\2ej\3\2\2\2fd\3\2\2\2gi\5\6\4\2hg\3\2\2\2il\3\2\2\2jh\3\2\2"+
		"\2jk\3\2\2\2kp\3\2\2\2lj\3\2\2\2mo\5$\23\2nm\3\2\2\2or\3\2\2\2pn\3\2\2"+
		"\2pq\3\2\2\2qv\3\2\2\2rp\3\2\2\2su\5\f\7\2ts\3\2\2\2ux\3\2\2\2vt\3\2\2"+
		"\2vw\3\2\2\2w\5\3\2\2\2xv\3\2\2\2yz\7\3\2\2z{\7#\2\2{|\7\4\2\2|}\5\26"+
		"\f\2}~\7\"\2\2~\7\3\2\2\2\177\u0084\5\2\2\2\u0080\u0081\7\16\2\2\u0081"+
		"\u0083\5\2\2\2\u0082\u0080\3\2\2\2\u0083\u0086\3\2\2\2\u0084\u0082\3\2"+
		"\2\2\u0084\u0085\3\2\2\2\u0085\t\3\2\2\2\u0086\u0084\3\2\2\2\u0087\u008c"+
		"\5\32\16\2\u0088\u0089\7\16\2\2\u0089\u008b\5\32\16\2\u008a\u0088\3\2"+
		"\2\2\u008b\u008e\3\2\2\2\u008c\u008a\3\2\2\2\u008c\u008d\3\2\2\2\u008d"+
		"\13\3\2\2\2\u008e\u008c\3\2\2\2\u008f\u0090\5\24\13\2\u0090\u0091\5\16"+
		"\b\2\u0091\u0092\5\20\t\2\u0092\u0093\7\"\2\2\u0093\r\3\2\2\2\u0094\u0096"+
		"\7\25\2\2\u0095\u0097\5\n\6\2\u0096\u0095\3\2\2\2\u0096\u0097\3\2\2\2"+
		"\u0097\u0098\3\2\2\2\u0098\u0099\7\r\2\2\u0099\17\3\2\2\2\u009a\u009b"+
		"\7\22\2\2\u009b\u009c\5\b\5\2\u009c\21\3\2\2\2\u009d\u009e\5\24\13\2\u009e"+
		"\u009f\7\25\2\2\u009f\u00a4\5\2\2\2\u00a0\u00a1\7\16\2\2\u00a1\u00a3\5"+
		"\2\2\2\u00a2\u00a0\3\2\2\2\u00a3\u00a6\3\2\2\2\u00a4\u00a2\3\2\2\2\u00a4"+
		"\u00a5\3\2\2\2\u00a5\u00a7\3\2\2\2\u00a6\u00a4\3\2\2\2\u00a7\u00a8\7\r"+
		"\2\2\u00a8\23\3\2\2\2\u00a9\u00aa\7$\2\2\u00aa\25\3\2\2\2\u00ab\u00ac"+
		"\7$\2\2\u00ac\27\3\2\2\2\u00ad\u00ae\7\n\2\2\u00ae\u00af\7$\2\2\u00af"+
		"\u00b0\7\"\2\2\u00b0\31\3\2\2\2\u00b1\u00b2\7$\2\2\u00b2\u00b3\7$\2\2"+
		"\u00b3\33\3\2\2\2\u00b4\u00b8\7\24\2\2\u00b5\u00b7\5$\23\2\u00b6\u00b5"+
		"\3\2\2\2\u00b7\u00ba\3\2\2\2\u00b8\u00b6\3\2\2\2\u00b8\u00b9\3\2\2\2\u00b9"+
		"\u00bb\3\2\2\2\u00ba\u00b8\3\2\2\2\u00bb\u00bc\7\f\2\2\u00bc\35\3\2\2"+
		"\2\u00bd\u00be\7$\2\2\u00be\37\3\2\2\2\u00bf\u00c0\7$\2\2\u00c0!\3\2\2"+
		"\2\u00c1\u00c2\5,\27\2\u00c2\u00c3\7\20\2\2\u00c3\u00c4\5,\27\2\u00c4"+
		"#\3\2\2\2\u00c5\u00c6\5\"\22\2\u00c6\u00c7\7\13\2\2\u00c7\u00c8\5\2\2"+
		"\2\u00c8\u00dd\3\2\2\2\u00c9\u00ca\7\t\2\2\u00ca\u00dd\5\2\2\2\u00cb\u00cc"+
		"\7\6\2\2\u00cc\u00cd\7\25\2\2\u00cd\u00ce\5\2\2\2\u00ce\u00cf\7\r\2\2"+
		"\u00cf\u00d0\5\34\17\2\u00d0\u00dd\3\2\2\2\u00d1\u00d2\7\6\2\2\u00d2\u00d3"+
		"\7\25\2\2\u00d3\u00d4\5\2\2\2\u00d4\u00d5\7\r\2\2\u00d5\u00d6\5\34\17"+
		"\2\u00d6\u00d7\7\5\2\2\u00d7\u00d8\5\34\17\2\u00d8\u00dd\3\2\2\2\u00d9"+
		"\u00da\7\b\2\2\u00da\u00dd\5$\23\2\u00db\u00dd\5\2\2\2\u00dc\u00c5\3\2"+
		"\2\2\u00dc\u00c9\3\2\2\2\u00dc\u00cb\3\2\2\2\u00dc\u00d1\3\2\2\2\u00dc"+
		"\u00d9\3\2\2\2\u00dc\u00db\3\2\2\2\u00dd%\3\2\2\2\u00de\u00df\5(\25\2"+
		"\u00df\u00e3\7\24\2\2\u00e0\u00e2\5*\26\2\u00e1\u00e0\3\2\2\2\u00e2\u00e5"+
		"\3\2\2\2\u00e3\u00e1\3\2\2\2\u00e3\u00e4\3\2\2\2\u00e4\u00e6\3\2\2\2\u00e5"+
		"\u00e3\3\2\2\2\u00e6\u00e7\7\f\2\2\u00e7\'\3\2\2\2\u00e8\u00e9\5\"\22"+
		"\2\u00e9)\3\2\2\2\u00ea\u00eb\5,\27\2\u00eb\u00ec\7\13\2\2\u00ec\u00ed"+
		"\5\2\2\2\u00ed+\3\2\2\2\u00ee\u00ef\7$\2\2\u00ef-\3\2\2\2\u00f0\u00f1"+
		"\7$\2\2\u00f1/\3\2\2\2\228?K\\^djpv\u0084\u008c\u0096\u00a4\u00b8\u00dc"+
		"\u00e3";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}