declare @CreditNo varchar(max)  = 'PAEEEMDD11H00077';
                    SELECT
                    c.No_Credito,
                    c.ID_Prog_Proy,
                    c.Id_Proveedor,
                    c.Cve_Estatus_Credito,
                    cl.Cve_Tipo_Sociedad,
                    cl2.Cve_Tipo_Industria Cve_Tipo_Industria,
                    cl2.Nombre_Comercial Dx_Nombre_Comercial,
                    cl.Nombre Dx_Nombres,
                    cl.Ap_Paterno Dx_Apellido_Paterno,
                    cl.Ap_Materno Dx_Apellido_Materno,
                    cl.Razon_Social Dx_Razon_Social,
                    cl.Fec_Nacimiento Dt_Nacimiento_Fecha,
                    cl.email,
                    dn.telefono_local, 
                    cl.RFC Dx_RFC,
                    cl.CURP Dx_CURP,
                    cl.IdRegimenConyugal Cl_Regimen_Conyugal,
                    CASE
                        WHEN ISNULL(rc.Nombre,'') <> '' 
                            THEN UPPER(rc.Nombre) + ' ' + ISNULL(rc.Ap_Paterno,'') + ' ' + ISNULL(rc.Ap_Materno,'') 
                        WHEN rc.Nombre IS NULL AND cl.Razon_Social IS NOT NULL 
                            THEN UPPER(LTRIM(RTRIM(ISNULL(cl.Razon_Social,'')))) 
                        WHEN rc.Nombre IS NULL AND cl.Razon_Social IS NULL 
                            THEN UPPER(LTRIM(RTRIM(ISNULL(cl.Nombre,'') + ' ' + ISNULL(cl.Ap_Paterno,'') + ' ' + ISNULL(cl.Ap_Materno,''))))
                    END [Dx_Nombre_Repre_Legal],
                    cl2.IdTipoAcreditacion Cve_Acreditacion_Repre_legal,
                    cl2.IdTipoIdentificacion Cve_Identificacion_Repre_legal,
                    cl2.Numero_Identificacion Dx_No_Identificacion_Repre_Legal,
                    rc.Genero Fg_Sexo_Repre_legal,
                    c.RPU No_RPU,
                    cl.Cve_Estado_Civil Fg_Edo_Civil_Repre_legal,
                    rc.Reg_Conyugal Cve_Reg_Conyugal_Repre_legal,
                    rc.email Dx_Email_Repre_legal,
                    df.Calle Dx_Domicilio_Fisc_Calle,
                    df.Num_Ext Dx_Domicilio_Fisc_Num,
                    UPPER(/*(SELECT TOP 1 [Dx_Colonia] FROM [dbo].[CAT_CODIGO_POSTAL_SEPOMEX] where [Cve_CP] =  df.CVE_CP))*/df.Colonia) Dx_Domicilio_Fisc_Colonia,
                    df.CP Dx_Domicilio_Fisc_CP,
                    df.Cve_Estado Cve_Estado_Fisc,
                    df.Cve_Deleg_Municipio Cve_Deleg_Municipio_Fisc,
                    df.Cve_Tipo_Propiedad Cve_Tipo_Propiedad_Fisc,
                    df.Telefono_Oficina Dx_Tel_Fisc,
                    dn.Calle Dx_Domicilio_Neg_Calle,
                    dn.Num_Ext Dx_Domicilio_Neg_Num,
                    UPPER((SELECT TOP 1 [Dx_Colonia] FROM [dbo].[CAT_CODIGO_POSTAL_SEPOMEX] where [Cve_CP] =  dn.CVE_CP)) Dx_Domicilio_Neg_Colonia,
                    dn.CP Dx_Domicilio_Neg_CP,
                    dn.Cve_Estado Cve_Estado_Neg,
                    dn.Cve_Deleg_Municipio [Cve_Deleg_Municipio_Neg],
                    dn.Cve_Tipo_Propiedad Cve_Tipo_Propiedad_Neg,
                    dn.Telefono_Local Dx_Tel_Neg,
                    cl2.Ventas_Mes Mt_Ventas_Mes_Empresa,
                    cl2.Gastos_Mes Mt_Gastos_Mes_Empresa,
                    cl2.Ingreso_Neto Mt_Ingreso_Neto_Mes_Empresa,
                    CASE WHEN ra.Nombre IS NOT NULL THEN ra.Nombre + ' ' + isnull(ra.Ap_Paterno,'') + ' ' + isnull(ra.Ap_Materno,'') ELSE ra.Razon_Social END Dx_Nombre_Aval,
                    CASE WHEN ra.RFC IS NOT NULL THEN ra.RFC ELSE ra.CURP END Dx_RFC_CURP_Aval,
                    ra.Telefono_Local Dx_Tel_Aval,
                    ra.Genero Fg_Sexo_Aval,
                    da.Calle Dx_Domicilio_Aval_Calle,
                    da.Num_Ext Dx_Domicilio_Aval_Num,
                    UPPER(CPda.Dx_Colonia) Dx_Domicilio_Aval_Colonia,
                    da.CP Dx_Domicilio_Aval_CP,
                    da.Cve_Estado Cve_Estado_Aval,
                    da.Cve_Deleg_Municipio Cve_Deleg_Municipio_Aval,
                    ra.Ventas_Mes Mt_Ventas_Mes_Aval,
                    ra.Gastos_Mes Mt_Gastos_Mes_Aval,
                    ra.Ingreso_Neto Mt_Ingreso_Neto_Mes_Aval,
                    --ra.RPU No_RPU_AVAL,
                    rn.Numero_Escritura Dx_No_Escritura_Aval,
                    rn.Fecha_Escritura Dt_Fecha_Escritura_Aval,
                    rn.Nombre_Notario Dx_Nombre_Notario_Escritura_Aval,
                    rn.Numero_Notaria Dx_No_Notario_Escritura_Aval,
                    rn.Estado Cve_Estado_Escritura_Aval,
                    rn.Municipio Cve_Deleg_Municipio_Escritura_Aval,
                    pn.Numero_Escritura Dx_No_Escritura_Poder,
                    pn.Fecha_Escritura Dt_Fecha_Poder,
                    pn.Nombre_Notario Dx_Nombre_Notario_Poder,
                    pn.Numero_Notaria Dx_No_Notario_Poder,
                    pn.Estado Cve_Estado_Poder,
                    pn.Municipio Cve_Deleg_Municipio_Poder,
                    ac.Numero_Escritura Dx_No_Escritura_Acta,
                    ac.Fecha_Escritura Dt_Fecha_Acta,
                    ac.Nombre_Notario Dx_Nombre_Notario_Acta,
                    ac.Numero_Notaria Dx_No_Notario_Acta,
                    ac.Estado Cve_Estado_Acta,
                    ac.Municipio Cve_Deleg_Municipio_Acta,
                    c.No_Ahorro_Energetico No_Ahorro_Energetico,
                    c.No_Ahorro_Economico No_Ahorro_Economico,
                    c.Monto_Solicitado Mt_Monto_Solicitado,
                    c.Monto_Total_Pagar Mt_Monto_Total_Pagar,
                    c.Capacidad_Pago Mt_Capacidad_Pago,
                    c.No_Plazo_Pago No_Plazo_Pago,
                    c.Cve_Periodo_Pago Cve_Periodo_Pago,
                    c.Tasa_IVA_Intereses Pct_Tasa_Interes,
                    c.Tasa_Fija Pct_Tasa_Fija,
                    c.CAT Pct_CAT,
                    c.Tasa_IVA Pct_Tasa_IVA,
                    c.Adquisicion_Sust Fg_Adquisicion_Sust,
                    c.Fecha_Cancelado Dt_Fecha_Cancelado,
                    c.Fecha_Pendiente Dt_Fecha_Pendiente,
                    c.Fecha_Por_entregar Dt_Fecha_Por_entregar,
                    c.Fecha_En_revision Dt_Fecha_En_revision,
                    c.Fecha_Autorizado Dt_Fecha_Autorizado,
                    c.Fecha_Rechazado Dt_Fecha_Rechazado,
                    c.Fecha_Finanzas Dt_Fecha_Finanzas,
                    c.Fecha_Ultmod Dt_Fecha_Ultmod,
                    c.Usr_Ultmod Dx_Usr_Ultmod,
                    c.Cancel_Sistema Dx_Cancel_Sistema,
                    c.Tasa_IVA_Intereses Pct_Tasa_IVA_Intereses,
                    c.Fecha_Beneficiario_con_adeudos Dt_Fecha_Beneficiario_con_adeudos,
                    c.Fecha_Tarifa_fuera_de_programa Dt_Fecha_Tarifa_fuera_de_programa,
                    c.No_consumo_promedio No_consumo_promedio,
                    c.Tipo_Usuario Tipo_Usuario,
                    c.Fecha_Calificación_MOP_no_válida Dt_Fecha_Calificación_MOP_no_válida,
                    c.ID_Intelisis ID_Intelisis,
                    c.ATB01,
                    c.ATB02,
                    c.ATB03,
                    c.ATB04,
                    c.ATB05,
                    c.Fecha_Pago_Intelisis Dt_Fecha_Pago_Intelisis,
                    c.Fecha_Liberacion_Intelisis Dt_Fecha_Liberacion_Intelisis,
                    c.Afectacion_SICOM_fecha Afectacion_SICOM_fecha,
                    c.Afectacion_SICOM_DigitoVer Afectacion_SICOM_DigitoVer,
                    c.Afectacion_SICOM_DigitoVerOk Afectacion_SICOM_DigitoVerOk,
                    c.Afectacion_SICOM_Sufijo,
                    c.Afectacion_SIRCA_Fecha,
                    c.Afectacion_SIRCA_Digito,
                    c.Gastos_Instalacion Mt_Gastos_Instalacion_Mano_Obra
                    ,cl2.Cve_Sector
                    ,cl2.Numero_Empleados No_Empleados
                    ,am.Nombre_Notario DX_NOMBRE_COACREDITADO
                    FROM [CRE_Credito] c 
                    LEFT JOIN	CLI_Cliente cl ON	cl.IdCliente = c.IdCliente
                    LEFT JOIN	CLI_Negocio cl2 ON	cl2.IdCliente = c.IdCliente	
                           AND cl2.IdNegocio = c.IdNegocio			                    
                    LEFT JOIN CLI_Ref_Cliente rc ON rc.IdCliente = c.IdCliente AND rc.IdTipoReferencia = 1
                                    AND rc.IdNegocio = c.IdNegocio
                    LEFT JOIN CLI_Ref_Cliente ra ON ra.IdCliente = c.IdCliente  AND ra.IdTipoReferencia = 2
                                     AND ra.IdNegocio = c.IdNegocio
                    LEFT JOIN DIR_Direcciones dn ON dn.IdCliente = c.IdCliente AND dn.IdTipoDomicilio = 1
                                    AND dn.IdNegocio = c.IdNegocio
                    LEFT JOIN DIR_Direcciones df ON df.IdCliente = c.IdCliente AND df.IdTipoDomicilio = 2
                                     AND df.IdNegocio = c.IdNegocio
                    LEFT JOIN DIR_Direcciones da ON da.IdCliente = c.IdCliente AND da.IdTipoDomicilio = 3
                                     AND da.IdNegocio = c.IdNegocio
                    LEFT JOIN CAT_CODIGO_POSTAL_SEPOMEX CpDa ON da.CVE_CP = CpDa.CVE_CP
                    LEFT JOIN CLI_Referencias_Notariales rn ON rn.IdCliente = c.IdCliente AND rn.IdTipoReferencia = 2
                                     AND rn.IdNegocio = c.IdNegocio
                    LEFT JOIN CLI_Referencias_Notariales pn ON pn.IdCliente = c.IdCliente AND pn.IdTipoReferencia = 6
                                     AND pn.IdNegocio = c.IdNegocio
                    LEFT JOIN CLI_Referencias_Notariales ac ON ac.IdCliente = c.IdCliente AND ac.IdTipoReferencia = 7
                                     AND ac.IdNegocio = c.IdNegocio				                     
                    LEFT JOIN CLI_Referencias_Notariales am ON am.IdCliente = c.IdCliente AND am.IdTipoReferencia = 8
                                    AND am.IdNegocio = c.IdNegocio
                    WHERE c.No_Credito  = @CreditNo
                    AND cl.Cve_Tipo_Sociedad = 1
                    UNION ALL
                    SELECT
                    c.No_Credito,
                    c.ID_Prog_Proy,
                    c.Id_Proveedor,
                    c.Cve_Estatus_Credito,
                    cl.Cve_Tipo_Sociedad,
                    cl2.Cve_Tipo_Industria Cve_Tipo_Industria,
                    cl2.Nombre_Comercial Dx_Nombre_Comercial,
                    cl.Nombre Dx_Nombres,
                    cl.Ap_Paterno Dx_Apellido_Paterno,
                    cl.Ap_Materno Dx_Apellido_Materno,
                    cl.Razon_Social Dx_Razon_Social,
                    cl.Fec_Nacimiento Dt_Nacimiento_Fecha,
                    cl.email,
                    dn.telefono_local, 
                    cl.RFC Dx_RFC,
                    cl.CURP Dx_CURP,
                    cl.IdRegimenConyugal Cl_Regimen_Conyugal,
                    CASE
                        WHEN ISNULL(rc.Nombre,'') <> '' 
                            THEN UPPER(rc.Nombre) +' '+ rc.Ap_Paterno +' '+ rc.Ap_Materno
                        WHEN rc.Nombre IS NULL AND cl.Razon_Social IS NOT NULL 
                            THEN UPPER(LTRIM(RTRIM(ISNULL(cl.Razon_Social,'')))) 
                        WHEN rc.Nombre IS NULL AND cl.Razon_Social IS NULL 
                            THEN UPPER(LTRIM(RTRIM(ISNULL(cl.Nombre,'') + ' ' + ISNULL(cl.Ap_Paterno,'') + ' ' + ISNULL(cl.Ap_Materno,''))))
                    END [Dx_Nombre_Repre_Legal],
                    cl2.IdTipoAcreditacion Cve_Acreditacion_Repre_legal,
                    cl2.IdTipoIdentificacion Cve_Identificacion_Repre_legal,
                    cl2.Numero_Identificacion Dx_No_Identificacion_Repre_Legal,
                    rc.Genero Fg_Sexo_Repre_legal,
                    c.RPU No_RPU,
                    cl.Cve_Estado_Civil Fg_Edo_Civil_Repre_legal,
                    rc.Reg_Conyugal Cve_Reg_Conyugal_Repre_legal,
                    rc.email Dx_Email_Repre_legal,
                    df.Calle Dx_Domicilio_Fisc_Calle,
                    df.Num_Ext Dx_Domicilio_Fisc_Num,
                    UPPER((SELECT TOP 1 [Dx_Colonia] FROM [dbo].[CAT_CODIGO_POSTAL_SEPOMEX] where [Cve_CP] =  df.CVE_CP)) Dx_Domicilio_Fisc_Colonia,
                    df.CP Dx_Domicilio_Fisc_CP,
                    df.Cve_Estado Cve_Estado_Fisc,
                    df.Cve_Deleg_Municipio Cve_Deleg_Municipio_Fisc,
                    df.Cve_Tipo_Propiedad Cve_Tipo_Propiedad_Fisc,
                    df.Telefono_Oficina Dx_Tel_Fisc,
                    dn.Calle Dx_Domicilio_Neg_Calle,
                    dn.Num_Ext Dx_Domicilio_Neg_Num,
                    UPPER((SELECT TOP 1 [Dx_Colonia] FROM [dbo].[CAT_CODIGO_POSTAL_SEPOMEX] where [Cve_CP] =  dn.CVE_CP)) Dx_Domicilio_Neg_Colonia,
                    dn.CP Dx_Domicilio_Neg_CP,
                    dn.Cve_Estado Cve_Estado_Neg,
                    dn.Cve_Deleg_Municipio [Cve_Deleg_Municipio_Neg],
                    dn.Cve_Tipo_Propiedad Cve_Tipo_Propiedad_Neg,
                    dn.Telefono_Local Dx_Tel_Neg,
                    cl2.Ventas_Mes Mt_Ventas_Mes_Empresa,
                    cl2.Gastos_Mes Mt_Gastos_Mes_Empresa,
                    cl2.Ingreso_Neto Mt_Ingreso_Neto_Mes_Empresa,
                    CASE WHEN ra.Nombre IS NOT NULL THEN ra.Nombre + ' ' + isnull(ra.Ap_Paterno,'') + ' ' + isnull(ra.Ap_Materno,'') ELSE ra.Razon_Social END Dx_Nombre_Aval,
                    CASE WHEN ra.RFC IS NOT NULL THEN ra.RFC ELSE ra.CURP END Dx_RFC_CURP_Aval,
                    ra.Telefono_Local Dx_Tel_Aval,
                    ra.Genero Fg_Sexo_Aval,
                    da.Calle Dx_Domicilio_Aval_Calle,
                    da.Num_Ext Dx_Domicilio_Aval_Num,
                    UPPER((SELECT TOP 1 [Dx_Colonia] FROM [dbo].[CAT_CODIGO_POSTAL_SEPOMEX] where [Cve_CP] =  da.CVE_CP)) as Dx_Domicilio_Aval_Colonia,
                    da.CP Dx_Domicilio_Aval_CP,                    
                    da.Cve_Estado Cve_Estado_Aval,
                    da.Cve_Deleg_Municipio Cve_Deleg_Municipio_Aval,
                    ra.Ventas_Mes Mt_Ventas_Mes_Aval,
                    ra.Gastos_Mes Mt_Gastos_Mes_Aval,
                    ra.Ingreso_Neto Mt_Ingreso_Neto_Mes_Aval,
                    rn.Numero_Escritura Dx_No_Escritura_Aval,
                    rn.Fecha_Escritura Dt_Fecha_Escritura_Aval,
                    rn.Nombre_Notario Dx_Nombre_Notario_Escritura_Aval,
                    rn.Numero_Notaria Dx_No_Notario_Escritura_Aval,
                    rn.Estado Cve_Estado_Escritura_Aval,
                    rn.Municipio Cve_Deleg_Municipio_Escritura_Aval,
                    pn.Numero_Escritura Dx_No_Escritura_Poder,
                    pn.Fecha_Escritura Dt_Fecha_Poder,
                    pn.Nombre_Notario Dx_Nombre_Notario_Poder,
                    pn.Numero_Notaria Dx_No_Notario_Poder,
                    pn.Estado Cve_Estado_Poder,
                    pn.Municipio Cve_Deleg_Municipio_Poder,
                    ac.Numero_Escritura Dx_No_Escritura_Acta,
                    ac.Fecha_Escritura Dt_Fecha_Acta,
                    ac.Nombre_Notario Dx_Nombre_Notario_Acta,
                    ac.Numero_Notaria Dx_No_Notario_Acta,
                    ac.Estado Cve_Estado_Acta,
                    ac.Municipio Cve_Deleg_Municipio_Acta,
                    c.No_Ahorro_Energetico No_Ahorro_Energetico,
                    c.No_Ahorro_Economico No_Ahorro_Economico,
                    c.Monto_Solicitado Mt_Monto_Solicitado,
                    c.Monto_Total_Pagar Mt_Monto_Total_Pagar,
                    c.Capacidad_Pago Mt_Capacidad_Pago,
                    c.No_Plazo_Pago No_Plazo_Pago,
                    c.Cve_Periodo_Pago Cve_Periodo_Pago,
                    c.Tasa_IVA_Intereses Pct_Tasa_Interes,
                    c.Tasa_Fija Pct_Tasa_Fija,
                    c.CAT Pct_CAT,
                    c.Tasa_IVA Pct_Tasa_IVA,
                    c.Adquisicion_Sust Fg_Adquisicion_Sust,
                    c.Fecha_Cancelado Dt_Fecha_Cancelado,
                    c.Fecha_Pendiente Dt_Fecha_Pendiente,
                    c.Fecha_Por_entregar Dt_Fecha_Por_entregar,
                    c.Fecha_En_revision Dt_Fecha_En_revision,
                    c.Fecha_Autorizado Dt_Fecha_Autorizado,
                    c.Fecha_Rechazado Dt_Fecha_Rechazado,
                    c.Fecha_Finanzas Dt_Fecha_Finanzas,
                    c.Fecha_Ultmod Dt_Fecha_Ultmod,
                    c.Usr_Ultmod Dx_Usr_Ultmod,
                    c.Cancel_Sistema Dx_Cancel_Sistema,
                    c.Tasa_IVA_Intereses Pct_Tasa_IVA_Intereses,
                    c.Fecha_Beneficiario_con_adeudos Dt_Fecha_Beneficiario_con_adeudos,
                    c.Fecha_Tarifa_fuera_de_programa Dt_Fecha_Tarifa_fuera_de_programa,
                    c.No_consumo_promedio No_consumo_promedio,
                    c.Tipo_Usuario Tipo_Usuario,
                    c.Fecha_Calificación_MOP_no_válida Dt_Fecha_Calificación_MOP_no_válida,
                    c.ID_Intelisis ID_Intelisis,
                    c.ATB01,
                    c.ATB02,
                    c.ATB03,
                    c.ATB04,
                    c.ATB05,
                    c.Fecha_Pago_Intelisis Dt_Fecha_Pago_Intelisis,
                    c.Fecha_Liberacion_Intelisis Dt_Fecha_Liberacion_Intelisis,
                    c.Afectacion_SICOM_fecha Afectacion_SICOM_fecha,
                    c.Afectacion_SICOM_DigitoVer Afectacion_SICOM_DigitoVer,
                    c.Afectacion_SICOM_DigitoVerOk Afectacion_SICOM_DigitoVerOk,
                    c.Afectacion_SICOM_Sufijo,
                    c.Afectacion_SIRCA_Fecha,
                    c.Afectacion_SIRCA_Digito,
                    c.Gastos_Instalacion Mt_Gastos_Instalacion_Mano_Obra
                    ,cl2.Cve_Sector
                    ,cl2.Numero_Empleados No_Empleados
                    ,am.Nombre_Notario DX_NOMBRE_COACREDITADO
                    FROM [CRE_Credito] c INNER JOIN	CLI_Cliente cl ON cl.IdCliente = c.IdCliente  
                     LEFT JOIN CLI_Negocio cl2 ON	cl2.IdCliente = c.IdCliente
                      AND cl2.IdNegocio = c.IdNegocio
                     LEFT JOIN CLI_Ref_Cliente rc ON rc.IdCliente = c.IdCliente AND rc.IdTipoReferencia = 1
                      AND rc.IdNegocio = c.IdNegocio
                     LEFT JOIN CLI_Ref_Cliente ra ON ra.IdCliente = CL.IdCliente  AND ra.IdTipoReferencia = 2
                      AND ra.IdNegocio = c.IdNegocio
                     LEFT JOIN DIR_Direcciones dn ON dn.IdCliente = c.IdCliente AND dn.IdTipoDomicilio = 1	 
                      AND dn.IdNegocio = c.IdNegocio                
                     LEFT JOIN DIR_Direcciones df ON df.IdCliente = c.IdCliente AND df.IdTipoDomicilio = 2
                      AND df.IdNegocio = c.IdNegocio
                     LEFT JOIN DIR_Direcciones da ON da.IdCliente = c.IdCliente AND da.IdTipoDomicilio = 3
                      AND da.IdNegocio = c.IdNegocio
                     LEFT JOIN CLI_Referencias_Notariales rn ON rn.IdCliente = c.IdCliente AND rn.IdTipoReferencia = 4
                      AND rn.IdNegocio = c.IdNegocio
                     LEFT JOIN CLI_Referencias_Notariales pn ON pn.IdCliente = c.IdCliente AND pn.IdTipoReferencia = 6
                      AND pn.IdNegocio = c.IdNegocio
                     LEFT JOIN CLI_Referencias_Notariales ac ON ac.IdCliente = c.IdCliente AND ac.IdTipoReferencia = 7
                      AND ac.IdNegocio = c.IdNegocio
                     LEFT JOIN CLI_Referencias_Notariales am ON am.IdCliente = c.IdCliente AND am.IdTipoReferencia = 8
                      AND am.IdNegocio = c.IdNegocio
                    WHERE c.No_Credito  = @CreditNo
                    AND cl.Cve_Tipo_Sociedad = 2