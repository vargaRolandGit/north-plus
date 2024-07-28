namespace North;

enum TokenType {

    PROC, DEFINE, EXIT,
    COMMENT, IDENTIFIER, STRING_LITERAL, NUMBER,

    ADD, SUB, DIV, MUL, 
    POP, PUSH, SWITH, DUP, STACK,

    PRINT,

    EOF    
} 

class Token {

    public string Value;
    public TokenType Type;
    public int? TokenPos;
    public string? FileName;
    public int? Line;
    public int? Char;

    public Token(string value, TokenType type) {
        this.Value = value;
        this.Type = type;
    }

}