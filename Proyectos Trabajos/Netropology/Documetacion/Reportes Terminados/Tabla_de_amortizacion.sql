declare @No_Credito varchar(max) = 'PAEEEMDX14C11074'; --dev
--declare @No_Credito varchar(max) = 'PAEEEMDB03A06557'; --pro

SELECT
	UPPER(LTRIM(RTRIM(ISNULL(CL.Razon_Social, '') + ISNULL(CL.Nombre, '') + ' ' + ISNULL(CL.Ap_Paterno, '') + ' ' + ISNULL(CL.Ap_Materno, '')))) 
	AS NbNombre, CRE.RPU AS No_RPU, pp.Dx_Ciclo, ISNULL(DFCL.Calle, '') + ' ' + ISNULL(DFCL.Num_Ext, 'S/N') 
    + ISNULL(' INT '+DFCL.Num_Int+' ','') + ', ' + UPPER(ISNULL((SELECT TOP 1 Dx_Colonia FROM [CAT_CODIGO_POSTAL_SEPOMEX] cspx WHERE cspx.Cve_CP=CONVERT(INT,DFCL.Colonia)), '')) + ', ' + ISNULL(ms.Dx_Deleg_Municipio, '') + ', ' + ISNULL(es.Dx_Nombre_Estado, '') + ' C.P. ' + ISNULL(DFCL.CP, '') 
    AS Dx_Domicilio_Suscriptor, CRE.Monto_Solicitado AS Mt_Importe, CRE.Tasa_Interes AS Pct_Tasa_Interes, CRE.Tasa_IVA_Intereses AS Pct_Tasa_IVA_Intereses, 
    CRE.CAT AS Pct_CAT, CRE.No_Credito, CAT_PRODUCTO.Dx_Nombre_Producto, CRE.Fecha_Pendiente AS Dt_Fecha_Pendiente, 
    dbo.NumeroEnLetra(CRE.Monto_Solicitado) AS Dx_Monto_Solicitado, K_CREDITO_PRODUCTO.No_Cantidad, CL.Cve_Tipo_Sociedad, 
    UPPER(LTRIM(RTRIM(ISNULL(RL.Nombre, '') + ' ' + ISNULL(RL.Ap_Paterno, '') + ' ' + ISNULL(RL.Ap_Materno, '')))) AS Dx_Nombre_Repre_Legal
FROM CLI_Cliente AS CL 
	LEFT JOIN CLI_Negocio AS NEG ON CL.IdCliente = NEG.IdCliente AND CL.Id_Branch = NEG.Id_Branch AND CL.Id_Proveedor = NEG.Id_Proveedor 
	LEFT JOIN CRE_Credito AS CRE ON CL.IdCliente = CRE.IdCliente AND CL.Id_Branch = CRE.Id_Branch AND CL.Id_Proveedor = CRE.Id_Proveedor AND NEG.IdNegocio = CRE.IdNegocio 
	LEFT JOIN DIR_Direcciones AS DFCL ON CL.IdCliente = DFCL.IdCliente AND CL.Id_Branch = DFCL.Id_Branch AND CL.Id_Proveedor = DFCL.Id_Proveedor AND NEG.IdNegocio = DFCL.IdNegocio AND DFCL.IdTipoDomicilio = 2 
	LEFT JOIN DIR_Direcciones AS DNCL ON CL.IdCliente = DNCL.IdCliente AND CL.Id_Branch = DNCL.Id_Branch AND CL.Id_Proveedor = DNCL.Id_Proveedor AND NEG.IdNegocio = DNCL.IdNegocio AND CRE.RPU = DNCL.RPU AND DNCL.IdTipoDomicilio = 1 
	LEFT JOIN CLI_Ref_Cliente AS RFOS ON CL.IdCliente = RFOS.IdCliente AND CL.Id_Branch = RFOS.Id_Branch AND CL.Id_Proveedor = RFOS.Id_Proveedor AND NEG.IdNegocio = RFOS.IdNegocio AND RFOS.IdTipoReferencia = 2 
	LEFT JOIN DIR_Direcciones AS DOS ON CL.IdCliente = DOS.IdCliente AND CL.Id_Branch = DOS.Id_Branch AND CL.Id_Proveedor = DOS.Id_Proveedor AND NEG.IdNegocio = DOS.IdNegocio AND DOS.IdTipoDomicilio = 3 
	LEFT JOIN CLI_Ref_Cliente AS RL ON CL.IdCliente = RL.IdCliente AND CL.Id_Branch = RL.Id_Branch AND CL.Id_Proveedor = RL.Id_Proveedor AND NEG.IdNegocio = RL.IdNegocio AND RL.IdTipoReferencia = 1 
	LEFT OUTER JOIN K_CREDITO_PRODUCTO ON CRE.No_Credito = K_CREDITO_PRODUCTO.No_Credito 
	LEFT OUTER JOIN CAT_PRODUCTO ON K_CREDITO_PRODUCTO.Cve_Producto = CAT_PRODUCTO.Cve_Producto 
	LEFT OUTER JOIN CAT_PERIODO_PAGO AS pp ON CRE.Cve_Periodo_Pago = pp.Cve_Periodo_Pago 
	LEFT OUTER JOIN CAT_PROGRAMA AS cp ON CRE.ID_Prog_Proy = cp.ID_Prog_Proy 
	LEFT OUTER JOIN CAT_DELEG_MUNICIPIO AS ms ON DFCL.Cve_Deleg_Municipio = ms.Cve_Deleg_Municipio 
	LEFT OUTER JOIN CAT_ESTADO AS es ON DFCL.Cve_Estado = es.Cve_Estado
WHERE (CRE.No_Credito = @No_Credito)