using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //librería para poder crear los objetos y establecer la conexión con la BD

namespace Pokedex
{
    internal class PokemonNegocio
    {
        //Esta clase devuelve Pokemonsss (varios), por eso devuelve una lista!
        public List<Pokemon> listar()
        {
            //Tiene que devolver una lista, entonces la creo:
            List<Pokemon> lista= new List<Pokemon>();

            //Luego creo el acceso a la BD con la que cargará esa lista
            //(Lo meto dentro de una excepción: si está todo bien, retorna una lista
            //si no está todo bien, retorna un error)
            // Y agrego la librería  System.Data.SqlClient

            //OBJETOS Y VARIABLE A CREAR:
            //Para poder conectarme a la bd: un obj de tipo SqlConection
            SqlConnection conexion = new SqlConnection();
            //Una vez conectado, preciso realizar acciones. Para ello, declaro el obj comando
            SqlCommand comando = new SqlCommand();
            //Al final, como rdo de la conexión, voy a obtener un set de datos.
            //Eso lo albergo en un lector: SqlDataReader
            SqlDataReader lector;  //No se le crea instancia, xq cdo realice la lectura, obtengo
                                   //como rdo una instancia de un obj de tipo SqlData Reader.
                                   //De hecho, si trato de crearla, me da error.
            
            //Luego se procede a configurarlo, ya dentro del try:

            try
            {
                // Primero configuro a dónde me voy a conectar:
                conexion.ConnectionString = "server =.\\SQLEXPRESS; database = POKEDEX_DB; integrated security = true";
                //Es una cadena que donde le agrego 1) a dónde me voy a conectar (server) ,
                //2) a qué BD (database) y 3) cómo me voy a conectar
                //3) De qué forma me voy a conectar

                //Luego configuro el comando, que sirve para realizar la acción, que es una lectura.
                //La lectura la hago, enviando la sentencia sql que quiero ejecutar:
                comando.CommandType = System.Data.CommandType.Text;
                //Pedí la de tipo texto, entonces ahora le digo qué texto quiero mandar, que va a ser la consulta SQL:
                comando.CommandText = "SELECT Numero, Nombre, Descripcion FROM POKEMONS";
                // Luego voy al comando Conexion, y le digo que ejecute ese comando en esa conexion:
                comando.Connection = conexion;  
                //Luego tengo que abrir la conexion:
                conexion.Open();
                //Realizo la lectura:
                lector = comando.ExecuteReader(); //Que da como resultado un SqlDataReader, por eso lo asigno a lector

                /* Hasta acá, si todo funcionó bien, tengo los datos en mi variable lector,
                   tengo mi objeto DatReader con los datos.
                   Sería como una "tabla virtual" con un puntero que puedo ir posicionado en memoria,
                   y que voy a transformar en una colección de objetos (la lista declarada al principio
                   todo). 
                   Para transformarla voy a tener que ir leyendo a ese lector, para eso uso un while con
                   el lector.Read(), el cual, si pudo leer (es decir, si hay un registro a continuación),
                   devuelve true y, también, va a posicionar el puntero en la siguiente posición. 
                */

                while (lector.Read())  
                {
                    Pokemon aux = new Pokemon();
                    aux.Numero = lector.GetInt32(0);
                    aux.Nombre = (String) lector["Nombre"];
                    aux.Descripcion = (String) lector["Descripcion"];

                    lista.Add(aux);  //De esta forma, x c/pokemon, voy a ir agregando los datos a la lista
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
