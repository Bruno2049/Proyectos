declare @FechaI date = '2014-04-30';
declare @FechaF date = '2014-06-30';
declare @Estatus tinyint = '0';
declare @Region varchar(max)= '0';
declare @Zona varchar(max) = '0';
declare @CD varchar(max) =   'GRUPO ECOLÛGICO MAC'; --'TODOS'
declare @Tec varchar(max) = '0';
declare @Reg varchar(max) = 'TODAS';
declare @Prog varchar(max) = '0';
declare @Tipo varchar(Max)= '';
declare @FI date = '01-05-2014';
declare @FF date = '01-07-2014';
declare @Estado Varchar (max) = '0';
declare @Tarifa nvarchar(2) = '02';
declare @Todos tinyint = 1;


SELECT 
cr.No_Credito, cd.Dx_Nombre_Comercial, sust.Dt_Fecha_Recepcion, cr.Cve_Estatus_Credito
--tec.Dx_Nombre_General AS 'Tecnologia', reg.Dx_Nombre_Region AS 'Region', 
--zon.Dx_Nombre_Zona AS 'Zona', cd.Id_Centro_Disp AS 'ID_CAYD', 
--cd.Dx_Razon_Social AS 'CD_Razon_Social', cd.Dx_Nombre_Comercial AS 'CD_Comercial', 
--est.Dx_Estatus_Centro_Disp AS 'Estatus', MIN(sust.Dt_Fecha_Recepcion) AS 'Fecha_Min', 
--MAX(sust.Dt_Fecha_Recepcion) AS 'Fecha_Max', COUNT(sust.Id_Credito_Sustitucion) 
--AS 'No_Eq_Rec', COUNT(sust.Dx_Imagen_Recepcion) AS 'Con_Foto_Rec', 
--COUNT(sust.Id_Credito_Sustitucion) - COUNT(sust.Dx_Imagen_Recepcion) AS 
--'Fotos_Rec_Falta', COUNT(inhp.Id_Credito_Sustitucion) AS 'No_Eq_Inh', 
--COUNT(sust.Dx_Imagen_Inhabilitacion) AS 'Con_Foto_Inh', 
--COUNT(inhp.Id_Credito_Sustitucion) - COUNT(sust.Dx_Imagen_Inhabilitacion) 
--AS 'Fotos_Inh_Falta', (COUNT(sust.Id_Credito_Sustitucion) + COUNT(inhp.Id_Credito_Sustitucion)) - (COUNT(sust.Dx_Imagen_Recepcion) + COUNT(sust.Dx_Imagen_Inhabilitacion)) AS 
--'Total_Fotos_Falta', p.ID_Prog_Proy AS 'ID_Proy', p.Dx_Nombre_Programa AS 'Programa' 
FROM dbo.K_CREDITO_SUSTITUCION AS sust WITH(NOLOCK) 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) ON sust.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN dbo.CAT_CENTRO_DISP AS cd WITH(NOLOCK) ON sust.Id_Centro_Disp = cd.Id_Centro_Disp 
INNER JOIN dbo.CAT_ESTATUS_CENTRO_DISP AS est WITH(NOLOCK) ON cd.Cve_Estatus_Centro_Disp = est.Cve_Estatus_Centro_Disp 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) ON cd.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) ON zon.Cve_Region = reg.cve_Region 
LEFT OUTER JOIN dbo.K_INHABILITACION_PRODUCTO AS inhp WITH(NOLOCK) ON sust.Id_Credito_Sustitucion = inhp.Id_Credito_Sustitucion 
LEFT OUTER JOIN dbo.K_INHABILITACION AS inh WITH(NOLOCK) ON inhp.Id_Inhabilitacion = inh.Id_Inhabilitacion 
LEFT OUTER JOIN dbo.CRE_Credito AS cr WITH(NOLOCK) ON sust.No_Credito = cr.No_Credito 
RIGHT OUTER JOIN dbo.CAT_PROGRAMA AS p WITH(NOLOCK) ON cr.ID_Prog_Proy = p.ID_Prog_Proy 
WHERE 
 (cd.Dx_Nombre_Comercial = @CD OR @CD = 'TODOS')/**/ 
AND sust.Dt_Fecha_Recepcion IS NOT NULL AND sust.Fg_Tipo_Centro_Disp = 'M'  ORDER By sust.Dt_Fecha_Recepcion