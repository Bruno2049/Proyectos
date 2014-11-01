<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Site.Master" CodeBehind="CreditRequest.aspx.cs"
    Inherits="PAEEEM.CreditRequest" Title="Alta de Solicitud de Crédito" MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/PageMask.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
        .blue
        {
            padding-top: 5px;
            vertical-align: middle;
        }
        .Label
        {
            width: 20%;
            color: #333333;
        }
        .titulo
        {
            color: #FFFFFF;
            font-size: 16px;
            background-image: url(   '../Resources/Images/tabla.png' );
            background-repeat: repeat-x;
            width: 100%;
            height: 100%;
            border: 0px;
        }
        .Label_1
        {
            width: 45%;
            color: #333333;
        }
        .Label_2
        {
            width: 10%;
            color: #333333;
        }
        .TextBox
        {
            width: 25%;
            text-transform:uppercase;
        }
        .TextBox_1
        {
            width: 12.5%;
            text-transform:uppercase;
        }
        .TextBox_gdvc
        {
            width: 100%;
        }
        .TextBox_gdv
        {
            width: 100%;
        }
        .DropDownList
        {
            width: 25%;
        }
        .DropDownList_gdv
        {
            width: 100%;
        }
        .Button
        {
            width: 80px;
        }
        .RadioButton
        {
            width: 12.5%;
            color: Maroon;
        }
        .CheckBox
        {
            width: 25%;
            color: Maroon;
        }
    </style>

    <script type="text/javascript">
        function lockScreen() {
            var lock = document.getElementById('lock');        
             lock.style.width = '300px';
            lock.style.height = '30px';        
            // lock.style.top = document.documentElement.clientHeight/2 - lock.style.height.replace('px','')/2 + 'px';
            lock.style.top = document.body.offsetHeight/1.5 - lock.style.height.replace('px','')/2 + 'px';
            lock.style.left = document.body.offsetWidth/2 - lock.style.width.replace('px','')/2 + 'px';
            if (lock)
                lock.className = 'LockOn'; 
        }
           function lockScreen1() {
            var lock = document.getElementById('lock');        
             lock.style.width = '300px';
            lock.style.height = '30px';        
            // lock.style.top = document.documentElement.clientHeight/2 - lock.style.height.replace('px','')/2 + 'px';
            lock.style.top = document.body.offsetHeight/5 * 4 + 'px';
            lock.style.left = document.body.offsetWidth/2 - lock.style.width.replace('px','')/2 + 'px';
            if (lock)
                lock.className = 'LockOn';
        }
        function CapitalASCII(obj) {
            var valor = obj.value.toUpperCase();
            valor = valor.replace("Á", "A", "gmi");
            valor = valor.replace("É", "E", "gmi");
            valor = valor.replace("Í", "I", "gmi");
            valor = valor.replace("Ó", "O", "gmi");
            valor = valor.replace("Ú", "U", "gmi");
            // RSA 20131008 allow ñ
            // valor = valor.replace("Ñ", "N", "gmi");
            if (obj.value != valor)
                obj.value = valor;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="lock" class="LockOff">
                <img src="../images/progress.gif" alt="¡ En Proceso, Por favor Espere !" style="vertical-align: middle;
                    position: relative;" />
            </div>
            <asp:Wizard ID="wizardPages" runat="server" DisplaySideBar="false" Style="width: 100%"
                StartNextButtonText="Siguiente" OnNextButtonClick="wizardPages_NextButtonClick"
                ActiveStepIndex="0" StepNextButtonText="Siguiente" StepPreviousButtonText="Regresar"
                FinishCompleteButtonText="Guardar" FinishPreviousButtonText="Regresar" 
                OnFinishButtonClick="wizardPages_FinishButtonClick">
                <FinishNavigationTemplate>
                    <asp:Button ID="btnFinishPre" runat="server" CssClass="Button" Text="Regresar" CommandName="MovePrevious"
                        OnClientClick="return confirm('¿ Desea Regresar a la Pantalla Previa ?');" />
                    <asp:Button ID="btnFinishCom" runat="server" CssClass="Button" Text="Guardar" CommandName="MoveComplete" 
                        OnClientClick="this.style.display='none'; return true;"/>
                    <asp:Button ID="btnCancel3" runat="server" CssClass="Button" Text="Salir" OnClick="btnCancel3_Click"
                        OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" />
                </FinishNavigationTemplate>
                <StartNavigationTemplate>
                    <div align="right">
                        <asp:Button ID="btnStartNext" runat="server" Text="Siguiente" CssClass="Button" CommandName="MoveNext"
                            OnClientClick="return confirm('¿ Desea Continuar con el Registro de la Solicitud de Crédito ?');" />
                        <asp:Button ID="btnSalir" Text="Salir" runat="server" CssClass="Button" OnClick="btnSalir_Click"
                            OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" />
                    </div>
                </StartNavigationTemplate>
                <WizardSteps>
                    <asp:WizardStep runat="server" ID="wizCreditRequest" StepType="Start">
                        <div id="divCreditRequest">
                            <div>
                                <br>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Image runat="server" ImageUrl="images/t_alta.png" ID="imgAlta1"/>
                                                <asp:Image runat="server" ImageUrl="images/t_edicioncredito.png" ID="imgEdit1" Visible="false"/>
                                                <asp:Label runat="server" Text="ALTA DE SOLICITUD DE CREDITO" CssClass="Label_1"
                                                    ID="lblTitle" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div align="left">
                                                    <asp:Label ID="lblCredito1" Text="No. Crédito" runat="server" CssClass="Label" Width="165px" ForeColor="#333333" Visible="False"/>
                                                    <asp:TextBox ID="txtCredito1" runat="server" CssClass="TextBox" Width="250px" Enabled="false" Visible="False"/>
                                                </div>
                                            </td>
                                            <td align="right">
                                                <asp:Label runat="server" Text="Fecha" CssClass="Label_2" ID="lblDate"></asp:Label>
                                                <asp:Label runat="server" ID="lbbNowdate"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div>
                                                    <asp:Panel runat="server" ID="pnl1">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <br />
                                                                        <asp:Image runat="server" ImageUrl="images/t1.png" /></div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:UpdatePanel ID="firstSection" runat="server">
                                                            <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 155px">
                                                                    <div>
                                                                        <asp:Label runat="server" Text="P. Física/P. Moral (*)" CssClass="Label" Font-Size="11pt"
                                                                            Width="155px" ID="lblDX_TIPO_SOCIEDAD"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:DropDownList runat="server" AutoPostBack="True" CssClass="DropDownList" Font-Size="11px"
                                                                            Width="250px" ID="ddlDX_TIPO_SOCIEDAD" OnSelectedIndexChanged="ddlDX_TIPO_SOCIEDAD_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDX_TIPO_SOCIEDAD"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator8">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td width="160px">
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Nombre (*)" CssClass="Label_2" Font-Size="11pt" Width="150px"
                                                                            ID="lblName"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td style="width: 250px">
                                                                    <div>
                                                                        <asp:TextBox runat="server" CssClass="TextBox_1" Font-Size="11px" ToolTip="(Campo Obligatorio)"
                                                                            Width="250px" ID="txtName"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ErrorMessage="(*) Campo Obligatorio"
                                                                            ValidationGroup="Siguiente" ID="Required">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="150px">
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Apellido Paterno P. Física (*)" CssClass="Label_2" Font-Size="11pt"
                                                                            Width="150px" ID="lblLastname"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td width="250px">
                                                                    <div>
                                                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                            ToolTip="( Campo Obligatorio)" Width="250px" ID="txtLastname"></asp:TextBox>
                                                                        <asp:CheckBox runat="server" AutoPostBack="True" Text="Marcar si NO cuenta con Apellido Paterno"
                                                                            Font-Size="11px" ForeColor="#333333" Width="250px" ID="ckbLastname"
                                                                            OnCheckedChanged="ckbLastname_CheckedChanged"/>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastname" ErrorMessage="(*) Campo Obligatorio"
                                                                            ValidationGroup="Siguiente" ID="reqTxtLastName">
                                                                        </asp:RequiredFieldValidator>
                                                                </td>
                                                                <td width="150px">
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Apellido Materno P. Física (*)" CssClass="Label_2" Font-Size="11pt"
                                                                            Width="150px" ID="lblMotherName"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                            ToolTip="(Campo Obligatorio)" Width="250px" ID="txtMotherName"></asp:TextBox>
                                                                        <asp:CheckBox runat="server" AutoPostBack="True" Text="Marcar si NO cuenta con Apellido Materno"
                                                                            Font-Size="11px" ForeColor="#333333" Width="250px" ID="ckbMothername"
                                                                            OnCheckedChanged="ckbMothername_CheckedChanged"/>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMotherName" ErrorMessage="(*) Campo Obligatorio"
                                                                            ValidationGroup="Siguiente" ID="reqTxtMotherName">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="153px">
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Fecha de Nacimiento (*)" CssClass="Label" Font-Size="11pt"
                                                                            Width="160px" ID="lblBirthDate"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                            ToolTip="(Formato de Fecha AAAA-MM-DD)" Width="250px" ID="txtBirthDate"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px"
                                                                            ControlToValidate="txtBirthDate" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                                                            ID="revTxtBirthDate">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBirthDate"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="rfvTxtBirthDate">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td width="150px">
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Giro de la Empresa(*)" CssClass="Label" Font-Size="11pt"
                                                                            Width="150px" ID="lblDX_TIPO_INDUSTRIA"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:DropDownList runat="server" ValidationGroup="Siguiente" CssClass="DropDownList"
                                                                            Font-Size="11px" ToolTip="(Campo Obligatorio)" Width="250px" ID="ddlDX_TIPO_INDUSTRIA">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDX_TIPO_INDUSTRIA"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator9">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                                    <%--  add by coco 2012-07-16--%>
                                                                    <tr>
                                                                        <td width="153px">
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Teléfono (*)" CssClass="Label" Font-Size="11pt" Width="160px"
                                                                                    ID="lblTelephone"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:TextBox runat="server" MaxLength="10" CssClass="TextBox_1" Font-Size="11px"
                                                                                    ToolTip="(10 Dígitos Numéricos)" Width="250px" ID="txtTelephone"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^((\d{10}))$" Height="0px"
                                                                                    ControlToValidate="txtTelephone" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                                                                    ID="RegularExpressionValidatorTelephone">
                                                                                </asp:RegularExpressionValidator>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTelephone"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="rfvTxtTelephone">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                        <td width="150px">
                                                                            <div>
                                                                                <asp:Label runat="server" Text="E-mail (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                                    ID="lblEmail"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                                    Width="250px" ID="txtEmail"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^(([a-zA-Z0-9_\-\.ñáéíóúü]+)@([a-zA-Z0-9_\-\.ñáéíóúü]+)\.([a-zA-Zñáéíóúü]{2,5}){1,25})+([;.](([a-zA-Zñáéíóúü0-9_\-\.]+)@([a-zA-Zñáéíóúü0-9_\-\.]+)\.([a-zA-Zñáéíóúü]{2,5}){1,25})+)*$" Height="0px"
                                                                                    ControlToValidate="txtEmail" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RegularExpressionValidatorEmail">
                                                                                </asp:RegularExpressionValidator>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="rfvTxtEmail">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <%--  end add coco--%>
                                                            <!--- --->
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Clave Unica Registro Población (CURP) (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                            ID="lblDX_CURP"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" CssClass="TextBox_1" Font-Size="11px" Width="250px" ID="txtDX_CURP"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDX_CURP" ErrorMessage="(*) Campo Obligatorio" Height="0px"
                                                                            ValidationGroup="Siguiente" ID="revDX_CURP" ValidationExpression="^.{18}$">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDX_CURP"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="rfvDX_CURP">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Nombre Comercial (*)" CssClass="Label" Font-Size="11pt"
                                                                            Width="150px" ID="lblDx_Nombre_Comercial"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                            ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_Nombre_Comercial"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_Nombre_Comercial"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator3">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Registro Federal Causantes (RFC) (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                            ID="lblDX_RFC"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" MaxLength="13" ValidationGroup="Siguiente" CssClass="TextBox_1"
                                                                            Font-Size="11px" ToolTip="(Formato Alfanumérico CCCCAAMMDDCCC ó CCCAAMMDDCCC)" Width="250px"
                                                                            ID="txtDX_RFC"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDX_RFC" ErrorMessage="(*) Campo Obligatorio" Height="0px"
                                                                            ValidationGroup="Siguiente" ID="rfvRFC">
                                                                        </asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDX_RFC" ErrorMessage="(*) Campo Obligatorio" Height="0px"
                                                                            ValidationGroup="Siguiente" ID="revRFC12" ValidationExpression="^.{12}$" Enabled="false">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDX_RFC" ErrorMessage="(*) Campo Obligatorio"
                                                                            ValidationGroup="Siguiente" ID="revRFC13" ValidationExpression="^.{13}$">
                                                                        </asp:RegularExpressionValidator>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Nombre Completo Representante Legal (*)" CssClass="Label" Font-Size="11pt"
                                                                            Width="155px" ID="lblDX_NOMBRE_REPRE_LEGAL"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                            ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDX_NOMBRE_REPRE_LEGAL"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDX_NOMBRE_REPRE_LEGAL"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator4">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Acredita Ocupación del Negocio con (*)" CssClass="Label" Width="150px"
                                                                            ID="lblDX_ACREDITACION_REPRE_LEGAL"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:DropDownList runat="server" CssClass="DropDownList" Font-Size="11px" Width="250px"
                                                                            ID="ddlDX_ACREDITACION_REPRE_LEGAL">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDX_ACREDITACION_REPRE_LEGAL"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator7">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Sexo" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                            ID="lblFG_SEXO_REPRE_LEGAL"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td width="250px">
                                                                    <div>
                                                                        <asp:RadioButton runat="server" GroupName="Sex1" Text="M" CssClass="RadioButton"
                                                                            Font-Size="11px" ForeColor="#333333" Width="50px" ID="radFG_SEXO_REPRE_LEGAL1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton
                                                                                runat="server" GroupName="Sex1" Text="F" CssClass="RadioButton" Font-Size="11px"
                                                                                ForeColor="#333333" ID="radFG_SEXO_REPRE_LEGAL2" /></div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Número de Servicio (RPU) (*)" CssClass="Label" Font-Size="11pt"
                                                                            Width="150px" ID="lblNO_RPU"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" MaxLength="12" ValidationGroup="Campo Obligatorio (12 digitos)"
                                                                            CssClass="TextBox_1" Font-Size="11px" Width="250px" ID="txtNO_RPU" 
                                                                            Enabled="False"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNO_RPU" ErrorMessage="(*) Campo Obligatorio"
                                                                            ValidationGroup="Siguiente" ToolTip="(12 Dígitos Numéricos)" ID="RequiredFieldValidator1"
                                                                            ValidationExpression="^((\d{12}))$">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Estado Civil (*)" CssClass="Label" Font-Size="11pt"
                                                                            Width="150px" ID="lbl_EDO_CIVIL_REPRE_LEGAL"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td width="250px">
                                                                    <div>
                                                                        <asp:RadioButton runat="server" GroupName="EstadoCivil" AutoPostBack="True" Text="Soltero(a)"
                                                                            CssClass="RadioButton" Font-Size="11px" ForeColor="#333333" Width="120px" ID="radFG_EDO_CIVIL_REPRE_LEGAL1"
                                                                            OnCheckedChanged="radFG_EDO_CIVIL_REPRE_LEGAL1_CheckedChanged" />
                                                                        <asp:RadioButton runat="server" GroupName="EstadoCivil" AutoPostBack="True" Text="Casado(a)"
                                                                            CssClass="RadioButton" Font-Size="11px" ForeColor="#333333" Width="120px" ID="radFG_EDO_CIVIL_REPRE_LEGAL2"
                                                                            OnCheckedChanged="radFG_EDO_CIVIL_REPRE_LEGAL2_CheckedChanged" /></div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Régimen Matrimonial" CssClass="Label" Font-Size="11pt"
                                                                            Width="150px" ID="lblDX_REG_CONYUGAL_REPRE_LEGAL"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:DropDownList runat="server" CssClass="DropDownList" Font-Size="11px"
                                                                            Width="250px" ID="ddlDX_REG_CONYUGAL_REPRE_LEGAL" OnSelectedIndexChanged="ddlDX_REG_CONYUGAL_REPRE_LEGAL_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Tipo de Identificación Oficial (*)" CssClass="Label" Font-Size="11pt"
                                                                            Width="155px" ID="lblDX_IDENTIFICACION_REPRE_LEGAL"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:DropDownList runat="server" CssClass="DropDownList" Font-Size="11px" Width="250px"
                                                                            ID="ddlDX_IDENTIFICACION_REPRE_LEGAL">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDX_IDENTIFICACION_REPRE_LEGAL"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator10">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Número Identificación (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                            ID="lblDX_NO_IDENTIFICACION_REPRE_LEGAL"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                            ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDX_NO_IDENTIFICACION_REPRE_LEGAL"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td width="50px">
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDX_NO_IDENTIFICACION_REPRE_LEGAL"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator5">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td width="150px">
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Promedio Mensual de Ventas" CssClass="Label" Font-Size="11pt"
                                                                            Width="150px" ID="lblMT_VENTAS_MES_EMPRESA"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td width="150px">
                                                                    <div>
                                                                        <asp:TextBox runat="server" CssClass="TextBox_1" Font-Size="11px" Width="250px" ID="txtMT_VENTAS_MES_EMPRESA"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d+(\.\d+)?$" Height="0px"
                                                                            ControlToValidate="txtMT_VENTAS_MES_EMPRESA" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                                                            ToolTip="(Formato Numérico con 2 Decimales)" ID="RegularExpressionValidator7">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMT_VENTAS_MES_EMPRESA"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator41">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Total de Gastos Mensuales" CssClass="Label" Font-Size="11pt"
                                                                            Width="150px" ID="lblMT_GASTOS_MES_EMPRESA"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" CssClass="TextBox_1" Font-Size="11px" Width="250px" ID="txtMT_GASTOS_MES_EMPRESA"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d+(\.\d+)?$" Height="0px"
                                                                            ControlToValidate="txtMT_GASTOS_MES_EMPRESA" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                                                            ToolTip="(Formato Numérico con 2 Decimales)" ID="RegularExpressionValidator6">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMT_GASTOS_MES_EMPRESA"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator42">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td width="155px">
                                                                    <div>
                                                                        <asp:Label runat="server" Text="E-mail del Representante Legal (*)" CssClass="Label"
                                                                            Font-Size="11pt" Width="155px" ID="lblDX_EMAIL_REPRE_LEGAL"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                            ToolTip="(Formato [a-9-&][@][a-9-&][.][a])" Width="250px" ID="txtDX_EMAIL_REPRE_LEGAL"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td width="30px">
                                                                    <div>
                                                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="^(([a-zA-Z0-9_\-\.ñáéíóúü]+)@([a-zA-Z0-9_\-\.ñáéíóúü]+)\.([a-zA-Zñáéíóúü]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.ñáéíóúü]+)@([a-zA-Z0-9_\-\.ñáéíóúü]+)\.([a-zA-Zñáéíóúü]{2,5}){1,25})+)*$" Height="0px"
                                                                            ControlToValidate="txtDX_EMAIL_REPRE_LEGAL" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                                                            ID="RegularExpressionValidator10">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDX_EMAIL_REPRE_LEGAL"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator40">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Consumo Promedio" CssClass="Label_2" Font-Size="11pt"
                                                                            Width="150px" ID="lblNO_CONSUMO_PROMEDIO" Visible="False"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" CssClass="TextBox_1" Font-Size="11px" Width="250px" ID="txtNO_CONSUMO_PROMEDIO"
                                                                            Visible="False"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]*[1-9][0-9]*$"
                                                                            ControlToValidate="txtNO_CONSUMO_PROMEDIO" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                                                            ToolTip="(Formato Numérico Enteros)" ID="RegularExpressionValidator5" Enabled="false">
                                                                        </asp:RegularExpressionValidator>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td width="50px">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </asp:Panel>
                                                    <!---datos d la empresa o personas fisicas --->
                                                    <asp:Image runat="server" ImageUrl="images/t2.png" />
                                                    <asp:UpdatePanel ID="secondSection" runat="server">
                                                        <ContentTemplate>
                                                    <asp:Panel runat="server" ID="Pnl2">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="160px">
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Calle del Domicilio (*)" CssClass="Label_2" Font-Size="11pt" Width="160px"
                                                                            ID="lblDx_Domicilio_fisc_calle"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td width="250px">
                                                                    <div>
                                                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                            ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_Domicilio_fisc_calle"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_Domicilio_fisc_calle"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator6">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td width="160px">
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Número e Interior (*)" CssClass="Label_2" Font-Size="11pt" Width="150px"
                                                                            ID="lblDx_Domicilio_fisc_num"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td width="250px">
                                                                    <div>
                                                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                            ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_Domicilio_fisc_num"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_Domicilio_fisc_num"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator11">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Código Postal (*)" CssClass="Label_2" Font-Size="11pt" Width="150px"
                                                                            ID="lblDx_Domicilio_fisc_cp"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" MaxLength="5" ValidationGroup="Siguiente" CssClass="TextBox_1"
                                                                            Font-Size="11px" ToolTip="(5 Dígitos Numéricos)" Width="250px" ID="txtDx_Domicilio_fisc_cp"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d{5}$" ControlToValidate="txtDx_Domicilio_fisc_cp" Height="0px"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RegularExpressionValidator1">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_Domicilio_fisc_cp"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator12">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Colonia (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                            ID="lblDx_Domicilio_Fisc_Colonia"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                            ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_Domicilio_Fisc_Colonia"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_Domicilio_Fisc_Colonia"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator17">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Estado (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                            ID="lblDx_nombre_estado"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:DropDownList runat="server" AutoPostBack="True" ValidationGroup="Siguiente"
                                                                            CssClass="DropDownList" Font-Size="11px" ToolTip="(Campo Obligatorio)" Width="250px"
                                                                            ID="ddlDx_nombre_estado" OnSelectedIndexChanged="ddlDx_nombre_estado_SelectedIndexChanged" onChange="lockScreen();">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDx_nombre_estado"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator13">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Delegación o Municipio (*)" CssClass="Label" Font-Size="11pt"
                                                                            Width="150px" ID="lblDx_deleg_municipio"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:DropDownList runat="server" ValidationGroup="Siguiente" CssClass="DropDownList"
                                                                            Font-Size="11px" ToolTip="(Campo Obligatorio)" Width="250px" ID="ddlDx_deleg_municipio">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                                <td width="50px">
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDx_deleg_municipio"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator14">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                  </tr>
                                                                  <tr>

           
                                                            
<%--                         
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Ciudad (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                            ID="lblCiudad"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                            ToolTip="(Campo Obligatorio)" Width="250px" ID="txtCiudad"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCiudad" ErrorMessage="(*) Campo Obligatorio"
                                                                            ValidationGroup="Siguiente" ID="RequiredFieldValidator15">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
--%>                                                   <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Teléfono (*)" CssClass="Label_2" Font-Size="11pt"
                                                                            Width="150px" ID="lblDx_tel_neg"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                            ToolTip="(10 Dígitos Numéricos)" Width="250px" ID="txtDx_tel_neg"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="^((\d{10}))$" Height="0px"
                                                                            ControlToValidate="txtDx_tel_neg" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                                                            ID="RegularExpressionValidator12">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_tel_neg" ValidationGroup="Siguiente"
                                                                            ErrorMessage="(*) Campo Obligatorio" ID="RequiredFieldValidator38">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label runat="server" Text="Tipo de Propiedad (*)" CssClass="Label" Font-Size="11pt"
                                                                            Width="145px" ID="lblDx_Tipo_propiedad"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:DropDownList runat="server" CssClass="DropDownList" Font-Size="11px" Width="250px"
                                                                            ID="ddlDx_Tipo_propiedad">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDx_Tipo_propiedad"
                                                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator16">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <br>
                                                        <asp:Image runat="server" ImageUrl="images/t3.png" />
                                                        <asp:UpdatePanel ID="thirdSection" runat="server">
                                                            <ContentTemplate>
                                                        <asp:Panel runat="server" ID="pnlBusinessAddress">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label runat="server" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 250px">
                                                                        <asp:CheckBox runat="server" AutoPostBack="True" Text="Marcar si el Domicilio del Negocio es el mismo que el Domicilio Fiscal"
                                                                            CssClass="CheckBox" ForeColor="#333333" Width="250px" ID="ckbFg_mismo_domicilio"
                                                                            OnCheckedChanged="ckbFg_mismo_domicilio_CheckedChanged" onclick="lockScreen();" />
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="150px">
                                                                        <div>
                                                                            <asp:Label runat="server" Text="Calle del Domicilio (*)" CssClass="Label_2" Font-Size="11pt" Width="150px"
                                                                                ID="lblDx_domicilio_neg_calle"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td width="250px">
                                                                        <div>
                                                                            <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                                ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_domicilio_neg_calle"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td width="70px">
                                                                        <div>
                                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_domicilio_neg_calle"
                                                                                ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator18">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </td>
                                                                    <td width="158px">
                                                                        <div>
                                                                            <asp:Label runat="server" Text="Número e Interior (*)" CssClass="Label_2" Font-Size="11pt" Width="150px"
                                                                                ID="lblDx_domicilio_neg_num"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td width="250px">
                                                                        <div>
                                                                            <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                                ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_domicilio_neg_num"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_domicilio_neg_num"
                                                                                ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator19">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div>
                                                                            <asp:Label runat="server" Text="Código Postal (*)" CssClass="Label_2" Font-Size="11pt" Width="150px"
                                                                                ID="lblDx_domicilio_neg_cp"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:TextBox runat="server" MaxLength="5" ValidationGroup="Siguiente" CssClass="TextBox_1"
                                                                                Font-Size="11px" ToolTip="(5 Dígitos Numéricos)" Width="250px" ID="txtDx_domicilio_neg_cp"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d{5}$" ControlToValidate="txtDx_domicilio_neg_cp" Height="0px"
                                                                                ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RegularExpressionValidator2">
                                                                            </asp:RegularExpressionValidator>
                                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_Domicilio_neg_cp"
                                                                                ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator37">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:Label runat="server" Text="Colonia (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                                ID="lblDx_Domicilio_Neg_Colonia"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                                ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_Domicilio_Neg_Colonia"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_Domicilio_Neg_Colonia"
                                                                                ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator20">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                      <td>
                                                                        <div>
                                                                            <asp:Label runat="server" Text="Estado (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                                ID="lblDx_nombre_estado_Neg"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:DropDownList runat="server" AutoPostBack="True" CssClass="DropDownList" Font-Size="11px"
                                                                                Width="250px" ID="ddlDx_nombre_estado_Neg" OnSelectedIndexChanged="ddlDx_nombre_estado_Neg_SelectedIndexChanged"  onChange="lockScreen1();">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDx_nombre_estado_neg"
                                                                                ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator21">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </td>
                                                              
                                                                
                                                                    <td style="width: 158px">
                                                                        <div>
                                                                            <asp:Label runat="server" Text="Delegación o Municipio (*)" CssClass="Label" Font-Size="11pt"
                                                                                Width="150px" ID="lblDx_deleg_municipio_Neg"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:DropDownList runat="server" CssClass="DropDownList" Font-Size="11px" Width="250px"
                                                                                ID="ddlDx_deleg_municipio_Neg">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDX_deleg_municipio_Neg"
                                                                                ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator22">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div>
                                                                            <asp:Label runat="server" Text="Teléfono (*)" CssClass="Label_2" Font-Size="11pt"
                                                                                Width="150px" ID="lblDx_tel_neg_Neg"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:TextBox runat="server" MaxLength="10" ValidationGroup="Siguiente" CssClass="TextBox_1"
                                                                                Font-Size="11px" ToolTip="(10 Dígitos Numéricos)" Width="250px" ID="txtDx_tel_neg_Neg"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td width="40px">
                                                                        <div>
                                                                            <asp:RegularExpressionValidator runat="server" ValidationExpression="^((\d{10}))$" Height="0px"
                                                                                ControlToValidate="txtDx_tel_neg_Neg" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                                                                ID="RegularExpressionValidator13">
                                                                            </asp:RegularExpressionValidator>
                                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_tel_neg_Neg"
                                                                                ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator39">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:Label runat="server" Text="Tipo de Propiedad (*)" CssClass="Label" Font-Size="11pt"
                                                                                Width="150px" ID="lblDx_Tipo_propiedad_Neg"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:DropDownList runat="server" CssClass="DropDownList" Font-Size="11px" Width="250px"
                                                                                ID="ddlDx_Tipo_propiedad_Neg">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDx_Tipo_propiedad_Neg"
                                                                                ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="rfvDdlDx_Tipo_propiedad_Neg">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <!-- Fin tabla domicilio del negocio -->
                                                        <br>
                                                            <asp:Image runat="server" ImageUrl="images/t4.png" />
                                                            <asp:UpdatePanel ID="fourSection" runat="server">
                                                                <ContentTemplate>
                                                            <asp:Panel runat="server" ID="Pnl3">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td width="150px">
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Nombre (*)" CssClass="Label" Font-Size="11pt"
                                                                                    Width="150px" ID="lblDx_First_Name_aval"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td width="250px">
                                                                            <div>
                                                                                <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                                    ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_First_Name_aval"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td width="70px">
                                                                            <div>
                                                                            </div>
                                                                        </td>
                                                                        <td width="155px">
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Apellido Paterno" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                                    ID="lblDx_Last_Name_aval"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td width="250px">
                                                                            <div>
                                                                                <asp:TextBox runat="server" CssClass="TextBox_1" Font-Size="11px" Width="250px" ID="txtDx_Last_Name_aval"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="150px">
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Apellido Materno" CssClass="Label" Font-Size="11pt"
                                                                                    Width="150px" ID="lblDx_Mother_Name_aval"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td width="250px">
                                                                            <div>
                                                                                <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                                    ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_Mother_Name_aval"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td width="70px">
                                                                            <div>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDX_Mother_Name_aval"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="rfvDX_Mother_Name_aval">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                        <td width="155px">
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Fecha de Nacimiento (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                                    ToolTip="(Formato de Fecha AAAA-MM-DD)" ID="lblDt_BirthDate_Aval"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td width="250px">
                                                                            <div>
                                                                                <asp:TextBox runat="server" CssClass="TextBox_1" Font-Size="11px" Width="250px" ID="txtDt_BirthDate_Aval"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px"
                                                                                    ControlToValidate="txtDt_BirthDate_Aval" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                                                                    ID="revDt_BirthDate_Aval">
                                                                                </asp:RegularExpressionValidator>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDt_BirthDate_Aval"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="rfvDt_BirthDate_Aval">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="150px">
                                                                            <div>
                                                                                <asp:Label runat="server" Text="RFC (*)" CssClass="Label" Font-Size="11pt"
                                                                                    Width="150px" ID="lblDx_RFC_Aval"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td width="250px">
                                                                            <div>
                                                                                <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                                    ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_RFC_Aval"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td width="70px">
                                                                            <div>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_RFC_Aval"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="rfvDx_RFC_Aval">
                                                                                </asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDX_RFC_Aval" ErrorMessage="(*) Campo Obligatorio"
                                                                                    ValidationGroup="Siguiente" ID="revDX_RFC_Aval" ValidationExpression="^.{10}(...)?$">
                                                                                </asp:RegularExpressionValidator>
                                                                            </div>
                                                                        </td>
                                                                        <td width="155px">
                                                                            <div>
                                                                                <asp:Label runat="server" Text="CURP (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                                    ID="lblDx_CURP_Aval"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td width="250px">
                                                                            <div>
                                                                                <asp:TextBox runat="server" CssClass="TextBox_1" Font-Size="11px" Width="250px" ID="txtDx_CURP_Aval"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDX_CURP_Aval" ErrorMessage="(*) Campo Obligatorio" Height="0px"
                                                                                    ValidationGroup="Siguiente" ID="revDX_CURP_Aval" ValidationExpression="^.{18}$">
                                                                                </asp:RegularExpressionValidator>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_CURP_Aval"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="rfvDx_CURP_Aval">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Sexo" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                                    ID="lblDx_sexo"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RadioButton runat="server" GroupName="Sex2" Text="M" CssClass="RadioButton"
                                                                                    Font-Size="11px" ForeColor="#333333" ID="radDx_sexo1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton
                                                                                        runat="server" GroupName="Sex2" Text="F" CssClass="RadioButton" Font-Size="11px"
                                                                                        ForeColor="#333333" ID="radDx_sexo2" /></div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                            </div>
                                                                        </td>
                                                                        <td width="70px">
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Teléfono (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                                    ID="lblDx_tel_aval"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td width="250px">
                                                                            <div>
                                                                                <asp:TextBox runat="server" MaxLength="10" ValidationGroup="Siguiente" CssClass="TextBox_1"
                                                                                    Font-Size="11px" ToolTip="(10 Dígitos Numéricos)" Width="250px" ID="txtDx_tel_aval"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^((\d{10}))$" Height="0px"
                                                                                    ControlToValidate="txtDx_tel_aval" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                                                                    ID="RegularExpressionValidator14">
                                                                                </asp:RegularExpressionValidator>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_tel_aval" ErrorMessage="(*) Campo Obligatorio"
                                                                                    ValidationGroup="Siguiente" ID="RequiredFieldValidator45">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Calle del Domicilio (*)" CssClass="Label_2" Font-Size="11pt" Width="150px"
                                                                                    ID="lblDx_domicilio_aval_calle"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                                    ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_domicilio_aval_calle"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_domicilio_aval_calle"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator25">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Número e Interior(*)" CssClass="Label_2" Font-Size="11pt" Width="150px"
                                                                                    ID="lblDx_domicilio_aval_num"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                                    ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_domicilio_aval_num"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_domicilio_aval_num"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator26">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Código Postal (*)" CssClass="Label_2" Font-Size="11pt" Width="150px"
                                                                                    ID="lblDx_domicilio_aval_cp"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:TextBox runat="server" MaxLength="5" CssClass="TextBox_1" Font-Size="11px" Width="250px"
                                                                                    ID="txtDx_domicilio_aval_cp"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d{5}$" ControlToValidate="txtDx_domicilio_aval_cp" Height="0px"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RegularExpressionValidator3">
                                                                                </asp:RegularExpressionValidator>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_domicilio_aval_cp"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="rfvTxtDx_domicilio_aval_cp">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                        <td style="width: 150px">
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Colonia (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                                    ID="lblDx_Domicilio_Aval_Colonia"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                                                    ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_Domicilio_Aval_Colonia"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_Domicilio_Aval_Colonia"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator28">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Estado (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                                                    ID="lblDx_nombre_estado_aval"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:DropDownList runat="server" AutoPostBack="True" CssClass="DropDownList" Font-Size="11px"
                                                                                    Width="250px" ID="ddlDx_nombre_estado_aval" OnSelectedIndexChanged="ddlDx_nombre_estado_aval_SelectedIndexChanged" onChange="lockScreen1();">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDX_nombre_estado_aval"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator27">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>                                                                 
                                                                        <td>
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Delegación o Municipio (*)" CssClass="Label" Font-Size="11pt"
                                                                                    Width="150px" ID="lblDx_deleg_municipio_aval"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:DropDownList runat="server" CssClass="DropDownList" Font-Size="11px" Width="250px"
                                                                                    ID="ddlDx_deleg_municipio_aval">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDX_deleg_municipio_aval"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator29">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
   
                                                                    </tr>
  <%--                                                                  <tr>
                                                                        <td style="width: 160px">
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Número de Servicio (RPU)" CssClass="Label" Font-Size="11pt"
                                                                                    Width="150px" ID="lblNo_RPU_aval"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:TextBox runat="server" AutoPostBack="True" MaxLength="12" CssClass="TextBox_1"
                                                                                    Font-Size="11px" Width="250px" ID="txtNo_RPU_aval"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Ingresos Mensuales (*)" CssClass="Label" Font-Size="11pt"
                                                                                    Width="150px" ID="lblMt_ventas_mes_aval"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:TextBox runat="server" CssClass="TextBox_1" Font-Size="11px" Width="250px" ID="txtMt_ventas_mes_aval"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d+(\.\d+)?$"
                                                                                    ControlToValidate="txtMt_ventas_mes_aval" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                                                                    ID="RegularExpressionValidator8">
                                                                                </asp:RegularExpressionValidator>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMt_ventas_mes_aval"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator43">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 150px">
                                                                            <div>
                                                                                <asp:Label runat="server" Text="Total de Gastos Mensuales (*)" CssClass="Label" Font-Size="11pt"
                                                                                    Width="150px" ID="lblMt_gastos_mes_aval"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td style="width: 250px">
                                                                            <div>
                                                                                <asp:TextBox runat="server" CssClass="TextBox_1" Font-Size="11px" Width="250px" ID="txtMt_gastos_mes_aval"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td width="103px">
                                                                            <div>
                                                                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d+(\.\d+)?$" Height="0px"
                                                                                    ControlToValidate="txtMt_gastos_mes_aval" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                                                                    ID="RegularExpressionValidator9">
                                                                                </asp:RegularExpressionValidator>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMt_gastos_mes_aval"
                                                                                    ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator44">
                                                                                </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                            </div>
  --%>
                                                                    <tr>
                                                                        <td>
                                                                            <div>
                                                                                        <asp:Button ID="btnPreliminary" runat="server" Text="Guardado Temporal" OnClick="btnPreliminary_Click" />
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>                                                         
                                                            
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
     
   
 
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" ID="wizFinancialSupportPerson" StepType="Step">
                        <div>
                            <br>
                                <asp:Image runat="server" ImageUrl="images/t_alta.png" ID="imgAlta2"/>
                                <asp:Image runat="server" ImageUrl="images/t_edicioncredito.png" ID="imgEdit2" Visible="false"/>
                                <asp:Label runat="server" Text="ALTA DE SOLICITUD DE CREDITO" CssClass="Label_1"
                                    ID="Label1" Visible="False"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div align="left">
                                                <asp:Label ID="lblCredito2" Text="No. Crédito" runat="server" CssClass="Label" Width="165px" ForeColor="#333333" Visible="False"/>
                                                <asp:TextBox ID="txtCredito2" runat="server" CssClass="TextBox" Width="250px" Enabled="false" Visible="False"/>
                                            </div>
                                        </td>
                                        <td>
                                            <div align="right">
                                                <asp:Label runat="server" Text="Fecha" CssClass="Label" ID="lblFecha1"></asp:Label>
                                                <asp:Label runat="server" ID="lblDate1"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                           
                        </div>
                        <br />
                        <asp:Image runat="server" ImageUrl="images/t5.png" />
                        <asp:Panel runat="server" ID="pnlFinancialSupportPerson">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Nombre Completo (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                            ID="lblDx_nombre_coacreditado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                            ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_nombre_coacreditado"></asp:TextBox>
                                    </td>
                                    <td width="100px">
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDX_nombre_coacreditado"
                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator30">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" Text="CURP/RFC (*)" CssClass="Label_2" Font-Size="11pt"
                                            Width="150px" ID="lblDx_RFC_CURP_Coacreditado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="TextBox_1" Font-Size="11px" Width="250px" ID="txtDx_RFC_CURP_Coacreditado"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_RFC_CURP_Coacreditado"
                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator31">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Sexo" CssClass="Label" Font-Size="11pt" Width="150px"
                                            ID="lblSex1"></asp:Label>
                                    </td>
                                    <td width="250px">
                                        <asp:RadioButton runat="server" GroupName="Sex" Text="M" CssClass="RadioButton" Font-Size="11px"
                                            ForeColor="#333333" Width="100px" ID="RadioButton11" />
                                        <asp:RadioButton runat="server" GroupName="Sex" Text="F" CssClass="RadioButton" Font-Size="11px"
                                            ForeColor="#333333" Width="100px" ID="RadioButton12" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" Text="Teléfono (*)" CssClass="Label_2" Font-Size="11pt"
                                            Width="150px" ID="lblDx_telefono_coacreditado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" MaxLength="10" CssClass="TextBox_1" Font-Size="11px"
                                            ToolTip="(10 Dígitos Numéricos)" Width="250px" ID="txtDx_telefono_coacreditado"></asp:TextBox>
                                    </td>
                                    <td style="width: 10px">
                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="^((\d{10}))$" Height="0px"
                                            ControlToValidate="txtDx_telefono_coacreditado" ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente"
                                            ID="RegularExpressionValidator11">
                                        </asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_telefono_coacreditado"
                                            ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator43">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Label runat="server" Text="Calle del Domicilio (*)" CssClass="Label_2" Font-Size="11pt" Width="150px"
                                                ID="lblDx_domicilio_coacreditado_calle"></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox_1" Font-Size="11px"
                                                ToolTip="(Campo Obligatorio)" Width="250px" ID="txtDx_domicilio_coacreditado_calle"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_domicilio_coacreditado_calle"
                                                ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator32">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Label runat="server" Text="Número e Interior(*)" CssClass="Label_2" Font-Size="11pt" Width="150px"
                                                ID="lblDx_domicilio_coacreditado_num"></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:TextBox runat="server" CssClass="TextBox_1" Font-Size="11px" ToolTip="(Campo Obligatorio)"
                                                Width="250px" ID="txtDx_domicilio_coacreditado_num"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 100px">
                                        <div>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDx_domicilio_coacreditado_num"
                                                ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RequiredFieldValidator33">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Label runat="server" Text="Código Postal (*)" CssClass="Label_2" Font-Size="11pt" Width="150px"
                                                ID="lblDx_domicilio_coacreditado_cp"></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:TextBox runat="server" MaxLength="5" CssClass="TextBox_1" Font-Size="11px" ToolTip="(5 Dígitos Numéricos)"
                                                Width="250px" ID="txtDx_domicilio_coacreditado_cp"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:RegularExpressionValidator runat="server" ValidationExpression="^\d{5}$" ControlToValidate="txtDx_domicilio_coacreditado_cp" Height="0px"
                                                ErrorMessage="(*) Campo Obligatorio" ValidationGroup="Siguiente" ID="RegularExpressionValidator4">
                                            </asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                                ControlToValidate="txtDx_domicilio_coacreditado_cp" ErrorMessage="(*) Campo Obligatorio" 
                                                ValidationGroup="Siguiente"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Label runat="server" Text="Colonia (*)" CssClass="Label" Font-Size="11pt" Width="150px"
                                                ID="lblDx_Domicilio_Coacreditado_Colonia"></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:TextBox ID="txtDx_Domicilio_Coacreditado_Colonia" runat="server" 
                                                CssClass="TextBox_1" Font-Size="11px" ToolTip="(Campo Obligatorio)" 
                                                ValidationGroup="Siguiente" Width="250px"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 100px">
                                        <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server"
                                                ControlToValidate="txtDx_Domicilio_Coacreditado_Colonia" ErrorMessage="(*) Campo Obligatorio" 
                                                ValidationGroup="Siguiente">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Label runat="server" Text="Estado (*)" CssClass="Label" Font-Size="11pt"
                                                Width="150px" ID="lblDx_nombre_estado2"></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:DropDownList ID="ddlDx_nombre_estado2" runat="server" AutoPostBack="True" 
                                                CssClass="DropDownList" Font-Size="11px" 
                                                OnSelectedIndexChanged="ddlDx_nombre_estado2_SelectedIndexChanged" 
                                                Width="250px">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server"
                                                ControlToValidate="ddlDX_nombre_estado2" ErrorMessage="(*) Campo Obligatorio" 
                                                ValidationGroup="Siguiente" ></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Label runat="server" Text="Delegación o Municipio (*)" CssClass="Label" 
                                                Font-Size="11pt" Width="150px"
                                                ID="lblDx_deleg_municipio2"></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:DropDownList ID="ddlDx_deleg_municipio2" runat="server" CssClass="DropDownList" 
                                                Font-Size="11px" Width="250px">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 100px">
                                        <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server"
                                                ControlToValidate="ddlDX_deleg_municipio2" ErrorMessage="(*) Campo Obligatorio" 
                                                ValidationGroup="Siguiente">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                      
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" ID="wiz" StepType="Step">
            
                        <div>
                            <asp:Image runat="server" ImageUrl="images/t_presupuesto2.png" />
                            <asp:Label ID="Label3" runat="server" CssClass="Label_1" 
                                Text="GENERACION DE PRESUPUESTO DE INVERSION" Visible="False"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div align="left">
                                            <asp:Label ID="lblCredito3" Text="No. Crédito" runat="server" CssClass="Label" Width="180px" ForeColor="#333333" Visible="False"/>
                                            <asp:TextBox ID="txtCredito3" runat="server" CssClass="TextBox" Width="250px" Enabled="false" Visible="False"/>
                                        </div>
                                    </td>
                                    <td>
                                        <div align="right">
                                            <asp:Label ID="lblFecha2" runat="server" CssClass="Label" Text="Fecha"></asp:Label>
                                            <asp:Label ID="lblDate2" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
 
                        <table width="100%">
                            <tr>
                                <td width="180px">
                                    <asp:Label ID="lblDx_razon_social3" runat="server" CssClass="Label" 
                                        Font-Size="11pt" Text="Nombre o Razón Social" Width="180px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDx_razon_social3" runat="server" CssClass="TextBox" 
                                        Enabled="False" Font-Size="11px" Width="250px"></asp:TextBox>
                                </td>
                                <td width="150px">
                                </td>
                                <td width="150px">
                                    <asp:Label ID="lblDx_tipo_industria3" runat="server" CssClass="Label" 
                                        Font-Size="11pt" Text="Giro de la Empresa" Width="150px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDx_tipo_industria3" runat="server" CssClass="TextBox" 
                                        Enabled="False" Font-Size="11px" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlPage3" runat="server">
                            <br />
                            <div>
                                <asp:Image runat="server" ImageUrl="images/t_equipos.png" />
                                <asp:Label ID="lblTitle3" runat="server" CssClass="Label_1" 
                                    Text="DATOS DE LOS EQUIPOS SOLICITADOS" Visible="False"></asp:Label>
                            </div>
                            <table width="100%">
                                <tr>
                                    <td style="font-size: small; color: #0499B6">
                                        <img alt="" height="20" src="images/notice.png" width="22" /> El Costo Unitario de los 
                                        Equipos NO incluye el IVA
                                    </td>
                                </tr>
                            </table>
                            <div>
                            </div>
                            <div>
                                <asp:GridView ID="gvTecPro" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" OnDataBound="gvTecPro_DataBound" Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" CssClass="titulo" Text="Tecnología"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTecnolog" runat="server" AutoPostBack="true" 
                                                    CssClass="DropDownList_gdv" 
                                                    OnSelectedIndexChanged="ddlTecnolog_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" CssClass="titulo" Text="Marca"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlMarca" runat="server" AutoPostBack="true" 
                                                    CssClass="DropDownList_gdv" 
                                                    OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" CssClass="titulo" Text="Modelo"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" 
                                                    CssClass="DropDownList_gdv" 
                                                    OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label9" runat="server" CssClass="titulo" Text="Tipo de Producto"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTypeOfProduct" runat="server" AutoPostBack="true" 
                                                    CssClass="DropDownList_gdv" Enabled="false"
                                                    OnSelectedIndexChanged="ddlTypeOfProduct_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" CssClass="titulo" Text="Cantidad"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCantidad" runat="server" AutoPostBack="true" 
                                                    CssClass="TextBox_gdvc" OnTextChanged="txtCantidad_TextChanged"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" CssClass="titulo" Text="Precio Unitario s/IVA"
                                                    ToolTip="(Sin IVA)"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGridText1" runat="server" AutoPostBack="true" CssClass="TextBox_gdv"
                                                    OnTextChanged="txtGridText1_TextChanged" Style="text-align: right"></asp:TextBox>
                                                <asp:HiddenField ID="hfExactGridText1" runat="server" />
                                                <asp:HiddenField ID="hfBasePrice" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblCapacidad" runat="server" CssClass="titulo" Text="Capacidad"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlCapacidad" runat="server" CssClass="DropDownList_gdv">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" CssClass="titulo" Text="Importe Total s/IVA" ToolTip="(Sin IVA)"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSubtotal" runat="server" CssClass="TextBox_gdv" 
                                                    Enabled="false" Style="text-align: right"></asp:TextBox>
                                                <asp:HiddenField ID="hfExactSubtotal" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="70px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" CssClass="titulo" Text="Gtos Inst.y MO s/IVA"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGastos" runat="server" AutoPostBack="true" Visible="false" CssClass="TextBox_gdv" 
                                                    Style="text-align: right" OnTextChanged="txtGastos_TextChanged"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="140px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div style="position:absolute;">
                                    <asp:Literal ID="lDescription" runat="server" Visible="false"></asp:Literal>
                                </div>
                                <div align="right">
                                    <br />
                                    <asp:Label ID="Label5" runat="server" Text="Subtotal"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtSubTotal" runat="server" Enabled="False" Style="text-align: right" Width="127px"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label6" runat="server" Text="IVA"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtIVA" runat="server" Enabled="False" Style="text-align: right" Width="127px"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label7" runat="server" Text="Costo Equipo(s)"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtCostEquipo" runat="server" Enabled="False" Style="text-align: right" Width="127px"></asp:TextBox>
                                    <br />
                                    <asp:Label runat="server" Text="Costo Acopio y Destrucción"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtTotalCost" runat="server" Enabled="False" Style="text-align: right" Width="127px"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label2" runat="server" Text="Incentivo (10%)"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtTotalDescount" runat="server" Enabled="False" Style="text-align: right" Width="127px"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label8" runat="server" Text="Descuento"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtDescount" runat="server" Enabled="False" Style="text-align: right" Width="127px"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtTotal" runat="server" CssClass="TextBox_gdv2" 
                                        Enabled="False" Style="text-align: right" Width="124px"></asp:TextBox>
                                    <asp:HiddenField ID="hfTotal" runat="server" />
                                    &nbsp;</div>
                            </div>
                            <div>
                                <asp:Button ID="btnAddRow" runat="server" CssClass="Button" 
                                    OnClick="btnAddRow_Click" 
                                    OnClientClick="return confirm('¿ Desea Agregar una Tecnología al Presupuesto de Inversión ?');" 
                                    Text="Agregar" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnClear" runat="server" CssClass="Button" 
                                    OnClick="btnClear_Click" 
                                    OnClientClick="return confirm('¿ Desea Borrar el Presupuesto de Inversión ?');" 
                                    Text="Borrar Cotización" Width="120px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                        </asp:Panel>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" ID="wizSaveCreditRequest" StepType="Finish">
                        <br />
                        <div>
                            <asp:Image runat="server" ImageUrl="images/t_solicitud.png" />
                            <asp:Label runat="server" Text="GUARDAR SOLICITUD DE CREDITO" CssClass="Label_1"
                                ID="Label4" Visible="False"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;
                            <div>
                                <table width="100%">
                                    <tr>
                                        <td width="800px">
                                            <asp:Label ID="lblCredito4" Text="No. Crédito" runat="server" CssClass="Label" Width="200px" ForeColor="#333333" Visible="False"/>
                                            <asp:TextBox ID="txtCredito4" runat="server" CssClass="TextBox" Width="250px" Enabled="false" Visible="False"/>
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" Text="Fecha" CssClass="Label_2" ID="lblFecha3"></asp:Label>
                                            <asp:Label runat="server" ID="lblDate3"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <br />
                        <table width="100%">
                            <tr>
                                <td width="200px">
                                    <asp:Label runat="server" Text="Nombre o Razón Social" CssClass="Label" Font-Size="11pt"
                                        Width="200px" ID="lblDx_razon_social4"></asp:Label>
                                </td>
                                <td width="250px">
                                    <asp:TextBox runat="server" CssClass="TextBox" Enabled="False" Font-Size="11px" Width="250px"
                                        ID="txtDx_razon_social4"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td width="150px">
                                    <asp:Label runat="server" Text="Giro de la Empresa" CssClass="Label" Font-Size="11pt"
                                        Width="150px" ID="lblDx_tipo_industria4"></asp:Label>
                                </td>
                                <td width="250px">
                                    <asp:TextBox runat="server" CssClass="TextBox" Enabled="False" Font-Size="11px" Width="250px"
                                        ID="txtDx_tipo_industria4"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel runat="server" ID="pnlPage4">
                            <br />
                            <div>
                                <asp:Image runat="server" ImageUrl="images/t_solicitud2.png" />
                                <asp:Label runat="server" Text="DATOS DE LA SOLICITUD" CssClass="Label_1" ID="lblTitle4"
                                    Visible="False"></asp:Label>
                            </div>
                            <table width="100%">
                                <tr>
                                    <td width="200px">
                                        <asp:Label runat="server" Text="Monto a Financiar (MXP)" CssClass="Label" Font-Size="11pt"
                                            Width="180px" ID="lblMt_monto_solicitado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="TextBox" Enabled="False" Font-Size="11px" Width="250px"
                                            ID="txtMt_monto_solicitado"></asp:TextBox>
                                    </td>
                                    <td width="150px">
                                        <asp:Button runat="server" OnClientClick="return confirm('¿ Desea Realizar la Impresión de la Autorización de Consulta a Círculo/Buro de Crédito ?');"
                                            Text="Autorización Círculo/Buro" CssClass="Button" Enabled="False" Width="155px"
                                            ID="btn41" OnClick="btn41_Click" />
                                    </td>
                                    <td width="150px">
                                        <asp:Button runat="server" OnClientClick="return confirm('¿ Desea Realizar la Impresión de la Tabla de Amortización ?');"
                                            Text="Tabla de Amortización" CssClass="Button" Enabled="False" Width="155px"
                                            ID="btn43" OnClick="btn43_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="130px">
                                        <asp:Label runat="server" Text="Número de Pagos" CssClass="Label" Font-Size="11pt"
                                            Width="120px" ID="lblNo_Plazo_Pago"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="TextBox" Enabled="False" Font-Size="11px" Width="250px"
                                            ID="txtNo_Plazo_Pago"></asp:TextBox>
                                    </td>
                                    <td width="150px">
                                        <asp:Button runat="server" OnClientClick="return confirm('¿ Desea Realizar la Impresión de la Carta Presupuesto de Inversión ?');"
                                            Text="Carta Presupuesto" CssClass="Button" Enabled="False" Width="155px" ID="btn42"
                                            OnClick="btn42_Click" />
                                    </td>
                                    <td width="150px">
                                        <asp:Button runat="server" OnClientClick="return confirm('¿ Desea Realizar la Impresión de la Solicitud de Crédito ?');"
                                            Text="Solicitud" CssClass="Button" Enabled="False" Width="155px" ID="ButtonRQT"
                                            OnClick="btnDisplayRequest_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        <asp:Label runat="server" Text="Periodicidad de Pago" CssClass="Label" Font-Size="11pt"
                                            Width="150px" ID="lblDx_periodo_pago"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" CssClass="DropDownList" Enabled="False" Font-Size="11px"
                                            Width="250px" ID="ddlDx_periodo_pago">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:WizardStep>
                </WizardSteps>
                <StepNavigationTemplate>
                    <asp:Button ID="btnStepPre" runat="server" Text="Regresar" CssClass="Button" CommandName="MovePrevious"
                        OnClientClick="return confirm('¿ Desea Regresar a la Pantalla Previa ?');" />
                    <asp:Button ID="btnStepNext" runat="server" Text="Siguiente" CssClass="Button" CommandName="MoveNext"
                        OnClientClick="return confirm('¿ Desea Continuar con la Generación del Presupuesto de Inversión correspondiente a ésta Solicitud de Crédito ?');" />
                    <asp:Button ID="BtnCacel" runat="server" Text="Salir" CssClass="Button" OnClick="BtnCacel_Click"
                        OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" />
                </StepNavigationTemplate>
            </asp:Wizard>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
