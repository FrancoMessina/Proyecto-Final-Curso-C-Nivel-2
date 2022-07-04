using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Excepciones
{
    public class CamposVaciosONullException : Exception
    {
        public CamposVaciosONullException(string mensaje) : base(mensaje)
        {

        }
    }
}
