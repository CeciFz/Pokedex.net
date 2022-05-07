// pokemon.com/el/pokedex

use POKEDEX_DB
go
SELECT * FROM POKEMONS
go

SELECT * FROM ELEMENTOS
go

SELECT Numero, Nombre, Descripcion FROM POKEMONS	
go


Update POKEMONS Set Numero = 1, Nombre = '', Descripcion = '', UrlImagen = '', IdTipo = 1,  IdDebilidad = 1 where Id = 1

delete POKEMONS where Id = 1