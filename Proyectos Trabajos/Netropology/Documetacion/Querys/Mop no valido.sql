use PAEEEM_PROD;
SELECT DISTINCT 
                      c.No_Credito, c.Id_Proveedor, c.Cve_Estatus_Credito, UPPER(LTRIM(RTRIM(ISNULL(cl.Razon_Social, '') + ISNULL(cl.Nombre, '') + ' ' + ISNULL(cl.Ap_Paterno, '') 
                      + ' ' + ISNULL(cl.Ap_Materno, '')))) AS Dx_Razon_Social, CASE WHEN ISNULL(rc.Nombre, '') <> '' THEN UPPER(ISNULL(rc.Nombre, '') + ' ' + ISNULL(rc.Ap_Paterno, '') 
                      + ' ' + ISNULL(rc.Ap_Materno, '')) ELSE UPPER(LTRIM(RTRIM(ISNULL(cl.Razon_Social, '') + ISNULL(cl.Nombre, '') + ' ' + ISNULL(cl.Ap_Paterno, '') 
                      + ' ' + ISNULL(cl.Ap_Materno, '')))) END AS Dx_Nombre_Repre_Legal, d.Telefono_Oficina AS Dx_Tel_Fisc, c.Monto_Solicitado AS Mt_Monto_Solicitado, 
                      e.Dx_Estatus_Credito, c.Fecha_Pendiente AS Dt_Fecha_Pendiente, c.Tipo_Usuario, c.No_MOP
FROM         dbo.CRE_Credito AS c INNER JOIN
                      dbo.CLI_Negocio AS ng ON ng.Id_Proveedor = c.Id_Proveedor AND ng.Id_Branch = c.Id_Branch AND c.IdCliente = ng.IdCliente AND 
                      ng.IdNegocio = c.IdNegocio INNER JOIN
                      dbo.CLI_Cliente AS cl ON c.IdCliente = cl.IdCliente AND cl.Id_Branch = ng.Id_Branch AND cl.Id_Proveedor = ng.Id_Proveedor LEFT OUTER JOIN
                      dbo.DIR_Direcciones AS d ON cl.IdCliente = d.IdCliente AND d.IdTipoDomicilio = 2 AND d.Id_Branch = ng.Id_Branch AND d.Id_Proveedor = ng.Id_Proveedor AND 
                      d.IdNegocio = ng.IdNegocio LEFT OUTER JOIN
                      dbo.CAT_ESTATUS_CREDITO AS e ON c.Cve_Estatus_Credito = e.Cve_Estatus_Credito LEFT OUTER JOIN
                      dbo.CLI_Ref_Cliente AS rc ON rc.IdCliente = cl.IdCliente AND rc.IdTipoReferencia = 1 AND rc.Id_Branch = ng.Id_Branch AND rc.Id_Proveedor = ng.Id_Proveedor AND 
                      rc.IdNegocio = ng.IdNegocio

Where (c.Id_Proveedor = 166 and Tipo_Usuario = 'S' OR (c.Id_Proveedor in (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Id_Proveedor = 166/*76*/) AND Tipo_Usuario = 'S_B'))

SELECT No_Credito, Id_Proveedor, No_MOP, Tipo_Usuario  FROM CRE_Credito WHERE No_Credito in ('PAEEEMDU07E03790','PAEEEMDU07E03792') ;

use paeeem_produccion19_06

SELECT * FROM US_USUARIO WHERE Nombre_Usuario ='MANRIVER'

SELECT     dbo.K_CREDITO.No_Credito, dbo.K_CREDITO.Id_Proveedor, dbo.K_CREDITO.Cve_Estatus_Credito, dbo.K_CREDITO.Dx_Razon_Social, 
                      dbo.K_CREDITO.Dx_Nombre_Repre_Legal, dbo.K_CREDITO.Dx_Tel_Fisc, dbo.K_CREDITO.Mt_Monto_Solicitado, dbo.CAT_ESTATUS_CREDITO.Dx_Estatus_Credito, 
                      dbo.K_CREDITO.Dt_Fecha_Pendiente, dbo.K_CREDITO.Tipo_Usuario, dbo.CAT_AUXILIAR.No_MOP
FROM         dbo.K_CREDITO INNER JOIN
                      dbo.CAT_ESTATUS_CREDITO ON dbo.K_CREDITO.Cve_Estatus_Credito = dbo.CAT_ESTATUS_CREDITO.Cve_Estatus_Credito INNER JOIN
                      dbo.CAT_AUXILIAR ON dbo.K_CREDITO.No_Credito = dbo.CAT_AUXILIAR.No_Credito
                      
                      Where (dbo.K_CREDITO.Id_Proveedor = 166 and Tipo_Usuario = 'S' OR (dbo.K_CREDITO.Id_Proveedor in (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Id_Proveedor = 166) AND Tipo_Usuario = 'S_B'))