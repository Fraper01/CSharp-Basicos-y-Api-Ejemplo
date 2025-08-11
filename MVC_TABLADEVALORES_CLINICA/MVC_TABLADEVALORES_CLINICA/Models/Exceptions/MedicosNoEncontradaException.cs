namespace MVC_TABLADEVALORES_CLINICA.Models.Exceptions
{
    public class MedicosNoEncontradaException: Exception
    {
        public MedicosNoEncontradaException() { }

        public MedicosNoEncontradaException(string message) : base(message) { }

        public MedicosNoEncontradaException(string message, Exception inner) : base(message, inner) { }

    }
}
