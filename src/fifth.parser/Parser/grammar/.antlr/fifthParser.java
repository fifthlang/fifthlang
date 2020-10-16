// Generated from /Users/aabs/dev/by-technology/dotnet/fifthlang/src/fifth.parser/Parser/grammar/Fifth.g4 by ANTLR 4.8
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
		CLOSEBRACE=10, CLOSEPAREN=11, COLON=12, COMMA=13, DIVIDE=14, DOT=15, EQ=16, 
		HASH=17, LAMBDASEP=18, MINUS=19, OPENBRACE=20, OPENPAREN=21, PLUS=22, 
		QMARK=23, TIMES=24, PERCENT=25, POWER=26, NEQ=27, GT=28, LT=29, GEQ=30, 
		LEQ=31, AMP=32, AND=33, OR=34, NOT=35, SEMICOLON=36, IDENTIFIER=37, STRING=38, 
		INT=39, FLOAT=40, WS=41;
	public static final int
		RULE_fifth = 0, RULE_alias = 1, RULE_block = 2, RULE_exp = 3, RULE_formal_parameters = 4, 
		RULE_function_declaration = 5, RULE_function_args = 6, RULE_function_body = 7, 
		RULE_function_call = 8, RULE_function_name = 9, RULE_iri = 10, RULE_iri_query = 11, 
		RULE_iri_query_param = 12, RULE_module_import = 13, RULE_module_name = 14, 
		RULE_packagename = 15, RULE_parameter_declaration = 16, RULE_parameter_type = 17, 
		RULE_parameter_name = 18, RULE_statement = 19, RULE_type_initialiser = 20, 
		RULE_type_name = 21, RULE_type_property_init = 22, RULE_var_name = 23;
	private static String[] makeRuleNames() {
		return new String[] {
			"fifth", "alias", "block", "exp", "formal_parameters", "function_declaration", 
			"function_args", "function_body", "function_call", "function_name", "iri", 
			"iri_query", "iri_query_param", "module_import", "module_name", "packagename", 
			"parameter_declaration", "parameter_type", "parameter_name", "statement", 
			"type_initialiser", "type_name", "type_property_init", "var_name"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'alias'", "'as'", "'else'", "'if'", "'new'", "'with'", "'return'", 
			"'use'", "'='", "'}'", "')'", "':'", "','", "'/'", "'.'", "'=='", "'#'", 
			"'=>'", "'-'", "'{'", "'('", "'+'", "'?'", "'*'", "'%'", "'^'", "'!='", 
			"'>'", "'<'", "'>='", "'<='", "'&'", "'&&'", "'||'", "'!'", "';'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "ALIAS", "AS", "ELSE", "IF", "NEW", "WITH", "RETURN", "USE", "ASSIGN", 
			"CLOSEBRACE", "CLOSEPAREN", "COLON", "COMMA", "DIVIDE", "DOT", "EQ", 
			"HASH", "LAMBDASEP", "MINUS", "OPENBRACE", "OPENPAREN", "PLUS", "QMARK", 
			"TIMES", "PERCENT", "POWER", "NEQ", "GT", "LT", "GEQ", "LEQ", "AMP", 
			"AND", "OR", "NOT", "SEMICOLON", "IDENTIFIER", "STRING", "INT", "FLOAT", 
			"WS"
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
	public String getGrammarFileName() { return "Fifth.g4"; }

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
		enterRule(_localctx, 0, RULE_fifth);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(51);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==USE) {
				{
				{
				setState(48);
				module_import();
				}
				}
				setState(53);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(57);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==ALIAS) {
				{
				{
				setState(54);
				alias();
				}
				}
				setState(59);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(63);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,2,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(60);
					statement();
					}
					} 
				}
				setState(65);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,2,_ctx);
			}
			setState(69);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==IDENTIFIER) {
				{
				{
				setState(66);
				function_declaration();
				}
				}
				setState(71);
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
		public IriContext iri() {
			return getRuleContext(IriContext.class,0);
		}
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
		enterRule(_localctx, 2, RULE_alias);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(72);
			match(ALIAS);
			setState(73);
			iri();
			setState(74);
			match(AS);
			setState(75);
			packagename();
			setState(76);
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
		enterRule(_localctx, 4, RULE_block);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(78);
			match(OPENBRACE);
			setState(82);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << IF) | (1L << NEW) | (1L << WITH) | (1L << RETURN) | (1L << OPENPAREN) | (1L << NOT) | (1L << IDENTIFIER) | (1L << STRING) | (1L << INT) | (1L << FLOAT))) != 0)) {
				{
				{
				setState(79);
				statement();
				}
				}
				setState(84);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(85);
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

	public static class ExpContext extends ParserRuleContext {
		public ExpContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_exp; }
	 
		public ExpContext() { }
		public void copyFrom(ExpContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class EFuncCallContext extends ExpContext {
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
		public List<TerminalNode> COMMA() { return getTokens(FifthParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(FifthParser.COMMA, i);
		}
		public EFuncCallContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class ETypeCreateContext extends ExpContext {
		public TerminalNode NEW() { return getToken(FifthParser.NEW, 0); }
		public Type_initialiserContext type_initialiser() {
			return getRuleContext(Type_initialiserContext.class,0);
		}
		public ETypeCreateContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class EVarnameContext extends ExpContext {
		public Var_nameContext var_name() {
			return getRuleContext(Var_nameContext.class,0);
		}
		public EVarnameContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class EIntContext extends ExpContext {
		public TerminalNode INT() { return getToken(FifthParser.INT, 0); }
		public EIntContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class ELTContext extends ExpContext {
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public TerminalNode LT() { return getToken(FifthParser.LT, 0); }
		public ELTContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class EDivContext extends ExpContext {
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public TerminalNode DIVIDE() { return getToken(FifthParser.DIVIDE, 0); }
		public EDivContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class EGEQContext extends ExpContext {
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public TerminalNode GEQ() { return getToken(FifthParser.GEQ, 0); }
		public EGEQContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class EAndContext extends ExpContext {
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public TerminalNode AND() { return getToken(FifthParser.AND, 0); }
		public EAndContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class EGTContext extends ExpContext {
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public TerminalNode GT() { return getToken(FifthParser.GT, 0); }
		public EGTContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class ELEQContext extends ExpContext {
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public TerminalNode LEQ() { return getToken(FifthParser.LEQ, 0); }
		public ELEQContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class ENegationContext extends ExpContext {
		public TerminalNode NOT() { return getToken(FifthParser.NOT, 0); }
		public ExpContext exp() {
			return getRuleContext(ExpContext.class,0);
		}
		public ENegationContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class ESubContext extends ExpContext {
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public TerminalNode MINUS() { return getToken(FifthParser.MINUS, 0); }
		public ESubContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class EDoubleContext extends ExpContext {
		public TerminalNode FLOAT() { return getToken(FifthParser.FLOAT, 0); }
		public EDoubleContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class EFuncParenContext extends ExpContext {
		public TerminalNode OPENPAREN() { return getToken(FifthParser.OPENPAREN, 0); }
		public ExpContext exp() {
			return getRuleContext(ExpContext.class,0);
		}
		public TerminalNode CLOSEPAREN() { return getToken(FifthParser.CLOSEPAREN, 0); }
		public EFuncParenContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class EAddContext extends ExpContext {
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public TerminalNode PLUS() { return getToken(FifthParser.PLUS, 0); }
		public EAddContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class EStringContext extends ExpContext {
		public TerminalNode STRING() { return getToken(FifthParser.STRING, 0); }
		public EStringContext(ExpContext ctx) { copyFrom(ctx); }
	}
	public static class EMulContext extends ExpContext {
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public TerminalNode TIMES() { return getToken(FifthParser.TIMES, 0); }
		public EMulContext(ExpContext ctx) { copyFrom(ctx); }
	}

	public final ExpContext exp() throws RecognitionException {
		return exp(0);
	}

	private ExpContext exp(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExpContext _localctx = new ExpContext(_ctx, _parentState);
		ExpContext _prevctx = _localctx;
		int _startState = 6;
		enterRecursionRule(_localctx, 6, RULE_exp, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(114);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,7,_ctx) ) {
			case 1:
				{
				_localctx = new EIntContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(88);
				match(INT);
				}
				break;
			case 2:
				{
				_localctx = new EDoubleContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(89);
				match(FLOAT);
				}
				break;
			case 3:
				{
				_localctx = new EStringContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(90);
				match(STRING);
				}
				break;
			case 4:
				{
				_localctx = new EVarnameContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(91);
				var_name();
				}
				break;
			case 5:
				{
				_localctx = new EFuncCallContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(92);
				function_name();
				setState(93);
				match(OPENPAREN);
				setState(102);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NEW) | (1L << OPENPAREN) | (1L << NOT) | (1L << IDENTIFIER) | (1L << STRING) | (1L << INT) | (1L << FLOAT))) != 0)) {
					{
					setState(94);
					exp(0);
					setState(99);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==COMMA) {
						{
						{
						setState(95);
						match(COMMA);
						setState(96);
						exp(0);
						}
						}
						setState(101);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
				}

				setState(104);
				match(CLOSEPAREN);
				}
				break;
			case 6:
				{
				_localctx = new EFuncParenContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(106);
				match(OPENPAREN);
				setState(107);
				exp(0);
				setState(108);
				match(CLOSEPAREN);
				}
				break;
			case 7:
				{
				_localctx = new ENegationContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(110);
				match(NOT);
				setState(111);
				exp(2);
				}
				break;
			case 8:
				{
				_localctx = new ETypeCreateContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(112);
				match(NEW);
				setState(113);
				type_initialiser();
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(145);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,9,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(143);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,8,_ctx) ) {
					case 1:
						{
						_localctx = new ELTContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(116);
						if (!(precpred(_ctx, 17))) throw new FailedPredicateException(this, "precpred(_ctx, 17)");
						setState(117);
						match(LT);
						setState(118);
						exp(18);
						}
						break;
					case 2:
						{
						_localctx = new EGTContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(119);
						if (!(precpred(_ctx, 16))) throw new FailedPredicateException(this, "precpred(_ctx, 16)");
						setState(120);
						match(GT);
						setState(121);
						exp(17);
						}
						break;
					case 3:
						{
						_localctx = new ELEQContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(122);
						if (!(precpred(_ctx, 15))) throw new FailedPredicateException(this, "precpred(_ctx, 15)");
						setState(123);
						match(LEQ);
						setState(124);
						exp(16);
						}
						break;
					case 4:
						{
						_localctx = new EGEQContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(125);
						if (!(precpred(_ctx, 14))) throw new FailedPredicateException(this, "precpred(_ctx, 14)");
						setState(126);
						match(GEQ);
						setState(127);
						exp(15);
						}
						break;
					case 5:
						{
						_localctx = new EAndContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(128);
						if (!(precpred(_ctx, 13))) throw new FailedPredicateException(this, "precpred(_ctx, 13)");
						setState(129);
						match(AND);
						setState(130);
						exp(14);
						}
						break;
					case 6:
						{
						_localctx = new EAddContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(131);
						if (!(precpred(_ctx, 12))) throw new FailedPredicateException(this, "precpred(_ctx, 12)");
						setState(132);
						match(PLUS);
						setState(133);
						exp(13);
						}
						break;
					case 7:
						{
						_localctx = new ESubContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(134);
						if (!(precpred(_ctx, 11))) throw new FailedPredicateException(this, "precpred(_ctx, 11)");
						setState(135);
						match(MINUS);
						setState(136);
						exp(12);
						}
						break;
					case 8:
						{
						_localctx = new EMulContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(137);
						if (!(precpred(_ctx, 10))) throw new FailedPredicateException(this, "precpred(_ctx, 10)");
						setState(138);
						match(TIMES);
						setState(139);
						exp(11);
						}
						break;
					case 9:
						{
						_localctx = new EDivContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(140);
						if (!(precpred(_ctx, 9))) throw new FailedPredicateException(this, "precpred(_ctx, 9)");
						setState(141);
						match(DIVIDE);
						setState(142);
						exp(10);
						}
						break;
					}
					} 
				}
				setState(147);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,9,_ctx);
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
			setState(148);
			parameter_declaration();
			setState(153);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(149);
				match(COMMA);
				setState(150);
				parameter_declaration();
				}
				}
				setState(155);
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
			setState(156);
			function_name();
			setState(157);
			function_args();
			setState(158);
			function_body();
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
			setState(160);
			match(OPENPAREN);
			setState(162);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==IDENTIFIER) {
				{
				setState(161);
				formal_parameters();
				}
			}

			setState(164);
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
		public BlockContext block() {
			return getRuleContext(BlockContext.class,0);
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
			setState(166);
			block();
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
			setState(168);
			function_name();
			setState(169);
			match(OPENPAREN);
			setState(170);
			exp(0);
			setState(175);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(171);
				match(COMMA);
				setState(172);
				exp(0);
				}
				}
				setState(177);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(178);
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
			setState(180);
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

	public static class IriContext extends ParserRuleContext {
		public List<TerminalNode> IDENTIFIER() { return getTokens(FifthParser.IDENTIFIER); }
		public TerminalNode IDENTIFIER(int i) {
			return getToken(FifthParser.IDENTIFIER, i);
		}
		public TerminalNode COLON() { return getToken(FifthParser.COLON, 0); }
		public TerminalNode QMARK() { return getToken(FifthParser.QMARK, 0); }
		public Iri_queryContext iri_query() {
			return getRuleContext(Iri_queryContext.class,0);
		}
		public TerminalNode HASH() { return getToken(FifthParser.HASH, 0); }
		public IriContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_iri; }
	}

	public final IriContext iri() throws RecognitionException {
		IriContext _localctx = new IriContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_iri);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(182);
			match(IDENTIFIER);
			setState(183);
			match(COLON);
			setState(184);
			match(IDENTIFIER);
			setState(187);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==QMARK) {
				{
				setState(185);
				match(QMARK);
				setState(186);
				iri_query();
				}
			}

			setState(191);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==HASH) {
				{
				setState(189);
				match(HASH);
				setState(190);
				match(IDENTIFIER);
				}
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

	public static class Iri_queryContext extends ParserRuleContext {
		public List<Iri_query_paramContext> iri_query_param() {
			return getRuleContexts(Iri_query_paramContext.class);
		}
		public Iri_query_paramContext iri_query_param(int i) {
			return getRuleContext(Iri_query_paramContext.class,i);
		}
		public List<TerminalNode> AMP() { return getTokens(FifthParser.AMP); }
		public TerminalNode AMP(int i) {
			return getToken(FifthParser.AMP, i);
		}
		public Iri_queryContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_iri_query; }
	}

	public final Iri_queryContext iri_query() throws RecognitionException {
		Iri_queryContext _localctx = new Iri_queryContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_iri_query);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(193);
			iri_query_param();
			setState(198);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==AMP) {
				{
				{
				setState(194);
				match(AMP);
				setState(195);
				iri_query_param();
				}
				}
				setState(200);
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

	public static class Iri_query_paramContext extends ParserRuleContext {
		public List<TerminalNode> IDENTIFIER() { return getTokens(FifthParser.IDENTIFIER); }
		public TerminalNode IDENTIFIER(int i) {
			return getToken(FifthParser.IDENTIFIER, i);
		}
		public TerminalNode ASSIGN() { return getToken(FifthParser.ASSIGN, 0); }
		public Iri_query_paramContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_iri_query_param; }
	}

	public final Iri_query_paramContext iri_query_param() throws RecognitionException {
		Iri_query_paramContext _localctx = new Iri_query_paramContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_iri_query_param);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(201);
			match(IDENTIFIER);
			setState(202);
			match(ASSIGN);
			setState(203);
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
		public List<Module_nameContext> module_name() {
			return getRuleContexts(Module_nameContext.class);
		}
		public Module_nameContext module_name(int i) {
			return getRuleContext(Module_nameContext.class,i);
		}
		public TerminalNode SEMICOLON() { return getToken(FifthParser.SEMICOLON, 0); }
		public List<TerminalNode> COMMA() { return getTokens(FifthParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(FifthParser.COMMA, i);
		}
		public Module_importContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_module_import; }
	}

	public final Module_importContext module_import() throws RecognitionException {
		Module_importContext _localctx = new Module_importContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_module_import);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(205);
			match(USE);
			setState(206);
			module_name();
			setState(211);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(207);
				match(COMMA);
				setState(208);
				module_name();
				}
				}
				setState(213);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(214);
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

	public static class Module_nameContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(FifthParser.IDENTIFIER, 0); }
		public Module_nameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_module_name; }
	}

	public final Module_nameContext module_name() throws RecognitionException {
		Module_nameContext _localctx = new Module_nameContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_module_name);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(216);
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
		enterRule(_localctx, 30, RULE_packagename);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(218);
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

	public static class Parameter_declarationContext extends ParserRuleContext {
		public Type_nameContext type_name() {
			return getRuleContext(Type_nameContext.class,0);
		}
		public Var_nameContext var_name() {
			return getRuleContext(Var_nameContext.class,0);
		}
		public Parameter_declarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameter_declaration; }
	}

	public final Parameter_declarationContext parameter_declaration() throws RecognitionException {
		Parameter_declarationContext _localctx = new Parameter_declarationContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_parameter_declaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(220);
			type_name();
			setState(221);
			var_name();
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
		enterRule(_localctx, 34, RULE_parameter_type);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(223);
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
		enterRule(_localctx, 36, RULE_parameter_name);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(225);
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

	public static class StatementContext extends ParserRuleContext {
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
	 
		public StatementContext() { }
		public void copyFrom(StatementContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class ExpStmtContext extends StatementContext {
		public ExpContext exp() {
			return getRuleContext(ExpContext.class,0);
		}
		public TerminalNode SEMICOLON() { return getToken(FifthParser.SEMICOLON, 0); }
		public ExpStmtContext(StatementContext ctx) { copyFrom(ctx); }
	}
	public static class IfStmtContext extends StatementContext {
		public TerminalNode IF() { return getToken(FifthParser.IF, 0); }
		public TerminalNode OPENPAREN() { return getToken(FifthParser.OPENPAREN, 0); }
		public ExpContext exp() {
			return getRuleContext(ExpContext.class,0);
		}
		public TerminalNode CLOSEPAREN() { return getToken(FifthParser.CLOSEPAREN, 0); }
		public BlockContext block() {
			return getRuleContext(BlockContext.class,0);
		}
		public IfStmtContext(StatementContext ctx) { copyFrom(ctx); }
	}
	public static class IfElseStmtContext extends StatementContext {
		public TerminalNode IF() { return getToken(FifthParser.IF, 0); }
		public TerminalNode OPENPAREN() { return getToken(FifthParser.OPENPAREN, 0); }
		public ExpContext exp() {
			return getRuleContext(ExpContext.class,0);
		}
		public TerminalNode CLOSEPAREN() { return getToken(FifthParser.CLOSEPAREN, 0); }
		public List<BlockContext> block() {
			return getRuleContexts(BlockContext.class);
		}
		public BlockContext block(int i) {
			return getRuleContext(BlockContext.class,i);
		}
		public TerminalNode ELSE() { return getToken(FifthParser.ELSE, 0); }
		public IfElseStmtContext(StatementContext ctx) { copyFrom(ctx); }
	}
	public static class VarDeclStmtContext extends StatementContext {
		public Type_nameContext type_name() {
			return getRuleContext(Type_nameContext.class,0);
		}
		public Var_nameContext var_name() {
			return getRuleContext(Var_nameContext.class,0);
		}
		public TerminalNode SEMICOLON() { return getToken(FifthParser.SEMICOLON, 0); }
		public TerminalNode ASSIGN() { return getToken(FifthParser.ASSIGN, 0); }
		public ExpContext exp() {
			return getRuleContext(ExpContext.class,0);
		}
		public VarDeclStmtContext(StatementContext ctx) { copyFrom(ctx); }
	}
	public static class WithStmtContext extends StatementContext {
		public TerminalNode WITH() { return getToken(FifthParser.WITH, 0); }
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public TerminalNode SEMICOLON() { return getToken(FifthParser.SEMICOLON, 0); }
		public WithStmtContext(StatementContext ctx) { copyFrom(ctx); }
	}
	public static class AssignmentStmtContext extends StatementContext {
		public Var_nameContext var_name() {
			return getRuleContext(Var_nameContext.class,0);
		}
		public TerminalNode ASSIGN() { return getToken(FifthParser.ASSIGN, 0); }
		public ExpContext exp() {
			return getRuleContext(ExpContext.class,0);
		}
		public TerminalNode SEMICOLON() { return getToken(FifthParser.SEMICOLON, 0); }
		public AssignmentStmtContext(StatementContext ctx) { copyFrom(ctx); }
	}
	public static class ReturnStmtContext extends StatementContext {
		public TerminalNode RETURN() { return getToken(FifthParser.RETURN, 0); }
		public ExpContext exp() {
			return getRuleContext(ExpContext.class,0);
		}
		public TerminalNode SEMICOLON() { return getToken(FifthParser.SEMICOLON, 0); }
		public ReturnStmtContext(StatementContext ctx) { copyFrom(ctx); }
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_statement);
		int _la;
		try {
			setState(265);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,18,_ctx) ) {
			case 1:
				_localctx = new VarDeclStmtContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(227);
				type_name();
				setState(228);
				var_name();
				setState(231);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==ASSIGN) {
					{
					setState(229);
					match(ASSIGN);
					setState(230);
					exp(0);
					}
				}

				setState(233);
				match(SEMICOLON);
				}
				break;
			case 2:
				_localctx = new AssignmentStmtContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(235);
				var_name();
				setState(236);
				match(ASSIGN);
				setState(237);
				exp(0);
				setState(238);
				match(SEMICOLON);
				}
				break;
			case 3:
				_localctx = new ReturnStmtContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(240);
				match(RETURN);
				setState(241);
				exp(0);
				setState(242);
				match(SEMICOLON);
				}
				break;
			case 4:
				_localctx = new IfStmtContext(_localctx);
				enterOuterAlt(_localctx, 4);
				{
				setState(244);
				match(IF);
				setState(245);
				match(OPENPAREN);
				setState(246);
				exp(0);
				setState(247);
				match(CLOSEPAREN);
				setState(248);
				block();
				}
				break;
			case 5:
				_localctx = new IfElseStmtContext(_localctx);
				enterOuterAlt(_localctx, 5);
				{
				setState(250);
				match(IF);
				setState(251);
				match(OPENPAREN);
				setState(252);
				exp(0);
				setState(253);
				match(CLOSEPAREN);
				setState(254);
				block();
				setState(255);
				match(ELSE);
				setState(256);
				block();
				}
				break;
			case 6:
				_localctx = new WithStmtContext(_localctx);
				enterOuterAlt(_localctx, 6);
				{
				setState(258);
				match(WITH);
				setState(259);
				statement();
				setState(260);
				match(SEMICOLON);
				}
				break;
			case 7:
				_localctx = new ExpStmtContext(_localctx);
				enterOuterAlt(_localctx, 7);
				{
				setState(262);
				exp(0);
				setState(263);
				match(SEMICOLON);
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
		enterRule(_localctx, 40, RULE_type_initialiser);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(267);
			type_name();
			setState(268);
			match(OPENBRACE);
			setState(272);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==IDENTIFIER) {
				{
				{
				setState(269);
				type_property_init();
				}
				}
				setState(274);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(275);
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
		public TerminalNode IDENTIFIER() { return getToken(FifthParser.IDENTIFIER, 0); }
		public Type_nameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type_name; }
	}

	public final Type_nameContext type_name() throws RecognitionException {
		Type_nameContext _localctx = new Type_nameContext(_ctx, getState());
		enterRule(_localctx, 42, RULE_type_name);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(277);
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
		enterRule(_localctx, 44, RULE_type_property_init);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(279);
			var_name();
			setState(280);
			match(ASSIGN);
			setState(281);
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
		enterRule(_localctx, 46, RULE_var_name);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(283);
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
		case 3:
			return exp_sempred((ExpContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean exp_sempred(ExpContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 17);
		case 1:
			return precpred(_ctx, 16);
		case 2:
			return precpred(_ctx, 15);
		case 3:
			return precpred(_ctx, 14);
		case 4:
			return precpred(_ctx, 13);
		case 5:
			return precpred(_ctx, 12);
		case 6:
			return precpred(_ctx, 11);
		case 7:
			return precpred(_ctx, 10);
		case 8:
			return precpred(_ctx, 9);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3+\u0120\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\3\2\7\2\64\n\2\f\2\16\2\67\13\2\3\2\7\2:\n\2\f\2\16\2=\13\2\3\2\7\2@"+
		"\n\2\f\2\16\2C\13\2\3\2\7\2F\n\2\f\2\16\2I\13\2\3\3\3\3\3\3\3\3\3\3\3"+
		"\3\3\4\3\4\7\4S\n\4\f\4\16\4V\13\4\3\4\3\4\3\5\3\5\3\5\3\5\3\5\3\5\3\5"+
		"\3\5\3\5\3\5\7\5d\n\5\f\5\16\5g\13\5\5\5i\n\5\3\5\3\5\3\5\3\5\3\5\3\5"+
		"\3\5\3\5\3\5\3\5\5\5u\n\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5"+
		"\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\7\5\u0092"+
		"\n\5\f\5\16\5\u0095\13\5\3\6\3\6\3\6\7\6\u009a\n\6\f\6\16\6\u009d\13\6"+
		"\3\7\3\7\3\7\3\7\3\b\3\b\5\b\u00a5\n\b\3\b\3\b\3\t\3\t\3\n\3\n\3\n\3\n"+
		"\3\n\7\n\u00b0\n\n\f\n\16\n\u00b3\13\n\3\n\3\n\3\13\3\13\3\f\3\f\3\f\3"+
		"\f\3\f\5\f\u00be\n\f\3\f\3\f\5\f\u00c2\n\f\3\r\3\r\3\r\7\r\u00c7\n\r\f"+
		"\r\16\r\u00ca\13\r\3\16\3\16\3\16\3\16\3\17\3\17\3\17\3\17\7\17\u00d4"+
		"\n\17\f\17\16\17\u00d7\13\17\3\17\3\17\3\20\3\20\3\21\3\21\3\22\3\22\3"+
		"\22\3\23\3\23\3\24\3\24\3\25\3\25\3\25\3\25\5\25\u00ea\n\25\3\25\3\25"+
		"\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25"+
		"\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25"+
		"\3\25\3\25\5\25\u010c\n\25\3\26\3\26\3\26\7\26\u0111\n\26\f\26\16\26\u0114"+
		"\13\26\3\26\3\26\3\27\3\27\3\30\3\30\3\30\3\30\3\31\3\31\3\31\2\3\b\32"+
		"\2\4\6\b\n\f\16\20\22\24\26\30\32\34\36 \"$&(*,.\60\2\2\2\u012d\2\65\3"+
		"\2\2\2\4J\3\2\2\2\6P\3\2\2\2\bt\3\2\2\2\n\u0096\3\2\2\2\f\u009e\3\2\2"+
		"\2\16\u00a2\3\2\2\2\20\u00a8\3\2\2\2\22\u00aa\3\2\2\2\24\u00b6\3\2\2\2"+
		"\26\u00b8\3\2\2\2\30\u00c3\3\2\2\2\32\u00cb\3\2\2\2\34\u00cf\3\2\2\2\36"+
		"\u00da\3\2\2\2 \u00dc\3\2\2\2\"\u00de\3\2\2\2$\u00e1\3\2\2\2&\u00e3\3"+
		"\2\2\2(\u010b\3\2\2\2*\u010d\3\2\2\2,\u0117\3\2\2\2.\u0119\3\2\2\2\60"+
		"\u011d\3\2\2\2\62\64\5\34\17\2\63\62\3\2\2\2\64\67\3\2\2\2\65\63\3\2\2"+
		"\2\65\66\3\2\2\2\66;\3\2\2\2\67\65\3\2\2\28:\5\4\3\298\3\2\2\2:=\3\2\2"+
		"\2;9\3\2\2\2;<\3\2\2\2<A\3\2\2\2=;\3\2\2\2>@\5(\25\2?>\3\2\2\2@C\3\2\2"+
		"\2A?\3\2\2\2AB\3\2\2\2BG\3\2\2\2CA\3\2\2\2DF\5\f\7\2ED\3\2\2\2FI\3\2\2"+
		"\2GE\3\2\2\2GH\3\2\2\2H\3\3\2\2\2IG\3\2\2\2JK\7\3\2\2KL\5\26\f\2LM\7\4"+
		"\2\2MN\5 \21\2NO\7&\2\2O\5\3\2\2\2PT\7\26\2\2QS\5(\25\2RQ\3\2\2\2SV\3"+
		"\2\2\2TR\3\2\2\2TU\3\2\2\2UW\3\2\2\2VT\3\2\2\2WX\7\f\2\2X\7\3\2\2\2YZ"+
		"\b\5\1\2Zu\7)\2\2[u\7*\2\2\\u\7(\2\2]u\5\60\31\2^_\5\24\13\2_h\7\27\2"+
		"\2`e\5\b\5\2ab\7\17\2\2bd\5\b\5\2ca\3\2\2\2dg\3\2\2\2ec\3\2\2\2ef\3\2"+
		"\2\2fi\3\2\2\2ge\3\2\2\2h`\3\2\2\2hi\3\2\2\2ij\3\2\2\2jk\7\r\2\2ku\3\2"+
		"\2\2lm\7\27\2\2mn\5\b\5\2no\7\r\2\2ou\3\2\2\2pq\7%\2\2qu\5\b\5\4rs\7\7"+
		"\2\2su\5*\26\2tY\3\2\2\2t[\3\2\2\2t\\\3\2\2\2t]\3\2\2\2t^\3\2\2\2tl\3"+
		"\2\2\2tp\3\2\2\2tr\3\2\2\2u\u0093\3\2\2\2vw\f\23\2\2wx\7\37\2\2x\u0092"+
		"\5\b\5\24yz\f\22\2\2z{\7\36\2\2{\u0092\5\b\5\23|}\f\21\2\2}~\7!\2\2~\u0092"+
		"\5\b\5\22\177\u0080\f\20\2\2\u0080\u0081\7 \2\2\u0081\u0092\5\b\5\21\u0082"+
		"\u0083\f\17\2\2\u0083\u0084\7#\2\2\u0084\u0092\5\b\5\20\u0085\u0086\f"+
		"\16\2\2\u0086\u0087\7\30\2\2\u0087\u0092\5\b\5\17\u0088\u0089\f\r\2\2"+
		"\u0089\u008a\7\25\2\2\u008a\u0092\5\b\5\16\u008b\u008c\f\f\2\2\u008c\u008d"+
		"\7\32\2\2\u008d\u0092\5\b\5\r\u008e\u008f\f\13\2\2\u008f\u0090\7\20\2"+
		"\2\u0090\u0092\5\b\5\f\u0091v\3\2\2\2\u0091y\3\2\2\2\u0091|\3\2\2\2\u0091"+
		"\177\3\2\2\2\u0091\u0082\3\2\2\2\u0091\u0085\3\2\2\2\u0091\u0088\3\2\2"+
		"\2\u0091\u008b\3\2\2\2\u0091\u008e\3\2\2\2\u0092\u0095\3\2\2\2\u0093\u0091"+
		"\3\2\2\2\u0093\u0094\3\2\2\2\u0094\t\3\2\2\2\u0095\u0093\3\2\2\2\u0096"+
		"\u009b\5\"\22\2\u0097\u0098\7\17\2\2\u0098\u009a\5\"\22\2\u0099\u0097"+
		"\3\2\2\2\u009a\u009d\3\2\2\2\u009b\u0099\3\2\2\2\u009b\u009c\3\2\2\2\u009c"+
		"\13\3\2\2\2\u009d\u009b\3\2\2\2\u009e\u009f\5\24\13\2\u009f\u00a0\5\16"+
		"\b\2\u00a0\u00a1\5\20\t\2\u00a1\r\3\2\2\2\u00a2\u00a4\7\27\2\2\u00a3\u00a5"+
		"\5\n\6\2\u00a4\u00a3\3\2\2\2\u00a4\u00a5\3\2\2\2\u00a5\u00a6\3\2\2\2\u00a6"+
		"\u00a7\7\r\2\2\u00a7\17\3\2\2\2\u00a8\u00a9\5\6\4\2\u00a9\21\3\2\2\2\u00aa"+
		"\u00ab\5\24\13\2\u00ab\u00ac\7\27\2\2\u00ac\u00b1\5\b\5\2\u00ad\u00ae"+
		"\7\17\2\2\u00ae\u00b0\5\b\5\2\u00af\u00ad\3\2\2\2\u00b0\u00b3\3\2\2\2"+
		"\u00b1\u00af\3\2\2\2\u00b1\u00b2\3\2\2\2\u00b2\u00b4\3\2\2\2\u00b3\u00b1"+
		"\3\2\2\2\u00b4\u00b5\7\r\2\2\u00b5\23\3\2\2\2\u00b6\u00b7\7\'\2\2\u00b7"+
		"\25\3\2\2\2\u00b8\u00b9\7\'\2\2\u00b9\u00ba\7\16\2\2\u00ba\u00bd\7\'\2"+
		"\2\u00bb\u00bc\7\31\2\2\u00bc\u00be\5\30\r\2\u00bd\u00bb\3\2\2\2\u00bd"+
		"\u00be\3\2\2\2\u00be\u00c1\3\2\2\2\u00bf\u00c0\7\23\2\2\u00c0\u00c2\7"+
		"\'\2\2\u00c1\u00bf\3\2\2\2\u00c1\u00c2\3\2\2\2\u00c2\27\3\2\2\2\u00c3"+
		"\u00c8\5\32\16\2\u00c4\u00c5\7\"\2\2\u00c5\u00c7\5\32\16\2\u00c6\u00c4"+
		"\3\2\2\2\u00c7\u00ca\3\2\2\2\u00c8\u00c6\3\2\2\2\u00c8\u00c9\3\2\2\2\u00c9"+
		"\31\3\2\2\2\u00ca\u00c8\3\2\2\2\u00cb\u00cc\7\'\2\2\u00cc\u00cd\7\13\2"+
		"\2\u00cd\u00ce\7\'\2\2\u00ce\33\3\2\2\2\u00cf\u00d0\7\n\2\2\u00d0\u00d5"+
		"\5\36\20\2\u00d1\u00d2\7\17\2\2\u00d2\u00d4\5\36\20\2\u00d3\u00d1\3\2"+
		"\2\2\u00d4\u00d7\3\2\2\2\u00d5\u00d3\3\2\2\2\u00d5\u00d6\3\2\2\2\u00d6"+
		"\u00d8\3\2\2\2\u00d7\u00d5\3\2\2\2\u00d8\u00d9\7&\2\2\u00d9\35\3\2\2\2"+
		"\u00da\u00db\7\'\2\2\u00db\37\3\2\2\2\u00dc\u00dd\7\'\2\2\u00dd!\3\2\2"+
		"\2\u00de\u00df\5,\27\2\u00df\u00e0\5\60\31\2\u00e0#\3\2\2\2\u00e1\u00e2"+
		"\7\'\2\2\u00e2%\3\2\2\2\u00e3\u00e4\7\'\2\2\u00e4\'\3\2\2\2\u00e5\u00e6"+
		"\5,\27\2\u00e6\u00e9\5\60\31\2\u00e7\u00e8\7\13\2\2\u00e8\u00ea\5\b\5"+
		"\2\u00e9\u00e7\3\2\2\2\u00e9\u00ea\3\2\2\2\u00ea\u00eb\3\2\2\2\u00eb\u00ec"+
		"\7&\2\2\u00ec\u010c\3\2\2\2\u00ed\u00ee\5\60\31\2\u00ee\u00ef\7\13\2\2"+
		"\u00ef\u00f0\5\b\5\2\u00f0\u00f1\7&\2\2\u00f1\u010c\3\2\2\2\u00f2\u00f3"+
		"\7\t\2\2\u00f3\u00f4\5\b\5\2\u00f4\u00f5\7&\2\2\u00f5\u010c\3\2\2\2\u00f6"+
		"\u00f7\7\6\2\2\u00f7\u00f8\7\27\2\2\u00f8\u00f9\5\b\5\2\u00f9\u00fa\7"+
		"\r\2\2\u00fa\u00fb\5\6\4\2\u00fb\u010c\3\2\2\2\u00fc\u00fd\7\6\2\2\u00fd"+
		"\u00fe\7\27\2\2\u00fe\u00ff\5\b\5\2\u00ff\u0100\7\r\2\2\u0100\u0101\5"+
		"\6\4\2\u0101\u0102\7\5\2\2\u0102\u0103\5\6\4\2\u0103\u010c\3\2\2\2\u0104"+
		"\u0105\7\b\2\2\u0105\u0106\5(\25\2\u0106\u0107\7&\2\2\u0107\u010c\3\2"+
		"\2\2\u0108\u0109\5\b\5\2\u0109\u010a\7&\2\2\u010a\u010c\3\2\2\2\u010b"+
		"\u00e5\3\2\2\2\u010b\u00ed\3\2\2\2\u010b\u00f2\3\2\2\2\u010b\u00f6\3\2"+
		"\2\2\u010b\u00fc\3\2\2\2\u010b\u0104\3\2\2\2\u010b\u0108\3\2\2\2\u010c"+
		")\3\2\2\2\u010d\u010e\5,\27\2\u010e\u0112\7\26\2\2\u010f\u0111\5.\30\2"+
		"\u0110\u010f\3\2\2\2\u0111\u0114\3\2\2\2\u0112\u0110\3\2\2\2\u0112\u0113"+
		"\3\2\2\2\u0113\u0115\3\2\2\2\u0114\u0112\3\2\2\2\u0115\u0116\7\f\2\2\u0116"+
		"+\3\2\2\2\u0117\u0118\7\'\2\2\u0118-\3\2\2\2\u0119\u011a\5\60\31\2\u011a"+
		"\u011b\7\13\2\2\u011b\u011c\5\b\5\2\u011c/\3\2\2\2\u011d\u011e\7\'\2\2"+
		"\u011e\61\3\2\2\2\26\65;AGTeht\u0091\u0093\u009b\u00a4\u00b1\u00bd\u00c1"+
		"\u00c8\u00d5\u00e9\u010b\u0112";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}