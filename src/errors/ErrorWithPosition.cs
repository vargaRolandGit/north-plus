using North;

class ErrorWithPosition : Exception {
    public ErrorWithPosition(string errorMSG, Token token) : base(
        $"ERROR: {errorMSG}\n\t"
        + $"file: {token.FileName}\n\t"
        + $"line: {token.Line}"
    ) {}
}