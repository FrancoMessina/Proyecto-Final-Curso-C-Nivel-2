using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;
using Negocio.Excepciones;
namespace Vista
{
    public partial class frmMenu : Form
    {
        private ArticuloNegocio negocio;
        public frmMenu()
        {
            InitializeComponent();
            this.negocio = new ArticuloNegocio();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            try
            {
                this.ActualizarLista();
            }
            catch (AccesoDatosException ex)
            {

                throw ex;
            }
        }
        private void ActualizarLista()
        {
            try
            {
                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = this.negocio.Listar();
                dgvArticulos.Columns["Id"].Visible = false;
                dgvArticulos.Columns["UrlImagen"].Visible = false;
            }
            catch (AccesoDatosException ex)
            {
                throw ex;
            }
        }
        public void CargarImagen(string imagen)
        {
            try
            {
                pbArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbArticulo.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo aux = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                CargarImagen(aux.UrlImagen);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                frmNuevoArticulo nuevoForm = new frmNuevoArticulo();
                nuevoForm.ShowDialog();
                this.ActualizarLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarArticuloSeleccionado())
                {
                    Articulo articulo = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    frmModificarArticulo ventanaModificar = new frmModificarArticulo(articulo);
                    ventanaModificar.ShowDialog();
                    this.ActualizarLista();
                }
            }
            catch(ArticuloNoExistenteException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidarArticuloSeleccionado()
        {
            if (this.dgvArticulos.CurrentRow is null)
            {
                throw new ArticuloNoExistenteException("No hay ningun Articulo seleccionado");
            }
            return true;
        }
    }
}
