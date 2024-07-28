using System.Globalization;

namespace North;

class Interpreter {

    List<Token> _tokens;

    public List<double> _stack;

    public Interpreter(List<Token> tokens) {
        _tokens = tokens;
        _stack = new();
    }

    public void Run(int startIndex, int endIndex) {
        for (int i = startIndex; i < endIndex; i++) {
            if (_tokens[i].Type == TokenType.PUSH) {
                if (_tokens[i + 1].Type == TokenType.NUMBER) {
                    _stack.Add(double.Parse(_tokens[i+1].Value, CultureInfo.InvariantCulture));   
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
        if (_stack.Count < 2) throw new NotEoughElementsError(token); // TODO
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
        if (_stack.Count == 0) throw new EmptyStackError(token); // TODO
        var last = _stack[_stack.Count - 1];
        _stack.RemoveAt(_stack.Count - 1);
        return last;
    }

    void Push(double element) {
        _stack.Add(element);
    }

    void Add(Token token) {
        if (_stack.Count < 2) throw new NotEoughElementsError(token); // TODO
        var a = Pop(token);
        var b = Pop(token);
        Push(a+b);
    }

}