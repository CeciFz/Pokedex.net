using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //librería para poder crear los objetos y establecer la conexión con la BD
using Dominio;

namespace Negocio
{
    public class PokemonNegocio
    {
        public List<Pokemon> listar()
        {
            List<Pokemon> lista= new List<Pokemon>();


            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;  

            try
            {
                conexion.ConnectionString = "server =.\\SQLEXPRESS; database = POKEDEX_DB; integrated security = true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT P.Id, Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D WHERE E.Id = P.IdTipo AND D.Id = P.IdDebilidad";
                comando.Connection = conexion;  
                conexion.Open();
                lector = comando.ExecuteReader(); 

                while (lector.Read())  
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = lector.GetInt32(0);
                    aux.Numero = lector.GetInt32(1);
                    aux.Nombre = (String) lector["Nombre"];
                    aux.Descripcion = (String) lector["Descripcion"];

                    /* Validación de columna NULL:

                     if (!(lector.IsDBNull(lector.GetOrdinal("UrlImagen"))))
                         aux.UrlImagen = (String)lector["UrlImagen"];

                     Otra opción de validación más corta: */

                    if (!(lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (String)lector["UrlImagen"];


                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)lector["IdTipo"];
                    aux.Tipo.Descripcion = (String)lector["Tipo"]; 
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (String)lector["Debilidad"];

                    lista.Add(aux);  
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void agregar(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into POKEMONS (Numero, Nombre, Descripcion,Activo,IdTipo,IdDebilidad,UrlImagen) values (" + nuevo.Numero + ",'" + nuevo.Nombre + "','" + nuevo.Descripcion + "',1,@idTipo, @idDebilidad, @UrlImagen)");
                datos.SetearParametro("@UrlImagen", nuevo.UrlImagen);
                datos.SetearParametro("@idTipo", nuevo.Tipo.Id);
                datos.SetearParametro("@idDebilidad", nuevo.Debilidad.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        
        public void modificar(Pokemon pokemon)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update POKEMONS Set Numero = @Numero, Nombre = @Nombre, Descripcion = @Descripcion, UrlImagen = @UrlImagen, IdTipo = @IdTipo,  IdDebilidad = @IdDebilidad where Id = @Id");
                datos.SetearParametro("@Numero", pokemon.Numero);
                datos.SetearParametro("@Nombre", pokemon.Nombre);
                datos.SetearParametro("@Descripcion", pokemon.Descripcion);
                datos.SetearParametro("@UrlImagen", pokemon.UrlImagen);
                datos.SetearParametro("@IdTipo", pokemon.Tipo.Id);
                datos.SetearParametro("@IdDebilidad", pokemon.Debilidad.Id);
                datos.SetearParametro("@Id", pokemon.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
              datos.cerrarConexion();
            }
        }



    }
}
