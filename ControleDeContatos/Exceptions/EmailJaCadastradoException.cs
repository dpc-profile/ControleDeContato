[System.Serializable]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class EmailJaCadastradoException : System.Exception
{
    public EmailJaCadastradoException() { }
    public EmailJaCadastradoException(string message) : base(message) { }
    public EmailJaCadastradoException(string message, System.Exception inner) : base(message, inner) { }
    protected EmailJaCadastradoException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}