<%@ Page Language="C#" MasterPageFile="~/Forms/MasterPage.master"  AutoEventWireup="true" CodeBehind="wfrmAcceso.aspx.cs" Inherits="ControlMantenimiento_NetWeb.Forms.wfrmAcceso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >


     <form id="frmAcceso" runat="server" class="formoid-solid-green" style="background-color:#FFFFFF;font-size:14px;font-family:'Roboto',Arial,Helvetica,sans-serif;
    color:#34495E;max-width:480px;min-width:150px">
            <div class="title"><h2 align="left"><i class="fa fa-key"></i>&#160;Acceso</h2></div>
           	<div class="element-name" align="left">
                 <label class="LabelCell"><span class="required">Documento *</span></label>
                 <div class="nameFirst" align="left">
                      <asp:TextBox ID="txtDocumento" runat="server" MaxLength="10"  placeholder="Usuario" ValidationGroup="Acceso" required="required"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDocumento" ErrorMessage="Campo Requerido" ValidationGroup="Acceso" Font-Bold="False"></asp:RequiredFieldValidator>
                      <span class="icon-place"></span>
                 </div>
            </div>
	        <div class="element-password">
                 <label class="LabelCell"><span class="required">Clave *</span></label>
                 <div class="item-cont" align="left">
                      <asp:TextBox ID="txtClave" runat="server" MaxLength="20" TextMode="Password" placeholder="Password" ValidationGroup="Acceso" required="required"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtClave" ErrorMessage="Campo Requerido" ValidationGroup="Acceso" Font-Bold="False"></asp:RequiredFieldValidator>
                      <span class="icon-place"></span>
                 </div>
            </div>
            <div>
                 <asp:TextBox ID="txtMensajeError" runat="server" MaxLength="90" Width="409px" 
                     BorderStyle="None" ForeColor="Red" ReadOnly="True" Height="21px"></asp:TextBox>
            </div>
            <div class="submit">
                 <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" ValidationGroup="Acceso" onclick="btnIngresar_Click"  OnClientClick="return VerificarAcceso()"/>                    
            </div>             
    </form>
</asp:Content>



