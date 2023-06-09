namespace namesource.ControleDeContatos.Exceptions
{
    [System.Serializable]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class UsuarioInvalidoException : System.Exception
    {
        public UsuarioInvalidoException() { }
        public UsuarioInvalidoException(string message) : base(message) { }
        public UsuarioInvalidoException(string message, System.Exception inner) : base(message, inner) { }
        protected UsuarioInvalidoException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}