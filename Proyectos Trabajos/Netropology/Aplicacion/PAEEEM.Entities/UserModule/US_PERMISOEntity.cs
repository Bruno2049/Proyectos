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
    public class US_PERMISOModel
 {
     ///<summary>
  ///
  ///</summary>
  public int Id_Permiso { get; set; }
   
     ///<summary>
  ///
  ///</summary>
  public string Estatus_Permiso { get; set; }
   
     ///<summary>
  ///
  ///</summary>
  public int Valor_Permiso { get; set; }
   
     ///<summary>
  ///
  ///</summary>
  public string Tipo_Permiso { get; set; }
   
 }
}
