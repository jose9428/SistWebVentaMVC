namespace SistWebVentaMVC.Utils
{
    public static class Validacion
    {
        public const string CORREO = "[_A-Za-z0-9-]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})";
        public const string LETRAS = "^[a-zA-ZÑÁÉÍÓÚñ]+$";
        public const string ENTERO = "^[0-9]*$";
        public const string DECIMAL = @"^\$?\d+(\.(\d{2}))?$";


    }
}
