using System;
using System.Data;
using System.Windows.Forms;
using ControlMantenimiento.Model;
using ControlMantenimiento_NetDesktop.BLL;

namespace ControlMantenimiento_NetDesktop
{
    public partial class frmListaValores : Form
    {
        public frmListaValores()
        {
            InitializeComponent();
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            this.lstListaValores.Click += new System.EventHandler(this.lstListaValores_Click);
        }

        private Controlador _controlador = Funciones.CrearControlador();
        private bool Grabar;
        private KeyPressEventArgs Tecla = new KeyPressEventArgs('\r'); // Send Enter
       
        private void FrmListaValores_Load(object sender, EventArgs e)
        {
            this.Text = BLL.Funciones.ValorTipo.ToLower();
            this.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(this.Text);
            CargarListaSeleccion();
        }

        private void CargarListaSeleccion()
        {
            lstListaValores.ValueMember = "CODIGO";
            lstListaValores.DisplayMember = "DETALLE";
            
            if (BLL.Funciones.ValorTipo == "LINEAS")
            {
                lstListaValores.DataSource = _controlador.CargarListas("CLineas");
            }
            else
            {
                lstListaValores.DataSource = _controlador.CargarListas("CMarcas");
            }
        }

        private void txtNombre_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.MensajeCampoRequerido, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombre.Focus();
                    errorPro.SetError(txtNombre, BLL.Mensajes.MensajeCampoRequerido);
                }
                else
                {
                    txtDescripcion.Focus();                    
                }
            }
        }

        private void txtDescripcion_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                ControlMantenimiento.Business.Funciones.EliminarTabulador(txtDescripcion.Text, "");
                btnGrabar.Focus();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Grabar = true;
            txtNombre_KeyPress(btnGrabar, Tecla);
            if (Grabar)
            {  
               Guardar(Convert.ToInt32(lblCodigo.Text),  (lblCodigo.Text == "0") ? BLL.Mensajes.MensajeGraba : BLL.Mensajes.MensajeActualiza);               
            }
        }

        private void Guardar(int ElCodigo, string Mensaje)
        {
            int Resultado;
            ListaValores listavalores = new ListaValores();
            listavalores.Codigo = ElCodigo;
            listavalores.Nombre = txtNombre.Text;
            listavalores.Descripcion = txtDescripcion.Text;
            listavalores.Tipo = BLL.Funciones.ValorTipo;
            
            Resultado = _controlador.Guardar(listavalores);
            if (Resultado == 0)
            {
                MessageBox.Show(Mensaje, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarListaSeleccion();
                Limpiar();
            }
            else if (Resultado == 1)
            {
              MessageBox.Show(BLL.Mensajes.Mensaje8, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
              txtNombre.Focus();
              errorPro.SetError(txtNombre, BLL.Mensajes.Mensaje8);
            }
            else
            {
                MessageBox.Show(BLL.Mensajes.MensajeErrorBD, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (lstListaValores.Items.Count != 0)
            {
                lstListaValores.Visible = true;
            }
        }

        private void lstListaValores_Click(object sender, EventArgs e)
        {
            lblCodigo.Text = lstListaValores.SelectedValue.ToString();
            lstListaValores.Visible = false;
            LlenarCampos();
        }

        private void LlenarCampos()
        {
            ListaValores listavalores = (ListaValores) _controlador.ObtenerRegistro(lblCodigo.Text,"L");
            if (listavalores != null)
            {
                btnEliminar.Enabled = true;
                txtNombre.Text = listavalores.Nombre;
                txtDescripcion.Text = listavalores.Descripcion;
                txtNombre.Focus();
            }
        }

        private void Limpiar()
        {
            BLL.Funciones.LimpiarForma(panel2);
            lstListaValores.Visible = false;
            btnEliminar.Enabled = false;
            errorPro.Clear();
            lblCodigo.Text = "0";
            txtNombre.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {            
            Limpiar();
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
                Resultado = _controlador.EliminarRegistro(lblCodigo.Text, "LISTAVALORES");
                if (Resultado == 0)
                {
                    MessageBox.Show(BLL.Mensajes.MensajeBorrado, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarListaSeleccion();
                    Limpiar();
                }
                else if (Resultado == 1)
                {
                    MessageBox.Show(BLL.Mensajes.Mensaje9, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(BLL.Mensajes.MensajeErrorBD, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
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
