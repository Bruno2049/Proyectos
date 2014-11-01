--declare @No_Credito varchar(max) = 'PAEEEMDX14C11074'; --dev
--declare @No_Credito varchar(max) = 'PAEEEMDD18A00165'; --pro m
declare @No_Credito varchar(max) = 'PAEEEMDD10B00092'; --pro


SELECT
CL.Nombre,rc.Nombre,rc.Ap_Paterno,cl.Razon_Social,rc.Nombre,
	UPPER(ISNULL(m.Dx_Deleg_Municipio,'') + ', ' + 
		  ISNULL(e.Dx_Nombre_Estado,''))
	Dx_Lugar,CRE.Fecha_Pendiente Dt_Fecha, 
	UPPER(LTRIM(RTRIM(ISNULL(CL.Razon_Social,'') +   
					  ISNULL(CL.Nombre,'') + ' ' + 
					  ISNULL(CL.Ap_Paterno,'') + ' ' + 
					  ISNULL(CL.Ap_Materno,'')))) Nb_Nombre, 
	UPPER(ISNULL(DFCL.Calle, '') + ' ' + 
		  ISNULL(DFCL.Num_Ext, 'S/N')  + 
		  ISNULL(' Int ' + DFCL.Num_Int, '') + ', ' + 
		  ISNULL (/*(SELECT TOP 1 Dx_Colonia 
			      FROM [CAT_CODIGO_POSTAL_SEPOMEX] cspx 
			      WHERE cspx.Cve_CP=CONVERT(INT,DFCL.Colonia))*/ dfcl.Colonia, '') + ', ' + 
			      --WHERE cspx.Cve_CP= CONVERT(INT,DFCL.Cve_cp)) ,'') +' '+
				  ISNULL(ms.Dx_Deleg_Municipio, '') + ', ' + 
				  ISNULL(es.Dx_Nombre_Estado, '') + ' C.P. ' + 
				  ISNULL(CONVERT(varchar, DFCL.CVE_CP), '') )				  
    Dx_Domicilio_Fiscal,
     CL.CURP Dx_CURP, CL.RFC Dx_RFC,DFCL.Telefono_Oficina Dx_Telefono,
	CASE WHEN rc.Razon_Social IS NULL 
			THEN UPPER(ISNULL(rc.Nombre,'') + ' ' + 
				   ISNULL(rc.Ap_Paterno,'') + ' ' + 
				   ISNULL(rc.Ap_Materno,''))
			ELSE UPPER(ISNULL(CL.Nombre,'') + ' ' + 
				   ISNULL(CL.Ap_Paterno,'') + ' ' + 
				   ISNULL(CL.Ap_Materno,'')) 
	
	END Nb_Representate, 
	UPPER(ISNULL(cp.Dx_Nombre_Programa,'')) 
	Dx_Nombre_Programa,CRE.No_Credito No_Credito, 
	UPPER(ISNULL(mx.Dx_Deleg_Municipio,'') + ', ' + 
	      ISNULL(ex.Dx_Nombre_Estado,'')) 
    Dx_Lugar_Suc,CRE.Tipo_Usuario Tipo_Usuario, CL.Cve_Tipo_Sociedad  Cve_Tipo_Sociedad, 
	ISNULL(Cre.Folio_Consulta, '') as Expr1 , ISNULL(CRE.Fecha_Consulta, '') Expr2
	
	--select *
FROM CLI_Cliente CL 
	INNER JOIN CLI_NEGOCIO     NEG	   ON  CL.IdCliente = NEG.IdCliente  AND CL.Id_Branch = NEG.Id_Branch  AND CL.Id_Proveedor = NEG.Id_Proveedor
	INNER JOIN CRE_Credito     CRE     ON  CL.IdCliente = CRE.IdCliente  AND CL.Id_Branch = CRE.Id_Branch  AND CL.Id_Proveedor = CRE.Id_Proveedor  AND NEG.IdNegocio = CRE.IdNegocio
	INNER JOIN DIR_Direcciones DFCL    ON  CL.IdCliente = DFCL.IdCliente AND CL.Id_Branch = DFCL.Id_Branch AND CL.Id_Proveedor = DFCL.Id_Proveedor AND NEG.IdNegocio = DFCL.IdNegocio AND DFCL.IdTipoDomicilio = 2 --FISCAL
	INNER JOIN DIR_Direcciones DNCL    ON  CL.IdCliente = DNCL.IdCliente AND CL.Id_Branch = DNCL.Id_Branch AND CL.Id_Proveedor = DNCL.Id_Proveedor AND NEG.IdNegocio = DNCL.IdNegocio AND CRE.RPU = DNCL.RPU AND DNCL.IdTipoDomicilio = 1 --NEGOCIO
	LEFT OUTER JOIN CLI_Ref_Cliente rc ON  CL.IdCliente = rc.IdCliente AND CL.Id_Branch = rc.Id_Branch AND CL.Id_Proveedor = rc.Id_Proveedor AND NEG.IdNegocio = rc.IdNegocio AND rc.IdTipoReferencia =1
	LEFT OUTER JOIN CAT_PROGRAMA	    AS cp WITH (NOLOCK) ON CRE.ID_Prog_Proy = cp.ID_Prog_Proy 
	LEFT OUTER JOIN CAT_PERIODO_PAGO	AS pp WITH (NOLOCK) ON CRE.Cve_Periodo_Pago = pp.Cve_Periodo_Pago 
    LEFT OUTER JOIN CAT_PROVEEDOR		AS p  WITH (NOLOCK) ON CRE.Id_Proveedor = p.Id_Proveedor 
    LEFT OUTER JOIN CAT_DELEG_MUNICIPIO AS m  WITH (NOLOCK) ON p.Cve_Deleg_Municipio_Part = m.Cve_Deleg_Municipio 
    LEFT OUTER JOIN CAT_ESTADO			AS e  WITH (NOLOCK) ON p.Cve_Estado_Part = e.Cve_Estado 
    LEFT OUTER JOIN CAT_DELEG_MUNICIPIO AS ms WITH (NOLOCK) ON DFCL.Cve_Deleg_Municipio = ms.Cve_Deleg_Municipio 
    LEFT OUTER JOIN CAT_ESTADO			AS es WITH (NOLOCK) ON DFCL.Cve_Estado = es.Cve_Estado 
    LEFT OUTER JOIN CAT_PROVEEDORBRANCH AS px WITH (NOLOCK) ON CRE.Id_Proveedor = px.Id_Branch 
    LEFT OUTER JOIN CAT_DELEG_MUNICIPIO AS mx WITH (NOLOCK) ON px.Cve_Deleg_Municipio_Part = mx.Cve_Deleg_Municipio 
    LEFT OUTER JOIN CAT_ESTADO			AS ex WITH (NOLOCK) ON px.Cve_Estado_Part = ex.Cve_Estado
WHERE (CRE.No_Credito = @No_Credito)