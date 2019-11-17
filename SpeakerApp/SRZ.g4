grammar SRZ;
options
{
   //language = CSharp;
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
            | left=expression op=(MUL|DIV) right=expression  #opExp 		
            | left=expression op=(ADD|SUB) right=expression  #opExp
			// opExpComp: результат операции = bool, операнды: decimal
			| left=expression op=(EQU|NOTEQU) right=set  #opSetComp
			| left=expression op=(EQU|NOTEQU) right=expression  #opExpComp
            | left=expression op=(LT|LE|GT|GE) right=expression #opExpComp
            // opExpBool: результат операции = bool, операнды: bool
			| left=expression op=(OR|AND) right=expression #opExpBool
            | function #funcExp
	        | variable #varExp
            | literal #literalExp
            ;

params
  :  expression (COMMA expression)* 
  ;

function
  : fnAny
  ;

variable : ID;
range    : from=INT DOT DOT DOT? to=INT;
set      : (literal|range) (COMMA (literal | range) )*;   
literal  : typ=(INT | NUMBER | STRING)
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
MUL                 : '*';
DIV                 : '/';
ADD                 : '+';
SUB                 : '-';
fragment LETTER     : [A-Za-z];
fragment DIGIT      : [0-9];
UNDERSCORE          : '_'; 
ID                  : [A-Za-z_][A-Za-z_0-9]*;
INT                 : DIGIT+;
NUMBER              : DIGIT+ (DOT DIGIT+)? ;
EQU                 : '=';
NOTEQU              : '!='|'<>'|'#';
AND                 : '&'|'AND';
OR                  : '!' | '|' | 'OR';
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

fragment ESC
 : '\\' (["\\/bfnrt] | UNICODE)
 ;
fragment UNICODE
 : 'u' HEX HEX HEX HEX
 ;
fragment HEX
 : [0-9a-fA-F]
 ;
