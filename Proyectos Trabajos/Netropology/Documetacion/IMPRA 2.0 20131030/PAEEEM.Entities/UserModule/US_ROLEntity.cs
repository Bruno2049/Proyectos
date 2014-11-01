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
    public class US_ROLModel
 {
     ///<summary>
  ///role id
  ///</summary>
  public int Id_Rol { get; set; }
   
     ///<summary>
  ///role name
  ///</summary>
  public string Nombre_Rol { get; set; }
   
     ///<summary>
  ///role relation
  ///</summary>
  public string Relacion_Rol { get; set; }
   
 }
}
