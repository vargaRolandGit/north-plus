using System;

namespace North;
class Lexer {

    public Dictionary<string, TokenType> Keywords = new() {
        {"push", TokenType.PUSH}, 
        {"pop", TokenType.POP}, 
        {"stack", TokenType.STACK},
        {"add", TokenType.ADD},
    };

    readonly string _fileName;
    List<Token> _tokens = new();
    int _currentPos = 0;
    int _currentLine = 1;

    public Lexer(string fileName) {
        this._fileName = fileName;
    }

    char shift(char[] src) {
        return src[_currentPos++];
    }

    void AddToken(string value, TokenType type, char[] src) {
        _tokens.Add(new Token(value, type) {
                    Line = _currentLine,
                    Char = _currentPos,
                    TokenPos = _tokens.Count - 1,
                    FileName = _fileName
        });
        shift(src);
    }

    public List<Token> Tokenize(string sourceCode) {
        char[] src = sourceCode.ToCharArray(); 

        while ((_currentPos + 1) <= src.Length) {
            if (src[_currentPos] == '\n') {
                _currentLine++;
                shift(src);
            } else if (src[_currentPos] == ' ' || src[_currentPos] == '\t') {
                shift(src);
            } else if (src[_currentPos] == '#') {
                while (!(src[_currentPos] == '\n')) {
                    shift(src);
                }
                _currentLine++;
                shift(src);
            } else if (Char.IsNumber(src[_currentPos])) {
                string buffer = "";
                while (Char.IsNumber(src[_currentPos])
                 || src[_currentPos] == '.') {
                    buffer += src[_currentPos].ToString();
                    shift(src);
                }
                AddToken(buffer, TokenType.NUMBER, src);
            } else if (Char.IsLetter(src[_currentPos])) {
                string buffer = "";
                while (Char.IsLetterOrDigit(src[_currentPos])
                 && !Char.IsWhiteSpace(src[_currentPos])) {
                    buffer += src[_currentPos].ToString();
                    shift(src);
                }
                TokenType type;
                if (!Keywords.TryGetValue(buffer, out type)) {
                    throw new Exception("Error: unkown identifier at " +
                        "line: " + _currentLine +
                        " file: " + _fileName +  "!"
                    );
                } 
                switch (type) {
                    case TokenType.PUSH:
                        AddToken(buffer, TokenType.PUSH, src);
                    break;
                    case TokenType.POP:
                        AddToken(buffer, TokenType.POP, src);
                    break;                    
                    case TokenType.STACK:
                        AddToken(buffer, TokenType.STACK, src);
                    break;
                    case TokenType.ADD:
                        AddToken(buffer, TokenType.ADD, src);
                    break;                      
                }
            }

            else {
                throw new Exception("Error: unkown character at " +
                    "line: " + _currentLine +
                    " char: " + _currentPos + 
                    " file: " + _fileName +  "!"
                );
            }
        }

        return _tokens;
    }
    
}