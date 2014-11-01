using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.ModuloCentral;
using PAEEEM.LogicaNegocios.Tarifas;

namespace PAEEEM.CentralModule
{
    public partial class AdministradorTecnologias : Page
    {
        #region Variables Globales
        public string UserName
        {
            get { return ViewState["UserName"] == null ? string.Empty : ViewState["UserName"].ToString(); }
            set { ViewState["UserName"] = value; }
        }
        public List<COMP_TECNOLOGIA> LstTecnologias
        {
            set
            {
                ViewState["LstTecnologias"] = value;
            }
            get
            {
                return (List<COMP_TECNOLOGIA>)ViewState["LstTecnologias"];
            }
        }

        public COMP_TECNOLOGIA Tecnologia
        {
            set
            {
                ViewState["Tecnologia"] = value;
            }
            get
            {
                return (COMP_TECNOLOGIA)ViewState["Tecnologia"];
            }
        }

        public List<CAT_TARIFA> LstTarifas
        {
            set
            {
                ViewState["LstTarifas"] = value;
            }
            get
            {
                return (List<CAT_TARIFA>)ViewState["LstTarifas"];
            }
        }

        public int IdTecnologia
        {
            set
            {
                ViewState["IdTecnologia"] = value;
            }
            get
            {
                return (int)ViewState["IdTecnologia"];
            }
        }

        public int InsertaActualiza
        {
            set
            {
                ViewState["InsertaActualiza"] = value;
            }
            get
            {
                return (int)ViewState["InsertaActualiza"];
            }
        }
        #endregion

        #region Carga Inicial
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (IsPostBack) return;
            try
            {
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                UserName = Session["UserName"].ToString();
                LLenaGridTecnologias();
                ImgBtnHome.Visible = false;
            }
            catch (Exception ex)
            { }
        }

        #endregion       

        #region Metodos Protegidos
        protected void LLenaGridTecnologias()
        {
            LstTecnologias = CatalogosTecnologia.ClassInstance.ObtenTecnologias();
            grdAdministraTecnologias.DataSource = LstTecnologias;
            grdAdministraTecnologias.DataBind();
            AspNetPager.RecordCount = LstTecnologias.Count;
            ImgBtnHome.Visible = false;
        }

        protected void LLenaGridTecnologias2()
        {
            var paginaActual = AspNetPager.CurrentPageIndex;
            var tamanioPagina = AspNetPager.PageSize;

            grdAdministraTecnologias.DataSource = LstTecnologias.FindAll(
                    me =>
                        me.Rownum >= (((paginaActual - 1) * tamanioPagina) + 1) &&
                        me.Rownum <= (paginaActual * tamanioPagina));

            grdAdministraTecnologias.DataBind();
            ImgBtnHome.Visible = false;
        }

        protected void AddMergedCells(GridViewRow objgridviewrow,TableCell objtablecell, int colspan, string celltext, string backcolor)
        {
            if (objtablecell == null) throw new ArgumentNullException("objtablecell");
            objtablecell = new TableCell {Text = celltext, ColumnSpan = colspan};
            objtablecell.Style.Add("background-color", backcolor);
            objtablecell.Style.Add("font-weight", "bold");
            objtablecell.Style.Add("padding", "4px");
            objtablecell.Style.Add("margin-right", "0px");
            objtablecell.Style.Add("border-left", "1px solid #EBE9ED;");
            objtablecell.Style.Add("border-right", "1px solid #EBE9ED");
            objtablecell.Style.Add("border-bottom", "1px solid #EBE9ED;");
            objtablecell.Style.Add("border-top", "1px solid #EBE9ED");
            objtablecell.HorizontalAlign = HorizontalAlign.Center;
            objgridviewrow.Cells.Add(objtablecell);
        }

        protected void grdAdministraTecnologias_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;
            //Creating a gridview object            
            var objGridView = (GridView)sender;

            //Creating a gridview row object
            var objgridviewrow = new GridViewRow
                (1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            var objtablecell = new TableCell();

            AddMergedCells(objgridviewrow, objtablecell, 1, " ", "#00A3D9");

            AddMergedCells(objgridviewrow, objtablecell, 1, " ", "#00A3D9");

            AddMergedCells(objgridviewrow, objtablecell, 1, " ", "#00A3D9");

            AddMergedCells(objgridviewrow, objtablecell, 1, " ", "#00A3D9");

            AddMergedCells(objgridviewrow, objtablecell, 1, "Chatarrización", "#00A3D9");

            AddMergedCells(objgridviewrow, objtablecell, 2, "Equipos", "#00A3D9");

            AddMergedCells(objgridviewrow, objtablecell, 1, " ", "#00A3D9");

            AddMergedCells(objgridviewrow, objtablecell, 4, "Tarifa", "#00A3D9");

            AddMergedCells(objgridviewrow, objtablecell, 1, "Incentivo", "#00A3D9");

            AddMergedCells(objgridviewrow, objtablecell, 2, " ", "#00A3D9");

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
        }

        protected void CargaDatosTecnologiaSeleccionada()
        {
            Tecnologia = CatalogosTecnologia.ClassInstance.ObtenTecnologiaSeleccionada(IdTecnologia);

            if (Tecnologia == null) return;
            TxtNombreTecnologia.Text = Tecnologia.DxNombreGeneral;
            TxtClaveTecnologia.Text = Tecnologia.DxCveCC;
            DDXEstatusTecnologia.SelectedValue = Tecnologia.Estatus != null ? Tecnologia.Estatus.ToString() : "";
            //DDXTipoCatTecnologia.SelectedValue = Tecnologia.CveTipoTecnologia.ToString(CultureInfo.InvariantCulture);
            DDXTipoTecnologia.SelectedValue = Tecnologia.CveTipoMovimiento ?? "";
            DDXChatarrizacion.SelectedValue = Tecnologia.CveChatarrizacion != null ? Tecnologia.CveChatarrizacion.ToString() : "";
            DDXEquiposBaja.SelectedValue = Tecnologia.CveEquiposBaja != null ? Tecnologia.CveEquiposBaja.ToString() : "";
            DDXEquiposAlta.SelectedValue = Tecnologia.CveEquiposAlta != null ? Tecnologia.CveEquiposAlta.ToString() : "";
            DDXFactorSustucion.SelectedValue = Tecnologia.CveFactorSustitucion != null ? Tecnologia.CveFactorSustitucion.ToString() : "";
            DDXIncentivo.SelectedValue = Tecnologia.CveIncentivo != null ? Tecnologia.CveIncentivo.ToString() : "";
            DDXCombinacionTecnologias.SelectedValue = Tecnologia.CombinaTecnologias != null ? Tecnologia.CombinaTecnologias.ToString() : "";
            DDXTipoPlantilla.SelectedValue = Tecnologia.CvePlantilla != null ? Tecnologia.CvePlantilla.ToString() : "";
            TxtMontoChatarrizacion.Text = Tecnologia.Monto_Chatarrizacion != null ? Tecnologia.Monto_Chatarrizacion.ToString() : "0.00";
            TxtMontoIncentivo.Text = Tecnologia.MontoIncentivo != null ? Tecnologia.MontoIncentivo.ToString() : "0.00";
            DDXLeyendaDescriptiva.SelectedValue = Tecnologia.CveLeyenda != null ? Tecnologia.CveLeyenda.ToString() : "";

            TxtMontoChatarrizacion.Enabled = Tecnologia.CveChatarrizacion == 1 ? true : false;
            TxtMontoIncentivo.Enabled = Tecnologia.CveIncentivo == 1 ? true : false;
            
            CargaTarifasTecnologia();

            if (DDXCombinacionTecnologias.SelectedValue == "1")
            {
                CargaTecnologiasCombinadas();
                ChkLstTecnologiasCombiladas.Visible = true;
            }

            BtnGuardar.Text = @"Actualizar";
            HiddenEstatus.Value = "1";
            PanelGridTecnologias.Visible = false;
            PanelActualizaTecnologia.Visible = true;
        }

        protected void CargaTarifasTecnologia()
        {
            var lstTarifasTecnologia = CatalogosTecnologia.ClassInstance.ObtenTarifasXTecnologia(Tecnologia.CveTecnologia);

            if (lstTarifasTecnologia == null) return;
            for (var i = 0; i < ChkLstTarifas.Items.Count; i++)
            {
                foreach (var tarifa in lstTarifasTecnologia.FindAll(me => me.Estatus == 1))
                {
                    if (ChkLstTarifas.Items[i].Value != tarifa.CveTarifa.ToString(CultureInfo.InvariantCulture)) continue;
                    ChkLstTarifas.Items[i].Selected = true;
                    ChkLstAgoritmos.Items[i].Selected = true;
                }
            }

            Tecnologia.tarifasTecnologia = lstTarifasTecnologia;
        }

        protected void CargaTecnologiasCombinadas()
        {
            var lstTecnologiasCombinadas = CatalogosTecnologia.ClassInstance.ObtenCombinacionTecnologias(Tecnologia.CveTecnologia);

            if (lstTecnologiasCombinadas == null) return;

            for (var i = 0; i < ChkLstTecnologiasCombiladas.Items.Count; i++)
            {
                foreach (var tecnologiaComb in lstTecnologiasCombinadas.FindAll(me => me.Estatus == 1).Where(tecnologiaComb => ChkLstTecnologiasCombiladas.Items[i].Value == tecnologiaComb.CveTecnologiaCombinada.ToString(CultureInfo.InvariantCulture)))
                {
                    ChkLstTecnologiasCombiladas.Items[i].Selected = true;
                }
            }

            Tecnologia.tecnologiasCombinadas = lstTecnologiasCombinadas;
        }

        protected void AgregarNuevaTecnología()
        {
            try
            {
                Tecnologia = new COMP_TECNOLOGIA();
                IdTecnologia = LstTecnologias.Max(me => me.CveTecnologia);
                Tecnologia.CveTecnologia = IdTecnologia + 1;
                Tecnologia.DxNombreGeneral = TxtNombreTecnologia.Text;
                //Tecnologia.CveTipoTecnologia = Convert.ToInt32(DDXTipoCatTecnologia.SelectedValue);
                Tecnologia.DxCveCC = TxtClaveTecnologia.Text;
                Tecnologia.CveTipoMovimiento = DDXTipoTecnologia.SelectedValue;
                Tecnologia.CveEquiposBaja = Convert.ToInt32(DDXEquiposBaja.SelectedValue);
                Tecnologia.CveEquiposAlta = Convert.ToInt32(DDXEquiposAlta.SelectedValue);
                Tecnologia.CveChatarrizacion = Convert.ToInt32(DDXChatarrizacion.SelectedValue);
                Tecnologia.Monto_Chatarrizacion = DDXChatarrizacion.SelectedValue == "1" ? Convert.ToDecimal(TxtMontoChatarrizacion.Text) : 0;
                Tecnologia.CveFactorSustitucion = Convert.ToInt32(DDXFactorSustucion.SelectedValue);
                Tecnologia.CveIncentivo = Convert.ToInt32(DDXIncentivo.SelectedValue);
                Tecnologia.CvePlantilla = Convert.ToInt32(DDXTipoPlantilla.SelectedValue);
                Tecnologia.MontoIncentivo = DDXIncentivo.SelectedValue == "1" ? Convert.ToDecimal(TxtMontoIncentivo.Text) : 0;
                Tecnologia.Estatus = Convert.ToInt32(DDXEstatusTecnologia.SelectedValue);
                Tecnologia.CombinaTecnologias = Convert.ToInt32(DDXCombinacionTecnologias.SelectedValue);
                Tecnologia.CveLeyenda = Convert.ToInt32(DDXLeyendaDescriptiva.SelectedValue);
                Tecnologia.AdicionadoPor = UserName;
                
                if (Tecnologia.CombinaTecnologias == 1)
                {
                    var lstCombinaTecnologias = new List<Combinacion_Tecnologia>();

                    for (var i = 0; i < ChkLstTecnologiasCombiladas.Items.Count; i++)
                    {
                        if (!ChkLstTecnologiasCombiladas.Items[i].Selected) continue;
                        var tecnologiaCombinada = new Combinacion_Tecnologia
                        {
                            CveTecnologiaCombinada = Convert.ToInt32(ChkLstTecnologiasCombiladas.Items[i].Value),
                            Estatus = 1
                        };

                        lstCombinaTecnologias.Add(tecnologiaCombinada);
                    }

                    Tecnologia.tecnologiasCombinadas = lstCombinaTecnologias;
                }

                var lstTarifasTecnologia = new List<Tarifa_Tecnologia>();

                for (var i = 0; i < ChkLstTarifas.Items.Count; i++)
                {
                    if (!ChkLstTarifas.Items[i].Selected) continue;
                    var tar = new Tarifa_Tecnologia
                    {
                        CveTarifa = Convert.ToInt32(ChkLstTarifas.Items[i].Value),
                        Estatus = 1
                    };

                    lstTarifasTecnologia.Add(tar);
                }

                Tecnologia.tarifasTecnologia = lstTarifasTecnologia;

                IdTecnologia = CatalogosTecnologia.ClassInstance.AgregarNuevaTecnologia(Tecnologia);
                
                if (IdTecnologia == 0) return;
                /*INSERTAR EVENTO ALTA DEL PROCESO TECNOLOGIA EN LOG*/
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "TECNOLOGIA", "ALTA",
                    Tecnologia.DxNombreGeneral, "","Fecha alta: " + DateTime.Now, "", "");
             
                    ScriptManager.RegisterClientScriptBlock(PanelActualizaTecnologia, typeof(Page), "Agregar", "alert('Se ha guardado con éxito la Tecnología ')", true);
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(PanelActualizaTecnologia, typeof(Page), "Error", "alert('" + ex.Message + " ')", true);
            }
        }

        protected void ActualizaDatosTecnologia()
        {
            try
            {
                Tecnologia.DxNombreGeneral = TxtNombreTecnologia.Text;
                //Tecnologia.CveTipoTecnologia = Convert.ToInt32(DDXTipoCatTecnologia.SelectedValue);
                Tecnologia.DxCveCC = TxtClaveTecnologia.Text;
                Tecnologia.CveTipoMovimiento = DDXTipoTecnologia.SelectedValue;
                Tecnologia.CveEquiposBaja = Convert.ToInt32(DDXEquiposBaja.SelectedValue);
                Tecnologia.CveEquiposAlta = Convert.ToInt32(DDXEquiposAlta.SelectedValue);
                Tecnologia.CveChatarrizacion = Convert.ToInt32(DDXChatarrizacion.SelectedValue);
                Tecnologia.Monto_Chatarrizacion = DDXChatarrizacion.SelectedValue == "1" ? Convert.ToDecimal(TxtMontoChatarrizacion.Text) : 0;
                Tecnologia.CveFactorSustitucion = Convert.ToInt32(DDXFactorSustucion.SelectedValue);
                Tecnologia.CveIncentivo = Convert.ToInt32(DDXIncentivo.SelectedValue);
                Tecnologia.CvePlantilla = Convert.ToInt32(DDXTipoPlantilla.SelectedValue);
                Tecnologia.MontoIncentivo = DDXIncentivo.SelectedValue == "1" ? Convert.ToDecimal(TxtMontoIncentivo.Text) : 0;
                Tecnologia.Estatus = Convert.ToInt32(DDXEstatusTecnologia.SelectedValue);
                Tecnologia.CombinaTecnologias = Convert.ToInt32(DDXCombinacionTecnologias.SelectedValue);
                Tecnologia.CveLeyenda = Convert.ToInt32(DDXLeyendaDescriptiva.SelectedValue);

                var datosAnteriores = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ObtenObjetoTecnologia(Tecnologia.CveTecnologia);
                       
                var datosDesTecAnteriores = AccesoDatos.Log.Registar.GetDesTecnologias(Tecnologia.CveTecnologia);
               
                var actualizoTecnologia =  CatalogosTecnologia.ClassInstance.ActualizarTecnologia(Tecnologia);

                if (actualizoTecnologia)
                {
                    /*INSERTAR EVENTO CAMBIOS DEL PROCESO TECNOLOGIA EN LOG*/ //(Datos generales de la Tecnología)
                    var datosDesTecActuales = AccesoDatos.Log.Registar.GetDesTecnologias(Tecnologia.CveTecnologia);
                    var cambiosDesTec = Insertlog.GetCambiosDatos(datosDesTecAnteriores, datosDesTecActuales);
                    if (cambiosDesTec[0] != null)
                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                        Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "TECNOLOGIA", "CAMBIOS",
                        Tecnologia.DxNombreGeneral, "MOTIVOS??",
                        "", cambiosDesTec[0], cambiosDesTec[1]);

                    COMP_TECNOLOGIA datosActuales;
                    if (Tecnologia.CombinaTecnologias == 1)
                    {
                        for (var i = 0; i < ChkLstTecnologiasCombiladas.Items.Count; i++)
                        {
                            //if (Tecnologia.CombinaTecnologias == 1)
                            //{
                            //    var lstCombinaTecnologias = new List<Combinacion_Tecnologia>();
                            //    Combinacion_Tecnologia tecnologiaCombinada;

                            //    for (var i = 0; i < ChkLstTecnologiasCombiladas.Items.Count; i++)
                            //    {
                            //        if (ChkLstTecnologiasCombiladas.Items[i].Selected)
                            //        {
                            //            tecnologiaCombinada = new Combinacion_Tecnologia();
                            //            tecnologiaCombinada.CveTecnologiaCombinada =
                            //                Convert.ToInt32(ChkLstTecnologiasCombiladas.Items[i].Value);
                            //            tecnologiaCombinada.Estatus = 1;

                            //            lstCombinaTecnologias.Add(tecnologiaCombinada);
                            //        }
                            //    }

                            //    Tecnologia.tecnologiasCombinadas = lstCombinaTecnologias;
                            //}
                            var idCombinada = Convert.ToInt32(ChkLstTecnologiasCombiladas.Items[i].Value);
                            var tecnologiaExistente =Tecnologia.tecnologiasCombinadas.FirstOrDefault(
                                    me => me.CveTecnologiaCombinada == idCombinada);

                            if (tecnologiaExistente != null)
                            {
                                tecnologiaExistente.Estatus = ChkLstTecnologiasCombiladas.Items[i].Selected ? 1 : 0;
                                tecnologiaExistente.EstatusInt = 2;
                            }
                            else
                            {
                                if (!ChkLstTecnologiasCombiladas.Items[i].Selected) continue;
                                var tecnologiaCombinada = new Combinacion_Tecnologia
                                {
                                    CveTecnologia = Tecnologia.CveTecnologia,
                                    CveTecnologiaCombinada =
                                        Convert.ToInt32(ChkLstTecnologiasCombiladas.Items[i].Value),
                                    Estatus = 1,
                                    EstatusInt = 0
                                };
                                Tecnologia.tecnologiasCombinadas.Add(tecnologiaCombinada);
                            }
                        }
                       
                        var actualizoTecnologiasCombinadas = CatalogosTecnologia.ClassInstance.ActualizaTecnologiasCombinadas(Tecnologia.tecnologiasCombinadas);

                        if (actualizoTecnologiasCombinadas)
                        {
                            /*INSERTAR EVENTO CAMBIOS DEL PROCESO TECNOLOGIA EN LOG*/ //Combinacion Tecnologia
                            if (datosAnteriores.tecnologiasCombinadas.Count > 0)
                            {
                                datosActuales = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ObtenObjetoTecnologia(Tecnologia.CveTecnologia);
                                Insertlog.InsertaCambiosCombinacionTecnologia(datosAnteriores.tecnologiasCombinadas,
                                    datosActuales.tecnologiasCombinadas, Tecnologia.DxNombreGeneral, Convert.ToInt16(Session["IdUserLogueado"]),
                                 Convert.ToInt16(Session["IdRolUserLogueado"]),
                                 Convert.ToInt16(Session["IdDepartamento"]));
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(PanelActualizaTecnologia, typeof (Page), "Error","alert('Ocurrió un problema al Actualizar las Tecnologías Combinadas')", true);
                            return;
                        }
                    }
                    else
                    {
                        if (Tecnologia.tecnologiasCombinadas != null)
                        {
                            foreach (var tec in Tecnologia.tecnologiasCombinadas)
                            {
                                tec.Estatus = 0;
                                tec.EstatusInt = 2;
                            }
                        }
                    }

                    for (var i = 0; i < ChkLstTarifas.Items.Count; i++)
                    {
                        var idTarifaTec = Convert.ToInt32(ChkLstTarifas.Items[i].Value);
                        var tarifaExistente =
                            Tecnologia.tarifasTecnologia.FirstOrDefault(me => me.CveTarifa == idTarifaTec);

                        if (tarifaExistente != null)
                        {
                            tarifaExistente.Estatus = ChkLstTarifas.Items[i].Selected ? 1 : 0;
                            tarifaExistente.EstatusInt = 2;
                        }
                        else
                        {
                            if (!ChkLstTarifas.Items[i].Selected) continue;
                            var tar = new Tarifa_Tecnologia
                            {
                                CveTecnologia = Tecnologia.CveTecnologia,
                                CveTarifa = Convert.ToInt32(ChkLstTarifas.Items[i].Value),
                                Estatus = 1,
                                EstatusInt = 0
                            };
                            Tecnologia.tarifasTecnologia.Add(tar);
                        }
                    }

                    var actualizoTarifas = CatalogosTecnologia.ClassInstance.ActualizaTarifasXTecnologia(Tecnologia.tarifasTecnologia);

                    if (!actualizoTarifas) return;
                   
                    /*INSERTAR EVENTO CAMBIOS DEL PROCESO TECNOLOGIA EN LOG*/ // Tarifas por Tecnología
                    datosActuales = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ObtenObjetoTecnologia(Tecnologia.CveTecnologia);
                    Insertlog.InsertaCambiosTarifasTecnologia(datosAnteriores.tarifasTecnologia, datosActuales.tarifasTecnologia, Convert.ToInt16(Session["IdUserLogueado"]),
                                 Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]));

                    HiddenEstatus.Value = "0";
                    ScriptManager.RegisterClientScriptBlock (PanelActualizaTecnologia, typeof(Page), "Actualizar","alert('Se realizarón los cambios con exito ')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock
                        (PanelActualizaTecnologia, typeof (Page), "Error",
                            "alert('Ocurrió un problema al Actualizar la Tecnología ')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(PanelActualizaTecnologia, typeof(Page), "Error", "alert('" + ex.Message + " ')", true);
            }
        }
        #endregion

        #region Llenar catalogos

        //private void LLenarDdlTipoTecnologia()
        //{
        //    DDXTipoCatTecnologia.Items.Clear();

        //    var lstTipoTecnologia = CatalogosTecnologia.ClassInstance.ObtenCatTipoTecnologia();
        //    DDXTipoCatTecnologia.DataSource = lstTipoTecnologia;
        //    DDXTipoCatTecnologia.DataValueField = "Cve_Tipo_Tecnologia";
        //    DDXTipoCatTecnologia.DataTextField = "Dx_Nombre";
        //    DDXTipoCatTecnologia.DataBind();

        //    DDXTipoCatTecnologia.Items.Insert(0, new ListItem("", ""));
        //    DDXTipoCatTecnologia.SelectedIndex = 0;
        //}

        private void LLenaDdlTipoMovimiento()
        {
            DDXTipoTecnologia.Items.Clear();

            var lstTipoMovimiento = CatalogosTecnologia.ClassInstance.ObtenCatTipoMovimiento();
            if (lstTipoMovimiento == null) throw new ArgumentNullException("lstTipoMo" + "vimiento");
            DDXTipoTecnologia.DataSource = lstTipoMovimiento;
            DDXTipoTecnologia.DataValueField = "Cve_Tipo_Movimiento";
            DDXTipoTecnologia.DataTextField = "Dx_Tipo_Movimiento";
            DDXTipoTecnologia.DataBind();

            DDXTipoTecnologia.Items.Insert(0, new ListItem("", ""));
            DDXTipoTecnologia.SelectedIndex = 0;
        }

        private void LLenarDdlEquiposBajaAlta()
        {
            DDXEquiposBaja.Items.Clear();

            var lstEquiposBajaAlta = CatalogosTecnologia.ClassInstance.ObtenCatEquiposBajaAlta();
            DDXEquiposBaja.DataSource = lstEquiposBajaAlta;
            DDXEquiposBaja.DataValueField = "Cve_Equipos_Baja_Alta";
            DDXEquiposBaja.DataTextField = "Dx_Equipos_Baja_Alta";
            DDXEquiposBaja.DataBind();

            DDXEquiposBaja.Items.Insert(0, new ListItem("", ""));
            DDXEquiposBaja.SelectedIndex = 0;

            DDXEquiposAlta.DataSource = lstEquiposBajaAlta;
            DDXEquiposAlta.DataValueField = "Cve_Equipos_Baja_Alta";
            DDXEquiposAlta.DataTextField = "Dx_Equipos_Baja_Alta";
            DDXEquiposAlta.DataBind();

            DDXEquiposAlta.Items.Insert(0, new ListItem("", ""));
            DDXEquiposAlta.SelectedIndex = 0;
        }

        private void LlenarDdlFactorSustitucion()
        {
            DDXFactorSustucion.Items.Clear();

            var lstFactorSustitucion = CatalogosTecnologia.ClassInstance.ObtenCatFactorSustitucion();
            DDXFactorSustucion.DataSource = lstFactorSustitucion;
            DDXFactorSustucion.DataTextField = "Dx_Factor_Sustitucion";
            DDXFactorSustucion.DataValueField = "Cve_Factor_Sutitucion";
            DDXFactorSustucion.DataBind();

            DDXFactorSustucion.Items.Insert(0, new ListItem("", ""));
            DDXFactorSustucion.SelectedIndex = 0;
        }

        private void LlenarDdlPlantillas()
        {
            DDXTipoPlantilla.Items.Clear();

            var listTiposPlantilla = CatalogosTecnologia.OCatPlantillas();
            DDXTipoPlantilla.DataSource = listTiposPlantilla;
            DDXTipoPlantilla.DataValueField = "Cve_Plantilla";
            DDXTipoPlantilla.DataTextField = "Dx_Descripcion";
            DDXTipoPlantilla.DataBind();

            DDXTipoPlantilla.Items.Insert(0, new ListItem("", ""));
            DDXTipoPlantilla.SelectedIndex = 0;
        }

        private void LLenaDdlGenericos()
        {
            DDXChatarrizacion.Items.Clear();
            DDXChatarrizacion.Items.Add(new ListItem("NO", "0"));
            DDXChatarrizacion.Items.Add(new ListItem("SI", "1"));
            DDXChatarrizacion.Items.Insert(0, new ListItem("", ""));
            DDXChatarrizacion.SelectedIndex = 0;

            DDXCombinacionTecnologias.Items.Clear();
            DDXCombinacionTecnologias.Items.Add(new ListItem("NO", "0"));
            DDXCombinacionTecnologias.Items.Add(new ListItem("SI", "1"));
            DDXCombinacionTecnologias.Items.Insert(0, new ListItem("", ""));
            DDXCombinacionTecnologias.SelectedIndex = 0;

            DDXIncentivo.Items.Clear();
            DDXIncentivo.Items.Add(new ListItem("NO", "0"));
            DDXIncentivo.Items.Add(new ListItem("SI", "1"));
            DDXIncentivo.Items.Insert(0, new ListItem("", ""));
            DDXIncentivo.SelectedIndex = 0;

            DDXLeyendaDescriptiva.Items.Clear();
            DDXLeyendaDescriptiva.Items.Add(new ListItem("NO", "0"));
            DDXLeyendaDescriptiva.Items.Add(new ListItem("SI", "1"));
            DDXLeyendaDescriptiva.Items.Insert(0, new ListItem("", ""));
            DDXLeyendaDescriptiva.SelectedIndex = 0;

            DDXEstatusTecnologia.Items.Add(new ListItem("INACTIVO", "0"));
            DDXEstatusTecnologia.Items.Add(new ListItem("ACTIVO", "1"));
            DDXEstatusTecnologia.Items.Insert(0, new ListItem("", ""));
            DDXEstatusTecnologia.SelectedIndex = 0;

        }

        private void LlenarCheckTarifas()
        {
            ChkLstTarifas.Items.Clear();
            var listTarifas = RegistrarTarifas.TiposTarifa();
            ChkLstTarifas.DataSource = listTarifas;
            ChkLstTarifas.DataValueField = "Cve_Tarifa";
            ChkLstTarifas.DataTextField = "Dx_Tarifa";
            ChkLstTarifas.DataBind();

            ChkLstAgoritmos.Items.Clear();
            ChkLstAgoritmos.DataSource = listTarifas;
            ChkLstAgoritmos.DataValueField = "Cve_Tarifa";
            ChkLstAgoritmos.DataTextField = "Dx_Algoritmo";
            ChkLstAgoritmos.DataBind();

        }

        private void LlenarChkTecnologias()
        {
            ChkLstTecnologiasCombiladas.Items.Clear();
            ChkLstTecnologiasCombiladas.DataSource = LstTecnologias;
            ChkLstTecnologiasCombiladas.DataValueField = "CveTecnologia";
            ChkLstTecnologiasCombiladas.DataTextField = "DxNombreGeneral";
            ChkLstTecnologiasCombiladas.DataBind();
        }
        #endregion

        #region Eventos
        protected void DDXCombinacionTecnologias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDXCombinacionTecnologias.SelectedValue == "1")
            {
                LlenarChkTecnologias();
                ChkLstTecnologiasCombiladas.Visible = true;
                ChkLstTecnologiasCombiladas.Focus();
            }
            else
            {
                ChkLstTecnologiasCombiladas.Visible = false;
            }
        }

        protected void BtnImgEditar_Click(object sender, ImageClickEventArgs e)
        {
            var gridViewRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            var dataKey = grdAdministraTecnologias.DataKeys[gridViewRow.RowIndex];
            if (dataKey != null)
            {
                var idTecnologiaSel = dataKey[0];
                IdTecnologia = (int)idTecnologiaSel;
            }
            InsertaActualiza = 1;
            LblTitulo.Text = @"Editar Tecnología";
            
            LLenaDdlGenericos();
            LLenaDdlTipoMovimiento();
            LLenarDdlEquiposBajaAlta();
            LlenarDdlFactorSustitucion();
            LlenarCheckTarifas();
            LlenarChkTecnologias();
            //LLenarDdlTipoTecnologia();
            LlenarDdlPlantillas();

            CargaDatosTecnologiaSeleccionada();
            ImgBtnHome.Visible = true;
            lblTitle.Visible = false;
        }

        protected void BtnImgAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LLenaDdlGenericos();
                LLenaDdlTipoMovimiento();
                LLenarDdlEquiposBajaAlta();
                LlenarDdlFactorSustitucion();
                LlenarCheckTarifas();
                LlenarChkTecnologias();
                //LLenarDdlTipoTecnologia();
                LlenarDdlPlantillas();

                ImgBtnHome.Visible = true;
                lblTitle.Visible = false;
                PanelGridTecnologias.Visible = false;
                PanelActualizaTecnologia.Visible = true;
                InsertaActualiza = 0;
                LblTitulo.Text = @"Agregar Nueva Tecnología";
                BtnGuardar.Text = @"Guardar";
                TxtNombreTecnologia.Focus();
            }
            catch(Exception ex)
            { 
                
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (!Page.IsValid) return;
            if (InsertaActualiza == 0)
                AgregarNuevaTecnología();
            if (InsertaActualiza == 1)
                ActualizaDatosTecnologia();

            grdAdministraTecnologias.DataSource = null;
            grdAdministraTecnologias.DataBind();
            LLenaGridTecnologias();
            PanelActualizaTecnologia.Controls.Clear();
            PanelActualizaTecnologia.Visible = false;
            PanelGridTecnologias.Visible = true;
            ImgBtnHome.Visible = false;
            lblTitle.Visible = true;
        }

        protected void ChkLstTarifas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var result = Request.Form["__EVENTTARGET"];

            var checkedBox = result.Split('$'); 

            var index = int.Parse(checkedBox[checkedBox.Length - 1]);

            ChkLstAgoritmos.Items[index].Selected = ChkLstTarifas.Items[index].Selected;

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            ImgBtnHome.Visible = false;
            lblTitle.Visible = true;
            TxtNombreTecnologia.Text = "";
            TxtClaveTecnologia.Text = "";
            TxtMontoIncentivo.Text = "";
            TxtMontoChatarrizacion.Text = "";
            PanelActualizaTecnologia.Controls.Clear();
            PanelActualizaTecnologia.Visible = false;           
            PanelGridTecnologias.Visible = true;
        }

        protected void DDXChatarrizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDXChatarrizacion.SelectedValue == "1")
            {
                TxtMontoChatarrizacion.Text = @"400.0";
                TxtMontoChatarrizacion.Enabled = true;
            }
            if (DDXChatarrizacion.SelectedValue == "0")
            {
                TxtMontoChatarrizacion.Text = @"0.00";
                TxtMontoChatarrizacion.Enabled = false;
            }
        }

        protected void DDXIncentivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDXIncentivo.SelectedValue == "1")
            {
                TxtMontoIncentivo.Text = @"10";
                TxtMontoIncentivo.Enabled = true;
                TxtMontoIncentivo.Focus();
            }
            if (DDXIncentivo.SelectedValue == "0")
            {
                TxtMontoIncentivo.Text = @"0.00";
                TxtMontoIncentivo.Enabled = false;
            }
        }

        protected void ImgBtnHome_Click(object sender, ImageClickEventArgs e)
        {
            PanelActualizaTecnologia.Controls.Clear();
            PanelActualizaTecnologia.Visible = false;
            PanelGridTecnologias.Visible = true;
            LLenaGridTecnologias();
        }

        #endregion               

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            lblTitle.Visible = true;
            PanelActualizaTecnologia.Controls.Clear();
            PanelActualizaTecnologia.Visible = false;
            PanelGridTecnologias.Visible = true;
            LLenaGridTecnologias();
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            LLenaGridTecnologias2();
        }        
    }
}