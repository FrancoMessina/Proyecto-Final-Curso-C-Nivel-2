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
using Negocio.Excepciones;
using Negocio.ValidacionesArticulo;
using Dominio;
namespace Vista
{
    public partial class frmNuevoArticulo : Form
    {
        private MarcaNegocio marcaNegocio;
        private CategoriaNegocio categoriaNegocio;
        private ArticuloNegocio articuloNegocio;
        public frmNuevoArticulo()
        {
            InitializeComponent();
            this.marcaNegocio = new MarcaNegocio();
            this.categoriaNegocio = new CategoriaNegocio();
            this.articuloNegocio = new ArticuloNegocio();
        }

        private void frmNuevoArticulo_Load(object sender, EventArgs e)
        {
            try
            {
                this.cmbMarca.DataSource = this.marcaNegocio.Listar();
                this.cmbCategoria.DataSource = this.categoriaNegocio.Listar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void CargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }

        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            try
            {
                this.CargarImagen(txtImagen.Text);
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarArticulo.ValidarCamposArticulo(txtCodigo.Text, txtNombre.Text, txtDescripcion.Text, txtImagen.Text, txtPrecio.Text))
                {

                    if (ValidarArticulo.ValidarCampo(txtNombre.Text) && ValidarArticulo.ValidarCampo(txtCodigo.Text)
                        && ValidarArticulo.ValidarCampo(txtDescripcion.Text) && ValidarArticulo.ValidarImagen(txtImagen.Text)
                        && ValidarArticulo.ValidarPrecio(txtPrecio.Text)
                        )
                    {
                        Articulo articulo = new Articulo(txtCodigo.Text,txtNombre.Text,txtDescripcion.Text,txtImagen.Text);
                        articulo.Marca = (Marca)cmbMarca.SelectedItem;
                        articulo.Categoria = (Categoria)cmbCategoria.SelectedItem;
                        articulo.Precio = decimal.Parse(txtPrecio.Text);
                        string mensaje = articuloNegocio.AgregarArticulo(articulo);
                        MessageBox.Show(mensaje, "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (CamposVaciosONullException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (CampoInvalidoException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (PrecioInvalidoException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Error.Ingrese los datos nuevamente!");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Seguro que quieres salir?","Salir",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dialogResult == DialogResult.Yes)
                this.DialogResult = DialogResult.OK;
        }
    }
}