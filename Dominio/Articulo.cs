using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public Articulo()
        {

        }
        public Articulo(string codigo, string nombre, string descripcion, string urlImagen)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.urlImagen = urlImagen;
        }
        public int Id { get => id; set => id = value; }
        [DisplayName("Código")]
        public string Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        [DisplayName("Descripción")]
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public Marca Marca { get => marca; set => marca = value; }
        [DisplayName("Categoría")]
        public Categoria Categoria { get => categoria; set => categoria = value; }
        public string UrlImagen { get => urlImagen; set => urlImagen = value; }
        public decimal Precio { get => precio; set => precio = value; }
    }
}
