<%@ Page Language="C#" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="true" CodeBehind="wfrmError.aspx.cs" Inherits="ControlMantenimiento_NetWeb.Forms.wfrmError" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
     <form id="frmError" runat="server" class="formoid-solid-green" style="background-color:#FFFFFF;font-size:14px;font-family:'Roboto',Arial,Helvetica,sans-serif;
           color:#34495E;max-width:480px;min-width:150px">
            <div class="title"><h2><asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Error.png"/></h2></div>
            
            <br /><br />

            <div>            
                  <asp:TextBox ID="txtMensajeError" runat="server" MaxLength="90" Width="473px" BorderStyle="None" ForeColor="Red" ReadOnly="True" Height="21px" 
                                    style="text-align: center">No se pudo procesar la solicitud</asp:TextBox>
            </div>

            <div>
                 <asp:HyperLink ID="hplMenu" runat="server" NavigateUrl="~/Forms/WfrmMenu.aspx" 
                     ForeColor="#0066FF">Regresar Menu</asp:HyperLink>
            </div>
    
                
    </form>
</asp:Content>

