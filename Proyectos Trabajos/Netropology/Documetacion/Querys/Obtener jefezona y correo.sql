SELECT *--dx_email_zona,US2.Nombre_Usuario 
FROM  US_USUARIO AS US INNER JOIN CAT_PROVEEDOR AS Pro
on US.ID_Departamento = Pro.id_Proveedor INNER JOIN CAT_ZONA as ZN on
pro.CVE_Zona = ZN.cve_ZONA
--INNER JOIN US_USUARIO as US2 on ZN.Dx_email_zona =  US2.Correoelectronico
WHERE US.NOMBRE_USUARIO = 'dist.mayanee.gdl'

SELECT dx_email_zona,US2.Nombre_Usuario FROM  US_USUARIO AS US INNER JOIN CAT_PROVEEDORBRANCH AS Bra
on US.ID_Departamento = Bra.id_Branch INNER JOIN CAT_ZONA as ZN on
Bra.CVE_Zona = ZN.cve_ZONA
INNER JOIN US_USUARIO as US2 on ZN.Dx_email_zona =  US2.Correoelectronico
WHERE US.NOMBRE_USUARIO = 'tecnogas.lincoln'