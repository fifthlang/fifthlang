// Generated from /Users/aabs/dev/by-technology/dotnet/fifthlang/src/fifth.parser/Parser/grammar/FifthLexer.g4 by ANTLR 4.8
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class FifthLexer extends Lexer {
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
		COMMENTS_CHANNEL=2, DIRECTIVE=3;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN", "COMMENTS_CHANNEL", "DIRECTIVE"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"ALIAS", "AS", "ELSE", "IF", "NEW", "WITH", "RETURN", "USE", "ASSIGN", 
			"CLOSEBRACE", "CLOSEPAREN", "COMMA", "DIVIDE", "DOT", "EQ", "LAMBDASEP", 
			"MINUS", "OPENBRACE", "OPENPAREN", "PLUS", "TIMES", "PERCENT", "POWER", 
			"NEQ", "GT", "LT", "GEQ", "LEQ", "AND", "OR", "NOT", "SEMICOLON", "URICONSTANT", 
			"IDENTIFIER", "IDSTART", "IDPART", "TIMEINTERVAL", "LETTER", "DIGIT", 
			"POSITIVEDIGIT", "NAT", "STRING", "FLOAT", "INT", "EXP", "WS", "VARIABLE", 
			"VALID_ID_START", "VALID_ID_CHAR", "ScientificNumber", "E1", "E2", "SIGN", 
			"NUMBER"
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


	public FifthLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "FifthLexer.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\62\u0153\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31"+
		"\t\31\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t"+
		" \4!\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t"+
		"+\4,\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64"+
		"\t\64\4\65\t\65\4\66\t\66\4\67\t\67\3\2\3\2\3\2\3\2\3\2\3\2\3\3\3\3\3"+
		"\3\3\4\3\4\3\4\3\4\3\4\3\5\3\5\3\5\3\6\3\6\3\6\3\6\3\7\3\7\3\7\3\7\3\7"+
		"\3\b\3\b\3\b\3\b\3\b\3\b\3\b\3\t\3\t\3\t\3\t\3\n\3\n\3\13\3\13\3\f\3\f"+
		"\3\r\3\r\3\16\3\16\3\17\3\17\3\20\3\20\3\20\3\21\3\21\3\21\3\22\3\22\3"+
		"\23\3\23\3\24\3\24\3\25\3\25\3\26\3\26\3\27\3\27\3\30\3\30\3\31\3\31\3"+
		"\31\3\32\3\32\3\33\3\33\3\34\3\34\3\34\3\35\3\35\3\35\3\36\3\36\3\36\3"+
		"\37\3\37\3\37\3 \3 \3!\3!\3\"\3\"\3\"\3\"\3#\3#\7#\u00d2\n#\f#\16#\u00d5"+
		"\13#\3$\3$\5$\u00d9\n$\3%\3%\5%\u00dd\n%\3&\3&\3&\3\'\3\'\3(\3(\3)\3)"+
		"\3*\3*\7*\u00ea\n*\f*\16*\u00ed\13*\3+\3+\7+\u00f1\n+\f+\16+\u00f4\13"+
		"+\3+\3+\3+\7+\u00f9\n+\f+\16+\u00fc\13+\3+\5+\u00ff\n+\3,\5,\u0102\n,"+
		"\3,\3,\3,\6,\u0107\n,\r,\16,\u0108\5,\u010b\n,\3,\5,\u010e\n,\3-\3-\3"+
		"-\7-\u0113\n-\f-\16-\u0116\13-\5-\u0118\n-\3.\3.\5.\u011c\n.\3.\3.\3/"+
		"\6/\u0121\n/\r/\16/\u0122\3/\3/\3\60\3\60\7\60\u0129\n\60\f\60\16\60\u012c"+
		"\13\60\3\61\5\61\u012f\n\61\3\62\3\62\5\62\u0133\n\62\3\63\3\63\3\63\5"+
		"\63\u0138\n\63\3\63\5\63\u013b\n\63\3\63\3\63\5\63\u013f\n\63\3\64\3\64"+
		"\3\65\3\65\3\66\3\66\3\67\6\67\u0148\n\67\r\67\16\67\u0149\3\67\3\67\6"+
		"\67\u014e\n\67\r\67\16\67\u014f\5\67\u0152\n\67\2\28\3\3\5\4\7\5\t\6\13"+
		"\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35\20\37\21!\22#\23%\24\'"+
		"\25)\26+\27-\30/\31\61\32\63\33\65\34\67\359\36;\37= ?!A\"C#E$G%I&K\'"+
		"M(O)Q*S+U,W-Y.[/]\60_\61a\2c\2e\62g\2i\2k\2m\2\3\2\f\6\2FFJJOOUU\4\2C"+
		"\\c|\3\2\62;\3\2\63;\3\2$$\3\2))\4\2GGgg\4\2--//\5\2\13\f\17\17\"\"\5"+
		"\2C\\aac|\2\u0163\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13"+
		"\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2"+
		"\2\2\27\3\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2"+
		"!\3\2\2\2\2#\3\2\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3"+
		"\2\2\2\2/\3\2\2\2\2\61\3\2\2\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2"+
		"\29\3\2\2\2\2;\3\2\2\2\2=\3\2\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E"+
		"\3\2\2\2\2G\3\2\2\2\2I\3\2\2\2\2K\3\2\2\2\2M\3\2\2\2\2O\3\2\2\2\2Q\3\2"+
		"\2\2\2S\3\2\2\2\2U\3\2\2\2\2W\3\2\2\2\2Y\3\2\2\2\2[\3\2\2\2\2]\3\2\2\2"+
		"\2_\3\2\2\2\2e\3\2\2\2\3o\3\2\2\2\5u\3\2\2\2\7x\3\2\2\2\t}\3\2\2\2\13"+
		"\u0080\3\2\2\2\r\u0084\3\2\2\2\17\u0089\3\2\2\2\21\u0090\3\2\2\2\23\u0094"+
		"\3\2\2\2\25\u0096\3\2\2\2\27\u0098\3\2\2\2\31\u009a\3\2\2\2\33\u009c\3"+
		"\2\2\2\35\u009e\3\2\2\2\37\u00a0\3\2\2\2!\u00a3\3\2\2\2#\u00a6\3\2\2\2"+
		"%\u00a8\3\2\2\2\'\u00aa\3\2\2\2)\u00ac\3\2\2\2+\u00ae\3\2\2\2-\u00b0\3"+
		"\2\2\2/\u00b2\3\2\2\2\61\u00b4\3\2\2\2\63\u00b7\3\2\2\2\65\u00b9\3\2\2"+
		"\2\67\u00bb\3\2\2\29\u00be\3\2\2\2;\u00c1\3\2\2\2=\u00c4\3\2\2\2?\u00c7"+
		"\3\2\2\2A\u00c9\3\2\2\2C\u00cb\3\2\2\2E\u00cf\3\2\2\2G\u00d8\3\2\2\2I"+
		"\u00dc\3\2\2\2K\u00de\3\2\2\2M\u00e1\3\2\2\2O\u00e3\3\2\2\2Q\u00e5\3\2"+
		"\2\2S\u00e7\3\2\2\2U\u00fe\3\2\2\2W\u0101\3\2\2\2Y\u0117\3\2\2\2[\u0119"+
		"\3\2\2\2]\u0120\3\2\2\2_\u0126\3\2\2\2a\u012e\3\2\2\2c\u0132\3\2\2\2e"+
		"\u0134\3\2\2\2g\u0140\3\2\2\2i\u0142\3\2\2\2k\u0144\3\2\2\2m\u0147\3\2"+
		"\2\2op\7c\2\2pq\7n\2\2qr\7k\2\2rs\7c\2\2st\7u\2\2t\4\3\2\2\2uv\7c\2\2"+
		"vw\7u\2\2w\6\3\2\2\2xy\7g\2\2yz\7n\2\2z{\7u\2\2{|\7g\2\2|\b\3\2\2\2}~"+
		"\7k\2\2~\177\7h\2\2\177\n\3\2\2\2\u0080\u0081\7p\2\2\u0081\u0082\7g\2"+
		"\2\u0082\u0083\7y\2\2\u0083\f\3\2\2\2\u0084\u0085\7y\2\2\u0085\u0086\7"+
		"k\2\2\u0086\u0087\7v\2\2\u0087\u0088\7j\2\2\u0088\16\3\2\2\2\u0089\u008a"+
		"\7t\2\2\u008a\u008b\7g\2\2\u008b\u008c\7v\2\2\u008c\u008d\7w\2\2\u008d"+
		"\u008e\7t\2\2\u008e\u008f\7p\2\2\u008f\20\3\2\2\2\u0090\u0091\7w\2\2\u0091"+
		"\u0092\7u\2\2\u0092\u0093\7g\2\2\u0093\22\3\2\2\2\u0094\u0095\7?\2\2\u0095"+
		"\24\3\2\2\2\u0096\u0097\7\177\2\2\u0097\26\3\2\2\2\u0098\u0099\7+\2\2"+
		"\u0099\30\3\2\2\2\u009a\u009b\7.\2\2\u009b\32\3\2\2\2\u009c\u009d\7\61"+
		"\2\2\u009d\34\3\2\2\2\u009e\u009f\7\60\2\2\u009f\36\3\2\2\2\u00a0\u00a1"+
		"\7?\2\2\u00a1\u00a2\7?\2\2\u00a2 \3\2\2\2\u00a3\u00a4\7?\2\2\u00a4\u00a5"+
		"\7@\2\2\u00a5\"\3\2\2\2\u00a6\u00a7\7/\2\2\u00a7$\3\2\2\2\u00a8\u00a9"+
		"\7}\2\2\u00a9&\3\2\2\2\u00aa\u00ab\7*\2\2\u00ab(\3\2\2\2\u00ac\u00ad\7"+
		"-\2\2\u00ad*\3\2\2\2\u00ae\u00af\7,\2\2\u00af,\3\2\2\2\u00b0\u00b1\7\'"+
		"\2\2\u00b1.\3\2\2\2\u00b2\u00b3\7`\2\2\u00b3\60\3\2\2\2\u00b4\u00b5\7"+
		"#\2\2\u00b5\u00b6\7?\2\2\u00b6\62\3\2\2\2\u00b7\u00b8\7@\2\2\u00b8\64"+
		"\3\2\2\2\u00b9\u00ba\7>\2\2\u00ba\66\3\2\2\2\u00bb\u00bc\7@\2\2\u00bc"+
		"\u00bd\7?\2\2\u00bd8\3\2\2\2\u00be\u00bf\7>\2\2\u00bf\u00c0\7?\2\2\u00c0"+
		":\3\2\2\2\u00c1\u00c2\7(\2\2\u00c2\u00c3\7(\2\2\u00c3<\3\2\2\2\u00c4\u00c5"+
		"\7~\2\2\u00c5\u00c6\7~\2\2\u00c6>\3\2\2\2\u00c7\u00c8\7#\2\2\u00c8@\3"+
		"\2\2\2\u00c9\u00ca\7=\2\2\u00caB\3\2\2\2\u00cb\u00cc\5\65\33\2\u00cc\u00cd"+
		"\5U+\2\u00cd\u00ce\5\63\32\2\u00ceD\3\2\2\2\u00cf\u00d3\5G$\2\u00d0\u00d2"+
		"\5I%\2\u00d1\u00d0\3\2\2\2\u00d2\u00d5\3\2\2\2\u00d3\u00d1\3\2\2\2\u00d3"+
		"\u00d4\3\2\2\2\u00d4F\3\2\2\2\u00d5\u00d3\3\2\2\2\u00d6\u00d9\5M\'\2\u00d7"+
		"\u00d9\7a\2\2\u00d8\u00d6\3\2\2\2\u00d8\u00d7\3\2\2\2\u00d9H\3\2\2\2\u00da"+
		"\u00dd\5G$\2\u00db\u00dd\5O(\2\u00dc\u00da\3\2\2\2\u00dc\u00db\3\2\2\2"+
		"\u00ddJ\3\2\2\2\u00de\u00df\5S*\2\u00df\u00e0\t\2\2\2\u00e0L\3\2\2\2\u00e1"+
		"\u00e2\t\3\2\2\u00e2N\3\2\2\2\u00e3\u00e4\t\4\2\2\u00e4P\3\2\2\2\u00e5"+
		"\u00e6\t\5\2\2\u00e6R\3\2\2\2\u00e7\u00eb\5Q)\2\u00e8\u00ea\5O(\2\u00e9"+
		"\u00e8\3\2\2\2\u00ea\u00ed\3\2\2\2\u00eb\u00e9\3\2\2\2\u00eb\u00ec\3\2"+
		"\2\2\u00ecT\3\2\2\2\u00ed\u00eb\3\2\2\2\u00ee\u00f2\7$\2\2\u00ef\u00f1"+
		"\n\6\2\2\u00f0\u00ef\3\2\2\2\u00f1\u00f4\3\2\2\2\u00f2\u00f0\3\2\2\2\u00f2"+
		"\u00f3\3\2\2\2\u00f3\u00f5\3\2\2\2\u00f4\u00f2\3\2\2\2\u00f5\u00ff\7$"+
		"\2\2\u00f6\u00fa\7)\2\2\u00f7\u00f9\n\7\2\2\u00f8\u00f7\3\2\2\2\u00f9"+
		"\u00fc\3\2\2\2\u00fa\u00f8\3\2\2\2\u00fa\u00fb\3\2\2\2\u00fb\u00fd\3\2"+
		"\2\2\u00fc\u00fa\3\2\2\2\u00fd\u00ff\7)\2\2\u00fe\u00ee\3\2\2\2\u00fe"+
		"\u00f6\3\2\2\2\u00ffV\3\2\2\2\u0100\u0102\7/\2\2\u0101\u0100\3\2\2\2\u0101"+
		"\u0102\3\2\2\2\u0102\u0103\3\2\2\2\u0103\u010a\5Y-\2\u0104\u0106\7\60"+
		"\2\2\u0105\u0107\5O(\2\u0106\u0105\3\2\2\2\u0107\u0108\3\2\2\2\u0108\u0106"+
		"\3\2\2\2\u0108\u0109\3\2\2\2\u0109\u010b\3\2\2\2\u010a\u0104\3\2\2\2\u010a"+
		"\u010b\3\2\2\2\u010b\u010d\3\2\2\2\u010c\u010e\5[.\2\u010d\u010c\3\2\2"+
		"\2\u010d\u010e\3\2\2\2\u010eX\3\2\2\2\u010f\u0118\7\62\2\2\u0110\u0114"+
		"\t\5\2\2\u0111\u0113\t\4\2\2\u0112\u0111\3\2\2\2\u0113\u0116\3\2\2\2\u0114"+
		"\u0112\3\2\2\2\u0114\u0115\3\2\2\2\u0115\u0118\3\2\2\2\u0116\u0114\3\2"+
		"\2\2\u0117\u010f\3\2\2\2\u0117\u0110\3\2\2\2\u0118Z\3\2\2\2\u0119\u011b"+
		"\t\b\2\2\u011a\u011c\t\t\2\2\u011b\u011a\3\2\2\2\u011b\u011c\3\2\2\2\u011c"+
		"\u011d\3\2\2\2\u011d\u011e\5Y-\2\u011e\\\3\2\2\2\u011f\u0121\t\n\2\2\u0120"+
		"\u011f\3\2\2\2\u0121\u0122\3\2\2\2\u0122\u0120\3\2\2\2\u0122\u0123\3\2"+
		"\2\2\u0123\u0124\3\2\2\2\u0124\u0125\b/\2\2\u0125^\3\2\2\2\u0126\u012a"+
		"\5a\61\2\u0127\u0129\5c\62\2\u0128\u0127\3\2\2\2\u0129\u012c\3\2\2\2\u012a"+
		"\u0128\3\2\2\2\u012a\u012b\3\2\2\2\u012b`\3\2\2\2\u012c\u012a\3\2\2\2"+
		"\u012d\u012f\t\13\2\2\u012e\u012d\3\2\2\2\u012fb\3\2\2\2\u0130\u0133\5"+
		"a\61\2\u0131\u0133\4\62;\2\u0132\u0130\3\2\2\2\u0132\u0131\3\2\2\2\u0133"+
		"d\3\2\2\2\u0134\u013e\5m\67\2\u0135\u0138\5g\64\2\u0136\u0138\5i\65\2"+
		"\u0137\u0135\3\2\2\2\u0137\u0136\3\2\2\2\u0138\u013a\3\2\2\2\u0139\u013b"+
		"\5k\66\2\u013a\u0139\3\2\2\2\u013a\u013b\3\2\2\2\u013b\u013c\3\2\2\2\u013c"+
		"\u013d\5m\67\2\u013d\u013f\3\2\2\2\u013e\u0137\3\2\2\2\u013e\u013f\3\2"+
		"\2\2\u013ff\3\2\2\2\u0140\u0141\7G\2\2\u0141h\3\2\2\2\u0142\u0143\7g\2"+
		"\2\u0143j\3\2\2\2\u0144\u0145\t\t\2\2\u0145l\3\2\2\2\u0146\u0148\4\62"+
		";\2\u0147\u0146\3\2\2\2\u0148\u0149\3\2\2\2\u0149\u0147\3\2\2\2\u0149"+
		"\u014a\3\2\2\2\u014a\u0151\3\2\2\2\u014b\u014d\7\60\2\2\u014c\u014e\4"+
		"\62;\2\u014d\u014c\3\2\2\2\u014e\u014f\3\2\2\2\u014f\u014d\3\2\2\2\u014f"+
		"\u0150\3\2\2\2\u0150\u0152\3\2\2\2\u0151\u014b\3\2\2\2\u0151\u0152\3\2"+
		"\2\2\u0152n\3\2\2\2\33\2\u00d3\u00d8\u00dc\u00eb\u00f2\u00fa\u00fe\u0101"+
		"\u0108\u010a\u010d\u0114\u0117\u011b\u0122\u012a\u012e\u0132\u0137\u013a"+
		"\u013e\u0149\u014f\u0151\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}