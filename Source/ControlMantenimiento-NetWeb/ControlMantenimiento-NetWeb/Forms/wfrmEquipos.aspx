<%@ Page Language="C#" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="true" CodeBehind="wfrmEquipos.aspx.cs" Inherits="ControlMantenimiento_NetWeb.Forms.wfrmEquipos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <form id="frmEquipos" runat="server" class="formoid-solid-green" style="background-color:#FFFFFF;font-size:14px;font-family:'Roboto',Arial,Helvetica,sans-serif;
          color:#34495E;max-width:480px;min-width:150px">
          
           <div class="title"><h2 align="left"><i class="fa fa-wrench"></i>&#160;Equipos</h2></div>    
          
          <div>
               <label class="LabelCell">Codigo <asp:Label ID="lblCodigo" runat="server">0</asp:Label></label> 
          </div>
          
          <br/>
          <div class="element-input">                      
                 <label class="LabelCell"><span class="required">Nombre *</span></label>
                 <div class="item-cont" align="left">
                      <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" placeholder="Nombre" required="required" ValidationGroup="Equipos"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre" ErrorMessage="Campo Requerido" ValidationGroup="Equipos" Font-Bold="False"></asp:RequiredFieldValidator>
                      <span class="icon-place"></span>
                 </div>
          </div>

          
          <div class="element-select">
               <label class="LabelCell"><span class="required">Marca *</span></label>
               <div class="item-cont">  
                    <div class="medium"><span>
                    <asp:DropDownList ID="dplMarcas" runat="server" Width="162px"></asp:DropDownList>
                    </span></div>
                    <span class="icon-place"></span>
                </div>
          </div>

          <div class="element-input">                      
                 <label class="LabelCell"><span class="required">Serie *</span></label>
                 <div class="item-cont" align="left">
                      <asp:TextBox ID="txtSerie" runat="server" MaxLength="50" placeholder="Serie Unica" required="required" ValidationGroup="Equipos"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSerie" ErrorMessage="Campo Requerido" ValidationGroup="Equipos" Font-Bold="False"></asp:RequiredFieldValidator>
                      <span class="icon-place"></span>
                 </div>
          </div>
          
          <div class="element-select">
               <label class="LabelCell"><span class="required">Linea *</span></label>
               <div class="item-cont">  
                    <div class="medium"><span>
                    <asp:DropDownList ID="dplLineas" runat="server" Width="162px"></asp:DropDownList>
                    </span></div>
                    <span class="icon-place"></span>
                </div>
          </div>

          <div class="element-checkbox" align="left">           
               <label class="LabelCell"><span class="required">Lubricacion</span></label>   
               <asp:CheckBox ID="chkLubricacion" runat="server"/>
               <span></span>
          </div>
                

          <div>
                 <asp:TextBox ID="txtMensajeError" runat="server" MaxLength="90" Width="409px" BorderStyle="None" ForeColor="Red" ReadOnly="True" Height="21px"></asp:TextBox>
          </div>
          <div class="submit">
               <asp:Button ID="btnGrabar" runat="server" Text="Grabar" ValidationGroup="Equipos" onclick="btnGrabar_Click"  OnClientClick="return VerificarEquipos()"/>                    
          </div>
    </form>
</asp:Content>

