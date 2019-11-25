grammar SRZ;
options
{
   language = CSharp;
   //language = Cpp;
}
@parser::namespace { Roslesinforg.Sigma.ExpressionParser }
@lexer::namespace  { Roslesinforg.Sigma.ExpressionLexer }
/*
 * Parser Rules
 */
 start      : (exp=expression)? EOF
			;

expression  : OPENBR expression CLOSEBR		#parenthesisExp
			| left=expression op=(EQU|NOTEQU) right=set  #opSetComp
            | left=expression op=(MUL|DIV) right=expression  #opExp 		
            | left=expression op=(ADD|SUB) right=expression  #opExp
			| NOT expression # opNotExp
			// opExpComp: результат операции = bool, операнды: decimal
			| left=expression op=(EQU|NOTEQU) right=expression  #opExpComp
            | left=expression op=(LT|LE|GT|GE) right=expression #opExpComp
            // opExpBool: результат операции = bool, операнды: bool
			| left=expression op=(OR|AND) right=expression #opExpBool
            | function #funcExp
	        | variable #varExp
            | constant #constExp
            ;

//params
//  :  expression (COMMA expression)* 
//  ;

function
  : fnAny
  ;

// переменна€: m23, экземпл€р макета 1
// переменна€: m23[2], экземпл€р макета 2

variable : name=ID | name=ID OPENSQBR exam=INT CLOSESQBR | name=ID OPENSQBR range=range CLOSESQBR;
// ƒопускаютс€ следующие вариации 1..4 или 0.1..99.4 или 3..5.5
range    : from=(INT|NUMBER) DOT DOT DOT? to=(INT | NUMBER);
set      : (INT|NUMBER|range) (COMMA (INT|NUMBER|range))*?;   
constant : typ=(INT | NUMBER | STRING)
         ;

// function rules
fnAny : 'Any' OPENBR mak=ID COMMA cond=expression CLOSEBR;

/*
 * Lexer Rules
 */
DOT		            : '.';
COMMA               : ',';
OPENBR              : '(';
CLOSEBR             : ')';
OPENSQBR			: '[';
CLOSESQBR			: ']';
MUL                 : '*';
DIV                 : '/';
ADD                 : '+';
SUB                 : '-';

fragment LETTER     : [A-Za-z];
fragment DIGIT      : [0-9];
UNDERSCORE          : '_'; 
NOT					: N O T;
AND                 : '&'| A N D;
OR                  : '!' | '|' | O R;
ID                  : [A-Za-z_][A-Za-z_0-9]*;
INT                 : DIGIT+;
NUMBER              : DIGIT+ (DOT DIGIT+)? ;
EQU                 : '=';
NOTEQU              : '!='|'<>'|'#';
LT                  : '<';
GT                  : '>';
LE	                : LT EQU;
GE	                : GT EQU;
WHITESPACE          : (' '|'\t')+ -> skip ;
COMMENT             :  ';' ~( '\r' | '\n' )* -> channel(HIDDEN);
NEWLINE             : ('\r'? '\n' | '\r')+ -> skip;


STRING
 : '"' (ESC | ~ ["\\])* '"'
 ;

fragment O: [oO];
fragment R: [rR];
fragment A: [aA];
fragment N: [nN];
fragment D: [dD];
fragment T: [tT];


fragment ESC
 : '\\' (["\\/bfnrt] | UNICODE)
 ;
fragment UNICODE
 : 'u' HEX HEX HEX HEX
 ;
fragment HEX
 : [0-9a-fA-F]
 ;
