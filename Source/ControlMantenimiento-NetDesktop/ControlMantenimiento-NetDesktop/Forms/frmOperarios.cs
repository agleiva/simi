using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ControlMantenimiento.Model;
using ControlMantenimiento_NetDesktop.BLL;

namespace ControlMantenimiento_NetDesktop
{
    public partial class frmOperarios : Form
    {
        public frmOperarios()
        {
            InitializeComponent();
            var blankContextMenu = new ContextMenuStrip();            
            txtDocumento.ContextMenuStrip = blankContextMenu;
            txtNombres.ContextMenuStrip = blankContextMenu;
            txtApellidos.ContextMenuStrip = blankContextMenu;
            txtTelefono.ContextMenuStrip = blankContextMenu;

            // Habilitando teclas de Enter y Tab
            this.txtDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDocumento_KeyPress);
            this.txtNombres.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombres_KeyPress);
            this.txtApellidos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtApellidos_KeyPress);
            this.txtCorreo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCorreo_KeyPress);
            this.txtTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefono_KeyPress);
            this.txtClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClave_KeyPress);
            this.cboPerfil.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboPerfil_KeyPress);
            
        }

        private Controlador _controlador = Funciones.CrearControlador();
        private bool   Grabar;
        private int    TipoPerfil;
        private string RutaFoto = "";
        private KeyPressEventArgs Tecla = new KeyPressEventArgs('\r'); // Send Enter
        
        private void frmOperarios_Load(object sender, EventArgs e)
        {
            cboPerfil.SelectedIndex = 0;
            CargarListaSeleccion();            
        }

        private void CargarListaSeleccion()
        {           
            lstOperarios.ValueMember = "CODIGO";        // Documento de Operario a relacionar en BD
            lstOperarios.DisplayMember = "DETALLE";     // Visualizar Nombres y Apellidos de Operario
            
            lstOperarios.DataSource = _controlador.CargarListas("OPERARIOS");            
        }

        private void txtDocumento_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {           
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                if (string.IsNullOrEmpty(txtDocumento.Text))
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.MensajeCampoRequerido, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDocumento.Focus();
                    errorPro.SetError(txtDocumento, BLL.Mensajes.MensajeCampoRequerido);
                }
                else if (txtDocumento.Text.Length < 6)
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.Mensaje15, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDocumento.Focus();
                    errorPro.SetError(txtDocumento, BLL.Mensajes.Mensaje15);
                }
                else if (txtDocumento.Text.Substring(0, 1) == "0")
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.Mensaje6, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDocumento.Focus();
                    errorPro.SetError(txtDocumento, BLL.Mensajes.Mensaje6);
                }
                else
                {
                    errorPro.Clear();
                    if (txtDocumento.Enabled)
                    {
                        LlenarCampos();
                    }
                    else
                    {
                        txtNombres.Focus();
                    }                    
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

        private void txtNombres_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
        if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                txtNombres.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtNombres.Text);
                if (string.IsNullOrEmpty(txtNombres.Text))
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.MensajeCampoRequerido, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombres.Focus();
                    errorPro.SetError(txtNombres, BLL.Mensajes.MensajeCampoRequerido);
                }               
                else
                {
                    txtNombres.Text = ControlMantenimiento.Business.Funciones.EliminarTabulador(txtNombres.Text, "1MAY");
                    errorPro.Clear();
                    txtApellidos.Focus();
                }  
            }
            else 
            {
                if ((e.KeyChar < 65 || e.KeyChar > 97) && (e.KeyChar < 90 || e.KeyChar > 122) && e.KeyChar != 8 && e.KeyChar != 32)                
                {
                    e.Handled = true ; // Remover el caracter
                }
            }
        }

        private void txtApellidos_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                txtApellidos.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtApellidos.Text);
                if (string.IsNullOrEmpty(txtApellidos.Text))
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.MensajeCampoRequerido, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtApellidos.Focus();
                    errorPro.SetError(txtApellidos, BLL.Mensajes.MensajeCampoRequerido);
                }
                else
                {
                    txtApellidos.Text = ControlMantenimiento.Business.Funciones.EliminarTabulador(txtApellidos.Text, "1MAY");
                    errorPro.Clear();
                    txtCorreo.Focus();
                }
            }
            else
            {
                if ((e.KeyChar < 65 || e.KeyChar > 97) && (e.KeyChar < 90 || e.KeyChar > 122) && e.KeyChar != 8 && e.KeyChar != 32)
                {
                    e.Handled = true; // Remover el caracter
                }
            }
        }

        private void txtCorreo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                txtCorreo.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtCorreo.Text);
                if (txtCorreo.Text.Length != 0)
                {
                    txtCorreo.Text = txtCorreo.Text.ToLower();
                    if (ControlMantenimiento.Business.Funciones.Validar_Correo(txtCorreo.Text))                    
                    {
                        Grabar = false;
                        MessageBox.Show(BLL.Mensajes.Mensaje16, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCorreo.Focus();
                        errorPro.SetError(txtCorreo, BLL.Mensajes.Mensaje16);
                    }
                    else
                    {
                        errorPro.Clear();
                        txtTelefono.Focus();
                    }                    
                }
                else
                {
                    txtTelefono.Focus();
                }                
            }
        }

        private void txtTelefono_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != 8) // Habilitar tecla de retroceso
            {
                if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
                {
                    txtTelefono.Text = txtTelefono.Text.Trim(); // Controlar espacios en blanco                        
                    if (string.IsNullOrEmpty(txtTelefono.Text))
                    {
                        Grabar = false;
                        MessageBox.Show(BLL.Mensajes.MensajeCampoRequerido, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtTelefono.Focus();
                        errorPro.SetError(txtTelefono, BLL.Mensajes.MensajeCampoRequerido);
                    }
                    else
                    {
                        if ((txtTelefono.Text.Length != 7) && (txtTelefono.Text.Length != 10))
                        {
                            Grabar = false;
                            MessageBox.Show(BLL.Mensajes.Mensaje17, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtTelefono.Focus();
                            errorPro.SetError(txtTelefono, BLL.Mensajes.Mensaje17);
                        }
                        else if (txtTelefono.Text.Substring(0, 1) == "0")
                        {
                            Grabar = false;
                            MessageBox.Show(BLL.Mensajes.Mensaje6, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtTelefono.Focus();
                            errorPro.SetError(txtTelefono, BLL.Mensajes.Mensaje6);
                        }
                        else
                        {
                            errorPro.Clear();
                            txtClave.Focus();
                        }
                    }
                }
                else
                    if (e.KeyChar >= 48 && e.KeyChar <= 57)
                        e.Handled = false;
                    else if (e.KeyChar == 46)
                        e.Handled = false;
                    else
                        e.Handled = true;
            }
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
                else if (txtClave.Text.Length < 6)
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.Mensaje21, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClave.Focus();
                    errorPro.SetError(txtClave, BLL.Mensajes.Mensaje21);
                }
                else
                {
                    errorPro.Clear();
                    cboPerfil.Focus();
                }
            }
        }

        private void cboPerfil_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                btnGrabar.Focus();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
          Grabar = true;
          txtDocumento_KeyPress(btnGrabar,Tecla);
          if (Grabar)
          {
              txtNombres_KeyPress(btnGrabar, Tecla);         
              if (Grabar)
              {
                  txtApellidos_KeyPress(btnGrabar, Tecla);         
                  if (Grabar)
                  {
                      txtCorreo_KeyPress(btnGrabar, Tecla);
                      if (Grabar)
                      {
                        txtTelefono_KeyPress(btnGrabar, Tecla);
                        if (Grabar)
                        {
                            txtClave_KeyPress(btnGrabar, Tecla);
                            if (Grabar)
                            {
                                Guardar((txtDocumento.Enabled) ? "I" : "U", (txtDocumento.Enabled) ? BLL.Mensajes.MensajeGraba : BLL.Mensajes.MensajeActualiza);
                            }
                        }
                      }
                  }
              }
          }     
        }
        
        private void Guardar(string Accion, string Mensaje)
        {
            Operario operario = new Operario();
            operario.Documento = Convert.ToInt64(txtDocumento.Text);
            operario.Nombres = txtNombres.Text;
            operario.Apellidos = txtApellidos.Text;
            operario.Telefono = Convert.ToInt64(txtTelefono.Text);
            operario.Correo = txtCorreo.Text;
            operario.Clave = txtClave.Text;
            if (TipoPerfil != 1) //Proteger Perfil de Administrador
            {
                if (cboPerfil.Text == "Solo Consulta")
                {
                    TipoPerfil = 3;
                }
                else
                {
                    TipoPerfil = 2;
                }
            }
            operario.Perfil = TipoPerfil;
            operario.Foto = RutaFoto;

            if (_controlador.Guardar(operario, Accion) == 0)
            {
                MessageBox.Show(Mensaje, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarListaSeleccion();
            }
            else
            {
                MessageBox.Show(BLL.Mensajes.MensajeErrorBD, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Limpiar();
        }
      
        private void Limpiar()
        {
          btnEliminar.Enabled = false; 
          BLL.Funciones.LimpiarForma(panel2);
          errorPro.Clear();
          ptbFotoUsuario.Image = null;
          RutaFoto = "";
          TipoPerfil = 0;
          lstOperarios.Visible = false;          
          txtDocumento.Enabled = true; // Habilitar Campo de documento si esta Inactivo            
          txtDocumento.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {          
          Limpiar();
        }

        private void ptbFotoUsuario_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.Title = "Seleccionar imagen";
            oFD.Filter = "Imagenes|*.jpg;*.gif;*.png;*.bmp|Todos (*.*)|*.*";
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                RutaFoto = oFD.FileName;                
                if ((RutaFoto.Length)>50)
                {
                  MessageBox.Show(BLL.Mensajes.Mensaje28, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);                
                }
                else
                {
                  this.ptbFotoUsuario.Image = Image.FromFile(RutaFoto = oFD.FileName);
                }
            }
        }

        private void LlenarCampos()// Volcar datos de estrucura de Base da Datos sobre el Formulario actual
        {  
            Operario operario = (Operario) _controlador.ObtenerRegistro(txtDocumento.Text, "O"); 
            if (operario != null)
            {
                txtDocumento.Enabled = false;
                btnEliminar.Enabled = true;
                txtNombres.Text = operario.Nombres;
                txtApellidos.Text = operario.Apellidos;
                txtCorreo.Text = operario.Correo;
                txtTelefono.Text = operario.Telefono.ToString();
                txtClave.Text = operario.Clave;
                TipoPerfil = operario.Perfil;
                if (TipoPerfil == 3)
                {
                    cboPerfil.SelectedIndex = 0;
                }
                else
                {
                    cboPerfil.SelectedIndex = 1;
                }
                if (operario.Foto != null && operario.Foto != "")
                {
                    try
                    {
                        RutaFoto = operario.Foto;
                        ptbFotoUsuario.Image = Image.FromFile(RutaFoto);
                    }
                    catch
                    {
                        MessageBox.Show(BLL.Mensajes.Mensaje24, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }                
            }
            txtNombres.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            _controlador = null;
            this.Close();
            this.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int Resultado;
            if (MessageBox.Show(BLL.Mensajes.MensajeConfirmarBorrado, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                Resultado = _controlador.EliminarRegistro(txtDocumento.Text, "OPERARIOS");
                if (Resultado == 0)
               {
                   MessageBox.Show(BLL.Mensajes.MensajeBorrado, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                   CargarListaSeleccion();
                   Limpiar();
               }
                else if (Resultado == 1)
               {
                  MessageBox.Show(BLL.Mensajes.Mensaje20, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
               else
               {
                    MessageBox.Show(BLL.Mensajes.MensajeErrorBD, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
               }                
               
            }
        }
       
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (lstOperarios.Items.Count != 0)
            {
                lstOperarios.Visible = true;
            }            
        }

        private void lstOperarios_DoubleClick(object sender, EventArgs e)
        {
            txtDocumento.Text = lstOperarios.SelectedValue.ToString();
            lstOperarios.Visible = false;
            LlenarCampos();       
        }

        private void lblBorrarFoto_Click(object sender, EventArgs e)
        {
            ptbFotoUsuario.Image = null;
            RutaFoto = "";          
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
