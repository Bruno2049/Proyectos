SELECT cli.Cve_Tipo_Sociedad, cre.No_Credito, Cli.Razon_Social, cli.Nombre + ' ' + cli.Ap_Paterno + ' ' + cli.Ap_Materno as 'Cliente', ref.Nombre + ' ' +ref.Ap_Paterno+' '+ Ref.Ap_Materno as 'Legal', Cli.Nombre,cli.Ap_Paterno,cli.Ap_Materno
FROM CRE_Credito AS Cre INNER JOIN CLI_Negocio as Neg on
Cre.Id_Proveedor = Neg.Id_Proveedor  and Cre.Id_Branch = Neg.Id_Branch and Cre.IdCliente = Neg.IdCliente and Cre.IdNegocio = Neg.IdNegocio
INNER JOIN CLI_Cliente as Cli on
Cli.Id_Proveedor = Neg.Id_Proveedor  and Cli.Id_Branch = Neg.Id_Branch and Cli.IdCliente = Neg.IdCliente
INNER JOIN DIR_Direcciones as Dir on
dir.Id_Proveedor = Neg.Id_Proveedor  and dir.Id_Branch = Neg.Id_Branch and dir.IdCliente = Neg.IdCliente and dir.IdNegocio = Neg.IdNegocio
INNER JOIN CLI_Ref_Cliente as Ref on
dir.Id_Proveedor = Ref.Id_Proveedor  and dir.Id_Branch = Ref.Id_Branch and dir.IdCliente = Ref.IdCliente and dir.IdNegocio = Ref.IdNegocio and ref.IdTipoReferencia = 1
--Where 
--cre.No_Credito = 'PAEEEMDB01A11130' 
--cli.Cve_Tipo_Sociedad = 2 
 --and cli.Razon_Social is not null