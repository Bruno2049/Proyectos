/* ----------------------------------------------------------------------
 * File Name: CAT_PROVEEDORModel.cs
 * 
 * Create Author: Eric
 * 
 * Create DateTime: 2011/7/5
 *
 * Description: CAT_PROVEEDOR business entity
 *----------------------------------------------------------------------*/

using System;
namespace PAEEEM.Entities
{
    [Serializable()]
    public class CAT_PROVEEDORModel
    {
        ///<summary>
        ///
        ///</summary>
        public int Id_Proveedor { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Cve_Estatus_Proveedor { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Cve_Region { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Razon_Social { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Nombre_Comercial { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_RFC { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Domicilio_Part_Calle { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Domicilio_Part_Num { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Domicilio_Part_CP { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Cve_Deleg_Municipio_Part { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Cve_Estado_Part { get; set; }

        ///<summary>
        ///
        ///</summary>
        public bool Fg_Mismo_Domicilio { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Domicilio_Fiscal_Calle { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Domicilio_Fiscal_Num { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Domicilio_Fiscal_CP { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Cve_Deleg_Municipio_Fisc { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Cve_Estado_Fisc { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Nombre_Repre { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Email_Repre { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Telefono_Repre { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Nombre_Repre_Legal { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Nombre_Banco { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Dx_Cuenta_Banco { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Binary_Acta_Constitutiva { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Binary_Poder_Notarial { get; set; }

        ///<summary>
        ///
        ///</summary>
        public double Pct_Tasa_IVA { get; set; }

        ///<summary>
        ///
        ///</summary>
        public object Dt_Fecha_Proveedor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Cve_Zona { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Codigo_Proveedor { get; set; }
    }
}
