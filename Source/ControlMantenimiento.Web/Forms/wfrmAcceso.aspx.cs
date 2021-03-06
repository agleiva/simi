﻿using System;
using ControlMantenimiento.Business;
using ControlMantenimiento.Model;
using Funciones = ControlMantenimiento_NetWeb.BLL.Funciones;


namespace ControlMantenimiento_NetWeb.Forms
{
    public partial class wfrmAcceso : System.Web.UI.Page
    {
        private bool Verificar()
        {
            txtDocumento.Text = txtDocumento.Text.Trim();
            if (string.IsNullOrEmpty(txtDocumento.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtDocumento.Focus();
                return false;
            }
            if (txtDocumento.Text.Length < 6)
            {
                txtMensajeError.Text = Mensajes.Mensaje15;
                txtDocumento.Focus();
                return false;
            }
            if (txtDocumento.Text.Substring(0, 1) == "0")
            {
                txtMensajeError.Text = Mensajes.Mensaje6;
                txtDocumento.Focus();
                return false;
            }
            if (txtDocumento.Text.ValidarSoloNumeros())
            {
                /* Acá no es bueno mostrar mensaje de que están enviando caracteres
                  no numéricos en el documento, para evitar que programas
                  maliciosos y/o hackers hagan daño
                */
               // txtMensajeError.Text = Mensajes.Mensaje25;
                txtDocumento.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtClave.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtClave.Focus();
                return false;
            }
            if (txtClave.Text.Length < 6) // Solo realizar búsqueda en BD si clave ingresada es mayor de 6
            {
                txtClave.Focus();
                return false;
            }
            return true;
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (Verificar())
            {
                var controlador = Funciones.CrearControlador();
                Operario operario = controlador.ObtenerAcceso(txtDocumento.Text, txtClave.Text);
                if (operario == null)
                {
                    txtMensajeError.Text = Mensajes.Mensaje2;
                    txtClave.Focus();
                }
                else
                {
                    Session["TIPO_USUARIO"] = operario.Perfil;
                    BLL.Funciones.UsuarioConectado = operario.Documento;
                    BLL.Funciones.NombreUsuario = operario.Nombres + " " + operario.Apellidos;
                    BLL.Funciones.PerfilAcceso = operario.Perfil;
                    Response.Redirect("~/Forms/wfrmMenu.aspx");
                }
            }
        }

        protected void ImageButtonAyuda_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
           /* System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "E:/Fuentes CM/ControlMantenimiento-NetWeb/ControlMantenimiento-NetWeb/ControlMantenimiento-NetWeb/Ayudas/Ayuda.chm";
            proc.Start();
            proc.Dispose();*/

            // Estas líneas son las encargadas de llamar el archivo de ayudas .chm, está en comentario para que usted le coloque la ruta
            // donde descomprimió el archivo descargado de la web

        }

        

       
    }
}
