using System.Globalization;

/*
TODO: refactor to this shi:
    
    bool Match(TokenType Type) {
      if (_tokens[i].Type == Type) {
        return true;
      }
    
      throw new Exception($"Expected {Type}, got {_tokens[i].Type}");
    }

*/

namespace North;

class Interpreter {

    List<Token> _tokens;

    public List<double> _stack;

    

    public Interpreter(List<Token> tokens) {
        _tokens = tokens;
        _stack = new();
    }


    // TODO: Refactor to switch
    public void Run(int startIndex, int endIndex) {
        for (int i = startIndex; i < endIndex; i++) {
            if (_tokens[i].Type == TokenType.PUSH) {
                if (_tokens[i + 1].Type == TokenType.NUMBER) {
                    _stack.Add(double.Parse(_tokens[i+1].Value,
                    CultureInfo.InvariantCulture));   
                } else {
                    throw new Exception("No int"); // TODO error handling
                }   
            } else if (_tokens[i].Type == TokenType.ADD) {
                Add(_tokens[i]);
            } else if (_tokens[i].Type == TokenType.STACK) {
                Stack();
            } else if (_tokens[i].Type == TokenType.POP) {
                Console.WriteLine(Pop(_tokens[i]));
            } else if (_tokens[i].Type == TokenType.DUP) {
                Dup(_tokens[i]);
            } else if (_tokens[i].Type == TokenType.MUL) {
                Mul(_tokens[i]);
            } else if (_tokens[i].Type == TokenType.PRINT) {
                if (_tokens[i+1].Type == TokenType.STRING_LITERAL) {
                    Console.Write(_tokens[i+1].Value);
                } 
                else {
                    throw new Exception("it has to be a string literal");
                     //TODO: Error: String literal excepted, where why etc...
                }
                // TODO: refactor this if shi-
            } else if (_tokens[i].Type == TokenType.IF) {
                if (_tokens[i+1].Type == TokenType.NUMBER) {
                    var num = double.Parse(_tokens[i+1].Value, CultureInfo.InvariantCulture);
                    int ifBlockStart  = i + 3;
                    int ifBlockEnd    = i + 3;
                    if (_tokens[i+2].Type == TokenType.BLOCK_START) {
                        while (_tokens[ifBlockEnd].Type != TokenType.BLOCK_END) {
                            ifBlockEnd++;
                        }
                        if (top() == num) {
                                i = ifBlockEnd;
                                int blockEnd = i + 3; 
                                if (_tokens[i+1].Type == TokenType.ELSE) 
                                    while (_tokens[blockEnd].Type != TokenType.BLOCK_END) {
                                        blockEnd++;
                                    }
                                Run(ifBlockStart, ifBlockEnd - 1);
                                i = blockEnd;
                        } else {
                            // TODO: else block
                            i = ifBlockEnd;
                            var elseBlockStart = i+3;
                            var elseBlockEnd = i+3;
                            if (_tokens[i+1].Type == TokenType.ELSE) {
                                if (_tokens[i+2].Type == TokenType.BLOCK_START) {
                                    while (_tokens[elseBlockEnd].Type != TokenType.BLOCK_END) {
                                        elseBlockEnd++;
                                    }
                                    i = elseBlockEnd;
                                    Run(elseBlockStart, elseBlockEnd - 1);
                                }
                            } 
                        }  
                    } 
                } else if (_tokens[i+1].Type == TokenType.BLOCK_START) {
                    // TODO: just the top element 
                } else {
                    // TODO: Error handling :( 
                    throw new Exception("it has to be a string literal");
                }
            }
            /*------------*/ 
            else {
                //TODO: error
            }
        }
    }

    double top() => _stack[_stack.Count - 1];

    void Dup(Token token) {
        if (_stack.Count == 0) throw new EmptyStackError(token);
        Push(top());
    }

    void Mul(Token token) {
        if (_stack.Count < 2) throw new NotEoughElementsError(token);
        var a = Pop(token);
        var b = Pop(token);
        Push(a*b);
    }

    void Stack() {
        Console.Write($"<{_stack.Count}> [");
        if (_stack.Count == 1) Console.Write($"{_stack[0]}");
        else 
            for (int i = 0; i < _stack.Count; ++i) {
                if (i == _stack.Count - 1)
                    Console.Write($"{_stack[i]}");
                else 
                    Console.Write($"{_stack[i]}, ");
            }
        Console.Write($"]");
    }

    double Pop(Token token) {
        if (_stack.Count == 0) throw new EmptyStackError(token);
        var last = _stack[_stack.Count - 1];
        _stack.RemoveAt(_stack.Count - 1);
        return last;
    }

    void Switch() {

    }

    void Push(double element) {
        _stack.Add(element);
    }

    void Add(Token token) {
        if (_stack.Count < 2) throw new NotEoughElementsError(token);
        var a = Pop(token);
        var b = Pop(token);
        Push(a+b);
    }

}