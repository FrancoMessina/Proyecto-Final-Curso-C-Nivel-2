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
using Negocio.ValidacionesArticulo;
namespace Vista
{
    public partial class frmMenu : Form
    {
        private ArticuloNegocio negocio;
        private List<Articulo> listaArticulos;
        public frmMenu()
        {
            InitializeComponent();
            this.negocio = new ArticuloNegocio();
            this.listaArticulos = new List<Articulo>();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            try
            {
                this.ActualizarLista();
                this.CargarCampoComboBox();
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
                this.dgvArticulos.DataSource = null;
                this.listaArticulos = this.negocio.Listar();
                this.dgvArticulos.DataSource = listaArticulos;
                this.CargarImagen(listaArticulos[0].UrlImagen);
                this.OcultarColumnas();
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
            catch (ArticuloNoExistenteException ex)
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarArticuloSeleccionado())
                {
                    if (MessageBox.Show("Quieres Eliminar?", "Eliminar Articulo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Articulo articulo = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                        string mensaje = negocio.EliminarArticulo(articulo.Id);
                        MessageBox.Show(mensaje, "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ActualizarLista();
                    }
                }
            }
            catch (ArticuloNoExistenteException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltroNombre.Text;
            if (filtro != string.Empty)
                listaFiltrada = listaArticulos.FindAll((x) => x.Nombre.ToUpper().Contains(filtro.ToUpper()));
            else
                listaFiltrada = listaArticulos;

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            this.OcultarColumnas();
        }
        private void OcultarColumnas()
        {
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
        }

        private void cmbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string campo = cmbCampo.SelectedItem.ToString();
            this.CargarCriterio(campo);
        }

        private void CargarCampoComboBox()
        {
            cmbCampo.Items.Add("Marca");
            cmbCampo.Items.Add("Precio");
            cmbCampo.Items.Add("Categoría");
        }
        private void CargarCriterio(string campo)
        {
            if (campo == "Precio")
            {
                cmbCriterio.Items.Clear();
                cmbCriterio.Items.Add("Menor a");
                cmbCriterio.Items.Add("Mayor a");
                cmbCriterio.Items.Add("Igual a");
            }
            else
            {
                cmbCriterio.Items.Clear();
                cmbCriterio.Items.Add("Comienza con");
                cmbCriterio.Items.Add("Termina con");
                cmbCriterio.Items.Add("Contiene");
            }
        }

        private void btnFiltroAvanzado_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.ValidarCampo() && this.ValidarCriterio())
                {
 
                    string criterio = cmbCriterio.SelectedItem.ToString();
                    string campo = cmbCampo.SelectedItem.ToString();
                    string filtro = txtFiltroAvanzado.Text;
                    if(criterio == "Precio")
                    {
                        if (ValidarArticulo.ValidarPrecio(filtro))
                        {
                            this.ListaFiltrada(criterio,campo,filtro);//Verifico que no ingreso ningúna letra
                        }
                    }
                    else
                    {
                        if (ValidarArticulo.ValidarTexto(filtro))
                        {
                            this.ListaFiltrada(criterio, campo, filtro);
                        }
                    }
                    
                }
            }
            catch (AccesoDatosException ex)
            {
                throw ex;
            }
            catch(CampoInvalidoException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (PrecioInvalidoException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool ValidarCriterio()
        {
            if (this.cmbCriterio.SelectedIndex < 0)
            {
                throw new CampoInvalidoException("Por favor, seleccione el critero a filtrar");
            }
            return true;
        }
        private bool ValidarCampo()
        {
            if (this.cmbCampo.SelectedIndex < 0)
            {
                throw new CampoInvalidoException("Por favor, seleccione el campo a filtrar");
            }
            return true;
        }
        private void ListaFiltrada(string criterio, string campo, string filtro)
        {
            this.dgvArticulos.DataSource = null;
            this.dgvArticulos.DataSource = this.negocio.Filtrar(criterio, campo, filtro);
        }
    }
}
