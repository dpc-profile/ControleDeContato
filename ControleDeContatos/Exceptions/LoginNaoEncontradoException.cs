[System.Serializable]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class LoginNaoEncontradoException : System.Exception
{
    public LoginNaoEncontradoException() { }
    public LoginNaoEncontradoException(string message) : base(message) { }
    public LoginNaoEncontradoException(string message, System.Exception inner) : base(message, inner) { }
    protected LoginNaoEncontradoException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}