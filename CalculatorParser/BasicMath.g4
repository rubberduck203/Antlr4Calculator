grammar BasicMath;

/*
 * Parser Rules
 */

compileUnit : expression EOF;

expression :
    expression operatorToken=(MULTIPLY | DIVIDE) expression #Multiplicative
	| expression operatorToken=(ADD | SUBTRACT) expression #Additive
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
