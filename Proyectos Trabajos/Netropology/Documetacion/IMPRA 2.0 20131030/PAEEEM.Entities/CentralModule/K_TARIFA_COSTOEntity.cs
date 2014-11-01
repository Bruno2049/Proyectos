/* ----------------------------------------------------------------------
 * File Name: K_TARIFA_COSTOModel.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/6/27
 *
 * Description: K_TARIFA_COSTO business entity
 *----------------------------------------------------------------------*/
using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class K_TARIFA_COSTOModel
    {
        ///<summary>
        ///
        ///</summary>
        private int _Fl_Tarifa_Costo;
        public int Fl_Tarifa_Costo
        {
            get { return _Fl_Tarifa_Costo; }
            set { this._Fl_Tarifa_Costo = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private object _Cve_Tarifa;
        public object Cve_Tarifa
        {
            get { return _Cve_Tarifa; }
            set
            {
                if (null == value)
                {
                    this._Cve_Tarifa = DBNull.Value;
                }
                else
                {
                    this._Cve_Tarifa = value;
                }
            }
        }
        ///<summary>
        ///
        ///</summary>
        private decimal _Mt_Costo_Kw_h_Fijo;
        public decimal Mt_Costo_Kw_h_Fijo
        {
            get { return _Mt_Costo_Kw_h_Fijo; }
            set { this._Mt_Costo_Kw_h_Fijo = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private decimal _Mt_Costo_Kw_h_Basico;
        public decimal Mt_Costo_Kw_h_Basico
        {
            get { return _Mt_Costo_Kw_h_Basico; }
            set { this._Mt_Costo_Kw_h_Basico = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private decimal _Mt_Costo_Kw_h_Intermedio;
        public decimal Mt_Costo_Kw_h_Intermedio
        {
            get { return _Mt_Costo_Kw_h_Intermedio; }
            set { this._Mt_Costo_Kw_h_Intermedio = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private decimal _Mt_Costo_Kw_h_Excedente;
        public decimal Mt_Costo_Kw_h_Excedente
        {
            get { return _Mt_Costo_Kw_h_Excedente; }
            set { this._Mt_Costo_Kw_h_Excedente = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private DateTime _Dt_Periodo_Tarifa_Costo;
        public DateTime Dt_Periodo_Tarifa_Costo
        {
            get { return _Dt_Periodo_Tarifa_Costo; }
            set { this._Dt_Periodo_Tarifa_Costo = value; }
        }
        ///<summary>
        ///
        ///</summary>
        private object _Cve_Estado;
        public object Cve_Estado
        {
            get { return _Cve_Estado; }
            set
            {
                if (null == value)
                {
                    this._Cve_Estado = DBNull.Value;
                }
                else
                {
                    this._Cve_Estado = value;
                }
            }
        }        
        ///<summary>
        ///
        ///</summary>
        private DateTime _Dt_Fecha_UltMod;
        public DateTime Dt_Fecha_UltMod
        {
            get { return _Dt_Fecha_UltMod; }
            set { this._Dt_Fecha_UltMod = value; }
        }
        private decimal _MT_Tarifa_Demanda;

        public decimal MT_Tarifa_Demanda
        {
            get { return _MT_Tarifa_Demanda; }
            set { _MT_Tarifa_Demanda = value; }
        }

        private decimal _MT_Costo_Tarifa_Consumo;

        public decimal MT_Costo_Tarifa_Consumo
        {
            get { return _MT_Costo_Tarifa_Consumo; }
            set { _MT_Costo_Tarifa_Consumo = value; }
        }
    }
}
