create procedure SP_CreditosLiberados
--Entrada
@Cve_Estado int,
@Cve_Municipio int,
@Cve_Region int,
@Cve_Zona int,
@Desde DateTime,
@Hasta DateTime,
@No_Credito varchar(max),
@TipoUsuario varchar(max),

--Salida

@No_CreditoS Varchar(max) OUTPUT,
@FechaIngreso datetime OUTPUT,
@FechaAutorizado DATETIME OUTPUT,
@intelisis INT OUTPUT,
@MontoFinanciado MONEY OUTPUT,
@GastosInstalacion MONEY OUTPUT,
@kwhPromedio DECIMAL OUTPUT,
@kwPromedio DECIMAL OUTPUT,
@FactorPotencia DECIMAL OUTPUT,
@kwhAhorro DECIMAL OUTPUT,
@kwAhorro DECIMAL OUTPUT,
@Tarifa VARCHAR(MAX) OUTPUT,
@TarifaOrigen VARCHAR OUTPUT
AS

BEGIN

SET NOCOUNT ON;

DECLARE @WHERE varchar(max)
DECLARE @CONSULTA varchar(max)

SET @WHERE = 'CC.Afectacion_SICOM_fecha BETWEEN @Desde AND @Hasta'

IF @Cve_Estado = -1
	IF @Cve_Region <> 0
		SET @WHERE = @WHERE + 'AND CR.Cve_Region = @Cve_Region'
	IF @Cve_Municipio <> 0
		SET @WHERE = @WHERE + 'AND CZ.Cve_Zona = @Cve_Zona'
IF @Cve_Estado > 0
	IF @Cve_Municipio <> 0
		SET @WHERE = @WHERE + 'AND CDM.Cve_Deleg_Municipio = @Cve_Municipio'

	SET @WHERE = @WHERE + 'AND CE.Cve_Estado = @Cve_Estado'

IF @No_Credito <> 'AND CC.No_Credito = @No_Credito'

IF @TipoUsuario <> ''
	SET @WHERE = @WHERE + 'AND CPB.TipoSucursal = @Tipo_Sucursal'





END


BEGIN

SET @CONSULTA = 
SELECT

@No_CreditoS = CC.No_Credito ,@FechaIngreso = CC.Fecha_Pendiente , @FechaAutorizado = CC.Afectacion_SICOM_fecha, @intelisis = CC.ID_Intelisis,@MontoFinanciado = CC.Monto_Solicitado ,
@GastosInstalacion = CC.Gastos_Instalacion, @kwhPromedio = CC.Consumo_Promedio, @kwPromedio = CC.Demanda_Maxima, @FactorPotencia = CC.Factor_Potencia,
@kwhAhorro = CC.No_Ahorro_Consumo , @kwAhorro = CC.No_Ahorro_Demanda, @Tarifa = CT_A.Dx_Tarifa, @TarifaOrigen = CT_O.Dx_Algoritmo, 
CASE WHEN CCLI.Cve_Tipo_Sociedad = 1 then CCLI.Nombre + " " + CCLI.Ap_Paterno + " " + CCLI.Ap_Materno WHEN CCLI.Cve_Tipo_Sociedad = 2 then CCLI.Razon_Social end as Razon_Zocial, CCLI.RFC RFC,
CN.Nombre_Comercial NombreComercial, CPB.Dx_Nombre_Comercial NombreComercialDist, CPB.Tipo_Sucursal TipoSucuralDist, CR.Dx_Nombre_Region Region, CZ.Dx_Nombre_Zona Zona,
CC.Afectacion_SICOM_fecha FechaLiberado
,SUM(QCR.No_Cantidad) AS NO_EA_RC
,SUM(QAA.No_Cantidad) AS NO_EA_AA
,SUM(QIL.No_Cantidad) AS NO_EA_IL
,SUM(QME.No_Cantidad) AS NO_EA_ME
,SUM(QSE.No_Cantidad) AS NO_EA_SE
,SUM(QILED.No_Cantidad) AS NO_EA_ILED
,SUM(QBC.No_Cantidad) AS NO_EA_BC
,SUM(QII.No_Cantidad) AS NO_EA_II
,SUM(QACR.No_Cantidad) AS NO_EA_CR
,SUM(QBRC.No_Unidades) AS NO_EB_RC
,SUM(QBAA.No_Unidades) AS NO_EB_AA
,SUM(QBME.No_Unidades) AS NO_EB_ME
,SUM(QBRC.No_Unidades) AS NO_EB_RC

From Cre_Credito AS CC
INNER JOIN CLI_Negocio AS CN ON CC.IdNegocio = cn.IdNegocio and cc.IdCliente = cn.IdCliente and CC.Id_Branch = CN.Id_Branch
LEFT OUTER JOIN CRE_Facturacion AS CF_A ON CC.No_Credito = CF_A.No_Credito and CF_A.IdTipoFacturacion = 2
LEFT OUTER JOIN CAT_TARIFA AS CT_A ON CF_A.Cve_Tarifa = CT_A.Cve_Tarifa
LEFT OUTER JOIN CRE_Facturacion AS CF_O ON CC.No_Credito = CF_O.No_Credito AND CF_O.IdTipoFacturacion = 1
LEFT OUTER JOIN CAT_TARIFA AS CT_O ON CF_O.Cve_Tarifa = CT_O.Cve_Tarifa
INNER JOIN CLI_Cliente AS CCLI ON CC.Id_Branch = CCLI.Id_Branch and CC.IdCliente = CCLI.IdCliente
LEFT OUTER JOIN CAT_PROVEEDORBRANCH AS CPB ON CC.Id_Branch = CPB.Id_Branch
LEFT OUTER JOIN CAT_TIPO_INDUSTRIA AS CTI ON CN.Cve_Tipo_Industria = CTI.Cve_Tipo_Industria
INNER JOIN K_CREDITO_AMORTIZACION AS KCA ON CC.No_Credito = KCA.No_Credito and KCA.No_Pago = 1
INNER JOIN DIR_Direcciones AS DD ON CN.Id_Branch = DD.Id_Branch AND CN.IdCliente = DD.IdCliente AND CN.IdNegocio = DD.IdNegocio AND DD.IdTipoDomicilio =1
INNER JOIN CAT_ESTADO AS CE ON DD.Cve_Estado = CE.Cve_Estado
INNER JOIN CAT_DELEG_MUNICIPIO AS CDM ON DD.Cve_Deleg_Municipio = CDM.Cve_Deleg_Municipio
INNER JOIN CAT_ZONA AS CZ ON CPB.Cve_Zona = CZ.Cve_Zona
INNER JOIN CAT_REGION AS CR ON CPB.Cve_Region = CR.Cve_Region


LEFT JOIN (
			SELECT KPRC.No_Credito,KPRC.No_Cantidad  FROM K_CREDITO_PRODUCTO KPRC INNER JOIN CAT_PRODUCTO CPRC ON KPRC.Cve_Producto = CPRC.Cve_Producto 
			INNER JOIN CAT_TECNOLOGIA TCRC ON TCRC.Cve_Tecnologia = CPRC.Cve_Tecnologia AND TCRC.Cve_Tecnologia = 1
		  ) QCR ON CC.No_Credito = QCR.No_Credito
LEFT JOIN (
			SELECT KPRC.No_Credito,KPRC.No_Cantidad  FROM K_CREDITO_PRODUCTO KPRC INNER JOIN CAT_PRODUCTO CPRC ON KPRC.Cve_Producto = CPRC.Cve_Producto 
			INNER JOIN CAT_TECNOLOGIA TCRC ON TCRC.Cve_Tecnologia = CPRC.Cve_Tecnologia AND TCRC.Cve_Tecnologia = 2
		  ) QAA ON CC.No_Credito = QAA.No_Credito 
LEFT JOIN (
			SELECT KPRC.No_Credito,KPRC.No_Cantidad  FROM K_CREDITO_PRODUCTO KPRC INNER JOIN CAT_PRODUCTO CPRC ON KPRC.Cve_Producto = CPRC.Cve_Producto 
			INNER JOIN CAT_TECNOLOGIA TCRC ON TCRC.Cve_Tecnologia = CPRC.Cve_Tecnologia AND TCRC.Cve_Tecnologia = 3
		  ) QIL ON CC.No_Credito = QIL.No_Credito 

LEFT JOIN (
			SELECT KPRC.No_Credito,KPRC.No_Cantidad  FROM K_CREDITO_PRODUCTO KPRC INNER JOIN CAT_PRODUCTO CPRC ON KPRC.Cve_Producto = CPRC.Cve_Producto 
			INNER JOIN CAT_TECNOLOGIA TCRC ON TCRC.Cve_Tecnologia = CPRC.Cve_Tecnologia AND TCRC.Cve_Tecnologia = 4
		  ) QME ON CC.No_Credito = QME.No_Credito 

LEFT JOIN (
			SELECT KPRC.No_Credito,KPRC.No_Cantidad  FROM K_CREDITO_PRODUCTO KPRC INNER JOIN CAT_PRODUCTO CPRC ON KPRC.Cve_Producto = CPRC.Cve_Producto 
			INNER JOIN CAT_TECNOLOGIA TCRC ON TCRC.Cve_Tecnologia = CPRC.Cve_Tecnologia AND TCRC.Cve_Tecnologia = 5
		  ) QSE ON CC.No_Credito = QSE.No_Credito 

LEFT JOIN (
			SELECT KPRC.No_Credito,KPRC.No_Cantidad  FROM K_CREDITO_PRODUCTO KPRC INNER JOIN CAT_PRODUCTO CPRC ON KPRC.Cve_Producto = CPRC.Cve_Producto 
			INNER JOIN CAT_TECNOLOGIA TCRC ON TCRC.Cve_Tecnologia = CPRC.Cve_Tecnologia AND TCRC.Cve_Tecnologia = 6
		  ) QILED ON CC.No_Credito = QILED.No_Credito 

LEFT JOIN (
			SELECT KPRC.No_Credito,KPRC.No_Cantidad  FROM K_CREDITO_PRODUCTO KPRC INNER JOIN CAT_PRODUCTO CPRC ON KPRC.Cve_Producto = CPRC.Cve_Producto 
			INNER JOIN CAT_TECNOLOGIA TCRC ON TCRC.Cve_Tecnologia = CPRC.Cve_Tecnologia AND TCRC.Cve_Tecnologia = 7
		  ) QBC ON CC.No_Credito = QBC.No_Credito 

LEFT JOIN (
			SELECT KPRC.No_Credito,KPRC.No_Cantidad  FROM K_CREDITO_PRODUCTO KPRC INNER JOIN CAT_PRODUCTO CPRC ON KPRC.Cve_Producto = CPRC.Cve_Producto 
			INNER JOIN CAT_TECNOLOGIA TCRC ON TCRC.Cve_Tecnologia = CPRC.Cve_Tecnologia AND TCRC.Cve_Tecnologia = 8
		  ) QII ON CC.No_Credito = QII.No_Credito

LEFT JOIN (
			SELECT KPRC.No_Credito,KPRC.No_Cantidad  FROM K_CREDITO_PRODUCTO KPRC INNER JOIN CAT_PRODUCTO CPRC ON KPRC.Cve_Producto = CPRC.Cve_Producto 
			INNER JOIN CAT_TECNOLOGIA TCRC ON TCRC.Cve_Tecnologia = CPRC.Cve_Tecnologia AND TCRC.Cve_Tecnologia = 9
		  ) QACR ON CC.No_Credito = QACR.No_Credito

LEFT JOIN (
			SELECT KCS.No_Credito,KCS.No_Unidades FROM K_CREDITO_SUSTITUCION As KCS
			INNER JOIN CAT_TECNOLOGIA AS CT ON KCS.Cve_Tecnologia=CT.Cve_Tecnologia and CT.Cve_Tecnologia=1 
		  ) QBRC ON CC.No_Credito = QBRC.No_Credito

LEFT JOIN (
			SELECT KCS.No_Credito,KCS.No_Unidades FROM K_CREDITO_SUSTITUCION As KCS
			INNER JOIN CAT_TECNOLOGIA AS CT ON KCS.Cve_Tecnologia=CT.Cve_Tecnologia and CT.Cve_Tecnologia=2 
		  ) QBAA ON CC.No_Credito = QBAA.No_Credito

LEFT JOIN (
			SELECT KCS.No_Credito,KCS.No_Unidades FROM K_CREDITO_SUSTITUCION As KCS
			INNER JOIN CAT_TECNOLOGIA AS CT ON KCS.Cve_Tecnologia=CT.Cve_Tecnologia and CT.Cve_Tecnologia=4 
		  ) QBME ON CC.No_Credito = QBME.No_Credito

LEFT JOIN (
			SELECT KCS.No_Credito,KCS.No_Unidades FROM K_CREDITO_SUSTITUCION As KCS
			INNER JOIN CAT_TECNOLOGIA AS CT ON KCS.Cve_Tecnologia=CT.Cve_Tecnologia and CT.Cve_Tecnologia=9 
		  ) QBCR ON CC.No_Credito = QBCR.No_Credito

--WHERE'+ @WHERE+ '

--GROUP BY CC.No_Credito ,CC.Fecha_Pendiente,CC.RPU,CC.Afectacion_SICOM_fecha , CC.ID_Intelisis
--, CC.Monto_Solicitado , CC.Gastos_Instalacion 
--, CC.Consumo_Promedio  , CC.Demanda_Maxima, CC.Factor_Potencia , CC.No_Ahorro_Consumo, CC.No_Ahorro_Demanda
--,CT_A.Dx_Tarifa
--,CT_O.Dx_Tarifa
--,CASE WHEN CCLI.Cve_Tipo_Sociedad = 1 then CCLI.Nombre + " " + CCLI.Ap_Paterno + " " + CCLI.Ap_Materno WHEN CCLI.Cve_Tipo_Sociedad = 2 then CCLI.Razon_Social end , CCLI.RFC
--,CN.Nombre_Comercial
--,CTI.DESCRIPCION_SCIAN
--,KCA.Dt_Fecha , KCA.Mt_Pago
--,CE.Dx_Nombre_Estado 
--,CDM.Dx_Deleg_Municipio 
--,CPB.Dx_Razon_Social ,CPB.Dx_Nombre_Comercial , CPB.Tipo_Sucursal 
--,CZ.Dx_Nombre_Zona
--,CR.Dx_Nombre_Region'
END