using North;

class EmptyStackError : ErrorWithPosition {
    public EmptyStackError(Token token) : 
    base("The Stack Is Empty!\n\tYou can't do this operation, the stack is empty."
    ,token) {
    }
}

class NotEoughElementsError : ErrorWithPosition {
    public NotEoughElementsError(Token token) : base(
        "Not Enough Elements In The Stack!\n\tYou can't do this operation!"
        ,token
    ) {}
}