grammar ArrayPP;

program: line* EOF;

line: statement;

statement
    : declaration ';'
    | variableDeclaration ';'
    | assignment ';'
    | functionCall ';'
    | ifBlock
    | forCycle
    | whileCycle
    | function
    | printToScreen ';'
    | return ';'
    | arrayAssignment ';'
    | arrayRemoval ';'
    | arrayInsert ';'
    | arrayRandomiser ';'
    | arrayFill ';'
    | arrayFilter ';'
    | arrayUnique ';'
    | arraySlice ';'
    ;

ifBlock: 'if' '(' expression ')' block ('else' elseIfBlock)?;
elseIfBlock: block | ifBlock;
declaration: type IDENTIFIER;
variableDeclaration: declaration '=' (expression | arrayLiteral);
assignment:  IDENTIFIER '=' expression;

arrayAssignment: IDENTIFIER '[' expression ']' '=' expression;
arrayRemoval: IDENTIFIER '[' expression ']' '.remove';
arrayInsert: (IDENTIFIER | functionCall) '.insert(' expression ',' expression ')';
arrayLength: (IDENTIFIER | functionCall) '.length';
arrayRandomiser: (IDENTIFIER | functionCall) '.randomise';
arrayFill: (IDENTIFIER | functionCall ) '.fill(' expression ',' expression ')';
arrayFilter: (IDENTIFIER | functionCall ) '.filter(' expression ',' expression ')';
arrayUnique: (IDENTIFIER | functionCall ) '.unique';
arraySlice: (IDENTIFIER | functionCall ) '.slice(' expression ',' expression ')';

functionCall: IDENTIFIER '(' (expression (',' expression)*)? ')';

forCycle: 'for' '(' (variableDeclaration | assignment) ';' expression ';' assignment ')' block;
whileCycle: 'while' '(' expression ')' block;

function: 'function' IDENTIFIER '(' (declaration (',' declaration)*)? ')' (':' type)? block;

return:  'return' expression?;

printToScreen: 'print(' expression ')';
readFromFile: 'readFile(' expression ')';

substring: 'substring(' expression ',' expression ')';
split:  (IDENTIFIER | functionCall)'.split(' (expression) ')';


expression
    : IDENTIFIER                            #identifierExpression
    | functionCall                          #functionCallExpression
    | '(' expression ')'                    #parenExpression
    | expression multOp expression          #multExpression
    | expression addOp expression           #addExpression
    | expression compareOp expression       #compareExpression
    | expression boolOp expression          #boolExpression
    | arrayLiteral                          #arrayExpression
    | indexAccess                           #indexAccessExpression
    | readFromFile                          #readFromFileExprission
    | substring                             #substringExpression
    | split                                 #splitExpression
    | arrayLength                           #lengthExpression
    | arrayUnique                           #uniqueExpression
    | constant                              #constantExpression
    ;

multOp: '*' | '/';
addOp: '+' | '-';
compareOp: '==' | '!=' | '<' | '>' | '<=' | '>=';
boolOp: '&&' | '||';


arrayLiteral: ('[' expressionList? ']') | '[]';
expressionList: expression (',' (expression))*;


indexAccess: IDENTIFIER '[' expression ']';

primitiveType:
    'int'
    |'char'
    |'string'
    |'bool'
    |'float';

type: primitiveType | (primitiveType '[]');

constant:
    INTEGER     #integerConstant
    | FLOAT     #floatConstant
    | STRING    #stringConstant
    | BOOL      #boolConstant
    | CHAR      #charConstant
    | NULL      #nullConstant
    ;

INTEGER: ('-')? [0-9]+;
FLOAT: ('-')? [0-9]+ '.' [0-9]+;
STRING: '"' (~["\\])* '"';
CHAR: '\'' ~'\'' '\'';
BOOL: 'true' | 'false';
NULL: 'null';

block: '{' line* '}';
COMMENT : ( '//' ~[\r\n]*  ) -> skip ;
WS: [ \t\r\n]+ -> skip;
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;