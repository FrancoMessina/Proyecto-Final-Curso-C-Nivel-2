using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Excepciones
{
    public class CampoInvalidoException : Exception
    {
        public CampoInvalidoException(string mensaje) : base(mensaje)
        {

        }
    }
}
