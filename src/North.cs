using System;
using System.IO;
using System.Text;


namespace North;
class North {

    static void Usage() {
        // TODO
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

        //foreach (var item in tokens)
        //{   
        //    Console.WriteLine(item.Value + " || " + item.Type);
        //}
        
    }

}