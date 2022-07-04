﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Categoria
    {
        private int id;
        private string descripcion;

        public int Id { get { return id; } set { id = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
