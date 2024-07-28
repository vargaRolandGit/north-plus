using System.Globalization;

namespace North;

class Interpreter {

    List<Token> _tokens;

    public List<double> _stack;

    public Interpreter(List<Token> tokens) {
        _tokens = tokens;
        _stack = new();
    }

    void PreProcess() { // finding entrypoint and making labels

    }

    public void Run(int startIndex, int endIndex) {
        for (int i = startIndex; i < endIndex; i++) {
            if (_tokens[i].Type == TokenType.PUSH) {
                if (_tokens[i + 1].Type == TokenType.NUMBER) {
                    _stack.Add(double.Parse(_tokens[i+1].Value, CultureInfo.InvariantCulture));   
                } else {
                    throw new Exception("No int"); // error handling
                }   
            } 
        }
    }

}