/* ----------------------------------------------------------------------
 * File Name: CAT_PRODUCTOModel.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/11/30
 *
 * Description: CAT_PRODUCTOModel business entity
 *----------------------------------------------------------------------*/
using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class CAT_PRODUCTOModel
    {
        /// <summary>
        /// Cve_Producto
        /// </summary>
        private int _Cve_Producto;
        public int Cve_Producto
        {
            get { return _Cve_Producto; }
            set { this._Cve_Producto = value; }
        }
        ///<summary>
        ///Dx_Producto_Code
        ///</summary>
        private string _Dx_Producto_Code;
        public string Dx_Producto_Code
        {
            get { return _Dx_Producto_Code; }
            set { this._Dx_Producto_Code = value; }
        }
        /// <summary>
        /// Cve_Tecnologia
        /// </summary>
        private int _Cve_Tecnologia;
        public int Cve_Tecnologia
        {
            get { return _Cve_Tecnologia; }
            set { this._Cve_Tecnologia = value; }
        }
        /// <summary>
        /// Cve_Fabricante
        /// </summary>
        private int _Cve_Fabricante;
        public int Cve_Fabricante
        {
            get { return _Cve_Fabricante; }
            set { this._Cve_Fabricante = value; }
        }
        /// <summary>
        /// Cve_Marca
        /// </summary>
        private int _Cve_Marca;
        public int Cve_Marca
        {
            get { return _Cve_Marca; }
            set {this._Cve_Marca=value;}
        }
        /// <summary>
        /// Cve_Estatus_Producto
        /// </summary>
        private int _Cve_Estatus_Producto;
        public int Cve_Estatus_Producto
        {
            get { return _Cve_Estatus_Producto; }
            set { this._Cve_Estatus_Producto = value; }
        }
        /// <summary>
        /// Dx_Nombre_Producto
        /// </summary>
        private string _Dx_Nombre_Producto;
        public string Dx_Nombre_Producto
        {
            get { return _Dx_Nombre_Producto; }
            set { this._Dx_Nombre_Producto = value; }
        }
        /// <summary>
        /// Dx_Modelo_Producto
        /// </summary>
        private string _Dx_Modelo_Producto;
        public string Dx_Modelo_Producto
        {
            get { return _Dx_Modelo_Producto; }
            set { this._Dx_Modelo_Producto = value; }
        }
        /// <summary>
        /// Mt_Precio_Max
        /// </summary>
        private decimal _Mt_Precio_Max;
        public decimal Mt_Precio_Max
        {
            get { return _Mt_Precio_Max; }
            set { this._Mt_Precio_Max = value; }
        }
        /// <summary>
        /// No_Eficiencia_Energia
        /// </summary>
        private float _No_Eficiencia_Energia;
        public float No_Eficiencia_Energia
        {
            get { return _No_Eficiencia_Energia; }
            set { this._No_Eficiencia_Energia = value; }
         }
        /// <summary>
        /// No_Max_Consumo_24h
        /// </summary>
        private float _No_Max_Consumo_24h;
        public float No_Max_Consumo_24h
        {
            get { return _No_Max_Consumo_24h; }
            set { this._No_Max_Consumo_24h = value; }
        }
        // updated by Tina 2012-02-24
        /// <summary>
        /// No_Capacidad
        /// </summary>
        private int _Cve_Capacidad_Sust;
        public int Cve_Capacidad_Sust
        {
            get { return _Cve_Capacidad_Sust; }
            set { this._Cve_Capacidad_Sust = value; }
        }
        // end
        /// <summary>
        /// Dt_Fecha_Producto
        /// </summary>
        private DateTime _Dt_Fecha_Producto;
        public DateTime Dt_Fecha_Producto
        {
            get { return _Dt_Fecha_Producto; }
            set { this._Dt_Fecha_Producto = value; }
        }
        /// <summary>
        /// Ft_Tipo_Producto
        /// </summary>
        private int _Ft_Tipo_Producto;
        public int Ft_Tipo_Producto
        {
            get { return _Ft_Tipo_Producto; }
            set { this._Ft_Tipo_Producto = value; }
        }
        /// <summary>
        /// Mt_Precio_Max
        /// </summary>
        private decimal _Mt_Precio_Unitario;
        public decimal Mt_Precio_Unitario
        {
            get { return _Mt_Precio_Unitario; }
            set { this._Mt_Precio_Unitario = value; }
        }

        private float _Ahorro_Consumo;

        public float Ahorro_Consumo
        {
            get { return _Ahorro_Consumo; }
            set { _Ahorro_Consumo = value; }
        }

        private float _Ahorro_Demanda;

        public float Ahorro_Demanda
        {
            get { return _Ahorro_Demanda; }
            set { _Ahorro_Demanda = value; }
        }

        // RSA 2012-09-13 SE attributes Start

        /// <summary>
        /// Cve_Tipo_SE
        /// </summary>
        private int _Cve_Tipo_SE;
        public int Cve_Tipo_SE
        {
            get { return _Cve_Tipo_SE; }
            set { this._Cve_Tipo_SE = value; }
        }
        /// <summary>
        /// Dx_Tipo_SE
        /// </summary>
        private string _Dx_Tipo_SE;
        public string Dx_Tipo_SE
        {
            get { return _Dx_Tipo_SE; }
            set { this._Dx_Tipo_SE = value; }
        }
        /// <summary>
        /// Cve_Transformador_SE
        /// </summary>
        private int _Cve_Transformador_SE;
        public int Cve_Transformador_SE
        {
            get { return _Cve_Transformador_SE; }
            set { this._Cve_Transformador_SE = value; }
        }
        /// <summary>
        /// Dx_Transformador_SE
        /// </summary>
        private string _Dx_Transformador_SE;
        public string Dx_Transformador_SE
        {
            get { return _Dx_Transformador_SE; }
            set { this._Dx_Transformador_SE = value; }
        }
        /// <summary>
        /// Cve_Fase_Transformador
        /// </summary>
        private int _Cve_Fase_Transformador;
        public int Cve_Fase_Transformador
        {
            get { return _Cve_Fase_Transformador; }
            set { this._Cve_Fase_Transformador = value; }
        }
        /// <summary>
        /// Dx_Fase_Transformador
        /// </summary>
        private string _Dx_Fase_Transformador;
        public string Dx_Fase_Transformador
        {
            get { return _Dx_Fase_Transformador; }
            set { this._Dx_Fase_Transformador = value; }
        }
        /// <summary>
        /// Cve_Marca_Transform
        /// </summary>
        private int _Cve_Marca_Transform;
        public int Cve_Marca_Transform
        {
            get { return _Cve_Marca_Transform; }
            set { this._Cve_Marca_Transform = value; }
        }
        /// <summary>
        /// Dx_Marca_Transform
        /// </summary>
        private string _Dx_Marca_Transform;
        public string Dx_Marca_Transform
        {
            get { return _Dx_Marca_Transform; }
            set { this._Dx_Marca_Transform = value; }
        }
        /// <summary>
        /// Cve_Relacion_Transform
        /// </summary>
        private int _Cve_Relacion_Transform;
        public int Cve_Relacion_Transform
        {
            get { return _Cve_Relacion_Transform; }
            set { this._Cve_Relacion_Transform = value; }
        }
        /// <summary>
        /// Dx_Relacion_Transform
        /// </summary>
        private string _Dx_Relacion_Transform;
        public string Dx_Relacion_Transform
        {
            get { return _Dx_Relacion_Transform; }
            set { this._Dx_Relacion_Transform = value; }
        }
        /// <summary>
        /// Cve_Apartarrayos_SE
        /// </summary>
        private int _Cve_Apartarrayos_SE;
        public int Cve_Apartarrayos_SE
        {
            get { return _Cve_Apartarrayos_SE; }
            set { this._Cve_Apartarrayos_SE = value; }
        }
        /// <summary>
        /// Dx_Apartarrayos_SE
        /// </summary>
        private string _Dx_Apartarrayos_SE;
        public string Dx_Apartarrayos_SE
        {
            get { return _Dx_Apartarrayos_SE; }
            set { this._Dx_Apartarrayos_SE = value; }
        }
        /// <summary>
        /// Cve_Marca_Apartar
        /// </summary>
        private int _Cve_Marca_Apartar;
        public int Cve_Marca_Apartar
        {
            get { return _Cve_Marca_Apartar; }
            set { this._Cve_Marca_Apartar = value; }
        }
        /// <summary>
        /// Dx_Marcar_Apartar
        /// </summary>
        private string _Dx_Marcar_Apartar;
        public string Dx_Marcar_Apartar
        {
            get { return _Dx_Marcar_Apartar; }
            set { this._Dx_Marcar_Apartar = value; }
        }
        /// <summary>
        /// Cve_Cortacircuito_SE
        /// </summary>
        private int _Cve_Cortacircuito_SE;
        public int Cve_Cortacircuito_SE
        {
            get { return _Cve_Cortacircuito_SE; }
            set { this._Cve_Cortacircuito_SE = value; }
        }
        /// <summary>
        /// Dx_Cortacircuito_SE
        /// </summary>
        private string _Dx_Cortacircuito_SE;
        public string Dx_Cortacircuito_SE
        {
            get { return _Dx_Cortacircuito_SE; }
            set { this._Dx_Cortacircuito_SE = value; }
        }
        /// <summary>
        /// Cve_Marca_Cortacirc
        /// </summary>
        private int _Cve_Marca_Cortacirc;
        public int Cve_Marca_Cortacirc
        {
            get { return _Cve_Marca_Cortacirc; }
            set { this._Cve_Marca_Cortacirc = value; }
        }
        /// <summary>
        /// Dx_Marca_Cortacirc
        /// </summary>
        private string _Dx_Marca_Cortacirc;
        public string Dx_Marca_Cortacirc
        {
            get { return _Dx_Marca_Cortacirc; }
            set { this._Dx_Marca_Cortacirc = value; }
        }
        /// <summary>
        /// Cve_Interruptor_SE
        /// </summary>
        private int _Cve_Interruptor_SE;
        public int Cve_Interruptor_SE
        {
            get { return _Cve_Interruptor_SE; }
            set { this._Cve_Interruptor_SE = value; }
        }
        /// <summary>
        /// Dx_Interruptor_SE
        /// </summary>
        private string _Dx_Interruptor_SE;
        public string Dx_Interruptor_SE
        {
            get { return _Dx_Interruptor_SE; }
            set { this._Dx_Interruptor_SE = value; }
        }
        /// <summary>
        /// Cve_Marca_Interrup
        /// </summary>
        private int _Cve_Marca_Interrup;
        public int Cve_Marca_Interrup
        {
            get { return _Cve_Marca_Interrup; }
            set { this._Cve_Marca_Interrup = value; }
        }
        /// <summary>
        /// Dx_Marca_Interrup
        /// </summary>
        private string _Dx_Marca_Interrup;
        public string Dx_Marca_Interrup
        {
            get { return _Dx_Marca_Interrup; }
            set { this._Dx_Marca_Interrup = value; }
        }
        /// <summary>
        /// Cve_Conductor_SE
        /// </summary>
        private int _Cve_Conductor_SE;
        public int Cve_Conductor_SE
        {
            get { return _Cve_Conductor_SE; }
            set { this._Cve_Conductor_SE = value; }
        }
        /// <summary>
        /// Dx_Conductor_SE
        /// </summary>
        private string _Dx_Conductor_SE;
        public string Dx_Conductor_SE
        {
            get { return _Dx_Conductor_SE; }
            set { this._Dx_Conductor_SE = value; }
        }
        /// <summary>
        /// Cve_Marca_Conductor
        /// </summary>
        private int _Cve_Marca_Conductor;
        public int Cve_Marca_Conductor
        {
            get { return _Cve_Marca_Conductor; }
            set { this._Cve_Marca_Conductor = value; }
        }
        /// <summary>
        /// Dx_Marca_Conductor
        /// </summary>
        private string _Dx_Marca_Conductor;
        public string Dx_Marca_Conductor
        {
            get { return _Dx_Marca_Conductor; }
            set { this._Dx_Marca_Conductor = value; }
        }
        /// <summary>
        /// Cve_Cond_Conex_SE
        /// </summary>
        private int _Cve_Cond_Conex_SE;
        public int Cve_Cond_Conex_SE
        {
            get { return _Cve_Cond_Conex_SE; }
            set { this._Cve_Cond_Conex_SE = value; }
        }
        /// <summary>
        /// Dx_Cond_Conex_SE
        /// </summary>
        private string _Dx_Cond_Conex_SE;
        public string Dx_Cond_Conex_SE
        {
            get { return _Dx_Cond_Conex_SE; }
            set { this._Dx_Cond_Conex_SE = value; }
        }
        /// <summary>
        /// Cve_Cond_Conex_Mca
        /// </summary>
        private int _Cve_Cond_Conex_Mca;
        public int Cve_Cond_Conex_Mca
        {
            get { return _Cve_Cond_Conex_Mca; }
            set { this._Cve_Cond_Conex_Mca = value; }
        }
        /// <summary>
        /// Dx_Cond_Conex_Mca
        /// </summary>
        private string _Dx_Cond_Conex_Mca;
        public string Dx_Cond_Conex_Mca
        {
            get { return _Dx_Cond_Conex_Mca; }
            set { this._Dx_Cond_Conex_Mca = value; }
        }
        // RSA 2012-09-13 SE attributes End
    }
}
