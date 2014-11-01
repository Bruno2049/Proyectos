declare @FechaI date = '01-05-2009';
declare @FechaF date = '01-05-2013';
declare @Estatus varchar(max) = 'Todos';
declare @Region varchar(max)= '0';
declare @Zone varchar(max) = '0';
declare @CD varchar(max) = 'TODOS';
declare @Tec varchar(max) = '0';
declare @Reg varchar(max) = '0';
declare @Prog varchar(max) = '0';
declare @Zona Varchar(max) = '0';
declare @UserID int = 7;
declare @FI date = '01-01-2000';
declare @FF date = '01-01-2013';



DECLARE @CD1 AS INT 
DECLARE @CD2 AS INT 

SET @CD1= (SELECT CASE WHEN [Id_Departamento]< 0 THEN NULL ELSE [Id_Departamento] END
	FROM [dbo].[US_Usuario] WITH(NOLOCK)
WHERE [Id_Rol] = 5 AND [Id_Usuario] = @UserID AND [Tipo_Usuario] = 'D_C') 

SET @CD2= (SELECT CASE WHEN [Id_Departamento]< 0 THEN NULL ELSE [Id_Departamento] END
	FROM [dbo].[US_Usuario] WITH(NOLOCK)
WHERE [Id_Rol] = 5 AND [Id_Usuario] = @UserID AND [Tipo_Usuario] = 'D_C_B') 

SELECT k.No_Credito AS 'Credito', ks.Id_Pre_Folio AS 'Pre-Boleta', ks.Id_Folio AS 'Boleta', 
prod.Dx_Tipo_Producto AS 'Producto', ks.Dx_Modelo_Producto AS 'Modelo', ks.Dx_Marca 
AS 'Marca', ks.No_Serial AS 'No_Serie', ks.Dx_Color AS 'Color', ks.Dt_Fecha_Recepcion 
AS 'Fecha_Recepcion_Eq', /*k.Dx_Razon_Social*/ Ref.Razon_Social AS 'RS_Beneficiario',/**/ Cli.Nombre_Comercial
AS 'Comerc_Beneficiario', k.Id_Proveedor AS 'ID_Proveedor', prov.Dx_Razon_Social AS 
'RS_Proveedor', prov.Dx_Nombre_Comercial AS 'Comerc_Proveedor', prov.Dx_RFC AS 'RFC', 
prov.Dx_Nombre_Repre AS 'Resp_Proveedor', prov.Dx_Telefono_Repre AS 'Tel_Resp_Prov', 
prov.Dx_Email_Repre AS 'Email_Resp_Prov', prov.Dx_Nombre_Repre_Legal AS 'Rep_Legal_Prov', 
/*k.Dt_Fecha_Autorizado*/ k.Fecha_Autorizado AS 'Fecha_Autorizado', k.ID_Intelisis AS 'Intelisis', cd.Id_Centro_Disp 
AS 'ID_CAYD', cd.Dx_Razon_Social AS 'RS_CAYD', cd.Dx_Nombre_Comercial AS 'Comerc_CAYD', 
k.Id_Prog_Proy AS 'ID_Programa', p.Dx_Nombre_Programa AS 'Programa' 
FROM /*dbo.K_CREDITO*/ dbo.CRE_Credito AS k WITH(NOLOCK) 
/*Inner join Esteban*/
INNER JOIN dbo.CLI_Negocio As Cli WITH(NOLOCK)
ON k.IdCliente = Cli.IdCliente

INNER JOIN dbo.CLI_Ref_Cliente AS Ref WITH(NOLOCK)
on k.IdCliente = Ref.IdCliente
/**/  
INNER JOIN dbo.CAT_PROGRAMA AS p WITH(NOLOCK) 
ON k.Id_Prog_Proy = p.Id_Prog_Proy 
INNER JOIN dbo.CAT_PROVEEDOR AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Proveedor 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS ks WITH(NOLOCK) 
ON k.No_Credito = ks.No_Credito 
INNER JOIN dbo.CAT_CENTRO_DISP AS cd WITH(NOLOCK) 
ON ks.Id_Centro_Disp = cd.Id_Centro_Disp 
INNER JOIN dbo.CAT_TIPO_PRODUCTO AS prod WITH(NOLOCK) 
ON ks.Dx_Tipo_Producto = prod.Ft_Tipo_Producto 
WHERE (k.Tipo_Usuario = 'S' AND ks.Fg_Tipo_Centro_Disp = 'M') 
AND cd.Id_Centro_Disp = @CD1 
AND k.Cve_Estatus_Credito = 4 
AND (k.ID_Intelisis IS NOT NULL OR k.ID_Intelisis <> '') 
AND (k.Afectacion_SICOM_Fecha IS NOT NULL AND k.Afectacion_SICOM_DigitoVerOk IS NOT NULL) 
AND (k.Afectacion_SICOM_Fecha BETWEEN @FI AND @FF) 
AND (p.Id_Prog_Proy = @Prog OR @Prog = 0) 
UNION ALL 
SELECT k.No_Credito, ks.Id_Pre_Folio, ks.Id_Folio, prod.Dx_Tipo_Producto, ks.Dx_Modelo_Producto, 
ks.Dx_Marca, ks.No_Serial, ks.Dx_Color, ks.Dt_Fecha_Recepcion, /*k.Dx_Razon_Social*/ ref.Razon_Social, 
/*k.Dx_Nombre_Comercial*/ Cli.Nombre_Comercial, k.Id_Proveedor, prov.Dx_Razon_Social, prov.Dx_Nombre_Comercial, 
prov.Dx_RFC, prov.Dx_Nombre_Repre, prov.Dx_Telefono_Repre, prov.Dx_Email_Repre, 
prov.Dx_Nombre_Repre_Legal, /*k.Dt_Fecha_Autorizado*/k.Fecha_Autorizado, k.ID_Intelisis, cd.Id_Centro_Disp, 
cd.Dx_Razon_Social, cd.Dx_Nombre_Comercial, k.Id_Prog_Proy, p.Dx_Nombre_Programa 
FROM /*dbo.K_CREDITO*/ dbo.CRE_Credito AS k WITH(NOLOCK) 
/**/
INNER JOIN dbo.CLI_Negocio As Cli WITH(NOLOCK)
ON k.IdCliente = Cli.IdCliente

INNER JOIN dbo.CLI_Ref_Cliente AS Ref WITH(NOLOCK)
on k.IdCliente = Ref.IdCliente
/**/
INNER JOIN dbo.CAT_PROGRAMA AS p WITH(NOLOCK) 
ON k.Id_Prog_Proy = p.Id_Prog_Proy 
INNER JOIN dbo.CAT_PROVEEDORBRANCH AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Branch 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS ks WITH(NOLOCK) 
ON k.No_Credito = ks.No_Credito 
INNER JOIN dbo.CAT_CENTRO_DISP AS cd WITH(NOLOCK) 
ON ks.Id_Centro_Disp = cd.Id_Centro_Disp 
INNER JOIN dbo.CAT_TIPO_PRODUCTO AS prod WITH(NOLOCK) 
ON ks.Dx_Tipo_Producto = prod.Ft_Tipo_Producto 
WHERE (k.Tipo_Usuario = 'S_B' AND ks.Fg_Tipo_Centro_Disp = 'M') 
AND cd.Id_Centro_Disp = @CD1 
AND k.Cve_Estatus_Credito = 4 
AND (k.ID_Intelisis IS NOT NULL OR k.ID_Intelisis <> '') 
AND (k.Afectacion_SICOM_Fecha IS NOT NULL AND k.Afectacion_SICOM_DigitoVerOk IS NOT NULL) 
AND (k.Afectacion_SICOM_Fecha BETWEEN @FI AND @FF) 
AND (p.Id_Prog_Proy = @Prog OR @Prog = 0) 
UNION ALL 
SELECT k.No_Credito, ks.Id_Pre_Folio, ks.Id_Folio, prod.Dx_Tipo_Producto, ks.Dx_Modelo_Producto, 
ks.Dx_Marca, ks.No_Serial, ks.Dx_Color, ks.Dt_Fecha_Recepcion, /*k.Dx_Razon_Social*/ ref.Razon_Social, 
/*k.Dx_Nombre_Comercial*/ cli.Nombre_Comercial, k.Id_Proveedor, prov.Dx_Razon_Social, prov.Dx_Nombre_Comercial, 
prov.Dx_RFC, prov.Dx_Nombre_Repre, prov.Dx_Telefono_Repre, prov.Dx_Email_Repre, 
prov.Dx_Nombre_Repre_Legal, /*k.Dt_Fecha_Autorizado*/ k.Fecha_Autorizado, k.ID_Intelisis, cd.Id_Centro_Disp, 
cd.Dx_Razon_Social, cd.Dx_Nombre_Comercial, k.Id_Prog_Proy, p.Dx_Nombre_Programa 
FROM /*dbo.K_CREDITO*/ dbo.CRE_Credito AS k WITH(NOLOCK) 
/**/
INNER JOIN dbo.CLI_Negocio As Cli WITH(NOLOCK)
ON k.IdCliente = Cli.IdCliente

INNER JOIN dbo.CLI_Ref_Cliente AS Ref WITH(NOLOCK)
on k.IdCliente = Ref.IdCliente
/**/
INNER JOIN dbo.CAT_PROGRAMA AS p WITH(NOLOCK) 
ON k.Id_Prog_Proy = p.Id_Prog_Proy 
INNER JOIN dbo.CAT_PROVEEDOR AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Proveedor  
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS ks WITH(NOLOCK) 
ON k.No_Credito = ks.No_Credito 
INNER JOIN dbo.CAT_CENTRO_DISP_SUCURSAL AS cd WITH(NOLOCK) 
ON ks.Id_Centro_Disp = cd.Id_Centro_Disp_Sucursal 
INNER JOIN dbo.CAT_TIPO_PRODUCTO AS prod WITH(NOLOCK) 
ON ks.Dx_Tipo_Producto = prod.Ft_Tipo_Producto 
WHERE (k.Tipo_Usuario = 'S' AND ks.Fg_Tipo_Centro_Disp = 'B') 
AND cd.Id_Centro_Disp_Sucursal = @CD2 
AND k.Cve_Estatus_Credito = 4 
AND (k.ID_Intelisis IS NOT NULL OR k.ID_Intelisis <> '') 
AND (k.Afectacion_SICOM_Fecha IS NOT NULL AND k.Afectacion_SICOM_DigitoVerOk IS NOT NULL) 
AND (k.Afectacion_SICOM_Fecha BETWEEN @FI AND @FF) 
AND (p.Id_Prog_Proy = @Prog OR @Prog = 0) 
UNION ALL 
SELECT k.No_Credito, ks.Id_Pre_Folio, ks.Id_Folio, prod.Dx_Tipo_Producto, ks.Dx_Modelo_Producto, 
ks.Dx_Marca, ks.No_Serial, ks.Dx_Color, ks.Dt_Fecha_Recepcion, /*k.Dx_Razon_Social*/ref.Razon_Social, 
/*k.Dx_Nombre_Comercial*/cli.Nombre_Comercial, k.Id_Proveedor, prov.Dx_Razon_Social, prov.Dx_Nombre_Comercial, 
prov.Dx_RFC, prov.Dx_Nombre_Repre, prov.Dx_Telefono_Repre, prov.Dx_Email_Repre, 
prov.Dx_Nombre_Repre_Legal, /*k.Dt_Fecha_Autorizado*/ k.Fecha_Autorizado, k.ID_Intelisis, cd.Id_Centro_Disp, 
cd.Dx_Razon_Social, cd.Dx_Nombre_Comercial, k.Id_Prog_Proy, p.Dx_Nombre_Programa 
FROM /*dbo.K_CREDITO*/ dbo.CRE_Credito AS k WITH(NOLOCK) 
/**/
INNER JOIN dbo.CLI_Negocio As Cli WITH(NOLOCK)
ON k.IdCliente = Cli.IdCliente

INNER JOIN dbo.CLI_Ref_Cliente AS Ref WITH(NOLOCK)
on k.IdCliente = Ref.IdCliente
/**/
INNER JOIN dbo.CAT_PROGRAMA AS p WITH(NOLOCK) 
ON k.Id_Prog_Proy = p.Id_Prog_Proy 
INNER JOIN dbo.CAT_PROVEEDORBRANCH AS prov WITH(NOLOCK) 
ON k.Id_Proveedor = prov.Id_Branch 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS ks WITH(NOLOCK) 
ON k.No_Credito = ks.No_Credito 
INNER JOIN dbo.CAT_CENTRO_DISP_SUCURSAL AS cd WITH(NOLOCK) 
ON ks.Id_Centro_Disp = cd.Id_Centro_Disp_Sucursal 
INNER JOIN dbo.CAT_TIPO_PRODUCTO AS prod WITH(NOLOCK) 
ON ks.Dx_Tipo_Producto = prod.Ft_Tipo_Producto 
WHERE (k.Tipo_Usuario = 'S_B' AND ks.Fg_Tipo_Centro_Disp = 'B') 
AND cd.Id_Centro_Disp_Sucursal = @CD2 
AND k.Cve_Estatus_Credito = 4 
AND (k.ID_Intelisis IS NOT NULL OR k.ID_Intelisis <> '') 
AND (k.Afectacion_SICOM_Fecha IS NOT NULL AND k.Afectacion_SICOM_DigitoVerOk IS NOT NULL) 
AND (k.Afectacion_SICOM_Fecha BETWEEN @FI AND @FF) 
AND (p.Id_Prog_Proy = @Prog OR @Prog = 0) 
ORDER BY k.No_Credito, ks.Id_Pre_Folio, ks.Id_Folio