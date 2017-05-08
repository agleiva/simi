
/*=================================================================================== 
 La norma es que no puede haber sino una sola programación por equipo
 Un Operario no puede estar asignado en la misma fecha para varios mantenimientos
 ==================================================================================== */

using System;
using System.Data;
using System.Windows.Forms;
using ControlMantenimiento.Business;
using ControlMantenimiento.Model;
using Funciones = ControlMantenimiento_NetDesktop.BLL.Funciones;

namespace ControlMantenimiento_NetDesktop
{
    public partial class frmMantenimiento : Form
    {
        public frmMantenimiento()
        {
            InitializeComponent();
            this.cboEquipos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboEquipos_KeyPress);
            this.cboOperarios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboOperarios_KeyPress);
            this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFecha_KeyPress);
            this.txtObservaciones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtObservaciones_KeyPress);
        }

        private Controlador _controlador = Funciones.CrearControlador();
        private bool     Grabar;
        private KeyPressEventArgs Tecla = new KeyPressEventArgs('\r'); // Send Enter

        private void frmMantenimiento_Load(object sender, EventArgs e)
        {
            if (Funciones.Fuente == "CPROGRAMACION")
            {
                btnBuscar.Visible = true;
                lbMensaje.Visible = true;
                btnGrabar.Enabled = false;
                _controlador.ControlProgramacion("CONTROLPROGRAMACION");
            }
            else 
            {
                _controlador.ControlProgramacion("CONTROLPROGRAMAREQUIPOS");               
            }
            cboEquipos.ValueMember = "CODIGO";
            cboEquipos.DisplayMember = "DETALLE";
            cboEquipos.DataSource = _controlador.ObtenerListaEquipos();

            cboOperarios.ValueMember = "CODIGO";
            cboOperarios.DisplayMember = "DETALLE";
            cboOperarios.DataSource = _controlador.ObtenerListaOperarios();
        }

        private void CargarEquipos()
        {   
           cboEquipos.ValueMember = "CODIGO";
           cboEquipos.DisplayMember = "DETALLE";
           cboEquipos.DataSource = _controlador.CargarListas(Funciones.Fuente);
        }

        private void cboEquipos_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                cboOperarios.Focus();
            }
        }

        private void cboOperarios_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                dtpFecha.Focus();
            }
        }

        private void dtpFecha_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
                dtpFecha_ValueChanged(dtpFecha, e);
            }            
        }

        private void txtObservaciones_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == 9)) // Si presionan Enter o Tab
            {
               txtObservaciones.Text.EliminarTabulador("");
               btnGrabar.Focus();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Grabar = true;
            dtpFecha_KeyPress(btnGrabar, Tecla);              
            if (Grabar)
            {
                if (Funciones.Fuente == "CPROGRAMAREQUIPOS")
                {
                    Guardar("I", Mensajes.MensajeGraba);
                }
                else
                {
                    Guardar("U", Mensajes.MensajeActualiza);
                }
            }            
        }

        private void Guardar(string Accion, string Mensaje)
        {
            int Resultado;
            Mantenimiento mantenimiento = new Mantenimiento();
            mantenimiento.CodigoEquipo = Convert.ToInt32(cboEquipos.SelectedValue.ToString()); 
            mantenimiento.Documento = Convert.ToDouble(cboOperarios.SelectedValue.ToString()); 
            mantenimiento.Fecha = Convert.ToDateTime(dtpFecha.Value);
            mantenimiento.Observaciones = txtObservaciones.Text;
            
            Resultado = _controlador.Guardar(mantenimiento, Accion);
            if (Resultado == 0)
            {
                MessageBox.Show(Mensaje, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarEquipos();
                Limpiar();
            }
            else if (Resultado == 1)
            {
                MessageBox.Show(Mensajes.Mensaje10, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboOperarios.Focus();
                errorPro.SetError(cboOperarios, Mensajes.Mensaje10);
            }
            else
            {
                MessageBox.Show(Mensajes.MensajeErrorBD, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            if (cboEquipos.Items.Count <= 0)
            {
                btnSalir.PerformClick();
            }
        }

        private void Limpiar()
        {
            btnEliminar.Enabled = false;
            cboEquipos.Enabled = true;
            BLL.Funciones.LimpiarForma(panel2);
            dtpFecha.Value = DateTime.Now.Date;
            errorPro.Clear();
            if (Funciones.Fuente == "CPROGRAMACION")
            {
                btnGrabar.Enabled = false;
            }
            cboEquipos.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {            
            Limpiar();            
        }
                
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            LlenarCampos();        
        }
        
        private void LlenarCampos()
        {
          Mantenimiento mantenimiento = (Mantenimiento)_controlador.ObtenerRegistro(cboEquipos.SelectedValue.ToString(),"M");
          if (mantenimiento != null)
          {
              btnEliminar.Enabled = true;
              cboEquipos.Enabled = false;
              cboOperarios.SelectedValue = mantenimiento.Documento.ToString();
              btnEliminar.Enabled = true;
              dtpFecha.Value = mantenimiento.Fecha;
              txtObservaciones.Text = mantenimiento.Observaciones;
              btnGrabar.Enabled = true;
              cboOperarios.Focus();
          }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            _controlador = null;
            this.Close();
            this.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Mensajes.MensajeConfirmarBorrado, Mensajes.MensajeAplicacion, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                
                if (_controlador.EliminarRegistro(cboEquipos.SelectedValue.ToString(), "MANTENIMIENTO")==0)
                {
                    MessageBox.Show(Mensajes.MensajeBorrado, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Mensajes.MensajeErrorBD, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Limpiar();
                CargarEquipos();
                if (cboEquipos.Items.Count <= 0)
                {
                    btnSalir.PerformClick();
                }
            }
            
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFecha.Value < DateTime.Now.Date)
            {
                Grabar = false;
                MessageBox.Show(Mensajes.Mensaje27, Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpFecha.Focus();
                errorPro.SetError(dtpFecha, Mensajes.Mensaje27);
            }
            else
            {
                errorPro.Clear();
                txtObservaciones.Focus ();
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
