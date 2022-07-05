using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id , A.Codigo , A.Nombre , A.Descripcion , IdMarca , M.Descripcion Marca, IdCategoria ,C.Descripcion Categoria, A.ImagenUrl , A.Precio Precio  From ARTICULOS A , MARCAS M , CATEGORIAS C where A.IdMarca = M.Id and A.IdCategoria = C.Id";
                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Descripcion = (string)datos.Lector["Descripcion"];
                    articulo.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];
                    articulo.Precio = (decimal)datos.Lector["Precio"];
                    //Objeto Marca
                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                    articulo.Marca.Descripcion = (string)datos.Lector["Marca"];
                    //Objeto Categoria
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(articulo);
                }
                return lista;
            }
            catch (Exception)
            {
                throw new AccesoDatosException();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public string AgregarArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Insert into Articulos(Codigo,Nombre,Descripcion,ImagenUrl,Precio,IdMarca,IdCategoria)" +
                    "values(@codigo,@nombre,@descripcion,@imagenUrl,@Precio,@IdMarca,@IdCategoria)";
                datos.SetearConsulta(consulta);
                datos.SetearParametros("codigo", articulo.Codigo);
                datos.SetearParametros("nombre", articulo.Nombre);
                datos.SetearParametros("descripcion", articulo.Descripcion);
                datos.SetearParametros("imagenUrl", articulo.UrlImagen);
                datos.SetearParametros("Precio", articulo.Precio);
                datos.SetearParametros("IdMarca", articulo.Marca.Id);
                datos.SetearParametros("IdCategoria", articulo.Categoria.Id);
                datos.EjecutarAccion();
                return "Articulo agregado correctamente!!";
            }
            catch (Exception)
            {

                throw new AccesoDatosException();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public string ModificarArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Update ARTICULOS set Codigo = @codigo ,Nombre = @nombre, Descripcion = @descripcion,ImagenUrl = @imagenUrl,IdMarca = @IdMarca,IdCategoria = @IdCategoria, Precio = @Precio where Id = @Id";
                datos.SetearConsulta(consulta);
                datos.SetearParametros("Id", articulo.Id);
                datos.SetearParametros("codigo", articulo.Codigo);
                datos.SetearParametros("nombre", articulo.Nombre);
                datos.SetearParametros("descripcion", articulo.Descripcion);
                datos.SetearParametros("imagenUrl", articulo.UrlImagen);
                datos.SetearParametros("Precio", articulo.Precio);
                datos.SetearParametros("IdMarca", articulo.Marca.Id);
                datos.SetearParametros("IdCategoria", articulo.Categoria.Id);
                datos.EjecutarAccion();
                return "Articulo Modificado correctamente!!";
            }
            catch (Exception)
            {

                throw new AccesoDatosException();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
