declare @FechaI date = '01-05-2009';
declare @FechaF date = '01-05-2014';
declare @Estatus tinyint = '0';
declare @Region varchar(max)= '0';
declare @Zone varchar(max) = '0';
declare @CD varchar(max) = '0';
declare @Tec varchar(max) = '0';
declare @Reg varchar(max) = 'TODAS';
declare @Prog varchar(max) = '0';
declare @Tipo varchar(Max)= '';
declare @FI date = '01-01-2013';
declare @FF date = '01-01-2014';
declare @Estado Varchar (max) = '0';
declare @Tarifa nvarchar(2) = '02';
declare @Todos tinyint = 1;
					
SELECT DISTINCT k.No_Credito AS 'Credito', soc.Dx_Tipo_Sociedad AS 'Tipo', 
case when cli.Cve_Tipo_Sociedad = 1 then Cli.Nombre + ' ' + ISNULL (Cli.Ap_Paterno, ' ') + ' ' +ISNULL(cli.Ap_Materno, '') 
else  Cli.Razon_Social end AS 'Razón Social', neg.Nombre_Comercial AS 'Nombre Comercial', 
giro.DESCRIPCION_SCIAN AS 'Giro', k.RPU AS 'RPU', MAX(rd.Rate) AS 'Tarifa', 
k.Fecha_Ultmod AS 'Fecha Credito', estatus.Dx_Estatus_Credito AS 'Estatus', 
prog.Dx_Nombre_Programa AS 'Programa', prov.Dx_Razon_Social AS 'Proveedor RS', 
prov.Dx_Nombre_Comercial AS 'Proveedor NC', estado.Dx_Nombre_Estado AS 'Estado', 
reg.Dx_Nombre_Region AS 'Región', zon.Dx_Nombre_Zona AS 'Zona', tec.Dx_Nombre_General 
AS 'Tecnologia', prod2.Dx_Modelo_Producto AS 'Modelo', kprod.No_Cantidad AS 'No Equipos', 
kprod.Mt_Total AS 'Monto Equipos', kprod.No_Cantidad * 400 AS 'Costo CAyD', 
kprod.Mt_Total * .10 AS 'Descuento', (kprod.Mt_Total - kprod.Mt_Total * .10 
+ kprod.No_Cantidad * 400) AS 'Monto Solicitado' 

FROM K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN CAT_PRODUCTO AS prod2 WITH(NOLOCK) 
ON kprod.Cve_Producto = prod2.Cve_Producto 
INNER JOIN CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod2.Cve_Tecnologia = tec.Cve_Tecnologia 
LEFT OUTER JOIN /*dbo.K_CREDITO*/ dbo.CRE_Credito k WITH (NOLOCK) 
ON kprod.No_Credito = k.No_Credito /**/
INNER JOIN CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente=Neg.IdCliente and k.IdNegocio = Neg.IdNegocio
INNER JOIN CLI_Cliente as Cli on
Cli.Id_Proveedor = Neg.Id_Proveedor and Cli.Id_Branch = Neg.Id_Branch and cli.IdCliente = neg.IdCliente
INNER JOIN DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and Dir.Id_Branch = Neg.Id_Branch and  Dir.IdCliente = Neg.IdCliente and Dir.IdNegocio = Neg.IdNegocio and  Dir.IdTipoDomicilio = 2
/*INNER JOIN CLI_Ref_Cliente as Ref on
ref.Id_Proveedor = Dir.Id_Proveedor and ref.Id_Branch = dir.Id_Branch and ref.IdNegocio = dir.IdNegocio and ref.IdCliente = dir.IdCliente*/
LEFT OUTER JOIN CAT_TIPO_SOCIEDAD AS soc WITH (NOLOCK)
ON cli.Cve_Tipo_Sociedad = soc.Cve_Tipo_Sociedad 
/**/
LEFT OUTER JOIN CAT_TIPO_INDUSTRIA AS giro WITH (NOLOCK) /**/
ON neg.Cve_Tipo_Industria = giro.Cve_Tipo_Industria 
LEFT OUTER JOIN CAT_PROGRAMA AS prog WITH (NOLOCK) 
ON k.ID_Prog_Proy = prog.ID_Prog_Proy 
LEFT OUTER JOIN CAT_ESTATUS_CREDITO AS estatus WITH (NOLOCK) 
ON k.Cve_Estatus_Credito = estatus.Cve_Estatus_Credito 
LEFT OUTER JOIN CAT_ESTADO AS estado WITH (NOLOCK) 
ON Dir.Cve_Estado = estado.Cve_Estado 
LEFT OUTER JOIN ResponseData AS rd WITH (NOLOCK) 
ON k.RPU = rd.ServiceCode 
LEFT OUTER JOIN CAT_PROVEEDOR AS prov WITH (NOLOCK) 
ON k.Id_Proveedor = prov.Id_Proveedor 
LEFT OUTER JOIN CAT_ZONA AS zon WITH (NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
LEFT OUTER JOIN CAT_REGION AS reg WITH (NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE (k.ID_Intelisis IS NULL OR k.Afectacion_SICOM_DigitoVerOk IS NULL 
OR k.Afectacion_SIRCA_Digito IS NULL) AND tec.Cve_Tecnologia IN (1,2,4) 
AND k.Tipo_Usuario = 'S' 
AND (k.Fecha_Ultmod BETWEEN @FechaI AND @FechaF) 
AND (estatus.Cve_Estatus_Credito = @Estatus OR @Estatus = '0') 
AND (rd.Rate = @Tarifa OR @Tarifa = 'TODAS') 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
GROUP BY k.No_Credito, soc.Dx_Tipo_Sociedad, Cli.Razon_Social, 
neg.Nombre_Comercial, giro.Dx_Tipo_Industria, 
k.RPU, rd.Rate, k.Fecha_Ultmod, estatus.Dx_Estatus_Credito, 
prog.Dx_Nombre_Programa, prov.Dx_Razon_Social, prov.Dx_Nombre_Comercial, 
estado.Dx_Nombre_Estado, reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, 
tec.Dx_Nombre_General, prod2.Dx_Modelo_Producto, kprod.No_Cantidad, kprod.Mt_Total, Cli.Nombre,cli.Ap_Materno,cli.Ap_Paterno,cli.Cve_Tipo_Sociedad
,giro.DESCRIPCION_SCIAN



UNION ALL 
SELECT DISTINCT k.No_Credito AS 'Credito', soc.Dx_Tipo_Sociedad AS 'Tipo',
case when cli.Cve_Tipo_Sociedad = 1 then Cli.Nombre + ' ' + ISNULL (Cli.Ap_Paterno, ' ') + ' ' +ISNULL(cli.Ap_Materno, '') 
else cli.Razon_Social end AS 'Razón Social', Neg.Nombre_Comercial AS 'Nombre Comercial', 
giro.DESCRIPCION_SCIAN AS 'Giro', k.RPU AS 'RPU', MAX(rd.Rate) AS 'Tarifa', 
k.Fecha_Ultmod AS 'Fecha Credito', estatus.Dx_Estatus_Credito AS 'Estatus', 
prog.Dx_Nombre_Programa AS 'Programa', prov.Dx_Razon_Social AS 'Proveedor RS', 
prov.Dx_Nombre_Comercial AS 'Proveedor NC', estado.Dx_Nombre_Estado AS 'Estado', 
reg.Dx_Nombre_Region AS 'Región', zon.Dx_Nombre_Zona AS 'Zona', tec.Dx_Nombre_General 
AS 'Tecnologia', prod2.Dx_Modelo_Producto, kprod.No_Cantidad AS 'No Equipos', 
kprod.Mt_Total AS 'Monto Equipos', kprod.No_Cantidad * 400 AS 'Costo CAyD', 
kprod.Mt_Total * .10 AS 'Descuento', (kprod.Mt_Total - kprod.Mt_Total * .10 + 
kprod.No_Cantidad * 400) AS 'Monto Solicitado' 
FROM K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN CAT_PRODUCTO AS prod2 WITH(NOLOCK) 
ON kprod.Cve_Producto = prod2.Cve_Producto 
INNER JOIN CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod2.Cve_Tecnologia = tec.Cve_Tecnologia 
LEFT OUTER JOIN /*dbo.K_CREDITO*/ dbo.CRE_Credito k WITH (NOLOCK) 
ON kprod.No_Credito = k.No_Credito 
/**/
INNER JOIN CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente=Neg.IdCliente and k.IdNegocio = Neg.IdNegocio
INNER JOIN CLI_Cliente as Cli on
Cli.Id_Proveedor = Neg.Id_Proveedor and Cli.Id_Branch = Neg.Id_Branch and cli.IdCliente = neg.IdCliente
INNER JOIN DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and Dir.Id_Branch = Neg.Id_Branch and  Dir.IdCliente = Neg.IdCliente and Dir.IdNegocio = Neg.IdNegocio and  Dir.IdTipoDomicilio = 2
/*INNER JOIN CLI_Ref_Cliente as Ref on
ref.Id_Proveedor = Dir.Id_Proveedor and ref.Id_Branch = dir.Id_Branch and ref.IdNegocio = dir.IdNegocio and ref.IdCliente = dir.IdCliente*/
LEFT OUTER JOIN CAT_TIPO_SOCIEDAD AS soc WITH (NOLOCK)
ON cli.Cve_Tipo_Sociedad = soc.Cve_Tipo_Sociedad 
/**/
LEFT OUTER JOIN CAT_TIPO_INDUSTRIA AS giro WITH (NOLOCK) 
ON Neg.Cve_Tipo_Industria = giro.Cve_Tipo_Industria 
LEFT OUTER JOIN CAT_PROGRAMA AS prog WITH (NOLOCK) 
ON k.ID_Prog_Proy = prog.ID_Prog_Proy 
LEFT OUTER JOIN CAT_ESTATUS_CREDITO AS estatus WITH (NOLOCK) 
ON k.Cve_Estatus_Credito = estatus.Cve_Estatus_Credito 
LEFT OUTER JOIN CAT_ESTADO AS estado WITH (NOLOCK) 
ON Dir.Cve_Estado = estado.Cve_Estado 
LEFT OUTER JOIN ResponseData AS rd WITH (NOLOCK) 
ON k.RPU = rd.ServiceCode 
LEFT OUTER JOIN CAT_PROVEEDOR AS prov WITH (NOLOCK) 
ON k.Id_Proveedor = prov.Id_Proveedor 
LEFT OUTER JOIN CAT_ZONA AS zon WITH (NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
LEFT OUTER JOIN CAT_REGION AS reg WITH (NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE (estatus.Cve_Estatus_Credito = 4 AND k.ID_Intelisis IS NOT NULL AND 
k.Afectacion_SICOM_DigitoVerOk IS NOT NULL AND k.Afectacion_SIRCA_Digito IS NOT NULL) 
AND tec.Cve_Tecnologia IN (1,2,4) AND k.Tipo_Usuario = 'S' 
AND (k.Afectacion_SICOM_Fecha BETWEEN @FechaI AND @FechaF) 
AND (@Estatus = '11' OR @Estatus = '0') 
AND (rd.Rate = @Tarifa OR @Tarifa = 'TODAS') 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
GROUP BY k.No_Credito, soc.Dx_Tipo_Sociedad, Cli.Razon_Social, neg.Nombre_Comercial, 
giro.Dx_Tipo_Industria, k.RPU, rd.Rate, k.Fecha_Ultmod, estatus.Dx_Estatus_Credito, 
prog.Dx_Nombre_Programa, prov.Dx_Razon_Social, prov.Dx_Nombre_Comercial, estado.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, tec.Dx_Nombre_General, prod2.Dx_Modelo_Producto, 
kprod.No_Cantidad, kprod.Mt_Total ,Cli.Nombre,cli.Ap_Materno,cli.Ap_Paterno,cli.Cve_Tipo_Sociedad
,giro.DESCRIPCION_SCIAN


UNION ALL 
SELECT DISTINCT k.No_Credito AS 'Credito', soc.Dx_Tipo_Sociedad AS 'Tipo', 
case when cli.Cve_Tipo_Sociedad = 1 then Cli.Nombre + ' ' + ISNULL (Cli.Ap_Paterno, ' ') + ' ' +ISNULL(cli.Ap_Materno, '') 
else Cli.Razon_Social end AS 'Razón Social', Neg.Nombre_Comercial AS 'Nombre Comercial', 
giro.DESCRIPCION_SCIAN AS 'Giro', k.RPU AS 'RPU', MAX(rd.Rate) AS 'Tarifa', 
k.Fecha_Ultmod AS 'Fecha Credito', estatus.Dx_Estatus_Credito AS 'Estatus', 
prog.Dx_Nombre_Programa AS 'Programa', prov.Dx_Razon_Social AS 'Proveedor RS', 
prov.Dx_Nombre_Comercial AS 'Proveedor NC', estado.Dx_Nombre_Estado AS 'Estado', 
reg.Dx_Nombre_Region AS 'Región', zon.Dx_Nombre_Zona AS 'Zona', tec.Dx_Nombre_General 
AS 'Tecnologia', prod2.Dx_Modelo_Producto, kprod.No_Cantidad AS 'No Equipos', 
kprod.Mt_Total AS 'Monto Equipos', kprod.No_Cantidad * 0 AS 'Costo CAyD', 
kprod.Mt_Total * 0 AS 'Descuento', kprod.Mt_Total AS 'Monto Solicitado' 
FROM K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN CAT_PRODUCTO AS prod2 WITH(NOLOCK) 
ON kprod.Cve_Producto = prod2.Cve_Producto 
INNER JOIN CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod2.Cve_Tecnologia = tec.Cve_Tecnologia 
LEFT OUTER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito k WITH (NOLOCK) 
ON kprod.No_Credito = k.No_Credito 
/**/

INNER JOIN CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente=Neg.IdCliente and k.IdNegocio = Neg.IdNegocio
INNER JOIN CLI_Cliente as Cli on
Cli.Id_Proveedor = Neg.Id_Proveedor and Cli.Id_Branch = Neg.Id_Branch and cli.IdCliente = neg.IdCliente
INNER JOIN DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and Dir.Id_Branch = Neg.Id_Branch and  Dir.IdCliente = Neg.IdCliente and Dir.IdNegocio = Neg.IdNegocio and  Dir.IdTipoDomicilio = 2
/*INNER JOIN CLI_Ref_Cliente as Ref on
ref.Id_Proveedor = Dir.Id_Proveedor and ref.Id_Branch = dir.Id_Branch and ref.IdNegocio = dir.IdNegocio and ref.IdCliente = dir.IdCliente*/
LEFT OUTER JOIN CAT_TIPO_SOCIEDAD AS soc WITH (NOLOCK)
ON cli.Cve_Tipo_Sociedad = soc.Cve_Tipo_Sociedad 

/**/
LEFT OUTER JOIN CAT_TIPO_INDUSTRIA AS giro WITH (NOLOCK) 
ON neg.Cve_Tipo_Industria = giro.Cve_Tipo_Industria 
LEFT OUTER JOIN CAT_PROGRAMA AS prog WITH (NOLOCK) 
ON k.ID_Prog_Proy = prog.ID_Prog_Proy 
LEFT OUTER JOIN CAT_ESTATUS_CREDITO AS estatus WITH (NOLOCK) 
ON k.Cve_Estatus_Credito = estatus.Cve_Estatus_Credito 
LEFT OUTER JOIN CAT_ESTADO AS estado WITH (NOLOCK) 
ON Dir.Cve_Estado = estado.Cve_Estado 
LEFT OUTER JOIN ResponseData AS rd WITH (NOLOCK) 
ON k.RPU = rd.ServiceCode 
LEFT OUTER JOIN CAT_PROVEEDOR AS prov WITH (NOLOCK) 
ON k.Id_Proveedor = prov.Id_Proveedor 
LEFT OUTER JOIN CAT_ZONA AS zon WITH (NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
LEFT OUTER JOIN CAT_REGION AS reg WITH (NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE (k.ID_Intelisis IS NULL OR k.Afectacion_SICOM_DigitoVerOk IS NULL 
OR k.Afectacion_SIRCA_Digito IS NULL) AND tec.Cve_Tecnologia NOT IN (1,2,4) 
AND k.Tipo_Usuario = 'S' 
AND (k.Fecha_Ultmod BETWEEN @FechaI AND @FechaF) 
AND (estatus.Cve_Estatus_Credito = @Estatus OR @Estatus = '0') 
AND (rd.Rate = @Tarifa OR @Tarifa = 'TODAS') 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
GROUP BY k.No_Credito, soc.Dx_Tipo_Sociedad, Cli.Razon_Social, Neg.Nombre_Comercial, 
giro.Dx_Tipo_Industria, k.RPU, rd.Rate, k.Fecha_Ultmod, estatus.Dx_Estatus_Credito, 
prog.Dx_Nombre_Programa, prov.Dx_Razon_Social, prov.Dx_Nombre_Comercial, estado.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, tec.Dx_Nombre_General, prod2.Dx_Modelo_Producto, 
kprod.No_Cantidad, kprod.Mt_Total  ,Cli.Nombre,cli.Ap_Materno,cli.Ap_Paterno,cli.Cve_Tipo_Sociedad
,giro.DESCRIPCION_SCIAN


UNION ALL 
SELECT DISTINCT k.No_Credito AS 'Credito', soc.Dx_Tipo_Sociedad AS 'Tipo', 
case when cli.Cve_Tipo_Sociedad = 1 then Cli.Nombre + ' ' + ISNULL (Cli.Ap_Paterno, ' ') + ' ' +ISNULL(cli.Ap_Materno, '') 
else 
Cli.Razon_Social end AS 'Razón Social', Neg.Nombre_Comercial AS 'Nombre Comercial', 
giro.DESCRIPCION_SCIAN AS 'Giro', k.RPU AS 'RPU', MAX(rd.Rate) AS 'Tarifa', 
k.Fecha_Ultmod AS 'Fecha Credito', estatus.Dx_Estatus_Credito AS 'Estatus', 
prog.Dx_Nombre_Programa AS 'Programa', prov.Dx_Razon_Social AS 'Proveedor RS', 
prov.Dx_Nombre_Comercial AS 'Proveedor NC', estado.Dx_Nombre_Estado AS 'Estado', 
reg.Dx_Nombre_Region AS 'Región', zon.Dx_Nombre_Zona AS 'Zona', tec.Dx_Nombre_General 
AS 'Tecnologia', prod2.Dx_Modelo_Producto, kprod.No_Cantidad AS 'No Equipos', 
kprod.Mt_Total AS 'Monto Equipos', kprod.No_Cantidad * 0 AS 'Costo CAyD', 
kprod.Mt_Total * 0 AS 'Descuento', kprod.Mt_Total AS 'Monto Solicitado' 
FROM K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN CAT_PRODUCTO AS prod2 WITH(NOLOCK) 
ON kprod.Cve_Producto = prod2.Cve_Producto 
INNER JOIN CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod2.Cve_Tecnologia = tec.Cve_Tecnologia 
LEFT OUTER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito k WITH (NOLOCK) 
ON kprod.No_Credito = k.No_Credito 
/**/

INNER JOIN CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente=Neg.IdCliente and k.IdNegocio = Neg.IdNegocio
INNER JOIN CLI_Cliente as Cli on
Cli.Id_Proveedor = Neg.Id_Proveedor and Cli.Id_Branch = Neg.Id_Branch and cli.IdCliente = neg.IdCliente
INNER JOIN DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and Dir.Id_Branch = Neg.Id_Branch and  Dir.IdCliente = Neg.IdCliente and Dir.IdNegocio = Neg.IdNegocio and  Dir.IdTipoDomicilio = 2
/*INNER JOIN CLI_Ref_Cliente as Ref on
ref.Id_Proveedor = Dir.Id_Proveedor and ref.Id_Branch = dir.Id_Branch and ref.IdNegocio = dir.IdNegocio and ref.IdCliente = dir.IdCliente*/
LEFT OUTER JOIN CAT_TIPO_SOCIEDAD AS soc WITH (NOLOCK)
ON cli.Cve_Tipo_Sociedad = soc.Cve_Tipo_Sociedad 

/**/

LEFT OUTER JOIN CAT_TIPO_INDUSTRIA AS giro WITH (NOLOCK) 
ON Neg.Cve_Tipo_Industria = giro.Cve_Tipo_Industria 
LEFT OUTER JOIN CAT_PROGRAMA AS prog WITH (NOLOCK) 
ON k.ID_Prog_Proy = prog.ID_Prog_Proy 
LEFT OUTER JOIN CAT_ESTATUS_CREDITO AS estatus WITH (NOLOCK) 
ON k.Cve_Estatus_Credito = estatus.Cve_Estatus_Credito 
LEFT OUTER JOIN CAT_ESTADO AS estado WITH (NOLOCK) 
ON Dir.Cve_Estado = estado.Cve_Estado 
LEFT OUTER JOIN ResponseData AS rd WITH (NOLOCK) 
ON k.RPU = rd.ServiceCode 
LEFT OUTER JOIN CAT_PROVEEDOR AS prov WITH (NOLOCK) 
ON k.Id_Proveedor = prov.Id_Proveedor 
LEFT OUTER JOIN CAT_ZONA AS zon WITH (NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
LEFT OUTER JOIN CAT_REGION AS reg WITH (NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE (estatus.Cve_Estatus_Credito = 4 AND k.ID_Intelisis IS NOT NULL AND 
k.Afectacion_SICOM_DigitoVerOk IS NOT NULL AND k.Afectacion_SIRCA_Digito IS NOT NULL) 
AND tec.Cve_Tecnologia NOT IN (1,2,4) AND k.Tipo_Usuario = 'S' 
AND (k.Afectacion_SICOM_Fecha BETWEEN @FechaI AND @FechaF) 
AND (@Estatus = '11' OR @Estatus = '0') 
AND (rd.Rate = @Tarifa OR @Tarifa = 'TODAS') 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
GROUP BY k.No_Credito, soc.Dx_Tipo_Sociedad, Cli.Razon_Social, Neg.Nombre_Comercial, 
giro.Dx_Tipo_Industria, k.RPU, rd.Rate, k.Fecha_Ultmod, estatus.Dx_Estatus_Credito, 
prog.Dx_Nombre_Programa, prov.Dx_Razon_Social, prov.Dx_Nombre_Comercial, estado.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, tec.Dx_Nombre_General, prod2.Dx_Modelo_Producto, 
kprod.No_Cantidad, kprod.Mt_Total  ,Cli.Nombre,cli.Ap_Materno,cli.Ap_Paterno,cli.Cve_Tipo_Sociedad
,giro.DESCRIPCION_SCIAN


UNION ALL 
SELECT DISTINCT k.No_Credito AS 'Credito', soc.Dx_Tipo_Sociedad AS 'Tipo', 
case when cli.Cve_Tipo_Sociedad = 1 then Cli.Nombre + ' ' + ISNULL (Cli.Ap_Paterno, ' ') + ' ' +ISNULL(cli.Ap_Materno, '') 
else cli.Razon_Social end AS 'Razón Social', Neg.Nombre_Comercial AS 'Nombre Comercial', 
giro.DESCRIPCION_SCIAN AS 'Giro', k.RPU AS 'RPU', MAX(rd.Rate) AS 'Tarifa', 
k.Fecha_Ultmod AS 'Fecha Credito', estatus.Dx_Estatus_Credito AS 'Estatus', 
prog.Dx_Nombre_Programa AS 'Programa', prov.Dx_Razon_Social AS 'Proveedor RS', 
prov.Dx_Nombre_Comercial AS 'Proveedor NC', estado.Dx_Nombre_Estado AS 'Estado', 
reg.Dx_Nombre_Region AS 'Región', zon.Dx_Nombre_Zona AS 'Zona', tec.Dx_Nombre_General 
AS 'Tecnologia', prod2.Dx_Modelo_Producto, kprod.No_Cantidad AS 'No Equipos', 
kprod.Mt_Total AS 'Monto Equipos', kprod.No_Cantidad * 400 AS 'Costo CAyD', 
kprod.Mt_Total * .10 AS 'Descuento', (kprod.Mt_Total - kprod.Mt_Total * .10 
+ kprod.No_Cantidad * 400) AS 'Monto Solicitado' 
FROM K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN CAT_PRODUCTO AS prod2 WITH(NOLOCK) 
ON kprod.Cve_Producto = prod2.Cve_Producto 
INNER JOIN CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod2.Cve_Tecnologia = tec.Cve_Tecnologia 
LEFT OUTER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito k WITH (NOLOCK) 
ON kprod.No_Credito = k.No_Credito 
/**/

INNER JOIN CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente=Neg.IdCliente and k.IdNegocio = Neg.IdNegocio
INNER JOIN CLI_Cliente as Cli on
Cli.Id_Proveedor = Neg.Id_Proveedor and Cli.Id_Branch = Neg.Id_Branch and cli.IdCliente = neg.IdCliente
INNER JOIN DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and Dir.Id_Branch = Neg.Id_Branch and  Dir.IdCliente = Neg.IdCliente and Dir.IdNegocio = Neg.IdNegocio and  Dir.IdTipoDomicilio = 2
/*INNER JOIN CLI_Ref_Cliente as Ref on
ref.Id_Proveedor = Dir.Id_Proveedor and ref.Id_Branch = dir.Id_Branch and ref.IdNegocio = dir.IdNegocio and ref.IdCliente = dir.IdCliente*/
LEFT OUTER JOIN CAT_TIPO_SOCIEDAD AS soc WITH (NOLOCK)
ON cli.Cve_Tipo_Sociedad = soc.Cve_Tipo_Sociedad 

/**/
LEFT OUTER JOIN CAT_TIPO_INDUSTRIA AS giro WITH (NOLOCK) 
ON Neg.Cve_Tipo_Industria = giro.Cve_Tipo_Industria 
LEFT OUTER JOIN CAT_PROGRAMA AS prog WITH (NOLOCK) 
ON k.ID_Prog_Proy = prog.ID_Prog_Proy 
LEFT OUTER JOIN CAT_ESTATUS_CREDITO AS estatus WITH (NOLOCK) 
ON k.Cve_Estatus_Credito = estatus.Cve_Estatus_Credito 
LEFT OUTER JOIN CAT_ESTADO AS estado WITH (NOLOCK) 
ON Dir.Cve_Estado = estado.Cve_Estado 
LEFT OUTER JOIN ResponseData AS rd WITH (NOLOCK) 
ON k.RPU = rd.ServiceCode 
LEFT OUTER JOIN CAT_PROVEEDORBRANCH AS prov WITH (NOLOCK) 
ON k.Id_Proveedor = prov.Id_Branch 
LEFT OUTER JOIN CAT_ZONA AS zon WITH (NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
LEFT OUTER JOIN CAT_REGION AS reg WITH (NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE (k.ID_Intelisis IS NULL OR k.Afectacion_SICOM_DigitoVerOk IS NULL 
OR k.Afectacion_SIRCA_Digito IS NULL) AND tec.Cve_Tecnologia IN (1,2,4) 
AND k.Tipo_Usuario = 'S_B' 
AND (k.Fecha_Ultmod BETWEEN @FechaI AND @FechaF) 
AND (estatus.Cve_Estatus_Credito = @Estatus OR @Estatus = '0') 
AND (rd.Rate = @Tarifa OR @Tarifa = 'TODAS') 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
GROUP BY k.No_Credito, soc.Dx_Tipo_Sociedad, cli.Razon_Social, Neg.Nombre_Comercial, 
giro.Dx_Tipo_Industria, k.RPU, rd.Rate, k.Fecha_Ultmod, estatus.Dx_Estatus_Credito, 
prog.Dx_Nombre_Programa, prov.Dx_Razon_Social, prov.Dx_Nombre_Comercial, estado.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, tec.Dx_Nombre_General, prod2.Dx_Modelo_Producto, 
kprod.No_Cantidad, kprod.Mt_Total  ,Cli.Nombre,cli.Ap_Materno,cli.Ap_Paterno,cli.Cve_Tipo_Sociedad
,giro.DESCRIPCION_SCIAN

UNION ALL 
SELECT DISTINCT k.No_Credito AS 'Credito', soc.Dx_Tipo_Sociedad AS 'Tipo', 
case when cli.Cve_Tipo_Sociedad = 1 then Cli.Nombre + ' ' + ISNULL (Cli.Ap_Paterno, ' ') + ' ' +ISNULL(cli.Ap_Materno, '') 
else cli.Razon_Social end AS 'Razón Social', Neg.Nombre_Comercial AS 'Nombre Comercial', 
giro.DESCRIPCION_SCIAN AS 'Giro', k.RPU AS 'RPU', MAX(rd.Rate) AS 'Tarifa', 
k.Fecha_Ultmod AS 'Fecha Credito', estatus.Dx_Estatus_Credito AS 'Estatus', 
prog.Dx_Nombre_Programa AS 'Programa', prov.Dx_Razon_Social AS 'Proveedor RS', 
prov.Dx_Nombre_Comercial AS 'Proveedor NC', estado.Dx_Nombre_Estado AS 'Estado', 
reg.Dx_Nombre_Region AS 'Región', zon.Dx_Nombre_Zona AS 'Zona', tec.Dx_Nombre_General 
AS 'Tecnologia', prod2.Dx_Modelo_Producto, kprod.No_Cantidad AS 'No Equipos', 
kprod.Mt_Total AS 'Monto Equipos', kprod.No_Cantidad * 400 AS 'Costo CAyD', 
kprod.Mt_Total * .10 AS 'Descuento', (kprod.Mt_Total - kprod.Mt_Total * .10 
+ kprod.No_Cantidad * 400) AS 'Monto Solicitado' 
FROM K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN CAT_PRODUCTO AS prod2 WITH(NOLOCK) 
ON kprod.Cve_Producto = prod2.Cve_Producto 
INNER JOIN CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod2.Cve_Tecnologia = tec.Cve_Tecnologia 
LEFT OUTER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito k WITH (NOLOCK) 
ON kprod.No_Credito = k.No_Credito 
/**/

INNER JOIN CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente=Neg.IdCliente and k.IdNegocio = Neg.IdNegocio
INNER JOIN CLI_Cliente as Cli on
Cli.Id_Proveedor = Neg.Id_Proveedor and Cli.Id_Branch = Neg.Id_Branch and cli.IdCliente = neg.IdCliente
INNER JOIN DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and Dir.Id_Branch = Neg.Id_Branch and  Dir.IdCliente = Neg.IdCliente and Dir.IdNegocio = Neg.IdNegocio and  Dir.IdTipoDomicilio = 2
/*INNER JOIN CLI_Ref_Cliente as Ref on
ref.Id_Proveedor = Dir.Id_Proveedor and ref.Id_Branch = dir.Id_Branch and ref.IdNegocio = dir.IdNegocio and ref.IdCliente = dir.IdCliente*/
LEFT OUTER JOIN CAT_TIPO_SOCIEDAD AS soc WITH (NOLOCK)
ON cli.Cve_Tipo_Sociedad = soc.Cve_Tipo_Sociedad 

/**/
LEFT OUTER JOIN CAT_TIPO_INDUSTRIA AS giro WITH (NOLOCK) 
ON Neg.Cve_Tipo_Industria = giro.Cve_Tipo_Industria 
LEFT OUTER JOIN CAT_PROGRAMA AS prog WITH (NOLOCK) 
ON k.ID_Prog_Proy = prog.ID_Prog_Proy 
LEFT OUTER JOIN CAT_ESTATUS_CREDITO AS estatus WITH (NOLOCK) 
ON k.Cve_Estatus_Credito = estatus.Cve_Estatus_Credito 
LEFT OUTER JOIN CAT_ESTADO AS estado WITH (NOLOCK) 
ON Dir.Cve_Estado = estado.Cve_Estado 
LEFT OUTER JOIN ResponseData AS rd WITH (NOLOCK) 
ON k.RPU = rd.ServiceCode 
LEFT OUTER JOIN CAT_PROVEEDORBRANCH AS prov WITH (NOLOCK) 
ON k.Id_Proveedor = prov.Id_Branch 
LEFT OUTER JOIN CAT_ZONA AS zon WITH (NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
LEFT OUTER JOIN CAT_REGION AS reg WITH (NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE (estatus.Cve_Estatus_Credito = 4 AND k.ID_Intelisis IS NOT NULL AND 
k.Afectacion_SICOM_DigitoVerOk IS NOT NULL AND k.Afectacion_SIRCA_Digito IS NOT NULL) 
AND tec.Cve_Tecnologia IN (1,2,4) AND k.Tipo_Usuario = 'S_B' 
AND (k.Afectacion_SICOM_Fecha BETWEEN @FechaI AND @FechaF) 
AND (@Estatus = '11' OR @Estatus = '0') 
AND (rd.Rate = @Tarifa OR @Tarifa = 'TODAS') 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
GROUP BY k.No_Credito, soc.Dx_Tipo_Sociedad, Cli.Razon_Social, Neg.Nombre_Comercial, 
giro.Dx_Tipo_Industria, k.RPU, rd.Rate, k.Fecha_Ultmod, estatus.Dx_Estatus_Credito, 
prog.Dx_Nombre_Programa, prov.Dx_Razon_Social, prov.Dx_Nombre_Comercial, estado.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, tec.Dx_Nombre_General, prod2.Dx_Modelo_Producto, 
kprod.No_Cantidad, kprod.Mt_Total  ,Cli.Nombre,cli.Ap_Materno,cli.Ap_Paterno,cli.Cve_Tipo_Sociedad
,giro.DESCRIPCION_SCIAN



UNION ALL 
SELECT DISTINCT k.No_Credito AS 'Credito', soc.Dx_Tipo_Sociedad AS 'Tipo', 
case when cli.Cve_Tipo_Sociedad = 1 then Cli.Nombre + ' ' + ISNULL (Cli.Ap_Paterno, ' ') + ' ' +ISNULL(cli.Ap_Materno, '') 
else Cli.Razon_Social end AS 'Razón Social', Neg.Nombre_Comercial AS 'Nombre Comercial', 
giro.DESCRIPCION_SCIAN AS 'Giro', k.RPU AS 'RPU', MAX(rd.Rate) AS 'Tarifa', 
k.Fecha_Ultmod AS 'Fecha Credito', estatus.Dx_Estatus_Credito AS 'Estatus', 
prog.Dx_Nombre_Programa AS 'Programa', prov.Dx_Razon_Social AS 'Proveedor RS', 
prov.Dx_Nombre_Comercial AS 'Proveedor NC', estado.Dx_Nombre_Estado AS 'Estado', 
reg.Dx_Nombre_Region AS 'Región', zon.Dx_Nombre_Zona AS 'Zona', tec.Dx_Nombre_General 
AS 'Tecnologia', prod2.Dx_Modelo_Producto, kprod.No_Cantidad AS 'No Equipos', 
kprod.Mt_Total AS 'Monto Equipos', kprod.No_Cantidad * 0 AS 'Costo CAyD', 
kprod.Mt_Total * 0 AS 'Descuento', kprod.Mt_Total AS 'Monto Solicitado' 
FROM K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN CAT_PRODUCTO AS prod2 WITH(NOLOCK) 
ON kprod.Cve_Producto = prod2.Cve_Producto 
INNER JOIN CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod2.Cve_Tecnologia = tec.Cve_Tecnologia 
LEFT OUTER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito k WITH (NOLOCK) 
ON kprod.No_Credito = k.No_Credito 
/**/

INNER JOIN CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente=Neg.IdCliente and k.IdNegocio = Neg.IdNegocio
INNER JOIN CLI_Cliente as Cli on
Cli.Id_Proveedor = Neg.Id_Proveedor and Cli.Id_Branch = Neg.Id_Branch and cli.IdCliente = neg.IdCliente
INNER JOIN DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and Dir.Id_Branch = Neg.Id_Branch and  Dir.IdCliente = Neg.IdCliente and Dir.IdNegocio = Neg.IdNegocio and  Dir.IdTipoDomicilio = 2
/*INNER JOIN CLI_Ref_Cliente as Ref on
ref.Id_Proveedor = Dir.Id_Proveedor and ref.Id_Branch = dir.Id_Branch and ref.IdNegocio = dir.IdNegocio and ref.IdCliente = dir.IdCliente*/
LEFT OUTER JOIN CAT_TIPO_SOCIEDAD AS soc WITH (NOLOCK)
ON cli.Cve_Tipo_Sociedad = soc.Cve_Tipo_Sociedad 

/**/
LEFT OUTER JOIN CAT_TIPO_INDUSTRIA AS giro WITH (NOLOCK) 
ON Neg.Cve_Tipo_Industria = giro.Cve_Tipo_Industria 
LEFT OUTER JOIN CAT_PROGRAMA AS prog WITH (NOLOCK) 
ON k.ID_Prog_Proy = prog.ID_Prog_Proy 
LEFT OUTER JOIN CAT_ESTATUS_CREDITO AS estatus WITH (NOLOCK) 
ON k.Cve_Estatus_Credito = estatus.Cve_Estatus_Credito 
LEFT OUTER JOIN CAT_ESTADO AS estado WITH (NOLOCK) 
ON Dir.Cve_Estado = estado.Cve_Estado 
LEFT OUTER JOIN ResponseData AS rd WITH (NOLOCK) 
ON k.RPU = rd.ServiceCode 
LEFT OUTER JOIN CAT_PROVEEDORBRANCH AS prov WITH (NOLOCK) 
ON k.Id_Proveedor = prov.Id_Branch 
LEFT OUTER JOIN CAT_ZONA AS zon WITH (NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
LEFT OUTER JOIN CAT_REGION AS reg WITH (NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE (k.ID_Intelisis IS NULL OR k.Afectacion_SICOM_DigitoVerOk IS NULL 
OR k.Afectacion_SIRCA_Digito IS NULL) AND tec.Cve_Tecnologia NOT IN (1,2,4) 
AND k.Tipo_Usuario = 'S_B' 
AND (k.Fecha_Ultmod BETWEEN @FechaI AND @FechaF) 
AND (estatus.Cve_Estatus_Credito = @Estatus OR @Estatus = '0') 
AND (rd.Rate = @Tarifa OR @Tarifa = 'TODAS') 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
GROUP BY k.No_Credito, soc.Dx_Tipo_Sociedad, Cli.Razon_Social, Neg.Nombre_Comercial, 
giro.Dx_Tipo_Industria, k.RPU, rd.Rate, k.Fecha_Ultmod, estatus.Dx_Estatus_Credito, 
prog.Dx_Nombre_Programa, prov.Dx_Razon_Social, prov.Dx_Nombre_Comercial, estado.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, tec.Dx_Nombre_General, prod2.Dx_Modelo_Producto, 
kprod.No_Cantidad, kprod.Mt_Total  ,Cli.Nombre,cli.Ap_Materno,cli.Ap_Paterno,cli.Cve_Tipo_Sociedad
,giro.DESCRIPCION_SCIAN



UNION ALL 
SELECT DISTINCT k.No_Credito AS 'Credito', soc.Dx_Tipo_Sociedad AS 'Tipo', 
case when cli.Cve_Tipo_Sociedad = 1 then Cli.Nombre + ' ' + ISNULL (Cli.Ap_Paterno, ' ') + ' ' +ISNULL(cli.Ap_Materno, '') 
else Cli.Razon_Social end AS 'Razón Social', Neg.Nombre_Comercial AS 'Nombre Comercial', 
giro.DESCRIPCION_SCIAN AS 'Giro', k.RPU AS 'RPU', MAX(rd.Rate) AS 'Tarifa', 
k.Fecha_Ultmod AS 'Fecha Credito', estatus.Dx_Estatus_Credito AS 'Estatus', 
prog.Dx_Nombre_Programa AS 'Programa', prov.Dx_Razon_Social AS 'Proveedor RS', 
prov.Dx_Nombre_Comercial AS 'Proveedor NC', estado.Dx_Nombre_Estado AS 'Estado', 
reg.Dx_Nombre_Region AS 'Región', zon.Dx_Nombre_Zona AS 'Zona', tec.Dx_Nombre_General 
AS 'Tecnologia', prod2.Dx_Modelo_Producto, kprod.No_Cantidad AS 'No Equipos', 
kprod.Mt_Total AS 'Monto Equipos', kprod.No_Cantidad * 0 AS 'Costo CAyD', 
kprod.Mt_Total * 0 AS 'Descuento', kprod.Mt_Total AS 'Monto Solicitado' 
FROM K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN CAT_PRODUCTO AS prod2 WITH(NOLOCK) 
ON kprod.Cve_Producto = prod2.Cve_Producto 
INNER JOIN CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod2.Cve_Tecnologia = tec.Cve_Tecnologia 
LEFT OUTER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito k WITH (NOLOCK) 
ON kprod.No_Credito = k.No_Credito 
/**/

INNER JOIN CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente=Neg.IdCliente and k.IdNegocio = Neg.IdNegocio
INNER JOIN CLI_Cliente as Cli on
Cli.Id_Proveedor = Neg.Id_Proveedor and Cli.Id_Branch = Neg.Id_Branch and cli.IdCliente = neg.IdCliente
INNER JOIN DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and Dir.Id_Branch = Neg.Id_Branch and  Dir.IdCliente = Neg.IdCliente and Dir.IdNegocio = Neg.IdNegocio and  Dir.IdTipoDomicilio = 2
/*INNER JOIN CLI_Ref_Cliente as Ref on
ref.Id_Proveedor = Dir.Id_Proveedor and ref.Id_Branch = dir.Id_Branch and ref.IdNegocio = dir.IdNegocio and ref.IdCliente = dir.IdCliente*/
LEFT OUTER JOIN CAT_TIPO_SOCIEDAD AS soc WITH (NOLOCK)
ON cli.Cve_Tipo_Sociedad = soc.Cve_Tipo_Sociedad 

/**/
LEFT OUTER JOIN CAT_TIPO_INDUSTRIA AS giro WITH (NOLOCK) 
ON Neg.Cve_Tipo_Industria = giro.Cve_Tipo_Industria 
LEFT OUTER JOIN CAT_PROGRAMA AS prog WITH (NOLOCK) 
ON k.ID_Prog_Proy = prog.ID_Prog_Proy 
LEFT OUTER JOIN CAT_ESTATUS_CREDITO AS estatus WITH (NOLOCK) 
ON k.Cve_Estatus_Credito = estatus.Cve_Estatus_Credito 
LEFT OUTER JOIN CAT_ESTADO AS estado WITH (NOLOCK) 
ON Dir.Cve_Estado = estado.Cve_Estado 
LEFT OUTER JOIN ResponseData AS rd WITH (NOLOCK) 
ON k.RPU = rd.ServiceCode 
LEFT OUTER JOIN CAT_PROVEEDORBRANCH AS prov WITH (NOLOCK) 
ON k.Id_Proveedor = prov.Id_Branch 
LEFT OUTER JOIN CAT_ZONA AS zon WITH (NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
LEFT OUTER JOIN CAT_REGION AS reg WITH (NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE (estatus.Cve_Estatus_Credito = 4 AND k.ID_Intelisis IS NOT NULL AND 
k.Afectacion_SICOM_DigitoVerOk IS NOT NULL AND k.Afectacion_SIRCA_Digito IS NOT NULL) 
AND tec.Cve_Tecnologia NOT IN (1,2,4) AND k.Tipo_Usuario = 'S_B' 
AND (k.Afectacion_SICOM_Fecha BETWEEN @FechaI AND @FechaF) 
AND (@Estatus = '11' OR @Estatus = '0') 
AND (rd.Rate = @Tarifa OR @Tarifa = 'TODAS') 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
GROUP BY k.No_Credito, soc.Dx_Tipo_Sociedad, Cli.Razon_Social, Neg.Nombre_Comercial, 
giro.Dx_Tipo_Industria, k.RPU, rd.Rate, k.Fecha_Ultmod, estatus.Dx_Estatus_Credito, 
prog.Dx_Nombre_Programa, prov.Dx_Razon_Social, prov.Dx_Nombre_Comercial, estado.Dx_Nombre_Estado, 
reg.Dx_Nombre_Region, zon.Dx_Nombre_Zona, tec.Dx_Nombre_General, prod2.Dx_Modelo_Producto, 
kprod.No_Cantidad, kprod.Mt_Total ,Cli.Nombre,cli.Ap_Materno,cli.Ap_Paterno,cli.Cve_Tipo_Sociedad, giro.DESCRIPCION_SCIAN
		