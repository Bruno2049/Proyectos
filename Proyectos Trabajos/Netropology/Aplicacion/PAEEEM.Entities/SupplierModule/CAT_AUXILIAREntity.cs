/* ----------------------------------------------------------------------
 * File Name:CAT_AUXILIAREntity.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/7/29
 *
 * Description: CAT_AUXILIAR business entity
 *----------------------------------------------------------------------*/

using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class CAT_AUXILIAREntity
    {
        /// <summary>
        /// Id_Auxiliar
        /// </summary>
        private int _Id_Auxiliar;
        public int Id_Auxiliar
        {
            get { return _Id_Auxiliar; }
            set { _Id_Auxiliar = value; }
        }

        /// <summary>
        /// No_Credito
        /// </summary>
        private string  _No_Credito;
        public string No_Credito
        {
            get { return _No_Credito; }
            set { _No_Credito = value; }
        }

        /// <summary>
        /// Dx_Nombres
        /// </summary>
        private string _Dx_Nombres;
        public string Dx_Nombres
        {
            get { return _Dx_Nombres; }
            set { _Dx_Nombres = value; }
        }

        /// <summary>
        /// Dx_Apellido_Paterno
        /// </summary>
        private string _Dx_Apellido_Paterno;
        public string Dx_Apellido_Paterno
        {
            get { return _Dx_Apellido_Paterno; }
            set { _Dx_Apellido_Paterno = value; }
        }

        /// <summary>
        /// Dx_Apellido_Materno
        /// </summary>
        private string _Dx_Apellido_Materno;
        public string Dx_Apellido_Materno
        {
            get { return _Dx_Apellido_Materno; }
            set { _Dx_Apellido_Materno = value; }
        }

        /// <summary>
        /// Dt_Nacimiento_Fecha
        /// </summary>
        private object _Dt_Nacimiento_Fecha;
        public object Dt_Nacimiento_Fecha
        {
            get { return _Dt_Nacimiento_Fecha; }
            set
            {
                if (value == null)
                {
                    _Dt_Nacimiento_Fecha = DBNull.Value;
                }
                else
                {
                    _Dt_Nacimiento_Fecha = value;
                }
            }
        }

        ///// <summary>
        ///// Dx_Numero_Interior
        ///// </summary>
        //private string _Dx_Numero_Interior;
        //public string Dx_Numero_Interior
        //{
        //    get { return _Dx_Numero_Interior; }
        //    set { _Dx_Numero_Interior = value; }
        //}

        /// <summary>
        /// Dx_Ciudad
        /// </summary>
        private string _Dx_Ciudad;
        public string Dx_Ciudad
        {
            get { return _Dx_Ciudad; }
            set { _Dx_Ciudad = value; }
        }

        /// <summary>
        /// No_MOP
        /// </summary>
        private object _No_MOP;
        public object No_MOP
        {
            get { return _No_MOP; }
            set
            {
                if (value == null)
                {
                    _No_MOP = DBNull.Value;
                }
                else
                {
                    _No_MOP = value;
                }
            }
        }

        /// <summary>
        /// Ft_Folio
        /// </summary>
        private object _Ft_Folio;
        public object Ft_Folio
        {
            get { return _Ft_Folio; }
            set
            {
                if (value == null)
                {
                    _Ft_Folio = DBNull.Value;
                }
                else
                {
                    _Ft_Folio = value;
                }
            }
        }
    }
}
