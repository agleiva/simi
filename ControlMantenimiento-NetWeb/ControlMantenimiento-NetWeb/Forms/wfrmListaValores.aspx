<%@ Page Language="C#" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="true" CodeBehind="wfrmListaValores.aspx.cs" Inherits="ControlMantenimiento_NetWeb.Forms.wfrmListaValores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


      <form id="frmListaValores" runat="server" class="formoid-solid-green" style="background-color:#FFFFFF;font-size:14px;font-family:'Roboto',Arial,Helvetica,sans-serif;
          color:#34495E;max-width:480px;min-width:150px">
          
          <div class="title">
               <% if (lblTitulo.Text=="MARCAS") { %>
               <h2><i class="fa fa-registered"></i>&#160;<asp:Label ID="Label2" runat="server" ForeColor="White">MARCAS</asp:Label></h2>
               <% } %>
               <% else{ %>
               <h2><i class="fa fa-list"></i>&#160;<asp:Label ID="lblTitulo" runat="server" ForeColor="White">LINEAS</asp:Label></h2>
               <% } %>
          </div>                    
  
          <div>        
                <label class="LabelCell">Codigo <asp:Label ID="lblCodigo" runat="server">0</asp:Label></label> 
          </div>
          
          <br/>
          <div class="element-input">                      
                 <label class="LabelCell"><span class="required">Nombre *</span></label>
                 <div class="item-cont" align="left">
                      <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" placeholder="Nombre" required="required" ValidationGroup="ListaValores"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre" ErrorMessage="Campo Requerido" ValidationGroup="ListaValores" Font-Bold="False"></asp:RequiredFieldValidator>
                      <span class="icon-place"></span>
                 </div>
          </div>

          <div class="element-textarea">                      
                 <label class="LabelCell"><span class="required">Descripcion</span></label>
                 <div class="item-cont" align="left">
                      <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="255" 
                          placeholder="Alguna Descripcion" TextMode="MultiLine"></asp:TextBox>
                      <span class="icon-place"></span>
                 </div>
          </div>

          <div>
                 <asp:TextBox ID="txtMensajeError" runat="server" MaxLength="90" Width="409px" BorderStyle="None" ForeColor="Red" ReadOnly="True" Height="21px"></asp:TextBox>
          </div>
          <div class="submit">
               <asp:Button ID="btnGrabar" runat="server" Text="Grabar" ValidationGroup="ListaValores" onclick="btnGrabar_Click"  OnClientClick="return VerificarListaValores()"/>                    
          </div>             
         
    </form>
</asp:Content>