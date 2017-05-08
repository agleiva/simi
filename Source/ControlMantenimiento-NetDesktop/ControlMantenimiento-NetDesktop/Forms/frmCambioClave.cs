using System;
using System.Data;
using System.Windows.Forms;
using ControlMantenimiento.Model;
using ControlMantenimiento_NetDesktop.BLL;

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

        private IControlador icontrolador = Funciones.CrearControlador();
        private bool Grabar;
        private Operario operario;
        private KeyPressEventArgs Tecla = new KeyPressEventArgs('\r'); // Send Enter

        private void frmCambioClave_Load(object sender, EventArgs e)
        {           
            operario = (Operario)icontrolador.ObtenerRegistro(BLL.Funciones.UsuarioConectado.ToString(),"O");                  
        }

        private void txtClave_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                txtClave.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtClave.Text);
                if (string.IsNullOrEmpty(txtClave.Text))
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.MensajeCampoRequerido, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClave.Focus();
                    errorPro.SetError(txtClave, BLL.Mensajes.MensajeCampoRequerido);
                }
                else if (txtClave.Text != operario.Clave) // Clave debe ser igual a la hallada en BD
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.Mensaje3, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClave.Focus();
                    errorPro.SetError(txtClave, BLL.Mensajes.Mensaje3);
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
                txtClaveNueva.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtClaveNueva.Text);
                if (string.IsNullOrEmpty(txtClaveNueva.Text))
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.MensajeCampoRequerido, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClaveNueva.Focus();
                    errorPro.SetError(txtClaveNueva, BLL.Mensajes.MensajeCampoRequerido);
                }
                else if (txtClaveNueva.Text == txtClave.Text) // Clave Nueva debe ser diferente de la actual
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.Mensaje4, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClaveNueva.Focus();
                    errorPro.SetError(txtClaveNueva, BLL.Mensajes.Mensaje4);
                }
                else if (txtClaveNueva.Text.Length < 6)
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.Mensaje21, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClaveNueva.Focus();
                    errorPro.SetError(txtClaveNueva, BLL.Mensajes.Mensaje21);
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
                txtConfirmar.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtConfirmar.Text);
                if (string.IsNullOrEmpty(txtConfirmar.Text))
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.MensajeCampoRequerido, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmar.Focus();
                    errorPro.SetError(txtConfirmar, BLL.Mensajes.MensajeCampoRequerido);
                }
                else if (txtConfirmar.Text != txtClaveNueva.Text) // Debe confirmar la clave y debe ser igual a Clave Nueva
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.Mensaje5, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmar.Focus();
                    errorPro.SetError(txtConfirmar, BLL.Mensajes.Mensaje5);
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
            if (icontrolador.GuardarCambioClave(txtClaveNueva.Text))
            {
                MessageBox.Show(BLL.Mensajes.MensajeActualiza, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {                
                MessageBox.Show(BLL.Mensajes.MensajeErrorBD, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            icontrolador = null;
            this.Close();
            this.Dispose();
        }

        

        
    }
}
