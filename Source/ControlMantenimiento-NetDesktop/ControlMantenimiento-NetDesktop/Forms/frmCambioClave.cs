﻿using System;
using System.Data;
using System.Windows.Forms;
using ControlMantenimiento.Business;
using ControlMantenimiento.Model;
using Funciones = ControlMantenimiento_NetDesktop.BLL.Funciones;

namespace ControlMantenimiento_NetDesktop
{
    public partial class frmCambioClave : Form
    {
        public frmCambioClave()
        {
            InitializeComponent();
            this.txtClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClave_KeyPress);
            this.txtClaveNueva.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClaveNueva_KeyPress);
            this.txtConfirmar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConfirmar_KeyPress);
        }

        private Controlador _controlador = Funciones.CrearControlador();
        private bool Grabar;
        private Operario operario;
        private KeyPressEventArgs Tecla = new KeyPressEventArgs('\r'); // Send Enter

        private void frmCambioClave_Load(object sender, EventArgs e)
        {           
            operario = (Operario)_controlador.ObtenerRegistro(BLL.Funciones.UsuarioConectado.ToString(),"O");                  
        }

        private void txtClave_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                txtClave.Text = txtClave.Text.Trim();
                if (string.IsNullOrEmpty(txtClave.Text))
                {
                    Grabar = false;
                    MessageBox.Show(Mensajes.MensajeCampoRequerido, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClave.Focus();
                    errorPro.SetError(txtClave, Mensajes.MensajeCampoRequerido);
                }
                else if (txtClave.Text != operario.Clave) // Clave debe ser igual a la hallada en BD
                {
                    Grabar = false;
                    MessageBox.Show(Mensajes.Mensaje3, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClave.Focus();
                    errorPro.SetError(txtClave, Mensajes.Mensaje3);
                }
                else
                {
                    txtClaveNueva.Focus();
                }
            }
        }

        private void txtClaveNueva_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                txtClaveNueva.Text = txtClaveNueva.Text.Trim();
                if (string.IsNullOrEmpty(txtClaveNueva.Text))
                {
                    Grabar = false;
                    MessageBox.Show(Mensajes.MensajeCampoRequerido, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClaveNueva.Focus();
                    errorPro.SetError(txtClaveNueva, Mensajes.MensajeCampoRequerido);
                }
                else if (txtClaveNueva.Text == txtClave.Text) // Clave Nueva debe ser diferente de la actual
                {
                    Grabar = false;
                    MessageBox.Show(Mensajes.Mensaje4, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClaveNueva.Focus();
                    errorPro.SetError(txtClaveNueva, Mensajes.Mensaje4);
                }
                else if (txtClaveNueva.Text.Length < 6)
                {
                    Grabar = false;
                    MessageBox.Show(Mensajes.Mensaje21, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClaveNueva.Focus();
                    errorPro.SetError(txtClaveNueva, Mensajes.Mensaje21);
                }
                else
                {
                    errorPro.Clear();
                    txtConfirmar.Focus();
                }
            }
        }

        private void txtConfirmar_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                txtConfirmar.Text = txtConfirmar.Text.Trim();
                if (string.IsNullOrEmpty(txtConfirmar.Text))
                {
                    Grabar = false;
                    MessageBox.Show(Mensajes.MensajeCampoRequerido, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmar.Focus();
                    errorPro.SetError(txtConfirmar, Mensajes.MensajeCampoRequerido);
                }
                else if (txtConfirmar.Text != txtClaveNueva.Text) // Debe confirmar la clave y debe ser igual a Clave Nueva
                {
                    Grabar = false;
                    MessageBox.Show(Mensajes.Mensaje5, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmar.Focus();
                    errorPro.SetError(txtConfirmar, Mensajes.Mensaje5);
                }
                else
                {
                    errorPro.Clear();
                    btnGrabar.Focus();
                }
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Grabar = true;
            txtClave_KeyPress(btnGrabar, Tecla);
            if (Grabar)
            {
                txtClaveNueva_KeyPress(btnGrabar, Tecla);
                if (Grabar)
                {
                    txtConfirmar_KeyPress(btnGrabar, Tecla);
                    if (Grabar)
                    {
                        Guardar();
                    }
                }
            }
        }

        private void Guardar()
        {
            if (_controlador.GuardarCambioClave(txtClaveNueva.Text))
            {
                MessageBox.Show(Mensajes.MensajeActualiza, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {                
                MessageBox.Show(Mensajes.MensajeErrorBD, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
            btnSalir.PerformClick(); 
        }
                     
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            BLL.Funciones.LimpiarForma(panel2);
            errorPro.Clear();
            txtClave.Focus();
        }
              
        private void btnSalir_Click(object sender, EventArgs e)
        {
            _controlador = null;
            this.Close();
            this.Dispose();
        }

        

        
    }
}
