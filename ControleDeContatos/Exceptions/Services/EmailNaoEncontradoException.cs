[System.Serializable]
public class EmailNaoEncontradoException : System.Exception
{
    public EmailNaoEncontradoException() { }
    public EmailNaoEncontradoException(string message) : base(message) { }
    public EmailNaoEncontradoException(string message, System.Exception inner) : base(message, inner) { }
    protected EmailNaoEncontradoException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}