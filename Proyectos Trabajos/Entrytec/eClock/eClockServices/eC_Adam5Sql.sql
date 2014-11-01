SELECT        trabajadores.trabajador PERSONA_LINK_ID, 
TRIM(trabajadores.registro_fiscal) RFC, 
REPLACE(trabajadores.nombre,'/',' ') NOMBRE_COMPLETO, 
TRIM(trabajadores.nombre_abreviado) NOMBRE, 
TRIM(trabajadores.sexo) SEXO,  
TRIM(trabajadores.fecha_nacimiento) FECHA_NAC, 
TRIM(trabajadores.domicilio) DP_CALLE_NO, 
TRIM(trabajadores.domicilio2) DP_COLONIA, 
TRIM(trabajadores.poblacion) DP_DELEGACION, 
TRIM(trabajadores.estado_provincia) DP_ESTADO, 
TRIM(trabajadores.pais) DP_PAIS,  
TRIM(trabajadores.codigo_postal) DP_CP, 
TRIM(trabajadores.telefono_particular) DP_TELEFONO1, 
TRIM(trabajadores.reg_seguro_social) IMSS, 
TRIM(trabajadores.clave_unica) CURP, 
TRIM(trabajadores.e_mail) PERSONA_EMAIL,
TRIM(trabajadores_grales.compania) COMPANIA,
trabajadores_grales.fecha_ingreso FECHA_INGRESO, 
trabajadores_grales.fecha_baja FECHA_BAJA, 
TRIM(trabajadores_grales.telefono_oficina) DT_TELEFONO2, 
TRIM(trabajadores_grales.clase_nomina) TIPO_NOMINA,  
(
	SELECT TRIM(min(puestos.descripcion)) FROM plazas, puestos 
    WHERE plazas.compania = trabajadores_grales.compania AND 
	plazas.trabajador = trabajadores_grales.trabajador AND  
	plazas.puesto = puestos.puesto
) PUESTO , 
(
	SELECT        TRIM(datos_agr_trab_5.descripcion )
	FROM            datos_agr_trab datos_agr_trab_5 INNER JOIN 
	rel_trab_agr rel_trab_agr_5 ON datos_agr_trab_5.agrupacion = rel_trab_agr_5.agrupacion AND  
	datos_agr_trab_5.dato = rel_trab_agr_5.dato 
	WHERE        (datos_agr_trab_5.agrupacion = 'CENTRO_CTO') AND (rel_trab_agr_5.compania = trabajadores_grales.compania) AND  
	(rel_trab_agr_5.trabajador = trabajadores_grales.trabajador)
) CENTRO_DE_COSTOS, 
(
	SELECT        TRIM(datos_agr_trab_5.descripcion )
	FROM            datos_agr_trab datos_agr_trab_5 INNER JOIN 
	rel_trab_agr rel_trab_agr_5 ON datos_agr_trab_5.agrupacion = rel_trab_agr_5.agrupacion AND  
	datos_agr_trab_5.dato = rel_trab_agr_5.dato 
	WHERE        (datos_agr_trab_5.agrupacion = 'PTA') AND (rel_trab_agr_5.compania = trabajadores_grales.compania) AND  
	(rel_trab_agr_5.trabajador = trabajadores_grales.trabajador)
) CAMPO1, 
(
	SELECT        TRIM(datos_agr_trab_5.descripcion )
	FROM            datos_agr_trab datos_agr_trab_5 INNER JOIN 
	rel_trab_agr rel_trab_agr_5 ON datos_agr_trab_5.agrupacion = rel_trab_agr_5.agrupacion AND  
	datos_agr_trab_5.dato = rel_trab_agr_5.dato 
	WHERE        (datos_agr_trab_5.agrupacion = 'SUBCTA') AND (rel_trab_agr_5.compania = trabajadores_grales.compania) AND  
	(rel_trab_agr_5.trabajador = trabajadores_grales.trabajador)
) AREA 
FROM       trabajadores,  trabajadores_grales  
where  trabajadores.trabajador = trabajadores_grales.trabajador AND COMPANIA <>'001' AND (FECHA_BAJA IS NULL OR FECHA_BAJA >= @FECHA_MINIMA_BAJA@)
ORDER BY trabajadores_grales.fecha_baja desc