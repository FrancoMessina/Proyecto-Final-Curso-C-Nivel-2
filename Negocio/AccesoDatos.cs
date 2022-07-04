using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public AccesoDatos()
        {
            this.conexion = new SqlConnection("Server =.; Database = CATALOGO_DB; Trusted_Connection = True;");
            this.comando = new SqlCommand();
        }

        public SqlDataReader Lector { get { return lector; } }

        public void SetearConsulta(string consulta)
        {
            this.comando.CommandType = System.Data.CommandType.Text;
            this.comando.CommandText = consulta;
        }
        public void EjecutarLectura()
        {
            try
            {
                this.comando.Connection = conexion;
                this.conexion.Open();
                this.lector = this.comando.ExecuteReader();

            }
            catch (Exception)
            {

                throw new AccesoDatosException();
            }
        }
        public void CerrarConexion()
        {
            if (this.lector != null)
                this.lector.Close();
            this.conexion.Close();
        }
        public void EjecutarAccion()
        {
            try
            {
                this.comando.Connection = conexion;
                this.conexion.Open();
                this.comando.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw new AccesoDatosException("Error");
            }
        }
        public void SetearParametros(string nombre, object valor)
        {
            this.comando.Parameters.AddWithValue(nombre, valor);
        }
    }
}
