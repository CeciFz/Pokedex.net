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
        private Pokemon pokemon = null;
        public frmAltaPokemon()  // Constructor que arranca en nulo
        {
            InitializeComponent();
        }

        public frmAltaPokemon(Pokemon pokemon) // Constructor que arranca con algún pokemon
        {
            InitializeComponent();
            this.pokemon = pokemon;
            Text = "Modificar Pokemon";  //Cambia nombre ventana
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            // o también puede ser solo Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();

            try
            {
                if (pokemon == null)   // Si apretaste aceptar y la variable pokemon estaba en nulo, es porque estabas agregando, no modificando. 
                    pokemon = new Pokemon(); //Por eso instancio un pokemon, para poder asignarle los valores de cero.
                                             
                //captura datos ingresados y los transforma en obj tipo Pokemon
                pokemon.Numero = int.Parse(txtNumero.Text);
                pokemon.Nombre = txtNombre.Text;
                pokemon.Descripcion = txtDescripcion.Text;
                pokemon.UrlImagen = txtUrlImagen.Text; 
                pokemon.Tipo = (Elemento)cboTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)cboTipo.SelectedItem;

                // Luego de cargarlo, lo mando a la BD. Si estás modificando, ya tiene un ID (Así diferencia cuál método ejecutar)
                if (pokemon.Id != 0)
                {
                    negocio.modificar(pokemon);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(pokemon);
                    MessageBox.Show("Agregado exitosamente");
                }
                
                


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString()); // Me muestra el msj con el error
            }
            finally
            {
                Close(); //Cierra la ventana y me vuelve a mostrar el listado
            }
        }

        private void frmAltaPokemon_Load(object sender, EventArgs e)
        {
            ElementoNegocio elementoNegocio = new ElementoNegocio();
            try
            {

                cboTipo.DataSource = elementoNegocio.listar();
                cboTipo.ValueMember = "Id"; //la clave
                cboTipo.DisplayMember = "Descripcion"; //El valor que voy a mostrar
                cboDebilidad.DataSource = elementoNegocio.listar();
                cboDebilidad.ValueMember = "Id";
                cboDebilidad.DisplayMember = "Descripcion"; 

                if (pokemon != null)  // si no es nulo, es xq estoy queriendo modificar
                {
                    txtNumero.Text = pokemon.Numero.ToString();  //.ToString xq es un número
                    txtNombre.Text = pokemon.Nombre;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrlImagen.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen);
                    cboTipo.SelectedValue = pokemon.Tipo.Id;
                    cboDebilidad.SelectedValue = pokemon.Debilidad.Id;

                }

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
