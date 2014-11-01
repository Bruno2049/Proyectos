--obtener Region y Zona por Clave
SELECT REGION.Cve_Region, 
 REGION.CLAVE AS  REGION,R.CLAVE,REGION.Dx_Nombre_Region,
Z.Cve_Zona, Z.Dx_Nombre_Zona,R.ZONA_CFE,R.REGION_CFE 
  FROM Regionalizacion R JOIN CAT_ZONA Z ON R.Cve_Zona = Z.Cve_Zona
 JOIN CAT_REGION REGION ON Z.Cve_Region = REGION.Cve_Region
WHERE R.CLAVE = 'DA01' --Campo Zone de la tabla ResponseData


SELECT * FROM Regionalizacion --WHERE Clave = 'DD10'

--Obtener sona de Distribuidor

SELECT case US.Tipo_Usuario when 'S' then CP.Cve_Zona when 'S_B' then CPB.Cve_Zona  end AS Zona FROM US_USUARIO AS US
LEFT OUTER JOIN CAT_PROVEEDOR AS CP on US.Id_Departamento = CP.Id_Proveedor
LEFT OUTER JOIN CAT_PROVEEDORBRANCH AS CPB on US.Id_Departamento = CPB.Id_Proveedor

 WHERE US.Nombre_Usuario = 'C.ENERGIAS ALTERNAS'