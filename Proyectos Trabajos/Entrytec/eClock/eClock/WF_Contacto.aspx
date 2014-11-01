<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_Contacto.aspx.cs" Inherits="WF_Contacto" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 218px;
        }
        .style2
        {
            width: 98px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table border="0" cellpadding="3" style="width: 618px;">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Lbl_Razon" runat="server" Text="Razon Social"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Lbl_RFC" runat="server" Text="R.F.C o Identificación Fiscal"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Tbx_Razon" runat="server" Width="429px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="Tbx_RFC" runat="server" Width="176px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Lbl_Nombre" runat="server" Text="Nombre"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="Tbx_Nombre" Display="Dynamic" 
                                    ErrorMessage=" Se requiere capturar el Nombre">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Lbl_APaterno" runat="server" Text="Apellido paterno"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                    runat="server" ControlToValidate="Tbx_APaterno" Display="Dynamic" 
                                    ErrorMessage=" Se requiere capturar el Apellido paterno">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Lbl_AMaterno" runat="server" Text="Apellido materno"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Tbx_Nombre" runat="server" Width="199px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="Tbx_APaterno" runat="server" Width="199px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="Tbx_AMaterno" runat="server" Width="199px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Lbl_eMail" runat="server" Text="Correo electrónico"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                    runat="server" ControlToValidate="Tbx_eMail" Display="Dynamic" 
                                    ErrorMessage=" Se requiere capturar el Correo electrónico">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Lbl_eMailConf" runat="server" Text="Confirmación correo electrónico"></asp:Label><asp:CompareValidator ID="CompareValidator1" 
                                    runat="server" ControlToCompare="Tbx_eMailConf" ControlToValidate="Tbx_eMail" 
                                    ErrorMessage="No coincide el correo electrónico">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Tbx_eMail" runat="server" Width="199px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="Tbx_eMailConf" runat="server" Width="199px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Lbl_Telefono" runat="server" Text="Teléfono de oficina con lada"></asp:Label><asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator4" runat="server" 
                                    ControlToValidate="Tbx_Telefono" 
                                    ErrorMessage=" Se requiere capturar el Teléfono">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Lbl_Extencion" runat="server" Text="Extensión"></asp:Label></td>
                            <td>
                                <asp:Label ID="Lbl_Celular" runat="server" Text="Teléfono celular con lada"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Tbx_Telefono" runat="server" Width="199px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="Tbx_Extencion" runat="server" Width="138px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="Tbx_Celular" runat="server" Width="199px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Lbl_Puesto" runat="server" Text="Puesto"></asp:Label></td>
                            <td>
                                <asp:Label ID="Lbl_Sector" runat="server" Text="Sector"></asp:Label></td>
                            <td>
                                <asp:Label ID="Lbl_Sitio" runat="server" Text="Sitio de instalación"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Tbx_Puesto" runat="server" Width="199px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="Cbx_Sector" runat="server" Width="145px">
                                    <asp:ListItem>Privado</asp:ListItem>
                                    <asp:ListItem>Publico</asp:ListItem>
                                    <asp:ListItem>Otro</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="Cbx_Sitio" runat="server"  Width="121px">
                                    <asp:ListItem>Oficina</asp:ListItem>
                                    <asp:ListItem>Fábrica</asp:ListItem>
                                    <asp:ListItem>Escuela</asp:ListItem>
                                    <asp:ListItem>Hospital</asp:ListItem>
                                    <asp:ListItem>Comercio</asp:ListItem>
                                    <asp:ListItem>Laboratorio</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Lbl_Calle" runat="server" Text="Calle y número"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Tbx_Calle" runat="server" Width="610px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Lbl_Colonia" runat="server" Text="Colonia"></asp:Label></td>
                            <td>
                                <asp:Label ID="Lbl_Delegacion" runat="server" Text="Delegación"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Tbx_Colonia" runat="server" Width="303px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="Tbx_Delegacion" runat="server" Width="303px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;" >
                        <tr>
                            <td>
                                <asp:Label ID="Lbl_Estado" runat="server" Text="Estado"></asp:Label></td>
                            <td>
                                <asp:Label ID="Lbl_CP" runat="server" Text="Código postal"></asp:Label></td>
                            <td>
                                <asp:Label ID="Lbl_Pais" runat="server" Text="Pais"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Tbx_Estado" runat="server" Width="313px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="Tbx_CP" runat="server" Width="91px"></asp:TextBox>
                            </td>
                            <td>
                            <asp:DropDownList ID="Cbx_Pais" runat="server" Width="145px">
                                    <asp:ListItem>Otro</asp:ListItem>
                                    <asp:ListItem>Andorra</asp:ListItem>
                                    <asp:ListItem>Argentina</asp:ListItem>
                                    <asp:ListItem>Bolivia</asp:ListItem>
                                    <asp:ListItem>Brasil</asp:ListItem>
                                    <asp:ListItem>Chile</asp:ListItem>
                                    <asp:ListItem>Colombia</asp:ListItem>
                                    <asp:ListItem>C.Verde</asp:ListItem>
                                    <asp:ListItem>Costa Rica</asp:ListItem>
                                    <asp:ListItem>Cuba</asp:ListItem>
                                    <asp:ListItem>España</asp:ListItem>
                                    <asp:ListItem>Ecuador</asp:ListItem>
                                    <asp:ListItem>Filipinas</asp:ListItem>
                                    <asp:ListItem>Guatemala</asp:ListItem>     
                                    <asp:ListItem>Honduras</asp:ListItem>
                                    <asp:ListItem>México</asp:ListItem>                                    
                                    <asp:ListItem>Nicaragua</asp:ListItem>
                                    <asp:ListItem>Panama</asp:ListItem>     
                                    <asp:ListItem>Paraguay</asp:ListItem>
                                    <asp:ListItem>Peru</asp:ListItem>
                                    <asp:ListItem>Portugal</asp:ListItem>                                                                                                                                                                                                                                                     
                                    <asp:ListItem>Puerto Rico</asp:ListItem>          
                                    <asp:ListItem>Republica Domominicana</asp:ListItem>
                                    <asp:ListItem>Salvador</asp:ListItem>
                                    <asp:ListItem>USA</asp:ListItem>
                                    <asp:ListItem>Uruguay</asp:ListItem>
                                    <asp:ListItem>Venezuela</asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>            
                        <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;" >
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Lbl_NoEmpleados" runat="server" Text="Número de empleados"></asp:Label></td>
                            <td class="style2">
                                <igtxt:WebNumericEdit ID="Tbx_NoEmpleados" runat="server" Width="70px">
                                </igtxt:WebNumericEdit>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator6" runat="server" 
                                    ControlToValidate="Tbx_NoEmpleados" 
                                    ErrorMessage="Requiere capturar el número de empleados que checaran" 
                                    EnableClientScript="False">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Lbl_Intereses" runat="server" Text="Tipo de uso"></asp:Label><asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="Obt_Intereses" 
                                    ErrorMessage="Se requiere elegir el tipo de interes">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="Obt_Intereses" runat="server" RepeatColumns="3" 
                                    Width="100%">
                                    <asp:ListItem>Control de Asistencia</asp:ListItem>
                                    <asp:ListItem>Control de Acceso</asp:ListItem>
                                    <asp:ListItem>Control de Acceso y Asistencia</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Lbl_Tecnologias" runat="server" Text="Preferiria las siguietes tecnologías"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBoxList ID="Cbl_Tecnologias" runat="server" RepeatColumns="3" 
                                    Width="100%">
                                    <asp:ListItem>Huella Digital</asp:ListItem>
                                    <asp:ListItem>Rostro</asp:ListItem>
                                    <asp:ListItem>Palma</asp:ListItem>
                                    <asp:ListItem>Tarjeta de proximidad</asp:ListItem>
                                    <asp:ListItem>Código de Barras</asp:ListItem>
                                    <asp:ListItem>Banda Magnética</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;" >
                        <tr>
                            <td class="style1">
                    <asp:CheckBox ID="Cbx_Sucursales" runat="server" 
                        Text="Se instalará en varias sucursales" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1" style="text-align: right">
                                <asp:Label ID="Lbl_NoSucursales" runat="server" Text="No. de sucursales"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="Tbx_NoSucursales" runat="server" Width="91px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Lbl_Comentarios" runat="server" Text="Comentarios"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Tbx_Comentarios" runat="server" Height="79px" TextMode="MultiLine" 
                                    Width="607px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        HeaderText="Para continuar corrija los siguientes errores:" />
                    <asp:Label ID="Lbl_Correcto" runat="server" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="Contactar" 
                        onclick="Button1_Click" UseSubmitBehavior="False" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
