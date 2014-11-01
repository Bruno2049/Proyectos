declare @FechaI date = '2010-06-12 00:00:00.000';
declare @FechaF date = '2014-06-23 00:00:00.000';
declare @Estatus tinyint = '0';
declare @Region varchar(max)= '0';
declare @Zone varchar(max) = '0';
declare @CD varchar(max) = 'TODOS';
declare @Tec varchar(max) = '0';
declare @Reg varchar(max) = '0';
declare @Prog varchar(max) = '0';
declare @Tipo varchar(Max)= 'TODOS';
declare @FI date = '01-01-2013';
declare @FF date = '06-24-2014'; 
declare @Estado Varchar (max) = '0';
declare @Tarifa nvarchar(2) = '02';
declare @Todos tinyint = 1;


SELECT DISTINCT inh.Id_Acta_Inhabilitacion AS 'Acta', actai.Dt_Fe_Creacion AS 'Dt_Creacion', 
actai.Dt_Fe_Inicio_Inhabilita AS 'Dt_Desde', actai.Dt_Fe_Fin_Inhabilita AS 'Dt_Hasta', 
cd.Id_Centro_Disp AS 'ID_CAyD', cd.Dx_Nombre_Comercial AS 'CD', prog.Dx_Nombre_Programa AS 'Programa', 
tec.Dx_Nombre_General AS 'Tecnologia', tec.Dx_Cve_CC AS 'Cve_Tecnologia', 'Equipos Inhabilitados' AS 'Concepto', zon.Dx_Nombre_Zona AS 'Zona', zon.Dx_Nombre_Responsable AS 'Resp_Zona', reg.Dx_Nombre_Region AS 'Region' 
FROM dbo.K_INHABILITACION AS inh WITH(NOLOCK) 
INNER JOIN dbo.K_ACTA_INHABILITACION AS actai WITH(NOLOCK) ON inh.Id_Acta_Inhabilitacion = actai.Id_Acta_Inhabilitacion 
INNER JOIN dbo.K_INHABILITACION_PRODUCTO AS inh2 WITH(NOLOCK) ON inh.Id_Inhabilitacion = inh2.Id_Inhabilitacion 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS sust WITH(NOLOCK) ON inh2.Id_Credito_Sustitucion = sust.Id_Credito_Sustitucion 
INNER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS crd WITH(NOLOCK) ON sust.No_Credito = crd.No_Credito 
INNER JOIN dbo.CAT_CENTRO_DISP AS cd WITH(NOLOCK) ON inh.Id_Centro_Disp = cd.Id_Centro_Disp 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) ON cd.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) ON zon.Cve_Region = reg.Cve_Region 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) ON sust.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN dbo.CAT_PROGRAMA AS prog WITH(NOLOCK) ON crd.ID_Prog_Proy = prog.ID_Prog_Proy 
WHERE inh.Id_Acta_Inhabilitacion IS NOT NULL 
AND @Tipo <> 'Recuperación' 
AND inh.Fg_Tipo_Centro_Disp = 'M' 
AND (cd.Dx_Nombre_Comercial = @CD OR @CD = 'TODOS') 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (reg.Cve_Region = @Reg OR @Reg = '0') 
AND (zon.Cve_Zona = @Zone OR @Zone = '0') 
AND actai.Dt_Fe_Creacion BETWEEN @FI AND @FF 



UNION ALL 
SELECT DISTINCT inhx.Id_Acta_Inhabilitacion, actaix.Dt_Fe_Creacion, actaix.Dt_Fe_Inicio_Inhabilita, actaix.Dt_Fe_Fin_Inhabilita, cdx.Id_Centro_Disp_Sucursal,  cdx.Dx_Nombre_Comercial, progx.Dx_Nombre_Programa, tecx.Dx_Nombre_General, tecx.Dx_Cve_CC, 'Equipos Inhabilitados', zonx.Dx_Nombre_Zona, zonx.Dx_Nombre_Responsable, regx.Dx_Nombre_Region 
FROM dbo.K_INHABILITACION AS inhx WITH(NOLOCK)  
INNER JOIN dbo.K_ACTA_INHABILITACION AS actaix WITH(NOLOCK) ON inhx.Id_Acta_Inhabilitacion = actaix.Id_Acta_Inhabilitacion 
INNER JOIN dbo.K_INHABILITACION_PRODUCTO AS inh2x WITH(NOLOCK) ON inhx.Id_Inhabilitacion = inh2x.Id_Inhabilitacion 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS sustx WITH(NOLOCK) ON inh2x.Id_Credito_Sustitucion = sustx.Id_Credito_Sustitucion 
INNER JOIN /*dbo.K_CREDITO*/dbo.CRE_Credito AS crdx WITH(NOLOCK) ON sustx.No_Credito = crdx.No_Credito 
INNER JOIN dbo.CAT_CENTRO_DISP_SUCURSAL AS cdx WITH(NOLOCK) ON inhx.Id_Centro_Disp = cdx.Id_Centro_Disp_Sucursal  
INNER JOIN dbo.CAT_ZONA AS zonx WITH(NOLOCK) ON cdx.Cve_Zona = zonx.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS regx WITH(NOLOCK) ON zonx.Cve_Region = regx.Cve_Region 
INNER JOIN dbo.CAT_TECNOLOGIA AS tecx WITH(NOLOCK) ON sustx.Cve_Tecnologia = tecx.Cve_Tecnologia 
INNER JOIN dbo.CAT_PROGRAMA AS progx WITH(NOLOCK) ON crdx.ID_Prog_Proy = progx.ID_Prog_Proy 
WHERE inhx.Id_Acta_Inhabilitacion IS NOT NULL 
AND @Tipo <> 'Recuperación' 
AND inhx.Fg_Tipo_Centro_Disp = 'B' 
AND (cdx.Dx_Nombre_Comercial = @CD OR @CD = 'TODOS') 
AND (tecx.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (regx.Cve_Region = @Reg OR @Reg = '0') 
AND (zonx.Cve_Zona = @Zone OR @Zone = '0') 
AND actaix.Dt_Fe_Creacion BETWEEN @FI AND @FF 
	
	
	
UNION ALL 
SELECT DISTINCT rec.Id_Acta_Recuperacion, actar.Dt_Fe_Creacion, actar.Dt_Fe_Inicio_Recup, 
actar.Dt_Fe_Fin_Recup, cdb.Id_Centro_Disp, cdb.Dx_Nombre_Comercial, prog.Dx_Nombre_Programa, tec.Dx_Nombre_General, tec.Dx_Cve_CC, 'Residuos y Materiales', zon.Dx_Nombre_Zona, zon.Dx_Nombre_Responsable, reg.Dx_Nombre_Region  
FROM dbo.K_RECUPERACION AS rec WITH(NOLOCK) 
INNER JOIN dbo.K_ACTA_RECUPERACION AS actar WITH(NOLOCK) ON rec.Id_Acta_Recuperacion = actar.Id_Acta_Recuperacion 
INNER JOIN dbo.K_RECUPERACION_PRODUCTO AS rec2 WITH(NOLOCK) ON rec.Id_Recuperacion = rec2.Id_Recuperacion 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS sust WITH(NOLOCK) ON rec2.Id_Credito_Sustitucion = sust.Id_Credito_Sustitucion 
INNER JOIN /*dbo.K_CREDITO*/ dbo.CRE_Credito AS crd WITH(NOLOCK) ON sust.No_Credito = crd.No_Credito 
INNER JOIN dbo.CAT_CENTRO_DISP AS cdb WITH(NOLOCK) ON rec.Id_Centro_Disp = cdb.Id_Centro_Disp 
INNER JOIN dbo.CAT_ZONA AS zon WITH(NOLOCK) ON cdb.Cve_Zona = zon.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS reg WITH(NOLOCK) ON zon.Cve_Region = reg.Cve_Region 
INNER JOIN dbo.CAT_TECNOLOGIA AS tec WITH(NOLOCK) ON sust.Cve_Tecnologia = tec.Cve_Tecnologia 
INNER JOIN dbo.CAT_PROGRAMA AS prog WITH(NOLOCK) ON crd.ID_Prog_Proy = prog.ID_Prog_Proy 
WHERE rec.Id_Acta_Recuperacion IS NOT NULL 
AND @Tipo <> 'Inhabilitación' 
AND rec.Fg_Tipo_Centro_Disp = 'M' 
AND (cdb.Dx_Nombre_Comercial = @CD OR @CD = 'TODOS') 
AND (tec.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (reg.Cve_Region = @Reg OR @Reg = '0') 
AND (zon.Cve_Zona = @Zone OR @Zone = '0') 
AND actar.Dt_Fe_Creacion BETWEEN @FI AND @FF 


UNION ALL 
SELECT DISTINCT recx.Id_Acta_Recuperacion, actarx.Dt_Fe_Creacion, actarx.Dt_Fe_Inicio_Recup, 
actarx.Dt_Fe_Fin_Recup, cdbx.Id_Centro_Disp_Sucursal, cdbx.Dx_Nombre_Comercial, progx.Dx_Nombre_Programa, tecx.Dx_Nombre_General, tecx.Dx_Cve_CC, 'Residuos y Materiales', zonx.Dx_Nombre_Zona, zonx.Dx_Nombre_Responsable, regx.Dx_Nombre_Region 
FROM dbo.K_RECUPERACION AS recx WITH(NOLOCK) 
INNER JOIN dbo.K_ACTA_RECUPERACION AS actarx WITH(NOLOCK) ON recx.Id_Acta_Recuperacion = actarx.Id_Acta_Recuperacion 
INNER JOIN dbo.K_RECUPERACION_PRODUCTO AS rec2x WITH(NOLOCK) ON recx.Id_Recuperacion = rec2x.Id_Recuperacion 
INNER JOIN dbo.K_CREDITO_SUSTITUCION AS sustx WITH(NOLOCK) ON rec2x.Id_Credito_Sustitucion = sustx.Id_Credito_Sustitucion 
INNER JOIN /*dbo.K_CREDITO*/ dbo.CRE_Credito AS crdx WITH(NOLOCK) ON sustx.No_Credito = crdx.No_Credito 
INNER JOIN dbo.CAT_CENTRO_DISP_SUCURSAL AS cdbx WITH(NOLOCK) ON recx.Id_Centro_Disp = cdbx.Id_Centro_Disp_Sucursal 
INNER JOIN dbo.CAT_ZONA AS zonx WITH(NOLOCK) ON cdbx.Cve_Zona = zonx.Cve_Zona 
INNER JOIN dbo.CAT_REGION AS regx WITH(NOLOCK) ON zonx.Cve_Region = regx.Cve_Region 
INNER JOIN dbo.CAT_TECNOLOGIA AS tecx WITH(NOLOCK) ON sustx.Cve_Tecnologia = tecx.Cve_Tecnologia 
INNER JOIN dbo.CAT_PROGRAMA AS progx WITH(NOLOCK) ON crdx.ID_Prog_Proy = progx.ID_Prog_Proy 
WHERE recx.Id_Acta_Recuperacion IS NOT NULL 
AND @Tipo <> 'Inhabilitación' 
AND recx.Fg_Tipo_Centro_Disp = 'B' 
AND (cdbx.Dx_Nombre_Comercial = @CD OR @CD = 'TODOS') 
AND (tecx.Cve_Tecnologia = @Tec OR @Tec = '0') 
AND (regx.Cve_Region = @Reg OR @Reg = '0') 
AND (zonx.Cve_Zona = @Zone OR @Zone = '0') 
AND actarx.Dt_Fe_Creacion BETWEEN @FI AND @FF 
ORDER BY Acta DESC