using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Infragistics.Shared;
using Infragistics.WebUI.Misc;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI;
using System.Drawing;
/// <summary>
/// Descripción breve de CeC_Grid
/// </summary>
public class CeC_Grid
{
    private static DS_Campos s_ds_Campos = null;
    protected DS_Campos m_ds_Campos = null;
    CeC_Sesion Sesion = null;

    public CeC_Grid()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// Especifica el tipo de columna, en caso de ser de tipo booleano
    /// se regresara una columna de tipo checkbox, si el CATALOGO_Id es mayor que cero
    /// se mostrara una lista desplegable
    /// </summary>
    /// <param name="Campo"></param>
    /// <returns></returns>
    private static ColumnType Tipo(DS_Campos.EC_CAMPOSRow Campo)
    {
        CeC_Campos.Tipo_Datos TDato = (CeC_Campos.Tipo_Datos)Campo.TIPO_DATO_ID;
        switch (TDato)
        {
            case CeC_Campos.Tipo_Datos.Boleano:
                return ColumnType.CheckBox;
                break;
        }
        if (Campo.CATALOGO_ID > 0)
        {
            return ColumnType.DropDownList;
        }
        return ColumnType.NotSet;
    }
    /// <summary>
    /// Regresa 
    /// </summary>
    /// <param name="Campo"></param>
    /// <param name="FilaCatalogo"></param>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    private static ValueList Catalogo(DS_Campos.EC_CAMPOSRow Campo, DS_Campos.EC_CATALOGOSRow FilaCatalogo, CeC_Sesion Sesion)
    {
        return Catalogo(Campo, FilaCatalogo, Sesion, false);
    }
    //Infragistics.WebUI.UltraWebGrid.UltraWebGrid Grid
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Campo"></param>
    /// <param name="Catalogo"></param>
    /// <param name="Sesion"></param>
    /// <param name="SinFiltro"></param>
    /// <returns></returns>
    private static ValueList Catalogo(DS_Campos.EC_CAMPOSRow Campo, DS_Campos.EC_CATALOGOSRow Catalogo, CeC_Sesion Sesion, bool SinFiltro)
    {
        DataSet DS;
        if (Catalogo.CATALOGO_ID == 1)
            DS = CeC_Campos.CatalogoDT(Campo.CAMPO_NOMBRE, Sesion);
        else
            DS = CeC_Campos.CatalogoDT(Catalogo, Sesion);
        if (DS == null)
            return null;
        if (Catalogo == null)
            return null;

        CeC_Campos.AplicaFormatoDataset(DS, DS.Tables[0].TableName);
        ValueList VL = new ValueList();
        VL.DataSource = DS;
        VL.DataMember = DS.Tables[0].TableName;
        if (Catalogo.CATALOGO_ID == 1)
        {
            VL.DisplayMember = DS.Tables[0].Columns[0].ColumnName;
            VL.ValueMember = DS.Tables[0].Columns[0].ColumnName;
        }
        else
        {
            VL.DisplayMember = Catalogo.CATALOGO_C_DESC;
            VL.ValueMember = Catalogo.CATALOGO_C_LLAVE;
        }
        VL.Prompt = "<Sin Selección>";
        VL.DisplayStyle = ValueListDisplayStyle.DisplayText;
        VL.DataBind();
        return VL;
    }
    /// <summary>
    /// Permite agrupar por entero,decimal,fecha,fecha y hora, hora,
    /// y texto
    /// </summary>
    /// <param name="Campo"></param>
    /// <returns></returns>
    private static AllowGroupBy PermitirAgrupar(DS_Campos.EC_CAMPOSRow Campo)
    {
        switch ((CeC_Campos.Tipo_Datos)(Campo.TIPO_DATO_ID))
        {
            case CeC_Campos.Tipo_Datos.Entero:
                return AllowGroupBy.Yes;
                break;
            case CeC_Campos.Tipo_Datos.Desconocido:
                return AllowGroupBy.No;
                break;
            case CeC_Campos.Tipo_Datos.Decimal:
                return AllowGroupBy.Yes;
                break;
            case CeC_Campos.Tipo_Datos.Fecha:
                return AllowGroupBy.Yes;
                break;
            case CeC_Campos.Tipo_Datos.Fecha_y_Hora:
                return AllowGroupBy.Yes;
                break;
            case CeC_Campos.Tipo_Datos.Hora:
                return AllowGroupBy.Yes;
                break;
            case CeC_Campos.Tipo_Datos.Imagen:
                return AllowGroupBy.No;
                break;
            case CeC_Campos.Tipo_Datos.Texto:
                return AllowGroupBy.Yes;
                break;
        }
        return AllowGroupBy.No;
    }


    /// <summary>
    /// Asigna el formato a las columnas, las mascaras, ancho,formato
    /// </summary>
    /// <param name="Columna"></param>
    /// <param name="Campo"></param>
    /// <returns></returns>
    private static bool Asigna_Formato_y_Ancho(Infragistics.Web.UI.GridControls.GridField Columna, DS_Campos.EC_CAMPOSRow Campo)
    {
        /* if (Campo.CAMPO_LONGITUD > 0)
             Columna.FieldLen = Convert.ToInt32(Campo.CAMPO_LONGITUD);*/
        if (Campo.MASCARA_ID != 0)
        {
            DS_Campos.EC_MASCARASRow Mascara = CeC_Campos.Obten_Mascara(Campo.MASCARA_ID);
            if (Mascara != null)
            {
                if (((CeC_Campos.Tipo_Datos)(Campo.TIPO_DATO_ID)) != CeC_Campos.Tipo_Datos.Texto)
                {
                    //Columna.Format = Mascara.MASCARA;
                }
                else
                {
                    //Columna.Format = "";
                }
                if (Campo.CAMPO_ANCHO_GRID > 0)
                    Columna.Width = Unit.Pixel(Convert.ToInt32(Campo.CAMPO_ANCHO_GRID));
                else
                    Columna.Width = Unit.Pixel(Convert.ToInt32(Mascara.MASCARA_ANCHO));
                return true;

            }

        }

        switch ((CeC_Campos.Tipo_Datos)(Campo.TIPO_DATO_ID))
        {
            case CeC_Campos.Tipo_Datos.Entero:
                //                Columna.Format = "###,###,###";
                Columna.Width = Unit.Pixel(60);
                break;
            case CeC_Campos.Tipo_Datos.Desconocido:
                //                Columna.Format = "";
                Columna.Width = Unit.Pixel(100);
                break;
            case CeC_Campos.Tipo_Datos.Decimal:
                Columna.Width = Unit.Pixel(60);
                //               Columna.Format = "";
                break;
            case CeC_Campos.Tipo_Datos.Fecha:
                //                Columna.Format = "dd/MM/yy";
                Columna.Width = Unit.Pixel(60);
                break;
            case CeC_Campos.Tipo_Datos.Fecha_y_Hora:
                //                Columna.Format = "dd/MM/yy HH:mm:ss";
                Columna.Width = Unit.Pixel(120);
                break;
            case CeC_Campos.Tipo_Datos.Hora:
                Columna.Width = Unit.Pixel(50);
                //                Columna.Format = "HH:mm:ss";
                break;
            case CeC_Campos.Tipo_Datos.Imagen:
                Columna.Width = Unit.Pixel(120);
                //                Columna.Format = "";
                break;
            case CeC_Campos.Tipo_Datos.Texto:
                Columna.Width = Unit.Pixel(120);
                //               Columna.Format = "";
                break;
            case CeC_Campos.Tipo_Datos.Boleano:
                Columna.Width = Unit.Pixel(20);
                //                Columna.Format = "";
                break;
        }
        if (Campo.CAMPO_ANCHO_GRID > 0)
            Columna.Width = Unit.Pixel(Convert.ToInt32(Campo.CAMPO_ANCHO_GRID));
        return true;
    }


    /// <summary>
    /// Asigna el formato a las columnas, las mascaras, ancho,formato
    /// </summary>
    /// <param name="Columna"></param>
    /// <param name="Campo"></param>
    /// <returns></returns>
    private static bool Asigna_Formato_y_Ancho(Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna, DS_Campos.EC_CAMPOSRow Campo)
    {
        if (Campo.CAMPO_LONGITUD > 0)
            Columna.FieldLen = Convert.ToInt32(Campo.CAMPO_LONGITUD);
        if (Campo.MASCARA_ID != 0)
        {
            DS_Campos.EC_MASCARASRow Mascara = CeC_Campos.Obten_Mascara(Campo.MASCARA_ID);
            if (Mascara != null)
            {
                if (((CeC_Campos.Tipo_Datos)(Campo.TIPO_DATO_ID)) != CeC_Campos.Tipo_Datos.Texto)
                {
                    Columna.Format = Mascara.MASCARA;
                }
                else
                {
                    Columna.Format = "";
                }
                if (Campo.CAMPO_ANCHO_GRID > 0)
                    Columna.Width = Unit.Pixel(Convert.ToInt32(Campo.CAMPO_ANCHO_GRID));
                else
                    Columna.Width = Unit.Pixel(Convert.ToInt32(Mascara.MASCARA_ANCHO));
                return true;

            }

        }

        switch ((CeC_Campos.Tipo_Datos)(Campo.TIPO_DATO_ID))
        {
            case CeC_Campos.Tipo_Datos.Entero:
                Columna.Format = "###,###,###";
                Columna.Width = Unit.Pixel(60);
                break;
            case CeC_Campos.Tipo_Datos.Desconocido:
                Columna.Format = "";
                Columna.Width = Unit.Pixel(100);
                break;
            case CeC_Campos.Tipo_Datos.Decimal:
                Columna.Width = Unit.Pixel(60);
                Columna.Format = "";
                break;
            case CeC_Campos.Tipo_Datos.Fecha:
                Columna.Format = "dd/MM/yy";
                Columna.Width = Unit.Pixel(60);
                break;
            case CeC_Campos.Tipo_Datos.Fecha_y_Hora:
                Columna.Format = "dd/MM/yy HH:mm:ss";
                Columna.Width = Unit.Pixel(120);
                break;
            case CeC_Campos.Tipo_Datos.Hora:
                Columna.Width = Unit.Pixel(50);
                Columna.Format = "HH:mm:ss";
                break;
            case CeC_Campos.Tipo_Datos.Imagen:
                Columna.Width = Unit.Pixel(120);
                Columna.Format = "";
                break;
            case CeC_Campos.Tipo_Datos.Texto:
                Columna.Width = Unit.Pixel(120);
                Columna.Format = "";
                break;
            case CeC_Campos.Tipo_Datos.Boleano:
                Columna.Width = Unit.Pixel(20);
                Columna.Format = "";
                break;
        }
        if (Campo.CAMPO_ANCHO_GRID > 0)
            Columna.Width = Unit.Pixel(Convert.ToInt32(Campo.CAMPO_ANCHO_GRID));
        return true;
    }

    private static bool AplicaFormatoGrafico(Infragistics.Web.UI.GridControls.WebDataGrid Grid)
    {
        //Grid.disp
        return true;
    }

    /// <summary>
    /// Establece el diseño del grid, bordes, color de  fondo,relleno
    /// </summary>
    /// <param name="Grid"></param>
    /// <returns></returns>
    private static bool AplicaFormatoGrafico(Infragistics.WebUI.UltraWebGrid.UltraWebGrid Grid)
    {
        try
        {
            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;
            Grid.BorderColor = Color.Black;
            Grid.BorderStyle = BorderStyle.Solid;
            Grid.DisplayLayout.HeaderStyleDefault.TextOverflow = TextOverflow.Ellipsis;
            //Grid.DisplayLayout.HeaderStyleDefault.Wrap = true;
            //Grid.DisplayLayout.Rows
            foreach (UltraGridColumn Columna in Grid.Columns)
            {
                Columna.CellStyle.TextOverflow = TextOverflow.Ellipsis;
                //Columna.Header.Style.Wrap = true;
                // Columna.CellStyle.Wrap = true;
            }
            Grid.BorderWidth = Unit.Pixel(0);
            Grid.CellPadding = 0;
            Grid.Font.Name = "";
            Grid.Font.Size = 8;
            Grid.ForeColor = Color.FromArgb(0x759AFD);
            Grid.DisplayLayout.StationaryMargins = StationaryMargins.HeaderAndFooter;
            Grid.DisplayLayout.ActivationObject.BorderColor = Color.FromArgb(1, 68, 208);
            Grid.DisplayLayout.ActivationObject.BorderStyle = BorderStyle.Dotted;
            Grid.DisplayLayout.AllowColSizingDefault = AllowSizing.Free;
            Grid.DisplayLayout.GridLinesDefault = UltraGridLines.Both;
            Grid.DisplayLayout.EditCellStyleDefault.BorderStyle = BorderStyle.Dashed;
            Grid.DisplayLayout.EditCellStyleDefault.BorderWidth = 0;
            Grid.DisplayLayout.FilterOptionsDefault.ContainsString = "Contiene";
            Grid.DisplayLayout.FilterOptionsDefault.DoesNotContainString = "No Contiene";
            Grid.DisplayLayout.FilterOptionsDefault.DoesNotEndWithString = "No termina con";
            Grid.DisplayLayout.FilterOptionsDefault.DoesNotStartWithString = "No inicia con";
            Grid.DisplayLayout.FilterOptionsDefault.EndsWithString = "Termina con";
            Grid.DisplayLayout.FilterOptionsDefault.EqualsString = "Igual a";
            Grid.DisplayLayout.FilterOptionsDefault.GreaterThanOrEqualsString = "Mayor o igual a";
            Grid.DisplayLayout.FilterOptionsDefault.GreaterThanString = "Mayor que";
            Grid.DisplayLayout.FilterOptionsDefault.LessThanOrEqualsString = "Menor o igual que";
            Grid.DisplayLayout.FilterOptionsDefault.LessThanString = "Menor que";
            Grid.DisplayLayout.FilterOptionsDefault.LikeString = "Como";
            Grid.DisplayLayout.FilterOptionsDefault.NonEmptyString = "(No vacía)";
            Grid.DisplayLayout.FilterOptionsDefault.NotEqualsString = "Diferente de";
            Grid.DisplayLayout.FilterOptionsDefault.NotLikeString = "No es como";
            Grid.DisplayLayout.FilterOptionsDefault.StartsWithString = "Inicia con";
            Grid.DisplayLayout.FooterStyleDefault.BackColor = Color.LightGray;
            Grid.DisplayLayout.FooterStyleDefault.BorderDetails.ColorLeft = Color.Black;
            Grid.DisplayLayout.FooterStyleDefault.BorderDetails.ColorTop = Color.Black;
            Grid.DisplayLayout.FooterStyleDefault.BorderDetails.WidthLeft = Unit.Pixel(1);
            Grid.DisplayLayout.FooterStyleDefault.BorderDetails.WidthTop = Unit.Pixel(1);
            Grid.DisplayLayout.FooterStyleDefault.BorderStyle = BorderStyle.Solid;
            Grid.DisplayLayout.FooterStyleDefault.BorderWidth = Unit.Pixel(1);
            Grid.DisplayLayout.FrameStyle.BorderColor = Color.Black;
            Grid.DisplayLayout.FrameStyle.BorderStyle = BorderStyle.Solid;
            Grid.DisplayLayout.FrameStyle.BorderWidth = 1;
            Grid.DisplayLayout.FrameStyle.Font.Name = "";
            Grid.DisplayLayout.FrameStyle.Font.Size = 8;
            Grid.DisplayLayout.FrameStyle.ForeColor = Color.FromArgb(0x759AFD);
            Grid.DisplayLayout.FrameStyle.Height = Unit.Percentage(100);
            Grid.DisplayLayout.FrameStyle.Width = Unit.Percentage(100);
            Grid.DisplayLayout.GroupByBox.Prompt = "Arrastre aquí la columna que desea agrupar...";
            Grid.DisplayLayout.GroupByBox.BoxStyle.BackColor = Color.LightSlateGray;
            Grid.DisplayLayout.GroupByBox.BoxStyle.BorderColor = Color.White;
            Grid.DisplayLayout.GroupByBox.BoxStyle.ForeColor = Color.Red;
            Grid.DisplayLayout.GroupByBox.BoxStyle.Height = Unit.Pixel(20);
            //        Grid.DisplayLayout.GroupByBox.sty
            //Grid.DisplayLayout.HeaderStyleDefault.BackColor = Color.FromArgb(0x1560BD);
            Grid.DisplayLayout.HeaderStyleDefault.BackColor = Color.FromArgb(0x143C6D);
            Grid.DisplayLayout.HeaderStyleDefault.BorderColor = Color.Black;
            Grid.DisplayLayout.HeaderStyleDefault.BorderDetails.WidthBottom = Unit.Pixel(1);
            Grid.DisplayLayout.HeaderStyleDefault.BorderDetails.WidthLeft = Unit.Pixel(0);
            Grid.DisplayLayout.HeaderStyleDefault.BorderDetails.WidthRight = Unit.Pixel(1);
            Grid.DisplayLayout.HeaderStyleDefault.BorderDetails.WidthTop = Unit.Pixel(0);
            Grid.DisplayLayout.HeaderStyleDefault.BorderStyle = BorderStyle.Solid;
            //        Grid.DisplayLayout.HeaderStyleDefault.CustomRules = "background-image:url(/ig_common/images/Office2003BlueBG.png);background-repeat:repeat-x;";
            Grid.DisplayLayout.HeaderStyleDefault.BackgroundImage = "GridTitulo.gif";
            Grid.DisplayLayout.HeaderStyleDefault.Font.Name = "";
            Grid.DisplayLayout.HeaderStyleDefault.Font.Size = FontUnit.XSmall;
            Grid.DisplayLayout.HeaderStyleDefault.ForeColor = Color.White;
            Grid.DisplayLayout.HeaderStyleDefault.HorizontalAlign = HorizontalAlign.Left;
            Grid.DisplayLayout.Images.CollapseImage.AlternateText = "Expandido";
            Grid.DisplayLayout.Images.CollapseImage.Url = "ig_tblcrm_rowarrow_down.gif";
            Grid.DisplayLayout.Images.ExpandImage.AlternateText = "Colapsado";
            Grid.DisplayLayout.Images.ExpandImage.Url = "ig_tblcrm_rowarrow_right.gif";
            Grid.DisplayLayout.Pager.PageSize = 20;
            /*  Grid.DisplayLayout.Pager.Style.BackColor = Color.LightSteelBlue;
              Grid.DisplayLayout.Pager.Style.BorderDetails.ColorLeft = Color.White;
              Grid.DisplayLayout.Pager.Style.BorderDetails.ColorTop = Color.White;
              Grid.DisplayLayout.Pager.Style.BorderDetails.WidthLeft = Unit.Pixel(1);
              Grid.DisplayLayout.Pager.Style.BorderDetails.WidthTop = Unit.Pixel(1);
              Grid.DisplayLayout.Pager.Style.BorderStyle = BorderStyle.Solid;*/
            //        Grid.DisplayLayout.RowAlternateStyleDefault.Height = Unit.Pixel(5);
            Grid.DisplayLayout.RowAlternateStyleDefault.BackColor = Color.White;
            Grid.DisplayLayout.RowAlternateStyleDefault.ForeColor = Color.Black;
            Grid.DisplayLayout.RowAlternateStylingDefault = Infragistics.WebUI.Shared.DefaultableBoolean.True;
            Grid.DisplayLayout.RowSizingDefault = AllowSizing.Fixed;
            //Grid.DisplayLayout.RowStyleDefault.Height = Unit.Pixel(5);
            //        Grid.DisplayLayout.RowAlternateStyleDefault.

            //        Grid.DisplayLayout.RowHeightDefault = Unit.Pixel(20);

            //        Grid.DisplayLayout.RowSelectorsDefault = RowSelectors.No;
            Grid.DisplayLayout.RowStyleDefault.BackColor = Color.FromArgb(0xCECEF6);
            //Grid.DisplayLayout.RowStyleDefault.BackColor = Color.Lavender;
            Grid.DisplayLayout.RowStyleDefault.BorderDetails.StyleBottom = BorderStyle.None;
            Grid.DisplayLayout.RowStyleDefault.BorderDetails.WidthLeft = Unit.Pixel(0);
            Grid.DisplayLayout.RowStyleDefault.BorderDetails.WidthTop = Unit.Pixel(0);
            Grid.DisplayLayout.RowStyleDefault.BorderStyle = BorderStyle.Solid;
            Grid.DisplayLayout.RowStyleDefault.BorderWidth = Unit.Pixel(1);
            Grid.DisplayLayout.RowStyleDefault.ForeColor = Color.Black;
            Grid.DisplayLayout.RowStyleDefault.Padding.Left = Unit.Pixel(3);
            Grid.DisplayLayout.RowStyleDefault.Padding.Bottom = Unit.Pixel(0);
            Grid.DisplayLayout.RowStyleDefault.Padding.Right = Unit.Pixel(3);
            Grid.DisplayLayout.RowStyleDefault.Padding.Top = Unit.Pixel(0);
            Grid.DisplayLayout.RowSelectorsDefault = RowSelectors.Yes;
            Grid.DisplayLayout.SelectedRowStyleDefault.BackColor = Color.DodgerBlue;
            Grid.DisplayLayout.SelectedRowStyleDefault.BorderDetails.ColorBottom = Color.Black;
            Grid.DisplayLayout.SelectedRowStyleDefault.BorderDetails.StyleBottom = BorderStyle.Solid;
            Grid.DisplayLayout.SelectedRowStyleDefault.BorderDetails.StyleLeft = BorderStyle.None;
            Grid.DisplayLayout.SelectedRowStyleDefault.BorderDetails.StyleRight = BorderStyle.None;
            Grid.DisplayLayout.SelectedRowStyleDefault.BorderDetails.StyleTop = BorderStyle.None;
            Grid.DisplayLayout.SelectedRowStyleDefault.BorderDetails.WidthBottom = Unit.Pixel(1);
            Grid.DisplayLayout.SelectedRowStyleDefault.BorderDetails.WidthLeft = Unit.Pixel(0);
            Grid.DisplayLayout.SelectedRowStyleDefault.BorderDetails.WidthRight = Unit.Pixel(0);
            Grid.DisplayLayout.SelectedRowStyleDefault.BorderDetails.WidthTop = Unit.Pixel(0);
            Grid.DisplayLayout.SelectedRowStyleDefault.BackgroundImage = "Office2003SelRow.gif";
            Grid.DisplayLayout.StationaryMargins = Infragistics.WebUI.UltraWebGrid.StationaryMargins.Header;

            Grid.DisplayLayout.UseFixedHeaders = true;
            Grid.DisplayLayout.FixedCellStyleDefault.BorderDetails.WidthLeft = Unit.Pixel(0);
            Grid.DisplayLayout.FixedCellStyleDefault.BorderDetails.WidthTop = Unit.Pixel(0);
            Grid.DisplayLayout.FixedCellStyleDefault.BorderStyle = BorderStyle.Solid;
            Grid.DisplayLayout.FixedCellStyleDefault.BorderWidth = Unit.Pixel(1);
            Grid.DisplayLayout.FixedCellStyleDefault.Padding.Left = Unit.Pixel(3);
            Grid.DisplayLayout.FixedCellStyleDefault.Padding.Bottom = Unit.Pixel(0);
            Grid.DisplayLayout.FixedCellStyleDefault.Padding.Right = Unit.Pixel(3);
            Grid.DisplayLayout.FixedCellStyleDefault.Padding.Top = Unit.Pixel(0);

            //        Grid.DisplayLayout.SelectedRowStyleDefault.CustomRules = "background-image:url(/ig_common/images/Office2003SelRow.png);background-repeat:repeat-x;";
            Grid.DisplayLayout.SelectedRowStyleDefault.ForeColor = Color.Black;
            Grid.DisplayLayout.HeaderTitleModeDefault = Infragistics.WebUI.UltraWebGrid.CellTitleMode.Always;
            // Grid.DisplayLayout.SelectTypeRowDefault = SelectType.Extended;
            //        Grid.DisplayLayout.TableLayout = TableLayout.Auto;

            return true;
        }
        catch (StackOverflowException ex)
        {
            CIsLog2.AgregaError("CeC_Grid.AplicaFormatoGrafico", ex);
            return false;
        }
    }
    /// <summary>
    /// Obtiene el valor de la celda
    /// </summary>
    /// <param name="Fila"></param>
    /// <param name="Campo"></param>
    /// <returns></returns>
    public static object ObtenValorCelda(Infragistics.WebUI.UltraWebGrid.UltraGridRow Fila, string Campo)
    {
        if (Fila == null)
            return null;
        UltraGridCell Cell = Fila.Cells.FromKey(Campo);
        if (Cell == null)
            return null;
        return Cell.Value;
    }
    /// <summary>
    /// Aplica el formato al grid y establece si es editable (solo en update)
    /// </summary>
    /// <param name="Grid"></param>
    /// <param name="Agrupar"></param>
    /// <param name="Filtro"></param>
    /// <param name="Multiseleccion"></param>
    /// <param name="Editable"></param>
    /// <returns></returns>
    public static bool AplicaFormato(Infragistics.WebUI.UltraWebGrid.UltraWebGrid Grid, bool Agrupar, bool Filtro, bool Multiseleccion, bool Editable)
    {
        bool Ret = false;
        Ret = AplicaFormato(Grid);
        if (Editable)
        {
            Grid.DisplayLayout.AllowUpdateDefault = AllowUpdate.Yes;
        }
        else
        {
            Grid.DisplayLayout.AllowAddNewDefault = AllowAddNew.No;
            Grid.DisplayLayout.AllowDeleteDefault = AllowDelete.No;
            Grid.DisplayLayout.AllowUpdateDefault = AllowUpdate.No;
        }

        Grid.DisplayLayout.CellClickActionDefault = CellClickAction.RowSelect;
        if (Multiseleccion)
        {
            Grid.DisplayLayout.SelectTypeRowDefault = SelectType.Extended;
        }
        else
        {
            Grid.DisplayLayout.SelectTypeRowDefault = SelectType.Single;
        }
        if (Agrupar)
        {
            Grid.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.OutlookGroupBy;

        }
        else
        {
            Grid.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Flat;
        }
        if (Filtro)
        {
            Grid.DisplayLayout.FilterOptionsDefault.AllowRowFiltering = RowFiltering.OnServer;
            Grid.DisplayLayout.FilterOptionsDefault.FilterUIType = FilterUIType.FilterRow;
            //Grid.DisplayLayout.FilterOptionsDefault.FilterDropDownStyle.CustomRules = 
        }
        else
        {
            Grid.DisplayLayout.FilterOptionsDefault.AllowRowFiltering = RowFiltering.No;
        }
        return Ret;
    }
    /// <summary>
    /// Aplica el formato a columnas campos
    /// </summary>
    /// <param name="Grid"></param>
    /// <returns></returns>
    public static bool AplicaFormato(Infragistics.Web.UI.GridControls.WebDataGrid Grid)
    {
        try
        {
            CeC_Campos.Inicializa();
            Grid.AutoGenerateColumns = false;
            AplicaFormatoGrafico(Grid);
            DataTable DT = (DataTable)Grid.DataSource;
            foreach (DataColumn Columna in DT.Columns)
            {
                DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(Columna.ColumnName);
                Infragistics.Web.UI.GridControls.BoundDataField field = null;
                field = new Infragistics.Web.UI.GridControls.BoundDataField(true);
                if (Campo != null)
                    if (Campo.TIPO_DATO_ID == Convert.ToInt32(CeC_Campos.Tipo_Datos.Boleano))
                    {                       
                        Infragistics.Web.UI.GridControls.BoundCheckBoxField Checkfield = new Infragistics.Web.UI.GridControls.BoundCheckBoxField(true);
                        Checkfield.ValueConverter = new DecimalBooleanConverter();
                        field = Checkfield;
                    }
                
                field.Key = Columna.ColumnName;
                field.DataFieldName = Columna.ColumnName;
                if (Campo != null)
                {
                    field.Header.Text = Campo.CAMPO_ETIQUETA;
                    Asigna_Formato_y_Ancho(field, Campo);
                }
                Grid.Columns.Add(field);
            }
            return true;
        }
        catch { }
        return false;
    }
    /// <summary>
    /// Aplica el formato a columnas campos
    /// </summary>
    /// <param name="Grid"></param>
    /// <returns></returns>
    public static bool AplicaFormato(Infragistics.WebUI.UltraWebGrid.UltraWebGrid Grid)
    {
        try
        {
            CeC_Campos.Inicializa();
            AplicaFormatoGrafico(Grid);
            for (int banda = 0; banda < Grid.Bands.Count; banda++)
            {
                for (int columna = 0; columna < Grid.Bands[banda].Columns.Count; columna++)
                {
                    DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(Grid.Bands[banda].Columns[columna].Key);
                    if (Campo != null)
                    {
                        Grid.Bands[banda].Columns[columna].Header.Caption = Campo.CAMPO_ETIQUETA;
                        //                Grid.Columns[Cont].Width = Unit.Pixel(Convert.ToInt32( Campo.CAMPO_ANCHO_GRID));
                        Grid.Bands[banda].Columns[columna].Type = Tipo(Campo);
                        Grid.Bands[banda].Columns[columna].AllowGroupBy = PermitirAgrupar(Campo);
                        Asigna_Formato_y_Ancho(Grid.Bands[banda].Columns[columna], Campo);
                        if (Grid.Bands[banda].Columns[columna].Type == ColumnType.DropDownList)
                        {
                            DS_Campos.EC_CATALOGOSRow rCatalogo = CeC_Campos.ds_Campos.EC_CATALOGOS.FindByCATALOGO_ID(Campo.CATALOGO_ID);
                            if (rCatalogo != null)
                            {

                                Grid.Bands[banda].Columns[columna].Width = Unit.Pixel(120);
                                //                            Grid.Columns[Cont].Header.Caption = rCatalogo.CATALOGO_NOMBRE;
                                Grid.Bands[banda].Columns[columna].ValueList = Catalogo(Campo, rCatalogo, CeC_Sesion.Nuevo(Grid.Page), false);
                            }
                        }
                        else
                        {
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex.StackTrace.ToString(), ex);
        }
        return false;
    }

    /// <summary>
    /// Establece el formato al webcombo
    /// </summary>
    /// <param name="Combo"></param>
    /// <returns></returns>
    private static bool AplicaFormatoGrafico(Infragistics.WebUI.WebCombo.WebCombo Combo)
    {
        Combo.BorderColor = Color.Black;
        Combo.BorderStyle = BorderStyle.Solid;
        Combo.BorderWidth = Unit.Pixel(1);
        //        Combo.CellPadding = 4;
        Combo.Font.Size = 8;
        Combo.ForeColor = Color.Black;

        /*        Combo.DropDownLayout.ActivationObject.BorderColor = Color.FromArgb(1, 68, 208);
                Combo.DropDownLayout.ActivationObject.BorderStyle = BorderStyle.Dotted;
                Combo.DropDownLayout.AllowColSizingDefault = AllowSizing.Free;
                Combo.DropDownLayout.GridLinesDefault = UltraGridLines.Both;
                Combo.DropDownLayout.EditCellStyleDefault.BorderStyle = BorderStyle.Dashed;
                Combo.DropDownLayout.EditCellStyleDefault.BorderWidth = 0;
                Combo.DropDownLayout.FilterOptionsDefault.ContainsString = "Contiene";
                Combo.DropDownLayout.FilterOptionsDefault.DoesNotContainString = "No Contiene";
                Combo.DropDownLayout.FilterOptionsDefault.DoesNotEndWithString = "No termina con";
                Combo.DropDownLayout.FilterOptionsDefault.DoesNotStartWithString = "No inicia con";
                Combo.DropDownLayout.FilterOptionsDefault.EndsWithString = "Termina con";
                Combo.DropDownLayout.FilterOptionsDefault.EqualsString = "Igual a";
                Combo.DropDownLayout.FilterOptionsDefault.GreaterThanOrEqualsString = "Mayor o igual a";
                Combo.DropDownLayout.FilterOptionsDefault.GreaterThanString = "Mayor que";
                Combo.DropDownLayout.FilterOptionsDefault.LessThanOrEqualsString = "Menor o igual que";
                Combo.DropDownLayout.FilterOptionsDefault.LessThanString = "Menor que";
                Combo.DropDownLayout.FilterOptionsDefault.LikeString = "Como";
                Combo.DropDownLayout.FilterOptionsDefault.NonEmptyString = "(No vacía)";
                Combo.DropDownLayout.FilterOptionsDefault.NotEqualsString = "Diferente de";
                Combo.DropDownLayout.FilterOptionsDefault.NotLikeString = "No es como";
                Combo.DropDownLayout.FilterOptionsDefault.StartsWithString = "Inicia con";
                Combo.DropDownLayout.FooterStyleDefault.BackColor = Color.LightGray;
                Combo.DropDownLayout.FooterStyleDefault.BorderDetails.ColorLeft = Color.White;
                Combo.DropDownLayout.FooterStyleDefault.BorderDetails.ColorTop = Color.White;
                Combo.DropDownLayout.FooterStyleDefault.BorderDetails.WidthLeft = Unit.Pixel(1);
                Combo.DropDownLayout.FooterStyleDefault.BorderDetails.WidthTop = Unit.Pixel(1);
                Combo.DropDownLayout.FooterStyleDefault.BorderStyle = BorderStyle.Solid;
                Combo.DropDownLayout.FooterStyleDefault.BorderWidth = Unit.Pixel(1);*/
        Combo.DropDownLayout.FrameStyle.BorderColor = Color.Black;
        Combo.DropDownLayout.FrameStyle.BorderStyle = BorderStyle.Solid;
        Combo.DropDownLayout.FrameStyle.BorderWidth = 1;

        Combo.DropDownLayout.FrameStyle.Font.Size = 8;
        Combo.DropDownLayout.FrameStyle.ForeColor = Color.FromArgb(0x759AFD);
        //        Combo.DropDownLayout.FrameStyle.Height = Unit.Percentage(100);
        //      Combo.DropDownLayout.FrameStyle.Width = Unit.Percentage(100);
        Combo.DropDownLayout.FrameStyle.BackColor = Color.WhiteSmoke;
        /*        Combo.DropDownLayout.GroupByBox.Prompt = "Arrastre la columna que desea agrupar...";
                Combo.DropDownLayout.GroupByBox.Style.BackColor = Color.LightSteelBlue;
                Combo.DropDownLayout.GroupByBox.Style.BorderColor = Color.White;
                Combo.DropDownLayout.GroupByBox.Style.ForeColor = Color.Navy;*/
        //        Combo.DropDownLayout.SelectedRowStyle
        Combo.DropDownLayout.HeaderStyle.BackColor = Color.FromArgb(153, 176, 217);
        Combo.DropDownLayout.HeaderStyle.BorderColor = Color.Black;
        Combo.DropDownLayout.HeaderStyle.BorderDetails.WidthBottom = Unit.Pixel(1);
        Combo.DropDownLayout.HeaderStyle.BorderDetails.WidthLeft = Unit.Pixel(1);
        Combo.DropDownLayout.HeaderStyle.BorderDetails.WidthRight = Unit.Pixel(1);
        Combo.DropDownLayout.HeaderStyle.BorderDetails.WidthTop = Unit.Pixel(1);
        Combo.DropDownLayout.HeaderStyle.BorderStyle = BorderStyle.Solid;
        //        Combo.DropDownLayout.HeaderStyle.CustomRules = "background-image:url(ig_common/images/Office2003BlueBG.png);background-repeat:repeat-x;";
        Combo.DropDownLayout.HeaderStyle.BackgroundImage = "GridTitulo.gif";

        Combo.DropDownLayout.HeaderStyle.Font.Size = FontUnit.XSmall;
        Combo.DropDownLayout.HeaderStyle.ForeColor = Color.White;
        Combo.DropDownLayout.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
        Combo.DropDownLayout.Images.CollapseImage.AlternateText = "Expandido";
        Combo.DropDownLayout.Images.CollapseImage.Url = "ig_tblcrm_rowarrow_down.gif";
        Combo.DropDownLayout.Images.ExpandImage.AlternateText = "Colapsado";
        Combo.DropDownLayout.Images.ExpandImage.Url = "ig_tblcrm_rowarrow_right.gif";
        Combo.DropDownLayout.Pager.PageSize = 20;
        /*
          Combo.DropDownLayout.Pager.Style.BackColor = Color.LightGray;
          Combo.DropDownLayout.Pager.Style.BorderDetails.ColorLeft = Color.White;
          Combo.DropDownLayout.Pager.Style.BorderDetails.ColorTop = Color.White;
          Combo.DropDownLayout.Pager.Style.BorderDetails.WidthLeft = Unit.Pixel(1);
          Combo.DropDownLayout.Pager.Style.BorderDetails.WidthTop = Unit.Pixel(1);
          Combo.DropDownLayout.Pager.Style.BorderStyle = BorderStyle.Solid;*/
        Combo.DropDownLayout.RowAlternateStyle.BackColor = Color.WhiteSmoke;
        Combo.DropDownLayout.RowAlternateStyle.ForeColor = Color.Black;
        Combo.DropDownLayout.RowAlternateStyling = Infragistics.WebUI.Shared.DefaultableBoolean.True;
        Combo.SelBackColor = Color.DodgerBlue;
        Combo.SelForeColor = Color.Black;
        //        Combo.DropDownLayout.RowAlternateStyleDefault.

        //        Combo.DropDownLayout.RowHeightDefault = Unit.Pixel(20);

        //        Combo.DropDownLayout.RowSelectorsDefault = RowSelectors.No;
        Combo.DropDownLayout.RowStyle.BackColor = Color.Lavender;
        /*      Combo.DropDownLayout.RowStyle.BorderDetails.WidthLeft = Unit.Pixel(0);
              Combo.DropDownLayout.RowStyle.BorderDetails.WidthTop = Unit.Pixel(0);*/
        Combo.DropDownLayout.RowStyle.BorderStyle = BorderStyle.Solid;
        //        Combo.DropDownLayout.RowStyle.BorderWidth = Unit.Pixel(1);
        Combo.DropDownLayout.RowStyle.ForeColor = Color.Black;
        /*        Combo.DropDownLayout.RowStyle.Padding.Left = Unit.Pixel(3);
                Combo.DropDownLayout.RowStyle.Padding.Bottom = Unit.Pixel(0);
                Combo.DropDownLayout.RowStyle.Padding.Right = Unit.Pixel(3);
                Combo.DropDownLayout.RowStyle.Padding.Top = Unit.Pixel(0);
          */
        Combo.DropDownLayout.RowStyle.Font.Size = 8;
        Combo.DropDownLayout.RowSelectors = RowSelectors.Yes;
        Combo.DropDownLayout.SelectedRowStyle.BackColor = Color.DodgerBlue;
        Combo.DropDownLayout.SelectedRowStyle.BorderDetails.ColorBottom = Color.Black;
        Combo.DropDownLayout.SelectedRowStyle.BorderDetails.StyleBottom = BorderStyle.Solid;
        Combo.DropDownLayout.SelectedRowStyle.BorderDetails.StyleLeft = BorderStyle.None;
        Combo.DropDownLayout.SelectedRowStyle.BorderDetails.StyleRight = BorderStyle.None;
        Combo.DropDownLayout.SelectedRowStyle.BorderDetails.StyleTop = BorderStyle.None;
        Combo.DropDownLayout.SelectedRowStyle.BorderDetails.WidthBottom = Unit.Pixel(1);
        Combo.DropDownLayout.SelectedRowStyle.BorderDetails.WidthLeft = Unit.Pixel(0);
        Combo.DropDownLayout.SelectedRowStyle.BorderDetails.WidthRight = Unit.Pixel(0);
        Combo.DropDownLayout.SelectedRowStyle.BorderDetails.WidthTop = Unit.Pixel(0);
        Combo.DropDownLayout.SelectedRowStyle.BackgroundImage = "Office2003SelRow.gif";

        Combo.DropDownLayout.SelectedRowStyle.ForeColor = Color.Black;
        Combo.DropDownLayout.SelectedRowStyle.BackColor = Color.DodgerBlue;
        //Combo.DropDownLayout.s = SelectType.Extended;
        //        Combo.DropDownLayout.TableLayout = TableLayout.Auto;

        return true;
    }
    /// <summary>
    /// Aplica el formato a los combos
    /// </summary>
    /// <param name="Combo"></param>
    /// <returns></returns>
    public static bool AplicaFormato(Infragistics.WebUI.WebCombo.WebCombo Combo)
    {
        CeC_Campos.Inicializa();
        AplicaFormatoGrafico(Combo);
        int AnchoDropDown = 50;

        for (int Cont = 0; Cont < Combo.Columns.Count; Cont++)
        {
            DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(Combo.Columns[Cont].Key);
            if (Campo != null)
            {
                Combo.Columns[Cont].Header.Caption = Campo.CAMPO_ETIQUETA;
                //                Combo.Columns[Cont].Width = Unit.Pixel(Convert.ToInt32( Campo.CAMPO_ANCHO_GRID));
                Combo.Columns[Cont].Type = Tipo(Campo);
                Asigna_Formato_y_Ancho(Combo.Columns[Cont], Campo);
                if (Combo.Columns[Cont].Type == ColumnType.DropDownList)
                {

                    //En el caso de los combos, momentáneamente no mostraran los ID que 
                    //son catálogos, se asumirá que es catalogo
                    /*
                    DS_Campos.EC_CATALOGOSRow rCatalogo = CeC_Campos.ds_Campos.EC_CATALOGOS.FindByCATALOGO_ID(Campo.CATALOGO_ID);
                    if (rCatalogo != null)
                    {
                        Combo.Columns[Cont].Width = Unit.Pixel(120);
                        Combo.Columns[Cont].Header.Caption = rCatalogo.CATALOGO_NOMBRE;
                        Combo.Columns[Cont].ValueList = Catalogo(Campo, rCatalogo);
                    }*/
                    //Si solo hay una columna se asume que se tiene que mostrar
                    if (Combo.Columns.Count > 1)
                        Combo.Columns[0].Hidden = true;
                }
                else
                {
                }
            }
            else
            {
                Combo.Columns[Cont].Width = Combo.DropDownLayout.ColWidthDefault;
            }
            if (Combo.Columns[Cont].Hidden != true)
                AnchoDropDown += Convert.ToInt32(Combo.Columns[Cont].Width.Value);

        }
        if (Combo.Width.Value == 0)
            Combo.Width = Unit.Pixel(200);
        if (Combo.Width.Type == UnitType.Pixel)
            if (Combo.Width.Value < AnchoDropDown)
                Combo.DropDownLayout.DropdownWidth = Unit.Pixel(AnchoDropDown);
            else
                Combo.DropDownLayout.DropdownWidth = Combo.Width;
        //        else
        //          Combo.DropDownLayout.DropdownWidth = Combo.Width;
        Combo.DropDownLayout.DropdownHeight = Unit.Pixel(200);
        return true;

    }


    /// <summary>
    /// Asigna un catálogo a un combo, esta función se deberá llamar en la carga
    /// de la pagina o en la inicialización de datos del combo
    /// </summary>
    /// <param name="Combo">Nombre del control Combo Box de Infragistics</param>
    /// <param name="CATALOGO_C_LLAVE">Campo al que hace referencia este combo, se tomara
    /// dicho campo para buscarlo en el catálogo y ver cual le corresponde</param>
    /// <param name="Sesion">Sesion que contiene todos las variables requeridas para ciertos filtros, puede ser nulo</param>
    /// <returns>Verdadero si se realizo la operación satisfactoriamente</returns>
    public static bool AsignaCatalogo(Infragistics.WebUI.WebCombo.WebCombo Combo, string CATALOGO_C_LLAVE)
    {
        DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(CATALOGO_C_LLAVE);
        if (Campo == null)
            return false;
        if (Campo.CATALOGO_ID > 1)
            return AsignaCatalogo(Combo, Campo.CATALOGO_ID);
        if (Campo.CATALOGO_ID <= 0)
            return false;
        Combo.Editable = true;
        DataSet DS;
        DS = CeC_Campos.CatalogoDT(Campo.CAMPO_NOMBRE, CeC_Sesion.Nuevo(Combo.Page));
        if (DS == null)
            return false;
        CeC_Campos.AplicaFormatoDataset(DS, DS.Tables[0].TableName);
        Combo.DataSource = DS;
        //   Combo.DataMember = DS.Tables[0].TableName;

        Combo.DataTextField = DS.Tables[0].Columns[0].ColumnName;
        //Combo.DisplayValue = DS.Tables[0].Columns[0].ColumnName;
        Combo.DataValueField = DS.Tables[0].Columns[0].ColumnName;
        Combo.DataBind();
        return true;
    }

    /// <summary>
    /// Asigna un catálogo a un combo, esta función se deberá llamar en la carga
    /// de la pagina o en la inicialización de datos del combo
    /// </summary>
    /// <param name="Combo">Nombre del control Combo Box de Infragistics</param>
    /// <param name="CATALOGO_C_LLAVE">Campo al que hace referencia este combo, se tomara
    /// dicho campo para buscarlo en el catálogo y ver cual le corresponde</param>
    /// <param name="Sesion">Sesion que contiene todos las variables requeridas para ciertos filtros, puede ser nulo</param>
    /// <returns>Verdadero si se realizo la operación satisfactoriamente</returns>
    public static bool AsignaCatalogo(Infragistics.Web.UI.ListControls.WebDropDown Combo, string CATALOGO_C_LLAVE)
    {
        DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(CATALOGO_C_LLAVE);
        if (Campo == null)
            return false;
        if (Campo.CATALOGO_ID > 1)
            return AsignaCatalogo(Combo, Campo.CATALOGO_ID);
        if (Campo.CATALOGO_ID <= 0)
            return false;
        //Combo.Editable = true;
        DataSet DS;
        DS = CeC_Campos.CatalogoDT(Campo.CAMPO_NOMBRE, CeC_Sesion.Nuevo(Combo.Page));
        if (DS == null)
            return false;
        CeC_Campos.AplicaFormatoDataset(DS, DS.Tables[0].TableName);
        Combo.DataSource = DS;
        //   Combo.DataMember = DS.Tables[0].TableName;

        Combo.ValueField = DS.Tables[0].Columns[0].ColumnName;
        //Combo.DisplayValue = DS.Tables[0].Columns[0].ColumnName;
        Combo.TextField = DS.Tables[0].Columns[0].ColumnName;
        Combo.DataBind();
        return true;
    }

    /// <summary>
    /// Asigna un catálogo a un combo, esta función se deberá llamar en la carga
    /// de la pagina o en la inicialización de datos del combo
    /// </summary>
    /// <param name="Combo">Combo que se ligara con el conjunto de datos</param>
    /// <param name="CATALOGO_ID">Identificador de catálogo</param>
    /// <returns>Verdadero si se realizo la operación satisfactoriamente</returns>
    public static bool AsignaCatalogo(Infragistics.WebUI.WebCombo.WebCombo Combo, decimal CATALOGO_ID)
    {
        DS_Campos.EC_CATALOGOSRow rCatalogo = CeC_Campos.ds_Campos.EC_CATALOGOS.FindByCATALOGO_ID(CATALOGO_ID);
        if (rCatalogo == null)
            return false;

        DataSet DS;
        if (rCatalogo.CATALOGO_ID == 1)
            return false;
        else
            DS = CeC_Campos.CatalogoDT(rCatalogo, CeC_Sesion.Nuevo(Combo.Page));
        if (DS == null)
            return false;
        CeC_Campos.AplicaFormatoDataset(DS, DS.Tables[0].TableName);
        Combo.DataSource = DS;
        //        Combo.DataMember = DS.Tables[0].TableName;
        if (DS.Tables[0].Columns.Count > 1)
        {
            Combo.DataTextField = DS.Tables[0].Columns[1].ColumnName;
            //            Combo.DisplayValue = DS.Tables[0].Columns[1].ColumnName;
        }
        else
        {
            Combo.DataTextField = DS.Tables[0].Columns[0].ColumnName;
            //            Combo.DisplayValue = DS.Tables[0].Columns[0].ColumnName;
        }
        Combo.DataValueField = DS.Tables[0].Columns[0].ColumnName;
        //Combo.DataTextField = DS.Tables[0].Columns[0].ColumnName;
        //Combo.DisplayValue = DS.Tables[0].Columns[0].ColumnName;
        Combo.DataBind();
        return true;
    }

    /// <summary>
    /// Asigna un catálogo a un combo, esta función se deberá llamar en la carga
    /// de la pagina o en la inicialización de datos del combo
    /// </summary>
    /// <param name="Combo">Combo que se ligara con el conjunto de datos</param>
    /// <param name="CATALOGO_ID">Identificador de catálogo</param>
    /// <returns>Verdadero si se realizo la operación satisfactoriamente</returns>
    public static bool AsignaCatalogo(Infragistics.Web.UI.ListControls.WebDropDown Combo, decimal CATALOGO_ID)
    {
        DS_Campos.EC_CATALOGOSRow rCatalogo = CeC_Campos.ds_Campos.EC_CATALOGOS.FindByCATALOGO_ID(CATALOGO_ID);
        if (rCatalogo == null)
            return false;

        DataSet DS;
        if (rCatalogo.CATALOGO_ID == 1)
            return false;
        else
            DS = CeC_Campos.CatalogoDT(rCatalogo, CeC_Sesion.Nuevo(Combo.Page));
        if (DS == null)
            return false;
        CeC_Campos.AplicaFormatoDataset(DS, DS.Tables[0].TableName);
        Combo.DataSource = DS;
        //        Combo.DataMember = DS.Tables[0].TableName;
        if (DS.Tables[0].Columns.Count > 1)
        {
            Combo.TextField = DS.Tables[0].Columns[1].ColumnName;
            //            Combo.DisplayValue = DS.Tables[0].Columns[1].ColumnName;
        }
        else
        {
            Combo.TextField = DS.Tables[0].Columns[0].ColumnName;
            //            Combo.DisplayValue = DS.Tables[0].Columns[0].ColumnName;
        }
        Combo.ValueField = DS.Tables[0].Columns[0].ColumnName;
        //Combo.DataTextField = DS.Tables[0].Columns[0].ColumnName;
        //Combo.DisplayValue = DS.Tables[0].Columns[0].ColumnName;
        Combo.DataBind();
        return true;
    }

    /// <summary>
    /// Selecciona el elemento que contiene el ID especificado
    /// </summary>
    /// <param name="Combo">Control combo que se seleccionara el elemento</param>
    /// <param name="ID">Identificador</param>
    /// <returns></returns>
    /// 
    public static DS_Campos ds_Campos
    {
        get
        {
            Inicializa();
            return s_ds_Campos;
        }

    }
    /// <summary>
    /// Inicializa los campos
    /// </summary>
    /// <returns></returns>
    public static bool Inicializa()
    {
        if (s_ds_Campos != null)
            return false;
        CeC_Campos.ChecaCampos_TE();
        CeC_Campos Clase = new CeC_Campos();
        Clase.InicializaCampos();
        s_ds_Campos = Clase.m_ds_Campos;
        return true;
    }
    public static int RegresaValorInt(Infragistics.WebUI.WebCombo.WebCombo Combo, int Predeterminado)
    {
        return CeC.Convierte2Int(Combo, Predeterminado);
    }

    /// <summary>
    /// Regresa el valor seleccionado del combo
    /// </summary>
    /// <param name="Combo"></param>
    /// <returns></returns>
    public static object RegresaValor(Infragistics.WebUI.WebCombo.WebCombo Combo)
    {
        try
        {
            if (Combo.DataValueField != null && Combo.DataValue != null)
                return Combo.DataValue;
            if (Combo.DisplayValue != null)
                return Combo.DisplayValue;
            if ((Combo.SelectedCell.Text != null || Combo.SelectedCell.Text != "") || (Combo.DataValueField == null && Combo.DataValue == null))
                return Combo.SelectedCell.Text;
            return Combo.DataValue;
        }
        catch (Exception ex)
        {
            return null;
        }
    }



    /// <summary>
    /// Selecciona el elemento en el combobox que esta guardado en la base de datos
    /// </summary>
    /// <param name="Combo"></param>
    /// <param name="ID"></param>
    /// <returns></returns>
    public static bool SeleccionaID(Infragistics.WebUI.WebCombo.WebCombo Combo, object ID)
    {
        try
        {
            int columna = -1;
            for (int i = 0; i < Combo.Columns.Count; i++)
            {
                if (Combo.Columns[i].Header.Key == Combo.DataValueField)
                {
                    columna = i;
                    break;
                }
            }
            if (columna > -1)
            {
                for (int i = 0; i < Combo.Rows.Count; i++)
                {
                    if ((Combo.Rows[i].Cells[columna].Value).ToString() == (ID).ToString())
                    {
                        Combo.SelectedIndex = i;
                        return true;
                    }

                }
                return true;
            }
            return true;
        }
        catch (Exception ex)
        {
            Combo.SelectedIndex = -1;
            return false;
        }
    }

    /********************************************************************
            Creado:	13:2:2006   19:57
            Nombre de Función: 	ActualizaRowBatch
            Autor:	Moises Trejo
            Parámetros:
            Propósito:	Esta funciona se deberá ejecutar siempre que se quieran guardar registros en forma batch 
        cuado sea una tabla simple, 
        *********************************************************************/
    public static string ActualizaRowBatch(Infragistics.WebUI.UltraWebGrid.UltraWebGrid Grid, Infragistics.WebUI.UltraWebGrid.RowEventArgs e, System.Data.DataSet DS, string Tabla, string ID)
    {
        string Retorno = "";
        try
        {
            if (e.Data.GetType().Name != "UltraGridRow")
            {
            }

            // The input argument e.Data contains the data before the modification is made. 
            Infragistics.WebUI.UltraWebGrid.UltraGridRow oldRow = (Infragistics.WebUI.UltraWebGrid.UltraGridRow)e.Data;

            // The update row event argument e.Row contains the value of the row as it was returned to the server.  With this information
            // it would be possible to begin updating the row object in the DataTable

            // The DataKey of a row is designed to hold the primary key information for the row object.  This value is taken from the DataKeyField set in the 
            // InitializeLayout event.
            // In this case, the DataKeyField is the CompanyID, a string value
            string dataKey = "";
            if (e.Row.DataKey != null)
                dataKey = (string)e.Row.DataKey.ToString();


            switch (e.Row.DataChanged)
            {
                // In the case of an added row, no row will be found in the underlying datasource to modify, so a new row object needs to be made to be added to the collection
                case (Infragistics.WebUI.UltraWebGrid.DataChanged.Added):
                    // make a new DataRow object
                    System.Data.DataRow newDataTableRow = DS.Tables[Tabla].NewRow();

                    // populate the DataRow object with values from the UltraGridRow object
                    // When using a bound grid, the UltraGridColumn.Key value is set to the name of the column from the bound source.
                    // Microsoft DataRow objects are indexable by their column name, so we can leverage this to quickly loop through
                    // all the UltraGridCells and move their values into the DataRow object.  There are other techniques and methodologies that
                    // can be followed should this not be robust enough.
                    foreach (Infragistics.WebUI.UltraWebGrid.UltraGridCell c in e.Row.Cells)
                        if (c.Value != null)
                            newDataTableRow[c.Column.Key] = c.Value;
                    if (newDataTableRow[0] == null)
                    {
                        newDataTableRow[0] = CeC_Autonumerico.GeneraAutonumerico(Tabla, ID);
                    }

                    // Once the DataRow object is populated, add the row to the DataTable object
                    DS.Tables[Tabla].Rows.Add(newDataTableRow);

                    // In this case, the CompanyID is a text string that would have to be populated by user.  However there are cases
                    // where the value for the DataKey of a row would not be known at this present moment.  These cases include when the primary key for 
                    // a row would be generated by the database server via an autoincrementing value or some other calculated manner.  In these cases, the following 
                    // code would need to be moved to a point after the DataAdapter.Update (or a corresponding moment if using a custom business object)

                    // Since we know the DataKey at this time, we assign it to the UltraGridRow object.
                    e.Row.DataKey = e.Row.Cells.FromKey(ID).Value;

                    break;
                case (Infragistics.WebUI.UltraWebGrid.DataChanged.Modified):
                    // in the case of an updated row, we find the row in a manner similar to how we want to find a row for deletion,
                    // except instead of removing the row we update the cell values for the row
                    if (dataKey.Length > 0)
                    {
                        // First we locate the row in the DataTable object with the corresponding key.                        
                        System.Data.DataRow dr = DS.Tables[Tabla].Rows.Find(new object[] { dataKey });

                        if (null != dr)
                            // The update code is similar to that used inside of the Add case.
                            foreach (Infragistics.WebUI.UltraWebGrid.UltraGridCell c in e.Row.Cells)
                                if (!dr[c.Column.Key].Equals(c.Value)) // one can check to see if the cell's .DataChanged propery is set to true
                                    if (c.Value != null)
                                        dr[c.Column.Key] = c.Value;

                        // Normally you would not allow modification of the value that is used to populate your DataKey as this most
                        // often corresponds to your primary key.  However, should you allow for the field that is used for the DataKey, you should
                        // update this value.                        
                        e.Row.DataKey = e.Row.Cells.FromKey(ID).Value;
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            Retorno = ex.Message;
            CIsLog2.AgregaError(ex);
        }
        return Retorno;
    }

    public static bool ExportarExcel(DataSet DS)//Infragistics.WebUI.UltraWebGrid.UltraWebGrid Gr)
    {
        /*    Infragistics.Excel.Workbook hojaCalc = new Infragistics.Excel.Workbook();
            Infragistics.WebUI.UltraWebGrid.UltraWebGrid Gr = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
            Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter Exporter = new Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter();
            try
            {
                Exporter.ExcelStartColumn = "A";
                Exporter.ExcelStartRow = 1;
                Exporter.TypeCoercion = Infragistics.WebUI.UltraWebGrid.ExcelExport.TypeCoercionMode.Default;
                Exporter.ExportMode = Infragistics.WebUI.UltraWebGrid.ExcelExport.ExportMode.Download;
                Exporter.DownloadName = "Consulta.XLS";
                Exporter.WorksheetName = "ConsultaSQL";
                Exporter.EnableViewState = true;
                hojaCalc.Worksheets.Add("ConsultaSQL");
                Gr.DataSource = DS;
                Gr.DataBind();
                AplicaFormato(Gr, false, false, false, false);

                Gr.DisplayLayout.ColHeadersVisibleDefault = Infragistics.WebUI.UltraWebGrid.ShowMarginInfo.Yes;
                Exporter.Export(Gr, hojaCalc);
                hojaCalc.ActiveWorksheet = hojaCalc.Worksheets[0];
                return true;
            }
            catch
            {
                return false;
            }*/
        return false;
    }

    public static void SumaTotales(Infragistics.WebUI.UltraWebGrid.UltraWebGrid Grid)
    {
        try
        {
            CeC_Grid.AplicaFormato(Grid, true, false, true, false);
            // Totales para Reportes de Comida
            for (int banda = 0; banda < Grid.Bands.Count; banda++)
            {
                for (int columna = 0; columna < Grid.Bands[banda].Columns.Count; columna++)
                {
                    DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(Grid.Bands[banda].Columns[columna].Key);
                    if (Campo != null)
                    {
                        Grid.Bands[banda].Columns[columna].Header.Caption = Campo.CAMPO_ETIQUETA;
                        //                Grid.Columns[Cont].Width = Unit.Pixel(Convert.ToInt32( Campo.CAMPO_ANCHO_GRID));
                        Grid.Bands[banda].Columns[columna].Type = Tipo(Campo);
                        Grid.Bands[banda].Columns[columna].AllowGroupBy = PermitirAgrupar(Campo);
                        Asigna_Formato_y_Ancho(Grid.Bands[banda].Columns[columna], Campo);
                        // Obtiene el toral de las horas para las columans de horas trabajadas, retardos, etc
                        switch (Grid.Bands[banda].Columns[columna].Header.Caption)
                        {
                            case "TOTAL_PRIMERA_COMIDA_COSTO":
                            case "Costo Primeras Comidas":
                            case "TOTAL_PRIMERA_COMIDA":
                            case "No. Primeras Comidas":
                            case "TOTAL_SEGUNDA_COMIDA_COSTO":
                            case "Costo Segundas Comidas":
                            case "TOTAL_SEGUNDA_COMIDA":
                            case "No. Segundas Comidas":
                            case "TOTAL_TIPO_COMIDA":
                            case "No. Comidas":
                            case "TOTAL_COMIDA_COSTO":
                            case "Total Costo Comida":
                            case "TIPO_COMIDA_COSTOA":
                            case "Tipo Comida Costo":
                            case "NUMERO_COMIDAS":
                            case "PRECIO":
                            case "Precio":
                            case "MONEDERO_SALDO":
                            case "Saldo":
                            case "MONEDERO_CONSUMO":
                            case "Consumo":
                            case "ABONO":
                            case "Abono":
                            case "SUBTOTAL_CONSUMO_EMPRESA":
                            case "Sub-Total Consumo":
                            case "SUBTOTAL_ABONO_EMPRESA":
                            case "Sub-Total Abono":
                            case "TOTAL_CONSUMO":
                            case "Total Consumo":
                            case "TOTAL_ABONO":
                            case "Total Abono":
                                try
                                {
                                    Grid.DisplayLayout.Bands[banda].Columns[columna].Footer.Total = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
                                    Grid.Columns[columna].FooterStyleResolved.ForeColor = Color.Black;
                                    Grid.Bands[banda].Columns[1].FooterText = "Totales :";
                                }
                                catch (Exception ex)
                                {
                                    CIsLog2.AgregaError(ex.StackTrace.ToString(), ex);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            Grid.DisplayLayout.ColWidthDefault = new Unit("150px");
            Grid.DisplayLayout.RowStyleDefault.HorizontalAlign = HorizontalAlign.Right;
            Grid.DisplayLayout.HeaderStyleDefault.HorizontalAlign = HorizontalAlign.Center;
            Grid.DisplayLayout.ColFootersVisibleDefault = ShowMarginInfo.Yes;
            Grid.DisplayLayout.FooterStyleDefault.ForeColor = Color.Black;

            Infragistics.WebUI.UltraWebGrid.UltraGridLayout addNew = Grid.DisplayLayout;
            addNew.HeaderStyleDefault.ForeColor = Color.White;
            addNew.FooterStyleDefault.ForeColor = Color.Black;
            addNew.AllowAddNewDefault = AllowAddNew.Yes;
            addNew.AddNewBox.Hidden = true;
            addNew.FrameStyle.BorderStyle = BorderStyle.Solid;
            addNew.FrameStyle.BorderWidth = new Unit("1px");
            addNew.FrameStyle.BorderColor = Color.FromArgb(51, 51, 51);
            addNew.GroupByBox.BoxStyle.BackColor = System.Drawing.Color.LightGray;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("CeC_Grid.SumaTotales", ex);
        }
    }

    /// <summary>
    /// Obtiene la suma de horas y minutos en las columnas Tiempo Trabajado, Tiempo de Retardo, Tiempo de Estancia, etc.
    /// </summary>
    /// <param name="Grid">Grid con los datos</param>
    /// <param name="banda">Variable contador para las columnas</param>
    /// <param name="contador"></param>
    public static void HorasSuma(Infragistics.WebUI.UltraWebGrid.UltraWebGrid Grid)
    {
        try
        {
            //CeC_Grid.AplicaFormato(Grid, true, false, true, false);
            // Totales para Reportes de Comida
            for (int banda = 0; banda < Grid.Bands.Count; banda++)
            {
                for (int columna = 0; columna < Grid.Bands[banda].Columns.Count; columna++)
                {
                    DS_Campos.EC_CAMPOSRow Campo = CeC_Campos.Obten_Campo(Grid.Bands[banda].Columns[columna].Key);
                    if (Campo != null)
                    {
                        Grid.Bands[banda].Columns[columna].Header.Caption = Campo.CAMPO_ETIQUETA;
                        Grid.Bands[banda].Columns[columna].Type = Tipo(Campo);
                        Grid.Bands[banda].Columns[columna].AllowGroupBy = PermitirAgrupar(Campo);
                        Asigna_Formato_y_Ancho(Grid.Bands[banda].Columns[columna], Campo);
                        // Obtiene el toral de las horas para las columans de horas trabajadas, retardos, etc
                        switch (Grid.Bands[banda].Columns[columna].Header.Caption)
                        {
                            case "PERSONA_DIARIO_TDE":
                            case "Tiempo de Retardo":
                            case "PERSONA_DIARIO_TE":
                            case "Tiempo de Estancia":
                            case "PERSONA_DIARIO_TC":
                            case "Tiempo de Comida":
                            case "PERSONA_DIARIO_TES":
                            case "Horas X Trabajar":
                            case "PERSONA_DIARIO_TT":
                            case "Tiempo Trabajado":
                            case "PERSONA_D_HE_SIS":
                            case "HE Reales":
                            case "PERSONA_D_HE_CAL":
                            case "HE Calculadas":
                            case "PERSONA_D_HE_APL":
                            case "HE a Aplicar":
                            case "PERSONA_D_HE_SIS_A":
                            case "PERSONA_D_HE_SIS_D":
                            case "PERSONA_D_HE_FH":
                            case "Confirmacion HE":
                            case "PERSONA_D_HE_SIMPLE":
                            case "HE Simples":
                            case "PERSONA_D_HE_DOBLE":
                            case "HE Dobles":
                            case "PERSONA_D_HE_TRIPLE":
                            case "HE Triples":
                                try
                                {
                                    DateTime horaSuma = CeC_BD.FechaNula;
                                    int horas = 0;
                                    int minutos = 0, auxMinutos = 0, modMinutos = 0;
                                    int segundos = 0, auxSegundos = 0, modSegundos = 0;
                                    foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow Fila in Grid.Rows)
                                    {
                                        DateTime tiempo = CeC.Convierte2DateTime(Fila.Cells[columna].Value);
                                        horas += tiempo.Hour;
                                        minutos += tiempo.Minute;
                                        segundos += tiempo.Second;
                                        // Sumamos minutos y segundos para que el formato sea entendible.
                                        if (minutos > 60)
                                        {
                                            auxMinutos = minutos / 60;
                                            modMinutos = minutos % 60;
                                            minutos = modMinutos;
                                            horas += auxMinutos;
                                        }
                                        if (segundos > 60)
                                        {
                                            auxSegundos = segundos / 60;
                                            modSegundos = segundos % 60;
                                            segundos = modSegundos;
                                            minutos += auxSegundos;
                                        }
                                        Grid.Columns[columna].Format = "HH:mm:ss";
                                        Grid.Bands[banda].Columns[columna].FooterText = horas + ":" + minutos + ":" + segundos;
                                        Grid.Columns[columna].FooterStyleResolved.ForeColor = Color.Black;
                                        Grid.Bands[banda].Columns[1].FooterText = "Totales :";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    CIsLog2.AgregaError(ex.StackTrace.ToString(), ex);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            Grid.DisplayLayout.ColWidthDefault = new Unit("150px");
            Grid.DisplayLayout.RowStyleDefault.HorizontalAlign = HorizontalAlign.Right;
            Grid.DisplayLayout.HeaderStyleDefault.HorizontalAlign = HorizontalAlign.Center;
            Grid.DisplayLayout.ColFootersVisibleDefault = ShowMarginInfo.Yes;
            Grid.DisplayLayout.FooterStyleDefault.ForeColor = Color.Black;

            Infragistics.WebUI.UltraWebGrid.UltraGridLayout addNew = Grid.DisplayLayout;
            addNew.HeaderStyleDefault.ForeColor = Color.White;
            addNew.FooterStyleDefault.ForeColor = Color.Black;
            addNew.AllowAddNewDefault = AllowAddNew.Yes;
            addNew.AddNewBox.Hidden = true;
            addNew.FrameStyle.BorderStyle = BorderStyle.Solid;
            addNew.FrameStyle.BorderWidth = new Unit("1px");
            addNew.FrameStyle.BorderColor = Color.FromArgb(51, 51, 51);
            addNew.GroupByBox.BoxStyle.BackColor = System.Drawing.Color.LightGray;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("CeC_Grid.HorasSuma", ex);
        }
    }
}

public class DecimalBooleanConverter : Infragistics.Web.UI.GridControls.IBooleanConverter
{

    public object DefaultFalseValue
    {
        get { return 0.0m; }
    }

    public object DefaultTrueValue
    {
        get { return 1m; }
    }

    public bool IsFalse(object value)
    {
        if (value == null)
            return true;

        return (decimal)value == 0m;
    }

    public bool IsTrue(object value)
    {
        if (value == null)
            return false;

        return (decimal)value != 0m;
    }
}