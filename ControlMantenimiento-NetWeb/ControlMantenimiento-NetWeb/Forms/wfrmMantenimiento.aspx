<%@ Page Language="C#" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="true" CodeBehind="wfrmMantenimiento.aspx.cs" Inherits="ControlMantenimiento_NetWeb.Forms.wfrmMantenimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <form id="frmMantenimiento" runat="server" class="formoid-solid-green" style="background-color:#FFFFFF;font-size:14px;font-family:'Roboto',Arial,Helvetica,sans-serif;
          color:#34495E;max-width:480px;min-width:150px">
          
          <div class="title"><h2 align="left"><i class="fa fa-calendar"></i>&#160;Mantenimiento</h2></div>  
          
          <div class="element-select">
               <label class="LabelCell"><span class="required">Equipo *</span></label>
               <div class="item-cont">  
                    <asp:DropDownList ID="dplEquipos" runat="server" Width="162px"></asp:DropDownList>
                    <span class="icon-place"></span>
                </div>
          </div>

          <div class="element-select">
               <label class="LabelCell"><span class="required">Operario *</span></label>
               <div class="item-cont">  
                    <asp:DropDownList ID="dplOperarios" runat="server" Width="162px"></asp:DropDownList>
                    <span class="icon-place"></span>
                </div>
          </div>        

          	<div class="element-date">
                 <label class="LabelCell">Fecha *</label>                 
                      <asp:Calendar ID="cdrFecha" runat="server" BackColor="#FFFFCC" 
                            BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                            Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" 
                            ShowGridLines="True" Width="220px">
                            <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                            <SelectorStyle BackColor="#FFCC66" />
                            <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                            <OtherMonthDayStyle ForeColor="#CC9966" />
                            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                            <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                            <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                      </asp:Calendar>                   
            </div>

          
          <div class="element-textarea">                      
                 <label class="LabelCell"><span class="required">Observaciones</span></label>
                 <div class="item-cont" align="left">
                      <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="255" placeholder="Alguna Observacion" TextMode="MultiLine"></asp:TextBox>
                      <span class="icon-place"></span>
                 </div>
          </div>

          <div>
                 <asp:TextBox ID="txtMensajeError" runat="server" MaxLength="90" Width="409px" BorderStyle="None" ForeColor="Red" ReadOnly="True" Height="21px"></asp:TextBox>
          </div>
          <div class="submit">
               <asp:Button ID="btnGrabar" runat="server" Text="Grabar" onclick="btnGrabar_Click"/>                                   
          </div>             
      
    </form>
</asp:Content>
