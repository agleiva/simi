<%@ Page Language="C#" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="true" CodeBehind="wfrmBusquedas.aspx.cs" Inherits="ControlMantenimiento_NetWeb.Forms.wfrmBusquedas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #formLists
        {
            margin-left: 234px;
        }
    </style>
    
   
    <script src="../JavaScript/ValidacionesPaginas.js" type="text/javascript"></script>
  
    <link href="../assets/css/material-dashboard.css" rel="stylesheet" type="text/css"/>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
   
       
    <form id="formLists"  
        runat="server">

    
                     
         <div align="center"> 
             
             <asp:Button ID="btnNuevo"                               
                              runat="server" 
                              Text="Nuevo" onclick="btnNuevo_Click" BackColor="#FFFF99">                              
             </asp:Button>

             <asp:Button ID="btnBuscar"                               
                              runat="server" 
                              Text="Buscar"
                              onclick="btnBuscar_Click">
             </asp:Button>  
              
             <asp:TextBox ID="txtBuscar" 
                               runat="server"     
                               onkeypress="return ValidarNumeros(event)" 
                               onpaste="return false"                                       
                               MaxLength="10" Width="72px"></asp:TextBox>
            
                 
              &nbsp;
                           
               <asp:Label ID="LabelInformacion" 
                          runat="server" 
                          Text="." 
                          ForeColor="#0000CC">
               </asp:Label>         
         </div>
        
         &nbsp;
        
          
                             
            <div align="center">   
              <asp:Repeater ID="Repeater1" 
                            runat="server" 
                            onitemcommand="Repeater1_ItemCommand" EnableTheming="True">

                <HeaderTemplate>
                       <table cellspacing="0" rules="all" border="1">
                                <tr>
                                     <th bgcolor="teal">Codigo</th>
                                     <th bgcolor="teal">Detalles</th>
                                     <th bgcolor="teal">Accion</th>
                                </tr>
                        </HeaderTemplate>

                        <ItemTemplate>
                          <tr>
                              <td style="color: #000000" align="left">
                                  <asp:Label runat="server" 
                                             ID="Label1" 
                                             text='<%# Eval("Codigo") %>' />
                              </td>
              
                              <td style="color: #000000" align="left">
                                  <asp:Label runat="server"
                                             ID="Label2" 
                                             text='<%# Eval("Detalle") %>' />
                              </td>

                              <td align="left"> 
                                   <asp:Button ID="ButtonEdit" 
                                               CommandArgument='<%# Eval("Codigo") %>'  
                                               CommandName="Editar" 
                                               runat="server" 
                                               class="btn btn-warning btn-xs"
                                               Text="Editar"/>

                                   <asp:Button ID="ButtonDelete" 
                                               CommandArgument='<%# Eval("Codigo") %>'  
                                               CommandName="Eliminar" 
                                               runat="server" 
                                               class="btn btn-danger btn-xs"
                                               Text="Eliminar" OnClientClick="return ConfirmarBorrado()"/>
                              </td>
              
                          </tr>
                        </ItemTemplate>
                
                         <FooterTemplate>
                      </table>
                  </FooterTemplate>
              </asp:Repeater>
           </div>          

     </form>  
     
</asp:Content>