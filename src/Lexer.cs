using System;

namespace North;
class Lexer {

    public Dictionary<string, TokenType> Keywords = new() {
        {"push", TokenType.PUSH}, 
        {"pop", TokenType.POP}, 
        {"stack", TokenType.STACK},
        {"add", TokenType.ADD},
        {"dup", TokenType.DUP},
        {"mul", TokenType.MUL},
        {"print", TokenType.PRINT},
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
            }
            /*STRING LITERALS*/
            else if (src[_currentPos] == '"') {
                shift(src);
                string buffer = "";
                while (!(src[_currentPos] == '"')) {
                    buffer += src[_currentPos].ToString();
                    shift(src);
                }
                AddToken(buffer, TokenType.STRING_LITERAL, src);
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
                AddToken(buffer, type, src);
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