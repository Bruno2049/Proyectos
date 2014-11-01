/* ----------------------------------------------------------------------
 * File Name:CAT_CENTRO_DISPEntity.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/10/26
 *
 * Description: CAT_CENTRO_DISP business entity
 *----------------------------------------------------------------------*/

using System;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class CAT_CENTRO_DISPModel
    {
        /// <summary>
        /// Id_Centro_Disp
        /// </summary>
        private int _Id_Centro_Disp;
        public int Id_Centro_Disp
        {
            get { return _Id_Centro_Disp; }
            set { _Id_Centro_Disp = value; }
        }
        /// <summary>
        /// Cve_Estatus_Centro_Disp
        /// </summary>
        private int _Cve_Estatus_Centro_Disp;
        public int Cve_Estatus_Centro_Disp
        {
            get { return _Cve_Estatus_Centro_Disp; }
            set { _Cve_Estatus_Centro_Disp = value; }
        }
        /// <summary>
        /// Cve_Region
        /// </summary>
        private int _Cve_Region;
        public int Cve_Region
        {
            get { return _Cve_Region; }
            set { _Cve_Region = value; }
        }
        /// <summary>
        /// Dx_Razon_Social
        /// </summary>
        private string _Dx_Razon_Social;
        public string Dx_Razon_Social
        {
            get { return _Dx_Razon_Social; }
            set { _Dx_Razon_Social = value; }
        }
        /// <summary>
        /// Dx_Nombre_Comercial
        /// </summary>
        private string _Dx_Nombre_Comercial;
        public string Dx_Nombre_Comercial
        {
            get { return _Dx_Nombre_Comercial; }
            set { _Dx_Nombre_Comercial = value; }
        }
        /// <summary>
        /// Dx_RFC
        /// </summary>
        private string _Dx_RFC;
        public string Dx_RFC
        {
            get { return _Dx_RFC; }
            set { _Dx_RFC = value; }
        }
        /// <summary>
        /// Dx_Domicilio_Part_Calle
        /// </summary>
        private string _Dx_Domicilio_Part_Calle;
        public string Dx_Domicilio_Part_Calle
        {
            get { return _Dx_Domicilio_Part_Calle; }
            set { _Dx_Domicilio_Part_Calle = value; }
        }
        /// <summary>
        /// Dx_Domicilio_Part_Num
        /// </summary>
        private string _Dx_Domicilio_Part_Num;
        public string Dx_Domicilio_Part_Num
        {
            get { return _Dx_Domicilio_Part_Num; }
            set { _Dx_Domicilio_Part_Num = value; }
        }
        /// <summary>
        /// Dx_Domicilio_Part_CP
        /// </summary>
        private string _Dx_Domicilio_Part_CP;
        public string Dx_Domicilio_Part_CP
        {
            get { return _Dx_Domicilio_Part_CP; }
            set { _Dx_Domicilio_Part_CP = value; }
        }
        /// <summary>
        /// Cve_Deleg_Municipio_Part
        /// </summary>
        private object _Cve_Deleg_Municipio_Part;
        public object Cve_Deleg_Municipio_Part
        {
            get { return _Cve_Deleg_Municipio_Part; }
            set
            {
                if (value == null)
                {
                    _Cve_Deleg_Municipio_Part = DBNull.Value;
                }
                else
                {
                    _Cve_Deleg_Municipio_Part = value;
                }
            }
        }
        /// <summary>
        /// Cve_Estado_Part
        /// </summary>
        private object _Cve_Estado_Part;
        public object Cve_Estado_Part
        {
            get { return _Cve_Estado_Part; }
            set
            {
                if (value == null)
                {
                    _Cve_Estado_Part = DBNull.Value;
                }
                else
                {
                    _Cve_Estado_Part = value;
                }
            }
        }
        /// <summary>
        /// Fg_Mismo_Domicilio
        /// </summary>
        private object _Fg_Mismo_Domicilio;
        public object Fg_Mismo_Domicilio
        {
            get { return _Fg_Mismo_Domicilio; }
            set
            {
                if (value == null)
                {
                    _Fg_Mismo_Domicilio = DBNull.Value;
                }
                else
                {
                    _Fg_Mismo_Domicilio = value;
                }
            }
        }
        /// <summary>
        /// Dx_Domicilio_Fiscal_Calle
        /// </summary>
        private string _Dx_Domicilio_Fiscal_Calle;
        public string Dx_Domicilio_Fiscal_Calle
        {
            get { return _Dx_Domicilio_Fiscal_Calle; }
            set { _Dx_Domicilio_Fiscal_Calle = value; }
        }
        /// <summary>
        /// Dx_Domicilio_Fiscal_Num
        /// </summary>
        private string _Dx_Domicilio_Fiscal_Num;
        public string Dx_Domicilio_Fiscal_Num
        {
            get { return _Dx_Domicilio_Fiscal_Num; }
            set { _Dx_Domicilio_Fiscal_Num = value; }
        }
        /// <summary>
        /// Dx_Domicilio_Fiscal_CP
        /// </summary>
        private string _Dx_Domicilio_Fiscal_CP;
        public string Dx_Domicilio_Fiscal_CP
        {
            get { return _Dx_Domicilio_Fiscal_CP; }
            set { _Dx_Domicilio_Fiscal_CP = value; }
        }
        /// <summary>
        /// Cve_Deleg_Municipio_Fisc
        /// </summary>
        private object _Cve_Deleg_Municipio_Fisc;
        public object Cve_Deleg_Municipio_Fisc
        {
            get { return _Cve_Deleg_Municipio_Fisc; }
            set
            {
                if (value == null)
                {
                    _Cve_Deleg_Municipio_Fisc = DBNull.Value;
                }
                else
                {
                    _Cve_Deleg_Municipio_Fisc = value;
                }
            }
        }
        /// <summary>
        /// Cve_Estado_Fisc
        /// </summary>
        private object _Cve_Estado_Fisc;
        public object Cve_Estado_Fisc
        {
            get { return _Cve_Estado_Fisc; }
            set
            {
                if (value == null)
                {
                    _Cve_Estado_Fisc = DBNull.Value;
                }
                else
                {
                    _Cve_Estado_Fisc = value;
                }
            }
        }
        /// <summary>
        /// Dx_Nombre_Repre
        /// </summary>
        private string _Dx_Nombre_Repre;
        public string Dx_Nombre_Repre
        {
            get { return _Dx_Nombre_Repre; }
            set { _Dx_Nombre_Repre = value; }
        }
        /// <summary>
        /// Dx_Email_Repre
        /// </summary>
        private string _Dx_Email_Repre;
        public string Dx_Email_Repre
        {
            get { return _Dx_Email_Repre; }
            set { _Dx_Email_Repre = value; }
        }
        /// <summary>
        /// Dx_Telefono_Repre
        /// </summary>
        private string _Dx_Telefono_Repre;
        public string Dx_Telefono_Repre
        {
            get { return _Dx_Telefono_Repre; }
            set { _Dx_Telefono_Repre = value; }
        }
        /// <summary>
        /// Dx_Nombre_Repre_Legal
        /// </summary>
        private string _Dx_Nombre_Repre_Legal;
        public string Dx_Nombre_Repre_Legal
        {
            get { return _Dx_Nombre_Repre_Legal; }
            set { _Dx_Nombre_Repre_Legal = value; }
        }
        /// <summary>
        /// Dx_Nombre_Banco
        /// </summary>
        private string _Dx_Nombre_Banco;
        public string Dx_Nombre_Banco
        {
            get { return _Dx_Nombre_Banco; }
            set { _Dx_Nombre_Banco = value; }
        }
        /// <summary>
        /// Dx_Cuenta_Banco
        /// </summary>
        private string _Dx_Cuenta_Banco;
        public string Dx_Cuenta_Banco
        {
            get { return _Dx_Cuenta_Banco; }
            set { _Dx_Cuenta_Banco = value; }
        }
        /// <summary>
        /// Binary_Acta_Constitutiva
        /// </summary>
        private string _Binary_Acta_Constitutiva;
        public string Binary_Acta_Constitutiva
        {
            get { return _Binary_Acta_Constitutiva; }
            set { _Binary_Acta_Constitutiva = value; }
        }
        /// <summary>
        /// Binary_Poder_Notarial
        /// </summary>
        private string _Binary_Poder_Notarial;
        public string Binary_Poder_Notarial
        {
            get { return _Binary_Poder_Notarial; }
            set { _Binary_Poder_Notarial = value; }
        }
        /// <summary>
        /// Dt_Fecha_Centro_Disp
        /// </summary>
        private object _Dt_Fecha_Centro_Disp;
        public object Dt_Fecha_Centro_Disp
        {
            get { return _Dt_Fecha_Centro_Disp; }
            set
            {
                if (value == null)
                {
                    _Dt_Fecha_Centro_Disp = DBNull.Value;
                }
                else
                {
                    _Dt_Fecha_Centro_Disp = value;
                }
            }
        }
        /// <summary>
        /// Cve_Zona
        /// </summary>
        private object _Cve_Zona;
        public object Cve_Zona
        {
            get { return _Cve_Zona; }
            set
            {
                if (value == null)
                {
                    _Cve_Zona = DBNull.Value;
                }
                else
                {
                    _Cve_Zona = value;
                }
            }
        }
        /// <summary>
        /// Codigo_Centro_Disp
        /// </summary>
        private string _Codigo_Centro_Disp;
        public string Codigo_Centro_Disp
        {
            get { return _Codigo_Centro_Disp; }
            set { _Codigo_Centro_Disp = value; }
        }
        // RSA 20120927 new fields Start

        /// <summary>
        /// No_Empleados
        /// </summary>
        private int _No_Empleados;
        public int No_Empleados
        {
            get { return _No_Empleados; }
            set { _No_Empleados = value; }
        }
        /// <summary>
        /// Marca_Analizador_Gas
        /// </summary>
        private string _Marca_Analizador_Gas;
        public string Marca_Analizador_Gas
        {
            get { return _Marca_Analizador_Gas; }
            set { _Marca_Analizador_Gas = value; }
        }
        /// <summary>
        /// Modelo_Analizador_Gas
        /// </summary>
        private string _Modelo_Analizador_Gas;
        public string Modelo_Analizador_Gas
        {
            get { return _Modelo_Analizador_Gas; }
            set { _Modelo_Analizador_Gas = value; }
        }
        /// <summary>
        /// Serie_Analizador_Gas
        /// </summary>
        private string _Serie_Analizador_Gas;
        public string Serie_Analizador_Gas
        {
            get { return _Serie_Analizador_Gas; }
            set { _Serie_Analizador_Gas = value; }
        }
        /// <summary>
        /// Horario_Desde
        /// </summary>
        private DateTime _Horario_Desde;
        public DateTime Horario_Desde
        {
            get { return _Horario_Desde; }
            set { _Horario_Desde = value; }
        }
        /// <summary>
        /// Horario_Hasta
        /// </summary>
        private DateTime _Horario_Hasta;
        public DateTime Horario_Hasta
        {
            get { return _Horario_Hasta; }
            set { _Horario_Hasta = value; }
        }
        /// <summary>
        /// Dias_Semana
        /// </summary>
        private string _Dias_Semana;
        public string Dias_Semana
        {
            get { return _Dias_Semana; }
            set { _Dias_Semana = value; }
        }
        /// <summary>
        /// No_Registro_Ambiental
        /// </summary>
        private string _No_Registro_Ambiental;
        public string No_Registro_Ambiental
        {
            get { return _No_Registro_Ambiental; }
            set { _No_Registro_Ambiental = value; }
        }
        /// <summary>
        /// Tipo
        /// </summary>
        private string _Tipo;
        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        /// <summary>
        /// Estatus Registro
        /// </summary>
        private string _EstatusRegistro;
        public string EstatusRegistro
        {
            get { return _EstatusRegistro; }
            set { _EstatusRegistro = value; }
        }
        /// <summary>
        /// Teléfono Atencion 1
        /// </summary>
        private string _TelefonoAtn1;
        public string TelefonoAtn1
        {
            get { return _TelefonoAtn1; }
            set { _TelefonoAtn1 = value; }
        }
        /// <summary>
        /// Teléfono Atencion 2
        /// </summary>
        private string _TelefonoAtn2;
        public string TelefonoAtn2
        {
            get { return _TelefonoAtn2; }
            set { _TelefonoAtn2 = value; }
        }
        /// <summary>
        /// Apellido Paterno Representante Legal
        /// </summary>
        private string _DxApPaternoRepLeg;
        public string DxApPaternoRepLeg
        {
            get { return _DxApPaternoRepLeg; }
            set { _DxApPaternoRepLeg = value; }
        }
        /// <summary>
        /// Apellido Materno Representante Legal
        /// </summary>
        private string _DxApMaternoRepLeg;
        public string DxApMaternoRepLeg
        {
            get { return _DxApMaternoRepLeg; }
            set { _DxApMaternoRepLeg = value; }
        }
        /// <summary>
        /// Email Representante Legal
        /// </summary>
        private string _DxEmailRepreLegal;
        public string DxEmailRepreLegal
        {
            get { return _DxEmailRepreLegal; }
            set { _DxEmailRepreLegal = value; }
        }
        /// <summary>
        /// Teléfono Representante Legal
        /// </summary>
        private string _DxTelefonoRepreLeg;
        public string DxTelefonoRepreLeg
        {
            get { return _DxTelefonoRepreLeg; }
            set { _DxTelefonoRepreLeg = value; }
        }
        /// <summary>
        /// Celular Representante Legal
        /// </summary>
        private string _DxCelularRepreLeg;
        public string DxCelularRepreLeg
        {
            get { return _DxCelularRepreLeg; }
            set { _DxCelularRepreLeg = value; }
        }
        /// <summary>
        /// Apellido Paterno Responsable
        /// </summary>
        private string _DxApPaternoRepre;
        public string DxApPaternoRepre
        {
            get { return _DxApPaternoRepre; }
            set { _DxApPaternoRepre = value; }
        }
        /// <summary>
        /// Apellido Materno Responsable
        /// </summary>
        private string _DxApMaternoRepre;
        public string DxApMaternoRepre
        {
            get { return _DxApMaternoRepre; }
            set { _DxApMaternoRepre = value; }
        }
        /// <summary>
        /// Celular Responsable
        /// </summary>
        private string _DxCelularRepre;
        public string DxCelularRepre
        {
            get { return _DxCelularRepre; }
            set { _DxCelularRepre = value; }
        }
        // RSA 20120927 new fields End
    }
}
