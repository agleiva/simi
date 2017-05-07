namespace ControlMantenimiento_NetDesktop
{
    partial class frmMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.administracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mantenimientoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.programarMantenimientoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarProgramacionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cambioClaveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.equiposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.marcasToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tostMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblUsuarioConectado = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tostMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.administracionToolStripMenuItem,
            this.menuToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(762, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // administracionToolStripMenuItem
            // 
            this.administracionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mantenimientoToolStripMenuItem1,
            this.cambioClaveToolStripMenuItem1});
            this.administracionToolStripMenuItem.Name = "administracionToolStripMenuItem";
            this.administracionToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.administracionToolStripMenuItem.Text = "Menu";
            // 
            // mantenimientoToolStripMenuItem1
            // 
            this.mantenimientoToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programarMantenimientoToolStripMenuItem1,
            this.modificarProgramacionToolStripMenuItem1});
            this.mantenimientoToolStripMenuItem1.Name = "mantenimientoToolStripMenuItem1";
            this.mantenimientoToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.mantenimientoToolStripMenuItem1.Text = "Mantenimiento";
            // 
            // programarMantenimientoToolStripMenuItem1
            // 
            this.programarMantenimientoToolStripMenuItem1.Name = "programarMantenimientoToolStripMenuItem1";
            this.programarMantenimientoToolStripMenuItem1.Size = new System.Drawing.Size(215, 22);
            this.programarMantenimientoToolStripMenuItem1.Text = "Programar Mantenimiento";
            this.programarMantenimientoToolStripMenuItem1.Click += new System.EventHandler(this.programarMantenimientoToolStripMenuItem_Click);
            // 
            // modificarProgramacionToolStripMenuItem1
            // 
            this.modificarProgramacionToolStripMenuItem1.Name = "modificarProgramacionToolStripMenuItem1";
            this.modificarProgramacionToolStripMenuItem1.Size = new System.Drawing.Size(215, 22);
            this.modificarProgramacionToolStripMenuItem1.Text = "Modificar Programacion";
            this.modificarProgramacionToolStripMenuItem1.Click += new System.EventHandler(this.modificarProgramacionToolStripMenuItem_Click);
            // 
            // cambioClaveToolStripMenuItem1
            // 
            this.cambioClaveToolStripMenuItem1.Name = "cambioClaveToolStripMenuItem1";
            this.cambioClaveToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.cambioClaveToolStripMenuItem1.Text = "Cambio Clave";
            this.cambioClaveToolStripMenuItem1.Click += new System.EventHandler(this.cambioClaveToolStripMenuItem_Click);
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operariosToolStripMenuItem,
            this.equiposToolStripMenuItem,
            this.lineasToolStripMenuItem1,
            this.marcasToolStripMenuItem2});
            this.menuToolStripMenuItem.Enabled = false;
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.menuToolStripMenuItem.Text = "Administracion";
            // 
            // operariosToolStripMenuItem
            // 
            this.operariosToolStripMenuItem.Name = "operariosToolStripMenuItem";
            this.operariosToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.operariosToolStripMenuItem.Text = "Operarios";
            this.operariosToolStripMenuItem.Click += new System.EventHandler(this.operariosToolStripMenuItem_Click);
            // 
            // equiposToolStripMenuItem
            // 
            this.equiposToolStripMenuItem.Name = "equiposToolStripMenuItem";
            this.equiposToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.equiposToolStripMenuItem.Text = "Equipos";
            this.equiposToolStripMenuItem.Click += new System.EventHandler(this.equiposToolStripMenuItem_Click);
            // 
            // lineasToolStripMenuItem1
            // 
            this.lineasToolStripMenuItem1.Name = "lineasToolStripMenuItem1";
            this.lineasToolStripMenuItem1.Size = new System.Drawing.Size(125, 22);
            this.lineasToolStripMenuItem1.Text = "Lineas";
            this.lineasToolStripMenuItem1.Click += new System.EventHandler(this.lineasToolStripMenuItem1_Click);
            // 
            // marcasToolStripMenuItem2
            // 
            this.marcasToolStripMenuItem2.Name = "marcasToolStripMenuItem2";
            this.marcasToolStripMenuItem2.Size = new System.Drawing.Size(125, 22);
            this.marcasToolStripMenuItem2.Text = "Marcas";
            this.marcasToolStripMenuItem2.Click += new System.EventHandler(this.marcasToolStripMenuItem2_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // tostMenu
            // 
            this.tostMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.tostMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6});
            this.tostMenu.Location = new System.Drawing.Point(0, 24);
            this.tostMenu.Name = "tostMenu";
            this.tostMenu.Size = new System.Drawing.Size(762, 57);
            this.tostMenu.TabIndex = 1;
            this.tostMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tostMenu_ItemClicked);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(54, 54);
            this.toolStripButton1.Text = "Operarios";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(54, 54);
            this.toolStripButton2.Text = "Equipos";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(54, 54);
            this.toolStripButton3.Text = "Programacion Mto";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(54, 54);
            this.toolStripButton4.Text = "Modificar Mto";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Enabled = false;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(54, 54);
            this.toolStripButton5.Text = "Lineas";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Enabled = false;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(54, 54);
            this.toolStripButton6.Text = "Marcas";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 84);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(762, 321);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblUsuarioConectado
            // 
            this.lblUsuarioConectado.AutoSize = true;
            this.lblUsuarioConectado.ForeColor = System.Drawing.Color.White;
            this.lblUsuarioConectado.Location = new System.Drawing.Point(507, 90);
            this.lblUsuarioConectado.Name = "lblUsuarioConectado";
            this.lblUsuarioConectado.Size = new System.Drawing.Size(66, 13);
            this.lblUsuarioConectado.TabIndex = 3;
            this.lblUsuarioConectado.Text = "Bienvenido: ";
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(762, 404);
            this.Controls.Add(this.lblUsuarioConectado);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tostMenu);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " SIMI - SISTEMA DE MANTENIMIENTO INDUSTRIAL";
            this.Load += new System.EventHandler(this.frmMenu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tostMenu.ResumeLayout(false);
            this.tostMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem equiposToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem marcasToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem administracionToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tostMenu;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mantenimientoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem programarMantenimientoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem modificarProgramacionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cambioClaveToolStripMenuItem1;
        private System.Windows.Forms.Label lblUsuarioConectado;
    }
}