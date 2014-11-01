declare @No_credito varchar= 'PAEEEMDA01A18022'

SELECT * FROM K_CREDITO_PRODUCTO as CreditoProducton 
join CRE_Credito as c on CreditoProducton.No_Credito = c.No_Credito
join K_PROVEEDOR_PRODUCTO as d on c.Id_Proveedor = d.Id_Proveedor
join CAT_PRODUCTO as p on CreditoProducton.Cve_Producto = p.Cve_Producto
join CAT_TECNOLOGIA as t on p.Cve_Tecnologia = t.Cve_Tecnologia
join CAT_MARCA as m on p.Cve_Marca = m.Cve_Marca
where
CreditoProducton.No_Credito = @No_Credito and CreditoProducton.Cve_Producto = d.Cve_Producto