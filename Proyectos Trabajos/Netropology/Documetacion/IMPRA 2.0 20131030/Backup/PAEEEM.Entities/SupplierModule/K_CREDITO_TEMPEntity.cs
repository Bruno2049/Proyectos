using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entities
{
    public class K_CREDITO_TEMPEntity
    {
        private string  _cve_tipo_sociedad;
        public string  Cve_Tipo_Sociedad
        {
            get { return _cve_tipo_sociedad; }
            set { this._cve_tipo_sociedad = value; }
        }

        private string _Dx_First_Name;
        public string Dx_First_Name
        {
            get
            {
                return _Dx_First_Name;
            }
            set
            {
                this._Dx_First_Name = value;
            }
        }

        private string _Dx_Last_Name;
        public string Dx_Last_Name
        {
            get
            {
                return _Dx_Last_Name;
            }
            set
            {
                this._Dx_Last_Name = value;
            }
        }

        private string _Dx_Mother_Name;
        public string Dx_Mother_Name
        {
            get
            {
                return _Dx_Mother_Name;
            }
            set
            {
                this._Dx_Mother_Name = value;
            }
        }

        private string   _Dt_Fecha_Nacimiento;
        public string   Dt_Fecha_Nacimiento
        {
            get { return _Dt_Fecha_Nacimiento; }
            set { this._Dt_Fecha_Nacimiento = value; }
        }

        private string  _cve_tipo_industria;
        public string  Cve_Tipo_Industria
        {
            get { return _cve_tipo_industria; }
            set { this._cve_tipo_industria = value; }
        }

        private string _Dx_Telephone;
        public string Dx_Telephone
        {
            get
            {
                return _Dx_Telephone;
            }
            set
            {
                this._Dx_Telephone = value;
            }
        }

        private string _Dx_Email;
        public string Dx_Email
        {
            get
            {
                return _Dx_Email;
            }
            set
            {
                this._Dx_Email = value;
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

        private string  _cve_acreditacion_repre_legal;
        public string  Cve_Acreditacion_Repre_legal
        {
            get { return _cve_acreditacion_repre_legal; }
            set { this._cve_acreditacion_repre_legal = value; }
        }

        private string  _fg_sexo_repre_legal;
        public string  Fg_Sexo_Repre_legal
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

        private string  _fg_edo_civil_repre_legal;
        public string  Fg_Edo_Civil_Repre_legal
        {
            get { return _fg_edo_civil_repre_legal; }
            set { this._fg_edo_civil_repre_legal = value; }
        }

        private string  _Cve_Reg_Conyugal_Repre_legal;
        public string  Cve_Reg_Conyugal_Repre_legal
        {
            get { return _Cve_Reg_Conyugal_Repre_legal; }
            set { this._Cve_Reg_Conyugal_Repre_legal = value; }
        }

        private string  _cve_identificacion_repre_legal;
        public string  Cve_Identificacion_Repre_legal
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

        private string   _Mt_Ventas_Mes_Empresa;
        public string   Mt_Ventas_Mes_Empresa
        {
            get { return _Mt_Ventas_Mes_Empresa; }
            set { this._Mt_Ventas_Mes_Empresa = value; }
        }

        private string   _Mt_Gastos_Mes_Empresa;
        public string   Mt_Gastos_Mes_Empresa
        {
            get { return _Mt_Gastos_Mes_Empresa; }
            set { this._Mt_Gastos_Mes_Empresa = value; }
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

        private string   _No_consumo_promedio;
        public string   No_consumo_promedio
        {
            get { return _No_consumo_promedio; }
            set { this._No_consumo_promedio = value; }
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

        private string  _Cve_Estado_Fisc;
        public string  Cve_Estado_Fisc
        {
            get { return _Cve_Estado_Fisc; }
            set { this._Cve_Estado_Fisc = value; }
        }

        private string  _Cve_Deleg_Municipio_Fisc;
        public string  Cve_Deleg_Municipio_Fisc
        {
            get { return _Cve_Deleg_Municipio_Fisc; }
            set { this._Cve_Deleg_Municipio_Fisc = value; }
        }

        private string _Dx_Ciudad;
        public string Dx_Ciudad
        {
            get { return _Dx_Ciudad; }
            set { this._Dx_Ciudad = value; }
        }

        //private string _Dx_Numero_Interior;
        //public string Dx_Numero_Interior
        //{
        //    get { return _Dx_Numero_Interior; }
        //    set { this._Dx_Numero_Interior = value; }
        //}

        private string  _Cve_Tipo_Propiedad_Fisc;
        public string  Cve_Tipo_Propiedad_Fisc
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

        private string  _Fg_Mismo_Domicilio;
        public string  Fg_Mismo_Domicilio
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

        private string  _Cve_Estado_Neg;
        public string  Cve_Estado_Neg
        {
            get { return _Cve_Estado_Neg; }
            set { this._Cve_Estado_Neg = value; }
        }

        private string  _Cve_Deleg_Municipio_Neg;
        public string  Cve_Deleg_Municipio_Neg
        {
            get { return _Cve_Deleg_Municipio_Neg; }
            set { this._Cve_Deleg_Municipio_Neg = value; }
        }

        private string  _Cve_Tipo_Propiedad_Neg;
        public string  Cve_Tipo_Propiedad_Neg
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

        private string _Dx_FirstName_Aval;
        public string Dx_First_Name_Aval
        {
            get { return _Dx_FirstName_Aval; }
            set
            {
                if (null == value)
                {
                    this._Dx_FirstName_Aval = "";
                }
                else
                {
                    this._Dx_FirstName_Aval = value;
                }
            }
        }


        private string _Dx_LastName_Aval;
        public string Dx_Last_Name_Aval
        {
            get { return _Dx_LastName_Aval; }
            set
            {
                if (null == value)
                {
                    this._Dx_LastName_Aval = "";
                }
                else
                {
                    this._Dx_LastName_Aval = value;
                }
            }
        }
        private string _Dx_MotherName_Aval;
        public string Dx_Mother_Name_Aval
        {
            get { return _Dx_MotherName_Aval; }
            set
            {
                if (null == value)
                {
                    this._Dx_MotherName_Aval = "";
                }
                else
                {
                    this._Dx_MotherName_Aval = value;
                }
            }
        }

        private string _Dt_Fecha_Nacimiento_Aval;
        public string Dt_BirthDate_Aval
        {
            get { return _Dt_Fecha_Nacimiento_Aval; }
            set { this._Dt_Fecha_Nacimiento_Aval = value; }
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

        private string  _Fg_Sexo_Aval;
        public string  Fg_Sexo_Aval
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

        private string  _Cve_Estado_Aval;
        public string  Cve_Estado_Aval
        {
            get { return _Cve_Estado_Aval; }
            set { this._Cve_Estado_Aval = value; }
        }

        private string  _Cve_Deleg_Municipio_Aval;
        public string  Cve_Deleg_Municipio_Aval
        {
            get { return _Cve_Deleg_Municipio_Aval; }
            set { this._Cve_Deleg_Municipio_Aval = value; }
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

        private string _No_RPU_AVAL;
        public string No_RPU_AVAL
        {
            get { return _No_RPU_AVAL; }
            set { this._No_RPU_AVAL = value; }
        }
        private string   _Mt_Ventas_Mes_Aval;
        public string   Mt_Ventas_Mes_Aval
        {
            get { return _Mt_Ventas_Mes_Aval; }
            set { this._Mt_Ventas_Mes_Aval = value; }
        }

        private string   _Mt_Gastos_Mes_Aval;
        public string   Mt_Gastos_Mes_Aval
        {
            get { return _Mt_Gastos_Mes_Aval; }
            set { this._Mt_Gastos_Mes_Aval = value; }
        }

        private string _User_Name;
        public string User_Name
        {
            get { return _User_Name; }
            set { this._User_Name = value; }
        }
    }
}
