
update mensajesservicios set titulo='Cambio de contrase�a',clave='ResetUsrContrasenia',Descripcion='Reset de contrase�a' where titulo='Contrase�a actualizada'
GO
INSERT INTO [dbo].[MensajesServicios]([idAplicacion],[Titulo],[Mensaje],[Clave],[Descripcion],[EsHtml],[Tipo]) VALUES (0,'Contrase�a actualizada','Estimado Usuario:<br> Su contrase�a se ha cambiado exitosamente.','ActUsrContrasenia','Actualizacion de contrase�a',1,1)
GO




