using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Pokedex
{
    public partial class frmAltaPokemon : Form
    {
        public frmAltaPokemon()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            // o también puede ser solo Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Pokemon poke = new Pokemon();
            PokemonNegocio negocio = new PokemonNegocio();

            try
            {
                //captura datos ingresados y los transforma en obj tipo Pokemon
                poke.Numero = int.Parse(txtNumero.Text);
                poke.Nombre = txtNombre.Text;
                poke.Descripcion = txtDescripcion.Text;
                poke.UrlImagen = txtUrlImagen.Text; 
                poke.Tipo = (Elemento)cboTipo.SelectedItem;
                poke.Debilidad = (Elemento)cboTipo.SelectedItem;

                // Luego de cargarlo, lo mando a la BD
                negocio.agregar(poke);
                MessageBox.Show("Agregado exitosamente");
                Close(); //Cierra la ventana y me vuelve a mostrar el listado

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString()); // Me muestra el msj con el error
            }
        }

        private void frmAltaPokemon_Load(object sender, EventArgs e)
        {
            ElementoNegocio elementoNegocio = new ElementoNegocio();
            try
            {

                cboTipo.DataSource = elementoNegocio.listar();
                cboDebilidad.DataSource = elementoNegocio.listar(); 

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void cargarImagen (string imagen)
        {
            try
            {
                pbxUrlImagen.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxUrlImagen.Load("https://www.blackwallst.directory/images/NoImageAvailable.png");
            }
        }
    }
}
