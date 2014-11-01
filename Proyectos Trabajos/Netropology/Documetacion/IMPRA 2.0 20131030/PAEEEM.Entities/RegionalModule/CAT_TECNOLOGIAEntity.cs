/* ----------------------------------------------------------------------
 * File Name: CAT_TECNOLOGIAModel.cs
 * 
 * Create Author: Eric
 * 
 * Create DateTime: 2011/7/5
 *
 * Description: CAT_TECNOLOGIA business entity
 *----------------------------------------------------------------------*/

using System;
namespace PAEEEM.Entities
{
 [Serializable()]
    public class CAT_TECNOLOGIAModel
    {
        ///<summary>
        ///
        ///</summary>
        public int Cve_Tecnologia { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Nombre_General { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Nombre_Particular { get; set; }

        ///<summary>
        ///
        ///</summary>
        public DateTime Dt_Fecha_Tecnologoia { get; set; }

        public int Cve_Tipo_Tecnologia { get; set; }

        public string Dx_Cve_CC { get; set; }
    }
}
