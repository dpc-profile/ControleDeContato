[System.Serializable]
public class LoginJaCadastradoException : System.Exception
{
    public LoginJaCadastradoException() { }
    public LoginJaCadastradoException(string message) : base(message) { }
    public LoginJaCadastradoException(string message, System.Exception inner) : base(message, inner) { }
    protected LoginJaCadastradoException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}