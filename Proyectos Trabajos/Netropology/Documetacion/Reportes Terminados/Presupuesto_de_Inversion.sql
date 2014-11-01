--PAEEEMDA08A11165
--PAEEEMDA01A10992
declare @FechaI date = '2010-06-12 00:00:00.000';
declare @FechaF date = '2014-06-12 00:00:00.000';
declare @Estatus varchar(max) = 'Todos';
declare @Region varchar(max)= '0';
declare @Zone varchar(max) = '0';
declare @Tipo varchar = 'TODAS';
declare @CD varchar(max) = 'TODOS';
declare @Tec varchar(max) = '0';
declare @Reg varchar(max) = '0';
declare @Prog varchar(max) = '0';
declare @FI date = '01-01-2013';
declare @FF date = '01-01-2014';
declare @No_Credito varchar(Max) ='PAEEEMDD10B00092' --'PAEEEMDW12B11078'
/*
=Iif(First(Fields!Tipo.Value, "DataSet1") = 1, 
UCase(First(Fields!Nb_NombreRS.Value, "DataSet1")), 
UCase(First(Fields!Legal.Value, "DataSet1")))

*/

SELECT
	m.Dx_Deleg_Municipio + ', ' + e.Dx_Nombre_Estado AS Dx_Lugar, CRE.Fecha_Pendiente AS Dt_Fecha,
	CRE.No_Credito,
	LTRIM(RTRIM(ISNULL(CL.Razon_Social, '') + ISNULL(CL.Nombre, '') + ' ' + ISNULL(CL.Ap_Paterno, '') + ' ' + ISNULL(CL.Ap_Materno, ''))) AS Nb_Nombre,
	--case when cl.Cve_Tipo_Sociedad = 2 then Cl.Razon_Social else cl.Nombre + ' ' + cl.Ap_Paterno + ' ' + cl.Ap_Materno end  AS Nb_Nombre, 
	LTRIM(RTRIM(ISNULL(RFRL.Razon_Social, '') + ISNULL(RFRL.Nombre, '') + ' ' + ISNULL(RFRL.Ap_Paterno, '') + ' ' + ISNULL(RFRL.Ap_Materno, ''))) AS Nb_NombreRS, 
	--case when cl.Cve_Tipo_Sociedad = 2 then rfrl.Nombre + ' ' + rfrl.Ap_Paterno + ' ' + rfrl.Ap_Materno else cl.Nombre + ' ' + cl.Ap_Paterno + ' ' + cl.Ap_Materno end  AS Nb_NombreRS,
	ISNULL(DFCL.Calle, '') + ' ' + ISNULL(DFCL.Num_Ext, 'S/N') + ISNULL(' INT ' + DFCL.Num_Int, '') + ', ' + 
	UPPER(ISNULL(/*(SELECT TOP 1 Dx_Colonia FROM [CAT_CODIGO_POSTAL_SEPOMEX] cspx WHERE cspx.Cve_CP=CONVERT(INT,DFCL.Colonia))*/DFCL.Colonia, '')) + 
	--DFCL.Colonia +
	', ' + ISNULL(ms.Dx_Deleg_Municipio, '') + ', ' + ISNULL(es.Dx_Nombre_Estado, '') + ' C.P. ' + 
	ISNULL(DFCL.CP, '') AS Dx_Domicilio_Fiscal, 
	DFCL.Telefono_Oficina AS Dx_Telefono, 
	CL.email Dx_Email_Repre_legal,
	p.Dx_Razon_Social AS Dx_Nb_Distribuidor,
	K_CREDITO_COSTO.Mt_Costo, 
	K_CREDITO_DESCUENTO.Mt_Descuento,
	p.Dx_Nombre_Repre,
	p.Dx_Nombre_Repre_Legal,
	mx.Dx_Deleg_Municipio + ', ' + ex.Dx_Nombre_Estado AS Dx_Lugar_Suc,
	CRE.Tipo_Usuario, 
	px.Dx_Razon_Social,
	px.Dx_Nombre_Comercial,
	px.Dx_Nombre_Repre AS Nomb_Repr_Dist_Suc, 
	px.Dx_Nombre_Repre_Legal AS Nomb_Repr_Leg_Dist_Suc, 
	CL.EMAIL AS 'email', 
	CL.Cve_Tipo_Sociedad AS 'Tipo', 
	ISNULL(RFRL.Nombre,'') + ' ' + ISNULL(RFRL.Ap_Paterno,'') + ' ' + ISNULL(RFRL.Ap_Materno,'') AS 'Legal'
FROM CLI_Cliente CL 
	LEFT JOIN CLI_Negocio NEG ON CL.IdCliente = NEG.IdCliente AND CL.Id_Branch = NEG.Id_Branch AND CL.Id_Proveedor = NEG.Id_Proveedor
	LEFT JOIN CRE_Credito CRE ON CL.IdCliente = CRE.IdCliente AND CL.Id_Branch = CRE.Id_Branch AND CL.Id_Proveedor = CRE.Id_Proveedor AND NEG.IdNegocio = CRE.IdNegocio
    LEFT JOIN DIR_Direcciones DFCL ON CL.IdCliente = DFCL.IdCliente AND CL.Id_Branch = DFCL.Id_Branch AND CL.Id_Proveedor = DFCL.Id_Proveedor AND NEG.IdNegocio = DFCL.IdNegocio AND DFCL.IdTipoDomicilio = 2  
	LEFT JOIN DIR_Direcciones DNCL ON CL.IdCliente = DNCL.IdCliente AND CL.Id_Branch = DNCL.Id_Branch AND CL.Id_Proveedor = DNCL.Id_Proveedor AND NEG.IdNegocio = DNCL.IdNegocio AND CRE.RPU = DNCL.RPU AND DNCL.IdTipoDomicilio = 1  
    LEFT JOIN CLI_Ref_Cliente RFOS ON CL.IdCliente = RFOS.IdCliente AND CL.Id_Branch = RFOS.Id_Branch AND CL.Id_Proveedor = RFOS.Id_Proveedor AND NEG.IdNegocio = RFOS.IdNegocio AND RFOS.IdTipoReferencia = 2
	LEFT JOIN DIR_Direcciones DOS  ON CL.IdCliente = DOS.IdCliente  AND CL.Id_Branch = DOS.Id_Branch  AND CL.Id_Proveedor = DOS.Id_Proveedor  AND NEG.IdNegocio = DOS.IdNegocio  AND DOS.IdTipoDomicilio = 3
	LEFT JOIN K_CREDITO_COSTO ON CRE.No_Credito = K_CREDITO_COSTO.No_Credito 
    LEFT JOIN K_CREDITO_DESCUENTO ON CRE.No_Credito = K_CREDITO_DESCUENTO.No_Credito 
    LEFT OUTER JOIN CAT_PROVEEDOR AS p ON CRE.Id_Proveedor = p.Id_Proveedor 
    LEFT OUTER JOIN CAT_DELEG_MUNICIPIO AS m ON p.Cve_Deleg_Municipio_Part = m.Cve_Deleg_Municipio 
    LEFT OUTER JOIN CAT_ESTADO AS e ON p.Cve_Estado_Part = e.Cve_Estado 
    LEFT OUTER JOIN CAT_DELEG_MUNICIPIO AS ms ON DFCL.Cve_Deleg_Municipio = ms.Cve_Deleg_Municipio 
    LEFT OUTER JOIN CAT_ESTADO AS es ON DFCL.Cve_Estado = es.Cve_Estado 
    LEFT OUTER JOIN CAT_PROVEEDORBRANCH AS px ON CRE.Id_Proveedor = px.Id_Branch 
    LEFT OUTER JOIN CAT_DELEG_MUNICIPIO AS mx ON px.Cve_Deleg_Municipio_Part = mx.Cve_Deleg_Municipio 
    LEFT OUTER JOIN CAT_ESTADO AS ex ON px.Cve_Estado_Part = ex.Cve_Estado
    LEFT OUTER JOIN  CLI_Ref_Cliente AS RFRL ON RFRL.IdNegocio = NEG.IdNegocio AND RFRL.IdCliente = CL.IdCliente AND RFRL.Id_Branch = CL.Id_Branch AND 
                         RFRL.Id_Proveedor = CL.Id_Proveedor AND RFRL.IdTipoReferencia = 1
                         
WHERE     (CRE.No_Credito = @No_Credito)