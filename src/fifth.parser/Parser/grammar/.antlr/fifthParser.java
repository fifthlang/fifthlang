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
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << IF) | (1L << WITH) | (1L << RETURN) | (1L << IDENTIFIER))) != 0)) {
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
	public static class EStatementContext extends ExpContext {
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public EStatementContext(ExpContext ctx) { copyFrom(ctx); }
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
			setState(115);
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
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << IF) | (1L << NEW) | (1L << WITH) | (1L << RETURN) | (1L << OPENPAREN) | (1L << NOT) | (1L << IDENTIFIER) | (1L << STRING) | (1L << INT) | (1L << FLOAT))) != 0)) {
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
				exp(3);
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
			case 9:
				{
				_localctx = new EStatementContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(114);
				statement();
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(146);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,9,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(144);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,8,_ctx) ) {
					case 1:
						{
						_localctx = new ELTContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(117);
						if (!(precpred(_ctx, 18))) throw new FailedPredicateException(this, "precpred(_ctx, 18)");
						setState(118);
						match(LT);
						setState(119);
						exp(19);
						}
						break;
					case 2:
						{
						_localctx = new EGTContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(120);
						if (!(precpred(_ctx, 17))) throw new FailedPredicateException(this, "precpred(_ctx, 17)");
						setState(121);
						match(GT);
						setState(122);
						exp(18);
						}
						break;
					case 3:
						{
						_localctx = new ELEQContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(123);
						if (!(precpred(_ctx, 16))) throw new FailedPredicateException(this, "precpred(_ctx, 16)");
						setState(124);
						match(LEQ);
						setState(125);
						exp(17);
						}
						break;
					case 4:
						{
						_localctx = new EGEQContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(126);
						if (!(precpred(_ctx, 15))) throw new FailedPredicateException(this, "precpred(_ctx, 15)");
						setState(127);
						match(GEQ);
						setState(128);
						exp(16);
						}
						break;
					case 5:
						{
						_localctx = new EAndContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(129);
						if (!(precpred(_ctx, 14))) throw new FailedPredicateException(this, "precpred(_ctx, 14)");
						setState(130);
						match(AND);
						setState(131);
						exp(15);
						}
						break;
					case 6:
						{
						_localctx = new EAddContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(132);
						if (!(precpred(_ctx, 13))) throw new FailedPredicateException(this, "precpred(_ctx, 13)");
						setState(133);
						match(PLUS);
						setState(134);
						exp(14);
						}
						break;
					case 7:
						{
						_localctx = new ESubContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(135);
						if (!(precpred(_ctx, 12))) throw new FailedPredicateException(this, "precpred(_ctx, 12)");
						setState(136);
						match(MINUS);
						setState(137);
						exp(13);
						}
						break;
					case 8:
						{
						_localctx = new EMulContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(138);
						if (!(precpred(_ctx, 11))) throw new FailedPredicateException(this, "precpred(_ctx, 11)");
						setState(139);
						match(TIMES);
						setState(140);
						exp(12);
						}
						break;
					case 9:
						{
						_localctx = new EDivContext(new ExpContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(141);
						if (!(precpred(_ctx, 10))) throw new FailedPredicateException(this, "precpred(_ctx, 10)");
						setState(142);
						match(DIVIDE);
						setState(143);
						exp(11);
						}
						break;
					}
					} 
				}
				setState(148);
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
			setState(149);
			parameter_declaration();
			setState(154);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(150);
				match(COMMA);
				setState(151);
				parameter_declaration();
				}
				}
				setState(156);
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
			setState(157);
			function_name();
			setState(158);
			function_args();
			setState(159);
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
			setState(161);
			match(OPENPAREN);
			setState(163);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==IDENTIFIER) {
				{
				setState(162);
				formal_parameters();
				}
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

	public static class Function_bodyContext extends ParserRuleContext {
		public TerminalNode LAMBDASEP() { return getToken(FifthParser.LAMBDASEP, 0); }
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public TerminalNode SEMICOLON() { return getToken(FifthParser.SEMICOLON, 0); }
		public List<TerminalNode> COMMA() { return getTokens(FifthParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(FifthParser.COMMA, i);
		}
		public Function_bodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_body; }
	}

	public final Function_bodyContext function_body() throws RecognitionException {
		Function_bodyContext _localctx = new Function_bodyContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_function_body);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(167);
			match(LAMBDASEP);
			setState(168);
			exp(0);
			setState(173);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(169);
				match(COMMA);
				setState(170);
				exp(0);
				}
				}
				setState(175);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(176);
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
			setState(178);
			function_name();
			setState(179);
			match(OPENPAREN);
			setState(180);
			exp(0);
			setState(185);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(181);
				match(COMMA);
				setState(182);
				exp(0);
				}
				}
				setState(187);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(188);
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
			setState(190);
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
			setState(192);
			match(IDENTIFIER);
			setState(193);
			match(COLON);
			setState(194);
			match(IDENTIFIER);
			setState(197);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==QMARK) {
				{
				setState(195);
				match(QMARK);
				setState(196);
				iri_query();
				}
			}

			setState(201);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==HASH) {
				{
				setState(199);
				match(HASH);
				setState(200);
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
			setState(203);
			iri_query_param();
			setState(208);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==AMP) {
				{
				{
				setState(204);
				match(AMP);
				setState(205);
				iri_query_param();
				}
				}
				setState(210);
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
			setState(211);
			match(IDENTIFIER);
			setState(212);
			match(ASSIGN);
			setState(213);
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
			setState(215);
			match(USE);
			setState(216);
			module_name();
			setState(221);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(217);
				match(COMMA);
				setState(218);
				module_name();
				}
				}
				setState(223);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(224);
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
			setState(226);
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
			setState(228);
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
			setState(230);
			type_name();
			setState(231);
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
			setState(233);
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
			setState(235);
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
		public BlockContext block() {
			return getRuleContext(BlockContext.class,0);
		}
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
		public AssignmentStmtContext(StatementContext ctx) { copyFrom(ctx); }
	}
	public static class ReturnStmtContext extends StatementContext {
		public TerminalNode RETURN() { return getToken(FifthParser.RETURN, 0); }
		public ExpContext exp() {
			return getRuleContext(ExpContext.class,0);
		}
		public ReturnStmtContext(StatementContext ctx) { copyFrom(ctx); }
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_statement);
		try {
			setState(261);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,19,_ctx) ) {
			case 1:
				_localctx = new VarDeclStmtContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(237);
				type_name();
				setState(238);
				var_name();
				setState(241);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,18,_ctx) ) {
				case 1:
					{
					setState(239);
					match(ASSIGN);
					setState(240);
					exp(0);
					}
					break;
				}
				}
				break;
			case 2:
				_localctx = new AssignmentStmtContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(243);
				var_name();
				setState(244);
				match(ASSIGN);
				setState(245);
				exp(0);
				}
				break;
			case 3:
				_localctx = new ReturnStmtContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(247);
				match(RETURN);
				setState(248);
				exp(0);
				}
				break;
			case 4:
				_localctx = new IfElseStmtContext(_localctx);
				enterOuterAlt(_localctx, 4);
				{
				setState(249);
				match(IF);
				setState(250);
				match(OPENPAREN);
				setState(251);
				exp(0);
				setState(252);
				match(CLOSEPAREN);
				setState(253);
				block();
				setState(254);
				match(ELSE);
				setState(255);
				block();
				}
				break;
			case 5:
				_localctx = new WithStmtContext(_localctx);
				enterOuterAlt(_localctx, 5);
				{
				setState(257);
				match(WITH);
				setState(258);
				statement();
				setState(259);
				block();
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
			setState(263);
			type_name();
			setState(264);
			match(OPENBRACE);
			setState(268);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==IDENTIFIER) {
				{
				{
				setState(265);
				type_property_init();
				}
				}
				setState(270);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(271);
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
			setState(273);
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
			setState(275);
			var_name();
			setState(276);
			match(ASSIGN);
			setState(277);
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
			setState(279);
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
			return precpred(_ctx, 18);
		case 1:
			return precpred(_ctx, 17);
		case 2:
			return precpred(_ctx, 16);
		case 3:
			return precpred(_ctx, 15);
		case 4:
			return precpred(_ctx, 14);
		case 5:
			return precpred(_ctx, 13);
		case 6:
			return precpred(_ctx, 12);
		case 7:
			return precpred(_ctx, 11);
		case 8:
			return precpred(_ctx, 10);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3+\u011c\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\3\2\7\2\64\n\2\f\2\16\2\67\13\2\3\2\7\2:\n\2\f\2\16\2=\13\2\3\2\7\2@"+
		"\n\2\f\2\16\2C\13\2\3\2\7\2F\n\2\f\2\16\2I\13\2\3\3\3\3\3\3\3\3\3\3\3"+
		"\3\3\4\3\4\7\4S\n\4\f\4\16\4V\13\4\3\4\3\4\3\5\3\5\3\5\3\5\3\5\3\5\3\5"+
		"\3\5\3\5\3\5\7\5d\n\5\f\5\16\5g\13\5\5\5i\n\5\3\5\3\5\3\5\3\5\3\5\3\5"+
		"\3\5\3\5\3\5\3\5\3\5\5\5v\n\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5"+
		"\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\7"+
		"\5\u0093\n\5\f\5\16\5\u0096\13\5\3\6\3\6\3\6\7\6\u009b\n\6\f\6\16\6\u009e"+
		"\13\6\3\7\3\7\3\7\3\7\3\b\3\b\5\b\u00a6\n\b\3\b\3\b\3\t\3\t\3\t\3\t\7"+
		"\t\u00ae\n\t\f\t\16\t\u00b1\13\t\3\t\3\t\3\n\3\n\3\n\3\n\3\n\7\n\u00ba"+
		"\n\n\f\n\16\n\u00bd\13\n\3\n\3\n\3\13\3\13\3\f\3\f\3\f\3\f\3\f\5\f\u00c8"+
		"\n\f\3\f\3\f\5\f\u00cc\n\f\3\r\3\r\3\r\7\r\u00d1\n\r\f\r\16\r\u00d4\13"+
		"\r\3\16\3\16\3\16\3\16\3\17\3\17\3\17\3\17\7\17\u00de\n\17\f\17\16\17"+
		"\u00e1\13\17\3\17\3\17\3\20\3\20\3\21\3\21\3\22\3\22\3\22\3\23\3\23\3"+
		"\24\3\24\3\25\3\25\3\25\3\25\5\25\u00f4\n\25\3\25\3\25\3\25\3\25\3\25"+
		"\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\5\25"+
		"\u0108\n\25\3\26\3\26\3\26\7\26\u010d\n\26\f\26\16\26\u0110\13\26\3\26"+
		"\3\26\3\27\3\27\3\30\3\30\3\30\3\30\3\31\3\31\3\31\2\3\b\32\2\4\6\b\n"+
		"\f\16\20\22\24\26\30\32\34\36 \"$&(*,.\60\2\2\2\u0129\2\65\3\2\2\2\4J"+
		"\3\2\2\2\6P\3\2\2\2\bu\3\2\2\2\n\u0097\3\2\2\2\f\u009f\3\2\2\2\16\u00a3"+
		"\3\2\2\2\20\u00a9\3\2\2\2\22\u00b4\3\2\2\2\24\u00c0\3\2\2\2\26\u00c2\3"+
		"\2\2\2\30\u00cd\3\2\2\2\32\u00d5\3\2\2\2\34\u00d9\3\2\2\2\36\u00e4\3\2"+
		"\2\2 \u00e6\3\2\2\2\"\u00e8\3\2\2\2$\u00eb\3\2\2\2&\u00ed\3\2\2\2(\u0107"+
		"\3\2\2\2*\u0109\3\2\2\2,\u0113\3\2\2\2.\u0115\3\2\2\2\60\u0119\3\2\2\2"+
		"\62\64\5\34\17\2\63\62\3\2\2\2\64\67\3\2\2\2\65\63\3\2\2\2\65\66\3\2\2"+
		"\2\66;\3\2\2\2\67\65\3\2\2\28:\5\4\3\298\3\2\2\2:=\3\2\2\2;9\3\2\2\2;"+
		"<\3\2\2\2<A\3\2\2\2=;\3\2\2\2>@\5(\25\2?>\3\2\2\2@C\3\2\2\2A?\3\2\2\2"+
		"AB\3\2\2\2BG\3\2\2\2CA\3\2\2\2DF\5\f\7\2ED\3\2\2\2FI\3\2\2\2GE\3\2\2\2"+
		"GH\3\2\2\2H\3\3\2\2\2IG\3\2\2\2JK\7\3\2\2KL\5\26\f\2LM\7\4\2\2MN\5 \21"+
		"\2NO\7&\2\2O\5\3\2\2\2PT\7\26\2\2QS\5(\25\2RQ\3\2\2\2SV\3\2\2\2TR\3\2"+
		"\2\2TU\3\2\2\2UW\3\2\2\2VT\3\2\2\2WX\7\f\2\2X\7\3\2\2\2YZ\b\5\1\2Zv\7"+
		")\2\2[v\7*\2\2\\v\7(\2\2]v\5\60\31\2^_\5\24\13\2_h\7\27\2\2`e\5\b\5\2"+
		"ab\7\17\2\2bd\5\b\5\2ca\3\2\2\2dg\3\2\2\2ec\3\2\2\2ef\3\2\2\2fi\3\2\2"+
		"\2ge\3\2\2\2h`\3\2\2\2hi\3\2\2\2ij\3\2\2\2jk\7\r\2\2kv\3\2\2\2lm\7\27"+
		"\2\2mn\5\b\5\2no\7\r\2\2ov\3\2\2\2pq\7%\2\2qv\5\b\5\5rs\7\7\2\2sv\5*\26"+
		"\2tv\5(\25\2uY\3\2\2\2u[\3\2\2\2u\\\3\2\2\2u]\3\2\2\2u^\3\2\2\2ul\3\2"+
		"\2\2up\3\2\2\2ur\3\2\2\2ut\3\2\2\2v\u0094\3\2\2\2wx\f\24\2\2xy\7\37\2"+
		"\2y\u0093\5\b\5\25z{\f\23\2\2{|\7\36\2\2|\u0093\5\b\5\24}~\f\22\2\2~\177"+
		"\7!\2\2\177\u0093\5\b\5\23\u0080\u0081\f\21\2\2\u0081\u0082\7 \2\2\u0082"+
		"\u0093\5\b\5\22\u0083\u0084\f\20\2\2\u0084\u0085\7#\2\2\u0085\u0093\5"+
		"\b\5\21\u0086\u0087\f\17\2\2\u0087\u0088\7\30\2\2\u0088\u0093\5\b\5\20"+
		"\u0089\u008a\f\16\2\2\u008a\u008b\7\25\2\2\u008b\u0093\5\b\5\17\u008c"+
		"\u008d\f\r\2\2\u008d\u008e\7\32\2\2\u008e\u0093\5\b\5\16\u008f\u0090\f"+
		"\f\2\2\u0090\u0091\7\20\2\2\u0091\u0093\5\b\5\r\u0092w\3\2\2\2\u0092z"+
		"\3\2\2\2\u0092}\3\2\2\2\u0092\u0080\3\2\2\2\u0092\u0083\3\2\2\2\u0092"+
		"\u0086\3\2\2\2\u0092\u0089\3\2\2\2\u0092\u008c\3\2\2\2\u0092\u008f\3\2"+
		"\2\2\u0093\u0096\3\2\2\2\u0094\u0092\3\2\2\2\u0094\u0095\3\2\2\2\u0095"+
		"\t\3\2\2\2\u0096\u0094\3\2\2\2\u0097\u009c\5\"\22\2\u0098\u0099\7\17\2"+
		"\2\u0099\u009b\5\"\22\2\u009a\u0098\3\2\2\2\u009b\u009e\3\2\2\2\u009c"+
		"\u009a\3\2\2\2\u009c\u009d\3\2\2\2\u009d\13\3\2\2\2\u009e\u009c\3\2\2"+
		"\2\u009f\u00a0\5\24\13\2\u00a0\u00a1\5\16\b\2\u00a1\u00a2\5\20\t\2\u00a2"+
		"\r\3\2\2\2\u00a3\u00a5\7\27\2\2\u00a4\u00a6\5\n\6\2\u00a5\u00a4\3\2\2"+
		"\2\u00a5\u00a6\3\2\2\2\u00a6\u00a7\3\2\2\2\u00a7\u00a8\7\r\2\2\u00a8\17"+
		"\3\2\2\2\u00a9\u00aa\7\24\2\2\u00aa\u00af\5\b\5\2\u00ab\u00ac\7\17\2\2"+
		"\u00ac\u00ae\5\b\5\2\u00ad\u00ab\3\2\2\2\u00ae\u00b1\3\2\2\2\u00af\u00ad"+
		"\3\2\2\2\u00af\u00b0\3\2\2\2\u00b0\u00b2\3\2\2\2\u00b1\u00af\3\2\2\2\u00b2"+
		"\u00b3\7&\2\2\u00b3\21\3\2\2\2\u00b4\u00b5\5\24\13\2\u00b5\u00b6\7\27"+
		"\2\2\u00b6\u00bb\5\b\5\2\u00b7\u00b8\7\17\2\2\u00b8\u00ba\5\b\5\2\u00b9"+
		"\u00b7\3\2\2\2\u00ba\u00bd\3\2\2\2\u00bb\u00b9\3\2\2\2\u00bb\u00bc\3\2"+
		"\2\2\u00bc\u00be\3\2\2\2\u00bd\u00bb\3\2\2\2\u00be\u00bf\7\r\2\2\u00bf"+
		"\23\3\2\2\2\u00c0\u00c1\7\'\2\2\u00c1\25\3\2\2\2\u00c2\u00c3\7\'\2\2\u00c3"+
		"\u00c4\7\16\2\2\u00c4\u00c7\7\'\2\2\u00c5\u00c6\7\31\2\2\u00c6\u00c8\5"+
		"\30\r\2\u00c7\u00c5\3\2\2\2\u00c7\u00c8\3\2\2\2\u00c8\u00cb\3\2\2\2\u00c9"+
		"\u00ca\7\23\2\2\u00ca\u00cc\7\'\2\2\u00cb\u00c9\3\2\2\2\u00cb\u00cc\3"+
		"\2\2\2\u00cc\27\3\2\2\2\u00cd\u00d2\5\32\16\2\u00ce\u00cf\7\"\2\2\u00cf"+
		"\u00d1\5\32\16\2\u00d0\u00ce\3\2\2\2\u00d1\u00d4\3\2\2\2\u00d2\u00d0\3"+
		"\2\2\2\u00d2\u00d3\3\2\2\2\u00d3\31\3\2\2\2\u00d4\u00d2\3\2\2\2\u00d5"+
		"\u00d6\7\'\2\2\u00d6\u00d7\7\13\2\2\u00d7\u00d8\7\'\2\2\u00d8\33\3\2\2"+
		"\2\u00d9\u00da\7\n\2\2\u00da\u00df\5\36\20\2\u00db\u00dc\7\17\2\2\u00dc"+
		"\u00de\5\36\20\2\u00dd\u00db\3\2\2\2\u00de\u00e1\3\2\2\2\u00df\u00dd\3"+
		"\2\2\2\u00df\u00e0\3\2\2\2\u00e0\u00e2\3\2\2\2\u00e1\u00df\3\2\2\2\u00e2"+
		"\u00e3\7&\2\2\u00e3\35\3\2\2\2\u00e4\u00e5\7\'\2\2\u00e5\37\3\2\2\2\u00e6"+
		"\u00e7\7\'\2\2\u00e7!\3\2\2\2\u00e8\u00e9\5,\27\2\u00e9\u00ea\5\60\31"+
		"\2\u00ea#\3\2\2\2\u00eb\u00ec\7\'\2\2\u00ec%\3\2\2\2\u00ed\u00ee\7\'\2"+
		"\2\u00ee\'\3\2\2\2\u00ef\u00f0\5,\27\2\u00f0\u00f3\5\60\31\2\u00f1\u00f2"+
		"\7\13\2\2\u00f2\u00f4\5\b\5\2\u00f3\u00f1\3\2\2\2\u00f3\u00f4\3\2\2\2"+
		"\u00f4\u0108\3\2\2\2\u00f5\u00f6\5\60\31\2\u00f6\u00f7\7\13\2\2\u00f7"+
		"\u00f8\5\b\5\2\u00f8\u0108\3\2\2\2\u00f9\u00fa\7\t\2\2\u00fa\u0108\5\b"+
		"\5\2\u00fb\u00fc\7\6\2\2\u00fc\u00fd\7\27\2\2\u00fd\u00fe\5\b\5\2\u00fe"+
		"\u00ff\7\r\2\2\u00ff\u0100\5\6\4\2\u0100\u0101\7\5\2\2\u0101\u0102\5\6"+
		"\4\2\u0102\u0108\3\2\2\2\u0103\u0104\7\b\2\2\u0104\u0105\5(\25\2\u0105"+
		"\u0106\5\6\4\2\u0106\u0108\3\2\2\2\u0107\u00ef\3\2\2\2\u0107\u00f5\3\2"+
		"\2\2\u0107\u00f9\3\2\2\2\u0107\u00fb\3\2\2\2\u0107\u0103\3\2\2\2\u0108"+
		")\3\2\2\2\u0109\u010a\5,\27\2\u010a\u010e\7\26\2\2\u010b\u010d\5.\30\2"+
		"\u010c\u010b\3\2\2\2\u010d\u0110\3\2\2\2\u010e\u010c\3\2\2\2\u010e\u010f"+
		"\3\2\2\2\u010f\u0111\3\2\2\2\u0110\u010e\3\2\2\2\u0111\u0112\7\f\2\2\u0112"+
		"+\3\2\2\2\u0113\u0114\7\'\2\2\u0114-\3\2\2\2\u0115\u0116\5\60\31\2\u0116"+
		"\u0117\7\13\2\2\u0117\u0118\5\b\5\2\u0118/\3\2\2\2\u0119\u011a\7\'\2\2"+
		"\u011a\61\3\2\2\2\27\65;AGTehu\u0092\u0094\u009c\u00a5\u00af\u00bb\u00c7"+
		"\u00cb\u00d2\u00df\u00f3\u0107\u010e";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}