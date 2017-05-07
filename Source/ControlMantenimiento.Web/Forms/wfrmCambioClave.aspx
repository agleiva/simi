<%@ Page Language="C#" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="true" CodeBehind="wfrmCambioClave.aspx.cs" Inherits="ControlMantenimiento_NetWeb.Forms.wfrmCambioClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <form id="frmCambioClave" runat="server" class="formoid-solid-green" style="background-color:#FFFFFF;font-size:14px;font-family:'Roboto',Arial,Helvetica,sans-serif;
          color:#34495E;max-width:480px;min-width:150px">
          
          <div class="title"><h2 align="left"><i class="fa fa-key"></i>&#160;Cambio de Clave</h2></div>
      
          <div class="element-password">
                 <label class="LabelCell"><span class="required">Clave Actual *</span></label>
                 <div class="item-cont" align="left">
                      <asp:TextBox ID="txtClave" runat="server" MaxLength="20" TextMode="Password" placeholder="Password" ValidationGroup="Acceso" required="required"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClave" ErrorMessage="Campo Requerido" ValidationGroup="CambioClave" Font-Bold="False"></asp:RequiredFieldValidator>
                      <span class="icon-place"></span>
                 </div>
          </div>
          <div class="element-password">
                 <label class="LabelCell"><span class="required">Clave Nueva *</span></label>
                 <div class="item-cont" align="left">
                      <asp:TextBox ID="txtClaveNueva" runat="server" MaxLength="20" TextMode="Password" placeholder="Nuevo Password" ValidationGroup="Acceso" required="required"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtClaveNueva" ErrorMessage="Campo Requerido" ValidationGroup="CambioClave" Font-Bold="False"></asp:RequiredFieldValidator>
                      <span class="icon-place"></span>
                 </div>
          </div>
          <div class="element-password">
                 <label class="LabelCell"><span class="required">Confirmar *</span></label>
                 <div class="item-cont" align="left">
                      <asp:TextBox ID="txtConfirmar" runat="server" MaxLength="20" TextMode="Password" placeholder="Confirmar Password" ValidationGroup="Acceso" required="required"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmar" ErrorMessage="Campo Requerido" ValidationGroup="CambioClave" Font-Bold="False"></asp:RequiredFieldValidator>
                      <span class="icon-place"></span>
                 </div>
          </div>
          <div>
                 <asp:TextBox ID="txtMensajeError" runat="server" MaxLength="90" Width="409px" BorderStyle="None" ForeColor="Red" ReadOnly="True" Height="21px"></asp:TextBox>
          </div>
          <div class="submit">
               <asp:Button ID="btnGrabar" runat="server" Text="Grabar" ValidationGroup="CambioClave" onclick="btnGrabar_Click"  OnClientClick="return VerificarCambioClave()"/>                    
          </div>             
   
    </form>
</asp:Content>
