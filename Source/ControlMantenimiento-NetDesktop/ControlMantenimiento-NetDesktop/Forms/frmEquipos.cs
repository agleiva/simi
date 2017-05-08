using System;
using System.Data;
using System.Windows.Forms;
using ControlMantenimiento_NetDesktop.BLL;
using ControlMantenimiento_NetDesktop.BO;

namespace ControlMantenimiento_NetDesktop
{
    public partial class frmEquipos : Form
    {
        public frmEquipos()
        {
            InitializeComponent();
            this.txtNombreEquipo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreEquipo_KeyPress);
            this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSerie_KeyPress);
            this.cboMarcas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboMarcas_KeyPress);
            this.cboLineas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboLineas_KeyPress);
        }

        private IControlador icontrolador = Funciones.CrearControlador();
        private bool Grabar;
        private KeyPressEventArgs Tecla = new KeyPressEventArgs('\r'); // Send Enter

        private void frmEquipos_Load(object sender, EventArgs e)
        {            
            icontrolador.ControlEquipos();
            lstEquipos.ValueMember = "CODIGO";
            lstEquipos.DisplayMember = "DETALLE";
            lstEquipos.DataSource = icontrolador.ObtenerListaEquipos();

            cboLineas.ValueMember = "CODIGO";
            cboLineas.DisplayMember = "DETALLE";
            cboLineas.DataSource = icontrolador.ObtenerListaLineas();

            cboMarcas.ValueMember = "CODIGO";
            cboMarcas.DisplayMember = "DETALLE";
            cboMarcas.DataSource = icontrolador.ObtenerListaMarcas();
        }

        private void CargarListaSeleccion()
        {
            lstEquipos.ValueMember = "CODIGO";
            lstEquipos.DisplayMember = "DETALLE";
            lstEquipos.DataSource = icontrolador.CargarListas("EQUIPOS");
        }

        private void txtNombreEquipo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                if (string.IsNullOrEmpty(txtNombreEquipo.Text))
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.MensajeCampoRequerido, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombreEquipo.Focus();
                    errorPro.SetError(txtNombreEquipo, BLL.Mensajes.MensajeCampoRequerido);
                }
                else
                {
                    txtNombreEquipo.Text = ControlMantenimiento.Business.Funciones.EliminarTabulador(txtNombreEquipo.Text, "MAY");
                    cboMarcas.Focus();
                }
            }
        }

        private void txtSerie_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                if (string.IsNullOrEmpty(txtSerie.Text))
                {
                    Grabar = false;
                    MessageBox.Show(BLL.Mensajes.MensajeCampoRequerido, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSerie.Focus();
                    errorPro.SetError(txtSerie, BLL.Mensajes.MensajeCampoRequerido);
                }
                else
                {
                    cboLineas.Focus();
                }
            }
        }

        private void cboMarcas_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                txtSerie.Focus();
            }
        }

        private void cboLineas_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                chkLubricacion.Focus();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Grabar = true;
            txtNombreEquipo_KeyPress(btnGrabar, Tecla);
            if (Grabar)
            {
                txtSerie_KeyPress(btnGrabar, Tecla);
                if (Grabar)
                {
                    Guardar(Convert.ToInt32(lblCodigo.Text), (lblCodigo.Text == "0") ? BLL.Mensajes.MensajeGraba : BLL.Mensajes.MensajeActualiza);                  
                }
            }
        }

        private void Guardar(int ElCodigo, string Mensaje)
        {
            int Resultado;
            Equipo equipo = new Equipo();
            equipo.codigoequipo = ElCodigo;
            equipo.nombreequipo = txtNombreEquipo.Text;
            equipo.codigomarca = Convert.ToInt32(cboMarcas.SelectedValue.ToString());
            equipo.serie = txtSerie.Text;
            equipo.codigolinea = Convert.ToInt32(cboLineas.SelectedValue.ToString());
            if (chkLubricacion.Checked)
            {
                equipo.lubricacion = 1;
            }
            else
            {
                equipo.lubricacion = 0;
            }

            Resultado = icontrolador.Guardar(equipo);
            if (Resultado == 0)
            {
                MessageBox.Show(Mensaje, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarListaSeleccion();
                Limpiar();
            }
            else if (Resultado == 1)
            {
                MessageBox.Show(BLL.Mensajes.Mensaje7, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSerie.Focus();
                errorPro.SetError(txtSerie, BLL.Mensajes.Mensaje7);
            }
            else
            {
                MessageBox.Show(BLL.Mensajes.MensajeErrorBD, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void Limpiar()
        {
            BLL.Funciones.LimpiarForma(panel2);
            lstEquipos.Visible = false;
            btnEliminar.Enabled = false;
            errorPro.Clear();
            lblCodigo.Text = "0";
            chkLubricacion.Checked = false;
            txtNombreEquipo.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void LlenarCampos()
        {
            Equipo equipo = (Equipo)icontrolador.ObtenerRegistro(lblCodigo.Text, "E");
            if (equipo != null)
            {
                btnEliminar.Enabled = true;
                txtNombreEquipo.Text = equipo.nombreequipo;
                cboMarcas.SelectedValue = equipo.codigomarca.ToString();
                txtSerie.Text = equipo.serie;
                cboLineas.SelectedValue = equipo.codigolinea.ToString();
                if (equipo.lubricacion == 1)
                {
                    chkLubricacion.Checked = true;
                }
                else
                {
                    chkLubricacion.Checked = false;
                }
            }
            txtNombreEquipo.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (lstEquipos.Items.Count != 0)
            {
                lstEquipos.Visible = true;
            }
        }

        private void lstEquipos_DoubleClick(object sender, EventArgs e)
        {
            lblCodigo.Text = lstEquipos.SelectedValue.ToString();
            lstEquipos.Visible = false;
            LlenarCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            icontrolador = null;
            this.Close();
            this.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(BLL.Mensajes.MensajeConfirmarBorrado, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (icontrolador.EliminarRegistro(lblCodigo.Text, "EQUIPOS") == 0)
                {
                    MessageBox.Show(BLL.Mensajes.MensajeBorrado, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarListaSeleccion();
                    Limpiar();
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
    
    

