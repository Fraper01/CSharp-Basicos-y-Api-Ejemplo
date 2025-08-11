using System.Resources;

namespace MVC_TABLADEVALORES_PRODUCTO.Recursos
{
    public static class MensajesError
    {
        private static readonly ResourceManager resourceMan = new ResourceManager("MVC_TABLADEVALORES_PRODUCTO.Recursos.MensajesError", typeof(MensajesError).Assembly);

        public static ResourceManager ResourceManager => resourceMan;

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
        public static string EdadFueraDeRango => ResourceManager.GetString("EdadFueraDeRango");
        public static string CorreoExistente => ResourceManager.GetString("CorreoExistente");
        public static string ErrorAlGuardar => ResourceManager.GetString("ErrorAlGuardar");
        public static string ErrorInesperado => ResourceManager.GetString("ErrorInesperado");
        public static string ErrorLeerTabla => ResourceManager.GetString("ErrorLeerTabla");
        public static string DniExistente => ResourceManager.GetString("DniExistente");
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
    }
}