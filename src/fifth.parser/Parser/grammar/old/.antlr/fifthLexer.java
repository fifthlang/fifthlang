// Generated from /Users/aabs/dev/by-technology/dotnet/fifthlang/src/fifth.parser/Parser/grammar/old/FifthLexer.g4 by ANTLR 4.8
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
		LETTER=38, DIGIT=39, HEXDIGIT=40, POSITIVEDIGIT=41, NAT=42, STRING=43, 
		FLOAT=44, INT=45, EXP=46, WS=47;
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
			"HEXDIGIT", "POSITIVEDIGIT", "NAT", "STRING", "FLOAT", "INT", "EXP", 
			"WS"
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
			"HEXDIGIT", "POSITIVEDIGIT", "NAT", "STRING", "FLOAT", "INT", "EXP", 
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\61\u011f\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31"+
		"\t\31\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t"+
		" \4!\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t"+
		"+\4,\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\3\2\3\2\3\2\3\2\3\2\3\2\3\3\3\3\3"+
		"\3\3\4\3\4\3\4\3\4\3\4\3\5\3\5\3\5\3\6\3\6\3\6\3\6\3\7\3\7\3\7\3\7\3\7"+
		"\3\b\3\b\3\b\3\b\3\b\3\b\3\b\3\t\3\t\3\t\3\t\3\n\3\n\3\13\3\13\3\f\3\f"+
		"\3\r\3\r\3\16\3\16\3\17\3\17\3\20\3\20\3\20\3\21\3\21\3\21\3\22\3\22\3"+
		"\23\3\23\3\24\3\24\3\25\3\25\3\26\3\26\3\27\3\27\3\30\3\30\3\31\3\31\3"+
		"\31\3\32\3\32\3\33\3\33\3\34\3\34\3\34\3\35\3\35\3\35\3\36\3\36\3\36\3"+
		"\37\3\37\3\37\3 \3 \3!\3!\3\"\3\"\6\"\u00c0\n\"\r\"\16\"\u00c1\3\"\3\""+
		"\3#\3#\7#\u00c8\n#\f#\16#\u00cb\13#\3$\3$\5$\u00cf\n$\3%\3%\3%\5%\u00d4"+
		"\n%\3&\3&\3&\3\'\3\'\3(\3(\3)\3)\3*\3*\3+\3+\7+\u00e3\n+\f+\16+\u00e6"+
		"\13+\3,\3,\7,\u00ea\n,\f,\16,\u00ed\13,\3,\3,\3,\7,\u00f2\n,\f,\16,\u00f5"+
		"\13,\3,\5,\u00f8\n,\3-\5-\u00fb\n-\3-\3-\3-\6-\u0100\n-\r-\16-\u0101\5"+
		"-\u0104\n-\3-\5-\u0107\n-\3.\3.\3.\7.\u010c\n.\f.\16.\u010f\13.\5.\u0111"+
		"\n.\3/\3/\5/\u0115\n/\3/\3/\3\60\6\60\u011a\n\60\r\60\16\60\u011b\3\60"+
		"\3\60\2\2\61\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16"+
		"\33\17\35\20\37\21!\22#\23%\24\'\25)\26+\27-\30/\31\61\32\63\33\65\34"+
		"\67\359\36;\37= ?!A\"C#E$G%I&K\'M(O)Q*S+U,W-Y.[/]\60_\61\3\2\r\b\2((/"+
		"/\61<AAC\\c|\6\2FFJJOOUU\4\2C\\c|\3\2\62;\5\2\62;CHch\3\2\63;\3\2$$\3"+
		"\2))\4\2GGgg\4\2--//\5\2\13\f\17\17\"\"\2\u012f\2\3\3\2\2\2\2\5\3\2\2"+
		"\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21"+
		"\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2\2\2\33\3\2"+
		"\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2\2\2\2%\3\2\2\2\2\'\3"+
		"\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2\2\2\61\3\2\2\2\2\63\3"+
		"\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2\29\3\2\2\2\2;\3\2\2\2\2=\3\2\2\2\2?\3"+
		"\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E\3\2\2\2\2G\3\2\2\2\2I\3\2\2\2\2K\3\2\2"+
		"\2\2M\3\2\2\2\2O\3\2\2\2\2Q\3\2\2\2\2S\3\2\2\2\2U\3\2\2\2\2W\3\2\2\2\2"+
		"Y\3\2\2\2\2[\3\2\2\2\2]\3\2\2\2\2_\3\2\2\2\3a\3\2\2\2\5g\3\2\2\2\7j\3"+
		"\2\2\2\to\3\2\2\2\13r\3\2\2\2\rv\3\2\2\2\17{\3\2\2\2\21\u0082\3\2\2\2"+
		"\23\u0086\3\2\2\2\25\u0088\3\2\2\2\27\u008a\3\2\2\2\31\u008c\3\2\2\2\33"+
		"\u008e\3\2\2\2\35\u0090\3\2\2\2\37\u0092\3\2\2\2!\u0095\3\2\2\2#\u0098"+
		"\3\2\2\2%\u009a\3\2\2\2\'\u009c\3\2\2\2)\u009e\3\2\2\2+\u00a0\3\2\2\2"+
		"-\u00a2\3\2\2\2/\u00a4\3\2\2\2\61\u00a6\3\2\2\2\63\u00a9\3\2\2\2\65\u00ab"+
		"\3\2\2\2\67\u00ad\3\2\2\29\u00b0\3\2\2\2;\u00b3\3\2\2\2=\u00b6\3\2\2\2"+
		"?\u00b9\3\2\2\2A\u00bb\3\2\2\2C\u00bd\3\2\2\2E\u00c5\3\2\2\2G\u00ce\3"+
		"\2\2\2I\u00d3\3\2\2\2K\u00d5\3\2\2\2M\u00d8\3\2\2\2O\u00da\3\2\2\2Q\u00dc"+
		"\3\2\2\2S\u00de\3\2\2\2U\u00e0\3\2\2\2W\u00f7\3\2\2\2Y\u00fa\3\2\2\2["+
		"\u0110\3\2\2\2]\u0112\3\2\2\2_\u0119\3\2\2\2ab\7c\2\2bc\7n\2\2cd\7k\2"+
		"\2de\7c\2\2ef\7u\2\2f\4\3\2\2\2gh\7c\2\2hi\7u\2\2i\6\3\2\2\2jk\7g\2\2"+
		"kl\7n\2\2lm\7u\2\2mn\7g\2\2n\b\3\2\2\2op\7k\2\2pq\7h\2\2q\n\3\2\2\2rs"+
		"\7p\2\2st\7g\2\2tu\7y\2\2u\f\3\2\2\2vw\7y\2\2wx\7k\2\2xy\7v\2\2yz\7j\2"+
		"\2z\16\3\2\2\2{|\7t\2\2|}\7g\2\2}~\7v\2\2~\177\7w\2\2\177\u0080\7t\2\2"+
		"\u0080\u0081\7p\2\2\u0081\20\3\2\2\2\u0082\u0083\7w\2\2\u0083\u0084\7"+
		"u\2\2\u0084\u0085\7g\2\2\u0085\22\3\2\2\2\u0086\u0087\7?\2\2\u0087\24"+
		"\3\2\2\2\u0088\u0089\7\177\2\2\u0089\26\3\2\2\2\u008a\u008b\7+\2\2\u008b"+
		"\30\3\2\2\2\u008c\u008d\7.\2\2\u008d\32\3\2\2\2\u008e\u008f\7\61\2\2\u008f"+
		"\34\3\2\2\2\u0090\u0091\7\60\2\2\u0091\36\3\2\2\2\u0092\u0093\7?\2\2\u0093"+
		"\u0094\7?\2\2\u0094 \3\2\2\2\u0095\u0096\7?\2\2\u0096\u0097\7@\2\2\u0097"+
		"\"\3\2\2\2\u0098\u0099\7/\2\2\u0099$\3\2\2\2\u009a\u009b\7}\2\2\u009b"+
		"&\3\2\2\2\u009c\u009d\7*\2\2\u009d(\3\2\2\2\u009e\u009f\7-\2\2\u009f*"+
		"\3\2\2\2\u00a0\u00a1\7,\2\2\u00a1,\3\2\2\2\u00a2\u00a3\7\'\2\2\u00a3."+
		"\3\2\2\2\u00a4\u00a5\7`\2\2\u00a5\60\3\2\2\2\u00a6\u00a7\7#\2\2\u00a7"+
		"\u00a8\7?\2\2\u00a8\62\3\2\2\2\u00a9\u00aa\7@\2\2\u00aa\64\3\2\2\2\u00ab"+
		"\u00ac\7>\2\2\u00ac\66\3\2\2\2\u00ad\u00ae\7@\2\2\u00ae\u00af\7?\2\2\u00af"+
		"8\3\2\2\2\u00b0\u00b1\7>\2\2\u00b1\u00b2\7?\2\2\u00b2:\3\2\2\2\u00b3\u00b4"+
		"\7(\2\2\u00b4\u00b5\7(\2\2\u00b5<\3\2\2\2\u00b6\u00b7\7~\2\2\u00b7\u00b8"+
		"\7~\2\2\u00b8>\3\2\2\2\u00b9\u00ba\7#\2\2\u00ba@\3\2\2\2\u00bb\u00bc\7"+
		"=\2\2\u00bcB\3\2\2\2\u00bd\u00bf\5\65\33\2\u00be\u00c0\t\2\2\2\u00bf\u00be"+
		"\3\2\2\2\u00c0\u00c1\3\2\2\2\u00c1\u00bf\3\2\2\2\u00c1\u00c2\3\2\2\2\u00c2"+
		"\u00c3\3\2\2\2\u00c3\u00c4\5\63\32\2\u00c4D\3\2\2\2\u00c5\u00c9\5G$\2"+
		"\u00c6\u00c8\5I%\2\u00c7\u00c6\3\2\2\2\u00c8\u00cb\3\2\2\2\u00c9\u00c7"+
		"\3\2\2\2\u00c9\u00ca\3\2\2\2\u00caF\3\2\2\2\u00cb\u00c9\3\2\2\2\u00cc"+
		"\u00cf\5M\'\2\u00cd\u00cf\7a\2\2\u00ce\u00cc\3\2\2\2\u00ce\u00cd\3\2\2"+
		"\2\u00cfH\3\2\2\2\u00d0\u00d4\5G$\2\u00d1\u00d4\5O(\2\u00d2\u00d4\7\60"+
		"\2\2\u00d3\u00d0\3\2\2\2\u00d3\u00d1\3\2\2\2\u00d3\u00d2\3\2\2\2\u00d4"+
		"J\3\2\2\2\u00d5\u00d6\5U+\2\u00d6\u00d7\t\3\2\2\u00d7L\3\2\2\2\u00d8\u00d9"+
		"\t\4\2\2\u00d9N\3\2\2\2\u00da\u00db\t\5\2\2\u00dbP\3\2\2\2\u00dc\u00dd"+
		"\t\6\2\2\u00ddR\3\2\2\2\u00de\u00df\t\7\2\2\u00dfT\3\2\2\2\u00e0\u00e4"+
		"\5S*\2\u00e1\u00e3\5O(\2\u00e2\u00e1\3\2\2\2\u00e3\u00e6\3\2\2\2\u00e4"+
		"\u00e2\3\2\2\2\u00e4\u00e5\3\2\2\2\u00e5V\3\2\2\2\u00e6\u00e4\3\2\2\2"+
		"\u00e7\u00eb\7$\2\2\u00e8\u00ea\n\b\2\2\u00e9\u00e8\3\2\2\2\u00ea\u00ed"+
		"\3\2\2\2\u00eb\u00e9\3\2\2\2\u00eb\u00ec\3\2\2\2\u00ec\u00ee\3\2\2\2\u00ed"+
		"\u00eb\3\2\2\2\u00ee\u00f8\7$\2\2\u00ef\u00f3\7)\2\2\u00f0\u00f2\n\t\2"+
		"\2\u00f1\u00f0\3\2\2\2\u00f2\u00f5\3\2\2\2\u00f3\u00f1\3\2\2\2\u00f3\u00f4"+
		"\3\2\2\2\u00f4\u00f6\3\2\2\2\u00f5\u00f3\3\2\2\2\u00f6\u00f8\7)\2\2\u00f7"+
		"\u00e7\3\2\2\2\u00f7\u00ef\3\2\2\2\u00f8X\3\2\2\2\u00f9\u00fb\7/\2\2\u00fa"+
		"\u00f9\3\2\2\2\u00fa\u00fb\3\2\2\2\u00fb\u00fc\3\2\2\2\u00fc\u0103\5["+
		".\2\u00fd\u00ff\7\60\2\2\u00fe\u0100\5O(\2\u00ff\u00fe\3\2\2\2\u0100\u0101"+
		"\3\2\2\2\u0101\u00ff\3\2\2\2\u0101\u0102\3\2\2\2\u0102\u0104\3\2\2\2\u0103"+
		"\u00fd\3\2\2\2\u0103\u0104\3\2\2\2\u0104\u0106\3\2\2\2\u0105\u0107\5]"+
		"/\2\u0106\u0105\3\2\2\2\u0106\u0107\3\2\2\2\u0107Z\3\2\2\2\u0108\u0111"+
		"\7\62\2\2\u0109\u010d\5S*\2\u010a\u010c\5O(\2\u010b\u010a\3\2\2\2\u010c"+
		"\u010f\3\2\2\2\u010d\u010b\3\2\2\2\u010d\u010e\3\2\2\2\u010e\u0111\3\2"+
		"\2\2\u010f\u010d\3\2\2\2\u0110\u0108\3\2\2\2\u0110\u0109\3\2\2\2\u0111"+
		"\\\3\2\2\2\u0112\u0114\t\n\2\2\u0113\u0115\t\13\2\2\u0114\u0113\3\2\2"+
		"\2\u0114\u0115\3\2\2\2\u0115\u0116\3\2\2\2\u0116\u0117\5[.\2\u0117^\3"+
		"\2\2\2\u0118\u011a\t\f\2\2\u0119\u0118\3\2\2\2\u011a\u011b\3\2\2\2\u011b"+
		"\u0119\3\2\2\2\u011b\u011c\3\2\2\2\u011c\u011d\3\2\2\2\u011d\u011e\b\60"+
		"\2\2\u011e`\3\2\2\2\23\2\u00c1\u00c9\u00ce\u00d3\u00e4\u00eb\u00f3\u00f7"+
		"\u00fa\u0101\u0103\u0106\u010d\u0110\u0114\u011b\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}