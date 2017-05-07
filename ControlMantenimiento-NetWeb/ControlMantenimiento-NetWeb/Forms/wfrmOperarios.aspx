<%@ Page Language="C#" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="true" CodeBehind="wfrmOperarios.aspx.cs" Inherits="ControlMantenimiento_NetWeb.Forms.wfrmOperarios" %>
                                                         
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     

  <form id="frmOPerarios" runat="server" class="formoid-solid-green" style="background-color:#FFFFFF;font-size:12px;font-family:'Roboto',Arial,Helvetica,sans-serif;
    color:#34495E;max-width:580px;min-width:150px">
           
      <div class="title"><h2 align="left"><i class="fa fa-group"></i>&#160;Operarios</h2></div> 
      
      <br />
             <asp:Image ID="imgFoto" runat="server" />
       <br />               

      <div class="element-input" align="left">
           <label class="title"><span class="required">Documento *</span></label>
           <div class="item-cont" align="left">
                <asp:TextBox ID="txtDocumento" runat="server" MaxLength="10" required="required" placeholder="Solo Numeros" onkeypress="return ValidarNumeros(event)" onpaste="return false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDocumento" ErrorMessage="Campo Requerido" ValidationGroup="Operario" Font-Bold="False"></asp:RequiredFieldValidator>
                <span class="icon-place"></span>
           </div>
      </div>

	  <div class="element-name" align="left">
           <label class="title"><span class="required">Nombres *</span></label>
           <div class="nameFirst" align="left">                
                <asp:TextBox ID="txtNombres" runat="server" MaxLength="25" required="required" onpaste="return false" placeholder="Nombres del Operario" Width="265px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombres" ErrorMessage="Campo Requerido" ValidationGroup="Operario" Font-Bold="False"></asp:RequiredFieldValidator>
                <span class="icon-place"></span>
           </div>
       </div>

       <div class="element-name" align="left">
           <label class="title"><span class="required">Apellidos *</span></label>
           <div class="nameFirst" align="left">                
                <asp:TextBox ID="txtApellidos" runat="server" MaxLength="25" required="required" onpaste="return false" placeholder="Apellidos del Operario" Width="265px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtApellidos" ErrorMessage="Campo Requerido" ValidationGroup="Operario" Font-Bold="False"></asp:RequiredFieldValidator>
                <span class="icon-place"></span>
           </div>
       </div>

	<div class="element-phone" align="left">
         <label class="title"><span class="required">Telefono *</span></label>
         <div class="item-cont" align="left">              
              <asp:TextBox ID="txtTelefono" runat="server" onkeypress="return ValidarNumeros(event)" placeholder="Solo Numeros 7 o 10" onpaste="return false" MaxLength="10" required="required"></asp:TextBox> 
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTelefono" ErrorMessage="Campo Requerido" ValidationGroup="Operario" Font-Bold="False"></asp:RequiredFieldValidator>
              <span class="icon-place"></span>
         </div>
    </div>

	<div class="element-email" align="left">
         <label class="title">Email</label>
         <div class="item-cont"
          align="left">              
              <asp:TextBox ID="txtCorreo" runat="server" MaxLength="50" placeholder="Correo Electronico" Width="265px"></asp:TextBox>              
              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCorreo" ErrorMessage="Correo con formato errado" Font-Bold="False" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Operario"></asp:RegularExpressionValidator>
              <span class="icon-place"></span>
         </div>
    </div>

    <div class="element-password">
         <label class="LabelCell"><span class="required">Clave *</span></label>
         <div class="item-cont" align="left">
              <asp:TextBox ID="txtClave" runat="server" MaxLength="20" TextMode="Password" placeholder="Password" ValidationGroup="Acceso" required="required"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtClave" ErrorMessage="Campo Requerido" ValidationGroup="Acceso" Font-Bold="False"></asp:RequiredFieldValidator>
              <span class="icon-place"></span>
         </div>
    </div>
    
    <div class="element-select" align="left">
         <label class="LabelCell">Perfil *</label>
         <div class="item-cont">  
              <div class="medium">
                   <span>
                        <asp:DropDownList ID="dplPerfil" runat="server">
                             <asp:ListItem Value="3">Solo Lectura</asp:ListItem>
                             <asp:ListItem Value="2">Lectura Escritura</asp:ListItem>
                        </asp:DropDownList>
                   </span>
              </div>
              <span class="icon-place"></span>
         </div>
    </div>


    	     <div class="element-file" align="left">
                  <label class="title">Foto</label>
                 <div class="item-cont">
                      <asp:FileUpload ID="FileUpload1" runat="server"/>                                             
                      <span class="icon-place"></span>
                 </div>                
             </div>           

             
         
    <div>        
           <asp:TextBox ID="txtMensajeError" runat="server" MaxLength="90" Width="409px" BorderStyle="None" ForeColor="Red" ReadOnly="True" Height="21px"></asp:TextBox>
    </div>
          
    
    <div class="submit">
         <asp:Button ID="btnGrabar" runat="server" Text="Grabar" ValidationGroup="Operarios" onclick="btnGrabar_Click"  OnClientClick="return VerificarOperarios()"/>  
    </div>             
    

</form>


   
</asp:Content>