declare @FechaI date = '2010-06-12 00:00:00.000';
declare @FechaF date = '2014-06-23 00:00:00.000';
declare @Estatus varchar(max) = 'Todos';
declare @Region varchar(max)= '0';
declare @Zone varchar(max) = '0';
declare @CD varchar(max) = 'TODOS';
declare @Tec varchar(max) = '0';
declare @Reg varchar(max) = '0';
declare @Prog varchar(max) = '0';


SELECT k.No_Credito AS 'Credito', edo.Dx_Nombre_Estado AS 'Estado', 
zon.Dx_Nombre_Zona AS 'Zona', reg.Dx_Nombre_Region AS 'Region', 
'Refrigeración Comercial' AS 'Tecno', kprod.No_Cantidad AS 'No_Equipos', 
(kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10) AS 'Descuento', 
((kprod.Mt_Precio_Unitario * kprod.No_Cantidad) - 
(kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10) + 
(kprod.No_Cantidad * 400))  AS 'Monto_Financiar', 
(kprod.No_Cantidad * 400) AS 'Descuento2'
--Select *
FROM dbo.K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN dbo.CAT_PRODUCTO AS prod WITH(NOLOCK) 
ON kprod.Cve_Producto = prod.Cve_Producto 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS k WITH(NOLOCK) 
/**/
ON kprod.No_Credito = k.No_Credito 
INNER JOIN dbo.CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente = neg.IdCliente and k.IdNegocio = neg.IdNegocio
INNER JOIN dbo.DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and dir.Id_Branch = Neg.Id_Branch and Dir.IdCliente = neg.IdCliente and Dir.IdNegocio = neg.IdNegocio and Dir.IdTipoDomicilio = 2
INNER JOIN dbo.CAT_ESTADO as edo on 
edo.Cve_Estado = Dir.Cve_Estado
/**/
INNER JOIN dbo.CAT_PROVEEDOR AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Proveedor 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE prod.Cve_Tecnologia = 1 
AND k.Tipo_Usuario = 'S' AND k.ID_Intelisis IS NOT NULL 
AND k.Afectacion_SICOM_DigitoVerOk IS NOT NULL 
--AND k.Afectacion_SIRCA_Digito IS NOT NULL --Sin registros
AND k.Afectacion_SICOM_fecha IS NOT NULL
AND k.Afectacion_SICOM_fecha BETWEEN @FechaI AND @FechaF --sin Registros
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zone OR @Zone = '0') 
GROUP BY k.No_Credito, edo.Dx_Nombre_Estado, 
zon.Dx_Nombre_Zona, reg.Dx_Nombre_Region, kprod.No_Cantidad, 
kprod.Mt_Precio_Unitario 

UNION ALL 
SELECT k.No_Credito, edo.Dx_Nombre_Estado, zon.Dx_Nombre_Zona, 
reg.Dx_Nombre_Region, 'Refrigeración Comercial' AS 'Tecno', 
kprod.No_Cantidad, (kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10), 
((kprod.Mt_Precio_Unitario * kprod.No_Cantidad) - 
(kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10) + 
(kprod.No_Cantidad * 400)), (kprod.No_Cantidad * 400)   
FROM dbo.K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN dbo.CAT_PRODUCTO AS prod WITH(NOLOCK) 
ON kprod.Cve_Producto = prod.Cve_Producto 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS k WITH(NOLOCK) 
ON kprod.No_Credito = k.No_Credito /**/
INNER JOIN dbo.CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente = neg.IdCliente and k.IdNegocio = neg.IdNegocio
INNER JOIN dbo.DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and dir.Id_Branch = Neg.Id_Branch and Dir.IdCliente = neg.IdCliente and Dir.IdNegocio = neg.IdNegocio and Dir.IdTipoDomicilio = 2
INNER JOIN dbo.CAT_ESTADO as edo on 
edo.Cve_Estado = Dir.Cve_Estado
/**/
INNER JOIN dbo.CAT_PROVEEDORBRANCH AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Branch 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE prod.Cve_Tecnologia = 1 
AND k.Tipo_Usuario = 'S_B' AND k.ID_Intelisis IS NOT NULL 
AND k.Afectacion_SICOM_DigitoVerOk IS NOT NULL 
--AND k.Afectacion_SIRCA_Digito IS NOT NULL --Sin registros
AND k.Afectacion_SICOM_fecha IS NOT NULL
AND k.Afectacion_SICOM_fecha BETWEEN @FechaI AND @FechaF 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zone OR @Zone = '0') 
GROUP BY k.No_Credito, edo.Dx_Nombre_Estado, 
zon.Dx_Nombre_Zona, reg.Dx_Nombre_Region, kprod.No_Cantidad, 
kprod.Mt_Precio_Unitario 


UNION ALL 
SELECT k.No_Credito, edo.Dx_Nombre_Estado, zon.Dx_Nombre_Zona, 
reg.Dx_Nombre_Region, 'Aire Acondicionado' AS 'Tecno', 
kprod.No_Cantidad, (kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10), 
((kprod.Mt_Precio_Unitario * kprod.No_Cantidad) - 
(kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10) + 
(kprod.No_Cantidad * 400)), (kprod.No_Cantidad * 400)   
FROM dbo.K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN dbo.CAT_PRODUCTO AS prod WITH(NOLOCK) 
ON kprod.Cve_Producto = prod.Cve_Producto 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS k WITH(NOLOCK) 
ON kprod.No_Credito = k.No_Credito /**/

INNER JOIN dbo.CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente = neg.IdCliente and k.IdNegocio = neg.IdNegocio
INNER JOIN dbo.DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and dir.Id_Branch = Neg.Id_Branch and Dir.IdCliente = neg.IdCliente and Dir.IdNegocio = neg.IdNegocio and Dir.IdTipoDomicilio = 2
INNER JOIN dbo.CAT_ESTADO as edo on 
edo.Cve_Estado = Dir.Cve_Estado
/**/
INNER JOIN dbo.CAT_PROVEEDOR AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Proveedor 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE prod.Cve_Tecnologia = 2 
AND k.Tipo_Usuario = 'S' AND k.ID_Intelisis IS NOT NULL 
AND k.Afectacion_SICOM_DigitoVerOk IS NOT NULL 
--AND k.Afectacion_SIRCA_Digito IS NOT NULL --Sin registros
AND k.Afectacion_SICOM_fecha IS NOT NULL
AND k.Afectacion_SICOM_fecha BETWEEN @FechaI AND @FechaF 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zone OR @Zone = '0') 
GROUP BY k.No_Credito, edo.Dx_Nombre_Estado, 
zon.Dx_Nombre_Zona, reg.Dx_Nombre_Region, kprod.No_Cantidad, 
kprod.Mt_Precio_Unitario 
UNION ALL 


SELECT k.No_Credito, edo.Dx_Nombre_Estado, zon.Dx_Nombre_Zona, 
reg.Dx_Nombre_Region, 'Aire Acondicionado' AS 'Tecno', 
kprod.No_Cantidad, (kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10), 
((kprod.Mt_Precio_Unitario * kprod.No_Cantidad) - 
(kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10) + 
(kprod.No_Cantidad * 400)), (kprod.No_Cantidad * 400)   
FROM dbo.K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN dbo.CAT_PRODUCTO AS prod WITH(NOLOCK) 
ON kprod.Cve_Producto = prod.Cve_Producto 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS k WITH(NOLOCK) 
ON kprod.No_Credito = k.No_Credito /**/

INNER JOIN dbo.CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente = neg.IdCliente and k.IdNegocio = neg.IdNegocio
INNER JOIN dbo.DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and dir.Id_Branch = Neg.Id_Branch and Dir.IdCliente = neg.IdCliente and Dir.IdNegocio = neg.IdNegocio and Dir.IdTipoDomicilio = 2
INNER JOIN dbo.CAT_ESTADO as edo on 
edo.Cve_Estado = Dir.Cve_Estado
 /**/
INNER JOIN dbo.CAT_PROVEEDORBRANCH AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Branch 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE prod.Cve_Tecnologia = 2 
AND k.Tipo_Usuario = 'S_B' AND k.ID_Intelisis IS NOT NULL 
AND k.Afectacion_SICOM_DigitoVerOk IS NOT NULL 
--AND k.Afectacion_SIRCA_Digito IS NOT NULL --Sin registros
AND k.Afectacion_SICOM_fecha IS NOT NULL
AND k.Afectacion_SICOM_fecha BETWEEN @FechaI AND @FechaF 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zone OR @Zone = '0') 
GROUP BY k.No_Credito, edo.Dx_Nombre_Estado, 
zon.Dx_Nombre_Zona, reg.Dx_Nombre_Region, kprod.No_Cantidad, 
kprod.Mt_Precio_Unitario 
UNION ALL 

SELECT k.No_Credito, edo.Dx_Nombre_Estado, zon.Dx_Nombre_Zona, 
reg.Dx_Nombre_Region, 'Motores Eléctricos' AS 'Tecno', 
kprod.No_Cantidad, (kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10), 
((kprod.Mt_Precio_Unitario * kprod.No_Cantidad) - 
(kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10) + 
(kprod.No_Cantidad * 400)), (kprod.No_Cantidad * 400) 
FROM dbo.K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN dbo.CAT_PRODUCTO AS prod WITH(NOLOCK) 
ON kprod.Cve_Producto = prod.Cve_Producto 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS k WITH(NOLOCK) 
ON kprod.No_Credito = k.No_Credito /**/
INNER JOIN dbo.CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente = neg.IdCliente and k.IdNegocio = neg.IdNegocio
INNER JOIN dbo.DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and dir.Id_Branch = Neg.Id_Branch and Dir.IdCliente = neg.IdCliente and Dir.IdNegocio = neg.IdNegocio and Dir.IdTipoDomicilio = 2
INNER JOIN dbo.CAT_ESTADO as edo on 
edo.Cve_Estado = Dir.Cve_Estado
/**/
INNER JOIN dbo.CAT_PROVEEDOR AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Proveedor 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE prod.Cve_Tecnologia = 4 
AND k.Tipo_Usuario = 'S' AND k.ID_Intelisis IS NOT NULL 
AND k.Afectacion_SICOM_DigitoVerOk IS NOT NULL 
--AND k.Afectacion_SIRCA_Digito IS NOT NULL --Sin registros
AND k.Afectacion_SICOM_fecha IS NOT NULL
AND k.Afectacion_SICOM_fecha BETWEEN @FechaI AND @FechaF 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zone OR @Zone = '0') 
GROUP BY k.No_Credito, edo.Dx_Nombre_Estado, 
zon.Dx_Nombre_Zona, reg.Dx_Nombre_Region, kprod.No_Cantidad, 
kprod.Mt_Precio_Unitario 
UNION ALL 


SELECT k.No_Credito, edo.Dx_Nombre_Estado, zon.Dx_Nombre_Zona, 
reg.Dx_Nombre_Region, 'Motores Eléctricos' AS 'Tecno', 
kprod.No_Cantidad, (kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10), 
((kprod.Mt_Precio_Unitario * kprod.No_Cantidad) - 
(kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10) + 
(kprod.No_Cantidad * 400)), (kprod.No_Cantidad * 400) 
FROM dbo.K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN dbo.CAT_PRODUCTO AS prod WITH(NOLOCK) 
ON kprod.Cve_Producto = prod.Cve_Producto 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS k WITH(NOLOCK)
ON kprod.No_Credito = k.No_Credito  /**/
INNER JOIN dbo.CLI_Negocio as Neg on
k.Id_Proveedor = Neg.Id_Proveedor and k.Id_Branch = Neg.Id_Branch and k.IdCliente = neg.IdCliente and k.IdNegocio = neg.IdNegocio
INNER JOIN dbo.DIR_Direcciones as Dir on
Dir.Id_Proveedor = Neg.Id_Proveedor and dir.Id_Branch = Neg.Id_Branch and Dir.IdCliente = neg.IdCliente and Dir.IdNegocio = neg.IdNegocio and Dir.IdTipoDomicilio = 2
INNER JOIN dbo.CAT_ESTADO as edo on 
edo.Cve_Estado = Dir.Cve_Estado
/**/
INNER JOIN dbo.CAT_PROVEEDORBRANCH AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Branch 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE prod.Cve_Tecnologia = 4 
AND k.Tipo_Usuario = 'S_B' AND k.ID_Intelisis IS NOT NULL 
AND k.Afectacion_SICOM_DigitoVerOk IS NOT NULL 
--AND k.Afectacion_SIRCA_Digito IS NOT NULL --Sin registros
AND k.Afectacion_SICOM_fecha IS NOT NULL
AND k.Afectacion_SICOM_fecha BETWEEN @FechaI AND @FechaF 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zone OR @Zone = '0') 
GROUP BY k.No_Credito, edo.Dx_Nombre_Estado, 
zon.Dx_Nombre_Zona, reg.Dx_Nombre_Region, kprod.No_Cantidad, 
kprod.Mt_Precio_Unitario




------------------------------------ Adquisicion---------------------------------------------------

--Select k.ID_Intelisis
SELECT k.No_Credito AS 'Credito', edo.Dx_Nombre_Estado AS 'Estado', 
zon.Dx_Nombre_Zona AS 'Zona', reg.Dx_Nombre_Region AS 'Region', 
'Iluminación' AS 'Tecno', kprod.No_Cantidad AS 'No_Equipos', 
((kprod.Mt_Precio_Unitario * kprod.No_Cantidad) - 
(kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10) + 
(kprod.No_Cantidad * 400))  AS 'Monto_Financiar' 
FROM dbo.K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK)  
INNER JOIN dbo.CAT_PRODUCTO AS prod WITH(NOLOCK) 
ON kprod.Cve_Producto = prod.Cve_Producto 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS k WITH(NOLOCK) 
ON kprod.No_Credito = k.No_Credito 
INNER JOIN dbo.CLI_Negocio as Neg on
k.Id_Proveedor = neg.Id_Proveedor and k.Id_Branch = neg.Id_Branch and k.IdCliente = neg.IdCliente and k.IdNegocio = neg.IdNegocio
INNER JOIN dbo.DIR_Direcciones as Dir on
Neg.Id_Proveedor  = Dir.Id_Proveedor and neg.Id_Branch = dir.Id_Branch and neg.IdCliente = dir.IdCliente and neg.IdNegocio = dir.IdNegocio and dir.IdTipoDomicilio = 2
INNER JOIN dbo.CAT_ESTADO AS edo WITH(NOLOCK) 
ON dir.Cve_Estado = edo.Cve_Estado 
/**/
INNER JOIN dbo.CAT_PROVEEDOR AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Proveedor 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE prod.Cve_Tecnologia IN (3,6) AND 
k.Tipo_Usuario = 'S' AND k.ID_Intelisis IS NOT NULL 
AND k.Afectacion_SIRCA_Digito IS NOT NULL 
AND k.Afectacion_SICOM_fecha IS NOT NULL
AND k.Afectacion_SICOM_fecha BETWEEN @FechaI AND @FechaF
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zone OR @Zone = '0') 
GROUP BY k.No_Credito, edo.Dx_Nombre_Estado, 
zon.Dx_Nombre_Zona, reg.Dx_Nombre_Region, kprod.No_Cantidad, 
kprod.Mt_Precio_Unitario 
UNION ALL 


SELECT k.No_Credito, edo.Dx_Nombre_Estado, zon.Dx_Nombre_Zona, 
reg.Dx_Nombre_Region, 'Iluminación' AS 'Tecno', 
kprod.No_Cantidad, ((kprod.Mt_Precio_Unitario * kprod.No_Cantidad) - 
(kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10) + 
(kprod.No_Cantidad * 400))  
FROM dbo.K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN dbo.CAT_PRODUCTO AS prod WITH(NOLOCK) 
ON kprod.Cve_Producto = prod.Cve_Producto 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS k WITH(NOLOCK) 
ON kprod.No_Credito = k.No_Credito 
INNER JOIN dbo.CLI_Negocio as Neg on
k.Id_Proveedor = neg.Id_Proveedor and k.Id_Branch = neg.Id_Branch and k.IdCliente = neg.IdCliente and k.IdNegocio = neg.IdNegocio
INNER JOIN dbo.DIR_Direcciones as Dir on
Neg.Id_Proveedor  = Dir.Id_Proveedor and neg.Id_Branch = dir.Id_Branch and neg.IdCliente = dir.IdCliente and neg.IdNegocio = dir.IdNegocio and dir.IdTipoDomicilio = 2
INNER JOIN dbo.CAT_ESTADO AS edo WITH(NOLOCK) 
ON dir.Cve_Estado = edo.Cve_Estado 
/**/
INNER JOIN dbo.CAT_PROVEEDORBRANCH AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Branch 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE prod.Cve_Tecnologia IN (3,6) AND 
k.Tipo_Usuario = 'S_B' AND k.ID_Intelisis IS NOT NULL 
AND k.Afectacion_SIRCA_Digito IS NOT NULL 
AND k.Afectacion_SICOM_fecha BETWEEN @FechaI AND @FechaF 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zone OR @Zone = '0') 
GROUP BY k.No_Credito, edo.Dx_Nombre_Estado, 
zon.Dx_Nombre_Zona, reg.Dx_Nombre_Region, kprod.No_Cantidad, 
kprod.Mt_Precio_Unitario 
UNION ALL 
SELECT k.No_Credito, edo.Dx_Nombre_Estado, zon.Dx_Nombre_Zona, 
reg.Dx_Nombre_Region, 'Subestaciones Eléctricas' AS 'Tecno', 
kprod.No_Cantidad, ((kprod.Mt_Precio_Unitario * kprod.No_Cantidad) - 
(kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10) + 
(kprod.No_Cantidad * 400))  
FROM dbo.K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN dbo.CAT_PRODUCTO AS prod WITH(NOLOCK) 
ON kprod.Cve_Producto = prod.Cve_Producto 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS k WITH(NOLOCK) 
ON kprod.No_Credito = k.No_Credito 
INNER JOIN dbo.CLI_Negocio as Neg on
k.Id_Proveedor = neg.Id_Proveedor and k.Id_Branch = neg.Id_Branch and k.IdCliente = neg.IdCliente and k.IdNegocio = neg.IdNegocio
INNER JOIN dbo.DIR_Direcciones as Dir on
Neg.Id_Proveedor  = Dir.Id_Proveedor and neg.Id_Branch = dir.Id_Branch and neg.IdCliente = dir.IdCliente and neg.IdNegocio = dir.IdNegocio and dir.IdTipoDomicilio = 2
INNER JOIN dbo.CAT_ESTADO AS edo WITH(NOLOCK) 
ON dir.Cve_Estado = edo.Cve_Estado 
/**/
INNER JOIN dbo.CAT_PROVEEDOR AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Proveedor 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE prod.Cve_Tecnologia = 5 AND 
k.Tipo_Usuario = 'S' AND k.ID_Intelisis IS NOT NULL 
--AND k.Afectacion_SICOM_DigitoVerOk IS NOT NULL 
AND k.Afectacion_SIRCA_Digito IS NOT NULL 
AND k.Afectacion_SICOM_fecha IS NOT NULL
AND k.Afectacion_SICOM_fecha BETWEEN @FechaI AND @FechaF 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zone OR @Zone = '0') 
GROUP BY k.No_Credito, edo.Dx_Nombre_Estado, 
zon.Dx_Nombre_Zona, reg.Dx_Nombre_Region, kprod.No_Cantidad, 
kprod.Mt_Precio_Unitario 
UNION ALL 
SELECT k.No_Credito, edo.Dx_Nombre_Estado, zon.Dx_Nombre_Zona, 
reg.Dx_Nombre_Region, 'Subestaciones Eléctricas' AS 'Tecno', 
kprod.No_Cantidad, ((kprod.Mt_Precio_Unitario * kprod.No_Cantidad) - 
(kprod.Mt_Precio_Unitario * kprod.No_Cantidad * .10) + 
(kprod.No_Cantidad * 400))  
FROM dbo.K_CREDITO_PRODUCTO AS kprod WITH(NOLOCK) 
INNER JOIN dbo.CAT_PRODUCTO AS prod WITH(NOLOCK) 
ON kprod.Cve_Producto = prod.Cve_Producto 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) 
ON prod.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN dbo.K_CREDITO AS k WITH(NOLOCK) 
ON kprod.No_Credito = k.No_Credito 
INNER JOIN dbo.CAT_ESTADO AS edo WITH(NOLOCK) 
ON k.Cve_Estado_Fisc = edo.Cve_Estado 
INNER JOIN dbo.CAT_PROVEEDORBRANCH AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Branch 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) 
ON prov.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) 
ON zon.Cve_Region = reg.Cve_Region 
WHERE prod.Cve_Tecnologia = 5 AND 
k.Tipo_Usuario = 'S_B' AND k.ID_Intelisis IS NOT NULL 
--AND k.Afectacion_SICOM_DigitoVerOk IS NOT NULL 
AND k.Afectacion_SIRCA_Digito IS NOT NULL 
AND k.Afectacion_SICOM_fecha IS NOT NULL
AND k.Afectacion_SICOM_fecha BETWEEN @FechaI AND @FechaF 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (reg.Cve_Region = @Region OR @Region = '0') 
AND (zon.Cve_Zona = @Zone OR @Zone = '0') 
GROUP BY k.No_Credito, edo.Dx_Nombre_Estado, 
zon.Dx_Nombre_Zona, reg.Dx_Nombre_Region, kprod.No_Cantidad, 
kprod.Mt_Precio_Unitario