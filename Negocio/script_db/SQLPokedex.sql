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


/* Para agregar datos:
	1- Le aclaro la/s columna/s  --> Insert into POKEMONS (UrlImagen, IdEvolucion) values ( '' , 1)
	2- No le aclaro column/s, por lo que debo cargar todas en el orden en que están --> Insert into POKEMONS) values ( 1, '' , '' , '' , 1, 1, 1, 1)
*/


Insert into POKEMONS (Numero, Nombre, Descripcion,Activo) values (1,'','',1)

update POKEMONS set IdTipo = 1 , IdDebilidad = 2 , UrlImagen = '' where Numero = 27

update POKEMONS set UrlImagen = null where Numero = 9