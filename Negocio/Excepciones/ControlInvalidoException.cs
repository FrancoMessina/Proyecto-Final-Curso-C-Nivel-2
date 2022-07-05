using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Excepciones
{
    public class ControlInvalidoException : Exception
    {
        public ControlInvalidoException(string mensaje) : base(mensaje)
        {

        }
    }
}
