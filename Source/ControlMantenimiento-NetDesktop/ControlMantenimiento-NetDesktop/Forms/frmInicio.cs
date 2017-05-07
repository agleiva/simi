using System;
using System.Data;
using System.Windows.Forms;

namespace ControlMantenimiento_NetDesktop
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
            tmrInicio.Enabled = true;  
        }    

        private void tmrInicio_Tick(object sender, EventArgs e)
        {
            tmrInicio.Enabled = false;
            this.Hide();            
            ControlMantenimiento_NetDesktop.frmAcceso Form_Acceso = new ControlMantenimiento_NetDesktop.frmAcceso();
            Form_Acceso.ShowDialog();           
        }       
    }
}
