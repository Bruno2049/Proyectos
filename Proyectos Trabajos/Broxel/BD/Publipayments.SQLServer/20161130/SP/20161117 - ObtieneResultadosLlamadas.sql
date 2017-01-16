/****************************************************************************
* Proyecto:				portal.publipayments.com
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creaci�n:	03/11/2016
* Descripci�n:			Obtiene el catalogo de resultados de Llamadas sin exito
*****************************************************************************/
CREATE PROCEDURE [dbo].[ObtieneResultadosLlamadas]
AS
BEGIN
	BEGIN TRY
	
		SELECT 
			[ID_RESULTADO],
			[RESULTADO]
		FROM CatResultadoLlamadasSinExito WITH(NOLOCK)
			
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS Codigo, ERROR_MESSAGE() AS Descripcion;
	
	END CATCH
END