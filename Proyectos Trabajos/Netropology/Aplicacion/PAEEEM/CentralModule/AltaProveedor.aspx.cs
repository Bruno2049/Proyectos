using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.AccesoDatos;
using PAEEEM.LogicaNegocios;
using PAEEEM.Entidades.Utilizables;
using PAEEEM.Entidades;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using PAEEEM.LogicaNegocios.LOG;
using Telerik.Web.UI;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;
using System.Text.RegularExpressions;
using PAEEEM.Entidades.CirculoCredito;
using PAEEEM.Entidades.ModuloCentral;


namespace PAEEEM.CentralModule
{
    public partial class AltaProveedor : System.Web.UI.Page
    {
        int t_Estado;
        int t_deleg;
        int t_colonia;
        int valorId;
        CAT_CODIGO_POSTAL_SEPOMEX sepo;
        private int con_id_pro;

        private int SupplierId
        {
            get
            {
                return ViewState["SupplierId"] == null ? 0 : Convert.ToInt32(ViewState["SupplierId"].ToString());
            }
            set
            {
                ViewState["SupplierId"] = value;
            }
        }
        private string SupplierType
        {
            get
            {
                return ViewState["SupplierType"] == null ? "" : ViewState["SupplierType"].ToString();
            }
            set
            {
                ViewState["SupplierType"] = value;
            }
        }

        private string tipoCON
        {
            get
            {
                return ViewState["SupplierTipo"] == null ? "" : ViewState["SupplierTipo"].ToString();
            }
            set
            {
                ViewState["SupplierTipo"] = value;
            }
        }
       
        //
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                cargaEstado();
                comb();
                activar();
                cargaestadosFIS();
                carga_cmbZona();
                t_Estado = 0;
                t_deleg = 0;
                t_colonia = 0;
                con_id_pro = 0;
                cmb_TipoPersonaMAT();
                valorId = 0;
                RAD_UPPoderNot.MaxFileInputsCount = 1;
                RAD_UPActa_Const.MaxFileInputsCount = 1;

                RAD_DTP.MinDate = new DateTime(1930, 1, 1);
                RAD_DTP.MaxDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

                listPDFTemporal = new List<TemporalPDFs>();
                iniciarEdicion();
              
                                
            }

        }


        private void iniciarEdicion()
        {
            if (Request.QueryString["SupplierID"] == null || Request.QueryString["SupplierID"] == "" ||
                Request.QueryString["Type"] == null || Request.QueryString["Type"] == "") return;

            SupplierId = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["SupplierID"].Replace("%2B", "+"))));
            SupplierType = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Type"].Replace("%2B", "+")));
            tipoCON = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Tipo"].Replace("%2B", "+")));
           
            cargaInformacionEdicion();
          
        }
      
        private void cargaInformacionEdicion()
        {
            CAT_PROVEEDOR proveedor = null;
            CAT_PROVEEDORBRANCH branch = null;
            try
            {
                //rad_cmbTipoPro.SelectedValue = "2";
                //rad_cmbTipoPro.Enabled = false;
                lbl_tipoProveedor.Visible = false;
                rad_cmbTipoPro.Visible = false;

                lbl_TipoPersona.Visible = false;
                rad_cmbTipoPersona.Visible = false;
                cmb_tipoPersonaSUC();
              
                rad_cmbTipoPersona.Enabled = false;
                RAD_cmbSiNoDomicilio.Enabled = false;

                RAD_txtNomRES.Enabled = false;
                RAD_txtNomRL.Enabled = false;
                RAD_NtxtTelefonoRES.Enabled = false;
                RAD_ktxtCorreoRES.Enabled = false;
                
                    
                
                #region prov
                if (SupplierType.ToLower() == "proveedor" || SupplierType.ToLower() == "sb_f" || SupplierType.ToLower() == "sb_v")
                {


                    if (SupplierType.ToLower() == "sb_f" || SupplierType.ToLower() == "sb_v")
                    {
                        //BloquearControles

                        ///
                        branch = LogicaNegocios.ModuloCentral.AltaProveedor.objetoViejo(SupplierId);
                        proveedor = LogicaNegocios.ModuloCentral.AltaProveedor.obtienePORidConsulta(branch.Id_Proveedor);
                        rad_cmbTipoPersona.SelectedValue = proveedor.Id_Proveedor.ToString();

                        ////
                        RAD_FIS_nCodPos.Text = proveedor.Dx_Domicilio_Fiscal_CP;
                        cargaXcodPostalFIS();


                        RAD_txtNCodPos.Text = proveedor.Dx_Domicilio_Part_CP;
                        cargaXcodPostal(RAD_txtNCodPos.Text);
                        RAD_cmbColonia.SelectedValue = proveedor.Cve_CP_Part.ToString();
                        //

                        lbl_RAD_ApeMat.Visible = false;
                        RAD_txtApeMat.Visible = false;
                        RAD_txtNombre.Enabled = false;
                        RAD_txtApePat.Enabled = false;
                        RAD_txtRFC.Enabled = false;
                        RAD_DTP.Enabled = false;
                        RAD_txtNCodPos.Enabled = false;
                        RAD_txtCalle.Enabled = false;
                        RAD_txtNumero.Enabled = false;

                        btn_Salir_Sucursal.Visible = true;
                        //
                        RAD_btnFinalizar.Visible = false;
                        RAD_EDITAR_FINALIZAR.Visible = false;
                        //

                        if (proveedor.Fg_Mismo_Domicilio == null)
                        {
                            RAD_cmbSiNoDomicilio.SelectedIndex = 0;
                        }
                        else if (proveedor.Fg_Mismo_Domicilio == true)
                        {
                            RAD_cmbSiNoDomicilio.SelectedIndex = 1;
                        }
                        else
                        {
                            RAD_cmbSiNoDomicilio.SelectedIndex = 2;
                        }

                        habilitar();

                        RAD_FIS_calle.Enabled = false;
                        RAD_FIS_nCodPos.Enabled = false;
                        RAD_FIS_NUMERO.Enabled = false;
                        RAD_FIS_cmbEstados.Enabled = false;
                        RAD_FIS_DelegOMuni.Enabled = false;
                        RAD_FIS_Colonia.Enabled = false;
                        rad_CMB_ZONA.Enabled = false;
                        IVA.Enabled = false;
                       

                        RAD_txtAPRES.Enabled = false;
                        RAD_txtAMRES.Enabled = false;
                        RAD_txtAMRL.Enabled = false;
                        RAD_txtAPRL.Enabled = false;

                        rad_CMB_ZONA.Enabled = false;
                        RAD_cmbColonia.Enabled = false;

                        RAD_cmbSiNoDomicilio.Enabled = false;

                        
                        //
                        RAD_txtApeMat.Visible = true;
                        RAD_txtApeMat.Enabled = false;
                        lbl_RAD_ApeMat.Visible = true;
                        ////

                    } //--Matriz
                    else
                    {
                        RAD_txtApeMat.Visible = true;
                        RAD_txtApeMat.Enabled = true;
                        lbl_RAD_ApeMat.Visible = true;

                        lbl_Motivos.Visible = true;
                        txt_Motivos_Editar.Width = Unit.Percentage(80);
                        txt_Motivos_Editar.Visible = true;

                        proveedor = LogicaNegocios.ModuloCentral.AltaProveedor.obtienePORidConsulta(SupplierId);
                        rad_cmbTipoPersona.SelectedValue = proveedor.Id_Proveedor.ToString();

                        RAD_FIS_nCodPos.Text = proveedor.Dx_Domicilio_Fiscal_CP;
                        cargaXcodPostalFIS();

                        ///
                        RAD_txtNCodPos.Text = proveedor.Dx_Domicilio_Part_CP;

                        cargaXcodPostal(RAD_txtNCodPos.Text);
                        RAD_cmbColonia.SelectedValue = proveedor.Cve_CP_Part.ToString();
                        //




                        if (proveedor.Fg_Mismo_Domicilio == null)
                        {
                            RAD_cmbSiNoDomicilio.SelectedIndex = 0;
                        }
                        else if (proveedor.Fg_Mismo_Domicilio == true)
                        {
                            RAD_cmbSiNoDomicilio.SelectedIndex = 1;
                        }
                        else
                        {
                            RAD_cmbSiNoDomicilio.SelectedIndex = 2;
                        }

                        habilitar();

                        RAD_txtNombre.Enabled = true;
                        RAD_txtApePat.Enabled = true;
                        RAD_txtRFC.Enabled = true;
                        RAD_DTP.Enabled = true;
                        RAD_txtNCodPos.Enabled = true;
                        RAD_txtCalle.Enabled = true;
                        RAD_txtNumero.Enabled = true;

                        RAD_txtNomRES.Enabled = true;
                        RAD_txtNomRL.Enabled = true;
                        RAD_ktxtCorreoRES.Enabled = true;
                        RAD_NtxtTelefonoRES.Enabled = true;


                        RAD_FIS_calle.Enabled = true;
                        RAD_FIS_nCodPos.Enabled = true;
                        RAD_FIS_NUMERO.Enabled = true;
                        RAD_FIS_cmbEstados.Enabled = true;
                        RAD_FIS_DelegOMuni.Enabled = true;
                        RAD_FIS_Colonia.Enabled = true;
                        rad_CMB_ZONA.Enabled = true;
                        IVA.Enabled = true;

                        //
                        RAD_FIS_calle.Enabled = true;
                        RAD_FIS_cmbEstados.Enabled = true;
                        RAD_FIS_nCodPos.Enabled = true;
                        RAD_FIS_Colonia.Enabled = true;
                        RAD_FIS_DelegOMuni.Enabled = true;
                        RAD_FIS_NUMERO.Enabled = true;

                        RAD_cmbSiNoDomicilio.Enabled = true;

                        rad_CMB_ZONA.Enabled = true;
                        ////

                        RAD_btnSalir.Visible = true;
                        RAD_EDITAR_FINALIZAR.Visible = true;

                    }


                    if (proveedor == null)
                    {
                        return;
                    }
                    else
                    {
                        if (proveedor.Cve_Tipo_Sociedad == 1)//Fisica
                        {
                            RAD_txtNombre.Text = proveedor.Nombre;
                            RAD_txtApePat.Text = proveedor.Apellido_Paterno;
                            RAD_txtApeMat.Text = proveedor.Apellido_Materno;
                        }
                        else
                        {
                            lbl_RAD_Nombre_Razon.Text = "RAZON SOCIAL";
                            lbl_RAD_ApePAT.Text = "NOMBRE COMERCIAL";
                            lbl_RAD_Fecha.Text = "FECHA DE REGISTRO";
                            lbl_RAD_ApeMat.Visible = false;
                            RAD_txtApeMat.Visible = false;
                            RAD_txtNombre.Text = proveedor.Dx_Razon_Social;
                            RAD_txtApePat.Text = proveedor.Dx_Nombre_Comercial;
                            RAD_txtNombre.Width = Unit.Percentage(100);
                            RAD_txtApePat.Width = Unit.Percentage(100);
                        }




                        

                        //carga

                        RAD_txtRFC.Text = proveedor.Dx_RFC;
                        RAD_DTP.SelectedDate = proveedor.Fecha_Reg_Nac;

                        RAD_FIS_Colonia.SelectedValue = proveedor.Cve_CP_Fiscal.ToString();

                        RAD_txtCalle.Text = proveedor.Dx_Domicilio_Part_Calle;
                        RAD_txtNumero.Text = proveedor.Dx_Domicilio_Part_Num;

                        RAD_FIS_calle.Text = proveedor.Dx_Domicilio_Fiscal_Calle;

                        RAD_FIS_NUMERO.Text = proveedor.Dx_Domicilio_Fiscal_Num;

                        
                        RAD_txtNomRES.Text = proveedor.Nombre_Responsable;
                        RAD_txtAPRES.Text = proveedor.Apellido_Paterno_Resp;
                        RAD_txtAMRES.Text = proveedor.Apellido_Materno_Resp;

                        //RAD_txtNomRL.Width = Unit.Percentage(100);
                        RAD_txtNomRL.Text = proveedor.Nombre_Rep_Legal;
                        RAD_txtAPRL.Text = proveedor.Apellido_Paterno_Rep_Legal;
                        RAD_txtAMRL.Text = proveedor.Apellido_Materno_Rep_Legal;

                        RAD_ktxtCorreoRES.Text = proveedor.Dx_Email_Repre;
                        RAD_NtxtTelefonoRES.Text = proveedor.Dx_Telefono_Repre;


                        RAD_txtNombreBanco.Text = proveedor.Dx_Nombre_Banco;
                        RAD_txtNumClabe.Text = proveedor.Dx_Cuenta_Banco;
                        IVA.SelectedValue = proveedor.Pct_Tasa_IVA.ToString();

                        rad_CMB_ZONA.SelectedValue = proveedor.Cve_Zona.ToString();
                        if (proveedor.Acta_Constitutiva != null)
                        {
                            ActaConst_TEMP = proveedor.Acta_Constitutiva;
                            ver_ACTA_TEMP.ImageUrl = "~/CentralModule/images/visualizar.png";
                            ver_ACTA_TEMP.Visible = true;
                        }

                        if (proveedor.Poder_Notarial != null)
                        {
                            podNotarial_TEMP = proveedor.Poder_Notarial;
                            ver_POD_TEMP.ImageUrl = "~/CentralModule/images/visualizar.png";
                            ver_POD_TEMP.Visible = true;
                        }



                        carga_cmbZona_Cunsulta((int)proveedor.Cve_Region);

                        rad_CMB_ZONA.SelectedValue = proveedor.Cve_Zona.ToString();
                    }


                    RAD_txtNombreBanco.Enabled = false;
                    RAD_txtNumClabe.Enabled = false;

                    RAD_UPActa_Const.Visible = false;
                    RAD_UPPoderNot.Visible = false;



                    //
                    RAD_EDITAR_FINALIZAR.Visible = false;

                    //IVA.Enabled = true;
                    //rad_CMB_ZONA.Enabled = true;

                    

                    var estado = cargaXcodPostal_Fisica(proveedor.Dx_Domicilio_Part_CP);
                    if (estado == null)
                    {

                        return;
                    }



                    var sucursales_Fisicas = new LogicaNegocios.ModuloCentral.AltaProveedor().obtieneSucursales_x_idMatriz_Fisica(proveedor.Id_Proveedor);
                    //obtienetodosXmatriz_Fisica obtieneSucursales_x_idMatriz_Fisica

                    if (sucursales_Fisicas != null)
                    {
                        TemporalPDFs tt = new TemporalPDFs();
                        for (int i = 0; i < sucursales_Fisicas.Count; i++)
                        {

                            tt.acta = sucursales_Fisicas[i].Acta_Constitutiva;
                            tt.nocredit = sucursales_Fisicas[i].Id_Branch.ToString();

                            listPDFTemporal.Add(tt);

                        }
                    }
                   

                    RG_SUC_FIS.DataSource = sucursales_Fisicas.OrderBy(l => l.Id_Branch);
                    RG_SUC_FIS.DataBind();

                    List<datosSucursalVirtual> sucursales_Virtuales = new List<datosSucursalVirtual>();
                    foreach (var item in sucursales_Fisicas)
                    {
                        sucursales_Virtuales.AddRange(new LogicaNegocios.ModuloCentral.AltaProveedor().listaobtieneSucursales_x_idMatriz_Virtual_lista2(item.Id_Branch, (int)item.Cve_Estado_Part, (int)item.Cve_Deleg_Municipio_Part, item.Dx_Domicilio_Part_CP, item.Dx_Nombre_Comercial));
                    }
                    //var sucursales_Virtuales =new LogicaNegocios.ModuloCentral.AltaProveedor().listaobtieneSucursales_x_idMatriz_Virtual_lista(proveedor.Id_Proveedor);
                    RG_SUC_VIRTUALES.DataSource = sucursales_Virtuales;
                    RG_SUC_VIRTUALES.DataBind();

                    RG_SUC_VIRTUALES.Visible = true;
                    RG_SUC_FIS.Visible = true;
                    lbl_FIS.Visible = true;
                    lbl_VIR.Visible = true;

                    //
                    hiddentext.Text = proveedor.Id_Proveedor.ToString();

                    validarRadGrid();

                }
                #endregion

                #region consulta
                ////Consultar
                if (SupplierType.ToLower() == "consultar")
                {

                    if (tipoCON.ToLower()=="proveedor")
                    {
                        proveedor = LogicaNegocios.ModuloCentral.AltaProveedor.obtienePORidConsulta(SupplierId);
                        rad_cmbTipoPersona.SelectedValue = SupplierId.ToString();
                    }
                    else if (tipoCON.ToLower() == "sb_f" || tipoCON.ToLower() == "sb_v")
                    {
                        branch = LogicaNegocios.ModuloCentral.AltaProveedor.objetoViejo(SupplierId);
                        proveedor = LogicaNegocios.ModuloCentral.AltaProveedor.obtienePORidConsulta(branch.Id_Proveedor);
                        rad_cmbTipoPersona.SelectedValue = branch.Id_Proveedor.ToString();
                    }
                    

                    if (proveedor == null)
                    {
                        return;
                    }
                    else
                    {

                        if (proveedor.Cve_Tipo_Sociedad == 1)//Fisica
                        {
                            RAD_txtApeMat.Visible = true;
                            RAD_txtApeMat.Enabled = false;
                            lbl_RAD_ApeMat.Visible = true;
                            RAD_txtNombre.Text = proveedor.Nombre;
                            RAD_txtApePat.Text = proveedor.Apellido_Paterno;
                            RAD_txtApeMat.Text = proveedor.Apellido_Materno;
                        }
                        else
                        {
                            lbl_RAD_Nombre_Razon.Text = "RAZON SOCIAL";
                            lbl_RAD_ApePAT.Text = "NOMBRE COMERCIAL";
                            lbl_RAD_Fecha.Text = "FECHA DE REGISTRO";
                            lbl_RAD_ApeMat.Visible = false;
                            RAD_txtApeMat.Visible = false;
                            RAD_txtNombre.Text = proveedor.Dx_Razon_Social;
                            RAD_txtApePat.Text = proveedor.Dx_Nombre_Comercial;
                            
                        }


                        //lbl_RAD_ApeMat.Visible = false;
                        //RAD_txtApeMat.Visible = false;
                        RAD_txtNombre.Enabled = false;
                        RAD_txtApePat.Enabled = false;
                        RAD_txtRFC.Enabled = false;
                        RAD_DTP.Enabled = false;
                        RAD_txtNCodPos.Enabled = false;
                        RAD_txtCalle.Enabled = false;
                        RAD_txtNumero.Enabled = false;



                       
                        rad_CMB_ZONA.Enabled = false;
                        IVA.Enabled = false;

                        RAD_txtAPRES.Enabled = false;
                        RAD_txtAMRES.Enabled = false;
                        RAD_txtAMRL.Enabled = false;
                        RAD_txtAPRL.Enabled = false;

                        //lbl_apeMat_RL.Visible = false;
                        //lbl_apePat_RL.Visible = false;

                        //lbl_apePat_RES.Visible = false;
                        //lbl_apeMat_RES.Visible = false;

                        RAD_FIS_nCodPos.Text = proveedor.Dx_Domicilio_Fiscal_CP;
                        cargaXcodPostalFIS();


                        RAD_txtNCodPos.Text = proveedor.Dx_Domicilio_Part_CP;
                        cargaXcodPostal(RAD_txtNCodPos.Text);
                        RAD_cmbColonia.SelectedValue = proveedor.Cve_CP_Part.ToString();
                        RAD_FIS_Colonia.SelectedValue = proveedor.Cve_CP_Fiscal.ToString();
                        //lbl_RAD_Fecha.Text = "FECHA DE REGISTRO";


                        //carga
                      
                        RAD_txtRFC.Text = proveedor.Dx_RFC;
                        RAD_DTP.SelectedDate = proveedor.Fecha_Reg_Nac;

                        
                        RAD_txtCalle.Text = proveedor.Dx_Domicilio_Part_Calle;
                        RAD_txtNumero.Text = proveedor.Dx_Domicilio_Part_Num;



                        RAD_FIS_calle.Text = proveedor.Dx_Domicilio_Fiscal_Calle;
                      
                        RAD_FIS_NUMERO.Text = proveedor.Dx_Domicilio_Fiscal_Num;

                        

                     

                        RAD_txtNomRES.Text = proveedor.Nombre_Responsable;
                        RAD_txtAPRES.Text = proveedor.Apellido_Paterno_Resp;
                        RAD_txtAMRES.Text = proveedor.Apellido_Materno_Resp;
                        RAD_txtNomRL.Enabled = false;
                        RAD_txtNomRES.Enabled = false;

                        RAD_ktxtCorreoRES.Enabled = false;
                        RAD_NtxtTelefonoRES.Enabled = false;


                        RAD_txtNombreBanco.Enabled = false;
                        RAD_txtNumClabe.Enabled = false;
                        IVA.Enabled = false;
                        rad_CMB_ZONA.Enabled = false;

                        RAD_ktxtCorreoRES.Width = Unit.Percentage(100);
                       RAD_txtNomRL.Text = proveedor.Nombre_Responsable;
                       RAD_txtAPRL.Text = proveedor.Apellido_Paterno_Rep_Legal;
                       RAD_txtAMRL.Text = proveedor.Apellido_Materno_Rep_Legal;
                       
                       

                        RAD_ktxtCorreoRES.Text = proveedor.Dx_Email_Repre;
                        RAD_NtxtTelefonoRES.Text = proveedor.Dx_Telefono_Repre;


                        RAD_txtNombreBanco.Text = proveedor.Dx_Nombre_Banco;
                        RAD_txtNumClabe.Text = proveedor.Dx_Cuenta_Banco;
                        IVA.SelectedValue = proveedor.Pct_Tasa_IVA.ToString();


                        rad_CMB_ZONA.SelectedValue = proveedor.Cve_Zona.ToString();
                     

                        if (proveedor.Acta_Constitutiva != null)
                        {
                            ActaConst_TEMP = proveedor.Acta_Constitutiva;
                            ver_ACTA_TEMP.ImageUrl = "~/CentralModule/images/visualizar.png";
                            ver_ACTA_TEMP.Visible = true;
                        }

                        if (proveedor.Poder_Notarial != null)
                        {
                            podNotarial_TEMP = proveedor.Poder_Notarial;
                            ver_POD_TEMP.ImageUrl = "~/CentralModule/images/visualizar.png";
                            ver_POD_TEMP.Visible = true;
                        }




                        if (proveedor.Fg_Mismo_Domicilio == null)
                        {
                            RAD_cmbSiNoDomicilio.SelectedIndex = 0;
                        }
                        else if (proveedor.Fg_Mismo_Domicilio == true)
                        {
                            RAD_cmbSiNoDomicilio.SelectedIndex = 1;
                        }
                        else
                        {
                            RAD_cmbSiNoDomicilio.SelectedIndex = 2;
                        }

                        habilitar();

                        carga_cmbZona_Cunsulta((int)proveedor.Cve_Region);

                        rad_CMB_ZONA.SelectedValue = proveedor.Cve_Zona.ToString();


                        carga_cmbZona_Cunsulta((int)proveedor.Cve_Region);

                        rad_CMB_ZONA.Enabled = false;

                        RAD_cmbSiNoDomicilio.Enabled = false;
                    }


                    RAD_txtNombreBanco.Enabled = false;
                    RAD_txtNumClabe.Enabled = false;
                    RAD_UPActa_Const.Visible = false;
                    RAD_UPPoderNot.Visible = false;

                    RAD_btnFinalizar.Visible = false;

                    RAD_EDITAR_FINALIZAR.Visible = false;

                    btn_Salir_Sucursal.Visible = true;

                    RAD_btnSalir.Visible = false;

                    rad_cmbTipoPersona.Enabled = false;
                    rad_cmbTipoPro.Enabled = false;

                    RAD_cmbEstados.Enabled = false;
                    RAD_cmbDelMun.Enabled = false;
                    RAD_cmbColonia.Enabled = false;


                    var sucursales_Fisicas = new LogicaNegocios.ModuloCentral.AltaProveedor().obtieneSucursales_x_idMatriz_Fisica(proveedor.Id_Proveedor);

                    if (sucursales_Fisicas != null)
                    {
                        TemporalPDFs tt = new TemporalPDFs();
                        for (int i = 0; i < sucursales_Fisicas.Count; i++)
                        {

                            tt.acta = sucursales_Fisicas[i].Acta_Constitutiva;
                            tt.nocredit = sucursales_Fisicas[i].Id_Branch.ToString();

                            listPDFTemporal.Add(tt);

                        }
                    }

                    RG_SUC_FIS.DataSource = sucursales_Fisicas.OrderBy(l=>l.Id_Branch);
                    

                    RG_SUC_FIS.Enabled = false;

                    List<datosSucursalVirtual> sucursales_Virtuales = new List<datosSucursalVirtual>();
                    foreach (var item in sucursales_Fisicas)
                    {
                        sucursales_Virtuales.AddRange(new LogicaNegocios.ModuloCentral.AltaProveedor().listaobtieneSucursales_x_idMatriz_Virtual_lista2(item.Id_Branch, (int)item.Cve_Estado_Part, (int)item.Cve_Deleg_Municipio_Part, item.Dx_Domicilio_Part_CP, item.Dx_Nombre_Comercial));
                    }
                    
                    RG_SUC_VIRTUALES.DataSource = sucursales_Virtuales;
                    


                    RG_SUC_VIRTUALES.Enabled = false;

                    RG_SUC_VIRTUALES.Visible = true;
                    RG_SUC_FIS.Visible = true;
                    lbl_FIS.Visible = true;
                    lbl_VIR.Visible = true;
                    RAD_FIS_calle.Enabled = false;
                    RAD_FIS_nCodPos.Enabled = false;
                    RAD_FIS_NUMERO.Enabled = false;
                    RAD_FIS_cmbEstados.Enabled = false;
                    RAD_FIS_DelegOMuni.Enabled = false;
                    RAD_FIS_Colonia.Enabled = false;
                    RG_SUC_FIS.DataBind();
                    RG_SUC_VIRTUALES.DataBind();


                    hiddentext.Text = proveedor.Id_Proveedor.ToString();
                    validarRadGrid();
                }
                #endregion

            }
            catch (Exception)
            {

                return;
            }


        }
        /// <summary>
        
        //rad

        protected void validarRadGrid()
        {
            List<datosProveedorBranch> LstDatos = null;
            try
            {
                LstDatos = new LogicaNegocios.ModuloCentral.AltaProveedor().obtieneSucursales_x_idMatriz_Fisica(int.Parse(rad_cmbTipoPersona.SelectedValue));
            }
            catch (Exception)
            {
                return;
            }
            listPDFTemporal.Clear();
            if (LstDatos != null)
            {
                TemporalPDFs tt = new TemporalPDFs();
                for (int i = 0; i < LstDatos.Count; i++)
                {

                    tt.acta = LstDatos[i].Acta_Constitutiva;
                    tt.nocredit = LstDatos[i].Id_Branch.ToString();

                    listPDFTemporal.Add(tt);

                }

            }



            for (int i = 0; i < LstDatos.Count; i++)
            {
                foreach (GridDataItem dataItem in RG_SUC_FIS.MasterTableView.Items)
                {

                    if (LstDatos[i].Id_Branch == (int.Parse(RG_SUC_FIS.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Id_Branch").ToString())))
                   
                    {
                        (dataItem.FindControl("ver_actaNotarial_FIS") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatos[i].Id_Branch + "','carta','carga')");


                        var credi = listPDFTemporal.Find(o => o.nocredit == RG_SUC_FIS.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Id_Branch").ToString());

                            

                                if (credi.acta != null)
                                {
                                    (dataItem.FindControl("ver_actaNotarial_FIS") as ImageButton).Visible = true;                                    
                                   RG_SUC_FIS.MasterTableView.Items[dataItem.ItemIndex]["podernotarial"].Visible = true;
                                }
                               

                            

                        }

                     }

                }
        }

        
        
        //

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RAD_EDITAR_FINALIZAR_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                try
                {
                    var datosAnteriores = LogicaNegocios.ModuloCentral.AltaProveedor.obtienePORidConsulta(SupplierId);
                    var proveedor = LogicaNegocios.ModuloCentral.AltaProveedor.obtienePORidConsulta(SupplierId);

                    proveedor.Dx_Razon_Social = RAD_txtNombre.Text;

                    proveedor.Dx_Nombre_Comercial = RAD_txtApePat.Text;
                    proveedor.Dx_RFC = RAD_txtRFC.Text;

                    proveedor.Dx_Domicilio_Part_Calle = RAD_txtCalle.Text;
                    proveedor.Dx_Domicilio_Part_Num = RAD_txtNumero.Text;
                    proveedor.Dx_Domicilio_Part_CP = RAD_txtNCodPos.Text;
                    proveedor.Cve_Estado_Part = int.Parse(RAD_cmbEstados.SelectedValue);
                    proveedor.Cve_Deleg_Municipio_Part = int.Parse(RAD_cmbDelMun.SelectedValue);//
                    
                    proveedor.Cve_CP_Part = int.Parse(RAD_cmbColonia.SelectedValue);
                    proveedor.Fecha_Reg_Nac = RAD_DTP.SelectedDate.Value.Date;
                    proveedor.Dx_Domicilio_Fiscal_Colonia = RAD_FIS_Colonia.SelectedItem.Text;
                    proveedor.Dx_Domicilio_Part_Colonia = RAD_cmbColonia.SelectedItem.Text;

                    if (RAD_cmbSiNoDomicilio.SelectedValue == "1")
                    {
                        proveedor.Fg_Mismo_Domicilio = true;
                    }
                    else
                    {
                        proveedor.Fg_Mismo_Domicilio = false;
                    }

                    proveedor.Dx_Domicilio_Fiscal_Calle = RAD_FIS_calle.Text;
                    proveedor.Dx_Domicilio_Fiscal_Num = RAD_FIS_NUMERO.Text;
                    proveedor.Dx_Domicilio_Fiscal_CP = RAD_FIS_nCodPos.Text;
                    proveedor.Cve_CP_Fiscal = int.Parse(RAD_FIS_Colonia.SelectedValue);

                    proveedor.Cve_Deleg_Municipio_Fisc = int.Parse(RAD_FIS_DelegOMuni.SelectedValue);
                    proveedor.Cve_Estado_Fisc = int.Parse(RAD_FIS_cmbEstados.SelectedValue);
                    proveedor.Nombre_Responsable = RAD_txtNomRES.Text;
                    proveedor.Apellido_Paterno_Resp = RAD_txtAPRES.Text;
                    proveedor.Apellido_Materno_Resp = RAD_txtAMRES.Text;
                    proveedor.Dx_Email_Repre = RAD_ktxtCorreoRES.Text;
                    proveedor.Dx_Telefono_Repre = RAD_NtxtTelefonoRES.Text;

                    proveedor.Nombre_Rep_Legal = RAD_txtNomRL.Text;
                    proveedor.Apellido_Paterno_Rep_Legal = RAD_txtAPRL.Text;
                    proveedor.Apellido_Materno_Rep_Legal= RAD_txtAMRL.Text;

                    proveedor.Pct_Tasa_IVA = double.Parse(IVA.SelectedValue);
                    proveedor.Cve_Zona = int.Parse(rad_CMB_ZONA.SelectedValue);

                    proveedor.Nombre = RAD_txtNombre.Text;
                    proveedor.Apellido_Paterno = RAD_txtApePat.Text;
                    proveedor.Apellido_Materno = RAD_txtApeMat.Text;


                    /////////////////////
                    //sucursales FISICAS
                    ///////////////////// 

                    List<CAT_PROVEEDORBRANCH> nuevoObjeto = LogicaNegocios.ModuloCentral.AltaProveedor.listaActualizar(proveedor.Id_Proveedor);

                    if (nuevoObjeto == null)
                    {
                        return;
                    }
                    for (int i = 0; i < nuevoObjeto.Count; i++)
                    {

                        var datosanteriores = LogicaNegocios.ModuloCentral.AltaProveedor.objetoViejo(nuevoObjeto[i].Id_Branch);
                        nuevoObjeto[i].Dx_Razon_Social = RAD_txtNombre.Text;
                        nuevoObjeto[i].Dx_Domicilio_Fiscal_Calle = RAD_FIS_calle.Text;
                        nuevoObjeto[i].Dx_Domicilio_Fiscal_Num = RAD_FIS_NUMERO.Text;

                        nuevoObjeto[i].Dx_Domicilio_Fiscal_CP = RAD_FIS_nCodPos.Text;
                        nuevoObjeto[i].Cve_Estado_Fisc = int.Parse(RAD_FIS_cmbEstados.SelectedValue);
                        nuevoObjeto[i].Cve_Deleg_Municipio_Fisc = int.Parse(RAD_FIS_cmbEstados.SelectedValue);
                        nuevoObjeto[i].Dx_RFC = RAD_txtRFC.Text;
                        nuevoObjeto[i].Pct_Tasa_IVA = double.Parse(IVA.SelectedValue);

                        nuevoObjeto[i].Nombre = RAD_txtNombre.Text;
                        nuevoObjeto[i].Apellido_Paterno = RAD_txtApePat.Text;
                        nuevoObjeto[i].Apellido_Materno = RAD_txtApeMat.Text;

                        var sucFis = new LogicaNegocios.ModuloCentral.AltaProveedor().ActalizaSucursalFisica(nuevoObjeto[i]);
                        if (sucFis)
                        {
                            var cambioDatos = Insertlog.GetCambiosDatos(datosanteriores, nuevoObjeto[i]);
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "CAMBIOS", nuevoObjeto[i].Id_Branch.ToString(CultureInfo.InvariantCulture), txt_Motivos_Editar.Text,//txtMotivos.Text,
                                "", cambioDatos[0], cambioDatos[1]);
                        }
                    }

                    /////////////////////
                    //sucursales VIRTUALES|
                    /////////////////////


                    List<CAT_PROVEEDORBRANCH> nuevoObjetoV = LogicaNegocios.ModuloCentral.AltaProveedor.listaActualizarVirtual(proveedor.Id_Proveedor);

                    if (nuevoObjetoV == null)
                    {
                        return;
                    }
                    for (int i = 0; i < nuevoObjetoV.Count; i++)
                    {
                        var datosanteriores = LogicaNegocios.ModuloCentral.AltaProveedor.objetoViejo(nuevoObjetoV[i].Id_Branch);

                        nuevoObjetoV[i].Dx_Razon_Social = RAD_txtNombre.Text;
                        nuevoObjetoV[i].Dx_Domicilio_Fiscal_Calle = RAD_FIS_calle.Text;
                        nuevoObjetoV[i].Dx_Domicilio_Fiscal_Num = RAD_FIS_NUMERO.Text;

                        nuevoObjetoV[i].Dx_Domicilio_Fiscal_CP = RAD_FIS_nCodPos.Text;
                        nuevoObjetoV[i].Cve_Estado_Fisc = int.Parse(RAD_FIS_cmbEstados.SelectedValue);
                        nuevoObjetoV[i].Cve_Deleg_Municipio_Fisc = int.Parse(RAD_FIS_cmbEstados.SelectedValue);

                        nuevoObjetoV[i].Dx_RFC = RAD_txtRFC.Text;
                        nuevoObjetoV[i].Pct_Tasa_IVA = double.Parse(IVA.SelectedValue);

                        nuevoObjetoV[i].Nombre = RAD_txtNombre.Text;
                        nuevoObjetoV[i].Apellido_Paterno = RAD_txtApePat.Text;
                        nuevoObjetoV[i].Apellido_Materno = RAD_txtApeMat.Text;


                        var sucVirtual = new LogicaNegocios.ModuloCentral.AltaProveedor().actualizarSucursalVirtual(nuevoObjetoV[i]);
                        if (sucVirtual)
                        {
                            var cambioDatos = Insertlog.GetCambiosDatos(datosanteriores, nuevoObjetoV[i]);
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "CAMBIOS", nuevoObjetoV[i].Id_Branch.ToString(CultureInfo.InvariantCulture), txt_Motivos_Editar.Text,//txtMotivos.Text,
                                "", cambioDatos[0], cambioDatos[1]);
                        }
                    }

                    /////////////////////
                    /////////////////////
                    /////////////////////

                    var res = new LogicaNegocios.ModuloCentral.AltaProveedor().actualizaMatriz(proveedor);

                    if (res != null)
                    {
                        var cambioDatos = Insertlog.GetCambiosDatos(datosAnteriores, proveedor);
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "EMPRESAS", "CAMBIOS", proveedor.Id_Proveedor.ToString(CultureInfo.InvariantCulture), txt_Motivos_Editar.Text,//txtMotivos.Text,
                            "", cambioDatos[0], cambioDatos[1]);
                        RWM_vent.RadAlert("INFORMACIÓN ACTUALIZADA CORRECTAMENTE", 300, 100, "ATENCION: ", null);

                    }

                }
                catch (Exception ex)
                {
                    RWM_vent.RadAlert("OCURRIO UN ERROR AL ACTUALIZAR: \n" + ex.Message, 300, 100, "ATENCION: ", null);
                }   
            }
            else
            {
                RWM_vent.RadAlert("Revisa que los campos tengan el formato correcto y que no esten vacios.", 300, 100, "Atencion: ", null);
            }
            //matriz
            
        }        
        
        #region metodos
        public string generaRFC(string type, string name, string last, string mother, string birth, string rfc)
        {
            string flag = "";

            if (type.ToUpper().Equals("SELECTOR"))
            {
                type = (CompanyType)Enum.Parse(typeof(CompanyType), rad_cmbTipoPersona.SelectedItem.ToString()) == CompanyType.MORAL
                    ? "PERSONA MORAL"
                    : "PERSONA FISICA";
            }

            switch (type.ToUpper())
            {
                case "PERSONA MORAL":
                    var str1 = K_CREDITODal.ClassInstance.GenerateRFCMoral(name, birth, rfc);
                    flag = str1;
                    break;
                case "PERSONA FISICA":
                    var str2 = K_CREDITODal.ClassInstance.GenerateRFCInmoral(name, last, mother, birth);

                    flag = str2;
                    break;
                case "AVAL":
                    var str3 = K_CREDITODal.ClassInstance.GenerateRFCInmoral(name, last, mother, birth);
                    flag = str3.Substring(0, 10);
                    break;
            }
            if ((name.Contains("Ñ") || last.Contains("Ñ") || mother.Contains("Ñ")))
                flag = generaRFC(type, name.Replace("Ñ", "&"), last.Replace("Ñ", "&"), mother.Replace("Ñ", "&"), birth, rfc);
            return flag;
        }

        public bool ValidateRfc(string type, string name, string last, string mother, string birth, string rfc)
        {
            var flag = false;

            if (type.ToUpper().Equals("SELECTOR"))
            {
                type = (CompanyType)Enum.Parse(typeof(CompanyType), rad_cmbTipoPersona.SelectedItem.ToString()) == CompanyType.MORAL
                    ? "Moral"
                    : "Fisica";
            }

            switch (type.ToUpper())
            {
                case "MORAL":
                    var str1 = K_CREDITODal.ClassInstance.GenerateRFCMoral(name, birth, rfc);
                    flag = rfc.Equals(str1);
                    break;
                case "FISICA":
                    var str2 = K_CREDITODal.ClassInstance.GenerateRFCInmoral(name, last, mother, birth);

                    flag = rfc.Equals(str2);
                    break;
                case "AVAL":
                    var str3 = K_CREDITODal.ClassInstance.GenerateRFCInmoral(name, last, mother, birth);
                    flag = rfc.StartsWith(str3.Substring(0, 10));
                    break;
            }
            if (!flag && (name.Contains("Ñ") || last.Contains("Ñ") || mother.Contains("Ñ")))
                flag = ValidateRfc(type, name.Replace("Ñ", "&"), last.Replace("Ñ", "&"), mother.Replace("Ñ", "&"), birth, rfc);
            return flag;
        }

        public void limpiaCampos()
        {
            RAD_txtNombre.Text = "";
            RAD_txtApeMat.Text = "";
            RAD_txtApePat.Text = "";
            RAD_DTP.Clear();
            RAD_txtRFC.Text = "";

        }

        private bool edad()
        {
            bool paso = true;
            if (RAD_DTP.SelectedDate.HasValue)
            {
                int an = RAD_DTP.SelectedDate.Value.Year;
                int to = DateTime.Today.Year;
                int dif = to - an;
                if (dif >= 18)
                {

                }
                else
                {
                    RWM_vent.RadAlert("No se cuenta con la edad minima", 300, 150, "Datos Generales", null);
                    RAD_DTP.Clear();
                    paso = false;
                }
            }
            return paso;
        }

        #endregion

        #region void
        private void activar()
        {
            RAD_FIS_calle.Enabled = false;
            RAD_FIS_cmbEstados.Enabled = false;
            RAD_FIS_nCodPos.Enabled = false;
            RAD_FIS_Colonia.Enabled = false;
            RAD_FIS_DelegOMuni.Enabled = false;
            RAD_FIS_NUMERO.Enabled = false;
        }

        private void tipoPersona()
        {
            RAD_txtAPRES.Visible = true;
            RAD_txtAMRES.Visible = true;
            RAD_txtAMRL.Visible = true;
            RAD_txtAPRL.Visible = true;

            lbl_apeMat_RL.Visible = true;
            lbl_apePat_RL.Visible = true;
            lbl_apePat_RES.Visible = true;
            lbl_apeMat_RES.Visible = true;



            if (rad_cmbTipoPersona.SelectedIndex == 1)
            {
                lbl_RAD_ApeMat.Visible = false;
                RAD_txtApeMat.Visible = false;
                lbl_RAD_ApePAT.Text = "NOMBRE COMERCIAL";
                lbl_RAD_Nombre_Razon.Text = "RAZON SOCIAL";

                lbl_RAD_Fecha.Text = "FECHA DE REGISTRO";
                RAD_txtNombre.Text = "";
                RAD_txtNombre.Width = Unit.Percentage(100);
                RAD_txtApePat.Width = Unit.Percentage(100);
                RAD_txtApeMat.Text = "";
                RAD_txtApePat.Text = "";
                RAD_txtRFC.Text = "";

                limpiaCampos();
            }
            else
            {
                lbl_RAD_ApeMat.Visible = true;
                RAD_txtApeMat.Visible = true;
                RAD_txtNombre.Width = Unit.Percentage(75);
                RAD_txtApePat.Width = Unit.Percentage(75);
                lbl_RAD_ApePAT.Text = "APELLIDO PATERNO";
                lbl_RAD_Fecha.Text = "FECHA DE NACIMIENTO";
                lbl_RAD_Nombre_Razon.Text = "NOMBRE(S): ";
                limpiaCampos();

            }
        }

        

        private void obtieneID()
        {
            int id;
            try
            {
                con_id_pro = int.Parse(rad_cmbTipoPersona.SelectedValue);
            }
            catch (Exception)
            {
                return;
            }
            var proveedor = LogicaNegocios.ModuloCentral.AltaProveedor.obtieneProvID(con_id_pro);
            if (proveedor == null)
            {
                return;
            }
            else
            {
                lbl_RAD_ApeMat.Visible = false;
                RAD_txtApeMat.Visible = false;

                lbl_RAD_Nombre_Razon.Text = "RAZON SOCIAL";
                lbl_RAD_ApePAT.Text = "NOMBRE COMERCIAL";
                lbl_RAD_Fecha.Text = "FECHA DE REGISTRO";

                RAD_txtNombre.Width = Unit.Percentage(100);
                RAD_txtApePat.Width = Unit.Percentage(100);

                //carga
                RAD_txtNombre.Text = proveedor.Dx_Razon_Social;
                RAD_txtApePat.Text = proveedor.Dx_Nombre_Comercial;
                RAD_txtRFC.Text = proveedor.Dx_RFC;
                RAD_DTP.SelectedDate = proveedor.Dt_Fecha_Proveedor;

                RAD_txtNCodPos.Text = proveedor.Dx_Domicilio_Part_CP;

                cargaXcodPostal(RAD_txtNCodPos.Text);

                RAD_txtCalle.Text = proveedor.Dx_Domicilio_Part_Calle;
                RAD_txtNumero.Text = proveedor.Dx_Domicilio_Part_Num;

                RAD_FIS_calle.Text = proveedor.Dx_Domicilio_Fiscal_Calle;
                RAD_FIS_nCodPos.Text = proveedor.Dx_Domicilio_Fiscal_CP;
                RAD_FIS_NUMERO.Text = proveedor.Dx_Domicilio_Fiscal_Num;
                RAD_FIS_calle.Enabled = false;
                RAD_FIS_nCodPos.Enabled = false;
                RAD_FIS_NUMERO.Enabled = false;
                RAD_FIS_cmbEstados.Enabled = false;
                RAD_FIS_DelegOMuni.Enabled = false;
                RAD_FIS_Colonia.Enabled = false;
                rad_CMB_ZONA.Enabled = false;
                IVA.Enabled = false;


                cargaXcodPostalFIS();


                //RAD_txtAPRES.Text = "";
                RAD_txtAPRES.Visible = false;
                RAD_txtAMRES.Visible = false;
                RAD_txtAMRL.Visible = false;
                RAD_txtAPRL.Visible = false;

                lbl_apeMat_RL.Visible = false;
                lbl_apePat_RL.Visible = false;

                lbl_apePat_RES.Visible = false;
                lbl_apeMat_RES.Visible = false;

                //RAD_txtNomRES.Width = Unit.Percentage(100);
                RAD_txtNomRES.Text = proveedor.Nombre_Responsable;
                RAD_txtAPRES.Text = proveedor.Apellido_Paterno_Resp;
                RAD_txtAMRES.Text = proveedor.Apellido_Materno_Resp;

                //RAD_txtNomRL.Width = Unit.Percentage(100);
                RAD_txtNomRL.Text = proveedor.Nombre_Rep_Legal;

                RAD_txtAPRL.Text = proveedor.Apellido_Paterno_Rep_Legal;
                RAD_txtAMRL.Text = proveedor.Apellido_Materno_Rep_Legal;

                RAD_ktxtCorreoRES.Text = proveedor.Dx_Email_Repre;
                RAD_NtxtTelefonoRES.Text = proveedor.Dx_Telefono_Repre;
              

                RAD_txtNombreBanco.Text = proveedor.Dx_Nombre_Banco;
                RAD_txtNumClabe.Text = proveedor.Dx_Cuenta_Banco;
                IVA.SelectedValue = proveedor.Pct_Tasa_IVA.Value.ToString();

                rad_CMB_ZONA.SelectedValue = proveedor.Cve_Zona.Value.ToString();
                if (proveedor.Acta_Constitutiva != null)
                {
                    ActaConst_TEMP = proveedor.Acta_Constitutiva;
                    ver_ACTA_TEMP.ImageUrl = "~/CentralModule/images/visualizar.png";
                    ver_ACTA_TEMP.Visible = true;
                }

                if (proveedor.Poder_Notarial != null)
                {
                    podNotarial_TEMP = proveedor.Poder_Notarial;
                    ver_POD_TEMP.ImageUrl = "~/CentralModule/images/visualizar.png";
                    ver_POD_TEMP.Visible = true;
                }
               



                if (proveedor.Fg_Mismo_Domicilio == null)
                {
                    RAD_cmbSiNoDomicilio.SelectedIndex = 0;
                }
                else if (proveedor.Fg_Mismo_Domicilio == true)
                {
                    RAD_cmbSiNoDomicilio.SelectedIndex = 1;
                }
                else
                {
                    RAD_cmbSiNoDomicilio.SelectedIndex = 2;
                }

                carga_cmbZona_Cunsulta(proveedor.Cve_Region.Value);

                rad_CMB_ZONA.SelectedValue = proveedor.Cve_Zona.Value.ToString();


            }
        }

        private void carga_cmbZona()
        {
            rad_CMB_ZONA.Items.Clear();
            //
            var userModel = Session["UserInfo"] as US_USUARIOModel;

            if (userModel == null)
            {
                return;
            }
            else
            {
                var zonas = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catZonaxidRegion(userModel.Id_Departamento);
                if (zonas == null) { return; }
                else
                {
                    rad_CMB_ZONA.DataSource = zonas;
                    rad_CMB_ZONA.DataTextField = "Dx_Nombre_Zona";
                    rad_CMB_ZONA.DataValueField = "Cve_Zona";
                    rad_CMB_ZONA.DataBind();

                    rad_CMB_ZONA.Items.Insert(0, new RadComboBoxItem("Seleccione.."));
                    rad_CMB_ZONA.SelectedIndex = 0;
                }
            }
        }
       
        private void carga_cmbZona_Cunsulta(int id)
        {
            rad_CMB_ZONA.Items.Clear();
            //

            var zonas = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catZonaxidRegion(id);
            if (zonas == null) { return; }
            else
            {
                rad_CMB_ZONA.DataSource = zonas;
                rad_CMB_ZONA.DataTextField = "Dx_Nombre_Zona";
                rad_CMB_ZONA.DataValueField = "Cve_Zona";
                rad_CMB_ZONA.DataBind();

            }
        }

        private void cmb_TipoPersonaMAT()
        {
            rad_cmbTipoPersona.Items.Clear();
            var tipoPersona = LogicaNegocios.ModuloCentral.AltaProveedor.tipoPersonas();
            

            //CComboBox tp = new CComboBox(1, "FISICA");
            //CComboBox tp1 = new CComboBox(2, "MORAL");
            //List<CComboBox> lis = new List<CComboBox>();
            //lis.Add(tp);
            //lis.Add(tp1);
            rad_cmbTipoPersona.DataSource = tipoPersona;
            rad_cmbTipoPersona.DataTextField = "Dx_Tipo_Sociedad";
            rad_cmbTipoPersona.DataValueField = "Cve_Tipo_Sociedad";
            rad_cmbTipoPersona.DataBind();
        }

        private void cmb_tipoPersonaSUC()
        {

            var proveedores = LogicaNegocios.ModuloCentral.AltaProveedor.obtieneTodos().OrderBy(c => c.Dx_Razon_Social);
            if (proveedores == null) return;

            rad_cmbTipoPersona.Items.Clear();
            rad_cmbTipoPersona.DataSource = proveedores;
            rad_cmbTipoPersona.DataTextField = "Dx_Razon_Social";
            rad_cmbTipoPersona.DataValueField = "ID_Proveedor";
            rad_cmbTipoPersona.DataBind();
            rad_cmbTipoPersona.Items.Insert(0, new RadComboBoxItem("Selecciona.."));
            rad_cmbTipoPersona.SelectedIndex = 0;
        }

        private void comb()
        {
            CComboBox el = new CComboBox(1, "MATRIZ");
            CComboBox ele = new CComboBox(2, "SUCURSAL");
            List<CComboBox> listaCMB = new List<CComboBox>();
            listaCMB.Add(el);
            listaCMB.Add(ele);
            rad_cmbTipoPro.DataSource = listaCMB;
            rad_cmbTipoPro.DataTextField = "Elemento";
            rad_cmbTipoPro.DataValueField = "IdElemento";
            rad_cmbTipoPro.DataBind();

            //  rad_cmbTipoPersona
            IVA.Items.Insert(0, new RadComboBoxItem("11%", "11"));
            IVA.Items.Insert(1, new RadComboBoxItem("16%", "16"));
            IVA.SelectedIndex = 1;


            //RAD_cmbSiNoDomicilio
            CComboBox SI = new CComboBox(1, "SI");
            CComboBox NO = new CComboBox(2, "NO");
            List<CComboBox> L = new List<CComboBox>();
            L.Add(SI);
            L.Add(NO);
            RAD_cmbSiNoDomicilio.DataSource = L;
            RAD_cmbSiNoDomicilio.DataTextField = "Elemento";
            RAD_cmbSiNoDomicilio.DataValueField = "IdElemento";
            RAD_cmbSiNoDomicilio.DataBind();


            RAD_cmbSiNoDomicilio.Items.Insert(0, new RadComboBoxItem(""));
            RAD_cmbSiNoDomicilio.SelectedIndex = 0;

        }

        private void cargaEstado()
        {
            var estMex = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catEstados();
            if (estMex == null)
            {
            }
            else
            {

                RAD_cmbEstados.DataSource = estMex;
                RAD_cmbEstados.DataTextField = "Dx_Nombre_Estado";
                RAD_cmbEstados.DataValueField = "Cve_Estado";
                RAD_cmbEstados.DataBind();


                RAD_cmbEstados.Items.Insert(0, new RadComboBoxItem(""));
                RAD_cmbEstados.SelectedIndex = 0;
            }
        }

        private void cargaestadosFIS()
        {
            var estFisMex = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catEstados();
            if (estFisMex == null)
            {

            }
            else
            {
                RAD_FIS_cmbEstados.DataSource = estFisMex;
                RAD_FIS_cmbEstados.DataTextField = "Dx_Nombre_Estado";
                RAD_FIS_cmbEstados.DataValueField = "Cve_Estado";
                RAD_FIS_cmbEstados.DataBind();


                RAD_FIS_cmbEstados.Items.Insert(0, new RadComboBoxItem(""));
                RAD_FIS_cmbEstados.SelectedIndex = 0;
            }
        }

        private void cargaDelgOMunicipio()
        {
            var degMunicipio = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catDelegacionOMunicipio(int.Parse(RAD_cmbEstados.SelectedValue));
            if (degMunicipio.Equals(null))
            {
            }
            else
            {
                RAD_cmbDelMun.DataSource = degMunicipio;
                RAD_cmbDelMun.DataTextField = "Dx_Deleg_Municipio";
                RAD_cmbDelMun.DataValueField = "Cve_Deleg_Municipio";
                RAD_cmbDelMun.DataBind();


                RAD_cmbDelMun.Items.Insert(0, new RadComboBoxItem(""));
                RAD_cmbDelMun.SelectedIndex = 0;

            }

        }

        private void cargaMunFIS()
        {
            List<CAT_DELEG_MUNICIPIO> degMunFIS = null;
            try
            {
                degMunFIS = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catDelegacionOMunicipio(int.Parse(RAD_FIS_cmbEstados.SelectedValue));
            }
            catch
            {
                return;
            }
            if (degMunFIS.Equals(null))
            {
            }
            else
            {
                RAD_FIS_DelegOMuni.DataSource = degMunFIS;
                RAD_FIS_DelegOMuni.DataTextField = "Dx_Deleg_Municipio";
                RAD_FIS_DelegOMuni.DataValueField = "Cve_Deleg_Municipio";
                RAD_FIS_DelegOMuni.DataBind();

                RAD_FIS_DelegOMuni.Items.Insert(0, new RadComboBoxItem(""));
                RAD_FIS_DelegOMuni.SelectedIndex = 0;


            }


        }

        private void cargaColonia()
        {
            var lisColonia = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catColonias(int.Parse(RAD_cmbDelMun.SelectedValue));
            if (lisColonia.Equals(null))
            {
            }
            else
            {
                RAD_cmbColonia.DataSource = lisColonia;
                RAD_cmbColonia.DataTextField = "Dx_Colonia";
                RAD_cmbColonia.DataValueField = "Cve_CP";
                RAD_cmbColonia.DataBind();


                RAD_cmbColonia.Items.Insert(0, new RadComboBoxItem(""));
                RAD_cmbColonia.SelectedIndex = 0;

            }

        }

        /// 
        private void cargaColonia(string Cve_CP)
        {



            var lisColonia = LogicaNegocios.ModuloCentral.CatalogosRegionZona.listasCP(Cve_CP);
            if (lisColonia.Equals(null))
            {
            }
            else
            {
                RAD_cmbColonia.DataSource = lisColonia;
                RAD_cmbColonia.DataTextField = "Dx_Colonia";
                RAD_cmbColonia.DataValueField = "Cve_CP";
                RAD_cmbColonia.DataBind();


                RAD_cmbColonia.Items.Insert(0, new RadComboBoxItem(""));
                RAD_cmbColonia.SelectedIndex = 0;

            }

        }
        /// 
        /// </summary>

        private void cargaColoniaFIS()
        {
            List<CAT_CODIGO_POSTAL_SEPOMEX> lisColoniaFIS = null;
            try
            {
                lisColoniaFIS = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catColonias(int.Parse(RAD_FIS_DelegOMuni.SelectedValue));
            }
            catch
            {
                return;
            }
            if (lisColoniaFIS.Equals(null))
            {
            }
            else
            {
                RAD_FIS_Colonia.DataSource = lisColoniaFIS;
                RAD_FIS_Colonia.DataTextField = "Dx_Colonia";
                RAD_FIS_Colonia.DataValueField = "Cve_CP";
                RAD_FIS_Colonia.DataBind();



                RAD_FIS_Colonia.Items.Insert(0, new RadComboBoxItem(""));
                RAD_FIS_Colonia.SelectedIndex = 0;
            }
        }

        private void cargaColoniaFIS(string cP)
        {
            List<CAT_CODIGO_POSTAL_SEPOMEX> lisColoniaFIS = null;
            try
            {
                var lisColonia = LogicaNegocios.ModuloCentral.CatalogosRegionZona.listasCP(cP);
                if (lisColonia.Equals(null))
                {
                }
                else
                {
                    RAD_FIS_Colonia.DataSource = lisColonia;
                    RAD_FIS_Colonia.DataTextField = "Dx_Colonia";
                    RAD_FIS_Colonia.DataValueField = "Cve_CP";
                    RAD_FIS_Colonia.DataBind();


                    RAD_FIS_Colonia.Items.Insert(0, new RadComboBoxItem(""));
                    RAD_FIS_Colonia.SelectedIndex = 0;

                }
            }
            catch
            {
                return;
            }
            
        }
        #endregion

        #region selectedChanged
        protected void RAD_cmbEstados_SelectedIndexChanged1(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            t_Estado = int.Parse(RAD_cmbEstados.SelectedValue);
            cargaDelgOMunicipio();


        }

        protected void habilitar()
        {
            if (RAD_cmbSiNoDomicilio.SelectedIndex == 1 || RAD_cmbSiNoDomicilio.SelectedIndex==0)
            {
                RAD_FIS_calle.Enabled = false;
                RAD_FIS_cmbEstados.Enabled = false;
                RAD_FIS_nCodPos.Enabled = false;
                RAD_FIS_Colonia.Enabled = false;
                RAD_FIS_DelegOMuni.Enabled = false;
                RAD_FIS_NUMERO.Enabled = false;

                cargaestadosFIS();
                RAD_FIS_cmbEstados.SelectedValue = RAD_cmbEstados.SelectedValue;
                cargaMunFIS();
                RAD_FIS_DelegOMuni.SelectedValue = RAD_cmbDelMun.SelectedValue;
                cargaColoniaFIS();
                //RAD_FIS_Colonia.SelectedValue = RAD_cmbColonia.SelectedValue;
                RAD_FIS_nCodPos.Text = RAD_txtNCodPos.Text;
                RAD_FIS_calle.Text = RAD_txtCalle.Text;
                RAD_FIS_NUMERO.Text = RAD_txtNumero.Text;
            }
            else
            {
                RAD_FIS_calle.Enabled = true;
                RAD_FIS_cmbEstados.Enabled = true;
                RAD_FIS_nCodPos.Enabled = true;
                RAD_FIS_Colonia.Enabled = true;
                RAD_FIS_DelegOMuni.Enabled = true;
                RAD_FIS_NUMERO.Enabled = true;
             

            }
        }

        protected void RAD_cmbSiNoDomicilio_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RAD_cmbSiNoDomicilio.SelectedIndex == 1 || RAD_cmbSiNoDomicilio.SelectedIndex == 0)
            {
                RAD_FIS_calle.Enabled = false;
                RAD_FIS_cmbEstados.Enabled = false;
                RAD_FIS_nCodPos.Enabled = false;
                RAD_FIS_Colonia.Enabled = false;
                RAD_FIS_DelegOMuni.Enabled = false;
                RAD_FIS_NUMERO.Enabled = false;

                cargaestadosFIS();
                RAD_FIS_cmbEstados.SelectedValue = RAD_cmbEstados.SelectedValue;
                cargaMunFIS();
                RAD_FIS_DelegOMuni.SelectedValue = RAD_cmbDelMun.SelectedValue;
                cargaColoniaFIS();
                RAD_FIS_Colonia.SelectedValue = RAD_cmbColonia.SelectedValue;
                RAD_FIS_nCodPos.Text = RAD_txtNCodPos.Text;
                RAD_FIS_calle.Text = RAD_txtCalle.Text;
                RAD_FIS_NUMERO.Text = RAD_txtNumero.Text;
            }
            else
            {
                RAD_FIS_calle.Enabled = true;
                RAD_FIS_cmbEstados.Enabled = true;
                RAD_FIS_nCodPos.Enabled = true;
                RAD_FIS_Colonia.Enabled = true;
                RAD_FIS_DelegOMuni.Enabled = true;
                RAD_FIS_NUMERO.Enabled = true;
                RAD_FIS_calle.Text = "";
                RAD_FIS_NUMERO.Text = "";
                RAD_FIS_nCodPos.Text = "";

                RAD_FIS_Colonia.SelectedIndex=0;
                RAD_FIS_DelegOMuni.SelectedIndex = 0;
                RAD_FIS_cmbEstados.SelectedIndex = 0;

                
            }


        }

        protected void RAD_cmbDelMun_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            t_deleg = int.Parse(RAD_cmbDelMun.SelectedValue);
            cargaColonia();
        }

        protected void RAD_FIS_cmbEstados_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cargaMunFIS();
        }

        protected void RAD_FIS_DelegOMuni_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cargaColoniaFIS();
        }
        #endregion

        protected void rad_cmbTipoPersona_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (rad_cmbTipoPro.SelectedValue == "1")
            {
                tipoPersona();

                btn_Salir_Sucursal.Visible = false;
                RAD_btnSalir.Visible = true;
                RAD_btnFinalizar.Visible = true;
            }
            else
            {
                limpiaTODO();
                obtieneID();
                try
                {

                    cargaGV_FIS_VIR();
                    btn_Salir_Sucursal.Visible = true;
                    RAD_btnSalir.Visible = false;
                    RAD_btnFinalizar.Visible = false;


                }
                catch (Exception m)
                {

                    RWM_vent.RadAlert(m.Message, 300, 300, "ERROR", null);
                }

            }
        }

        protected void cargaGV_FIS_VIR()
        {
            var sucursales_Fisicas = new LogicaNegocios.ModuloCentral.AltaProveedor().obtieneSucursales_x_idMatriz_Fisica(int.Parse(rad_cmbTipoPersona.SelectedValue));
            //obtienetodosXmatriz_Fisica obtieneSucursales_x_idMatriz_Fisica
            RG_SUC_FIS.DataSource = sucursales_Fisicas.OrderBy(l => l.Id_Branch);
            RG_SUC_FIS.DataBind();

            List<datosSucursalVirtual> sucursales_Virtuales = new List<datosSucursalVirtual>();
            foreach (var item in sucursales_Fisicas)
            {
                sucursales_Virtuales.AddRange(new LogicaNegocios.ModuloCentral.AltaProveedor().listaobtieneSucursales_x_idMatriz_Virtual_lista2(item.Id_Branch, (int)item.Cve_Estado_Part, (int)item.Cve_Deleg_Municipio_Part, item.Dx_Domicilio_Part_CP, item.Dx_Nombre_Comercial));
            }
            //var sucursales_Virtuales =new LogicaNegocios.ModuloCentral.AltaProveedor().listaobtieneSucursales_x_idMatriz_Virtual_lista(proveedor.Id_Proveedor);
            RG_SUC_VIRTUALES.DataSource = sucursales_Virtuales;
            RG_SUC_VIRTUALES.DataBind();

            //var sucursales_Virtuales = new LogicaNegocios.ModuloCentral.AltaProveedor().listaobtieneSucursales_x_idMatriz_Virtual_lista(int.Parse(rad_cmbTipoPersona.SelectedValue));
            //RG_SUC_VIRTUALES.DataSource = sucursales_Virtuales.OrderBy(l => l.Id_Branch);
            //RG_SUC_VIRTUALES.DataBind();
        }

        #region guarda
        protected void cargaXcodPostal(string codPos)
        {
            string cod = codPos;

            if (cod == null)
            {
                return;
            }
            else
            {

                var entidad = LogicaNegocios.ModuloCentral.CatalogosRegionZona.CODPOSTAL(cod);
                if (entidad == null)
                {
                    RWM_vent.RadAlert("EL CÓDIGO POSTAL DEL DOMICILIO DEL NEGOCIO NO ES VALIDO", 300, 150, "DATOS GENERALES", null);
                }
                else
                {
                    RAD_cmbEstados.SelectedValue = entidad.Cve_Estado.ToString();
                    t_Estado = entidad.Cve_Estado;
                    RAD_cmbEstados.Enabled = false;

                    cargaDelgOMunicipio();

                    RAD_cmbDelMun.SelectedValue = entidad.Cve_Deleg_Municipio.ToString();
                    t_deleg = int.Parse(RAD_cmbDelMun.SelectedValue);
                    RAD_cmbDelMun.Enabled = false;

                    cargaColonia(entidad.Codigo_Postal);
                    
                    //RAD_cmbColonia.SelectedValue = entidad.Cve_CP.ToString();
                    t_colonia = entidad.Cve_CP;
                }
            }
        }

        protected void cargaXcodPostalFIS()
        {
            string cod = RAD_FIS_nCodPos.Text;

            if (cod == (null))
            {


            }
            else
            {


                var entidad = LogicaNegocios.ModuloCentral.CatalogosRegionZona.CODPOSTAL(cod);
                if (entidad == (null))
                {
                    RWM_vent.RadAlert("EL CÓDIGO POSTAL DEL DOMICILIO FISCAL NO ES VALIDO", 300, 150, "DATOS GENERALES", null);

                }
                else
                {
                    RAD_FIS_cmbEstados.SelectedValue = entidad.Cve_Estado.ToString();
                    RAD_FIS_cmbEstados.Enabled = false;
                    cargaMunFIS();
                    
                    RAD_FIS_DelegOMuni.SelectedValue = entidad.Cve_Deleg_Municipio.ToString();
                    RAD_FIS_DelegOMuni.Enabled = false;
                    cargaColoniaFIS(entidad.Codigo_Postal);
                    //RAD_FIS_Colonia.SelectedValue = entidad.Cve_CP.ToString();
                }
            }
        }

        protected void RAD_txtCodPos_TextChanged(object sender, EventArgs e)
        {
            if (RAD_txtNCodPos.Text == "")
            {
                RAD_cmbEstados.Enabled = true;
                RAD_cmbDelMun.Enabled = true;
                RAD_cmbDelMun.SelectedIndex = 0;
                RAD_cmbEstados.SelectedIndex = 0;
                cargaXcodPostal(this.RAD_txtNCodPos.Text);
            }
            else
            {
                cargaXcodPostal(this.RAD_txtNCodPos.Text);
            }
        }

        protected void RAD_cmbColonia_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                t_colonia = int.Parse(RAD_cmbColonia.SelectedValue);
                var objeto = LogicaNegocios.ModuloCentral.CatalogosRegionZona.obtieneCodpostal(int.Parse(RAD_cmbColonia.SelectedValue));
                if (objeto.Equals(null))
                {
                    return;
                }
                else
                {
                    RAD_txtNCodPos.Text = objeto.Codigo_Postal;
                }
            }
            catch (Exception)
            {
                return;

            }
        }

        protected void RAD_FIS_CodPos_TextChanged(object sender, EventArgs e)
        {
            if (RAD_FIS_nCodPos.Text == "")
            {
                RAD_FIS_cmbEstados.Enabled = true;
                RAD_FIS_DelegOMuni.Enabled = true;

                RAD_FIS_cmbEstados.SelectedIndex = 0;
                RAD_FIS_DelegOMuni.SelectedIndex = 0;
                cargaXcodPostalFIS();
            }
            else
            {
                cargaXcodPostalFIS();
            }
        }

        protected void RAD_FIS_Colonia_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                var objeto = LogicaNegocios.ModuloCentral.CatalogosRegionZona.obtieneCodpostal(int.Parse(RAD_FIS_Colonia.SelectedValue));
                if (objeto.Equals(null))
                {
                    return;
                }
                else
                {
                    RAD_FIS_nCodPos.Text = objeto.Codigo_Postal;
                }
            }
            catch (Exception)
            {
                return;
            }

        }

        protected void RAD_DTP_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (rad_cmbTipoPersona.SelectedIndex == 0)
            {
                bool paso = edad();
                if (paso == true)
                {

                    if (RAD_txtNombre.Text != null && RAD_txtApeMat.Text != null && RAD_txtApePat.Text != null && RAD_DTP.SelectedDate != null && RAD_txtRFC.Text != null)
                    {
                        string FECHA = RAD_DTP.SelectedDate.Value.Year.ToString() + "-" + RAD_DTP.SelectedDate.Value.Month.ToString() + "-" + RAD_DTP.SelectedDate.Value.Day.ToString();
                        var RFC = generaRFC(rad_cmbTipoPersona.SelectedItem.Text, RAD_txtNombre.Text, RAD_txtApePat.Text, RAD_txtApeMat.Text, FECHA, RAD_txtRFC.Text);
                        RAD_txtRFC.Text = RFC;

                        var existe = LogicaNegocios.ModuloCentral.AltaProveedor.buscarProveedorRFC(RFC);
                        if (existe == null)
                        {
                            return;
                        }
                        else
                        {
                            RWM_vent.RadAlert("Ya existe una matriz con el mismo RFC, favor de seleccionar la opción  sucursal y revisar el listado de matrices para seleccionar una", 300, 150, "Datos Generales", null);
                            limpiaCampos();
                        }


                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                if (RAD_txtNombre.Text != null && RAD_txtApeMat.Text != null && RAD_txtApePat.Text != null && RAD_DTP.SelectedDate != null && RAD_txtRFC.Text != null)
                {
                    string FECHA = RAD_DTP.SelectedDate.Value.Year.ToString() + "-" + RAD_DTP.SelectedDate.Value.Month.ToString() + "-" + RAD_DTP.SelectedDate.Value.Day.ToString();
                    var RFC = generaRFC(rad_cmbTipoPersona.SelectedItem.Text, RAD_txtNombre.Text, RAD_txtApePat.Text, RAD_txtApeMat.Text, FECHA, RAD_txtRFC.Text);
                    RAD_txtRFC.Text = RFC;

                    var existe = LogicaNegocios.ModuloCentral.AltaProveedor.buscarProveedorRFC(RFC);
                    if (existe == null)
                    {
                        return;
                    }
                    else
                    {
                        RWM_vent.RadAlert("Ya existe una matriz con el mismo RFC, favor de seleccionar la opción  sucursal y revisar el listado de matrices para seleccionar una", 300, 150, "Datos Generales", null);
                        limpiaCampos();
                    }

                }
            }
        }

        protected void RAD_txtRFC_TextChanged(object sender, EventArgs e)
        {
            if (rad_cmbTipoPersona.SelectedIndex == 0)
            {
                var existe = LogicaNegocios.ModuloCentral.AltaProveedor.buscarProveedorRFC(RAD_txtRFC.Text);
                if (existe == null)
                {
                    if (RAD_txtNombre.Text != null && RAD_txtApeMat.Text != null && RAD_txtApePat.Text != null && RAD_DTP.SelectedDate != null && RAD_txtRFC.Text != null)
                    {
                        string FECHA = RAD_DTP.SelectedDate.Value.Year.ToString() + "-" + RAD_DTP.SelectedDate.Value.Month.ToString() + "-" + RAD_DTP.SelectedDate.Value.Day.ToString();

                        var rfch = ValidateRfc(rad_cmbTipoPersona.SelectedItem.Text, RAD_txtNombre.Text, RAD_txtApePat.Text, RAD_txtApeMat.Text, FECHA, RAD_txtRFC.Text);
                        if (rfch)
                        {
                            return;
                        }
                        else
                        {
                            RWM_vent.RadAlert("RFC no valido", 300, 150, "Datos Generales", null);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    RWM_vent.RadAlert("Ya existe una matriz con el mismo RFC, favor de seleccionar la opción  sucursal y revisar el listado de matrices para seleccionar una", 300, 150, "Datos Generales", null);
                    limpiaTODO();
                }

            }


        }




        protected void RAD_ktxtCorreoRES_TextChanged(object sender, EventArgs e)
        {

        }

        System.IO.Stream nombre;
        string nam;
        byte[] PoderNot;

        string nam1;
        byte[] ActaConst;

        private byte[] documentos
        {
            get
            {
                return Session["pdf1"] as byte[];
            }
            set { Session["pdf1"] = value; }
        }

        private byte[] poderNotarial
        {
            get
            {
                return Session["pdf_notarial"] as byte[];
            }
            set { Session["pdf_notarial"] = value; }
        }

        private byte[] docs
        {
            get
            {
                return Session["pdf2"] as byte[];
            }
            set { Session["pdf2"] = value; }
        }

        //temporales
        private byte[] podNotarial_TEMP
        {
            get
            {
                return Session["pdf_Pod_Not_Temp"] as byte[];
            }
            set { Session["pdf_Pod_Not_Temp"] = value; }
        }

        private byte[] ActaConst_TEMP
        {
            get
            {
                return Session["pdf_ActaConst_Temp"] as byte[];

            }
            set { Session["pdf_ActaConst_Temp"] = value; }
        }

        ///
        private List<TemporalPDFs> listPDFTemporal
        {
            get
            {
                return Session["Listapdf"] == null
                           ? new List<TemporalPDFs>()
                           : Session["Listapdf"] as List<TemporalPDFs>;
            }
            set { Session["Listapdf"] = value; }
        }
        
        /// <summary>
        /// 

        protected void RAD_UPPoderNot_FileUploaded(object sender, FileUploadedEventArgs e)
        {

            nam = e.File.FileName;
            PoderNot = new byte[e.File.ContentLength];
            e.File.InputStream.Read(PoderNot, 0, e.File.ContentLength);

            documentos = PoderNot;

            ok1.ImageUrl = "~/CentralModule/images/icono_correcto.png";
            ok1.Visible = true;

            verPN.ImageUrl = "~/CentralModule/images/visualizar.png";
            verPN.Visible = true;
            ClearContents(sender as Control);
        }

        private void ClearContents(Control control)
        {
            for (var i = 0; i < Session.Keys.Count; i++)
            {
                if (Session.Keys[i].Contains(control.ClientID))
                {
                    Session.Remove(Session.Keys[i]);
                    break;
                }
            }
        }

        protected void btn_ver_Click(object sender, ImageClickEventArgs e)
        {

            loadPDF.Value = "1";



            Response.Redirect("~/CentralModule/VisorImagenes.aspx?Id=1");//,top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no");


        }

        protected void verAC_Click(object sender, ImageClickEventArgs e)
        {



        }

        protected void RAD_UPActa_Const_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            nam1 = e.File.FileName;
            ActaConst = new byte[e.File.ContentLength];
            e.File.InputStream.Read(ActaConst, 0, e.File.ContentLength);

            docs = ActaConst;

            ok2.ImageUrl = "~/CentralModule/images/icono_correcto.png";
            ok2.Visible = true;

            verAC.ImageUrl = "~/CentralModule/images/visualizar.png";

            verAC.Visible = true;

            ClearContents(sender as Control);
        }

        protected void btnRefresh2_Click(object sender, EventArgs e)
        {

        }

        protected void RAD_btnSalir_Click(object sender, EventArgs e)
        {

            // RWM_vent.RadConfirm("Esta seguro que desea salir", "confirmCallBackFn", 300, 100, null, "Consulta Crediticia");

        }

        /// <summary> guardar
        /// Guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        int idMod = 0;
        protected void guardarSucursal()

        {
            //int idMod = //Insertlog.GetIdProveedor("DisponsalCenterBranch");
            var objeto = new CAT_PROVEEDORBRANCH();

            objeto.Cve_Estatus_Proveedor = (int)ProviderStatus.PENDIENTE;
            objeto.Id_Proveedor = idMod;
            if (Session["UserInfo"] != null)
            {
                var userModel = Session["UserInfo"] as US_USUARIOModel;
                if (userModel != null)
                {
                    objeto.Cve_Region = userModel.Id_Departamento;
                }
            }

            if (rad_cmbTipoPersona.SelectedIndex == 1)
            {
                objeto.Dx_Razon_Social = RAD_txtNombre.Text.ToUpper();
                objeto.Dx_Nombre_Comercial = RAD_txtApePat.Text.ToUpper();


            }
            else
            {
                objeto.Dx_Razon_Social = RAD_txtNombre.Text.ToUpper() + " " + RAD_txtApePat.Text.ToUpper() + " " + RAD_txtApeMat.Text.ToUpper();
                objeto.Dx_Nombre_Comercial = RAD_txtNombre.Text.ToUpper() +" " + RAD_txtApePat.Text.ToUpper() + " " + RAD_txtApeMat.Text.ToUpper();
            }
            objeto.Dx_RFC = RAD_txtRFC.Text.ToUpper();
            objeto.Dx_Domicilio_Part_Calle = RAD_txtCalle.Text.ToUpper();
            objeto.Dx_Domicilio_Part_Num = RAD_txtNumero.Text;
            objeto.Dx_Domicilio_Part_CP = RAD_txtNCodPos.Text;
            
            
            objeto.Cve_Deleg_Municipio_Part = int.Parse(RAD_cmbDelMun.SelectedValue);
            objeto.Cve_Estado_Part = int.Parse(RAD_cmbEstados.SelectedValue);
            if (RAD_cmbSiNoDomicilio.SelectedValue == "1")
            {
                objeto.Fg_Mismo_Domicilio = true;
            }
            else
            {
                objeto.Fg_Mismo_Domicilio = false;
            }
            objeto.Dx_Domicilio_Fiscal_Calle = RAD_FIS_calle.Text.ToUpper();
            objeto.Dx_Domicilio_Fiscal_CP = RAD_FIS_nCodPos.Text;
            objeto.Dx_Domicilio_Fiscal_Num = RAD_FIS_NUMERO.Text;
          
            //Colonia FIS//Colonia PART
            objeto.Tipo_Sucursal = "SB_F";
            objeto.Dx_Domicilio_Part_Colonia = RAD_cmbColonia.SelectedItem.Text;
            objeto.Dx_Domicilio_Fiscal_Colonia = RAD_FIS_Colonia.SelectedItem.Text;
            objeto.Cve_CP_Fiscal = int.Parse(RAD_FIS_Colonia.SelectedValue);
             objeto.Fecha_Reg_Nac = RAD_DTP.SelectedDate.Value.Date;
             objeto.Cve_Tipo_Sociedad = Convert.ToByte(rad_cmbTipoPersona.SelectedValue);
             objeto.Cve_CP_Part = int.Parse(RAD_cmbColonia.SelectedValue);

            objeto.Cve_Deleg_Municipio_Fisc = int.Parse(RAD_FIS_DelegOMuni.SelectedValue);
            objeto.Cve_Estado_Fisc = int.Parse(RAD_FIS_cmbEstados.SelectedValue);

            objeto.Nombre_Responsable = RAD_txtNomRES.Text.ToUpper();
            objeto.Apellido_Paterno_Resp = RAD_txtAPRES.Text.ToUpper();
            objeto.Apellido_Materno_Resp = RAD_txtAMRES.Text.ToUpper();
            objeto.Dx_Email_Repre = RAD_ktxtCorreoRES.Text;
            objeto.Dx_Telefono_Repre = RAD_NtxtTelefonoRES.Text;

            objeto.Nombre_Rep_Legal = RAD_txtNomRL.Text.ToUpper();
            objeto.Apellido_Paterno_Rep_Legal = RAD_txtAPRL.Text.ToUpper();
            objeto.Apellido_Materno_Rep_Legal = RAD_txtAMRL.Text.ToUpper();
            objeto.Dx_Nombre_Banco = RAD_txtNombreBanco.Text.ToUpper();
            objeto.Dx_Cuenta_Banco = RAD_txtNumClabe.Text.ToUpper();         

           
            objeto.Codigo_Branch = string.Format("{0:D3}", LsUtility.GetNumberSequence("BRANCHPRO"));


            objeto.Nombre = RAD_txtNombre.Text.ToUpper();
            objeto.Apellido_Paterno = RAD_txtApePat.Text.ToUpper();
            objeto.Apellido_Materno = RAD_txtApeMat.Text.ToUpper();
            objeto.Dt_Fecha_Creacion = DateTime.Now;
            objeto.Acta_Constitutiva = docs;
            objeto.Pct_Tasa_IVA = double.Parse(IVA.SelectedValue);
            //
        

            if (rad_CMB_ZONA.SelectedIndex != 0 && rad_CMB_ZONA.SelectedIndex != -1)
            {
                objeto.Cve_Zona = Convert.ToInt32(rad_CMB_ZONA.SelectedValue);
            }


            //

            var res = LogicaNegocios.ModuloCentral.AltaProveedor.agregarSucursal(objeto);
            if (res != null)
            {
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                    "EMPRESAS", "ALTA", objeto.Id_Branch.ToString(CultureInfo.InvariantCulture),
                    "", "Fecha alta: " + DateTime.Now, "", "Cve_Estatus_Proveedor: " + objeto.Cve_Estatus_Proveedor);

            }

        }

        protected void guardaMatriz()
        {

            var objeto = new CAT_PROVEEDOR();
            objeto.Cve_Estatus_Proveedor = (int)ProviderStatus.PENDIENTE;


            if (Session["UserInfo"] != null)
            {
                var userModel = Session["UserInfo"] as US_USUARIOModel;
                if (userModel != null)
                {
                    objeto.Cve_Region = userModel.Id_Departamento;
                }
            }

            if (rad_cmbTipoPersona.SelectedIndex == 1)
            {
                objeto.Dx_Razon_Social = RAD_txtNombre.Text.ToUpper();
                objeto.Dx_Nombre_Comercial = RAD_txtApePat.Text.ToUpper();

            }
            else
            {
                objeto.Dx_Razon_Social = RAD_txtNombre.Text.ToUpper() + " " + RAD_txtApePat.Text.ToUpper() + " " + RAD_txtApeMat.Text.ToUpper();
                objeto.Dx_Nombre_Comercial = RAD_txtNombre.Text.ToUpper() + " " + RAD_txtApePat.Text.ToUpper() + " " + RAD_txtApeMat.Text.ToUpper(); 
            }
            objeto.Dx_RFC = RAD_txtRFC.Text.ToUpper();
            objeto.Dx_Domicilio_Part_Calle = RAD_txtCalle.Text.ToUpper();
            objeto.Dx_Domicilio_Part_Num = RAD_txtNumero.Text.ToUpper();
            objeto.Dx_Domicilio_Part_CP = RAD_txtNCodPos.Text.ToUpper();
            objeto.Cve_Deleg_Municipio_Part = int.Parse(RAD_cmbDelMun.SelectedValue);
            objeto.Cve_Estado_Part = int.Parse(RAD_cmbEstados.SelectedValue);
            
           
            

            if (RAD_cmbSiNoDomicilio.SelectedValue == "1")
            {
                objeto.Fg_Mismo_Domicilio = true;
            }
            else
            {
                objeto.Fg_Mismo_Domicilio = false;
            }
            objeto.Dx_Domicilio_Fiscal_Calle = RAD_FIS_calle.Text.ToUpper();
            objeto.Dx_Domicilio_Fiscal_CP = RAD_FIS_nCodPos.Text.ToUpper();
            objeto.Dx_Domicilio_Fiscal_Num = RAD_FIS_NUMERO.Text.ToUpper();
            objeto.Dx_Domicilio_Fiscal_Num = RAD_FIS_NUMERO.Text.ToUpper();
            objeto.Dx_Domicilio_Part_CP = RAD_txtNCodPos.Text.ToUpper();
            objeto.Cve_CP_Fiscal = int.Parse(RAD_FIS_Colonia.SelectedValue);
            //Colonia FIS  //colonia PART
            objeto.Dx_Domicilio_Fiscal_Colonia = RAD_FIS_Colonia.SelectedItem.Text;
            objeto.Cve_CP_Part = (int.Parse(RAD_cmbColonia.SelectedValue));
            objeto.Fecha_Reg_Nac = RAD_DTP.SelectedDate.Value.Date;
            objeto.Dx_Domicilio_Part_Colonia = RAD_cmbColonia.SelectedItem.Text;
            objeto.Cve_Tipo_Sociedad = Convert.ToByte(rad_cmbTipoPersona.SelectedValue);

            objeto.Cve_Deleg_Municipio_Fisc = int.Parse(RAD_FIS_DelegOMuni.SelectedValue);
            objeto.Cve_Estado_Fisc = int.Parse(RAD_FIS_cmbEstados.SelectedValue);

            objeto.Nombre_Responsable = RAD_txtNomRES.Text.ToUpper();
            objeto.Apellido_Paterno_Resp = RAD_txtAPRES.Text.ToUpper();
            objeto.Apellido_Materno_Resp = RAD_txtAMRES.Text.ToUpper();
            objeto.Dx_Email_Repre = RAD_ktxtCorreoRES.Text;
            objeto.Dx_Telefono_Repre = RAD_NtxtTelefonoRES.Text;

            objeto.Nombre_Rep_Legal = RAD_txtNomRL.Text.ToUpper();
            objeto.Apellido_Paterno_Rep_Legal = RAD_txtAPRL.Text.ToUpper();
            objeto.Apellido_Materno_Rep_Legal = RAD_txtAMRL.Text.ToUpper();

            objeto.Dx_Nombre_Banco = RAD_txtNombreBanco.Text.ToUpper();
            objeto.Dx_Cuenta_Banco = RAD_txtNumClabe.Text; 
            objeto.Codigo_Proveedor = string.Format("{0:D3}", LsUtility.GetNumberSequence("PROVEEDOR"));
            objeto.Nombre = RAD_txtNombre.Text.ToUpper();
            objeto.Apellido_Paterno = RAD_txtApePat.Text.ToUpper();
            objeto.Apellido_Materno = RAD_txtApeMat.Text.ToUpper();
            objeto.Dt_Fecha_Creacion = DateTime.Now;
            objeto.Acta_Constitutiva = docs;
            objeto.Poder_Notarial = documentos;
            objeto.Pct_Tasa_IVA = double.Parse(IVA.SelectedValue);


            if (rad_CMB_ZONA.SelectedIndex != 0 && rad_CMB_ZONA.SelectedIndex != -1)
            {
                objeto.Cve_Zona = Convert.ToInt32(rad_CMB_ZONA.SelectedValue);
            }

            if (Session["UserInfo"] != null)
            {
                var userModel = Session["UserInfo"] as US_USUARIOModel;
                if (userModel != null) objeto.Cve_Region = userModel.Id_Departamento;
            }


            var res = LogicaNegocios.ModuloCentral.AltaProveedor.agregarMatriz(objeto);
            if (res != null)
            {
                idMod = res.Id_Proveedor;
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                    "EMPRESAS", "ALTA", objeto.Id_Proveedor.ToString(CultureInfo.InvariantCulture),
                    "", "Fecha alta: " + DateTime.Now, "", "Cve_Estatus_Proveedor: " + objeto.Cve_Estatus_Proveedor);

                
            }
        }

        /// <summary>
        /// //////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void RAD_btnFinalizar_Click(object sender, EventArgs e)
        {

            Page.Validate();

            if (Page.IsValid)
            {
                guardaMatriz();
                guardarSucursal();

                RWM_vent.RadAlert("INFORMACIÓN ALMACENADA CORRECTAMENTE", 300, 100, "ATENCION: ", null);

                limpiaTODO();
                rad_CMB_ZONA.SelectedIndex = 0;
            }
            else
            {
                RWM_vent.RadAlert("Revisa que los campos tengan el formato correcto y que no esten vacios.", 300, 100, "Atencion: ", null);
            }
        }

        protected void rad_cmbTipoPro_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RAD_txtAPRES.Visible = true;
            RAD_txtAMRES.Visible = true;
            RAD_txtAMRL.Visible = true;
            RAD_txtAPRL.Visible = true;
            lbl_apeMat_RL.Visible = true;
            lbl_apePat_RL.Visible = true;
            lbl_apePat_RES.Visible = true;
            lbl_apeMat_RES.Visible = true;

            if (rad_cmbTipoPro.SelectedValue == "1")
            {
                RAD_cmbColonia.Enabled = true;
                RAD_cmbDelMun.Enabled = true;
                RAD_cmbEstados.Enabled = true;
                RAD_cmbSiNoDomicilio.Enabled = true;
                RAD_txtCalle.Enabled = true;
                RAD_txtNCodPos.Enabled = true;
                RAD_txtNumero.Enabled = true;
                RAD_txtCalle.Enabled = true;
                RAD_txtNCodPos.Enabled = true;

                RAD_txtAMRES.Enabled = true;
                RAD_txtAPRES.Enabled = true;
                RAD_txtNomRES.Enabled = true;
                RAD_ktxtCorreoRES.Enabled = true;
                RAD_NtxtTelefonoRES.Enabled = true;

                RAD_txtAMRL.Enabled = true;
                RAD_txtAPRL.Enabled = true;
                RAD_txtNomRL.Enabled = true;

                RAD_txtApeMat.Enabled = true;
                RAD_txtApePat.Enabled = true;
                RAD_txtNombre.Enabled = true;
                RAD_txtRFC.Enabled = true;
                RAD_DTP.Enabled = true;

                RAD_txtNombreBanco.Enabled = true;
                RAD_txtNumClabe.Enabled = true;
                RAD_UPActa_Const.Visible = true;
                RAD_UPPoderNot.Visible = true;

                lbl_RAD_ApeMat.Visible = true;
                RAD_txtApeMat.Visible = true;

                lbl_RAD_Nombre_Razon.Text = "NOMBRE(S): ";
                lbl_RAD_ApePAT.Text = "APELLIDO PATERNO";
                lbl_RAD_Fecha.Text = "FECHA DE NACIMIENTO";
                lbl_TipoPersona.Text = "TIPO DE PERSONA: ";

                //rad_cmbTipoPersona.Width = Unit.Percentage(25);
                cmb_TipoPersonaMAT();
                RG_SUC_VIRTUALES.Visible = false;
                RG_SUC_FIS.Visible = false;
                lbl_FIS.Visible = false;
                lbl_VIR.Visible = false;
                limpiaTODO();


            }
            else
            {
                RAD_cmbColonia.Enabled = false;
                RAD_cmbDelMun.Enabled = false;
                RAD_cmbEstados.Enabled = false;
                RAD_cmbSiNoDomicilio.Enabled = false;
                RAD_txtCalle.Enabled = false;
                RAD_txtNCodPos.Enabled = false;
                RAD_txtNumero.Enabled = false;
                RAD_txtCalle.Enabled = false;
                RAD_txtNCodPos.Enabled = false;

                RAD_txtAMRES.Enabled = false;
                RAD_txtAPRES.Enabled = false;
                RAD_txtNomRES.Enabled = false;
                RAD_ktxtCorreoRES.Enabled = false;
                RAD_NtxtTelefonoRES.Enabled = false;

                RAD_txtAMRL.Enabled = false;
                RAD_txtAPRL.Enabled = false;
                RAD_txtNomRL.Enabled = false;

                RAD_txtApeMat.Enabled = false;
                RAD_txtApePat.Enabled = false;
                RAD_txtNombre.Enabled = false;
                RAD_txtRFC.Enabled = false;
                RAD_DTP.Enabled = false;

                RAD_txtNombreBanco.Enabled = false;
                RAD_txtNumClabe.Enabled = false;

                RAD_UPActa_Const.Visible = false;
                RAD_UPPoderNot.Visible = false;

                lbl_TipoPersona.Text = "MATRIZ: ";
                //rad_cmbTipoPersona.Width = Unit.Percentage(50);
                //rad_cmbTipoPro.Width = Unit.Percentage(100);
                cmb_tipoPersonaSUC();
                RG_SUC_VIRTUALES.Visible = true;
                RG_SUC_FIS.Visible = true;
                lbl_FIS.Visible = true;
                lbl_VIR.Visible = true;
                limpiaTODO();

            }

        }

        protected void limpiaTODO()
        {
            RAD_cmbSiNoDomicilio.SelectedIndex = 0;
            RAD_cmbColonia.SelectedIndex = 0;
            RAD_cmbDelMun.SelectedIndex = 0;
            RAD_cmbEstados.SelectedIndex = 0;

            RAD_FIS_Colonia.SelectedIndex = 0;
            RAD_FIS_DelegOMuni.SelectedIndex = 0;
            RAD_FIS_cmbEstados.SelectedIndex = 0;

            RAD_FIS_calle.Text = "";
            RAD_FIS_nCodPos.Text = "";
            RAD_FIS_NUMERO.Text = "";



            RAD_txtCalle.Text = "";
            RAD_txtNCodPos.Text = "";
            RAD_txtNumero.Text = "";



            RAD_txtAMRES.Text = "";
            RAD_txtAPRES.Text = "";
            RAD_txtNomRES.Text = "";
            RAD_ktxtCorreoRES.Text = "";
            RAD_NtxtTelefonoRES.Text = "";

            RAD_txtAMRL.Text = "";
            RAD_txtAPRL.Text = "";
            RAD_txtNomRL.Text = "";



            RAD_txtApeMat.Text = "";
            RAD_txtApePat.Text = "";
            RAD_txtNombre.Text = "";
            RAD_txtRFC.Text = "";
            RAD_DTP.Clear();

            RAD_txtNombreBanco.Text = "";
            RAD_txtNumClabe.Text = "";

            PoderNot = null;
            ActaConst = null;
            documentos = null;
            docs = null;

            ok1.Visible = false;
            verPN.Visible = false;

            ok2.Visible = false;
            verAC.Visible = false;


        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #endregion

        protected List<CAT_ESTADO> GV_FIS_cmbestado()
        {
            var estMex = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catEstados();
            if (estMex == null)
            {
                return null;
            }

            return estMex;

        }

        #region FIS
        protected void RG_SUC_FIS_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {

                var item = e.Item as GridEditableItem;
                var objeto = new CAT_PROVEEDORBRANCH();

                objeto.Cve_Estatus_Proveedor = (int)ProviderStatus.PENDIENTE;
                objeto.Id_Proveedor = int.Parse(rad_cmbTipoPersona.SelectedValue);

                if (Session["UserInfo"] != null)
                {
                    var userModel = Session["UserInfo"] as US_USUARIOModel;
                    if (userModel != null)
                    {
                        objeto.Cve_Region = userModel.Id_Departamento;
                    }
                }


                objeto.Dx_Razon_Social = RAD_txtNombre.Text;
                objeto.Dx_Nombre_Comercial = (item.FindControl("txtNombreComercial_edit") as RadTextBox).Text.ToUpper();
                objeto.Dx_Domicilio_Part_Calle = (item.FindControl("gv_fisica_cmbcalle") as RadTextBox).Text.ToUpper();
                objeto.Dx_Domicilio_Part_Num = (item.FindControl("gv_fisica_txtnumero") as RadTextBox).Text.ToUpper();
                objeto.Dx_Domicilio_Part_CP = (item.FindControl("GV_txt_CodigoPostal_FIS_edit") as RadNumericTextBox).Text;

                objeto.Cve_Estado_Part = int.Parse((item.FindControl("FIS_estado_GV") as RadComboBox).SelectedValue);
                objeto.Cve_Deleg_Municipio_Part = int.Parse((item.FindControl("GV_cmbDelegoMunic") as RadComboBox).SelectedValue);

                //ColoniaPART
                objeto.Dx_Domicilio_Part_Colonia = ((item.FindControl("gv_fisica_cmbcolonia") as RadComboBox).SelectedItem.Text);

                objeto.Dx_RFC = RAD_txtRFC.Text;

                //Domicilio Fiscal
                objeto.Dx_Domicilio_Fiscal_Calle = RAD_FIS_calle.Text;
                objeto.Dx_Domicilio_Fiscal_CP = RAD_FIS_nCodPos.Text;
                objeto.Dx_Domicilio_Fiscal_Num = RAD_FIS_NUMERO.Text;
                objeto.Cve_Deleg_Municipio_Fisc = int.Parse(RAD_FIS_DelegOMuni.SelectedValue);
                objeto.Cve_Estado_Fisc = int.Parse(RAD_FIS_cmbEstados.SelectedValue);
                //ColoniaFIS
                objeto.Dx_Domicilio_Fiscal_Colonia = RAD_FIS_Colonia.SelectedItem.Text;
                ///

                objeto.Nombre_Responsable = (item.FindControl("gv_fisica_txtNombre") as RadTextBox).Text.ToUpper();
                objeto.Apellido_Paterno_Resp = (item.FindControl("gv_fisica_txtAP_res") as RadTextBox).Text.ToUpper();
                objeto.Apellido_Materno_Resp = (item.FindControl("gv_fisica_txtAM_res") as RadTextBox).Text.ToUpper();

                objeto.Dx_Email_Repre = (item.FindControl("gv_fisica_txtcorreo") as RadTextBox).Text;
                objeto.Dx_Telefono_Repre = (item.FindControl("gv_fisica_txttelefono") as RadNumericTextBox).Text.ToUpper();

                objeto.Nombre_Rep_Legal = (item.FindControl("gv_fisica_txtnombreRL") as RadTextBox).Text.ToUpper();
                objeto.Apellido_Paterno_Rep_Legal = (item.FindControl("gv_fisica_txtAP_RL") as RadTextBox).Text.ToUpper();
                objeto.Apellido_Materno_Rep_Legal = (item.FindControl("gv_fisica_txtAM_RL") as RadTextBox).Text.ToUpper();
                
                objeto.Dx_Nombre_Banco = RAD_txtNombreBanco.Text;
                objeto.Dx_Cuenta_Banco = RAD_txtNumClabe.Text;
                objeto.Pct_Tasa_IVA = double.Parse(IVA.SelectedValue);
                objeto.Dt_Fecha_Branch = DateTime.Now;
                objeto.Codigo_Branch = string.Format("{0:D3}", LsUtility.GetNumberSequence("BRANCHPRO"));

                objeto.Nombre = RAD_txtNombre.Text;
                objeto.Apellido_Paterno = RAD_txtApePat.Text;
                objeto.Apellido_Materno = RAD_txtApeMat.Text;

                objeto.Dt_Fecha_Creacion = DateTime.Now;
                objeto.Acta_Constitutiva = poderNotarial;
                objeto.Tipo_Sucursal = "SB_F";

                if (rad_CMB_ZONA.SelectedIndex != 0 && rad_CMB_ZONA.SelectedIndex != -1)
                {
                    objeto.Cve_Zona = Convert.ToInt32(rad_CMB_ZONA.SelectedValue);
                }


                //

                var res = LogicaNegocios.ModuloCentral.AltaProveedor.agregarSucursal(objeto);
                if (res != null)
                {
                    RWM_vent.RadAlert("INFORMACIÓN ALMACENDA CORRECTAMENTE", 300, 100, "SUCURSAL FISICA: ", null);

                }
            }
            catch (Exception x)
            {
                RWM_vent.RadAlert(x.Message, 300, 100, "ATENCION: ", null);

            }


        }
      
        protected void RG_SUC_FIS_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {             

                var sucursales_Fisicas = new LogicaNegocios.ModuloCentral.AltaProveedor().obtieneSucursales_x_idMatriz_Fisica(int.Parse(rad_cmbTipoPersona.SelectedValue));                
                RG_SUC_FIS.DataSource = sucursales_Fisicas.OrderBy(l => l.Id_Branch);
                //((validarRadGrid();

                
                
            }
            catch (Exception)
            {

            }


        }

        protected void RG_SUC_FIS_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
            validarRadGrid();
            //


            if (!e.Item.IsInEditMode)
            {

                return;
            }
            else
            {

                var item = (GridEditableItem)e.Item;

                if ((e.Item is IGridInsertItem))
                {



                    var combo = (RadComboBox)item.FindControl("FIS_estado_GV");
                    var comboZona = (RadComboBox)item.FindControl("gv_fisica_cmb_zona");
                  

                    if (combo != null)
                    {
                       
                        combo.DataSource = GV_FIS_cmbestado();
                        combo.DataTextField = "Dx_Nombre_Estado";
                        combo.DataValueField = "Cve_Estado";
                        combo.DataBind();

                        combo.Items.Insert(0, new RadComboBoxItem(""));
                        combo.SelectedIndex = 0;

                        combo.DataBind();

                    }

                    if (comboZona != null)
                    {
                        var userModel = Session["UserInfo"] as US_USUARIOModel;

                        if (userModel == null)
                        {
                            return;
                        }
                        else
                        {
                            var zonas = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catZonaxidRegion(userModel.Id_Departamento);
                            if (zonas == null) { return; }
                            else
                            {
                                comboZona.DataSource = zonas;
                                comboZona.DataTextField = "Dx_Nombre_Zona";
                                comboZona.DataValueField = "Cve_Zona";
                                comboZona.DataBind();

                                string de = ((Label)item.FindControl("lbl_ESTADO_FIS")).Text;
                                combo.SelectedValue = de;
                            }
                        }

                    }
                    
                }

                if (e.Item is GridEditableItem)
                {
                    try
                    {

                        var label = (Label)item.FindControl("txt_FIS_idBranch");

                        var nombreComercial = (RadTextBox)item.FindControl("txtNombreComercial_edit");
                        var zona = (RadComboBox)item.FindControl("gv_fisica_cmb_zona");

                        var combo = (RadComboBox)item.FindControl("FIS_estado_GV");
                        var comboZona = (RadComboBox)item.FindControl("gv_fisica_cmb_zona");
                        var comboDeleg = (RadComboBox)item.FindControl("GV_cmbDelegoMunic");
                        var comboColonia = (RadComboBox)item.FindControl("gv_fisica_cmbcolonia");


                        if (combo != null)
                        {
                            var eso = (GridColumn)RG_SUC_FIS.MasterTableView.Columns.FindByUniqueName("motivoFIS");
                            var it = (RadTextBox)item.FindControl("txt_Motivos_Editar_FIS");
                            eso.Visible = true;
                            it.Visible = true;
                           
                            combo.DataSource = GV_FIS_cmbestado();
                            combo.DataTextField = "Dx_Nombre_Estado";
                            combo.DataValueField = "Cve_Estado";
                            combo.DataBind();

                            combo.Items.Insert(0, new RadComboBoxItem(""));
                            combo.SelectedIndex = 0;

                            string de = ((Label)item.FindControl("lbl_ESTADO_FIS")).Text;
                            combo.SelectedValue = de;

                            var listaDeleg = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catDelegacionOMunicipio(int.Parse(de));
                            comboDeleg.DataSource = listaDeleg;
                            comboDeleg.DataTextField = "Dx_Deleg_Municipio";
                            comboDeleg.DataValueField = "Cve_Deleg_Municipio";
                            comboDeleg.DataBind();
                            
                            comboDeleg.SelectedValue = ((Label)item.FindControl("lbl_DELEG_FIS")).Text;
                            
                            var listaColo = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catColonias(int.Parse(((Label)item.FindControl("lbl_DELEG_FIS")).Text));


                            comboColonia.DataSource = listaColo;
                            comboColonia.DataTextField = "Dx_Colonia";
                            comboColonia.DataValueField = "Cve_CP";
                            comboColonia.DataBind();
                            
                            string codPostalFis=((RadNumericTextBox)item.FindControl("GV_txt_CodigoPostal_FIS_edit")).Text;
                            var entidad = LogicaNegocios.ModuloCentral.CatalogosRegionZona.CODPOSTAL(codPostalFis);                           

                            comboColonia.SelectedValue = entidad.Cve_CP.ToString();
                            //lbl_Colonia_FIS

                        }
                        if (comboZona != null)
                        {
                            var userModel = Session["UserInfo"] as US_USUARIOModel;

                            if (userModel == null)
                            {
                                return;
                            }
                            else
                            {
                                var zonas = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catZonaxidRegion(userModel.Id_Departamento);
                                if (zonas == null) { return; }
                                else
                                {
                                    comboZona.DataSource = zonas;
                                    comboZona.DataTextField = "Dx_Nombre_Zona";
                                    comboZona.DataValueField = "Cve_Zona";
                                    comboZona.DataBind();

                                    string de = ((Label)item.FindControl("lbl_ESTADO_FIS")).Text;
                                    combo.SelectedValue = de;
                                }
                            }

                        }

                        if (label != null)
                        {

                            string branch = label.Text;
                            var credito = LogicaNegocios.ModuloCentral.AltaProveedor.tieneSolicitud(int.Parse(branch));

                            if (credito != null)
                            {
                                nombreComercial.Enabled = false;
                                zona.Enabled = false;
                            }
                        }
                     
                    }
                    catch (Exception ex)
                    {
                        //RWM_vent.RadAlert(ex.Message, 100, 100, "REG", null);
                    }
                }

            }
        }

        protected void RG_SUC_FIS_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var it = (RadTextBox)item.FindControl("txt_Motivos_Editar_FIS");
            if (it.Text != "")
            {
                try
                {

                    var id = int.Parse((item.FindControl("txt_FIS_idBranch") as Label).Text);

                    var nuevoObjeto = LogicaNegocios.ModuloCentral.AltaProveedor.objetoViejo(id);

                    if (nuevoObjeto == null)
                    {
                        return;
                    }


                    nuevoObjeto.Dx_Nombre_Comercial = (item.FindControl("txtNombreComercial_edit") as RadTextBox).Text.ToUpper();
                    nuevoObjeto.Dx_Domicilio_Part_Calle = (item.FindControl("gv_fisica_cmbcalle") as RadTextBox).Text.ToUpper();
                    nuevoObjeto.Dx_Domicilio_Part_Num = (item.FindControl("gv_fisica_txtnumero") as RadTextBox).Text.ToUpper();

                    nuevoObjeto.Dx_Domicilio_Part_CP = (item.FindControl("GV_txt_CodigoPostal_FIS_edit") as RadNumericTextBox).Text;
                    nuevoObjeto.Cve_Estado_Part = int.Parse((item.FindControl("FIS_estado_GV") as RadComboBox).SelectedValue);
                    nuevoObjeto.Cve_Deleg_Municipio_Part = int.Parse((item.FindControl("GV_cmbDelegoMunic") as RadComboBox).SelectedValue);

                    //coloniaPART
                    nuevoObjeto.Dx_Domicilio_Part_Colonia = ((item.FindControl("gv_fisica_cmbcolonia") as RadComboBox).SelectedItem.Text);


                    nuevoObjeto.Nombre_Responsable = (item.FindControl("gv_fisica_txtNombre") as RadTextBox).Text.ToUpper();
                    nuevoObjeto.Apellido_Paterno_Resp = (item.FindControl("gv_fisica_txtAP_res") as RadTextBox).Text.ToUpper();
                    nuevoObjeto.Apellido_Materno_Resp = (item.FindControl("gv_fisica_txtAM_res") as RadTextBox).Text.ToUpper();

                    nuevoObjeto.Dx_Email_Repre = (item.FindControl("gv_fisica_txtcorreo") as RadTextBox).Text;
                    nuevoObjeto.Dx_Telefono_Repre = (item.FindControl("gv_fisica_txttelefono") as RadNumericTextBox).Text.ToUpper();


                    nuevoObjeto.Nombre_Rep_Legal = (item.FindControl("gv_fisica_txtnombreRL") as RadTextBox).Text.ToUpper();
                    nuevoObjeto.Apellido_Paterno_Rep_Legal = (item.FindControl("gv_fisica_txtAP_RL") as RadTextBox).Text.ToUpper();
                    nuevoObjeto.Apellido_Materno_Rep_Legal = (item.FindControl("gv_fisica_txtAM_RL") as RadTextBox).Text.ToUpper();
                




                    nuevoObjeto.Acta_Constitutiva = poderNotarial;

                    if (rad_CMB_ZONA.SelectedIndex != 0 && rad_CMB_ZONA.SelectedIndex != -1)
                    {
                        nuevoObjeto.Cve_Zona = Convert.ToInt32(rad_CMB_ZONA.SelectedValue);
                    }


                    //

                    var res = new LogicaNegocios.ModuloCentral.AltaProveedor().ActalizaSucursalFisica(nuevoObjeto);

                    if (res != null)
                    {
                        var infoNueva = LogicaNegocios.ModuloCentral.AltaProveedor.objetoViejo(id);
                        RWM_vent.RadAlert("INFORMACIÓN ACTUALIZADA CORRECTAMENTE", 300, 100, "ATENCION: ", null);

                        var cambioDatos = Insertlog.GetCambiosDatos(nuevoObjeto, infoNueva);

                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "EMPRESAS", "CAMBIOS", infoNueva.Id_Branch.ToString(CultureInfo.InvariantCulture), it.Text,//txtMotivos.Text,
                            "", cambioDatos[0], cambioDatos[1]);

                    }
                }
                catch (Exception x)
                {
                    RWM_vent.RadAlert(x.Message, 300, 100, "ATENCION: ", null);

                }


            }
            else
            {
                RWM_vent.RadAlert("DEBE COLOCAR LOS MOTIVOS", 300, 100, "ATENCION: ", null);
                e.Canceled = true;
            }
           

            ///////////////

        }

        #endregion

        #region Virtual


        protected void RG_SUC_VIRTUALES_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                var sucursales_Fisicas = new LogicaNegocios.ModuloCentral.AltaProveedor().obtieneSucursales_x_idMatriz_Fisica(int.Parse(rad_cmbTipoPersona.SelectedValue));
                List<datosSucursalVirtual> sucursales_Virtuales = new List<datosSucursalVirtual>();
                foreach (var item in sucursales_Fisicas)
                {
                    sucursales_Virtuales.AddRange(new LogicaNegocios.ModuloCentral.AltaProveedor().listaobtieneSucursales_x_idMatriz_Virtual_lista2(item.Id_Branch, (int)item.Cve_Estado_Part, (int)item.Cve_Deleg_Municipio_Part, item.Dx_Domicilio_Part_CP, item.Dx_Nombre_Comercial));
                }
                
                RG_SUC_VIRTUALES.DataSource = sucursales_Virtuales;
                RG_SUC_VIRTUALES.DataBind();

            }
            catch (Exception)
            {
                
               
            }
           
        }

        protected void RG_SUC_VIRTUALES_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (!e.Item.IsInEditMode)
            {

                return;
            }
            else
            {

                var item = (GridEditableItem)e.Item;

                if ((e.Item is IGridInsertItem))
                {
                    var regioncombo = (RadComboBox)item.FindControl("GV_VIR_cmbregion");



                    if (regioncombo != null)
                    {
                        var listaRegiones = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catRegion();
                        regioncombo.DataSource = listaRegiones.OrderBy(c => c.Dx_Nombre_Region);
                        regioncombo.DataTextField = "Dx_Nombre_Region";
                        regioncombo.DataValueField = "Cve_Region";
                        regioncombo.DataBind();

                        regioncombo.Items.Insert(0, new RadComboBoxItem(""));
                        regioncombo.SelectedIndex = 0;
                    }

                    var sucursales_Fisicas = LogicaNegocios.ModuloCentral.AltaProveedor.obtieneSucursales_Fisica(int.Parse(rad_cmbTipoPersona.SelectedValue));

                    var comboSucursal = (RadComboBox)item.FindControl("CMB_asociarA");

                    comboSucursal.DataSource = sucursales_Fisicas;
                    comboSucursal.DataTextField = "Dx_Nombre_Comercial";
                    comboSucursal.DataValueField = "Id_Branch";
                    comboSucursal.DataBind();

                    comboSucursal.Items.Insert(0, new RadComboBoxItem("Selecciona.."));
                    comboSucursal.SelectedIndex = 0;
                    return;
                }
                if (e.Item is GridEditableItem)
                {
                    //Cuando Edita
                    try
                    {
                        var label = (Label)item.FindControl("GV_VIR_ID");
                        var region = (RadComboBox)item.FindControl("GV_VIR_cmbregion");
                        var zona = (RadComboBox)item.FindControl("gv_VIR_cmbZona");
                        var NombreComercial = (RadTextBox)item.FindControl("txt_VIR_NombreComercial");
                        var dependencia = (RadComboBox)item.FindControl("CMB_asociarA");

                       var eso = (GridColumn)RG_SUC_VIRTUALES.MasterTableView.Columns.FindByUniqueName("motivoCol");
                        var it = (RadTextBox)item.FindControl("txt_Motivos_Editar_Virtual");                        
                       eso.Visible = true;
                        it.Visible = true;
                         
                        var regioncombo = (RadComboBox)item.FindControl("GV_VIR_cmbregion");



                        if (regioncombo != null)
                        {
                            var listaRegiones = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catRegion();
                            regioncombo.DataSource = listaRegiones.OrderBy(c => c.Dx_Nombre_Region);
                            regioncombo.DataTextField = "Dx_Nombre_Region";
                            regioncombo.DataValueField = "Cve_Region";
                            regioncombo.DataBind();
                            //
                            regioncombo.SelectedValue = ((Label)item.FindControl("lbl_REGION_VIRTUAL")).Text;

                            var listaZona = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catZonaxidRegion(int.Parse(((Label)item.FindControl("lbl_REGION_VIRTUAL")).Text));

                            zona.DataSource = listaZona;
                            zona.DataTextField = "Dx_Nombre_Zona";
                            zona.DataValueField = "Cve_Zona";
                            zona.DataBind();

                            zona.SelectedValue = ((Label)item.FindControl("lbl_ZONA_VIRTUAL")).Text;
                            ///////
                            



                        }

                        var sucursales_Fisicas = LogicaNegocios.ModuloCentral.AltaProveedor.obtieneSucursales_Fisica(int.Parse(rad_cmbTipoPersona.SelectedValue));

                        var comboSucursal = (RadComboBox)item.FindControl("CMB_asociarA");

                        comboSucursal.DataSource = sucursales_Fisicas;
                        comboSucursal.DataTextField = "Dx_Nombre_Comercial";
                        comboSucursal.DataValueField = "Id_Branch";
                        comboSucursal.DataBind();


                        comboSucursal.Items.Insert(0, new RadComboBoxItem("Selecciona.."));

                        comboSucursal.SelectedValue = ((Label)item.FindControl("lbl_idVinculado_VIRTUAL")).Text;
                       
                        ///
                        /////
                        var codigoZona =  ((RadComboBox)item.FindControl("CMB_asociarA")).SelectedValue;                        


                        var objetoBranch = LogicaNegocios.ModuloCentral.AltaProveedor.objetoViejo(int.Parse(codigoZona));

                        var estado = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catEstados();

                        ((RadComboBox)item.FindControl("gv_VIR_ESTADO")).DataSource = estado;
                        ((RadComboBox)item.FindControl("gv_VIR_ESTADO")).DataTextField = "Dx_Nombre_Estado";
                        ((RadComboBox)item.FindControl("gv_VIR_ESTADO")).DataValueField = "Cve_Estado";
                        ((RadComboBox)item.FindControl("gv_VIR_ESTADO")).DataBind();

                        ((RadComboBox)item.FindControl("gv_VIR_ESTADO")).SelectedValue = objetoBranch.Cve_Estado_Part.Value.ToString();

                        //deleg

                        var deleg = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catDelegacionOMunicipio(objetoBranch.Cve_Estado_Part.Value);

                        ((RadComboBox)item.FindControl("gv_VIR_DELEG")).DataSource = deleg;
                        ((RadComboBox)item.FindControl("gv_VIR_DELEG")).DataTextField = "Dx_Deleg_Municipio";
                        ((RadComboBox)item.FindControl("gv_VIR_DELEG")).DataValueField = "Cve_Deleg_Municipio";
                        ((RadComboBox)item.FindControl("gv_VIR_DELEG")).DataBind();

                        ((RadComboBox)item.FindControl("gv_VIR_DELEG")).SelectedValue = objetoBranch.Cve_Deleg_Municipio_Part.Value.ToString();

                        //Colonia
                        var colonia = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catColonias(objetoBranch.Cve_Deleg_Municipio_Part.Value);

                        ((RadComboBox)item.FindControl("gv_VIR_colonia")).DataSource = colonia;
                        ((RadComboBox)item.FindControl("gv_VIR_colonia")).DataTextField = "Dx_Colonia";
                        ((RadComboBox)item.FindControl("gv_VIR_colonia")).DataValueField = "Cve_CP";
                        ((RadComboBox)item.FindControl("gv_VIR_colonia")).DataBind();

                        var edi = LogicaNegocios.ModuloCentral.CatalogosRegionZona.CODPOSTAL(objetoBranch.Dx_Domicilio_Part_CP);

                        ((RadComboBox)item.FindControl("gv_VIR_colonia")).SelectedValue = edi.Cve_CP.ToString();

                     ((Label)item.FindControl("lbl_IDFIS")).Text = codigoZona.ToString();
                   
                        ////
                        ///

                    
                        if (label != null)
                        {

                            string branch = label.Text;
                            var credito = LogicaNegocios.ModuloCentral.AltaProveedor.tieneSolicitud(int.Parse(branch));


                            if (credito != null)
                            {
                                region.Enabled = false;
                                zona.Enabled = false;
                                NombreComercial.Enabled = false;
                                dependencia.Enabled = false;

                                RWM_vent.RadAlert("NO SE PUEDE REALIZAR NINGUNA MODIFICACIÓN YA QUE SE CUENTA CON SOLICITUDES REGISTRADAS", 300, 300, "ATENCION", null);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        protected void RG_SUC_VIRTUALES_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {

                var item = e.Item as GridEditableItem;
                var objeto = new CAT_PROVEEDORBRANCH();

                objeto.Cve_Estatus_Proveedor = (int)ProviderStatus.PENDIENTE;
                objeto.Id_Proveedor = int.Parse(rad_cmbTipoPersona.SelectedValue);
                objeto.Cve_Region = int.Parse((item.FindControl("GV_VIR_cmbregion") as RadComboBox).SelectedValue);

                objeto.Dx_Razon_Social = RAD_txtNombre.Text;

                //
                var infoObjeto = (item.FindControl("CMB_asociarA") as RadComboBox).SelectedValue;
                var branch = LogicaNegocios.ModuloCentral.AltaProveedor.objetoViejo(int.Parse(infoObjeto));
                //
                objeto.Dx_Nombre_Comercial = (item.FindControl("txt_VIR_NombreComercial") as RadTextBox).Text.ToUpper();

                objeto.Dx_Domicilio_Part_CP = branch.Dx_Domicilio_Part_CP;
                //
                objeto.Dt_Fecha_Branch = branch.Fecha_Reg_Nac;
                objeto.Cve_CP_Part = branch.Cve_CP_Part;
                    objeto.Cve_CP_Fiscal = branch.Cve_CP_Fiscal;
                objeto.Cve_Estado_Part = branch.Cve_Estado_Part.Value;
                objeto.Cve_Deleg_Municipio_Part = branch.Cve_Deleg_Municipio_Part.Value;

                objeto.Dx_Domicilio_Part_Colonia = branch.Dx_Domicilio_Part_Colonia;

                objeto.Dx_RFC = RAD_txtRFC.Text;

                objeto.Dx_Domicilio_Part_Calle = branch.Dx_Domicilio_Part_Calle;
                objeto.Dx_Domicilio_Part_Num = branch.Dx_Domicilio_Part_Num;

                objeto.Dx_Domicilio_Fiscal_Colonia = RAD_FIS_Colonia.SelectedItem.Text;

                //Domicilio Fiscal
                //gv_VIR_colonia
                objeto.Dx_Domicilio_Fiscal_Calle = RAD_FIS_calle.Text;
                objeto.Dx_Domicilio_Fiscal_CP = RAD_FIS_nCodPos.Text;
                objeto.Dx_Domicilio_Fiscal_Num = RAD_FIS_NUMERO.Text;
                objeto.Cve_Deleg_Municipio_Fisc = int.Parse(RAD_FIS_DelegOMuni.SelectedValue);
                objeto.Cve_Estado_Fisc = int.Parse(RAD_FIS_cmbEstados.SelectedValue);
                ///

                objeto.Nombre_Responsable = branch.Nombre_Responsable;
                objeto.Apellido_Paterno_Resp = branch.Apellido_Paterno_Resp;
                objeto.Apellido_Materno_Resp = branch.Apellido_Materno_Resp;
                objeto.Dx_Email_Repre = branch.Dx_Email_Repre;
                objeto.Dx_Telefono_Repre = branch.Dx_Telefono_Repre;

                objeto.Nombre_Rep_Legal = branch.Nombre_Rep_Legal;
                objeto.Apellido_Paterno_Rep_Legal = branch.Apellido_Paterno_Rep_Legal;
                objeto.Apellido_Materno_Rep_Legal = branch.Apellido_Materno_Rep_Legal;

                objeto.Dx_Nombre_Banco = RAD_txtNombreBanco.Text;
                objeto.Dx_Cuenta_Banco = RAD_txtNumClabe.Text;
                objeto.Pct_Tasa_IVA = double.Parse(IVA.SelectedValue);
                
                objeto.Codigo_Branch = string.Format("{0:D3}", LsUtility.GetNumberSequence("BRANCHPRO"));

                objeto.Nombre = RAD_txtNombre.Text;
                objeto.Apellido_Paterno = RAD_txtApePat.Text;
                objeto.Apellido_Materno = RAD_txtApeMat.Text;

                objeto.Dt_Fecha_Creacion = DateTime.Now;
                objeto.Acta_Constitutiva = branch.Acta_Constitutiva;
                objeto.Tipo_Sucursal = "SB_V";

                objeto.id_Dependencia = int.Parse((item.FindControl("CMB_asociarA") as RadComboBox).SelectedValue);
                objeto.Cve_Zona = int.Parse((item.FindControl("gv_VIR_cmbZona") as RadComboBox).SelectedValue);

                //

                var res = LogicaNegocios.ModuloCentral.AltaProveedor.agregarSucursal(objeto);

                if (res != null)
                {
                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                        Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                        "EMPRESAS", "ALTA", objeto.Id_Branch.ToString(CultureInfo.InvariantCulture),
                        "", "Fecha alta: " + DateTime.Now, "", "Cve_Estatus_Proveedor: " + objeto.Cve_Estatus_Proveedor);


                    RWM_vent.RadAlert("INFORMACIÓN ALMACENADA CORRECTAMENTE", 300, 100, "SUCURSAL VIRTUAL: ", null);

                }
            }
            catch (Exception x)
            {
                RWM_vent.RadAlert(x.Message, 300, 100, "ATENCION: ", null);

            }

        }

        protected void RG_SUC_VIRTUALES_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            var it = (RadTextBox)item.FindControl("txt_Motivos_Editar_Virtual");     
            if (it.Text != "")
            {

                try
                {

                    var id = int.Parse((item.FindControl("GV_VIR_ID") as Label).Text);
                    var datosAnteriores = LogicaNegocios.ModuloCentral.AltaProveedor.objetoViejo(id);
                    var nuevoObjeto = LogicaNegocios.ModuloCentral.AltaProveedor.objetoViejo(id);

                    if (nuevoObjeto == null)
                    {
                        return;
                    }

                    nuevoObjeto.Cve_Region = int.Parse((item.FindControl("GV_VIR_cmbregion") as RadComboBox).SelectedValue);
                    nuevoObjeto.Cve_Zona = int.Parse((item.FindControl("gv_VIR_cmbZona") as RadComboBox).SelectedValue);
                    nuevoObjeto.Dx_Nombre_Comercial = (item.FindControl("txt_VIR_NombreComercial") as RadTextBox).Text.ToUpper();
                    nuevoObjeto.id_Dependencia = int.Parse((item.FindControl("CMB_asociarA") as RadComboBox).SelectedValue);

                    //
                    var infoObjeto = (item.FindControl("CMB_asociarA") as RadComboBox).SelectedValue;
                    var branch = LogicaNegocios.ModuloCentral.AltaProveedor.objetoViejo(int.Parse(infoObjeto));
                    //
                    nuevoObjeto.Dx_Nombre_Comercial = (item.FindControl("txt_VIR_NombreComercial") as RadTextBox).Text.ToUpper();

                    nuevoObjeto.Dx_Domicilio_Part_CP = branch.Dx_Domicilio_Part_CP;
                    nuevoObjeto.Cve_Estado_Part = branch.Cve_Estado_Part.Value;
                    nuevoObjeto.Cve_Deleg_Municipio_Part = branch.Cve_Deleg_Municipio_Part.Value;


                    nuevoObjeto.Dx_Domicilio_Part_Calle = branch.Dx_Domicilio_Part_Calle;
                    nuevoObjeto.Dx_Domicilio_Part_Num = branch.Dx_Domicilio_Part_Num;
                    nuevoObjeto.Dx_Domicilio_Part_Colonia = branch.Dx_Domicilio_Part_Colonia;
                    //nuevoObjeto.Dx_Domicilio_Fiscal_Colonia = 


                    nuevoObjeto.Nombre_Responsable = branch.Nombre_Responsable;
                    nuevoObjeto.Apellido_Paterno_Resp = branch.Apellido_Paterno_Resp;
                    nuevoObjeto.Apellido_Materno_Resp = branch.Apellido_Materno_Resp;
                    nuevoObjeto.Dx_Email_Repre = branch.Dx_Email_Repre;
                    nuevoObjeto.Dx_Telefono_Repre = branch.Dx_Telefono_Repre;

                    nuevoObjeto.Nombre_Rep_Legal = branch.Nombre_Rep_Legal;
                    nuevoObjeto.Apellido_Paterno_Rep_Legal = branch.Apellido_Paterno_Rep_Legal;
                    nuevoObjeto.Apellido_Materno_Rep_Legal = branch.Apellido_Materno_Rep_Legal;

                    nuevoObjeto.Acta_Constitutiva = branch.Acta_Constitutiva;

                    nuevoObjeto.Dt_Fecha_Branch = branch.Fecha_Reg_Nac;
                    nuevoObjeto.Cve_CP_Part = branch.Cve_CP_Part;

                    nuevoObjeto.Cve_CP_Fiscal = branch.Cve_CP_Fiscal;

                    var res = new LogicaNegocios.ModuloCentral.AltaProveedor().actualizarSucursalVirtual(nuevoObjeto);

                    if (res != null)
                    {
                        var cambioDatos = Insertlog.GetCambiosDatos(datosAnteriores, nuevoObjeto);
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "EMPRESAS", "CAMBIOS", nuevoObjeto.Id_Branch.ToString(CultureInfo.InvariantCulture), it.Text,//txtMotivos.Text,
                            "", cambioDatos[0], cambioDatos[1]);

                        RWM_vent.RadAlert("INFORMACIÓN ACTUALIZADA CORRECTAMENTE", 300, 100, "ATENCION: ", null);

                    }
                }
                catch (Exception x)
                {
                    RWM_vent.RadAlert(x.Message, 300, 100, "ATENCION: ", null);

                }


            }
            else
            {
                RWM_vent.RadAlert("DEBE COLOCAR LOS MOTIVOS", 300, 100, "ATENCION: ", null);
                e.Canceled = true;
            }
        }

        #endregion

        protected void GV_txt_CodigoPostal_FIS_TextChanged(object sender, EventArgs e)
        {
            var textbox = sender as RadNumericTextBox;

            var text = textbox.NamingContainer as GridEditableItem;
            if (string.IsNullOrEmpty((text.FindControl("GV_txt_CodigoPostal_FIS_edit") as RadNumericTextBox).Text))
            {
                (text.FindControl("FIS_estado_GV") as RadComboBox).Enabled = true;
                (text.FindControl("GV_cmbDelegoMunic") as RadComboBox).Enabled = true;
                (text.FindControl("gv_fisica_cmbcolonia") as RadComboBox).Enabled = true;
                (text.FindControl("GV_cmbDelegoMunic") as RadComboBox).Items.Clear();
                (text.FindControl("gv_fisica_cmbcolonia") as RadComboBox).Items.Clear();

                (text.FindControl("FIS_estado_GV") as RadComboBox).SelectedIndex = 0;

            }
            try
            {
                
                if (textbox != null)
                {
                    var editedItem = textbox.NamingContainer as GridEditableItem;

                    var estado = cargaXcodPostal_Fisica((editedItem.FindControl("GV_txt_CodigoPostal_FIS_edit") as RadNumericTextBox).Text);
                    if(estado == null)
                    {
                       
                        return;
                    }
                    var listaEstado = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catEstados();


                    (editedItem.FindControl("FIS_estado_GV") as RadComboBox).DataSource = listaEstado;
                    (editedItem.FindControl("FIS_estado_GV") as RadComboBox).DataTextField = "Dx_Nombre_Estado";
                    (editedItem.FindControl("FIS_estado_GV") as RadComboBox).DataValueField = "Cve_Estado";
                    (editedItem.FindControl("FIS_estado_GV") as RadComboBox).DataBind();
                    //Estado


                    (editedItem.FindControl("FIS_estado_GV") as RadComboBox).SelectedValue = estado.Cve_Estado.ToString();
                    (editedItem.FindControl("FIS_estado_GV") as RadComboBox).Enabled = false;

                    //Delegacion

                    var listaDeleg = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catDelegacionOMunicipio(estado.Cve_Estado);

                    (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).DataSource = listaDeleg;
                    (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).DataTextField = "Dx_Deleg_Municipio";
                    (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).DataValueField = "Cve_Deleg_Municipio";
                    (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).DataBind();

                    (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).SelectedValue = estado.Cve_Deleg_Municipio.Value.ToString();
                    (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).Enabled = false;

                    //gv_fisica_cmbcolonia

                    var listaColo = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catColonias(estado.Cve_Deleg_Municipio.Value);

                    (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).DataSource = listaColo;
                    (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).DataTextField = "Dx_Colonia";
                    (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).DataValueField = "Cve_CP";
                    (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).DataBind();



                    (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).SelectedValue = estado.Cve_CP.ToString();
                   

                }
            }
            catch (Exception)
            {

            }
        }


        protected CAT_CODIGO_POSTAL_SEPOMEX cargaXcodPostal_Fisica(string codPos)
        {

            string cod = codPos;

            if (cod == null)
            {
             
            }
            else
            {
                sepo = LogicaNegocios.ModuloCentral.CatalogosRegionZona.CODPOSTAL(cod);
                if (sepo == null)
                {
                    RWM_vent.RadAlert("EL CÓDIGO POSTAL DE LA SUCURSAL FISICA NO ES VALIDO", 300, 150, "AGREGAR SUCURSAL FISICA", null);
            
                }
               
            }

            return sepo;
        }

               
        protected void FIS_estado_GV_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
             var comboEstado = sender as RadComboBox;
             if (comboEstado != null)
             {
                 try
                 {
                     var editedItem = comboEstado.NamingContainer as GridEditableItem;
                     string codEstado = (editedItem.FindControl("FIS_estado_GV") as RadComboBox).SelectedValue;

                     var listaDeleg = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catDelegacionOMunicipio(int.Parse(codEstado));

                     (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).DataSource = listaDeleg;
                     (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).DataTextField = "Dx_Deleg_Municipio";
                     (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).DataValueField = "Cve_Deleg_Municipio";
                     (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).DataBind();

                     (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).Items.Insert(0, new RadComboBoxItem(""));
                     (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).SelectedIndex = 0;
                 }
                 catch (Exception)
                 {
                     var editedItem = comboEstado.NamingContainer as GridEditableItem;
                     (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).Items.Clear();

                     (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).Items.Clear();
                     (editedItem.FindControl("GV_txt_CodigoPostal_FIS_edit") as RadNumericTextBox).Text = "";

                     return;
                 }           

             }

        }

        protected void GV_cmbDelegoMunic_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var comboDeleg = sender as RadComboBox;
            if (comboDeleg != null)
            {
                try
                {
                    var editedItem = comboDeleg.NamingContainer as GridEditableItem;
                    string codigoColonia = (editedItem.FindControl("GV_cmbDelegoMunic") as RadComboBox).SelectedValue;


                    var listaColo = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catColonias(int.Parse(codigoColonia));

                    (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).DataSource = listaColo;
                    (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).DataTextField = "Dx_Colonia";
                    (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).DataValueField = "Cve_CP";
                    (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).DataBind();

                    (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).Items.Insert(0, new RadComboBoxItem(""));
                    (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).SelectedIndex = 0;
                }
                catch (Exception)
                {
                    var editedItem = comboDeleg.NamingContainer as GridEditableItem;
                    (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).Items.Clear();
                    (editedItem.FindControl("GV_txt_CodigoPostal_FIS_edit") as RadNumericTextBox).Text = "";
                    return;
                }



            }
        }

        protected void gv_fisica_cmbcolonia_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            var comboColonia = sender as RadComboBox;
            if (comboColonia != null)
            {
                var editedItem = comboColonia.NamingContainer as GridEditableItem;
                try
                {
                    string codigoPos = (editedItem.FindControl("gv_fisica_cmbcolonia") as RadComboBox).SelectedValue;

                    var cod_postal = LogicaNegocios.ModuloCentral.CatalogosRegionZona.obtieneCodpostal(int.Parse(codigoPos));

                    (editedItem.FindControl("GV_txt_CodigoPostal_FIS_edit") as RadNumericTextBox).Text = cod_postal.Codigo_Postal;
                }
                catch (Exception)
                {
                    (editedItem.FindControl("GV_txt_CodigoPostal_FIS_edit") as RadNumericTextBox).Text = "";
                    return;
                }
               


            }
        }

        protected void txtNombreComercial_TextChanged(object sender, EventArgs e)
        {
            var textbox = sender as RadTextBox;
            if (textbox != null)
            {
                var editedItem = textbox.NamingContainer as GridEditableItem;
                string nombreComercial = (editedItem.FindControl("txtNombreComercial_edit") as RadTextBox).Text;
                var existe = LogicaNegocios.ModuloCentral.AltaProveedor.buscarSucursalFisica(nombreComercial);
                if (existe == null)
                {
                    return;
                }
                else
                {
                    RWM_vent.RadAlert("YA EXISTE UNA SUCURSAL CON EL MISMO NOMBRE COMERCIAL, FAVOR DE INGRESAR OTRO NOMBRE", 300, 150, "SUCURSAL FISICA", null);

                }
            }
        }

        protected void cargaDoc_FileUploaded(object sender, FileUploadedEventArgs e)
        {
           

            //var Upload = sender as RadAsyncUpload;
            //if (Upload != null)
            //{
            //    var editedItem = Upload.NamingContainer as GridEditableItem;
            //    if (e.File.FileName != null)
            //    {
            //        PoderNot = new byte[e.File.ContentLength];
            //        e.File.InputStream.Read(PoderNot, 0, e.File.ContentLength);

            //        poderNotarial = PoderNot;

            //        if (e.IsValid)
            //        {
            //            (editedItem.FindControl("gv_fisica_verAC") as ImageButton).ImageUrl = "~/CentralModule/images/visualizar.png";
            //            (editedItem.FindControl("gv_fisica_verAC") as ImageButton).Visible = true;


            //        }
            //    }

            //}

            ////(editedItem.FindControl("EliminarCarta") as ImageButton).Visible = true;
            //(editedItem.FindControl("verCarta") as ImageButton).Visible = true;
        }

        protected void cargaPoderNotarial_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            var Upload = sender as RadAsyncUpload;
            var editedItem = Upload.NamingContainer as GridEditableItem;
            if (Upload != null)
            {
              
                if (e.File.FileName != null)
                {
                    var PoderNot = new byte[e.File.ContentLength];
                    e.File.InputStream.Read(PoderNot, 0, e.File.ContentLength);
                
                    poderNotarial = PoderNot;

                    if (e.IsValid)
                    {
                        (editedItem.FindControl("gv_verPODERNOT") as ImageButton).ImageUrl = "~/CentralModule/images/visualizar.png";
                        (editedItem.FindControl("gv_verPODERNOT") as ImageButton).Visible = true;
                        (editedItem.FindControl("gv_EliminarPoder") as ImageButton).Visible = true;
                        (editedItem.FindControl("cargaPoderNotarial") as RadAsyncUpload).Visible = false;


                    }

                    ClearContents(sender as Control);
                }
            }
        }

        protected void gv_EliminarPoder_Click(object sender, ImageClickEventArgs e)
        {
            var eliminar = sender as ImageButton;
            if (eliminar != null)
            {
                var item = eliminar.NamingContainer as GridEditableItem;

                (item.FindControl("gv_verPODERNOT") as ImageButton).Visible = false;
                (item.FindControl("gv_EliminarPoder") as ImageButton).Visible = false;
                (item.FindControl("cargaPoderNotarial") as RadAsyncUpload).Visible = true;
            }

        }

        protected void GV_VIR_cmbregion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var comboRegion = sender as RadComboBox;
            if (comboRegion != null)
            {
                try
                {
                    var editedItem = comboRegion.NamingContainer as GridEditableItem;
                    string codigoZona = (editedItem.FindControl("GV_VIR_cmbregion") as RadComboBox).SelectedValue;


                    var listaZona = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catZonaxidRegion(int.Parse(codigoZona));

                    (editedItem.FindControl("gv_VIR_cmbZona") as RadComboBox).DataSource = listaZona;
                    (editedItem.FindControl("gv_VIR_cmbZona") as RadComboBox).DataTextField = "Dx_Nombre_Zona";
                    (editedItem.FindControl("gv_VIR_cmbZona") as RadComboBox).DataValueField = "Cve_Zona";
                    (editedItem.FindControl("gv_VIR_cmbZona") as RadComboBox).DataBind();
                }
                catch (Exception)
                {
                    var editedItem = comboRegion.NamingContainer as GridEditableItem;
                    (editedItem.FindControl("gv_VIR_cmbZona") as RadComboBox).Items.Clear();
                    return;
                }
            }


        }

        protected void CMB_asociarA_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                var comboEstado = sender as RadComboBox;
                if (comboEstado != null)
                {
                    var editedItem = comboEstado.NamingContainer as GridEditableItem;
                    var codigoZona = (editedItem.FindControl("CMB_asociarA") as RadComboBox).SelectedValue;


                    var objetoBranch = LogicaNegocios.ModuloCentral.AltaProveedor.objetoViejo(int.Parse(codigoZona));

                    var estado = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catEstados();

                    (editedItem.FindControl("gv_VIR_ESTADO") as RadComboBox).DataSource = estado;
                    (editedItem.FindControl("gv_VIR_ESTADO") as RadComboBox).DataTextField = "Dx_Nombre_Estado";
                    (editedItem.FindControl("gv_VIR_ESTADO") as RadComboBox).DataValueField = "Cve_Estado";
                    (editedItem.FindControl("gv_VIR_ESTADO") as RadComboBox).DataBind();

                    (editedItem.FindControl("gv_VIR_ESTADO") as RadComboBox).SelectedValue = objetoBranch.Cve_Estado_Part.Value.ToString();

                    //deleg

                    var deleg = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catDelegacionOMunicipio(objetoBranch.Cve_Estado_Part.Value);

                    (editedItem.FindControl("gv_VIR_DELEG") as RadComboBox).DataSource = deleg;
                    (editedItem.FindControl("gv_VIR_DELEG") as RadComboBox).DataTextField = "Dx_Deleg_Municipio";
                    (editedItem.FindControl("gv_VIR_DELEG") as RadComboBox).DataValueField = "Cve_Deleg_Municipio";
                    (editedItem.FindControl("gv_VIR_DELEG") as RadComboBox).DataBind();

                    (editedItem.FindControl("gv_VIR_DELEG") as RadComboBox).SelectedValue = objetoBranch.Cve_Deleg_Municipio_Part.Value.ToString();

                    //Colonia
                    var colonia = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catColonias(objetoBranch.Cve_Deleg_Municipio_Part.Value);

                    (editedItem.FindControl("gv_VIR_colonia") as RadComboBox).DataSource = colonia;
                    (editedItem.FindControl("gv_VIR_colonia") as RadComboBox).DataTextField = "Dx_Colonia";
                    (editedItem.FindControl("gv_VIR_colonia") as RadComboBox).DataValueField = "Cve_CP";
                    (editedItem.FindControl("gv_VIR_colonia") as RadComboBox).DataBind();

                    var edi = LogicaNegocios.ModuloCentral.CatalogosRegionZona.CODPOSTAL(objetoBranch.Dx_Domicilio_Part_CP);

                    (editedItem.FindControl("gv_VIR_colonia") as RadComboBox).SelectedValue = edi.Cve_CP.ToString();

                    (editedItem.FindControl("lbl_IDFIS") as Label).Text = codigoZona.ToString();

                }

            }
            catch (Exception)
            {
                return;
            }
        }

        protected void txt_VIR_NombreComercial_TextChanged(object sender, EventArgs e)
        {
            var textbox = sender as RadTextBox;
            if (textbox != null)
            {
                var editedItem = textbox.NamingContainer as GridEditableItem;
                var nombreComercial = (editedItem.FindControl("txt_VIR_NombreComercial") as RadTextBox);
                var existe = LogicaNegocios.ModuloCentral.AltaProveedor.buscaSucursalVirtual(nombreComercial.Text);
                if (existe == null)
                {
                    return;
                }
                else
                {
                    RWM_vent.RadAlert("YA EXISTE UNA SUCURSAL CON EL MISMO NOMBRE COMERCIAL, FAVOR DE INGRESAR OTRO NOMBRE", 300, 150, "SUCURSAL VIRTUAL", null);
                    nombreComercial.Text = "";

                }
            }
        }

        protected void btn_Salir_Sucursal_Click(object sender, EventArgs e)
        {
            Response.Redirect("../RegionalModule/SupplierMonitor.aspx");
        }

       

       
    }
}