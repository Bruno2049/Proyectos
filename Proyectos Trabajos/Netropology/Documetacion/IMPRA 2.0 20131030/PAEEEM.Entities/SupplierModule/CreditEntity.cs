using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entities
{
    public class CreditEntity
    {
        private string  _no_credito;
        public string No_Credito
        {
            get 
            {                
                return _no_credito; 
            }
            set { this._no_credito = value; }
        }

        private int _id_prog_proy;
        public int ID_Prog_Proy
        {
            get { return _id_prog_proy; }
            set { this._id_prog_proy = value; }
        }

        private int _Id_Proveedor;
        public int Id_Proveedor
        {
            get { return _Id_Proveedor; }
            set { this._Id_Proveedor = value; }
        }

        private int _cve_estatus_credito;
        public int Cve_Estatus_Credito
        {
            get { return _cve_estatus_credito; }
            set { this._cve_estatus_credito = value; }
        }

        private int _cve_tipo_sociedad;
        public int Cve_Tipo_Sociedad
        {
            get { return _cve_tipo_sociedad; }
            set { this._cve_tipo_sociedad = value; }
        }

        private int _cve_tipo_industria;
        public int Cve_Tipo_Industria
        {
            get { return _cve_tipo_industria; }
            set { this._cve_tipo_industria = value; }
        }

        private string _dx_nombre_comercial;
        public string Dx_Nombre_Comercial
        {
            get { return _dx_nombre_comercial; }
            set 
            {
                if (null == value)
                {
                    this._dx_nombre_comercial = "";
                }
                else
                {
                    this._dx_nombre_comercial = value;
                }
            }
        }

        private string _dx_razon_social;
        public string Dx_Razon_Social
        {
            get { return _dx_razon_social; }
            set 
            {
                if (null == value)
                {
                    this._dx_razon_social = "";
                }
                else
                {
                    this._dx_razon_social = value;
                }
            }
        }

        private string _dx_rfc;
        public string Dx_RFC
        {
            get { return _dx_rfc; }
            set 
            {
                if (null == value)
                {
                    this._dx_rfc = "";
                }
                else
                {
                    this._dx_rfc = value;
                }
            }
        }

        private string _dx_curp;
        public string Dx_CURP
        {
            get { return _dx_curp; }
            set 
            {
                if (null == value)
                {
                    this._dx_curp = "";
                }
                else
                {
                    this._dx_curp = value;
                }
            }
        }

        private string _dx_nombre_repre_legal;
        public string Dx_Nombre_Repre_Legal
        {
            get { return _dx_nombre_repre_legal; }
            set 
            {
                if (null == value)
                {
                    this._dx_nombre_repre_legal = "";
                }
                else
                {
                    this._dx_nombre_repre_legal = value;
                }
            }
        }

        private int _cve_acreditacion_repre_legal;
        public int Cve_Acreditacion_Repre_legal
        {
            get { return _cve_acreditacion_repre_legal; }
            set { this._cve_acreditacion_repre_legal = value; }
        }

        private int _cve_identificacion_repre_legal;
        public int Cve_Identificacion_Repre_legal
        {
            get { return _cve_identificacion_repre_legal; }
            set { this._cve_identificacion_repre_legal = value; }
        }

        private string _dx_no_identificacion_repre_legal;
        public string Dx_No_Identificacion_Repre_Legal
        {
            get { return _dx_no_identificacion_repre_legal; }
            set 
            {
                if (null == value)
                {
                    this._dx_no_identificacion_repre_legal = "";
                }
                else
                {
                    this._dx_no_identificacion_repre_legal = value;
                }
            }
        }

        private int _fg_sexo_repre_legal;
        public int Fg_Sexo_Repre_legal
        {
            get { return _fg_sexo_repre_legal; }
            set { this._fg_sexo_repre_legal = value; }
        }

        private string _no_rpu;
        public string No_RPU
        {
            get { return _no_rpu; }
            set { this._no_rpu = value; }
        }

        private int _fg_edo_civil_repre_legal;
        public int Fg_Edo_Civil_Repre_legal
        {
            get { return _fg_edo_civil_repre_legal; }
            set { this._fg_edo_civil_repre_legal = value; }
        }

        private int _Cve_Reg_Conyugal_Repre_legal;
        public int Cve_Reg_Conyugal_Repre_legal
        {
            get { return _Cve_Reg_Conyugal_Repre_legal; }
            set { this._Cve_Reg_Conyugal_Repre_legal = value; }
        }

        private string _Dx_Email_Repre_legal;
        public string Dx_Email_Repre_legal
        {
            get { return _Dx_Email_Repre_legal; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Email_Repre_legal = "";
                }
                else
                {
                    this._Dx_Email_Repre_legal = value;
                }
            }
        }

        private string _Dx_Domicilio_Fisc_Calle;
        public string Dx_Domicilio_Fisc_Calle
        {
            get { return _Dx_Domicilio_Fisc_Calle; }
             set 
            {
                if (null == value)
                {
                    this._Dx_Domicilio_Fisc_Calle = "";
                }
                else
                {
                    this._Dx_Domicilio_Fisc_Calle = value;
                }
            }
        }

        private string _Dx_Domicilio_Fisc_Num;
        public string Dx_Domicilio_Fisc_Num
        {
            get { return _Dx_Domicilio_Fisc_Num; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Domicilio_Fisc_Num = "";
                }
                else
                {
                    this._Dx_Domicilio_Fisc_Num = value;
                }
            }
        }

        private string _Dx_Domicilio_Fisc_Colonia;
        public string Dx_Domicilio_Fisc_Colonia
        {
            get { return _Dx_Domicilio_Fisc_Colonia; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Domicilio_Fisc_Colonia = "";
                }
                else
                {
                    this._Dx_Domicilio_Fisc_Colonia = value;
                }
            }
        }

        private string _Dx_Domicilio_Fisc_CP;
        public string Dx_Domicilio_Fisc_CP
        {
            get { return _Dx_Domicilio_Fisc_CP; }
             set 
            {
                if (null == value)
                {
                    this._Dx_Domicilio_Fisc_CP = "";
                }
                else
                {
                    this._Dx_Domicilio_Fisc_CP = value;
                }
            }
        }

        private int _Cve_Estado_Fisc;
        public int Cve_Estado_Fisc
        {
            get { return _Cve_Estado_Fisc; }
            set { this._Cve_Estado_Fisc = value; }
        }

        private int _Cve_Deleg_Municipio_Fisc;
        public int Cve_Deleg_Municipio_Fisc
        {
            get { return _Cve_Deleg_Municipio_Fisc; }
            set { this._Cve_Deleg_Municipio_Fisc = value; }
        }

        private int _Cve_Tipo_Propiedad_Fisc;
        public int Cve_Tipo_Propiedad_Fisc
        {
            get { return _Cve_Tipo_Propiedad_Fisc; }
            set { this._Cve_Tipo_Propiedad_Fisc = value; }
        }

        private string _Dx_Tel_Fisc;
        public string Dx_Tel_Fisc
        {
            get { return _Dx_Tel_Fisc; }
             set 
            {
                if (null == value)
                {
                    this._Dx_Tel_Fisc = "";
                }
                else
                {
                    this._Dx_Tel_Fisc = value;
                }
            }
        }

        private bool _Fg_Mismo_Domicilio;
        public bool Fg_Mismo_Domicilio
        {
            get { return _Fg_Mismo_Domicilio; }
            set { this._Fg_Mismo_Domicilio = value; }
        }

        private string _Dx_Domicilio_Neg_Calle;
        public string Dx_Domicilio_Neg_Calle
        {
            get { return _Dx_Domicilio_Neg_Calle; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Domicilio_Neg_Calle = "";
                }
                else
                {
                    this._Dx_Domicilio_Neg_Calle = value;
                }
            }
        }

        private string _Dx_Domicilio_Neg_Num;
        public string Dx_Domicilio_Neg_Num
        {
            get { return _Dx_Domicilio_Neg_Num; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Domicilio_Neg_Num = "";
                }
                else
                {
                    this._Dx_Domicilio_Neg_Num = value;
                }
            }
        }

        private string _Dx_Domicilio_Neg_Colonia;
        public string Dx_Domicilio_Neg_Colonia
        {
            get { return _Dx_Domicilio_Neg_Colonia; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Domicilio_Neg_Colonia = "";
                }
                else
                {
                    this._Dx_Domicilio_Neg_Colonia = value;
                }
            }
        }

        private string _Dx_Domicilio_Neg_CP;
        public string Dx_Domicilio_Neg_CP
        {
            get { return _Dx_Domicilio_Neg_CP; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Domicilio_Neg_CP = "";
                }
                else
                {
                    this._Dx_Domicilio_Neg_CP = value;
                }
            }
        }

        private int _Cve_Estado_Neg;
        public int Cve_Estado_Neg
        {
            get { return _Cve_Estado_Neg; }
            set { this._Cve_Estado_Neg = value; }
        }

        private int _Cve_Deleg_Municipio_Neg;
        public int Cve_Deleg_Municipio_Neg
        {
            get { return _Cve_Deleg_Municipio_Neg; }
            set{this._Cve_Deleg_Municipio_Neg = value;}
        }

        private int _Cve_Tipo_Propiedad_Neg;
        public int Cve_Tipo_Propiedad_Neg
        {
            get { return _Cve_Tipo_Propiedad_Neg; }
            set { this._Cve_Tipo_Propiedad_Neg = value; }
        }

        private string _Dx_Tel_Neg;
        public string Dx_Tel_Neg
        {
            get { return _Dx_Tel_Neg; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Tel_Neg = "";
                }
                else
                {
                    this._Dx_Tel_Neg = value;
                }
            }
        }

        private double _Mt_Ventas_Mes_Empresa;
        public double Mt_Ventas_Mes_Empresa
        {
            get { return _Mt_Ventas_Mes_Empresa; }
            set { this._Mt_Ventas_Mes_Empresa = value; }
        }

        private double _Mt_Gastos_Mes_Empresa;
        public double Mt_Gastos_Mes_Empresa
        {
            get { return _Mt_Gastos_Mes_Empresa; }
            set { this._Mt_Gastos_Mes_Empresa = value; }
        }

        private double _Mt_Ingreso_Neto_Mes_Empresa;
        public double Mt_Ingreso_Neto_Mes_Empresa
        {
            get { return _Mt_Ingreso_Neto_Mes_Empresa; }
            set { this._Mt_Ingreso_Neto_Mes_Empresa = value; }
        }

        // RSA detailed Aval information for RFC validation
        private string _Dx_Nombre_Aval;
        public string Dx_Nombre_Aval
        {
            get { return _Dx_Nombre_Aval; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Nombre_Aval = "";
                }
                else
                {
                    this._Dx_Nombre_Aval = value;
                }
            }
        }

        private string _Dx_First_Name_Aval;
        public string Dx_First_Name_Aval
        {
            get { return _Dx_First_Name_Aval; }
            set 
            {
                if (null == value)
                {
                    this._Dx_First_Name_Aval = "";
                }
                else
                {
                    this._Dx_First_Name_Aval = value;
                }
            }
        }

        private string _Dx_Last_Name_Aval;
        public string Dx_Last_Name_Aval
        {
            get { return _Dx_Last_Name_Aval; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Last_Name_Aval = "";
                }
                else
                {
                    this._Dx_Last_Name_Aval = value;
                }
            }
        }

        private string _Dx_Mother_Name_Aval;
        public string Dx_Mother_Name_Aval
        {
            get { return _Dx_Mother_Name_Aval; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Mother_Name_Aval = "";
                }
                else
                {
                    this._Dx_Mother_Name_Aval = value;
                }
            }
        }

        private object _Dt_BirthDate_Aval;
        public object Dt_BirthDate_Aval
        {
            get { return _Dt_BirthDate_Aval; }
            set
            {
                if (value == null)
                {
                    this._Dt_BirthDate_Aval = DBNull.Value;
                }
                else
                {
                    this._Dt_BirthDate_Aval = value;
                }
            }
        }
        private string _Dx_RFC_CURP_Aval;
        public string Dx_RFC_CURP_Aval
        {
            get { return _Dx_RFC_CURP_Aval; }
            set 
            {
                if (null == value)
                {
                    this._Dx_RFC_CURP_Aval = "";
                }
                else
                {
                    this._Dx_RFC_CURP_Aval = value;
                }
            }
        }

        private string _Dx_RFC_Aval;
        public string Dx_RFC_Aval
        {
            get { return _Dx_RFC_Aval; }
            set
            {
                if (null == value)
                {
                    this._Dx_RFC_Aval = "";
                }
                else
                {
                    this._Dx_RFC_Aval = value;
                }
            }
        }

        private string _Dx_CURP_Aval;
        public string Dx_CURP_Aval
        {
            get { return _Dx_CURP_Aval; }
            set
            {
                if (null == value)
                {
                    this._Dx_CURP_Aval = "";
                }
                else
                {
                    this._Dx_CURP_Aval = value;
                }
            }
        }

        private string _Dx_Tel_Aval;
        public string Dx_Tel_Aval
        {
            get { return _Dx_Tel_Aval; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Tel_Aval = "";
                }
                else
                {
                    this._Dx_Tel_Aval = value;
                }
            }
        }

        private int _Fg_Sexo_Aval;
        public int Fg_Sexo_Aval
        {
            get { return _Fg_Sexo_Aval; }
            set { this._Fg_Sexo_Aval = value; }
        }

        private string _Dx_Domicilio_Aval_Calle;
        public string Dx_Domicilio_Aval_Calle
        {
            get { return _Dx_Domicilio_Aval_Calle; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Domicilio_Aval_Calle = "";
                }
                else
                {
                    this._Dx_Domicilio_Aval_Calle = value;
                }
            }
        }

        private string _Dx_Domicilio_Aval_Num;
        public string Dx_Domicilio_Aval_Num
        {
            get { return _Dx_Domicilio_Aval_Num; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Domicilio_Aval_Num = "";
                }
                else
                {
                    this._Dx_Domicilio_Aval_Num = value;
                }
            }
        }

        private string _Dx_Domicilio_Aval_Colonia;
        public string Dx_Domicilio_Aval_Colonia
        {
            get { return _Dx_Domicilio_Aval_Colonia; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Domicilio_Aval_Colonia = "";
                }
                else
                {
                    this._Dx_Domicilio_Aval_Colonia = value;
                }
            }
        }

        private string _Dx_Domicilio_Aval_CP;
        public string Dx_Domicilio_Aval_CP
        {
            get { return _Dx_Domicilio_Aval_CP; }
            set 
            {
                if (null == value)
                {
                    this._Dx_Domicilio_Aval_CP = "";
                }
                else
                {
                    this._Dx_Domicilio_Aval_CP = value;
                }
            }
        }

        private int _Cve_Estado_Aval;
        public int Cve_Estado_Aval
        {
            get { return _Cve_Estado_Aval; }
            set { this._Cve_Estado_Aval = value; }
        }

        private int _Cve_Deleg_Municipio_Aval;
        public int Cve_Deleg_Municipio_Aval
        {
            get { return _Cve_Deleg_Municipio_Aval; }
            set { this._Cve_Deleg_Municipio_Aval = value; }
        }

        private double _Mt_Ventas_Mes_Aval;
        public double Mt_Ventas_Mes_Aval
        {
            get { return _Mt_Ventas_Mes_Aval; }
            set { this._Mt_Ventas_Mes_Aval = value; }
        }

        private double _Mt_Gastos_Mes_Aval;
        public double Mt_Gastos_Mes_Aval
        {
            get { return _Mt_Gastos_Mes_Aval; }
            set { this._Mt_Gastos_Mes_Aval = value; }
        }

        private double _Mt_Ingreso_Neto_Mes_Aval;
        public double Mt_Ingreso_Neto_Mes_Aval
        {
            get { return _Mt_Ingreso_Neto_Mes_Aval; }
            set { this._Mt_Ingreso_Neto_Mes_Aval = value; }
        }

        private string _No_RPU_AVAL;
        public string No_RPU_AVAL
        {
            get { return _No_RPU_AVAL; }
            set { this._No_RPU_AVAL = value; }
        }

        private string _Dx_No_Escritura_Aval;
        public string Dx_No_Escritura_Aval
        {
            get { return _Dx_No_Escritura_Aval; }
            set { this._Dx_No_Escritura_Aval = value; }
        }

        private object _Dt_Fecha_Escritura_Aval;
        public object Dt_Fecha_Escritura_Aval
        {
            get { return _Dt_Fecha_Escritura_Aval;  }
            set 
            {
                if  (value == null)
                {
                    this._Dt_Fecha_Escritura_Aval = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_Escritura_Aval = value;
                }
            }
        }

        private string _Dx_Nombre_Notario_Escritura_Aval;
        public string Dx_Nombre_Notario_Escritura_Aval
        {
            get { return _Dx_Nombre_Notario_Escritura_Aval; }
            set { this._Dx_Nombre_Notario_Escritura_Aval = value; }
        }

        private string _Dx_No_Notario_Escritura_Aval;
        public string Dx_No_Notario_Escritura_Aval
        {
            get { return _Dx_No_Notario_Escritura_Aval; }
            set { this._Dx_No_Notario_Escritura_Aval = value; }
        }

        private int _Cve_Estado_Escritura_Aval;
        public int Cve_Estado_Escritura_Aval
        {
            get { return _Cve_Estado_Escritura_Aval; }
            set { this._Cve_Estado_Escritura_Aval = value; }
        }

        private int _Cve_Deleg_Municipio_Escritura_Aval;
        public int Cve_Deleg_Municipio_Escritura_Aval
        {
            get { return _Cve_Deleg_Municipio_Escritura_Aval; }
            set { this._Cve_Deleg_Municipio_Escritura_Aval = value; }
        }

        private object _Dt_Fecha_Gravamen;
        public object Dt_Fecha_Gravamen
        {
            get { return _Dt_Fecha_Gravamen; }
            set 
            {
                if (null == value)
                {
                    this._Dt_Fecha_Gravamen = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_Gravamen = value;
                }
            }
        }

        private string _Dx_Emite_Gravamen;
        public string Dx_Emite_Gravamen
        {
            get { return _Dx_Emite_Gravamen; }
            set { this._Dx_Emite_Gravamen = value; }
        }

        private string _Dx_Num_Acta_Matrimonio_Aval;
        public string Dx_Num_Acta_Matrimonio_Aval
        {
            get { return _Dx_Num_Acta_Matrimonio_Aval; }
            set { this._Dx_Num_Acta_Matrimonio_Aval = value; }
        }

        private string _Dx_Registro_Civil_Mat_Aval;
        public string Dx_Registro_Civil_Mat_Aval
        {
            get { return _Dx_Registro_Civil_Mat_Aval; }
            set { this._Dx_Registro_Civil_Mat_Aval = value; }
        }
        private string _Dx_Nombre_Coacreditado;
        public string Dx_Nombre_Coacreditado
        {
            get { return _Dx_Nombre_Coacreditado; }
            set { this._Dx_Nombre_Coacreditado = value; }
        }

        private string _Dx_RFC_CURP_Coacreditado;
        public string Dx_RFC_CURP_Coacreditado
        {
            get { return _Dx_RFC_CURP_Coacreditado; }
            set { this._Dx_RFC_CURP_Coacreditado = value; }
        }

        private string _Dx_Telefono_Coacreditado;
        public string Dx_Telefono_Coacreditado
        {
            get { return _Dx_Telefono_Coacreditado; }
            set { this._Dx_Telefono_Coacreditado = value; }
        }

        private int _Fg_Sexo_Coacreditado;
        public int Fg_Sexo_Coacreditado
        {
            get { return _Fg_Sexo_Coacreditado; }
            set { this._Fg_Sexo_Coacreditado = value; }
        }

        private string _Dx_Domicilio_Coacreditado_Calle;
        public string Dx_Domicilio_Coacreditado_Calle
        {
            get { return _Dx_Domicilio_Coacreditado_Calle; }
            set { this._Dx_Domicilio_Coacreditado_Calle = value; }
        }

        private string _Dx_Domicilio_Coacreditado_Num;
        public string Dx_Domicilio_Coacreditado_Num
        {
            get { return _Dx_Domicilio_Coacreditado_Num; }
            set { this._Dx_Domicilio_Coacreditado_Num = value; }
        }

        private string _Dx_Domicilio_Coacreditado_Colonia;
        public string Dx_Domicilio_Coacreditado_Colonia
        {
            get { return _Dx_Domicilio_Coacreditado_Colonia; }
            set { this._Dx_Domicilio_Coacreditado_Colonia = value; }
        }

        private string _Dx_Domicilio_Coacreditado_CP;
        public string Dx_Domicilio_Coacreditado_CP
        {
            get { return _Dx_Domicilio_Coacreditado_CP; }
            set { this._Dx_Domicilio_Coacreditado_CP = value; }
        }

        private int _Cve_Estado_Coacreditado;
        public int Cve_Estado_Coacreditado
        {
            get { return _Cve_Estado_Coacreditado; }
            set { this._Cve_Estado_Coacreditado = value; }
        }

        private int _Cve_Deleg_Municipio_Coacreditado;
        public int Cve_Deleg_Municipio_Coacreditado
        {
            get { return _Cve_Deleg_Municipio_Coacreditado; }
            set { this._Cve_Deleg_Municipio_Coacreditado = value; }
        }

        private string _Dx_No_Escritura_Poder;
        public string Dx_No_Escritura_Poder
        {
            get { return _Dx_No_Escritura_Poder; }
            set { this._Dx_No_Escritura_Poder = value; }
        }

        private object _Dt_Fecha_Poder;
        public object Dt_Fecha_Poder
        {
            get { return _Dt_Fecha_Poder; }
            set
            {
                if (value == null)
                {
                    this._Dt_Fecha_Poder = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_Poder = value;
                }
            }
        }

        private string _Dx_Nombre_Notario_Poder;
        public string Dx_Nombre_Notario_Poder
        {
            get { return _Dx_Nombre_Notario_Poder; }
            set { this._Dx_Nombre_Notario_Poder = value; }
        }

        private string _Dx_No_Notario_Poder;
        public string Dx_No_Notario_Poder
        {
            get { return _Dx_No_Notario_Poder; }
            set { this._Dx_No_Notario_Poder = value; }
        }

        private int _Cve_Estado_Poder;
        public int Cve_Estado_Poder
        {
            get { return _Cve_Estado_Poder; }
            set { this._Cve_Estado_Poder = value; }
        }

        private int _Cve_Deleg_Municipio_Poder;
        public int Cve_Deleg_Municipio_Poder
        {
            get { return _Cve_Deleg_Municipio_Poder; }
            set { this._Cve_Deleg_Municipio_Poder = value; }
        }

        private string _Dx_No_Escritura_Acta;
        public string Dx_No_Escritura_Acta
        {
            get { return _Dx_No_Escritura_Acta; }
            set { this._Dx_No_Escritura_Acta = value; }
        }

        private object _Dt_Fecha_Acta;
        public object Dt_Fecha_Acta
        {
            get { return _Dt_Fecha_Acta; }
            set 
            {
                if (null == value)
                {
                    this._Dt_Fecha_Acta = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_Acta = value;
                }
            }
        }

        private string _Dx_No_Notario_Acta;
        public string Dx_No_Notario_Acta
        {
            get { return _Dx_No_Notario_Acta; }
            set { this._Dx_No_Notario_Acta = value; }
        }

        private string _Dx_Nombre_Notario_Acta;
        public string Dx_Nombre_Notario_Acta
        {
            get { return _Dx_Nombre_Notario_Acta; }
            set { this._Dx_Nombre_Notario_Acta = value; }
        }

        private int _Cve_Estado_Acta;
        public int Cve_Estado_Acta
        {
            get { return _Cve_Estado_Acta; }
            set { this._Cve_Estado_Acta = value; }
        }

        private int _Cve_Deleg_Municipio_Acta;
        public int Cve_Deleg_Municipio_Acta
        {
            get { return _Cve_Deleg_Municipio_Acta; }
            set { this._Cve_Deleg_Municipio_Acta = value; }
        }

        private double _No_Ahorro_Energetico;
        public double No_Ahorro_Energetico
        {
            get { return _No_Ahorro_Energetico; }
            set { this._No_Ahorro_Energetico = value; }
        }

        private double _No_Ahorro_Economico;
        public double No_Ahorro_Economico
        {
            get { return _No_Ahorro_Economico; }
            set { this._No_Ahorro_Economico = value; }
        }

        private double _Mt_Monto_Solicitado;
        public double Mt_Monto_Solicitado
        {
            get { return _Mt_Monto_Solicitado; }
            set { this._Mt_Monto_Solicitado = value; }
        }

        private double _Mt_Monto_Total_Pagar;
        public double Mt_Monto_Total_Pagar
        {
            get { return _Mt_Monto_Total_Pagar; }
            set { this._Mt_Monto_Total_Pagar = value; }
        }

        private double _Mt_Capacidad_Pago;
        public double Mt_Capacidad_Pago
        {
            get { return _Mt_Capacidad_Pago; }
            set { this._Mt_Capacidad_Pago = value; }
        }

        private int _No_Plazo_Pago;
        public int No_Plazo_Pago
        {
            get { return _No_Plazo_Pago; }
            set { this._No_Plazo_Pago = value; }
        }

        private int _Cve_Periodo_Pago;
        public int Cve_Periodo_Pago
        {
            get { return _Cve_Periodo_Pago; }
            set { this._Cve_Periodo_Pago = value; }
        }

        private float _Pct_Tasa_Interes;
        public float Pct_Tasa_Interes
        {
            get { return _Pct_Tasa_Interes; }
            set { this._Pct_Tasa_Interes = value; }
        }

        private float _Pct_Tasa_Fija;
        public float Pct_Tasa_Fija
        {
            get { return _Pct_Tasa_Fija; }
            set { this._Pct_Tasa_Fija = value; }
        }

        private float _Pct_CAT;
        public float Pct_CAT
        {
            get { return _Pct_CAT; }
            set { this._Pct_CAT = value; }
        }

        private float _Pct_Tasa_IVA;
        public float Pct_Tasa_IVA
        {
            get { return _Pct_Tasa_IVA; }
            set { this._Pct_Tasa_IVA = value; }
        }

        private int _Fg_Adquisicion_Sust;
        public int Fg_Adquisicion_Sust
        {
            get { return _Fg_Adquisicion_Sust; }
            set { this._Fg_Adquisicion_Sust = value; }
        }

        private object _Dt_Fecha_Cancelado;
        public object Dt_Fecha_Cancelado
        {
            get { return _Dt_Fecha_Cancelado; }
            set
            {
                if (null == value)
                {
                    this._Dt_Fecha_Cancelado = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_Cancelado = value;
                }
            }
        }

        private object _Dt_Fecha_Pendiente;
        public object Dt_Fecha_Pendiente
        {
            get { return _Dt_Fecha_Pendiente; }
            set
            {
                if (null == value)
                {
                    this._Dt_Fecha_Pendiente = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_Pendiente = value;
                }
            }
        }

        private object _Dt_Fecha_Por_entregar;
        public object Dt_Fecha_Por_entregar
        {
            get { return _Dt_Fecha_Por_entregar; }
            set
            {
                if (null == value)
                {
                    this._Dt_Fecha_Por_entregar = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_Por_entregar = value;
                }
            }
        }

        private object _Dt_Fecha_En_revision;
        public object Dt_Fecha_En_revision
        {
            get { return _Dt_Fecha_En_revision; }
            set
            {
                if (null== value)
                {
                    this._Dt_Fecha_En_revision = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_En_revision = value;
                }
            }
        }

        private object _Dt_Fecha_Autorizado;
        public object Dt_Fecha_Autorizado
        {
            get { return _Dt_Fecha_Autorizado; }
            set
            {
                if (null == value)
                {
                    this._Dt_Fecha_Autorizado = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_Autorizado = value;
                }
            }
        }

        private object _Dt_Fecha_Rechazado;
        public object Dt_Fecha_Rechazado
        {
            get { return _Dt_Fecha_Rechazado; }
            set
            {
                if (null == value)
                {
                    this._Dt_Fecha_Rechazado = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_Rechazado = value;
                }
            }
        }

        private object _Dt_Fecha_Finanzas;
        public object Dt_Fecha_Finanzas
        {
            get { return _Dt_Fecha_Finanzas; }
            set
            {
                if (null == value)
                {
                    this._Dt_Fecha_Finanzas = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_Finanzas = value;
                }
            }
        }

        private object _Dt_Fecha_Ultmod;
        public object Dt_Fecha_Ultmod
        {
            get { return _Dt_Fecha_Ultmod; }
            set
            {
                if (null == value)
                {
                    this._Dt_Fecha_Ultmod = DBNull.Value;
                }
                else
                {
                    this._Dt_Fecha_Ultmod = value;
                }
            }
        }

        private string _Dx_Usr_Ultmod;
        public string Dx_Usr_Ultmod
        {
            get { return _Dx_Usr_Ultmod; }
            set { this._Dx_Usr_Ultmod = value; }
        }

        private float _Pct_Tasa_IVA_Intereses;
        public float Pct_Tasa_IVA_Intereses
        {
            get { return _Pct_Tasa_IVA_Intereses; }
            set { this._Pct_Tasa_IVA_Intereses = value; }
        }

        private object _Dt_Fecha_Beneficiario_con_adeudos;
        public object Dt_Fecha_Beneficiario_con_adeudos
        {
            get { return _Dt_Fecha_Beneficiario_con_adeudos; }
            set
            {
                if (null == value)
                {
                    _Dt_Fecha_Beneficiario_con_adeudos = DBNull.Value;
                }
                else
                {
                    _Dt_Fecha_Beneficiario_con_adeudos = value;
                }
            }
        }

        private object _Dt_Fecha_Tarifa_fuera_de_programa;
        public object Dt_Fecha_Tarifa_fuera_de_programa
        {
            get { return _Dt_Fecha_Tarifa_fuera_de_programa; }
            set
            {
                if (null == value)
                {
                    _Dt_Fecha_Tarifa_fuera_de_programa = DBNull.Value;
                }
                else
                {
                    _Dt_Fecha_Tarifa_fuera_de_programa = value;
                }
            }
        }

        private float _No_consumo_promedio;
        public float No_consumo_promedio
        {
            get { return _No_consumo_promedio; }
            set { this._No_consumo_promedio = value; }
        }

        private string _Tipo_Usuario;
        public string Tipo_Usuario
        {
            get { return _Tipo_Usuario; }
            set { this._Tipo_Usuario = value; }
        }

        private object _Dt_Fecha_Calificación_MOP_no_válida;
        public object Dt_Fecha_Calificación_MOP_no_válida
        {
            get { return _Dt_Fecha_Calificación_MOP_no_válida; }
            set
            {
                if (null == value)
                {
                    _Dt_Fecha_Calificación_MOP_no_válida = DBNull.Value;
                }
                else
                {
                    _Dt_Fecha_Calificación_MOP_no_válida = value;
                }
            }
        }

        //add by coco 2012-07-16
        private string _ATB04;
        public string Telephone
        {
            get
            {
                return _ATB04;
            }
            set
            {
                this._ATB04 = value;
            }
        }

        private string _ATB05;
        public string Email
        {
            get
            {
                return _ATB05;
            }
            set
            {
                this._ATB05 = value;
            }
        }
        //end add

        // RSA 20130903 total gastos
        private double _Mt_Gastos_Instalacion_Mano_Obra;
        public double Mt_Gastos_Instalacion_Mano_Obra
        {
            get { return _Mt_Gastos_Instalacion_Mano_Obra; }
            set { this._Mt_Gastos_Instalacion_Mano_Obra = value; }
        }
    }
}
