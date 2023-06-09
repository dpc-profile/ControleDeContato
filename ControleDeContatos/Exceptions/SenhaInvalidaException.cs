namespace namesource.ControleDeContatos.Exceptions
{
    [System.Serializable]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SenhaInvalidaException : System.Exception
    {
        public SenhaInvalidaException() { }
        public SenhaInvalidaException(string message) : base(message) { }
        public SenhaInvalidaException(string message, System.Exception inner) : base(message, inner) { }
        protected SenhaInvalidaException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}