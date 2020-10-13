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
		LETTER=38, DIGIT=39, HEXDIGIT=40, POSITIVEDIGIT=41, NAT=42, STRING=43, 
		FLOAT=44, INT=45, EXP=46, WS=47, VARIABLE=48, ScientificNumber=49, UCSCHAR=50, 
		IPRIVATE=51, D0=52, D1=53, D2=54, D3=55, D4=56, D5=57, D6=58, D7=59, D8=60, 
		D9=61, A=62, B=63, C=64, D=65, E=66, F=67, G=68, H=69, I=70, J=71, K=72, 
		L=73, M=74, N=75, O=76, P=77, Q=78, R=79, S=80, T=81, U=82, V=83, W=84, 
		X=85, Y=86, Z=87, COL2=88, COL=89, HYPHEN=90, TILDE=91, USCORE=92, EXCL=93, 
		DOLLAR=94, AMP=95, SQUOTE=96, OPAREN=97, CPAREN=98, STAR=99, SCOL=100, 
		EQUALS=101, FSLASH2=102, FSLASH=103, QMARK=104, HASH=105, OBRACK=106, 
		CBRACK=107, AT=108;
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
			"WS", "VARIABLE", "VALID_ID_START", "VALID_ID_CHAR", "ScientificNumber", 
			"E1", "E2", "SIGN", "NUMBER", "UCSCHAR", "IPRIVATE", "D0", "D1", "D2", 
			"D3", "D4", "D5", "D6", "D7", "D8", "D9", "A", "B", "C", "D", "E", "F", 
			"G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", 
			"U", "V", "W", "X", "Y", "Z", "COL2", "COL", "HYPHEN", "TILDE", "USCORE", 
			"EXCL", "DOLLAR", "AMP", "SQUOTE", "OPAREN", "CPAREN", "STAR", "SCOL", 
			"EQUALS", "FSLASH2", "FSLASH", "QMARK", "HASH", "OBRACK", "CBRACK", "AT"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'alias'", "'as'", "'else'", "'if'", "'new'", "'with'", "'return'", 
			"'use'", null, "'}'", null, "','", null, "'.'", "'=='", "'=>'", null, 
			"'{'", null, "'+'", null, "'%'", "'^'", "'!='", "'>'", "'<'", "'>='", 
			"'<='", "'&&'", "'||'", null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, "'0'", "'1'", "'2'", "'3'", "'4'", "'5'", "'6'", "'7'", "'8'", 
			"'9'", null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, "'::'", "':'", null, "'~'", "'_'", null, "'$'", "'&'", 
			"'''", null, null, null, null, null, "'//'", null, "'?'", "'#'", "'['", 
			"']'", "'@'"
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
			"WS", "VARIABLE", "ScientificNumber", "UCSCHAR", "IPRIVATE", "D0", "D1", 
			"D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "A", "B", "C", "D", "E", 
			"F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", 
			"T", "U", "V", "W", "X", "Y", "Z", "COL2", "COL", "HYPHEN", "TILDE", 
			"USCORE", "EXCL", "DOLLAR", "AMP", "SQUOTE", "OPAREN", "CPAREN", "STAR", 
			"SCOL", "EQUALS", "FSLASH2", "FSLASH", "QMARK", "HASH", "OBRACK", "CBRACK", 
			"AT"
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2n\u0249\b\1\4\2\t"+
		"\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\4B\tB\4C\tC\4D\tD\4E\tE\4F\tF\4G\tG\4H\tH\4I"+
		"\tI\4J\tJ\4K\tK\4L\tL\4M\tM\4N\tN\4O\tO\4P\tP\4Q\tQ\4R\tR\4S\tS\4T\tT"+
		"\4U\tU\4V\tV\4W\tW\4X\tX\4Y\tY\4Z\tZ\4[\t[\4\\\t\\\4]\t]\4^\t^\4_\t_\4"+
		"`\t`\4a\ta\4b\tb\4c\tc\4d\td\4e\te\4f\tf\4g\tg\4h\th\4i\ti\4j\tj\4k\t"+
		"k\4l\tl\4m\tm\4n\tn\4o\to\4p\tp\4q\tq\4r\tr\4s\ts\3\2\3\2\3\2\3\2\3\2"+
		"\3\2\3\3\3\3\3\3\3\4\3\4\3\4\3\4\3\4\3\5\3\5\3\5\3\6\3\6\3\6\3\6\3\7\3"+
		"\7\3\7\3\7\3\7\3\b\3\b\3\b\3\b\3\b\3\b\3\b\3\t\3\t\3\t\3\t\3\n\3\n\3\13"+
		"\3\13\3\f\3\f\3\r\3\r\3\16\3\16\3\17\3\17\3\20\3\20\3\20\3\21\3\21\3\21"+
		"\3\22\3\22\3\23\3\23\3\24\3\24\3\25\3\25\3\26\3\26\3\27\3\27\3\30\3\30"+
		"\3\31\3\31\3\31\3\32\3\32\3\33\3\33\3\34\3\34\3\34\3\35\3\35\3\35\3\36"+
		"\3\36\3\36\3\37\3\37\3\37\3 \3 \3!\3!\3\"\3\"\6\"\u0146\n\"\r\"\16\"\u0147"+
		"\3\"\3\"\3#\3#\7#\u014e\n#\f#\16#\u0151\13#\3$\3$\5$\u0155\n$\3%\3%\5"+
		"%\u0159\n%\3&\3&\3&\3\'\3\'\3(\3(\3)\3)\3*\3*\3+\3+\7+\u0168\n+\f+\16"+
		"+\u016b\13+\3,\3,\7,\u016f\n,\f,\16,\u0172\13,\3,\3,\3,\7,\u0177\n,\f"+
		",\16,\u017a\13,\3,\5,\u017d\n,\3-\5-\u0180\n-\3-\3-\3-\6-\u0185\n-\r-"+
		"\16-\u0186\5-\u0189\n-\3-\5-\u018c\n-\3.\3.\3.\7.\u0191\n.\f.\16.\u0194"+
		"\13.\5.\u0196\n.\3/\3/\5/\u019a\n/\3/\3/\3\60\6\60\u019f\n\60\r\60\16"+
		"\60\u01a0\3\60\3\60\3\61\3\61\7\61\u01a7\n\61\f\61\16\61\u01aa\13\61\3"+
		"\62\5\62\u01ad\n\62\3\63\3\63\5\63\u01b1\n\63\3\64\3\64\3\64\5\64\u01b6"+
		"\n\64\3\64\5\64\u01b9\n\64\3\64\3\64\5\64\u01bd\n\64\3\65\3\65\3\66\3"+
		"\66\3\67\3\67\38\68\u01c6\n8\r8\168\u01c7\38\38\68\u01cc\n8\r8\168\u01cd"+
		"\58\u01d0\n8\39\39\3:\3:\3;\3;\3<\3<\3=\3=\3>\3>\3?\3?\3@\3@\3A\3A\3B"+
		"\3B\3C\3C\3D\3D\3E\3E\3F\3F\3G\3G\3H\3H\3I\3I\3J\3J\3K\3K\3L\3L\3M\3M"+
		"\3N\3N\3O\3O\3P\3P\3Q\3Q\3R\3R\3S\3S\3T\3T\3U\3U\3V\3V\3W\3W\3X\3X\3Y"+
		"\3Y\3Z\3Z\3[\3[\3\\\3\\\3]\3]\3^\3^\3_\3_\3_\3`\3`\3a\3a\3b\3b\3c\3c\3"+
		"d\3d\3e\3e\3f\3f\3g\3g\3h\3h\3i\3i\3j\3j\3k\3k\3l\3l\3m\3m\3m\3n\3n\3"+
		"o\3o\3p\3p\3q\3q\3r\3r\3s\3s\2\2t\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23"+
		"\13\25\f\27\r\31\16\33\17\35\20\37\21!\22#\23%\24\'\25)\26+\27-\30/\31"+
		"\61\32\63\33\65\34\67\359\36;\37= ?!A\"C#E$G%I&K\'M(O)Q*S+U,W-Y.[/]\60"+
		"_\61a\62c\2e\2g\63i\2k\2m\2o\2q\64s\65u\66w\67y8{9}:\177;\u0081<\u0083"+
		"=\u0085>\u0087?\u0089@\u008bA\u008dB\u008fC\u0091D\u0093E\u0095F\u0097"+
		"G\u0099H\u009bI\u009dJ\u009fK\u00a1L\u00a3M\u00a5N\u00a7O\u00a9P\u00ab"+
		"Q\u00adR\u00afS\u00b1T\u00b3U\u00b5V\u00b7W\u00b9X\u00bbY\u00bdZ\u00bf"+
		"[\u00c1\\\u00c3]\u00c5^\u00c7_\u00c9`\u00cba\u00cdb\u00cfc\u00d1d\u00d3"+
		"e\u00d5f\u00d7g\u00d9h\u00dbi\u00ddj\u00dfk\u00e1l\u00e3m\u00e5n\3\2\'"+
		"\b\2((//\61<AAC\\c|\6\2FFJJOOUU\4\2C\\c|\3\2\62;\5\2\62;CHch\3\2\63;\3"+
		"\2$$\3\2))\4\2GGgg\4\2--//\5\2\13\f\17\17\"\"\5\2C\\aac|\4\2CCcc\4\2D"+
		"Ddd\4\2EEee\4\2FFff\4\2HHhh\4\2IIii\4\2JJjj\4\2KKkk\4\2LLll\4\2MMmm\4"+
		"\2NNnn\4\2OOoo\4\2PPpp\4\2QQqq\4\2RRrr\4\2SSss\4\2TTtt\4\2UUuu\4\2VVv"+
		"v\4\2WWww\4\2XXxx\4\2YYyy\4\2ZZzz\4\2[[{{\4\2\\\\||\4\23\2\u00a2\2\ud801"+
		"\2\uf902\2\ufdd1\2\ufdf2\2\ufff1\2\2\3\uffff\3\2\4\uffff\4\2\5\uffff\5"+
		"\2\6\uffff\6\2\7\uffff\7\2\b\uffff\b\2\t\uffff\t\2\n\uffff\n\2\13\uffff"+
		"\13\2\f\uffff\f\2\r\uffff\r\2\16\uffff\16\2\17\uffff\17\u1002\20\uffff"+
		"\20\5\2\ue002\2\uf901\2\2\21\uffff\21\2\22\uffff\22\u025a\2\3\3\2\2\2"+
		"\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2"+
		"\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2\2"+
		"\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2\2\2\2%\3\2\2"+
		"\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2\2\2\61\3\2\2"+
		"\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2\29\3\2\2\2\2;\3\2\2\2\2=\3\2"+
		"\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E\3\2\2\2\2G\3\2\2\2\2I\3\2\2\2"+
		"\2K\3\2\2\2\2M\3\2\2\2\2O\3\2\2\2\2Q\3\2\2\2\2S\3\2\2\2\2U\3\2\2\2\2W"+
		"\3\2\2\2\2Y\3\2\2\2\2[\3\2\2\2\2]\3\2\2\2\2_\3\2\2\2\2a\3\2\2\2\2g\3\2"+
		"\2\2\2q\3\2\2\2\2s\3\2\2\2\2u\3\2\2\2\2w\3\2\2\2\2y\3\2\2\2\2{\3\2\2\2"+
		"\2}\3\2\2\2\2\177\3\2\2\2\2\u0081\3\2\2\2\2\u0083\3\2\2\2\2\u0085\3\2"+
		"\2\2\2\u0087\3\2\2\2\2\u0089\3\2\2\2\2\u008b\3\2\2\2\2\u008d\3\2\2\2\2"+
		"\u008f\3\2\2\2\2\u0091\3\2\2\2\2\u0093\3\2\2\2\2\u0095\3\2\2\2\2\u0097"+
		"\3\2\2\2\2\u0099\3\2\2\2\2\u009b\3\2\2\2\2\u009d\3\2\2\2\2\u009f\3\2\2"+
		"\2\2\u00a1\3\2\2\2\2\u00a3\3\2\2\2\2\u00a5\3\2\2\2\2\u00a7\3\2\2\2\2\u00a9"+
		"\3\2\2\2\2\u00ab\3\2\2\2\2\u00ad\3\2\2\2\2\u00af\3\2\2\2\2\u00b1\3\2\2"+
		"\2\2\u00b3\3\2\2\2\2\u00b5\3\2\2\2\2\u00b7\3\2\2\2\2\u00b9\3\2\2\2\2\u00bb"+
		"\3\2\2\2\2\u00bd\3\2\2\2\2\u00bf\3\2\2\2\2\u00c1\3\2\2\2\2\u00c3\3\2\2"+
		"\2\2\u00c5\3\2\2\2\2\u00c7\3\2\2\2\2\u00c9\3\2\2\2\2\u00cb\3\2\2\2\2\u00cd"+
		"\3\2\2\2\2\u00cf\3\2\2\2\2\u00d1\3\2\2\2\2\u00d3\3\2\2\2\2\u00d5\3\2\2"+
		"\2\2\u00d7\3\2\2\2\2\u00d9\3\2\2\2\2\u00db\3\2\2\2\2\u00dd\3\2\2\2\2\u00df"+
		"\3\2\2\2\2\u00e1\3\2\2\2\2\u00e3\3\2\2\2\2\u00e5\3\2\2\2\3\u00e7\3\2\2"+
		"\2\5\u00ed\3\2\2\2\7\u00f0\3\2\2\2\t\u00f5\3\2\2\2\13\u00f8\3\2\2\2\r"+
		"\u00fc\3\2\2\2\17\u0101\3\2\2\2\21\u0108\3\2\2\2\23\u010c\3\2\2\2\25\u010e"+
		"\3\2\2\2\27\u0110\3\2\2\2\31\u0112\3\2\2\2\33\u0114\3\2\2\2\35\u0116\3"+
		"\2\2\2\37\u0118\3\2\2\2!\u011b\3\2\2\2#\u011e\3\2\2\2%\u0120\3\2\2\2\'"+
		"\u0122\3\2\2\2)\u0124\3\2\2\2+\u0126\3\2\2\2-\u0128\3\2\2\2/\u012a\3\2"+
		"\2\2\61\u012c\3\2\2\2\63\u012f\3\2\2\2\65\u0131\3\2\2\2\67\u0133\3\2\2"+
		"\29\u0136\3\2\2\2;\u0139\3\2\2\2=\u013c\3\2\2\2?\u013f\3\2\2\2A\u0141"+
		"\3\2\2\2C\u0143\3\2\2\2E\u014b\3\2\2\2G\u0154\3\2\2\2I\u0158\3\2\2\2K"+
		"\u015a\3\2\2\2M\u015d\3\2\2\2O\u015f\3\2\2\2Q\u0161\3\2\2\2S\u0163\3\2"+
		"\2\2U\u0165\3\2\2\2W\u017c\3\2\2\2Y\u017f\3\2\2\2[\u0195\3\2\2\2]\u0197"+
		"\3\2\2\2_\u019e\3\2\2\2a\u01a4\3\2\2\2c\u01ac\3\2\2\2e\u01b0\3\2\2\2g"+
		"\u01b2\3\2\2\2i\u01be\3\2\2\2k\u01c0\3\2\2\2m\u01c2\3\2\2\2o\u01c5\3\2"+
		"\2\2q\u01d1\3\2\2\2s\u01d3\3\2\2\2u\u01d5\3\2\2\2w\u01d7\3\2\2\2y\u01d9"+
		"\3\2\2\2{\u01db\3\2\2\2}\u01dd\3\2\2\2\177\u01df\3\2\2\2\u0081\u01e1\3"+
		"\2\2\2\u0083\u01e3\3\2\2\2\u0085\u01e5\3\2\2\2\u0087\u01e7\3\2\2\2\u0089"+
		"\u01e9\3\2\2\2\u008b\u01eb\3\2\2\2\u008d\u01ed\3\2\2\2\u008f\u01ef\3\2"+
		"\2\2\u0091\u01f1\3\2\2\2\u0093\u01f3\3\2\2\2\u0095\u01f5\3\2\2\2\u0097"+
		"\u01f7\3\2\2\2\u0099\u01f9\3\2\2\2\u009b\u01fb\3\2\2\2\u009d\u01fd\3\2"+
		"\2\2\u009f\u01ff\3\2\2\2\u00a1\u0201\3\2\2\2\u00a3\u0203\3\2\2\2\u00a5"+
		"\u0205\3\2\2\2\u00a7\u0207\3\2\2\2\u00a9\u0209\3\2\2\2\u00ab\u020b\3\2"+
		"\2\2\u00ad\u020d\3\2\2\2\u00af\u020f\3\2\2\2\u00b1\u0211\3\2\2\2\u00b3"+
		"\u0213\3\2\2\2\u00b5\u0215\3\2\2\2\u00b7\u0217\3\2\2\2\u00b9\u0219\3\2"+
		"\2\2\u00bb\u021b\3\2\2\2\u00bd\u021d\3\2\2\2\u00bf\u0220\3\2\2\2\u00c1"+
		"\u0222\3\2\2\2\u00c3\u0224\3\2\2\2\u00c5\u0226\3\2\2\2\u00c7\u0228\3\2"+
		"\2\2\u00c9\u022a\3\2\2\2\u00cb\u022c\3\2\2\2\u00cd\u022e\3\2\2\2\u00cf"+
		"\u0230\3\2\2\2\u00d1\u0232\3\2\2\2\u00d3\u0234\3\2\2\2\u00d5\u0236\3\2"+
		"\2\2\u00d7\u0238\3\2\2\2\u00d9\u023a\3\2\2\2\u00db\u023d\3\2\2\2\u00dd"+
		"\u023f\3\2\2\2\u00df\u0241\3\2\2\2\u00e1\u0243\3\2\2\2\u00e3\u0245\3\2"+
		"\2\2\u00e5\u0247\3\2\2\2\u00e7\u00e8\7c\2\2\u00e8\u00e9\7n\2\2\u00e9\u00ea"+
		"\7k\2\2\u00ea\u00eb\7c\2\2\u00eb\u00ec\7u\2\2\u00ec\4\3\2\2\2\u00ed\u00ee"+
		"\7c\2\2\u00ee\u00ef\7u\2\2\u00ef\6\3\2\2\2\u00f0\u00f1\7g\2\2\u00f1\u00f2"+
		"\7n\2\2\u00f2\u00f3\7u\2\2\u00f3\u00f4\7g\2\2\u00f4\b\3\2\2\2\u00f5\u00f6"+
		"\7k\2\2\u00f6\u00f7\7h\2\2\u00f7\n\3\2\2\2\u00f8\u00f9\7p\2\2\u00f9\u00fa"+
		"\7g\2\2\u00fa\u00fb\7y\2\2\u00fb\f\3\2\2\2\u00fc\u00fd\7y\2\2\u00fd\u00fe"+
		"\7k\2\2\u00fe\u00ff\7v\2\2\u00ff\u0100\7j\2\2\u0100\16\3\2\2\2\u0101\u0102"+
		"\7t\2\2\u0102\u0103\7g\2\2\u0103\u0104\7v\2\2\u0104\u0105\7w\2\2\u0105"+
		"\u0106\7t\2\2\u0106\u0107\7p\2\2\u0107\20\3\2\2\2\u0108\u0109\7w\2\2\u0109"+
		"\u010a\7u\2\2\u010a\u010b\7g\2\2\u010b\22\3\2\2\2\u010c\u010d\7?\2\2\u010d"+
		"\24\3\2\2\2\u010e\u010f\7\177\2\2\u010f\26\3\2\2\2\u0110\u0111\7+\2\2"+
		"\u0111\30\3\2\2\2\u0112\u0113\7.\2\2\u0113\32\3\2\2\2\u0114\u0115\7\61"+
		"\2\2\u0115\34\3\2\2\2\u0116\u0117\7\60\2\2\u0117\36\3\2\2\2\u0118\u0119"+
		"\7?\2\2\u0119\u011a\7?\2\2\u011a \3\2\2\2\u011b\u011c\7?\2\2\u011c\u011d"+
		"\7@\2\2\u011d\"\3\2\2\2\u011e\u011f\7/\2\2\u011f$\3\2\2\2\u0120\u0121"+
		"\7}\2\2\u0121&\3\2\2\2\u0122\u0123\7*\2\2\u0123(\3\2\2\2\u0124\u0125\7"+
		"-\2\2\u0125*\3\2\2\2\u0126\u0127\7,\2\2\u0127,\3\2\2\2\u0128\u0129\7\'"+
		"\2\2\u0129.\3\2\2\2\u012a\u012b\7`\2\2\u012b\60\3\2\2\2\u012c\u012d\7"+
		"#\2\2\u012d\u012e\7?\2\2\u012e\62\3\2\2\2\u012f\u0130\7@\2\2\u0130\64"+
		"\3\2\2\2\u0131\u0132\7>\2\2\u0132\66\3\2\2\2\u0133\u0134\7@\2\2\u0134"+
		"\u0135\7?\2\2\u01358\3\2\2\2\u0136\u0137\7>\2\2\u0137\u0138\7?\2\2\u0138"+
		":\3\2\2\2\u0139\u013a\7(\2\2\u013a\u013b\7(\2\2\u013b<\3\2\2\2\u013c\u013d"+
		"\7~\2\2\u013d\u013e\7~\2\2\u013e>\3\2\2\2\u013f\u0140\7#\2\2\u0140@\3"+
		"\2\2\2\u0141\u0142\7=\2\2\u0142B\3\2\2\2\u0143\u0145\5\65\33\2\u0144\u0146"+
		"\t\2\2\2\u0145\u0144\3\2\2\2\u0146\u0147\3\2\2\2\u0147\u0145\3\2\2\2\u0147"+
		"\u0148\3\2\2\2\u0148\u0149\3\2\2\2\u0149\u014a\5\63\32\2\u014aD\3\2\2"+
		"\2\u014b\u014f\5G$\2\u014c\u014e\5I%\2\u014d\u014c\3\2\2\2\u014e\u0151"+
		"\3\2\2\2\u014f\u014d\3\2\2\2\u014f\u0150\3\2\2\2\u0150F\3\2\2\2\u0151"+
		"\u014f\3\2\2\2\u0152\u0155\5M\'\2\u0153\u0155\7a\2\2\u0154\u0152\3\2\2"+
		"\2\u0154\u0153\3\2\2\2\u0155H\3\2\2\2\u0156\u0159\5G$\2\u0157\u0159\5"+
		"O(\2\u0158\u0156\3\2\2\2\u0158\u0157\3\2\2\2\u0159J\3\2\2\2\u015a\u015b"+
		"\5U+\2\u015b\u015c\t\3\2\2\u015cL\3\2\2\2\u015d\u015e\t\4\2\2\u015eN\3"+
		"\2\2\2\u015f\u0160\t\5\2\2\u0160P\3\2\2\2\u0161\u0162\t\6\2\2\u0162R\3"+
		"\2\2\2\u0163\u0164\t\7\2\2\u0164T\3\2\2\2\u0165\u0169\5S*\2\u0166\u0168"+
		"\5O(\2\u0167\u0166\3\2\2\2\u0168\u016b\3\2\2\2\u0169\u0167\3\2\2\2\u0169"+
		"\u016a\3\2\2\2\u016aV\3\2\2\2\u016b\u0169\3\2\2\2\u016c\u0170\7$\2\2\u016d"+
		"\u016f\n\b\2\2\u016e\u016d\3\2\2\2\u016f\u0172\3\2\2\2\u0170\u016e\3\2"+
		"\2\2\u0170\u0171\3\2\2\2\u0171\u0173\3\2\2\2\u0172\u0170\3\2\2\2\u0173"+
		"\u017d\7$\2\2\u0174\u0178\7)\2\2\u0175\u0177\n\t\2\2\u0176\u0175\3\2\2"+
		"\2\u0177\u017a\3\2\2\2\u0178\u0176\3\2\2\2\u0178\u0179\3\2\2\2\u0179\u017b"+
		"\3\2\2\2\u017a\u0178\3\2\2\2\u017b\u017d\7)\2\2\u017c\u016c\3\2\2\2\u017c"+
		"\u0174\3\2\2\2\u017dX\3\2\2\2\u017e\u0180\7/\2\2\u017f\u017e\3\2\2\2\u017f"+
		"\u0180\3\2\2\2\u0180\u0181\3\2\2\2\u0181\u0188\5[.\2\u0182\u0184\7\60"+
		"\2\2\u0183\u0185\5O(\2\u0184\u0183\3\2\2\2\u0185\u0186\3\2\2\2\u0186\u0184"+
		"\3\2\2\2\u0186\u0187\3\2\2\2\u0187\u0189\3\2\2\2\u0188\u0182\3\2\2\2\u0188"+
		"\u0189\3\2\2\2\u0189\u018b\3\2\2\2\u018a\u018c\5]/\2\u018b\u018a\3\2\2"+
		"\2\u018b\u018c\3\2\2\2\u018cZ\3\2\2\2\u018d\u0196\7\62\2\2\u018e\u0192"+
		"\5S*\2\u018f\u0191\5O(\2\u0190\u018f\3\2\2\2\u0191\u0194\3\2\2\2\u0192"+
		"\u0190\3\2\2\2\u0192\u0193\3\2\2\2\u0193\u0196\3\2\2\2\u0194\u0192\3\2"+
		"\2\2\u0195\u018d\3\2\2\2\u0195\u018e\3\2\2\2\u0196\\\3\2\2\2\u0197\u0199"+
		"\t\n\2\2\u0198\u019a\t\13\2\2\u0199\u0198\3\2\2\2\u0199\u019a\3\2\2\2"+
		"\u019a\u019b\3\2\2\2\u019b\u019c\5[.\2\u019c^\3\2\2\2\u019d\u019f\t\f"+
		"\2\2\u019e\u019d\3\2\2\2\u019f\u01a0\3\2\2\2\u01a0\u019e\3\2\2\2\u01a0"+
		"\u01a1\3\2\2\2\u01a1\u01a2\3\2\2\2\u01a2\u01a3\b\60\2\2\u01a3`\3\2\2\2"+
		"\u01a4\u01a8\5c\62\2\u01a5\u01a7\5e\63\2\u01a6\u01a5\3\2\2\2\u01a7\u01aa"+
		"\3\2\2\2\u01a8\u01a6\3\2\2\2\u01a8\u01a9\3\2\2\2\u01a9b\3\2\2\2\u01aa"+
		"\u01a8\3\2\2\2\u01ab\u01ad\t\r\2\2\u01ac\u01ab\3\2\2\2\u01add\3\2\2\2"+
		"\u01ae\u01b1\5c\62\2\u01af\u01b1\4\62;\2\u01b0\u01ae\3\2\2\2\u01b0\u01af"+
		"\3\2\2\2\u01b1f\3\2\2\2\u01b2\u01bc\5o8\2\u01b3\u01b6\5i\65\2\u01b4\u01b6"+
		"\5k\66\2\u01b5\u01b3\3\2\2\2\u01b5\u01b4\3\2\2\2\u01b6\u01b8\3\2\2\2\u01b7"+
		"\u01b9\5m\67\2\u01b8\u01b7\3\2\2\2\u01b8\u01b9\3\2\2\2\u01b9\u01ba\3\2"+
		"\2\2\u01ba\u01bb\5o8\2\u01bb\u01bd\3\2\2\2\u01bc\u01b5\3\2\2\2\u01bc\u01bd"+
		"\3\2\2\2\u01bdh\3\2\2\2\u01be\u01bf\7G\2\2\u01bfj\3\2\2\2\u01c0\u01c1"+
		"\7g\2\2\u01c1l\3\2\2\2\u01c2\u01c3\t\13\2\2\u01c3n\3\2\2\2\u01c4\u01c6"+
		"\4\62;\2\u01c5\u01c4\3\2\2\2\u01c6\u01c7\3\2\2\2\u01c7\u01c5\3\2\2\2\u01c7"+
		"\u01c8\3\2\2\2\u01c8\u01cf\3\2\2\2\u01c9\u01cb\7\60\2\2\u01ca\u01cc\4"+
		"\62;\2\u01cb\u01ca\3\2\2\2\u01cc\u01cd\3\2\2\2\u01cd\u01cb\3\2\2\2\u01cd"+
		"\u01ce\3\2\2\2\u01ce\u01d0\3\2\2\2\u01cf\u01c9\3\2\2\2\u01cf\u01d0\3\2"+
		"\2\2\u01d0p\3\2\2\2\u01d1\u01d2\t\'\2\2\u01d2r\3\2\2\2\u01d3\u01d4\t("+
		"\2\2\u01d4t\3\2\2\2\u01d5\u01d6\7\62\2\2\u01d6v\3\2\2\2\u01d7\u01d8\7"+
		"\63\2\2\u01d8x\3\2\2\2\u01d9\u01da\7\64\2\2\u01daz\3\2\2\2\u01db\u01dc"+
		"\7\65\2\2\u01dc|\3\2\2\2\u01dd\u01de\7\66\2\2\u01de~\3\2\2\2\u01df\u01e0"+
		"\7\67\2\2\u01e0\u0080\3\2\2\2\u01e1\u01e2\78\2\2\u01e2\u0082\3\2\2\2\u01e3"+
		"\u01e4\79\2\2\u01e4\u0084\3\2\2\2\u01e5\u01e6\7:\2\2\u01e6\u0086\3\2\2"+
		"\2\u01e7\u01e8\7;\2\2\u01e8\u0088\3\2\2\2\u01e9\u01ea\t\16\2\2\u01ea\u008a"+
		"\3\2\2\2\u01eb\u01ec\t\17\2\2\u01ec\u008c\3\2\2\2\u01ed\u01ee\t\20\2\2"+
		"\u01ee\u008e\3\2\2\2\u01ef\u01f0\t\21\2\2\u01f0\u0090\3\2\2\2\u01f1\u01f2"+
		"\t\n\2\2\u01f2\u0092\3\2\2\2\u01f3\u01f4\t\22\2\2\u01f4\u0094\3\2\2\2"+
		"\u01f5\u01f6\t\23\2\2\u01f6\u0096\3\2\2\2\u01f7\u01f8\t\24\2\2\u01f8\u0098"+
		"\3\2\2\2\u01f9\u01fa\t\25\2\2\u01fa\u009a\3\2\2\2\u01fb\u01fc\t\26\2\2"+
		"\u01fc\u009c\3\2\2\2\u01fd\u01fe\t\27\2\2\u01fe\u009e\3\2\2\2\u01ff\u0200"+
		"\t\30\2\2\u0200\u00a0\3\2\2\2\u0201\u0202\t\31\2\2\u0202\u00a2\3\2\2\2"+
		"\u0203\u0204\t\32\2\2\u0204\u00a4\3\2\2\2\u0205\u0206\t\33\2\2\u0206\u00a6"+
		"\3\2\2\2\u0207\u0208\t\34\2\2\u0208\u00a8\3\2\2\2\u0209\u020a\t\35\2\2"+
		"\u020a\u00aa\3\2\2\2\u020b\u020c\t\36\2\2\u020c\u00ac\3\2\2\2\u020d\u020e"+
		"\t\37\2\2\u020e\u00ae\3\2\2\2\u020f\u0210\t \2\2\u0210\u00b0\3\2\2\2\u0211"+
		"\u0212\t!\2\2\u0212\u00b2\3\2\2\2\u0213\u0214\t\"\2\2\u0214\u00b4\3\2"+
		"\2\2\u0215\u0216\t#\2\2\u0216\u00b6\3\2\2\2\u0217\u0218\t$\2\2\u0218\u00b8"+
		"\3\2\2\2\u0219\u021a\t%\2\2\u021a\u00ba\3\2\2\2\u021b\u021c\t&\2\2\u021c"+
		"\u00bc\3\2\2\2\u021d\u021e\7<\2\2\u021e\u021f\7<\2\2\u021f\u00be\3\2\2"+
		"\2\u0220\u0221\7<\2\2\u0221\u00c0\3\2\2\2\u0222\u0223\7/\2\2\u0223\u00c2"+
		"\3\2\2\2\u0224\u0225\7\u0080\2\2\u0225\u00c4\3\2\2\2\u0226\u0227\7a\2"+
		"\2\u0227\u00c6\3\2\2\2\u0228\u0229\7#\2\2\u0229\u00c8\3\2\2\2\u022a\u022b"+
		"\7&\2\2\u022b\u00ca\3\2\2\2\u022c\u022d\7(\2\2\u022d\u00cc\3\2\2\2\u022e"+
		"\u022f\7)\2\2\u022f\u00ce\3\2\2\2\u0230\u0231\7*\2\2\u0231\u00d0\3\2\2"+
		"\2\u0232\u0233\7+\2\2\u0233\u00d2\3\2\2\2\u0234\u0235\7,\2\2\u0235\u00d4"+
		"\3\2\2\2\u0236\u0237\7=\2\2\u0237\u00d6\3\2\2\2\u0238\u0239\7?\2\2\u0239"+
		"\u00d8\3\2\2\2\u023a\u023b\7\61\2\2\u023b\u023c\7\61\2\2\u023c\u00da\3"+
		"\2\2\2\u023d\u023e\7\61\2\2\u023e\u00dc\3\2\2\2\u023f\u0240\7A\2\2\u0240"+
		"\u00de\3\2\2\2\u0241\u0242\7%\2\2\u0242\u00e0\3\2\2\2\u0243\u0244\7]\2"+
		"\2\u0244\u00e2\3\2\2\2\u0245\u0246\7_\2\2\u0246\u00e4\3\2\2\2\u0247\u0248"+
		"\7B\2\2\u0248\u00e6\3\2\2\2\34\2\u0147\u014f\u0154\u0158\u0169\u0170\u0178"+
		"\u017c\u017f\u0186\u0188\u018b\u0192\u0195\u0199\u01a0\u01a8\u01ac\u01b0"+
		"\u01b5\u01b8\u01bc\u01c7\u01cd\u01cf\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}