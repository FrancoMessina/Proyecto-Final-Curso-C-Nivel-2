using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        private int id;
        private string codigo;
        private string nombre;
        private string descripcion;
        private Marca marca;
        private Categoria categoria;
        private string urlImagen;
        private decimal precio;

        public int Id { get => id; set => id = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public Marca Marca { get => marca; set => marca = value; }
        public Categoria Categoria { get => categoria; set => categoria = value; }
        public string UrlImagen { get => urlImagen; set => urlImagen = value; }
        public decimal Precio { get => precio; set => precio = value; }
    }
}
