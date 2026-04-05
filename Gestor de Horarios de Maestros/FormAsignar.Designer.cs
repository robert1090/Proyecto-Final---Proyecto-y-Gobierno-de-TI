namespace Gestor_de_Horarios_de_Maestros
{
    partial class FormAsignar
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbMaestros;
        private System.Windows.Forms.ComboBox cmbMaterias; // Nuevo ComboBox
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;

        private void InitializeComponent()
        {
            cmbMaestros = new ComboBox();
            cmbMaterias = new ComboBox();
            btnGuardar = new Button();
            lbl1 = new Label();
            lbl2 = new Label();
            cmbCuatrimestre = new ComboBox();
            label1 = new Label();
            menuStrip2 = new MenuStrip();
            cerrar3 = new ToolStripMenuItem();
            maximizarToolStripMenuItem = new ToolStripMenuItem();
            minimizarToolStripMenuItem = new ToolStripMenuItem();
            menuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // cmbMaestros
            // 
            cmbMaestros.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaestros.Location = new Point(20, 49);
            cmbMaestros.Name = "cmbMaestros";
            cmbMaestros.Size = new Size(240, 23);
            cmbMaestros.TabIndex = 0;
            // 
            // cmbMaterias
            // 
            cmbMaterias.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaterias.Location = new Point(20, 112);
            cmbMaterias.Name = "cmbMaterias";
            cmbMaterias.Size = new Size(240, 23);
            cmbMaterias.TabIndex = 1;
            cmbMaterias.SelectedIndexChanged += cmbMaterias_SelectedIndexChanged;
            // 
            // btnGuardar
            // 
            btnGuardar.ForeColor = SystemColors.ButtonHighlight;
            btnGuardar.Location = new Point(72, 249);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(140, 30);
            btnGuardar.TabIndex = 2;
            btnGuardar.Text = "Realizar Asignación";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.ForeColor = SystemColors.ButtonHighlight;
            lbl1.Location = new Point(20, 26);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(116, 15);
            lbl1.TabIndex = 3;
            lbl1.Text = "Seleccionar Maestro:";
            lbl1.Click += lbl1_Click;
            // 
            // lbl2
            // 
            lbl2.AutoSize = true;
            lbl2.ForeColor = SystemColors.Control;
            lbl2.Location = new Point(20, 80);
            lbl2.Name = "lbl2";
            lbl2.Size = new Size(113, 15);
            lbl2.TabIndex = 4;
            lbl2.Text = "Seleccionar Materia:";
            lbl2.Click += lbl2_Click;
            // 
            // cmbCuatrimestre
            // 
            cmbCuatrimestre.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCuatrimestre.Location = new Point(20, 166);
            cmbCuatrimestre.Name = "cmbCuatrimestre";
            cmbCuatrimestre.Size = new Size(240, 23);
            cmbCuatrimestre.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(20, 143);
            label1.Name = "label1";
            label1.Size = new Size(141, 15);
            label1.TabIndex = 6;
            label1.Text = "Seleccionar Cuatrimestre:";
            // 
            // menuStrip2
            // 
            menuStrip2.BackColor = Color.FromArgb(20, 20, 20);
            menuStrip2.ImageScalingSize = new Size(20, 20);
            menuStrip2.Items.AddRange(new ToolStripItem[] { cerrar3, maximizarToolStripMenuItem, minimizarToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(284, 28);
            menuStrip2.TabIndex = 7;
            menuStrip2.Text = "menuStrip2";
            menuStrip2.MouseDown += toolStrip1_MouseDown;
            // 
            // cerrar3
            // 
            cerrar3.Alignment = ToolStripItemAlignment.Right;
            cerrar3.Image = Properties.Resources.icon_icons__1_;
            cerrar3.Name = "cerrar3";
            cerrar3.Size = new Size(32, 24);
            cerrar3.Click += cerrarToolStripMenuItem_Click;
            // 
            // maximizarToolStripMenuItem
            // 
            maximizarToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            maximizarToolStripMenuItem.Image = Properties.Resources.maximizethewindow_theapplication_maximizar_2873;
            maximizarToolStripMenuItem.Name = "maximizarToolStripMenuItem";
            maximizarToolStripMenuItem.Size = new Size(32, 24);
            maximizarToolStripMenuItem.Click += maximizarToolStripMenuItem_Click;
            // 
            // minimizarToolStripMenuItem
            // 
            minimizarToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            minimizarToolStripMenuItem.Image = Properties.Resources.minimize_thewindow_theapplication_2872;
            minimizarToolStripMenuItem.Name = "minimizarToolStripMenuItem";
            minimizarToolStripMenuItem.Size = new Size(32, 24);
            minimizarToolStripMenuItem.Click += minimizarToolStripMenuItem_Click;
            // 
            // FormAsignar
            // 
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(284, 291);
            Controls.Add(menuStrip2);
            Controls.Add(cmbCuatrimestre);
            Controls.Add(label1);
            Controls.Add(cmbMaestros);
            Controls.Add(cmbMaterias);
            Controls.Add(btnGuardar);
            Controls.Add(lbl1);
            Controls.Add(lbl2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormAsignar";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Asignar Maestro";
            MouseDown += toolStrip1_MouseDown;
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private ComboBox cmbCuatrimestre;
        private Label label1;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem cerrar3;
        private ToolStripMenuItem maximizarToolStripMenuItem;
        private ToolStripMenuItem minimizarToolStripMenuItem;
    }
}