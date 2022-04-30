// pokemon.com/el/pokedex

use POKEDEX_DB
go
SELECT * FROM POKEMONS
go

SELECT * FROM ELEMENTOS
go

SELECT Numero, Nombre, Descripcion, UrlImagen FROM POKEMONS	
go

SELECT Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad
FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D
WHERE E.Id = P.IdTipo
AND D.Id = P.IdDebilidad
go

SELECT Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D WHERE E.Id = P.IdTipo AND D.Id = P.IdDebilidad
