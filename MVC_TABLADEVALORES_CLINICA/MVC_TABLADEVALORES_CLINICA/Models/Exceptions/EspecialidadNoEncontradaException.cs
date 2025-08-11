namespace MVC_TABLADEVALORES_CLINICA.Models.Exceptions
{
    public class EspecialidadNoEncontradaException: Exception
    {
        public EspecialidadNoEncontradaException() { }

        public EspecialidadNoEncontradaException(string message) : base(message) { }

        public EspecialidadNoEncontradaException(string message, Exception inner) : base(message, inner) { }

    }
}
