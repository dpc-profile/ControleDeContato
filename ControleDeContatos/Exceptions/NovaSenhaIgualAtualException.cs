[System.Serializable]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class NovaSenhaIgualAtualException : System.Exception
{
    public NovaSenhaIgualAtualException() { }
    public NovaSenhaIgualAtualException(string message) : base(message) { }
    public NovaSenhaIgualAtualException(string message, System.Exception inner) : base(message, inner) { }
    protected NovaSenhaIgualAtualException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}