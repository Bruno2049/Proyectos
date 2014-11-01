/*
	Copyright IMPRA, Inc. 2010
	All rights are reserved. Reproduction or transmission in whole or in part,
      in any form or by any means, electronic, mechanical or otherwise, is 
prohibited without the prior written consent of the copyright owner.

	$Archive:    $
	$Revision:   $
	$Author:     $
	$Date:       $
	Log at end of file
*/
namespace PAEEEM.Helpers
{
    /// <summary>
    /// Credit Request Status
    /// </summary>
    public enum CreditStatus
    {
        PENDIENTE = 1,
        PORENTREGAR = 2,
        ENREVISION = 3,
        PRE_AUTORIZADO = 11 ,
        AUTORIZADO = 4,
        RECHAZADO = 5,
        PARAFINANZAS = 6,
        CANCELADO = 7,
        BENEFICIARIO_CON_ADEUDOS = 8,
        TARIFA_FUERA_DE_PROGRAMA = 9,
        Calificación_MOP_no_válida = 10,
        CLASIFICACION_PYME_INCORRECTA = 11
    }
    /// <summary>
    /// Disposal Center Status
    /// </summary>
    public enum DisposalCenterStatus
    {
        PENDIENTE = 1,
        ACTIVO = 2,
        INACTIVO = 3,
        CANCELADO = 4
    }
    /// <summary>
    /// Provider Status
    /// </summary>
    public enum ProviderStatus
    {
        PENDIENTE = 1,
        ACTIVO = 2,
        INACTIVO = 3,
        CANCELADO = 4
    }
    /// <summary>
    /// Product Status
    /// </summary>
    public enum ProductStatus
    {
        ACTIVO = 1,
        INACTIVO = 2,
        PENDIENTE = 4,
        CANCELADO = 3

    }
    /// <summary>
    /// Roles of users
    /// </summary>
    public enum UserRole
    { 
        CENTRALOFFICE = 1,
        REGIONAL = 6,
        SUPPLIER = 3,
        MANUFACTURER =4,
        DISPOSALCENTER = 5,
        ZONE = 2,
        EXECUTIVE = 7,
        CFE = 8,
        ADMINCAYD = 9,
        SUPERCAYD = 10,
        VENDEDOR = 18
    }
    /// <summary>
    /// Company Type
    /// </summary>
    public enum CompanyType
    {
        PERSONAFISICA = 1,
        MORAL = 2,
        REPECO = 3,
        ASOCIACIONCIVIL = 4,
        SOCIEDADCIVIL = 5,
        ASOCIACIONRELIGIOSA = 6,
        SOCIEDADANONIMADECAPITALVARIABLE = 7
    }
    /// <summary>
    /// Disposal Status
    /// </summary>
    public enum DisposalStatus
    {
        ENRECEPCION = 1,
        PARAINHABILITACION = 2,
        INHABILITADO = 3,
        RECUPERACIONDERESIDUOS = 4,
        PENDIENTE = 5,
        COMPLETADO = 6
    }
}
