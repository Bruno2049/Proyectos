-- Credito
-- cliente
-- negocio
-- Direccion Fiscal
-- Direccion del Negocio
-- Referencia Obligado Solidario
-- Referencia Representante Legal
-- Referencias Notariales Acta Constitutiva
-- Referencia Notarial Acta de Matrimonio.

-- PAEEEMDX15T11053
Declare @id int = 18;
Declare @tipo int = 8;

--select * from dbo.CLI_Cliente where IdCliente = @id;

Select * FROM CLI_Tipo_Ref
--Select Cre.No_Credito,Cre.IdCliente,cre.Id_Proveedor, Cre.Id_Branch, Cre.IdNegocio,Neg.Nombre_Comercial, Cli.Nombre,
--Cli.Razon_Social,ref.Reg_Conyugal, Tipo.TipoReferencia
  
select  *
 From dbo.CRE_Credito as Cre
INNER JOIN dbo.CLI_Cliente as Cli on 
Cre.IdCliente = Cli.IdCliente and Cre.Id_Proveedor = Cli.Id_Proveedor and Cre.Id_Branch = Cli.Id_Branch
INNER JOIN dbo.CLI_Negocio  as Neg on 
Neg.IdCliente = Cli.IdCliente and Neg.Id_Proveedor = Cli.Id_Proveedor and Neg.Id_Branch = Cli.Id_Branch and Cre.IdNegocio = Neg.IdNegocio
INNER JOIN dbo.DIR_Direcciones as Dir on
Neg.IdCliente = Dir.IdCliente and Neg.Id_Proveedor = Dir.Id_Proveedor and Neg.Id_Branch = Dir.Id_Branch and Dir.IdNegocio = neg.IdNegocio and Dir.IdTipoDomicilio = 1
INNER JOIN dbo.DIR_Direcciones as DirFi on
Neg.IdCliente = DirFi.IdCliente and Neg.Id_Proveedor = DirFi.Id_Proveedor and Neg.Id_Branch = DirFi.Id_Branch and DirFi.IdNegocio = neg.IdNegocio and DirFi.IdTipoDomicilio = 2
INNER JOIN dbo.CLI_Ref_Cliente as Ref on
Ref.IdCliente = Dir.IdCliente and  Ref.Id_Proveedor = Dir.Id_Proveedor and Neg.Id_Branch = Dir.Id_Branch and Ref.IdNegocio = Dir.IdNegocio and Ref.IdTipoReferencia = 2 
Left JOIN dbo.CLI_Referencias_Notariales as Nota on
Ref.IdCliente = Nota.IdCliente and ref.IdNegocio = Nota.IdNegocio and Ref.Id_Proveedor = Nota.Id_Proveedor and Neg.Id_Branch = Nota.Id_Branch and Ref.IdTipoReferencia = 8
Left JOIN dbo.CLI_Referencias_Notariales as NotaCon on
Ref.IdCliente = NotaCon.IdCliente and ref.IdNegocio = NotaCon.IdNegocio and Ref.Id_Proveedor = NotaCon.Id_Proveedor and Neg.Id_Branch = NotaCon.Id_Branch and Ref.IdTipoReferencia = 7
--INNER JOIN dbo.CLI_Tipo_Ref AS Tipo  on
--Nota.IdTipoReferencia = Tipo.IdTipoReferencia*/
Where  Cre.No_Credito = 'PAEEEMDX15T11053' --and  Ref.IdTipoReferencia = @tipo;


/*Select * from  dbo.CLI_Negocio as Neg INNER JOIN  dbo.CLI_Cliente as Cli on
Cli.IdCliente = Neg.IdCliente and cli.Id_Branch = Neg.Id_Branch and Cli.Id_Proveedor = neg.Id_Proveedor
inner Join CRE_Credito as Cre on Cre.Id_Branch = Neg.Id_Branch and Cre.IdCliente = Neg.IdCliente 
and Neg.Id_Proveedor = Neg.Id_Proveedor INNER JOIN dbo.CAT_ESTATUS_CREDITO as Est on Cre.Cve_Estatus_Credito = Est.Cve_Estatus_Credito
where Cre.Cve_Estatus_Credito = 1 and Neg.IdNegocio = 2 and  Cli.IdCliente=@id
*/