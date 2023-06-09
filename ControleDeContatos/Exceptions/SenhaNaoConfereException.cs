namespace namesource.ControleDeContatos.Exceptions
{
    [System.Serializable]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SenhaNaoConfereException : System.Exception
    {
        public SenhaNaoConfereException() { }
        public SenhaNaoConfereException(string message) : base(message) { }
        public SenhaNaoConfereException(string message, System.Exception inner) : base(message, inner) { }
        protected SenhaNaoConfereException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}