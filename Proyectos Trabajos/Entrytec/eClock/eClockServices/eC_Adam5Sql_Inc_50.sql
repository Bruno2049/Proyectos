CREATE OR REPLACE FUNCTION INC_50_CUENTA (PersonaID IN NUMBER, FechaDesde IN Date,FechaFin In Date, Tipo_Nomina in varchar2, Empresa in varchar2 )
RETURN NUMBER
IS
	Proporcion NUMBER;
	NoDias NUMBER;
	DiasXTrabajar NUMBER;
	DiasSinTrabajar  NUMBER;
	DiasSinTrabajarRetardos  NUMBER;
BEGIN
--La fecha contiene un día mas de la fecha real maxima

	--Obtiene el Total de Dias por trabajar
	SELECT COUNT (*) INTO DiasXTrabajar FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = PersonaID AND PERSONA_DIARIO_FECHA >= FechaDesde AND 
	PERSONA_DIARIO_FECHA < FechaFin AND TURNO_DIA_ID > 0;

	--Obtiene el total de dias de trabajo, esto es igual a 7
	SELECT COUNT (*) INTO NoDias FROM EC_DIAS_TRABAJO WHERE DIAS_TRABAJO >= FechaDesde and DIAS_TRABAJO < FechaFin;

	--Obtiene la sumatoria de incidencias que son falta
	SELECT COUNT (*) INTO DiasSinTrabajar FROM EC_V_PERSONAS_DIARIO_EX WHERE 
	(TIPO_INCIDENCIAS_EX_TXT ='120' OR TIPO_INCIDENCIAS_EX_TXT ='100' OR TIPO_INCIDENCIAS_EX_TXT ='105' OR
	TIPO_INCIDENCIAS_EX_TXT ='110' OR TIPO_INCIDENCIAS_EX_TXT ='113' OR TIPO_INCIDENCIAS_EX_TXT ='115' )
	AND PERSONA_DIARIO_ID IN (
	SELECT PERSONA_DIARIO_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = PersonaID AND PERSONA_DIARIO_FECHA >= FechaDesde AND 
	PERSONA_DIARIO_FECHA < FechaFin	);
	
	--Si no debe trabajar ningun día regresaremos 0
	if DiasXTrabajar = 0 THEN
		return 0;     
	end if;

	--Calcla la proporcion para cada dia trabajado
	Proporcion := (NoDias * 1.0)/(DiasXTrabajar * 1.0);
	
	--Obtiene el tiempo acumulado de retardos en días Se divide el Tiempo de retardo entre el tiempo que debio permanecer el empleado
	SELECT SUM(
	(PERSONA_DIARIO_TDE - TO_DATE('01/01/2006','DD/MM/YYYY')) /
	(PERSONA_DIARIO_TES - TO_DATE('01/01/2006','DD/MM/YYYY'))
	) INTO DiasSinTrabajarRetardos FROM 
	EC_V_PERSONAS_DIARIO_EX, EC_PERSONAS_DIARIO WHERE 
	EC_V_PERSONAS_DIARIO_EX.PERSONA_DIARIO_ID = EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID AND 
	(TIPO_INCIDENCIAS_EX_TXT ='135' OR TIPO_INCIDENCIAS_EX_TXT ='135') AND PERSONA_ID = PersonaID AND PERSONA_DIARIO_FECHA >= FechaDesde AND 
	PERSONA_DIARIO_FECHA < FechaFin;
	
	--Si es nula asigna cero
	if DiasSinTrabajarRetardos is NULL THEN
		DiasSinTrabajarRetardos := 0;     
	end if;
	
	return(round(Proporcion * (0.0 + DiasXTrabajar - DiasSinTrabajar - DiasSinTrabajarRetardos),2));
END;


CREATE OR REPLACE FUNCTION INC_50_HORASTRABAJADAS (PersonaID IN NUMBER, FechaDesde IN Date,FechaFin In Date, Tipo_Nomina in varchar2, Empresa in varchar2 )
RETURN NUMBER
IS
	HorasTrabajo NUMBER;
	HorasFaltas NUMBER;
	HorasRetardos NUMBER;
BEGIN
--La fecha contiene un día mas de la fecha real maxima
--Obtiene el total de dias de trabajo, esto es igual a 7
	HorasTrabajo := 0;
	HorasFaltas := 0;
	HorasRetardos := 0;
	--Obtiene el total de horas a trabajar en el periodo
	SELECT INC_50_HORASXTRABAJAR(PersonaID,FechaDesde,FechaFin,Tipo_Nomina,Empresa)INTO HorasTrabajo FROM DUAL;

	--Obtiene las horas de los días con faltas
	SELECT SUM((PERSONA_DIARIO_TES - TO_DATE('01/01/2006','DD/MM/YYYY'))*24) INTO HorasFaltas FROM 
	EC_V_PERSONAS_DIARIO_EX, EC_PERSONAS_DIARIO WHERE 
	EC_V_PERSONAS_DIARIO_EX.PERSONA_DIARIO_ID = EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID AND 
	(TIPO_INCIDENCIAS_EX_TXT ='120' OR TIPO_INCIDENCIAS_EX_TXT ='100' OR TIPO_INCIDENCIAS_EX_TXT ='105' OR
	TIPO_INCIDENCIAS_EX_TXT ='110' OR TIPO_INCIDENCIAS_EX_TXT ='113' OR TIPO_INCIDENCIAS_EX_TXT ='115' ) 
	 AND PERSONA_ID = PersonaID AND PERSONA_DIARIO_FECHA >= FechaDesde AND 
	PERSONA_DIARIO_FECHA < FechaFin;

	SELECT SUM((PERSONA_DIARIO_TDE - TO_DATE('01/01/2006','DD/MM/YYYY'))*24) INTO HorasRetardos FROM 
	EC_V_PERSONAS_DIARIO_EX, EC_PERSONAS_DIARIO WHERE 
	EC_V_PERSONAS_DIARIO_EX.PERSONA_DIARIO_ID = EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID AND 
	TIPO_INCIDENCIAS_EX_TXT ='135' AND PERSONA_ID = PersonaID AND PERSONA_DIARIO_FECHA >= FechaDesde AND 
	PERSONA_DIARIO_FECHA < FechaFin;

	if HorasFaltas is NULL then
		HorasFaltas := 0;
	end if;
	
	if HorasRetardos is NULL then
		HorasRetardos := 0;
	end if;

	return(round(HorasTrabajo-HorasFaltas-HorasRetardos, 2));
END;

CREATE OR REPLACE FUNCTION INC_50_HORASXTRABAJAR (PersonaID IN NUMBER, FechaDesde IN Date,FechaFin In Date, Tipo_Nomina in varchar2, Empresa in varchar2 )
RETURN NUMBER
IS
	
	HorasXTrabajar NUMBER;
BEGIN
--La fecha contiene un día mas de la fecha real maxima
--Obtiene las horas a trabajar en el periodo

	SELECT sum((PERSONA_DIARIO_TES - TO_DATE('01/01/2006','DD/MM/YYYY'))*24) INTO HorasXTrabajar FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = PersonaID AND PERSONA_DIARIO_FECHA >= FechaDesde AND 
	PERSONA_DIARIO_FECHA < FechaFin;

			if HorasXTrabajar is NULL then        
			 HorasXTrabajar := 0;   
		end if;            

	return(round(HorasXTrabajar, 2));
END;

