[System.Serializable]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class FalhaAoEnviarEmailException : System.Exception
{
    public FalhaAoEnviarEmailException() { }
    public FalhaAoEnviarEmailException(string message) : base(message) { }
    public FalhaAoEnviarEmailException(string message, System.Exception inner) : base(message, inner) { }
    protected FalhaAoEnviarEmailException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}