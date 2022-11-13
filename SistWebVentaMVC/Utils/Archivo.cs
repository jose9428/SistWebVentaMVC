namespace SistWebVentaMVC.Utils
{
    public static class Archivo
    {
        public const string RUTA_PRODUCTO = "img/productos/";
        public const string RUTA_SIN_IMAGEN = "img/recursos/img_not_found.jpg";
        public const string RUTA_RAIZ = "wwwroot/";

        public static string GenerarNomImagen(int numeracion, string tipo)
        {
            return "PROD" + String.Format("{0:00000}", numeracion) + "." + tipo;
        }

        public static string GuardarArchivo(IFormFile archivo, int numeracion)
        {
            string ruta = RUTA_RAIZ + RUTA_PRODUCTO;
            string nombreImagen = "" , directorio = "";


            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            //  nombreImagen = GetUniqueFileName(archivo.FileName);
            nombreImagen = GetFileName(archivo.FileName, numeracion);
            //  nombreImagen = GenerarNomImagen(numeracion,"png");
          //  nombreImagen = new Random().Next(1, 100000) + Path.GetFileName(archivo.FileName);
            directorio = Path.Combine(ruta, nombreImagen);

            using (var stream = new FileStream(directorio, FileMode.Create))
            {
                archivo.CopyTo(stream);
            }

            return nombreImagen;
        }

        private static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        private static string GetFileName(string fileName, int numeracion)
        {
            fileName = Path.GetFileName(fileName);
            return "PROD" + String.Format("{0:00000}", numeracion)
                    //  + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        public static void EliminarArchivo(string nombreImagen)
        {
            string ruta = Path.Combine(RUTA_RAIZ + RUTA_PRODUCTO, nombreImagen);

            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }
        }

        public static bool ExisteArchivo(string nombreImagen)
        {
            string ruta = Path.Combine(RUTA_RAIZ + RUTA_PRODUCTO, nombreImagen);

            return File.Exists(ruta);
        }
    }
}
