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

        //Property:
        public SqlDataReader Lector // así tengo la posibilidad de leer el lector dese el exterior 
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("server =.\\SQLEXPRESS; database = POKEDEX_DB; integrated security = true");
            comando = new SqlCommand();        
        }

        public void setearConsulta(String consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;  //La consulta va a ser la query, el select
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void cerrarConexion()
        {
            if (lector != null) 
                lector.Close();
            conexion.Close();
        }
    }
}
