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
    public partial class frmModificarArticulo : Form
    {
        private MarcaNegocio marcaNegocio;
        private CategoriaNegocio categoriaNegocio;
        private ArticuloNegocio articuloNegocio;
        private Articulo articulo;
        public frmModificarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.marcaNegocio = new MarcaNegocio();
            this.categoriaNegocio = new CategoriaNegocio();
            this.articuloNegocio = new ArticuloNegocio();
            this.articulo = articulo;
        }
        private void frmModificarArticulo_Load(object sender, EventArgs e)
        {
            try
            {
                this.SetearCampos();
                this.SetearControles();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void SetearControles()
        {
            cmbMarca.DataSource = this.marcaNegocio.Listar();
            cmbMarca.ValueMember = "Id";
            cmbMarca.DisplayMember = "Descripcion";
            cmbCategoria.DataSource = this.categoriaNegocio.Listar();
            cmbCategoria.ValueMember = "Id";
            cmbCategoria.DisplayMember = "Descripcion";
            cmbMarca.SelectedValue = articulo.Marca.Id;
            cmbCategoria.SelectedValue = articulo.Categoria.Id;

        }
        private void SetearCampos()
        {
            this.txtCodigo.Text = articulo.Codigo;
            this.txtDescripcion.Text = articulo.Descripcion;
            this.txtImagen.Text = articulo.UrlImagen;
            this.txtPrecio.Text = articulo.Precio.ToString();
            this.txtNombre.Text = articulo.Nombre;
            this.CargarImagen(articulo.UrlImagen);
        }
        private void CargarImagen(string imagen)
        {
            try
            {
                this.pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                this.pbxArticulo.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Seguro que quieres salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
                this.DialogResult = DialogResult.OK;
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
                        articulo.Nombre = txtNombre.Text;
                        articulo.Descripcion = txtDescripcion.Text;
                        articulo.Codigo = txtCodigo.Text;
                        articulo.UrlImagen = txtImagen.Text;
                        articulo.Marca = (Marca)cmbMarca.SelectedItem;
                        articulo.Categoria = (Categoria)cmbCategoria.SelectedItem;
                        articulo.Precio = decimal.Parse(txtPrecio.Text);
                        string mensaje = articuloNegocio.ModificarArticulo(articulo);
                        MessageBox.Show(mensaje, "Modificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
