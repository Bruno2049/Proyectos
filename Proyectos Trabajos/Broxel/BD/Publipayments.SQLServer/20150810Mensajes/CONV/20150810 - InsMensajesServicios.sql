INSERT INTO [dbo].[MensajesServicios]
           ([idAplicacion]
           ,[Titulo]
           ,[Mensaje]
           ,[Clave]
           ,[Descripcion]
           ,[EsHtml]
           ,[Tipo])
     VALUES
           (0
           ,'Nuevo usuario creado'
           ,'Estimado Usuario:<br> Su cuenta se ha creado correctamente. Su nombre de usuario es {0} y su contrase�a {1}<br> Autent�cate en {2} para obtener accesso a la plataforma.'
           ,'NuevoUsr'
           ,'Nuevo Usuario'
           ,1
           ,1)
GO

INSERT INTO [dbo].[MensajesServicios]
           ([idAplicacion]
           ,[Titulo]
           ,[Mensaje]
           ,[Clave]
           ,[Descripcion]
           ,[EsHtml]
           ,[Tipo])
     VALUES
           (0
           ,'Actualizaci�n de datos exitosa'
           ,'Estimado Usuario:<br> Sus datos se han cambiado exitosamente. <br> Nombre: {0} <br> Email: {1}'
           ,'ActUsrDatos'
           ,'Actualizacion de datos de usuario'
           ,1
           ,1)

GO

INSERT INTO [dbo].[MensajesServicios]
           ([idAplicacion]
           ,[Titulo]
           ,[Mensaje]
           ,[Clave]
           ,[Descripcion]
           ,[EsHtml]
           ,[Tipo])
     VALUES
           (0
           ,'Contrase�a actualizada'
           ,'Estimado Usuario:<br> Su contrase�a se ha cambiado exitosamente. <br> Su nueva contrase�a es {0}'
           ,'ActUsrContrasenia'
           ,'Actualizacion de contrase�a'
           ,1
           ,1)

GO

INSERT INTO [dbo].[MensajesServicios]
           ([idAplicacion]
           ,[Titulo]
           ,[Mensaje]
           ,[Clave]
           ,[Descripcion]
           ,[EsHtml]
           ,[Tipo])
     VALUES
           (0
           ,'Usuario bloqueado'
           ,'Estimado Usuario.<br> Ha sobrepasado el n�mero de intentos permitidos (ip:{0}) , su cuenta permanecer� bloqueada hasta: {1},<br> si desea desbloquear en este momento, navegue al siguiente v�nculo para crear una nueva contrase�a:<br> {2}/Home/RecuperarContrasena?key={3}<br> Si usted no reconoce esta acci�n, favor de reportarlo al area de soporte tecnico.'
           ,'BloqueoUsr'
           ,'Bloqueo de usuario'
           ,1
           ,1)

GO

INSERT INTO [dbo].[MensajesServicios]
           ([idAplicacion]
           ,[Titulo]
           ,[Mensaje]
           ,[Clave]
           ,[Descripcion]
           ,[EsHtml]
           ,[Tipo])
     VALUES
           (0
           ,'Recuperar contrase�a'
           ,'Estimado Usuario:<br> Para recuperar su cuenta, navegue al siguiente v�nculo para crear una nueva contrase�a:<br> {0}/Home/RecuperarContrasena?key={1}<br> Si usted no ha solicitado ayuda con su contrase�a, ignore este correo.'
           ,'RecUsrContrasenia'
           ,'Recuperar contrase�a de usuario'
           ,1
           ,1)

GO

