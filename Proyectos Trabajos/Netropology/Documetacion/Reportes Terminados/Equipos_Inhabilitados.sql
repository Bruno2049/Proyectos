declare @FechaI date = '01-05-2009';
declare @FechaF date = '01-05-2014';
declare @Estatus tinyint = '0';
declare @Region varchar(max)= '0';
declare @Zona varchar(max) = '0';
declare @CD varchar(max) = 'TODOS';
declare @Tec varchar(max) = '0';
declare @Reg varchar(max) = 'TODAS';
declare @Prog varchar(max) = '0';
declare @Tipo varchar(Max)= '';
declare @FI date = '01-01-2013';
declare @FF date = '01-01-2014';
declare @Estado Varchar (max) = '0';
declare @Tarifa nvarchar(2) = '02';
declare @Todos tinyint = 1;

SELECT cd.Id_Centro_Disp AS 'ID CD', edo.Dx_Nombre_Estado 
AS 'Estado Fiscal', reg.Dx_Nombre_Region AS 'Region', 
zon.Dx_Nombre_Zona AS 'Zona', cd.Dx_Razon_Social AS 
'Razon Social', cd.Dx_Nombre_Comercial AS 'Comercial', 
COUNT(sust.Id_Credito_Sustitucion) AS 'Cantidad', 
'Refrigeración Comercial' AS 'Tec', 
inh2.Dt_Fecha_Inhabilitacion AS 'Fecha Inhabilitacion' 
FROM dbo.K_INHABILITACION_PRODUCTO AS inh WITH(NOLOCK) 
INNER JOIN dbo.K_INHABILITACION AS inh2 WITH(NOLOCK) 
ON inh.Id_Inhabilitacion = inh2.Id_Inhabilitacion 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS sust WITH(NOLOCK) 
ON inh.Id_Credito_Sustitucion = sust.Id_Credito_Sustitucion 
INNER JOIN dbo.CAT_CENTRO_DISP AS cd WITH(NOLOCK) 
ON sust.Id_Centro_Disp = cd.Id_Centro_Disp 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON sust.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN dbo.CAT_ESTADO AS edo WITH(NOLOCK) 
ON cd.Cve_Estado_Fisc = edo.Cve_Estado 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON cd.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
LEFT OUTER JOIN dbo.K_CREDITO AS k WITH(NOLOCK) 
ON sust.No_Credito = k.No_Credito 
LEFT OUTER JOIN dbo.CAT_PROGRAMA AS pr WITH(NOLOCK) 
ON k.Id_prog_Proy = pr.Id_Prog_Proy 
WHERE sust.Fg_Tipo_Centro_Disp = 'M' AND 
sust.Cve_Tecnologia = 1 AND inh2.Dt_Fecha_Inhabilitacion 
BETWEEN @FechaI AND @FechaF 
AND (edo.Cve_Estado = @Estado OR @Estado = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zona OR @Zona = '0') 
AND (cd.Dx_Nombre_Comercial = @CD OR @CD = 'TODOS') 
AND (pr.Id_Prog_Proy = @Prog OR @Prog = '0') 
GROUP BY cd.Id_Centro_Disp, edo.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, 
cd.Dx_Razon_Social, cd.Dx_Nombre_Comercial, 
inh2.Dt_Fecha_Inhabilitacion 
UNION ALL 
SELECT cd.Id_Centro_Disp, edo.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, 
cd.Dx_Razon_Social, cd.Dx_Nombre_Comercial, 
COUNT(sust.Id_Credito_Sustitucion), 
'Aire Acondicionado' AS 'Tec', 
inh2.Dt_Fecha_Inhabilitacion AS 'Fecha Inhabilitacion' 
FROM dbo.K_INHABILITACION_PRODUCTO AS inh WITH(NOLOCK) 
INNER JOIN dbo.K_INHABILITACION AS inh2 WITH(NOLOCK) 
ON inh.Id_Inhabilitacion = inh2.Id_Inhabilitacion 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS sust WITH(NOLOCK) 
ON inh.Id_Credito_Sustitucion = sust.Id_Credito_Sustitucion 
INNER JOIN dbo.CAT_CENTRO_DISP AS cd WITH(NOLOCK) 
ON sust.Id_Centro_Disp = cd.Id_Centro_Disp 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON sust.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN dbo.CAT_ESTADO AS edo WITH(NOLOCK) 
ON cd.Cve_Estado_Fisc = edo.Cve_Estado 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON cd.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
LEFT OUTER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS k WITH(NOLOCK) 
ON sust.No_Credito = k.No_Credito 
LEFT OUTER JOIN dbo.CAT_PROGRAMA AS pr WITH(NOLOCK) 
ON k.Id_prog_Proy = pr.Id_Prog_Proy 
WHERE sust.Fg_Tipo_Centro_Disp = 'M' AND 
sust.Cve_Tecnologia = 2  AND inh2.Dt_Fecha_Inhabilitacion 
BETWEEN @FechaI AND @FechaF 
AND (edo.Cve_Estado = @Estado OR @Estado = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zona OR @Zona = '0') 
AND (cd.Dx_Nombre_Comercial = @CD OR @CD = 'TODOS') 
AND (pr.Id_Prog_Proy = @Prog OR @Prog = '0') 
GROUP BY cd.Id_Centro_Disp, edo.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, 
cd.Dx_Razon_Social, cd.Dx_Nombre_Comercial, 
inh2.Dt_Fecha_Inhabilitacion 
UNION ALL 
SELECT cd.Id_Centro_Disp, edo.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, 
cd.Dx_Razon_Social, cd.Dx_Nombre_Comercial, 
COUNT(sust.Id_Credito_Sustitucion), 
'Motores Eléctricos' AS 'Tec', 
inh2.Dt_Fecha_Inhabilitacion AS 'Fecha Inhabilitacion' 
FROM dbo.K_INHABILITACION_PRODUCTO AS inh WITH(NOLOCK) 
INNER JOIN dbo.K_INHABILITACION AS inh2 WITH(NOLOCK) 
ON inh.Id_Inhabilitacion = inh2.Id_Inhabilitacion 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS sust WITH(NOLOCK) 
ON inh.Id_Credito_Sustitucion = sust.Id_Credito_Sustitucion 
INNER JOIN dbo.CAT_CENTRO_DISP AS cd WITH(NOLOCK) 
ON sust.Id_Centro_Disp = cd.Id_Centro_Disp 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON sust.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN dbo.CAT_ESTADO AS edo WITH(NOLOCK) 
ON cd.Cve_Estado_Fisc = edo.Cve_Estado 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON cd.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
LEFT OUTER JOIN dbo.K_CREDITO AS k WITH(NOLOCK) 
ON sust.No_Credito = k.No_Credito 
LEFT OUTER JOIN dbo.CAT_PROGRAMA AS pr WITH(NOLOCK) 
ON k.Id_prog_Proy = pr.Id_Prog_Proy 
WHERE sust.Fg_Tipo_Centro_Disp = 'M' AND 
sust.Cve_Tecnologia = 4 AND inh2.Dt_Fecha_Inhabilitacion 
BETWEEN @FechaI AND @FechaF 
AND (edo.Cve_Estado = @Estado OR @Estado = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zona OR @Zona = '0') 
AND (cd.Dx_Nombre_Comercial = @CD OR @CD = 'TODOS') 
AND (pr.Id_Prog_Proy = @Prog OR @Prog = '0') 
GROUP BY cd.Id_Centro_Disp, edo.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, 
cd.Dx_Razon_Social, cd.Dx_Nombre_Comercial, 
inh2.Dt_Fecha_Inhabilitacion 
UNION ALL 
SELECT cdb.Id_Centro_Disp, edo.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, 
cdb.Dx_Razon_Social, cdb.Dx_Nombre_Comercial, 
COUNT(sust.Id_Credito_Sustitucion), 
'Refrigeración Comercial' AS 'Tec', 
inh2.Dt_Fecha_Inhabilitacion AS 'Fecha Inhabilitacion' 
FROM dbo.K_INHABILITACION_PRODUCTO AS inh WITH(NOLOCK) 
INNER JOIN dbo.K_INHABILITACION AS inh2 WITH(NOLOCK) 
ON inh.Id_Inhabilitacion = inh2.Id_Inhabilitacion 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS sust WITH(NOLOCK) 
ON inh.Id_Credito_Sustitucion = sust.Id_Credito_Sustitucion 
INNER JOIN dbo.CAT_CENTRO_DISP_SUCURSAL AS cdb WITH(NOLOCK) 
ON sust.Id_Centro_Disp = cdb.Id_Centro_Disp_Sucursal 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON sust.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN dbo.CAT_ESTADO AS edo WITH(NOLOCK) 
ON cdb.Cve_Estado_Fisc = edo.Cve_Estado 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON cdb.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
LEFT OUTER JOIN /*dbo.K_CREDITO*/ dbo.CRE_Credito AS k WITH(NOLOCK) 
ON sust.No_Credito = k.No_Credito 
LEFT OUTER JOIN dbo.CAT_PROGRAMA AS pr WITH(NOLOCK) 
ON k.Id_prog_Proy = pr.Id_Prog_Proy 
WHERE sust.Fg_Tipo_Centro_Disp = 'B' AND 
sust.Cve_Tecnologia = 1 AND inh2.Dt_Fecha_Inhabilitacion 
BETWEEN @FechaI AND @FechaF 
AND (edo.Cve_Estado = @Estado OR @Estado = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zona OR @Zona = '0') 
AND (cdb.Dx_Nombre_Comercial = @CD OR @CD = 'TODOS') 
AND (pr.Id_Prog_Proy = @Prog OR @Prog = '0') 
GROUP BY cdb.Id_Centro_Disp, edo.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, 
cdb.Dx_Razon_Social, cdb.Dx_Nombre_Comercial, 
inh2.Dt_Fecha_Inhabilitacion 
UNION ALL 
SELECT cdb.Id_Centro_Disp, edo.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, 
cdb.Dx_Razon_Social, cdb.Dx_Nombre_Comercial, 
COUNT(sust.Id_Credito_Sustitucion), 
'Aire Acondicionado' AS 'Tec', 
inh2.Dt_Fecha_Inhabilitacion AS 'Fecha Inhabilitacion' 
FROM dbo.K_INHABILITACION_PRODUCTO AS inh WITH(NOLOCK) 
INNER JOIN dbo.K_INHABILITACION AS inh2 WITH(NOLOCK) 
ON inh.Id_Inhabilitacion = inh2.Id_Inhabilitacion 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS sust WITH(NOLOCK) 
ON inh.Id_Credito_Sustitucion = sust.Id_Credito_Sustitucion 
INNER JOIN dbo.CAT_CENTRO_DISP_SUCURSAL AS cdb WITH(NOLOCK) 
ON sust.Id_Centro_Disp = cdb.Id_Centro_Disp_Sucursal 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON sust.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN dbo.CAT_ESTADO AS edo WITH(NOLOCK) 
ON cdb.Cve_Estado_Fisc = edo.Cve_Estado 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON cdb.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
LEFT OUTER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS k WITH(NOLOCK) 
ON sust.No_Credito = k.No_Credito 
LEFT OUTER JOIN dbo.CAT_PROGRAMA AS pr WITH(NOLOCK) 
ON k.Id_prog_Proy = pr.Id_Prog_Proy 
WHERE sust.Fg_Tipo_Centro_Disp = 'B' AND 
sust.Cve_Tecnologia = 2 AND inh2.Dt_Fecha_Inhabilitacion 
BETWEEN @FechaI AND @FechaF 
AND (edo.Cve_Estado = @Estado OR @Estado = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zona OR @Zona = '0') 
AND (cdb.Dx_Nombre_Comercial = @CD OR @CD = 'TODOS') 
AND (pr.Id_Prog_Proy = @Prog OR @Prog = '0') 
GROUP BY cdb.Id_Centro_Disp, edo.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, 
cdb.Dx_Razon_Social, cdb.Dx_Nombre_Comercial, 
inh2.Dt_Fecha_Inhabilitacion 
UNION ALL 
SELECT cdb.Id_Centro_Disp, edo.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, 
cdb.Dx_Razon_Social, cdb.Dx_Nombre_Comercial, 
COUNT(sust.Id_Credito_Sustitucion), 
'Motores Eléctricos' AS 'Tec', 
inh2.Dt_Fecha_Inhabilitacion AS 'Fecha Inhabilitacion' 
FROM dbo.K_INHABILITACION_PRODUCTO AS inh WITH(NOLOCK) 
INNER JOIN dbo.K_INHABILITACION AS inh2 WITH(NOLOCK) 
ON inh.Id_Inhabilitacion = inh2.Id_Inhabilitacion 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS sust WITH(NOLOCK) 
ON inh.Id_Credito_Sustitucion = sust.Id_Credito_Sustitucion 
INNER JOIN dbo.CAT_CENTRO_DISP_SUCURSAL AS cdb WITH(NOLOCK) 
ON sust.Id_Centro_Disp = cdb.Id_Centro_Disp_Sucursal 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON sust.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN dbo.CAT_ESTADO AS edo WITH(NOLOCK) 
ON cdb.Cve_Estado_Fisc = edo.Cve_Estado 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON cdb.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
LEFT OUTER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS k WITH(NOLOCK) 
ON sust.No_Credito = k.No_Credito 
LEFT OUTER JOIN dbo.CAT_PROGRAMA AS pr WITH(NOLOCK) 
ON k.Id_prog_Proy = pr.Id_Prog_Proy 
WHERE sust.Fg_Tipo_Centro_Disp = 'B' AND 
sust.Cve_Tecnologia = 4 AND inh2.Dt_Fecha_Inhabilitacion 
BETWEEN @FechaI AND @FechaF 
AND (edo.Cve_Estado = @Estado OR @Estado = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zona OR @Zona = '0') 
AND (cdb.Dx_Nombre_Comercial = @CD OR @CD = 'TODOS') 
AND (pr.Id_Prog_Proy = @Prog OR @Prog = '0') 
GROUP BY cdb.Id_Centro_Disp, edo.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, 
cdb.Dx_Razon_Social, cdb.Dx_Nombre_Comercial, 
inh2.Dt_Fecha_Inhabilitacion 
ORDER BY edo.Dx_Nombre_Estado, reg.Dx_Nombre_Region