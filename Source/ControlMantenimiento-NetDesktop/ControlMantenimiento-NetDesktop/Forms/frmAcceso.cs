using System;
using System.Data;
using System.Windows.Forms;
using ControlMantenimiento.Model;
using ControlMantenimiento_NetDesktop.BLL;


namespace ControlMantenimiento_NetDesktop
{
    public partial class frmAcceso : Form
    {
        public frmAcceso()
        {
            InitializeComponent();
            var blankContextMenu = new ContextMenuStrip();
            txtDocumento.ContextMenuStrip = blankContextMenu;           
            this.txtDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDocumento_KeyPress);
            this.txtClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClave_KeyPress);
        }
      
        private bool Ingresar;
        private int  ContadorIntentos = 0;
        private KeyPressEventArgs Tecla = new KeyPressEventArgs('\r'); // Send Enter

        private void txtDocumento_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                if (string.IsNullOrEmpty(txtDocumento.Text))
                {
                    Ingresar = false;
                    MessageBox.Show(BLL.Mensajes.MensajeCampoRequerido, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDocumento.Focus();
                    errorPro.SetError(txtDocumento, ControlMantenimiento_NetDesktop.BLL.Mensajes.MensajeCampoRequerido);
                }
                else if (txtDocumento.Text.Length < 6)
                {
                    Ingresar = false;
                }
                else
                {
                    errorPro.Clear();
                    txtClave.Focus();
                }
            }
            else
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true; // Remover el caracter
                }
            }
        }

        private void txtClave_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                txtClave.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtClave.Text);
                if (string.IsNullOrEmpty(txtClave.Text))
                {
                    Ingresar = false;
                    MessageBox.Show(BLL.Mensajes.MensajeCampoRequerido, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClave.Focus();
                    errorPro.SetError(txtClave, BLL.Mensajes.MensajeCampoRequerido);
                }
                else if (txtDocumento.Text.Length < 6)
                {
                    Ingresar = false;
                }
                else
                {
                    errorPro.Clear();
                    btnIngresar.Focus();
                }
            }
        }

        
        
        private void btnIngresar_Click(object sender, EventArgs e)
        {          
          Intentos(); // Incrementar contador de intentos de acceso
          Ingresar = true;
          txtDocumento_KeyPress(btnIngresar, Tecla);
          if (Ingresar)
          {
              txtClave_KeyPress(btnIngresar, Tecla);
              if (Ingresar)
              {
                  var controlador = Funciones.CrearControlador();
                  Operario operario = controlador.ObtenerAcceso(txtDocumento.Text, txtClave.Text);
                  if (operario != null)
                  {
                      Ingresar = true;
                      BLL.Funciones.UsuarioConectado = operario.Documento;
                      BLL.Funciones.PerfilAcceso = operario.Perfil;
                      BLL.Funciones.NombreUsuario = operario.Nombres + " " + operario.Apellidos;
                      ControlMantenimiento_NetDesktop.frmMenu Form_Menu = new ControlMantenimiento_NetDesktop.frmMenu();
                      this.Close();
                      this.Dispose();
                      Form_Menu.ShowDialog();                       
                  }
                  else
                  {                      
                      MessageBox.Show(BLL.Mensajes.Mensaje2, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                      txtClave.Focus();
                      errorPro.SetError(txtClave, BLL.Mensajes.Mensaje2);                      
                  }
              }
          }
        }

        private void Intentos()
        {
            ContadorIntentos = (ContadorIntentos + 1);
            if (ContadorIntentos == 3)
            {
              Application.Exit(); // Usuario desconocido luego de 3 intentos, finalizar
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            BLL.Funciones.LimpiarForma(panel2);
            txtDocumento.Focus(); 
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            /* System.Diagnostics.Process proc = new System.Diagnostics.Process();
             proc.EnableRaisingEvents = false;
             proc.StartInfo.FileName = "E:/Fuentes CM/ControlMantenimiento-NetDesktop/ControlMantenimiento-NetDesktop/Ayudas/Ayuda.chm";
             proc.Start();
             proc.Dispose();*/

            // Estas líneas son las encargadas de llamar el archivo de ayudas .chm, está en comentario para que usted le coloque la ruta
            // donde descomprimió el archivo descargado de la web
        }                     
    }
}
