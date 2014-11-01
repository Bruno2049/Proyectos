using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Helpers;
using PAEEEM.Entities;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Disposal branches
    /// </summary>
    public class CAT_CENTRO_DISP_SUCURSALDAL
    {
        /// <summary>
        /// readonly class instance
        /// </summary>
        private static readonly CAT_CENTRO_DISP_SUCURSALDAL _classinstance = new CAT_CENTRO_DISP_SUCURSALDAL();
        /// <summary>
        /// Property
        /// </summary>
        public static CAT_CENTRO_DISP_SUCURSALDAL ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get disposal center branches
        /// </summary>
        /// <param name="zone">zone parameter</param>
        /// <param name="DisposalID">disposal main center</param>
        /// <returns></returns>
        public DataTable GetDisposalBranches(int zone,int DisposalID)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT * FROM [dbo].[CAT_CENTRO_DISP_SUCURSAL] WHERE (@Cve_Zona=-1 or  [Cve_Zona] = @Cve_Zona) and (@DisposalID=-1 or [Id_Centro_Disp]=@DisposalID)";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Zona", zone),
                    new SqlParameter("@DisposalID", DisposalID)
                };

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get disposal branch with zone failed: Execute method GetDisposalBranches in CAT_CENTRO_DISP_SUCURSALDAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// Get supplier branch
        /// </summary>
        /// <param name="disposalBranchID">Supplier Branch ID</param>
        /// <returns></returns>
        public DataTable GetDisposalByBranch(int disposalBranchID)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT * FROM [dbo].[CAT_CENTRO_DISP_SUCURSAL] WHERE [Id_Centro_Disp_Sucursal] = @DisposalBranch";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@DisposalBranch", disposalBranchID)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get disposal with supplier branch id failed: Execute method GetDisposalByBranch in CAT_CENTRO_DISP_SUCURSALDAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// get technology with disposal or disposal branch
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="disposal"></param>
        /// <returns></returns>
        public DataTable GetTechnologyByDisposal(string userType,int disposal)
        {
            DataTable dtResult = null;
            string SQL = "";

            try
            {
                if (userType == GlobalVar.DISPOSAL_CENTER)
                {
                    SQL = "select B.Cve_Tecnologia,B.Dx_Nombre_General from K_CENTRO_DISP_TECNOLOGIA A inner join CAT_TECNOLOGIA B on A.Cve_Tecnologia=B.Cve_Tecnologia "+
                            " inner join CAT_CENTRO_DISP C on A.Fg_Tipo_Centro_Disp='M' and A.Id_Centro_Disp=C.Id_Centro_Disp";
                }
                else
                {
                    SQL = "select B.Cve_Tecnologia,B.Dx_Nombre_General from K_CENTRO_DISP_TECNOLOGIA A inner join CAT_TECNOLOGIA B on A.Cve_Tecnologia=B.Cve_Tecnologia " +
                            " inner join CAT_CENTRO_DISP_SUCURSAL C on A.Fg_Tipo_Centro_Disp='B' and A.Id_Centro_Disp=C.Id_Centro_Disp_Sucursal";
                }
                SQL += " and A.Id_Centro_Disp = @disposal";

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@disposal", disposal)
                };

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch(Exception ex)
            {
                throw new LsDAException(this, "Get technology with disposal id or disposal branch id failed: Execute method getTechnologyByDisposal in CAT_CENTRO_DISP_SUCURSALDAL.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DisposalID"></param>
        /// <returns></returns>
        public string GetDisposalCenterBranchNameByDisposalID(int DisposalID)
        {
            string DisposalName = "";
            try
            {
                string Sql = "select Dx_Razon_Social from CAT_CENTRO_DISP_SUCURSAL where Id_Centro_Disp_Sucursal='" + DisposalID + "'";
                object obj = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql);
                if (obj != null)
                {
                    DisposalName = obj.ToString();
                }
                return DisposalName;
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Get disposalBranceName failed: Execute method GetDisposalCenterBranchNameByDisposalID in CAT_CENTRO_DISP_SUCURSALDAL.", ex, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_BranchDisposalCenter(CAT_CENTRO_DISP_SUCURSALModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO CAT_CENTRO_DISP_SUCURSAL (Id_Centro_Disp,Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num," +
                                             " Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP," +
                                             " Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco," +
                                             " No_Empleados, Marca_Analizador_Gas, Modelo_Analizador_Gas, Serie_Analizador_Gas, Horario_Desde," +
                                             " Horario_Hasta, Dias_Semana, No_Registro_Ambiental, Tipo," +
                                             " Estatus_Registro, Telefono_Atn1, Telefono_Atn2, Dx_Ap_Paterno_Rep_Leg, Dx_Ap_Materno_Rep_Leg, Dx_Email_Repre_Legal," +
                                             " Dx_Telefono_Repre_Leg, Dx_Celular_Repre_Leg, Dx_Ap_Paterno_Repre, Dx_Ap_Materno_Repre, Dx_Celular_Repre," +
                                             " Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp_Sucursal,Cve_Zona,Codigo_Centro_Disp_Sucursal) VALUES (@Id_Centro_Disp,@Cve_Estatus_Centro_Disp,@Cve_Region,@Dx_Razon_Social," +
                                             " @Dx_Nombre_Comercial,@Dx_RFC,@Dx_Domicilio_Part_Calle,@Dx_Domicilio_Part_Num,@Dx_Domicilio_Part_CP,@Cve_Deleg_Municipio_Part,@Cve_Estado_Part," +
                                             " @Fg_Mismo_Domicilio,@Dx_Domicilio_Fiscal_Calle,@Dx_Domicilio_Fiscal_Num,@Dx_Domicilio_Fiscal_CP,@Cve_Deleg_Municipio_Fisc,@Cve_Estado_Fisc," +
                                             " @Dx_Nombre_Repre,@Dx_Email_Repre,@Dx_Telefono_Repre,@Dx_Nombre_Repre_Legal,@Dx_Nombre_Banco,@Dx_Cuenta_Banco," +
                                             " @No_Empleados, @Marca_Analizador_Gas, @Modelo_Analizador_Gas, @Serie_Analizador_Gas, @Horario_Desde," +
                                             " @Horario_Hasta, @Dias_Semana, @No_Registro_Ambiental, @Tipo," +
                                             " @Estatus_Registro, @Telefono_Atn1, @Telefono_Atn2, @Dx_Ap_Paterno_Rep_Leg, @Dx_Ap_Materno_Rep_Leg, @Dx_Email_Repre_Legal," +
                                             " @Dx_Telefono_Repre_Leg, @Dx_Celular_Repre_Leg, @Dx_Ap_Paterno_Repre, @Dx_Ap_Materno_Repre, @Dx_Celular_Repre," +
                                             " @Binary_Acta_Constitutiva,@Binary_Poder_Notarial,@Dt_Fecha_Centro_Disp_Sucursal,@Cve_Zona,@Codigo_Centro_Disp_Sucursal)";
                SqlParameter[] para = new SqlParameter[] {
                    new SqlParameter("@Id_Centro_Disp",instance.Id_Centro_Disp),
                    new SqlParameter("@Cve_Estatus_Centro_Disp",instance.Cve_Estatus_Centro_Disp),
                    new SqlParameter("@Cve_Region",instance.Cve_Region),
                    new SqlParameter("@Dx_Razon_Social",instance.Dx_Razon_Social),
                    new SqlParameter("@Dx_Nombre_Comercial",instance.Dx_Nombre_Comercial),
                    new SqlParameter("@Dx_RFC",instance.Dx_RFC),
                    new SqlParameter("@Dx_Domicilio_Part_Calle",instance.Dx_Domicilio_Part_Calle),
                    new SqlParameter("@Dx_Domicilio_Part_Num",instance.Dx_Domicilio_Part_Num),
                    new SqlParameter("@Dx_Domicilio_Part_CP",instance.Dx_Domicilio_Part_CP),
                    new SqlParameter("@Cve_Deleg_Municipio_Part",instance.Cve_Deleg_Municipio_Part),
                    new SqlParameter("@Cve_Estado_Part",instance.Cve_Estado_Part),
                    new SqlParameter("@Fg_Mismo_Domicilio",instance.Fg_Mismo_Domicilio),
                    new SqlParameter("@Dx_Domicilio_Fiscal_Calle",instance.Dx_Domicilio_Fiscal_Calle),
                    new SqlParameter("@Dx_Domicilio_Fiscal_Num",instance.Dx_Domicilio_Fiscal_Num),
                    new SqlParameter("@Dx_Domicilio_Fiscal_CP",instance.Dx_Domicilio_Fiscal_CP),
                    new SqlParameter("@Cve_Deleg_Municipio_Fisc",instance.Cve_Deleg_Municipio_Fisc),
                    new SqlParameter("@Cve_Estado_Fisc",instance.Cve_Estado_Fisc),
                    new SqlParameter("@Dx_Nombre_Repre",instance.Dx_Nombre_Repre),
                    new SqlParameter("@Dx_Email_Repre",instance.Dx_Email_Repre),
                    new SqlParameter("@Dx_Telefono_Repre",instance.Dx_Telefono_Repre),
                    new SqlParameter("@Dx_Nombre_Repre_Legal",instance.Dx_Nombre_Repre_Legal),
                    new SqlParameter("@Dx_Nombre_Banco",instance.Dx_Nombre_Banco),
                    new SqlParameter("@Dx_Cuenta_Banco",instance.Dx_Cuenta_Banco),

                    new SqlParameter("@No_Empleados",instance.No_Empleados),
                    new SqlParameter("@Marca_Analizador_Gas",instance.Marca_Analizador_Gas),
                    new SqlParameter("@Modelo_Analizador_Gas",instance.Modelo_Analizador_Gas),
                    new SqlParameter("@Serie_Analizador_Gas",instance.Serie_Analizador_Gas),
                    new SqlParameter("@Horario_Desde",instance.Horario_Desde),
                    new SqlParameter("@Horario_Hasta",instance.Horario_Hasta),
                    new SqlParameter("@Dias_Semana",instance.Dias_Semana),
                    new SqlParameter("@No_Registro_Ambiental",instance.No_Registro_Ambiental),
                    new SqlParameter("@Tipo",instance.Tipo),

                    new SqlParameter("@Estatus_Registro",instance.EstatusRegistro),
                    new SqlParameter("@Telefono_Atn1",instance.TelefonoAtn1),
                    new SqlParameter("@Telefono_Atn2",instance.TelefonoAtn2),
                    new SqlParameter("@Dx_Ap_Paterno_Rep_Leg",instance.DxApPaternoRepLeg),
                    new SqlParameter("@Dx_Ap_Materno_Rep_Leg",instance.DxApMaternoRepLeg),
                    new SqlParameter("@Dx_Email_Repre_Legal",instance.DxEmailRepreLegal),
                    new SqlParameter("@Dx_Telefono_Repre_Leg",instance.DxTelefonoRepreLeg),
                    new SqlParameter("@Dx_Celular_Repre_Leg",instance.DxCelularRepreLeg),
                    new SqlParameter("@Dx_Ap_Paterno_Repre",instance.DxApPaternoRepre),
                    new SqlParameter("@Dx_Ap_Materno_Repre",instance.DxApMaternoRepre),
                    new SqlParameter("@Dx_Celular_Repre",instance.DxCelularRepre),

                    new SqlParameter("@Binary_Acta_Constitutiva",instance.Binary_Poder_Notarial),
                    new SqlParameter("@Binary_Poder_Notarial",instance.Binary_Acta_Constitutiva),
                    new SqlParameter("@Dt_Fecha_Centro_Disp_Sucursal",instance.Dt_Fecha_Centro_Disp_Sucursal),
                    new SqlParameter("@Cve_Zona",instance.Cve_Zona),
                    new SqlParameter("@Codigo_Centro_Disp_Sucursal",instance.Codigo_Centro_Disp_Sucursal)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add CAT_CENTRO_DISP failed: Execute method Insert_MainDisposalCenter in CAT_CENTRO_DISPDAL.", ex, true);
            }
        }
        /// <summary>
        /// Update disposal center branch
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Update_BranchDisposalCenter(CAT_CENTRO_DISP_SUCURSALModel instance)
        {
            try
            {
                string executesqlstr = "UPDATE CAT_CENTRO_DISP_SUCURSAL SET Id_Centro_Disp=@Id_Centro_Disp,Cve_Region=@Cve_Region,Dx_Razon_Social=@Dx_Razon_Social,Dx_Nombre_Comercial=@Dx_Nombre_Comercial," +
                                             " Dx_RFC=@Dx_RFC,Dx_Domicilio_Part_Calle=@Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num=@Dx_Domicilio_Part_Num,Dx_Domicilio_Part_CP=@Dx_Domicilio_Part_CP," +
                                             " Cve_Deleg_Municipio_Part=@Cve_Deleg_Municipio_Part,Cve_Estado_Part=@Cve_Estado_Part,Fg_Mismo_Domicilio=@Fg_Mismo_Domicilio," +
                                             " Dx_Domicilio_Fiscal_Calle=@Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num=@Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP=@Dx_Domicilio_Fiscal_CP," +
                                             " Cve_Deleg_Municipio_Fisc=@Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc=@Cve_Estado_Fisc,Dx_Nombre_Repre=@Dx_Nombre_Repre,Dx_Email_Repre=@Dx_Email_Repre," +
                                             " Dx_Telefono_Repre=@Dx_Telefono_Repre,Dx_Nombre_Repre_Legal=@Dx_Nombre_Repre_Legal,Dx_Nombre_Banco=@Dx_Nombre_Banco,Dx_Cuenta_Banco=@Dx_Cuenta_Banco," +
                                             " No_Empleados=@No_Empleados, Marca_Analizador_Gas=@Marca_Analizador_Gas, Modelo_Analizador_Gas=@Modelo_Analizador_Gas, Serie_Analizador_Gas=@Serie_Analizador_Gas," +
                                             " Horario_Desde=@Horario_Desde, Horario_Hasta=@Horario_Hasta, Dias_Semana=@Dias_Semana, No_Registro_Ambiental=@No_Registro_Ambiental, Tipo=@Tipo," +
                                             " Estatus_Registro=@Estatus_Registro, Telefono_Atn1=@Telefono_Atn1, Telefono_Atn2=@Telefono_Atn2, Dx_Ap_Paterno_Rep_Leg=@Dx_Ap_Paterno_Rep_Leg, Dx_Ap_Materno_Rep_Leg=@Dx_Ap_Materno_Rep_Leg, Dx_Email_Repre_Legal=@Dx_Email_Repre_Legal, " +
                                             " Dx_Telefono_Repre_Leg=@Dx_Telefono_Repre_Leg, Dx_Celular_Repre_Leg=@Dx_Celular_Repre_Leg, Dx_Ap_Paterno_Repre=@Dx_Ap_Paterno_Repre, Dx_Ap_Materno_Repre=@Dx_Ap_Materno_Repre,Dx_Celular_Repre=@Dx_Celular_Repre," +
                                             " Binary_Acta_Constitutiva=@Binary_Acta_Constitutiva,Binary_Poder_Notarial=@Binary_Poder_Notarial,Dt_Fecha_Centro_Disp_Sucursal=@Dt_Fecha_Centro_Disp_Sucursal,Cve_Zona=@Cve_Zona" +
                                             " WHERE Id_Centro_Disp_Sucursal=@Id_Centro_Disp_Sucursal";
                SqlParameter[] para = new SqlParameter[] {
                    new SqlParameter("@Id_Centro_Disp",instance.Id_Centro_Disp),
                    new SqlParameter("@Cve_Region",instance.Cve_Region),
                    new SqlParameter("@Dx_Razon_Social",instance.Dx_Razon_Social),
                    new SqlParameter("@Dx_Nombre_Comercial",instance.Dx_Nombre_Comercial),
                    new SqlParameter("@Dx_RFC",instance.Dx_RFC),
                    new SqlParameter("@Dx_Domicilio_Part_Calle",instance.Dx_Domicilio_Part_Calle),
                    new SqlParameter("@Dx_Domicilio_Part_Num",instance.Dx_Domicilio_Part_Num),
                    new SqlParameter("@Dx_Domicilio_Part_CP",instance.Dx_Domicilio_Part_CP),
                    new SqlParameter("@Cve_Deleg_Municipio_Part",instance.Cve_Deleg_Municipio_Part),
                    new SqlParameter("@Cve_Estado_Part",instance.Cve_Estado_Part),
                    new SqlParameter("@Fg_Mismo_Domicilio",instance.Fg_Mismo_Domicilio),
                    new SqlParameter("@Dx_Domicilio_Fiscal_Calle",instance.Dx_Domicilio_Fiscal_Calle),
                    new SqlParameter("@Dx_Domicilio_Fiscal_Num",instance.Dx_Domicilio_Fiscal_Num),
                    new SqlParameter("@Dx_Domicilio_Fiscal_CP",instance.Dx_Domicilio_Fiscal_CP),
                    new SqlParameter("@Cve_Deleg_Municipio_Fisc",instance.Cve_Deleg_Municipio_Fisc),
                    new SqlParameter("@Cve_Estado_Fisc",instance.Cve_Estado_Fisc),
                    new SqlParameter("@Dx_Nombre_Repre",instance.Dx_Nombre_Repre),
                    new SqlParameter("@Dx_Email_Repre",instance.Dx_Email_Repre),
                    new SqlParameter("@Dx_Telefono_Repre",instance.Dx_Telefono_Repre),
                    new SqlParameter("@Dx_Nombre_Repre_Legal",instance.Dx_Nombre_Repre_Legal),
                    new SqlParameter("@Dx_Nombre_Banco",instance.Dx_Nombre_Banco),
                    new SqlParameter("@Dx_Cuenta_Banco",instance.Dx_Cuenta_Banco),

                    new SqlParameter("@No_Empleados",instance.No_Empleados),
                    new SqlParameter("@Marca_Analizador_Gas",instance.Marca_Analizador_Gas),
                    new SqlParameter("@Modelo_Analizador_Gas",instance.Modelo_Analizador_Gas),
                    new SqlParameter("@Serie_Analizador_Gas",instance.Serie_Analizador_Gas),
                    new SqlParameter("@Horario_Desde",instance.Horario_Desde),
                    new SqlParameter("@Horario_Hasta",instance.Horario_Hasta),
                    new SqlParameter("@Dias_Semana",instance.Dias_Semana),
                    new SqlParameter("@No_Registro_Ambiental",instance.No_Registro_Ambiental),
                    new SqlParameter("@Tipo",instance.Tipo),

                    new SqlParameter("@Estatus_Registro",instance.EstatusRegistro),
                    new SqlParameter("@Telefono_Atn1",instance.TelefonoAtn1),
                    new SqlParameter("@Telefono_Atn2",instance.TelefonoAtn2),
                    new SqlParameter("@Dx_Ap_Paterno_Rep_Leg",instance.DxApPaternoRepLeg),
                    new SqlParameter("@Dx_Ap_Materno_Rep_Leg",instance.DxApMaternoRepLeg),
                    new SqlParameter("@Dx_Email_Repre_Legal",instance.DxEmailRepreLegal),
                    new SqlParameter("@Dx_Telefono_Repre_Leg",instance.DxTelefonoRepreLeg),
                    new SqlParameter("@Dx_Celular_Repre_Leg",instance.DxCelularRepreLeg),
                    new SqlParameter("@Dx_Ap_Paterno_Repre",instance.DxApPaternoRepre),
                    new SqlParameter("@Dx_Ap_Materno_Repre",instance.DxApMaternoRepre),
                    new SqlParameter("@Dx_Celular_Repre",instance.DxCelularRepre),

                    new SqlParameter("@Binary_Acta_Constitutiva",instance.Binary_Poder_Notarial),
                    new SqlParameter("@Binary_Poder_Notarial",instance.Binary_Acta_Constitutiva),
                    new SqlParameter("@Dt_Fecha_Centro_Disp_Sucursal",instance.Dt_Fecha_Centro_Disp_Sucursal),
                    new SqlParameter("@Cve_Zona",instance.Cve_Zona),
                    new SqlParameter("@Id_Centro_Disp_Sucursal",instance.Id_Centro_Disp_Sucursal)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "update disposal failed: Execute method Update_MainDisposalCenter in CAT_CENTRO_DISPDAL.", ex, true);
            }
        }
        /// <summary>
        /// Select disposal center branch
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Select_BranchDisposalCenter(CAT_CENTRO_DISP_SUCURSALModel instance)
        {
            int count = 0;

            try
            {
                string executesqlstr = "SELECT COUNT(*) FROM CAT_CENTRO_DISP_SUCURSAL WHERE Id_Centro_Disp=@Id_Centro_Disp and Cve_Region=@Cve_Region AND Cve_Zona=@Cve_Zona";
                SqlParameter[] para = new SqlParameter[] {
                    new SqlParameter("@Id_Centro_Disp",instance.Id_Centro_Disp),
                    new SqlParameter("@Cve_Region",instance.Cve_Region),
                    new SqlParameter("@Cve_Zona",instance.Cve_Zona)
                };
                object o = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
                if (o != null)
                    count = Convert.ToInt32(o);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "update disposal failed: Execute method Update_MainDisposalCenter in CAT_CENTRO_DISPDAL.", ex, true);
            }

            return count;
        }

        /// <summary>
        /// get disposal branch by disposal
        /// </summary>
        /// <param name="disposal"></param>
        /// <returns></returns>
        public DataTable GetDisposalBranchByDisposal(string disposal)
        {
            DataTable dtResult = null;
            string SQL = "";

            try
            {

                SQL = " select * from CAT_CENTRO_DISP_SUCURSAL where Id_Centro_Disp=@Id_Centro_Disp";

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Centro_Disp", disposal)
                };

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Get disposal branch by disposal failed: Execute method GetDisposalBranchByDisposal in CAT_CENTRO_DISP_SUCURSALDAL.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// Active/Desactive/Cancel Disposal Branch
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int UpdateDisposalBranchStatus(string BranchID, int Status)
        {
            int Result = 0;
            try
            {
                string Sql = "Update CAT_CENTRO_DISP_SUCURSAL set Cve_Estatus_Centro_Disp=@Cve_Estatus_Centro_Disp where Id_Centro_Disp_Sucursal in(" + BranchID + ");";

                if (Status == (int)DisposalCenterStatus.INACTIVO)
                {
                    Sql += "UPDATE US_USUARIO SET Estatus = 'I' FROM US_USUARIO AS u WITH(NOLOCK) WHERE u.Id_Departamento IN (" + BranchID + ") AND u.Tipo_Usuario = 'D_C_B'";
                }
                else if (Status == (int)DisposalCenterStatus.CANCELADO)
                {
                    Sql += "UPDATE US_USUARIO SET Estatus = 'C' FROM US_USUARIO AS u WITH(NOLOCK) WHERE u.Id_Departamento IN (" + BranchID + ") AND u.Tipo_Usuario = 'D_C_B'";
                }
                else if (Status == (int)DisposalCenterStatus.ACTIVO)
                {
                    Sql += "UPDATE US_USUARIO SET Estatus = 'A' FROM US_USUARIO AS u WITH(NOLOCK) WHERE u.Id_Departamento IN (" + BranchID + ") AND u.Tipo_Usuario = 'D_C_B'";
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Estatus_Centro_Disp", Status)
                  };
                Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, paras);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Change Branch Status failed:Execute method  UpdateDisposalBranchStatus in CAT_CENTRO_DISP_SUCURSALDAL.", ex, true);
            }
            return Result;
        }     
    }
}
