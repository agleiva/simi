<%@ Page Language="C#" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="true" CodeBehind="wfrmMenu.aspx.cs" Inherits="ControlMantenimiento_NetWeb.Forms.wfrmMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/material-dashboard.css" rel="stylesheet" type="text/css"/>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >


    <form id="frmMenu" runat="server">             
    
             <div class="wrapper">
                   
                  <div class="sidebar">
                       <div align="left"><asp:Label ID="lblUsuarioConectado" runat="server" Text="lblUsuarioConectado"></asp:Label></div>
                       
                       <div class="sidebar-wrapper">
                                           
                                <ul class="nav">
                                    <li>
                                         <i class="fa fa-group"></i>                                     
                                        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Operarios</asp:LinkButton>                                    
                                     </li>

                                    <li>
                                         <i class="fa fa-wrench"></i>                             
                                        <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Equipos</asp:LinkButton>                                         
                                     </li>

                                     <li>
                                         <i class="fa fa-registered"></i>                         
                                         <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click">Marcas</asp:LinkButton>                                       
                                     </li>

                                     <li>
                                         <i class="fa fa-list"></i>                                      
                                         <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click">Lineas</asp:LinkButton>                                
                                     </li>
                                    
                                     <li>
                                         <i class="fa fa-calendar"></i>                
                                         <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton5_Click">Mantenimiento</asp:LinkButton>                                                        
                                     </li>
                                                                        
                                    
                                    <li>  
                                          <i class="fa fa-key"></i>
                                        <asp:LinkButton ID="LinkButton6" runat="server" onclick="LinkButton6_Click">Cambiar Clave</asp:LinkButton>
                                    </li>
                                </ul>
                            
                       </div>
                  </div>
             </div>          
                      
          
       
          <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/SIMI-LOGO.jpg" 
             style="position: relative; top: -633px; left: 160px; height: 378px; width: 755px" />
        
      
  

         
    </form>
</asp:Content>
