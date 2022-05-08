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


SELECT P.Id, Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D WHERE E.Id = P.IdTipo AND D.Id = P.IdDebilidad