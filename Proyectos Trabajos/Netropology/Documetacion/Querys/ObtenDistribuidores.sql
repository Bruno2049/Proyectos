SELECT * from US_USUARIO as dis 
SELECT * FROM CAT_PROVEEDOR as mpro WHERE mpro.Cve_Estatus_Proveedor = 2 --and mpro.Id_Proveedor =
SELECT * FROM CAT_PROVEEDORBRANCH as spro WHERE spro.Cve_Estatus_Proveedor = 2 --spro.Id_Proveedor = 
SELECT * FROM US_USUARIO as jzo WHERE Id_Rol = 2 and Estatus = 'A'


SELECT

dist.Id_Usuario DistID, dist.Nombre_Usuario DistNombre, dist.Tipo_Usuario DistTipoUsuario, dist.Id_Departamento DistDepartamento
, jzon.Id_Usuario ZonaID, jzon.Nombre_Usuario, jzon.Tipo_Usuario, jzon.Id_Departamento 
--*
FROM US_USUARIO as dist 
LEFT OUTER JOIN CAT_PROVEEDOR as mpro on dist.Id_Departamento = mpro.Id_Proveedor and dist.Tipo_Usuario = 'S' and mpro.Cve_Estatus_Proveedor=2
LEFT OUTER JOIN CAT_PROVEEDORBRANCH spro on dist.Id_Departamento = spro.Id_Branch and dist.Tipo_Usuario = 'S_B' and spro.Cve_Estatus_Proveedor=2
JOIN US_USUARIO as jzon on jzon.Id_Departamento = (case  When dist.Tipo_Usuario = 'S' then mpro.Cve_Zona WHEN dist.Tipo_Usuario = 'S_B' then spro.Cve_Zona end )
and jzon.Tipo_Usuario = 'Z_O'
WHERE dist.Estatus = 'A'  and  jzon.Id_Usuario = 791

-----------------CRE_ Validacion RFC
SELECT * FROM CRE_ValidacionRFC WHERE Id_Distribuidor in (SELECT

dist.Id_Usuario
-- DistID, dist.Nombre_Usuario DistNombre, dist.Tipo_Usuario DistTipoUsuario, dist.Id_Departamento DistDepartamento
--, jzon.Id_Usuario ZonaID, jzon.Nombre_Usuario, jzon.Tipo_Usuario, jzon.Id_Departamento 
--*
FROM US_USUARIO as dist 
LEFT OUTER JOIN CAT_PROVEEDOR as mpro on dist.Id_Departamento = mpro.Id_Proveedor and dist.Tipo_Usuario = 'S' and mpro.Cve_Estatus_Proveedor=2
LEFT OUTER JOIN CAT_PROVEEDORBRANCH spro on dist.Id_Departamento = spro.Id_Branch and dist.Tipo_Usuario = 'S_B' and spro.Cve_Estatus_Proveedor=2
JOIN US_USUARIO as jzon on jzon.Id_Departamento = (case  When dist.Tipo_Usuario = 'S' then mpro.Cve_Zona WHEN dist.Tipo_Usuario = 'S_B' then spro.Cve_Zona end )
and jzon.Tipo_Usuario = 'Z_O'
WHERE dist.Estatus = 'A'  and  jzon.Id_Usuario = 791) and Estatus_Validacion = 1