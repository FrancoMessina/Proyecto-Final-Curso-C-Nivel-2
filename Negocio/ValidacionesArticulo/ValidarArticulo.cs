using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Excepciones;
namespace Negocio.ValidacionesArticulo
{
    public static class ValidarArticulo
    {
        public static bool ValidarCamposArticulo(string codigo, string nombre, string descripcion, string urlImagen, string precio)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion)
                || string.IsNullOrWhiteSpace(urlImagen) || string.IsNullOrWhiteSpace(precio))
            {
                throw new CamposVaciosONullException("Cargar todos los datos del articulo!");
            }
            return true;
        }
        public static bool ValidarCampo(string dato)
        {
            if (dato.Length > 40 || dato.Length < 2)
            {
                throw new CampoInvalidoException("El campo tiene que tener más de dos caracteres y menos de 40");
            }
            return true;
        }
        public static bool ValidarImagen(string url)
        {
            if (url.Length < 5)
            {
                throw new CampoInvalidoException("El campo tiene que tener más de 5 caracteres");
            }
            return true;
        }
        public static bool ValidarPrecio(string precio)
        {
            if (!decimal.TryParse(precio, out decimal auxPrecio))
            {
                throw new PrecioInvalidoException("El Precio tiene que ser númerico!");
            }
            else if (auxPrecio <  0)
            {
                throw new PrecioInvalidoException("El Precio no puede ser negativo");
            }
            return true;
        }
        public static bool ValidarTexto(string dato)
        {
            if (!VerificarSoloLetras(dato))
            {
                throw new CampoInvalidoException("Ingresar solo letras");
            }
            return true;
        }
        private static bool VerificarSoloLetras(string dato)
        {
            foreach (char letra in dato)
            {
                if (!char.IsLetter(letra))
                    return false;
            }
            return true;
        }

    }
}
