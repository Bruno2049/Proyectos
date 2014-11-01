/* ----------------------------------------------------------------------
 * File Name: K_CREDITO_SUSTITUCIONModel.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/6/27
 *
 * Description: K_CREDITO_SUSTITUCION business entity
 *----------------------------------------------------------------------*/

using System;


namespace PAEEEM.Entities
{
    [Serializable()]
    public class K_CREDITO_SUSTITUCIONModel
    {
        ///<summary>
        ///
        ///</summary>
        private string _No_Credito;
        public string No_Credito
        {
            get { return _No_Credito; }
            set { this._No_Credito = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private int _Cve_Tecnologia;
        public int Cve_Tecnologia
        {
            get { return _Cve_Tecnologia; }
            set { this._Cve_Tecnologia = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private object _No_Unidades;
        public object No_Unidades
        {
            get { return _No_Unidades; }
            set
            {
                if (null == value)
                {
                    this._No_Unidades = DBNull.Value;
                }
                else
                {
                    this._No_Unidades = value;
                }
            }
        }
        ///<summary>
        ///
        ///</summary>
        private object _Id_Centro_Disp;
        public object Id_Centro_Disp
        {
            get { return _Id_Centro_Disp; }
            set
            {
                if (null == value)
                {
                    this._Id_Centro_Disp = DBNull.Value;
                }
                else
                {
                    this._Id_Centro_Disp = value;
                }
            }
        }
        ///<summary>
        ///
        ///</summary>
        private DateTime _Dt_Fecha_Credito_Sustitucion;
        public DateTime Dt_Fecha_Credito_Sustitucion
        {
            get { return _Dt_Fecha_Credito_Sustitucion; }
            set { this._Dt_Fecha_Credito_Sustitucion = value; }
        }
        //updated by tina 2012/04/13
        private object _Dx_Tipo_Producto;
        public object Dx_Tipo_Producto
        {
            get { return _Dx_Tipo_Producto; }
            set
            {
                if (null == value)
                {
                    this._Dx_Tipo_Producto = DBNull.Value;
                }
                else
                {
                    this._Dx_Tipo_Producto = value;
                }
            }
        }
        //end

        private string _Dx_Modelo_Producto;
        public string Dx_Modelo_Producto
        {
            get { return _Dx_Modelo_Producto; }
            set { this._Dx_Modelo_Producto = value; }
        }

        private string _Dx_Marca;
        public string Dx_Marca
        {
            get { return _Dx_Marca; }
            set { this._Dx_Marca = value; }
        }

        private string _No_Serial;
        public string No_Serial
        {
            get { return _No_Serial; }
            set { this._No_Serial = value; }
        }

        private int _Id_Credito_Sustitucion;
        public int Id_Credito_Sustitucion
        {
            get { return _Id_Credito_Sustitucion; }
            set { this._Id_Credito_Sustitucion = value; }
        }

        private string _Dx_Color;
        public string Dx_Color
        {
            get { return _Dx_Color; }
            set { this._Dx_Color = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private object _No_Peso;
        public object No_Peso
        {
            get { return _No_Peso; }
            set
            {
                if (null == value)
                {
                    this._No_Peso = DBNull.Value;
                }
                else
                {
                    this._No_Peso = value;
                }
            }
        }

        private object _Cve_Capacidad_Sust;
        public object Cve_Capacidad_Sust
        {
            get { return _Cve_Capacidad_Sust; }
            set
            {
                if (null == value)
                {
                    this._Cve_Capacidad_Sust = DBNull.Value;
                }
                else
                {
                    this._Cve_Capacidad_Sust = value;
                }
            }
        }

        private string _Fg_Tipo_Centro_Disp;
        public string Fg_Tipo_Centro_Disp
        {
            get { return _Fg_Tipo_Centro_Disp; }
            set { this._Fg_Tipo_Centro_Disp = value; }
        }

        private string _Dx_Antiguedad;
        public string Dx_Antiguedad
        {
            get { return _Dx_Antiguedad; }
            set { this._Dx_Antiguedad = value; }
        }

        private object _Fg_Si_Funciona;
        public object Fg_Si_Funciona
        {
            get { return _Fg_Si_Funciona; }
            set
            {
                if (null == value)
                {
                    this._Fg_Si_Funciona = DBNull.Value;
                }
                else
                {
                    this._Fg_Si_Funciona = value;
                }
            }
        }

        private DateTime _Dt_Fecha_Recepcion;
        public DateTime Dt_Fecha_Recepcion
        {
            get { return _Dt_Fecha_Recepcion; }
            set { this._Dt_Fecha_Recepcion = value; }
        }

        private string _Dx_Imagen_Recepcion;
        public string Dx_Imagen_Recepcion
        {
            get { return _Dx_Imagen_Recepcion; }
            set { this._Dx_Imagen_Recepcion = value; }
        }

        private string _Dx_Imagen_Recuperacion;
        public string Dx_Imagen_Recuperacion
        {
            get { return _Dx_Imagen_Recuperacion; }
            set { this._Dx_Imagen_Recuperacion = value; }
        }

        private string _Dx_Imagen_Inhabilitacion;
        public string Dx_Imagen_Inhabilitacion
        {
            get { return _Dx_Imagen_Inhabilitacion; }
            set { this._Dx_Imagen_Inhabilitacion = value; }
        }

        private string _Id_Folio;
        public string Id_Folio
        {
            get { return _Id_Folio; }
            set { this._Id_Folio = value; }
        }

        private string _Id_Pre_Folio;
        public string Id_Pre_Folio
        {
            get { return _Id_Pre_Folio; }
            set { this._Id_Pre_Folio = value; }
        }
    }
}
