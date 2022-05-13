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
                comando.CommandText = "SELECT P.Id, Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D WHERE E.Id = P.IdTipo AND D.Id = P.IdDebilidad AND P.Activo = 1";
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

        public void eliminar (int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos ();
                datos.setearConsulta("Delete from POKEMONS where ID = @Id");
                datos.SetearParametro("@Id",id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void eliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update POKEMONS set Activo = 0 where id = @Id");
                datos.SetearParametro("@Id", id);
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

        public List<Pokemon> filtrar(string campo, string criterio, string filtro)
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                string consulta = "SELECT P.Id, Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D WHERE E.Id = P.IdTipo AND D.Id = P.IdDebilidad AND P.Activo = 1 AND ";

                switch (campo)
                {
                    case "Número":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "Numero > " + filtro;
                                break;
                            case "Menor a":
                                consulta += "Numero < " + filtro;
                                break;
                            default:
                                consulta += "Numero = " + filtro;
                                break;
                        }
                        break;
                    case "Nombre":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "Nombre like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "Nombre like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "Nombre like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Descripción":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "P.Descripcion like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "P.Descripcion like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "P.Descripcion like '%" + filtro + "%'";
                                break;
                        }
                        break;
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = datos.Lector.GetInt32(0);
                    aux.Numero = datos.Lector.GetInt32(1);
                    aux.Nombre = (String)datos.Lector["Nombre"];
                    aux.Descripcion = (String)datos.Lector["Descripcion"];

                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (String)datos.Lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (String)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (String)datos.Lector["Debilidad"];

                    lista.Add(aux);
                }

                return lista;
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


