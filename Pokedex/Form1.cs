using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokedex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Al final de todo, invoco esa lectura a la BD que se realixó
            PokemonNegocio negocio = new PokemonNegocio();
            dgvPokemons.DataSource = negocio.listar();

            //negocio.listar() va a la BD y te devuelve una BD
            //Datasource = recibe un origen de datos y lo modela en la tabla dgvPokemons
        }
    }
}
