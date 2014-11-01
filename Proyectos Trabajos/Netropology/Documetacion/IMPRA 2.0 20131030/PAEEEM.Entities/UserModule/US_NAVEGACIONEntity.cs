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
    public class US_NAVEGACIONModel
 {
     ///<summary>
  ///navigation id
  ///</summary>
  public int Id_Navegacion { get; set; }
   
     ///<summary>
  ///navigation name
  ///</summary>
  public string Nombre_Navegacion { get; set; }
   
     ///<summary>
  ///navigation url
  ///</summary>
  public string Url_Navegacion { get; set; }
   
     ///<summary>
  ///parent code
  ///</summary>
  public string Codigo_Padres { get; set; }
   
     ///<summary>
  ///parent path
  ///</summary>
  public string Ruta_Padres { get; set; }
   
     ///<summary>
  ///
  ///</summary>
  public string Estatus { get; set; }
   
     ///<summary>
  ///Navigation level
  ///</summary>
  public int Nivel_Navegacion { get; set; }
   
     ///<summary>
  ///Sequence
  ///</summary>
  public string Secuencia { get; set; }
   
 }
}
