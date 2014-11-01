--select * from CAT_TECNOLOGIA

--SELECT * FROM CAT_TIPO_PRODUCTO

  DECLARE @No_Credito varchar(max) = 'PAEEEMDF45A03098'


  SELECT  SUM(KPP.No_Cantidad) As Cantidad
   --KPP.No_Credito, KPP.No_Cantidad,CP.Cve_Tecnologia
  FROM K_CREDITO_PRODUCTO KPP 
  INNER JOIN CAT_PRODUCTO CP ON KPP.Cve_Producto = CP.Cve_Producto 
  WHERE KPP.No_Credito = @No_Credito and CP.Cve_Tecnologia = 1 
  Group by CP.Cve_Tecnologia



  SELECT ISNULL(SUM(KCS.No_Unidades),0) FROM K_CREDITO_SUSTITUCION As KCS
  INNER JOIN CAT_TECNOLOGIA AS CT ON KCS.Cve_Tecnologia=CT.Cve_Tecnologia and CT.Cve_Tecnologia=1
    WHERE KCS.No_Credito = @No_Credito group by CT.Cve_Tecnologia
