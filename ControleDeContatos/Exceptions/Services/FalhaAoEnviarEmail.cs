[System.Serializable]
public class FalhaAoEnviarEmail : System.Exception
{
    public FalhaAoEnviarEmail() { }
    public FalhaAoEnviarEmail(string message) : base(message) { }
    public FalhaAoEnviarEmail(string message, System.Exception inner) : base(message, inner) { }
    protected FalhaAoEnviarEmail(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}