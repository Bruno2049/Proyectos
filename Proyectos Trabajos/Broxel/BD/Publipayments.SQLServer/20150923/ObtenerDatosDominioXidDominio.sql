/*********************************************************************
* Proyecto:				London-PubliPayments
* Autor:				Pablo Rendon
* Fecha de creaci�n:	09/09/2015
* Descripci�n:			Obtener datos dominio por idDominio
**********************************************************************/
CREATE PROCEDURE ObtenerDatosDominioXidDominio
(
	@idDominio int
)
AS BEGIN
	SELECT nombreDominio,nom_corto,estatus 
	FROM dominio
	WHERE idDominio = @idDominio
end