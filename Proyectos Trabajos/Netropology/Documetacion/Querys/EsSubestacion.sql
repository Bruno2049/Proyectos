--SELECT Cre.No_Credito, Cat.Dx_Estatus_Credito FROM CRE_Credito AS CRE INNER JOIN CAT_ESTATUS_CREDITO AS Cat on Cre.Cve_Estatus_Credito = Cat.Cve_Estatus_Credito
--  WHERE No_Credito = 'PAEEEMDB08A14440'

--SELECT * FROM CAT_ESTATUS_CREDITO

--UPDATE CRE_Credito set Cve_Estatus_Credito = 1 WHERE No_Credito = 'PAEEEMDB08A14440'

SELECT * FROM K_CREDITO_PRODUCTO KP INNER JOIN CRE_Credito as cre on
cre.no_credito = kp.No_credito
  JOIN CAT_PRODUCTO P ON KP.Cve_Producto = P.Cve_Producto
  JOIN CAT_TECNOLOGIA T ON P.Cve_Tecnologia = T.Cve_Tecnologia 
  WHERE T.Cve_Tecnologia = 5 and cre.No_credito = 'PAEEEMDB03A06892'
  --AND KP.No_Credito = 'PAEEEMDD11F07906'
