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

using System;
namespace PAEEEM.Entities
{
 [Serializable()]
    public class US_USUARIOModel
 {
     ///<summary>
     ///user id
     ///</summary>
     public int Id_Usuario { get; set; }

     ///<summary>
  ///role id
  ///</summary>
  public int Id_Rol { get; set; }

  ///<summary>
  /// Nombre Completo de Usuario
  ///</summary>
  public string Nombre_Completo_Usuario { get; set; }

  ///<summary>
  ///user name
  ///</summary>
  public string Nombre_Usuario { get; set; }
   
     ///<summary>
  ///user type
  ///</summary>
  public string Tipo_Usuario { get; set; }
   
     ///<summary>
  ///password
  ///</summary>
  public string Contrasena { get; set; }
   
     ///<summary>
  ///email
  ///</summary>
  public string CorreoElectronico { get; set; }
   
     ///<summary>
  ///phone number
  ///</summary>
  public string Numero_Telefono { get; set; }
   
     ///<summary>
  ///address
  ///</summary>
  public string Direccion { get; set; }
   
     ///<summary>
  ///status
  ///</summary>
  public string Estatus { get; set; }
   
     ///<summary>
  ///department id
  ///</summary>
  public int Id_Departamento { get; set; }
   
 }
}
