SELECT * FROM CAT_TIPO_SOCIEDAD

SELECT cc.No_Credito, CC.RPU, RD.ServiceCode, CC.Cve_Estatus_Credito FROM ResponseData RD 
left OUTER join CRE_Credito CC ON RD.ServiceCode = CC.No_Credito
WHERE Zone= 'DD10' --RPU=992060700128 and 

SELECT No_Credito, RPU, Cve_Estatus_Credito FROM CRE_CREDITO 
WHERE RPU = 999100600185
ORDER BY RPU