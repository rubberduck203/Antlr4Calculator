grammar BasicMath;

/*
 * Parser Rules
 */

compileUnit : expression+ EOF;

expression :
    expression MULTIPLY expression #Multiplication
	| expression DIVIDE expression #Division
	| expression ADD expression #Addition
	| expression SUBTRACT expression #Subtraction
	| NUMBER #Number
	; 

/*
 * Lexer Rules
 */

NUMBER : INT; //Leave room to extend what kind of math we can do.

INT : ('0'..'9')+;
MULTIPLY : '*';
DIVIDE : '/';
SUBTRACT : '-';
ADD : '+';

WS : [ \t\r\n] -> channel(HIDDEN);
