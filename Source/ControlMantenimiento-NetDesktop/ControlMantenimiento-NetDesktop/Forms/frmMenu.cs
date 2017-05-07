using System;
using System.Data;
using System.Windows.Forms;
using ControlMantenimiento_NetDesktop.BLL;

namespace ControlMantenimiento_NetDesktop
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            if (BLL.Funciones.PerfilAcceso == 1) // Solo si tiene perfil de Administrador habilitar
            {
                menuToolStripMenuItem.Enabled = true;
                toolStripButton1.Enabled = true;
                toolStripButton2.Enabled = true;
                toolStripButton5.Enabled = true;
                toolStripButton6.Enabled = true;
            }
            lblUsuarioConectado.Text = lblUsuarioConectado.Text + " " + BLL.Funciones.NombreUsuario;
        }

        private void operariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeleccionarOpcion(0);  
        }

        private void equiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeleccionarOpcion(1);
        }

        private void programarMantenimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeleccionarOpcion(2);
        }

        private void modificarProgramacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeleccionarOpcion(3);
        }

        private void lineasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SeleccionarOpcion(4);
        }

        private void marcasToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SeleccionarOpcion(5); 
        }
        
        private void tostMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int Seleccion = tostMenu.Items.IndexOf(e.ClickedItem);
            SeleccionarOpcion(Seleccion);
        }

        private void cambioClaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlMantenimiento_NetDesktop.frmCambioClave Form_CambioClave = new ControlMantenimiento_NetDesktop.frmCambioClave();
            Form_CambioClave.ShowDialog(this);
        }

        private void SeleccionarOpcion(int Opcion)
        {
            int Resultado;
            IControlador icontrolador = new Controlador();
            switch (Opcion)
            {
                case 0:
                       ControlMantenimiento_NetDesktop.frmOperarios Form_Operarios = new ControlMantenimiento_NetDesktop.frmOperarios();
                       Form_Operarios.ShowDialog(this);
                       break;
                case 1:
                       Resultado = icontrolador.ValidarTablaVacia("CMARCAS");
                       if (Resultado == -1)
                       {
                           MessageBox.Show(BLL.Mensajes.Mensaje11, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                       }
                       else if (Resultado == -2)
                       {
                           MessageBox.Show(BLL.Mensajes.Mensaje12, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                       }
                       else
                       {
                           ControlMantenimiento_NetDesktop.frmEquipos Form_Equipos = new ControlMantenimiento_NetDesktop.frmEquipos();
                           Form_Equipos.ShowDialog(this);
                       }                       
                       break;                
                case 2:
                       Resultado = icontrolador.ValidarTablaVacia("CPROGRAMAREQUIPOS");
                       if (Resultado == -1)
                       {
                           MessageBox.Show(BLL.Mensajes.Mensaje14, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                       }
                       else if (Resultado == -2)
                       {
                           MessageBox.Show(BLL.Mensajes.Mensaje13, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                       }
                       else
                       {
                           ControlMantenimiento_NetDesktop.frmMantenimiento Form_Mantenimiento = new ControlMantenimiento_NetDesktop.frmMantenimiento();
                           Funciones.Fuente = "CPROGRAMAREQUIPOS";
                           Form_Mantenimiento.ShowDialog(this);
                       }                        
                       break;
                case 3:
                       Resultado = icontrolador.ValidarTablaVacia("CPROGRAMACION");
                       if (Resultado > 0)                      
                       {
                            ControlMantenimiento_NetDesktop.frmMantenimiento Form_Mantenimiento = new ControlMantenimiento_NetDesktop.frmMantenimiento();
                            Funciones.Fuente = "CPROGRAMACION";
                            Form_Mantenimiento.ShowDialog(this);
                       }    
                       else
                       {
                            MessageBox.Show(BLL.Mensajes.Mensaje14, BLL.Mensajes.MensajeAplicacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                       }
                       break;
                case 4:
                        BLL.Funciones.ValorTipo = "LINEAS";
                        ControlMantenimiento_NetDesktop.frmListaValores Form_ListaValores = new ControlMantenimiento_NetDesktop.frmListaValores();
                        Form_ListaValores.ShowDialog(this);
                        break;
                case 5:
                        BLL.Funciones.ValorTipo = "MARCAS";
                        ControlMantenimiento_NetDesktop.frmListaValores Form_ListaValoress = new ControlMantenimiento_NetDesktop.frmListaValores();
                        Form_ListaValoress.ShowDialog(this);
                        break;
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
    }
}
