using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Excepciones
{
    public class AccesoDatosException : Exception
    {
        public AccesoDatosException(string mensaje, Exception innerException) : base(mensaje, innerException)
        {

        }
        public AccesoDatosException(string mensaje) : this(mensaje, null)
        {

        }
        public AccesoDatosException()
        {

        }
    }
}
