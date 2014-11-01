DROP VIEW EC_V_TURNOS_DIAS;
CREATE VIEW EC_V_TURNOS_DIAS
AS                      
	SELECT     TURNO_DIA_ID, MIN(Turno) AS Turno, MIN(turno_id) as turno_id, MIN (TURNO_COLOR) AS TURNO_COLOR
	FROM         (SELECT     EC_TURNOS_DIA.TURNO_DIA_ID, CASE WHEN eC_TURNOS.TURNO_ID > 0 THEN RTRIM(eC_TURNOS.TURNO_NOMBRE) 
    WHEN EC_TURNOS_DIA.TURNO_DIA_ID = 0 THEN ' ?' WHEN EC_TURNOS_DIA.TURNO_DIA_ID = - 1 THEN ' -' ELSE
    CONVERT(CHAR(5), TURNO_DIA_HE, 108) + '-' + CONVERT(CHAR(5), TURNO_DIA_HS, 108) END AS TURNO, eC_turnos.turno_id,
	TURNO_COLOR
    FROM          eC_TURNOS RIGHT OUTER JOIN
        eC_TURNOS_SEMANAL_DIA ON eC_TURNOS.TURNO_ID = eC_TURNOS_SEMANAL_DIA.TURNO_ID RIGHT OUTER JOIN
        EC_TURNOS_DIA ON eC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID) Turnos
GROUP BY TURNO_DIA_ID;

-- Crea una vista para mostrar la incidencia cargada en determinado dia, el horario de entrada y salida
--y la hora de entrada y salida
DROP VIEW EC_V_PERSONAS_DIARIO;
CREATE VIEW EC_V_PERSONAS_DIARIO
AS
SELECT     EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID, 
                      CASE eC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID WHEN 0 THEN eC_TIPO_INC_SIS.TIPO_INC_SIS_NOMBRE ELSE RTRIM(eC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_NOMBRE)
                       END AS INCIDENCIA_NOMBRE, CASE WHEN eC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID = 0 AND eC_TIPO_INC_SIS.TIPO_INC_SIS_ABR IS NOT NULL 
                      THEN eC_TIPO_INC_SIS.TIPO_INC_SIS_ABR WHEN eC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID > 0 AND 
                      eC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ABR IS NOT NULL THEN eC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ABR ELSE '' END AS INCIDENCIA_ABR, 
					  CASE eC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID WHEN 0 THEN eC_TIPO_INC_SIS.TIPO_INC_SIS_COLOR ELSE EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_COLOR
                       END AS INCIDENCIA_COLOR,
                      EC_V_TURNOS_DIAS.TURNO, EC_V_TURNOS_DIAS.TURNO_COLOR, eC_ACCESOS.ACCESO_FECHAHORA AS ENTRADA, 
                      eC_ACCESOS_1.ACCESO_FECHAHORA AS SALIDA, CASE WHEN eC_ACCESOS.ACCESO_FECHAHORA IS NULL AND 
                      eC_ACCESOS_1.ACCESO_FECHAHORA IS NULL THEN ' ? - ?' WHEN eC_ACCESOS_1.ACCESO_FECHAHORA IS NULL THEN TO_CHAR(
                      eC_ACCESOS.ACCESO_FECHAHORA - EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA,'HH24:MI') || '- ? ' WHEN eC_ACCESOS.ACCESO_FECHAHORA IS NULL
                       THEN ' ? -' || TO_CHAR(eC_ACCESOS_1.ACCESO_FECHAHORA - EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA,'HH24:MI') ELSE TO_CHAR(
                      eC_ACCESOS.ACCESO_FECHAHORA - EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA,'HH24:MI') || '-' || TO_CHAR(
                      eC_ACCESOS_1.ACCESO_FECHAHORA - EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA,'HH24:MI') END AS ENTRADASALIDA, 
                      EC_V_TURNOS_DIAS.TURNO_ID, EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID, INCIDENCIA_COMENTARIO,
					  EC_PERSONAS_DIARIO.PERSONA_ID,EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA
FROM         eC_ACCESOS eC_ACCESOS_1 INNER JOIN
                      eC_INCIDENCIAS INNER JOIN
                      EC_PERSONAS_DIARIO ON eC_INCIDENCIAS.INCIDENCIA_ID = EC_PERSONAS_DIARIO.INCIDENCIA_ID INNER JOIN
                      eC_TIPO_INCIDENCIAS ON eC_INCIDENCIAS.TIPO_INCIDENCIA_ID = eC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID INNER JOIN
                      eC_TIPO_INC_SIS ON EC_PERSONAS_DIARIO.TIPO_INC_SIS_ID = eC_TIPO_INC_SIS.TIPO_INC_SIS_ID INNER JOIN
                      EC_V_TURNOS_DIAS ON EC_PERSONAS_DIARIO.TURNO_DIA_ID = EC_V_TURNOS_DIAS.TURNO_DIA_ID ON 
                      eC_ACCESOS_1.ACCESO_ID = EC_PERSONAS_DIARIO.ACCESO_S_ID INNER JOIN
                      eC_ACCESOS ON EC_PERSONAS_DIARIO.ACCESO_E_ID = eC_ACCESOS.ACCESO_ID ;
                      
-- Crea una vista para mostrar los detalles de asistencia en un dia
DROP VIEW EC_V_ASISTENCIAS;
CREATE VIEW EC_V_ASISTENCIAS   
AS       
SELECT     EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID, EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, EC_ACCESOS.ACCESO_FECHAHORA AS ACCESO_E, 
                      EC_ACCESOS_3.ACCESO_FECHAHORA AS ACCESO_S, EC_ACCESOS_1.ACCESO_FECHAHORA AS ACCESO_CS, 
                      EC_ACCESOS_2.ACCESO_FECHAHORA AS ACCESO_CR, EC_V_PERSONAS_DIARIO.INCIDENCIA_NOMBRE, EC_V_PERSONAS_DIARIO.TURNO, 
                      EC_PERSONAS_DIARIO.INCIDENCIA_ID, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TT, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TE, 
                      EC_PERSONAS_DIARIO.PERSONA_DIARIO_TC, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TDE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TES, 
                      EC_PERSONAS_D_HE.PERSONA_D_HE_SIS, EC_PERSONAS_D_HE.PERSONA_D_HE_CAL, EC_PERSONAS_D_HE.PERSONA_D_HE_APL, 
                      EC_V_PERSONAS_DIARIO.TURNO_ID, EC_PERSONAS_DIARIO.PERSONA_ID, EC_PERSONAS_DIARIO.TIPO_INC_SIS_ID, 
                      EC_PERSONAS_DIARIO.TIPO_INC_C_SIS_ID, EC_PERSONAS_DIARIO.TURNO_DIA_ID, EC_PERSONAS_DIARIO.PERSONA_D_HE_ID, 
                      EC_V_PERSONAS_DIARIO.ENTRADASALIDA, EC_PERSONAS.SUSCRIPCION_ID, EC_PERSONAS.PERSONA_LINK_ID, 
                      EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, EC_V_PERSONAS_DIARIO.INCIDENCIA_ABR, 
                      EC_TIPO_INC_COMIDA_SIS.TIPO_INC_C_SIS_NOMBRE, PERSONA_D_HE_SIMPLE, PERSONA_D_HE_DOBLE, PERSONA_D_HE_TRIPLE, PERSONA_D_HE_COMEN, EC_PERSONAS_D_HE.TIPO_INCIDENCIA_ID as TIPO_INCIDENCIA_IDHE,
                      EC_V_PERSONAS_DIARIO.TIPO_INCIDENCIA_ID, INCIDENCIA_COMENTARIO,TURNO_COLOR, INCIDENCIA_COLOR
FROM         EC_PERSONAS_DIARIO INNER JOIN
                      EC_V_PERSONAS_DIARIO ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = EC_V_PERSONAS_DIARIO.PERSONA_DIARIO_ID INNER JOIN
                      EC_PERSONAS ON EC_PERSONAS_DIARIO.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN
                      EC_TIPO_INC_COMIDA_SIS ON EC_PERSONAS_DIARIO.TIPO_INC_C_SIS_ID = EC_TIPO_INC_COMIDA_SIS.TIPO_INC_C_SIS_ID LEFT OUTER JOIN
                      EC_ACCESOS EC_ACCESOS_1 ON EC_PERSONAS_DIARIO.ACCESO_CS_ID = EC_ACCESOS_1.ACCESO_ID LEFT OUTER JOIN
                      EC_ACCESOS EC_ACCESOS_2 ON EC_PERSONAS_DIARIO.ACCESO_CR_ID = EC_ACCESOS_2.ACCESO_ID LEFT OUTER JOIN
                      EC_ACCESOS EC_ACCESOS_3 ON EC_PERSONAS_DIARIO.ACCESO_S_ID = EC_ACCESOS_3.ACCESO_ID LEFT OUTER JOIN
                      EC_ACCESOS ON EC_PERSONAS_DIARIO.ACCESO_E_ID = EC_ACCESOS.ACCESO_ID LEFT OUTER JOIN
                      EC_PERSONAS_D_HE ON EC_PERSONAS_DIARIO.PERSONA_D_HE_ID = EC_PERSONAS_D_HE.PERSONA_D_HE_ID;

DROP VIEW EC_V_ASISTENCIAS_V5;
CREATE VIEW EC_V_ASISTENCIAS_V5  
AS   
SELECT EC_V_ASISTENCIAS.*,TURNO_DIA_PHEX,TURNO_DIA_HAYCOMIDA
FROM EC_V_ASISTENCIAS INNER JOIN EC_TURNOS_DIA ON EC_V_ASISTENCIAS.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID;
                                  
DROP VIEW EC_V_ACCESOS;
CREATE VIEW EC_V_ACCESOS   
AS       
SELECT     EC_ACCESOS.ACCESO_ID, EC_PERSONAS.PERSONA_ID, EC_PERSONAS.SUSCRIPCION_ID, EC_PERSONAS.PERSONA_LINK_ID, 
                      EC_PERSONAS.PERSONA_NOMBRE, EC_TERMINALES.TERMINAL_NOMBRE, EC_TIPO_ACCESOS.TIPO_ACCESO_NOMBRE, 
                      EC_ACCESOS.ACCESO_FECHAHORA, EC_ACCESOS.ACCESO_CALCULADO, EC_PERSONAS.AGRUPACION_NOMBRE
FROM         EC_PERSONAS INNER JOIN
                      EC_ACCESOS ON EC_PERSONAS.PERSONA_ID = EC_ACCESOS.PERSONA_ID INNER JOIN
                      EC_TIPO_ACCESOS ON EC_ACCESOS.TIPO_ACCESO_ID = EC_TIPO_ACCESOS.TIPO_ACCESO_ID INNER JOIN
                      EC_TERMINALES ON EC_ACCESOS.TERMINAL_ID = EC_TERMINALES.TERMINAL_ID;

DROP VIEW EC_V_ASISTENCIAS_SEMANA;
CREATE VIEW EC_V_ASISTENCIAS_SEMANA   
AS                            
SELECT     EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID, EC_PERSONAS.PERSONA_ID, EC_PERSONAS.PERSONA_LINK_ID, 
                      EC_PERSONAS.PERSONA_NOMBRE, EC_TURNOS.TURNO_NOMBRE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, 
                      EC_V_PERSONAS_DIARIO.TURNO_ID AS TURNO_ID_D0, 
                      EC_V_PERSONAS_DIARIO.TURNO || ' / ' || EC_V_PERSONAS_DIARIO.INCIDENCIA_ABR AS TURNO_D0, 
                      EC_V_PERSONAS_DIARIO.ENTRADASALIDA || ' / ' || EC_V_PERSONAS_DIARIO.INCIDENCIA_ABR AS ASISTENCIA_D0, 
                      EC_V_PERSONAS_DIARIO_1.TURNO_ID AS TURNO_ID_D1, EC_V_PERSONAS_DIARIO_1.TURNO || ' / ' || EC_V_PERSONAS_DIARIO_1.INCIDENCIA_ABR AS TURNO_D1,
                       EC_V_PERSONAS_DIARIO_1.ENTRADASALIDA || ' / ' || EC_V_PERSONAS_DIARIO_1.INCIDENCIA_ABR AS ASISTENCIA_D1, 
                      EC_V_PERSONAS_DIARIO_2.TURNO_ID AS TURNO_ID_D2, EC_V_PERSONAS_DIARIO_2.TURNO || ' / ' || EC_V_PERSONAS_DIARIO_2.INCIDENCIA_ABR AS TURNO_D2,
                       EC_V_PERSONAS_DIARIO_2.ENTRADASALIDA || ' / ' || EC_V_PERSONAS_DIARIO_2.INCIDENCIA_ABR AS ASISTENCIA_D2, 
                      EC_V_PERSONAS_DIARIO_3.TURNO_ID AS TURNO_ID_D3, EC_V_PERSONAS_DIARIO_3.TURNO || ' / ' || EC_V_PERSONAS_DIARIO_3.INCIDENCIA_ABR AS TURNO_D3,
                       EC_V_PERSONAS_DIARIO_3.ENTRADASALIDA || ' / ' || EC_V_PERSONAS_DIARIO_3.INCIDENCIA_ABR AS ASISTENCIA_D3, 
                      EC_V_PERSONAS_DIARIO_4.TURNO_ID AS TURNO_ID_D4, EC_V_PERSONAS_DIARIO_4.TURNO || ' / ' || EC_V_PERSONAS_DIARIO_4.INCIDENCIA_ABR AS TURNO_D4,
                       EC_V_PERSONAS_DIARIO_4.ENTRADASALIDA || ' / ' || EC_V_PERSONAS_DIARIO_4.INCIDENCIA_ABR AS ASISTENCIA_D4, 
                      EC_V_PERSONAS_DIARIO_5.TURNO_ID AS TURNO_ID_D5, EC_V_PERSONAS_DIARIO_5.TURNO || ' / ' || EC_V_PERSONAS_DIARIO_5.INCIDENCIA_ABR AS TURNO_D5,
                       EC_V_PERSONAS_DIARIO_5.ENTRADASALIDA || ' / ' || EC_V_PERSONAS_DIARIO_5.INCIDENCIA_ABR AS ASISTENCIA_D5, 
                      EC_V_PERSONAS_DIARIO_6.TURNO_ID AS TURNO_ID_D6, EC_V_PERSONAS_DIARIO_6.TURNO || ' / ' || EC_V_PERSONAS_DIARIO_6.INCIDENCIA_ABR AS TURNO_D6,
                       EC_V_PERSONAS_DIARIO_6.ENTRADASALIDA || ' / ' || EC_V_PERSONAS_DIARIO_6.INCIDENCIA_ABR AS ASISTENCIA_D6, 
                      EC_PERSONAS.AGRUPACION_NOMBRE, EC_PERSONAS.TURNO_ID, EC_PERSONAS.SUSCRIPCION_ID
FROM         EC_PERSONAS_DIARIO INNER JOIN
                      EC_PERSONAS ON EC_PERSONAS_DIARIO.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN
                      EC_TURNOS ON EC_PERSONAS.TURNO_ID = EC_TURNOS.TURNO_ID INNER JOIN
                      EC_V_PERSONAS_DIARIO ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = EC_V_PERSONAS_DIARIO.PERSONA_DIARIO_ID INNER JOIN
                      EC_V_PERSONAS_DIARIO EC_V_PERSONAS_DIARIO_1 ON 
                      EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID + 1 = EC_V_PERSONAS_DIARIO_1.PERSONA_DIARIO_ID INNER JOIN
                      EC_V_PERSONAS_DIARIO EC_V_PERSONAS_DIARIO_2 ON 
                      EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID + 2 = EC_V_PERSONAS_DIARIO_2.PERSONA_DIARIO_ID INNER JOIN
                      EC_V_PERSONAS_DIARIO EC_V_PERSONAS_DIARIO_3 ON 
                      EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID + 3 = EC_V_PERSONAS_DIARIO_3.PERSONA_DIARIO_ID INNER JOIN
                      EC_V_PERSONAS_DIARIO EC_V_PERSONAS_DIARIO_4 ON 
                      EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID + 4 = EC_V_PERSONAS_DIARIO_4.PERSONA_DIARIO_ID INNER JOIN
                      EC_V_PERSONAS_DIARIO EC_V_PERSONAS_DIARIO_5 ON 
                      EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID + 5 = EC_V_PERSONAS_DIARIO_5.PERSONA_DIARIO_ID INNER JOIN
                      EC_V_PERSONAS_DIARIO EC_V_PERSONAS_DIARIO_6 ON 
                      EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID + 6 = EC_V_PERSONAS_DIARIO_6.PERSONA_DIARIO_ID;

DROP VIEW EC_V_ASISTENCIAS_ES;
CREATE VIEW EC_V_ASISTENCIAS_ES   
AS                           
SELECT     EC_PERSONAS_ES.PERSONA_ES_ID, EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, EC_PERSONAS_ES.PERSONA_ES_ORD, 
                      EC_ACCESOS.ACCESO_FECHAHORA AS ACCESO_E_ES, EC_ACCESOS_1.ACCESO_FECHAHORA AS ACCESO_S_ES, EC_PERSONAS_ES.PERSONA_ES_TE, 
                      EC_PERSONAS.PERSONA_ID, EC_PERSONAS_ES.TIPO_INC_SIS_ID, EC_PERSONAS.SUSCRIPCION_ID, EC_PERSONAS.PERSONA_LINK_ID, 
                      EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE
FROM         EC_PERSONAS_DIARIO INNER JOIN
                      EC_PERSONAS_ES ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = EC_PERSONAS_ES.PERSONA_DIARIO_ID INNER JOIN
                      EC_PERSONAS ON EC_PERSONAS_DIARIO.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN
                      EC_ACCESOS ON EC_PERSONAS_ES.ACCESO_E_ES_ID = EC_ACCESOS.ACCESO_ID INNER JOIN
                      EC_ACCESOS EC_ACCESOS_1 ON EC_PERSONAS_ES.ACCESO_S_ES_ID = EC_ACCESOS_1.ACCESO_ID;

DROP VIEW EC_V_PERSONAS_DIARIO_EX;
CREATE VIEW EC_V_PERSONAS_DIARIO_EX   
AS 
SELECT     EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID,
CASE WHEN EC_PERSONAS_DIARIO.INCIDENCIA_ID = 0 
	 THEN 
		EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_ID
	 ELSE
		EC_TIPO_INCIDENCIAS_EX_1.TIPO_INCIDENCIAS_EX_ID
	 END AS TIPO_INCIDENCIAS_EX_ID,
	 CASE WHEN EC_PERSONAS_DIARIO.INCIDENCIA_ID = 0 
	 THEN 
		EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_TXT
	 ELSE
		EC_TIPO_INCIDENCIAS_EX_1.TIPO_INCIDENCIAS_EX_TXT
	 END AS TIPO_INCIDENCIAS_EX_TXT, 
    CASE WHEN EC_PERSONAS_DIARIO.INCIDENCIA_ID = 0 
	 THEN 
		EC_TIPO_INCIDENCIAS_EX.TIPO_FALTA_EX_ID
	 ELSE
		EC_TIPO_INCIDENCIAS_EX_1.TIPO_FALTA_EX_ID
	 END AS TIPO_FALTA_EX_ID, 
	CASE WHEN EC_PERSONAS_DIARIO.INCIDENCIA_ID = 0 
	 THEN 
		EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_PARAM
	 ELSE
		EC_TIPO_INCIDENCIAS_EX_1.TIPO_INCIDENCIAS_EX_PARAM
	 END AS TIPO_INCIDENCIAS_EX_PARAM,
	 EC_INCIDENCIAS.INCIDENCIA_COMENTARIO
FROM         EC_TIPO_INCIDENCIAS_EX EC_TIPO_INCIDENCIAS_EX_1 INNER JOIN
                      EC_TIPO_INCIDENCIAS_EX_INC ON 
                      EC_TIPO_INCIDENCIAS_EX_1.TIPO_INCIDENCIAS_EX_ID = EC_TIPO_INCIDENCIAS_EX_INC.TIPO_INCIDENCIAS_EX_ID INNER JOIN
                      EC_INCIDENCIAS ON EC_TIPO_INCIDENCIAS_EX_INC.TIPO_INCIDENCIA_ID = EC_INCIDENCIAS.TIPO_INCIDENCIA_ID RIGHT OUTER JOIN
                      EC_PERSONAS_DIARIO LEFT OUTER JOIN
                      EC_TIPO_INCIDENCIAS_EX INNER JOIN
                      EC_TIPO_INCIDENCIAS_EX_INC_SIS ON 
                      EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_ID = EC_TIPO_INCIDENCIAS_EX_INC_SIS.TIPO_INCIDENCIAS_EX_ID ON 
                      EC_PERSONAS_DIARIO.TIPO_INC_SIS_ID = EC_TIPO_INCIDENCIAS_EX_INC_SIS.TIPO_INC_SIS_ID ON 
                      EC_INCIDENCIAS.INCIDENCIA_ID = EC_PERSONAS_DIARIO.INCIDENCIA_ID
WHERE EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_ID > 0 OR EC_TIPO_INCIDENCIAS_EX_1.TIPO_INCIDENCIAS_EX_ID >0;

--Crea la vista para reportes de Comida y Monedero
DROP VIEW EC_V_MONEDERO_SALDO;
CREATE VIEW EC_V_MONEDERO_SALDO
AS 
SELECT     MONEDERO_ID, TIPO_COBRO_ID, PERSONA_ID, SESION_ID, MONEDERO_FECHA, MONEDERO_CONSUMO, MONEDERO_SALDO
FROM         EC_MONEDERO
WHERE     (MONEDERO_ID IN (SELECT     MAX(MONEDERO_ID) MONEDERO_ID
                            FROM          EC_MONEDERO  EC_MONEDERO
                            GROUP BY PERSONA_ID));

DROP VIEW EC_V_PERSONAS_COMIDA;
CREATE VIEW EC_V_PERSONAS_COMIDA
AS
SELECT     PERSONA_COMIDA_ID, PERSONA_ID, PERSONA_COMIDA_FECHA, 
		CASE WHEN TIPO_COMIDA_ID = 1 THEN TIPO_COMIDA_COSTOA ELSE 0 END AS PRIMERA_COMIDA_COSTO, 
		CASE WHEN TIPO_COMIDA_ID = 1 THEN 1 ELSE 0 END AS PRIMERA_COMIDA_ES, 
		CASE WHEN TIPO_COMIDA_ID = 2 THEN TIPO_COMIDA_COSTOA ELSE 0 END AS SEGUNDA_COMIDA_COSTO, 
		CASE WHEN TIPO_COMIDA_ID = 2 THEN 2 ELSE 0 END AS SEGUNDA_COMIDA_ES, TIPO_COMIDA_ID, TIPO_COMIDA_COSTOA, TIPO_COBRO_ID, SESION_ID
FROM        EC_PERSONAS_COMIDA;
--Fin de vistas para reporte de Comida y Monedero

DROP VIEW EC_V_TERMINAL_CID_PERSONA;
CREATE VIEW EC_V_TERMINAL_CID_PERSONA  
AS 
SELECT     EC_TERMINALES.TERMINAL_CAMPO_LLAVE, EC_PERSONAS_TERMINALES.PERSONA_ID
FROM EC_TERMINALES INNER JOIN
    EC_PERSONAS_TERMINALES ON EC_TERMINALES.TERMINAL_ID = EC_PERSONAS_TERMINALES.TERMINAL_ID
WHERE     (EC_PERSONAS_TERMINALES.PERSONA_TERMINAL_UPDATE IS NOT NULL)
GROUP BY EC_TERMINALES.TERMINAL_CAMPO_LLAVE, EC_PERSONAS_TERMINALES.PERSONA_ID;

DROP VIEW EC_V_TERMINALES_EDO;
CREATE VIEW EC_V_TERMINALES_EDO   
AS 
SELECT     EC_TERMINALES.TERMINAL_ID, EC_TERMINALES.TERMINAL_NOMBRE, EC_SITIOS.SITIO_NOMBRE, 
EC_MODELOS_TERMINALES.MODELO_TERMINAL_NOMBRE, EC_TIPO_TECNOLOGIAS.TIPO_TECNOLOGIA_NOMBRE, 
EC_TIPO_TECNOLOGIAS_1.TIPO_TECNOLOGIA_NOMBRE AS TIPO_TECNOLOGIA_NOMBRE_ADD, EC_TERMINALES.TERMINAL_SINCRONIZACION, 
EC_TERMINALES.TERMINAL_CAMPO_LLAVE, EC_TERMINALES.TERMINAL_CAMPO_ADICIONAL, EC_TERMINALES.TERMINAL_ENROLA, 
EC_TERMINALES.TERMINAL_DIR, EC_TERMINALES.TERMINAL_ASISTENCIA, EC_TERMINALES.TERMINAL_MINUTOS_DIF, 
EC_TERMINALES.TERMINAL_BORRADO,
	(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS 
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 101) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS Conexion_Correcta,

	(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 102) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS Error_Conexion,

	(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 103) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS ComunicacionCorrecta,

	(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 104) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS Error_Comunicacion,

	(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 105) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS Log_Comunicacion,

	(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 106) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS FechaHora_Enviada,

	(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 107) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS FechaHora_Error,	

(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 108) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS Checadas_Descargadas,

	(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 109) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS Checadas_Error,

	(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 110) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS Vectores_Descargados,

	(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 111) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS Vectores_Error_Desc,

	(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 112) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS Vectores_Enviados,

	(SELECT     TERMINALES_DEXTRAS_TEXTO1
	FROM          EC_TERMINALES_DEXTRAS
	WHERE      (TERMINALES_DEXTRAS_ID IN
		(SELECT     MAX(TERMINALES_DEXTRAS_ID) AS TERMINALES_DEXTRAS_ID
		 FROM          EC_TERMINALES_DEXTRAS
		 WHERE      (TIPO_TERM_DEXTRAS_ID = 113) AND (TERMINAL_ID = EC_TERMINALES.TERMINAL_ID)))) AS Vectores_Error_Env	,

		( SELECT     COUNT(*)
FROM         EC_PERSONAS_TERMINALES WHERE TERMINAL_ID = EC_TERMINALES.TERMINAL_ID AND PERSONA_ID NOT IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 1)) AS NO_PERSONAS,
( SELECT     COUNT(*)
FROM         EC_PERSONAS_TERMINALES,EC_V_TERMINAL_CID_PERSONA WHERE EC_PERSONAS_TERMINALES.PERSONA_ID = EC_V_TERMINAL_CID_PERSONA.PERSONA_ID  
   AND TERMINAL_ID = EC_TERMINALES.TERMINAL_ID AND TERMINAL_CAMPO_LLAVE = EC_TERMINALES.TERMINAL_CAMPO_LLAVE AND EC_PERSONAS_TERMINALES.PERSONA_ID  NOT IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 1)) AS NO_PERSONAS_POS,

( SELECT     COUNT(*)
FROM         EC_PERSONAS_TERMINALES WHERE TERMINAL_ID = EC_TERMINALES.TERMINAL_ID AND PERSONA_TERMINAL_UPDATE IS NOT NULL AND PERSONA_ID NOT IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 1)) AS NO_PERSONAS_ENV,
( SELECT     COUNT(*)
FROM         EC_PERSONAS_TERMINALES, EC_PERSONAS_A_VEC WHERE EC_PERSONAS_TERMINALES.PERSONA_ID = EC_PERSONAS_A_VEC.PERSONA_ID AND TERMINAL_ID = EC_TERMINALES.TERMINAL_ID AND 
ALMACEN_VEC_ID = EC_TERMINALES.ALMACEN_VEC_ID AND EC_PERSONAS_TERMINALES.PERSONA_ID NOT IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 1) AND PERSONAS_A_VEC_1 IS NOT NULL) AS NO_PERSONAS_HUELLA
FROM         EC_TERMINALES INNER JOIN
                      EC_SITIOS ON EC_TERMINALES.SITIO_ID = EC_SITIOS.SITIO_ID INNER JOIN
                      EC_MODELOS_TERMINALES ON EC_TERMINALES.MODELO_TERMINAL_ID = EC_MODELOS_TERMINALES.MODELO_TERMINAL_ID INNER JOIN
                      EC_TIPO_TECNOLOGIAS ON EC_TERMINALES.TIPO_TECNOLOGIA_ID = EC_TIPO_TECNOLOGIAS.TIPO_TECNOLOGIA_ID INNER JOIN
                      EC_TIPO_TECNOLOGIAS EC_TIPO_TECNOLOGIAS_1 ON 
                      EC_TERMINALES.TIPO_TECNOLOGIA_ADD_ID = EC_TIPO_TECNOLOGIAS_1.TIPO_TECNOLOGIA_ID;

DROP VIEW EC_V_ACCESOS_X_DIA;
CREATE VIEW EC_V_ACCESOS_X_DIA   
AS 
SELECT TERMINAL_ID, TO_DATE(TO_CHAR(ACCESO_FECHAHORA, 'DD/MM/YYYY'), 'DD/MM/YYYY') AS ACCESO_FECHA, COUNT(*) AS ACCESOS_NO
FROM EC_ACCESOS
GROUP BY TERMINAL_ID, TO_CHAR(ACCESO_FECHAHORA, 'DD/MM/YYYY');

- Crea una vista de la tabla EC_TERMINALES

CREATE VIEW EC_V_TERMINALES
AS
SELECT     EC_TERMINALES.TERMINAL_ID, EC_TERMINALES.TIPO_TERMINAL_ACCESO_ID, EC_TERMINALES.TERMINAL_NOMBRE, 
                      EC_TERMINALES.ALMACEN_VEC_ID, EC_TERMINALES.SITIO_ID, EC_TERMINALES.MODELO_TERMINAL_ID, 
                      EC_TERMINALES.TIPO_TECNOLOGIA_ID, EC_TERMINALES.TIPO_TECNOLOGIA_ADD_ID, EC_TERMINALES.TERMINAL_SINCRONIZACION, 
                      EC_TERMINALES.TERMINAL_CAMPO_LLAVE, EC_TERMINALES.TERMINAL_CAMPO_ADICIONAL, EC_TERMINALES.TERMINAL_ENROLA, 
                      EC_TERMINALES.TERMINAL_DIR, EC_TERMINALES.TERMINAL_ASISTENCIA, EC_TERMINALES.TERMINAL_COMIDA, 
                      EC_TERMINALES.TERMINAL_MINUTOS_DIF, EC_TERMINALES.TERMINAL_VALIDAHORARIOE, EC_TERMINALES.TERMINAL_VALIDAHORARIOS, 
                      EC_TERMINALES.TERMINAL_BORRADO, EC_AUTONUM.SESION_ID, EC_AUTONUM.AUTONUM_FECHAHORA, EC_AUTONUM.SUSCRIPCION_ID
FROM         EC_TERMINALES INNER JOIN
                      EC_AUTONUM ON EC_TERMINALES.TERMINAL_ID = EC_AUTONUM.AUTONUM_TABLA_ID
WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_TERMINALES');

-- Crea una vista de la tabla EC_TERMINALES_DEXTRAS

CREATE VIEW EC_V_TERMINALES_DEXTRAS
AS
SELECT     EC_TERMINALES_DEXTRAS.TERMINAL_ID, EC_TERMINALES_DEXTRAS.TIPO_TERM_DEXTRAS_ID, 
                      EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_TEXTO1, EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_TEXTO2, 
                      EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_BIN, EC_AUTONUM.SUSCRIPCION_ID, EC_AUTONUM.AUTONUM_FECHAHORA, 
                      EC_AUTONUM.SESION_ID, EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_ID
FROM         EC_AUTONUM INNER JOIN
                      EC_TERMINALES_DEXTRAS ON EC_AUTONUM.AUTONUM_TABLA_ID = EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_ID
WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_TERMINALES_DEXTRAS');

--Crea la vista para reportes de Comida y Monedero

CREATE VIEW EC_V_MONEDERO_SALDO
AS 
SELECT     MONEDERO_ID, TIPO_COBRO_ID, PERSONA_ID, SESION_ID, MONEDERO_FECHA, MONEDERO_CONSUMO, MONEDERO_SALDO
FROM         EC_MONEDERO
WHERE     (MONEDERO_ID IN (SELECT     MAX(MONEDERO_ID) MONEDERO_ID
                            FROM          EC_MONEDERO  EC_MONEDERO
                            GROUP BY PERSONA_ID));


CREATE VIEW EC_V_PERSONAS_COMIDA
AS
SELECT     PERSONA_COMIDA_ID, PERSONA_ID, PERSONA_COMIDA_FECHA, 
		CASE WHEN TIPO_COMIDA_ID = 1 THEN TIPO_COMIDA_COSTOA ELSE 0 END AS PRIMERA_COMIDA_COSTO, 
		CASE WHEN TIPO_COMIDA_ID = 1 THEN 1 ELSE 0 END AS PRIMERA_COMIDA_ES, 
		CASE WHEN TIPO_COMIDA_ID = 2 THEN TIPO_COMIDA_COSTOA ELSE 0 END AS SEGUNDA_COMIDA_COSTO, 
		CASE WHEN TIPO_COMIDA_ID = 2 THEN 2 ELSE 0 END AS SEGUNDA_COMIDA_ES, TIPO_COMIDA_ID, TIPO_COMIDA_COSTOA, TIPO_COBRO_ID, SESION_ID
FROM        EC_PERSONAS_COMIDA;


CREATE VIEW EC_V_PERSONAS_TERMINALES
AS
SELECT        EC_PERSONAS_TERMINALES.TERMINAL_ID, EC_PERSONAS_TERMINALES.PERSONA_ID, EC_PERSONAS_S_HUELLA.PERSONA_ID AS PERSONA_ID_S_HUELLA, 
                         EC_PERSONAS_A_VEC.PERSONAS_A_VEC_1, EC_PERSONAS_A_VEC.PERSONAS_A_VEC_2, EC_PERSONAS_A_VEC.PERSONAS_A_VEC_3, 
                         EC_PERSONAS_DATO_L.PERSONA_DATO AS DATO_LLAVE, EC_PERSONAS_DATO_LA.PERSONA_DATO AS DATO_ADD,  
                         EC_PERSONAS_S_HUELLA.PERSONA_S_HUELLA_CLAVE, EC_PERSONAS_S_HUELLA.PERSONA_S_HUELLA_FECHA, 
                         EC_PERSONAS_A_VEC.PERSONAS_A_VEC_1_UMOD, EC_PERSONAS_A_VEC.PERSONAS_A_VEC_2_UMOD, 
                         EC_PERSONAS_A_VEC.PERSONAS_A_VEC_3_UMOD, EC_PERSONAS_DATO_L.PERSONA_DATO_FECHA AS DATO_LLAVE_UMOD, 
                         EC_PERSONAS_DATO_LA.PERSONA_DATO_FECHA AS DATO_ADD_UMOD,
						 PERSONA_LINK_ID, PERSONA_NOMBRE,PERSONA_TERMINAL_BORRADO
FROM            EC_PERSONAS_TERMINALES INNER JOIN
                         EC_PERSONAS ON EC_PERSONAS_TERMINALES.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN
                         EC_TERMINALES ON EC_PERSONAS_TERMINALES.TERMINAL_ID = EC_TERMINALES.TERMINAL_ID LEFT OUTER JOIN
                         EC_PERSONAS_A_VEC ON EC_TERMINALES.ALMACEN_VEC_ID = EC_PERSONAS_A_VEC.ALMACEN_VEC_ID AND 
                         EC_PERSONAS.PERSONA_ID = EC_PERSONAS_A_VEC.PERSONA_ID LEFT OUTER JOIN
                         EC_PERSONAS_DATO AS EC_PERSONAS_DATO_L ON EC_PERSONAS_TERMINALES.PERSONA_ID = EC_PERSONAS_DATO_L.PERSONA_ID AND 
                         EC_TERMINALES.TERMINAL_CAMPO_LLAVE = EC_PERSONAS_DATO_L.CAMPO_NOMBRE LEFT OUTER JOIN
                         EC_PERSONAS_DATO AS EC_PERSONAS_DATO_LA ON EC_PERSONAS_TERMINALES.PERSONA_ID = EC_PERSONAS_DATO_LA.PERSONA_ID AND 
                         EC_TERMINALES.TERMINAL_CAMPO_ADICIONAL = EC_PERSONAS_DATO_LA.CAMPO_NOMBRE LEFT OUTER JOIN
                         EC_PERSONAS_S_HUELLA ON EC_PERSONAS.PERSONA_ID = EC_PERSONAS_S_HUELLA.PERSONA_ID
WHERE        (EC_PERSONAS.PERSONA_BORRADO = 0);



CREATE VIEW EC_V_PERSONAS_TERM_CAMBIOS
AS
SELECT        EC_V_PERSONAS_TERMINALES.TERMINAL_ID, EC_V_PERSONAS_TERMINALES.PERSONA_ID, EC_V_PERSONAS_TERMINALES.DATO_LLAVE, 
                         EC_V_PERSONAS_TERMINALES.DATO_ADD, EC_V_PERSONAS_TERMINALES.PERSONAS_A_VEC_1, 
                         EC_V_PERSONAS_TERMINALES.PERSONAS_A_VEC_2, EC_V_PERSONAS_TERMINALES.PERSONAS_A_VEC_3, 
                         EC_V_PERSONAS_TERMINALES.PERSONA_ID_S_HUELLA, EC_V_PERSONAS_TERMINALES.PERSONA_S_HUELLA_CLAVE,
						 PERSONA_LINK_ID, PERSONA_NOMBRE,EC_V_PERSONAS_TERMINALES.PERSONA_TERMINAL_BORRADO
FROM            EC_V_PERSONAS_TERMINALES INNER JOIN
                         EC_PERSONAS_TERMINALES ON EC_V_PERSONAS_TERMINALES.PERSONA_ID = EC_PERSONAS_TERMINALES.PERSONA_ID AND 
                         EC_V_PERSONAS_TERMINALES.TERMINAL_ID = EC_PERSONAS_TERMINALES.TERMINAL_ID
where (DATO_LLAVE_UMOD <> PERSONA_TERMINAL_L_FH_UC OR (DATO_LLAVE_UMOD is null AND PERSONA_TERMINAL_L_FH_UC is not null) or 
(DATO_LLAVE_UMOD is not null AND PERSONA_TERMINAL_L_FH_UC is null) OR
DATO_ADD_UMOD <> PERSONA_TERMINAL_A_FH_UC OR (DATO_ADD_UMOD is null AND PERSONA_TERMINAL_A_FH_UC is not null) or 
(DATO_ADD_UMOD is not null AND PERSONA_TERMINAL_A_FH_UC is null) OR
PERSONAS_A_VEC_1_UMOD <> PERSONA_TERMINAL_V1_FH_UC OR (PERSONAS_A_VEC_1_UMOD is null AND PERSONA_TERMINAL_V1_FH_UC is not null) or 
(PERSONAS_A_VEC_1_UMOD is not null AND PERSONA_TERMINAL_V1_FH_UC is null) OR
PERSONAS_A_VEC_2_UMOD <> PERSONA_TERMINAL_V2_FH_UC OR (PERSONAS_A_VEC_2_UMOD is null AND PERSONA_TERMINAL_V2_FH_UC is not null) or 
(PERSONAS_A_VEC_2_UMOD is not null AND PERSONA_TERMINAL_V2_FH_UC is null) OR
PERSONAS_A_VEC_3_UMOD <> PERSONA_TERMINAL_V3_FH_UC OR (PERSONAS_A_VEC_3_UMOD is null AND PERSONA_TERMINAL_V3_FH_UC is not null) or 
(PERSONAS_A_VEC_3_UMOD is not null AND PERSONA_TERMINAL_V3_FH_UC is null) OR
PERSONA_S_HUELLA_FECHA <> PERSONA_TERMINAL_SH_UC OR (PERSONA_S_HUELLA_FECHA is null AND PERSONA_TERMINAL_SH_UC is not null) or 
(PERSONA_S_HUELLA_FECHA is not null AND PERSONA_TERMINAL_SH_UC is null) OR
PERSONA_TERMINAL_B_FH <> PERSONA_TERMINAL_B_APLICADO OR (PERSONA_TERMINAL_B_FH is null AND PERSONA_TERMINAL_B_APLICADO is not null) or 
(PERSONA_TERMINAL_B_FH is not null AND PERSONA_TERMINAL_B_APLICADO is null)
) AND (PERSONA_TERMINAL_ERRORFH IS NULL OR PERSONA_TERMINAL_ERRORFH < DATO_LLAVE_UMOD OR PERSONA_TERMINAL_ERRORFH < DATO_ADD_UMOD
OR PERSONA_TERMINAL_ERRORFH < PERSONAS_A_VEC_1_UMOD OR PERSONA_TERMINAL_ERRORFH < PERSONAS_A_VEC_2_UMOD
OR PERSONA_TERMINAL_ERRORFH < PERSONAS_A_VEC_3_UMOD OR PERSONA_TERMINAL_ERRORFH < PERSONA_TERMINAL_B_FH )
;

CREATE FUNCTION EC_F_TIENE_ACCESO
(
	-- Add the parameters for the function here
	@Persona_ID int,
	@Terminal_ID int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @X int

	-- Add the T-SQL statements to compute the return value here
	SELECT @X = PERSONA_ID FROM EC_PERSONAS_TERMINALES WHERE PERSONA_ID = @Persona_ID AND TERMINAL_ID = @Terminal_ID
		if(@X is null)
		return 0
	-- Return the result of the function
	RETURN @X

END;

CREATE VIEW EC_V_CHATS_MAILS_IDS
AS
SELECT       EC_MAILS.MAIL_DESTINOS AS CHAT,EC_MAILS.MAIL_DESDE AS CHAT2, MAIL_ID,MAIL_TIPO
FROM            EC_MAILS 
UNION 
SELECT       EC_MAILS.MAIL_DESDE AS CHAT,EC_MAILS.MAIL_DESTINOS AS CHAT2, MAIL_ID,MAIL_TIPO
FROM            EC_MAILS ;

CREATE VIEW EC_V_CHATS_MAILS_IDS_MAX
AS
SELECT CHAT,CHAT2,MAIL_TIPO, MAX(MAIL_ID)as MAIL_ID  
from EC_V_CHATS_MAILS_IDS 
group by CHAT,CHAT2,MAIL_TIPO;

CREATE VIEW EC_V_CHATS_ULTIMOS
AS
SELECT        EC_MAILS.MAIL_ID, EC_MAILS.MAIL_DESTINOS, EC_MAILS.MAIL_COPIAS, EC_MAILS.MAIL_TITULO, EC_MAILS.MAIL_ADJUNTO, 
                         EC_MAILS.MAIL_ADJUNTO_NOMBRE, EC_MAILS.MAIL_FECHAHORA, EC_MAILS.MAIL_ENVIADO, EC_MAILS.MAIL_FECHAHORAE, 
                         EC_MAILS.MAIL_MENSAJE, EC_MAILS.MAIL_TIPO, EC_MAILS.MAIL_DESDE, EC_V_CHATS_MAILS_IDS_MAX.CHAT, 
                         EC_USUARIOS.USUARIO_ID, EC_USUARIOS.USUARIO_NOMBRE
FROM            EC_MAILS INNER JOIN
                         EC_V_CHATS_MAILS_IDS_MAX ON EC_MAILS.MAIL_ID = EC_V_CHATS_MAILS_IDS_MAX.MAIL_ID INNER JOIN
                         EC_USUARIOS ON EC_V_CHATS_MAILS_IDS_MAX.CHAT2 = EC_USUARIOS.USUARIO_ID
WHERE        (EC_V_CHATS_MAILS_IDS_MAX.MAIL_TIPO = - 1);

CREATE VIEW EC_V_CHATS
AS
SELECT        TOP (100) PERCENT EC_MAILS.MAIL_ID, EC_MAILS.MAIL_DESTINOS, EC_MAILS.MAIL_COPIAS, EC_MAILS.MAIL_TITULO, 
                         EC_MAILS.MAIL_ADJUNTO, EC_MAILS.MAIL_ADJUNTO_NOMBRE, EC_MAILS.MAIL_FECHAHORA, EC_MAILS.MAIL_ENVIADO, 
                         EC_MAILS.MAIL_FECHAHORAE, EC_MAILS.MAIL_MENSAJE, EC_MAILS.MAIL_TIPO, EC_MAILS.MAIL_DESDE, 
                         EC_V_CHATS_MAILS_IDS.CHAT, EC_V_CHATS_MAILS_IDS.CHAT2, EC_USUARIOS.USUARIO_NOMBRE
FROM            EC_V_CHATS_MAILS_IDS INNER JOIN
                         EC_MAILS ON EC_V_CHATS_MAILS_IDS.MAIL_ID = EC_MAILS.MAIL_ID INNER JOIN
                         EC_USUARIOS ON EC_MAILS.MAIL_DESDE = EC_USUARIOS.USUARIO_ID
WHERE        (EC_MAILS.MAIL_TIPO = - 1)
ORDER BY EC_MAILS.MAIL_FECHAHORA;

CREATE VIEW EC_V_P_DIARIO_INC_SOL
AS
SELECT        EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, EC_PERSONAS_DIARIO.PERSONA_ID, MIN(EC_INCIDENCIAS.INCIDENCIA_ID) 
                         AS INCIDENCIA_ID, MIN(EC_SOLICITUDES.SOLICITUD_ID) AS SOLICITUD_ID
FROM            EC_PERSONAS_DIARIO LEFT OUTER JOIN
                         EC_INCIDENCIAS ON EC_PERSONAS_DIARIO.INCIDENCIA_ID = EC_INCIDENCIAS.INCIDENCIA_ID LEFT OUTER JOIN
                         EC_SOLICITUDES INNER JOIN
                         EC_SOLICITUDES_P_DIARIO ON EC_SOLICITUDES.SOLICITUD_ID = EC_SOLICITUDES_P_DIARIO.SOLICITUD_ID ON 
                         EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = EC_SOLICITUDES_P_DIARIO.PERSONA_DIARIO_ID
WHERE        (EC_SOLICITUDES.EDO_SOLICITUD_ID = 0) OR
                         (EC_INCIDENCIAS.INCIDENCIA_ID > 0)
GROUP BY EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, EC_PERSONAS_DIARIO.PERSONA_ID;

CREATE VIEW EC_V_USUARIOS_PERMISOS
AS
SELECT        EC_USUARIOS_PERMISOS.USUARIO_PERMISO_ID, EC_USUARIOS.USUARIO_USUARIO, EC_USUARIOS.USUARIO_NOMBRE, 
                         EC_TIPO_PERMISOS.TIPO_PERMISO_NOMBRE,EC_USUARIOS_PERMISOS.SUSCRIPCION_ID, 
                         EC_USUARIOS_PERMISOS.USUARIO_PERMISO, EC_USUARIOS_PERMISOS.USUARIO_ID
FROM            EC_USUARIOS INNER JOIN
                         EC_USUARIOS_PERMISOS ON EC_USUARIOS.USUARIO_ID = EC_USUARIOS_PERMISOS.USUARIO_ID INNER JOIN
                         EC_TIPO_PERMISOS ON EC_USUARIOS_PERMISOS.TIPO_PERMISO_ID = EC_TIPO_PERMISOS.TIPO_PERMISO_ID;

--Igual a PersonasDiario
CREATE VIEW EC_V_PERSONAS_DIARIO_IG
AS
SELECT     PERSONA_DIARIO_ID, 
PERSONA_D_HE_ID, 
ACCESO_E_ID, 
ACCESO_S_ID, 
ACCESO_CS_ID, 
ACCESO_CR_ID, PERSONA_ID, 
PERSONA_DIARIO_FECHA, TIPO_INC_SIS_ID, 
TIPO_INC_C_SIS_ID, INCIDENCIA_ID, 
TURNO_DIA_ID, PERSONA_DIARIO_TT, 
PERSONA_DIARIO_TE, PERSONA_DIARIO_TC, 
PERSONA_DIARIO_TDE, PERSONA_DIARIO_TES
FROM  EC_PERSONAS_DIARIO ;

CREATE VIEW EC_V_TURNOS_DIA_PD_ID
AS 
SELECT        EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID, 
	EC_PERSONAS_DIARIO.PERSONA_ID,
	EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA
	EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA + (EC_TURNOS_DIA.TURNO_DIA_HEMIN - CONVERT(datetime, '01/01/2006', 103)) AS TURNO_DIA_HEMIN,
	EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA + (EC_TURNOS_DIA.TURNO_DIA_HE - CONVERT(datetime, '01/01/2006', 103)) AS TURNO_DIA_HE,
	EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA + (EC_TURNOS_DIA.TURNO_DIA_HEMAX - CONVERT(datetime, '01/01/2006', 103)) AS TURNO_DIA_HEMAX,
	EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA + (EC_TURNOS_DIA.TURNO_DIA_HSMIN - CONVERT(datetime, '01/01/2006', 103)) AS TURNO_DIA_HSMIN,
	EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA + (EC_TURNOS_DIA.TURNO_DIA_HS - CONVERT(datetime, '01/01/2006', 103)) AS TURNO_DIA_HS,
	EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA + (EC_TURNOS_DIA.TURNO_DIA_HSMAX - CONVERT(datetime, '01/01/2006', 103)) AS TURNO_DIA_HSMAX,
	EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA + (EC_TURNOS_DIA.TURNO_DIA_HCS - CONVERT(datetime, '01/01/2006', 103)) AS TURNO_DIA_HCS, 
	EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA + (EC_TURNOS_DIA.TURNO_DIA_HCR - CONVERT(datetime, '01/01/2006', 103)) AS TURNO_DIA_HCR,
	EC_TURNOS_DIA.TURNO_DIA_HERETARDO, EC_TURNOS_DIA.TURNO_DIA_HBLOQUE, EC_TURNOS_DIA.TURNO_DIA_HBLOQUET, 
	EC_TURNOS_DIA.TURNO_DIA_HTIEMPO, EC_TURNOS_DIA.TURNO_DIA_HAYCOMIDA,  EC_TURNOS_DIA.TURNO_DIA_HCTIEMPO, EC_TURNOS_DIA.TURNO_DIA_HCTOLERA, 
	EC_TURNOS_DIA.TURNO_DIA_PHEX, EC_TURNOS_DIA.TURNO_DIA_NO_ASIS, EC_TURNOS_DIA.TURNO_DIA_HERETARDO_B, 
	EC_TURNOS_DIA.TURNO_DIA_HERETARDO_C, EC_TURNOS_DIA.TURNO_DIA_HERETARDO_D                         
FROM            EC_PERSONAS_DIARIO INNER JOIN
	EC_TURNOS_DIA ON EC_PERSONAS_DIARIO.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID;

CREATE VIEW 
AS 
	SELECT        EC_V_TURNOS_DIA_PD_ID.PERSONA_DIARIO_ID, EC_V_TURNOS_DIA_PD_ID.PERSONA_ID, 
	EC_V_TURNOS_DIA_PD_ID.PERSONA_DIARIO_FECHA, MIN(EC_ACCESOS.ACCESO_FECHAHORA) AS ACCESO_MIN, 
	MAX(EC_ACCESOS.ACCESO_FECHAHORA) AS ACCESO_MAX
FROM            EC_V_TURNOS_DIA_PD_ID INNER JOIN
	EC_ACCESOS ON EC_V_TURNOS_DIA_PD_ID.PERSONA_ID = EC_ACCESOS.PERSONA_ID AND 
	EC_V_TURNOS_DIA_PD_ID.TURNO_DIA_HEMIN <= EC_ACCESOS.ACCESO_FECHAHORA AND 
	EC_V_TURNOS_DIA_PD_ID.TURNO_DIA_HSMAX >= EC_ACCESOS.ACCESO_FECHAHORA
GROUP BY EC_V_TURNOS_DIA_PD_ID.PERSONA_DIARIO_ID, EC_V_TURNOS_DIA_PD_ID.PERSONA_ID, 
	 EC_V_TURNOS_DIA_PD_ID.PERSONA_DIARIO_FECHA;