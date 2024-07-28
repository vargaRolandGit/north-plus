using System;
using System.IO;
using System.Text;

namespace North;
class North {

    static void Usage() {

    } 
    
    static string ReadFile(string filePath) {
        if (!File.Exists(filePath)) throw new Exception("This File Not Exists!");
        return File.ReadAllText(filePath) + "\n";
    }

    static void Main(string[] args) {
        
        var path = "../tests/test.pnorth";
        var sourceCode = ReadFile(path);
        Lexer lexer = new Lexer(path);

        List<Token> tokens = lexer.Tokenize(sourceCode);
        Interpreter interpreter = new Interpreter(tokens);
        interpreter.Run(0, tokens.Count);

        foreach (var item in interpreter._stack) {
            System.Console.WriteLine(item);
        }

        //foreach (Token token in lexer.Tokenize(sourceCode)) {
        //    System.Console.WriteLine("-------------------");
        //    Console.WriteLine($"| TokenVal: {token.Value}, TokenType: {token.Type} |");
        //}


    }

}