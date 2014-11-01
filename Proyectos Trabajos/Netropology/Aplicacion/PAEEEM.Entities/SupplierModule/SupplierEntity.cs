using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entities
{
    public class SupplierEntity
    {
        private int _provider_id;
        public int Id_Proveedor
        {
            get { return _provider_id; }
            set { this._provider_id = value; }
        }

        private int _status;
        public int Cve_Estatus_Proveedor
        {
            get { return this._status; }
            set { this._status = value; }
        }

        private int _regional_id;
        public int Cve_Region
        {
            get { return _regional_id; }
            set { this._regional_id = value; }
        }

        private string _dx_razon_social;
        public string Dx_Razon_Social
        {
            get { return _dx_razon_social; }
            set { this._dx_razon_social = value; }
        }

        private string _dx_nombre_comercial;
        public string Dx_Nombre_Comercial
        {
            get { return _dx_nombre_comercial; }
            set { this._dx_nombre_comercial = value; }
        }

        private string _dx_rfc;
        public string Dx_RFC
        {
            get { return _dx_rfc; }
            set { this._dx_rfc = value; }
        }

        private string _dx_domicilio_part_calle;
        public string Dx_Domicilio_Part_Calle
        {
            get { return _dx_domicilio_part_calle; }
            set { this._dx_domicilio_part_calle = value; }
        }

        private string _dx_domicilio_part_num;
        public string Dx_Domicilio_Part_Num
        {
            get { return _dx_domicilio_part_num; }
            set { this._dx_domicilio_part_num = value; }
        }

        private string _dx_domicilio_part_cp;
        public string Dx_Domicilio_Part_CP
        {
            get { return _dx_domicilio_part_cp; }
            set { this._dx_domicilio_part_cp = value; }
        }

        private int _cve_deleg_municipio_part;
        public int Cve_Deleg_Municipio_Part
        {
            get { return _cve_deleg_municipio_part; }
            set { this._cve_deleg_municipio_part = value; }
        }

        private int _cve_estado_part;
        public int Cve_Estado_Part
        {
            get { return _cve_estado_part; }
            set { this._cve_estado_part = value; }
        }

        private bool _fg_mismo_domicilio;
        public bool Fg_Mismo_Domicilio
        {
            get { return _fg_mismo_domicilio; }
            set { this._fg_mismo_domicilio = value; }
        }

        private string _dx_domicilio_fiscal_calle;
        public string Dx_Domicilio_Fiscal_Calle
        {
            get { return _dx_domicilio_fiscal_calle; }
            set { this._dx_domicilio_fiscal_calle = value; }
        }

        private string _dx_domicilio_fiscal_num;
        public string Dx_Domicilio_Fiscal_Num
        {
            get { return _dx_domicilio_fiscal_num; }
            set { this._dx_domicilio_fiscal_num = value; }
        }

        private string _dx_domicilio_fiscal_cp;
        public string Dx_Domicilio_Fiscal_CP
        {
            get { return _dx_domicilio_fiscal_cp; }
            set { this._dx_domicilio_fiscal_cp = value; }
        }

        private int _cve_deleg_municipio_fisc;
        public int Cve_Deleg_Municipio_Fisc
        {
            get { return _cve_deleg_municipio_fisc; }
            set { this._cve_deleg_municipio_fisc = value; }
        }

        private int _cve_estado_fisc;
        public int Cve_Estado_Fisc
        {
            get { return _cve_estado_fisc; }
            set { this._cve_estado_fisc = value; }
        }

        private string _dx_nombre_repre;
        public string Dx_Nombre_Repre
        {
            get { return _dx_nombre_repre; }
            set { this._dx_nombre_repre = value; }
        }

        private string _dx_email_repre;
        public string Dx_Email_Repre
        {
            get { return _dx_email_repre; }
            set { this._dx_email_repre = value; }
        }

        private string _dx_telefono_repre;
        public string Dx_Telefono_Repre
        {
            get { return _dx_telefono_repre; }
            set { this._dx_telefono_repre = value; }
        }

        private string _dx_nombre_repre_legal;
        public string Dx_Nombre_Repre_Legal
        {
            get { return _dx_nombre_repre_legal; }
            set { this._dx_nombre_repre_legal = value; }
        }

        private string _dx_nombre_banco;
        public string Dx_Nombre_Banco
        {
            get { return _dx_nombre_banco; }
            set { this._dx_nombre_banco = value; }
        }

        private string _dx_cuenta_banco;
        public string Dx_Cuenta_Banco
        {
            get { return _dx_cuenta_banco; }
            set { this._dx_cuenta_banco = value; }
        }

        private byte[] _binary_acta_constitutiva;
        public byte[] Binary_Acta_Constitutiva
        {
            get { return _binary_acta_constitutiva; }
            set { this._binary_acta_constitutiva = value; }
        }

        private byte[] _binary_poder_notarial;
        public byte[] Binary_Poder_Notarial
        {
            get { return _binary_poder_notarial; }
            set { this._binary_poder_notarial = value; }
        }

        private float _pct_tasa_iva;
        public float Pct_Tasa_IVA
        {
            get { return this._pct_tasa_iva; }
            set { this._pct_tasa_iva = value; }
        }

        private object _dt_fecha_proveedor;
        public object Dt_Fecha_Proveedor
        {
            get { return this._dt_fecha_proveedor; }
            set
            {
                if (null == value)
                {
                    this._dt_fecha_proveedor = DBNull.Value;
                }
                else
                {
                    this._dt_fecha_proveedor = value; 
                }
            }
        }

        private int _Cve_Zona;
        public int Cve_Zona
        {
            get { return this._Cve_Zona; }
            set { this._Cve_Zona = value; }
        }
    }
}
