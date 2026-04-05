namespace Gestor_de_Horarios_de_Maestros
{
    partial class FormAgregar
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabMaestro = new TabPage();
            lblNombreMaestro = new Label();
            txtNombreMaestro = new TextBox();
            tabCuatrimestre = new TabPage();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            txtNombreCuatrimestre = new TextBox();
            dtpInicio = new DateTimePicker();
            dtpFin = new DateTimePicker();
            tabMateria = new TabPage();
            label16 = new Label();
            cmbCuatrimestre = new ComboBox();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtIdMateria = new TextBox();
            cmbMaestro = new ComboBox();
            txtNombreMateria = new TextBox();
            txtDias = new TextBox();
            txtHora = new TextBox();
            txtAula = new TextBox();
            txtHDCredito = new TextBox();
            txtSeccion = new TextBox();
            txtDiasMes = new TextBox();
            txtCredito = new TextBox();
            txtTotalCredito = new TextBox();
            txtInscritos = new TextBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            L1 = new Label();
            L2 = new Label();
            L3 = new Label();
            L4 = new Label();
            L5 = new Label();
            L6 = new Label();
            L7 = new Label();
            L8 = new Label();
            L9 = new Label();
            L10 = new Label();
            L11 = new Label();
            L12 = new Label();
            LC1 = new Label();
            LC2 = new Label();
            LC3 = new Label();
            menuStrip2 = new MenuStrip();
            cerrarToolStripMenuItem2 = new ToolStripMenuItem();
            maximizarToolStripMenuItem = new ToolStripMenuItem();
            minimizarToolStripMenuItem = new ToolStripMenuItem();
            tabControl.SuspendLayout();
            tabMaestro.SuspendLayout();
            tabCuatrimestre.SuspendLayout();
            tabMateria.SuspendLayout();
            menuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabMaestro);
            tabControl.Controls.Add(tabCuatrimestre);
            tabControl.Controls.Add(tabMateria);
            tabControl.Location = new Point(12, 31);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(435, 341);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            // 
            // tabMaestro
            // 
            tabMaestro.BackColor = Color.FromArgb(45, 45, 48);
            tabMaestro.Controls.Add(lblNombreMaestro);
            tabMaestro.Controls.Add(txtNombreMaestro);
            tabMaestro.Location = new Point(4, 24);
            tabMaestro.Name = "tabMaestro";
            tabMaestro.Size = new Size(427, 313);
            tabMaestro.TabIndex = 0;
            tabMaestro.Text = "Nuevo Maestro";
            tabMaestro.Click += tabMaestro_Click;
            // 
            // lblNombreMaestro
            // 
            lblNombreMaestro.AutoSize = true;
            lblNombreMaestro.Location = new Point(14, 44);
            lblNombreMaestro.Name = "lblNombreMaestro";
            lblNombreMaestro.Size = new Size(119, 15);
            lblNombreMaestro.TabIndex = 0;
            lblNombreMaestro.Text = "Nombre del Maestro:";
            lblNombreMaestro.Click += lblNombreMaestro_Click;
            // 
            // txtNombreMaestro
            // 
            txtNombreMaestro.BackColor = SystemColors.ScrollBar;
            txtNombreMaestro.Location = new Point(14, 80);
            txtNombreMaestro.Name = "txtNombreMaestro";
            txtNombreMaestro.Size = new Size(300, 23);
            txtNombreMaestro.TabIndex = 1;
            // 
            // tabCuatrimestre
            // 
            tabCuatrimestre.BackColor = Color.FromArgb(45, 45, 48);
            tabCuatrimestre.Controls.Add(label15);
            tabCuatrimestre.Controls.Add(label14);
            tabCuatrimestre.Controls.Add(label13);
            tabCuatrimestre.Controls.Add(txtNombreCuatrimestre);
            tabCuatrimestre.Controls.Add(dtpInicio);
            tabCuatrimestre.Controls.Add(dtpFin);
            tabCuatrimestre.Location = new Point(4, 24);
            tabCuatrimestre.Name = "tabCuatrimestre";
            tabCuatrimestre.Size = new Size(427, 313);
            tabCuatrimestre.TabIndex = 2;
            tabCuatrimestre.Text = "Nuevo Cuatrimestre";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(257, 59);
            label15.Name = "label15";
            label15.Size = new Size(38, 15);
            label15.TabIndex = 5;
            label15.Text = "Final: ";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(62, 58);
            label14.Name = "label14";
            label14.Size = new Size(42, 15);
            label14.TabIndex = 4;
            label14.Text = "Inicio: ";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(23, 20);
            label13.Name = "label13";
            label13.Size = new Size(81, 15);
            label13.TabIndex = 3;
            label13.Text = "Cuatrimestre: ";
            // 
            // txtNombreCuatrimestre
            // 
            txtNombreCuatrimestre.Location = new Point(129, 13);
            txtNombreCuatrimestre.Name = "txtNombreCuatrimestre";
            txtNombreCuatrimestre.Size = new Size(280, 23);
            txtNombreCuatrimestre.TabIndex = 0;
            txtNombreCuatrimestre.TextChanged += txtNombreCuatrimestre_TextChanged;
            // 
            // dtpInicio
            // 
            dtpInicio.Format = DateTimePickerFormat.Short;
            dtpInicio.Location = new Point(119, 53);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(120, 23);
            dtpInicio.TabIndex = 1;
            // 
            // dtpFin
            // 
            dtpFin.Format = DateTimePickerFormat.Short;
            dtpFin.Location = new Point(310, 52);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(110, 23);
            dtpFin.TabIndex = 2;
            // 
            // tabMateria
            // 
            tabMateria.BackColor = Color.FromArgb(45, 45, 48);
            tabMateria.Controls.Add(label16);
            tabMateria.Controls.Add(cmbCuatrimestre);
            tabMateria.Controls.Add(label12);
            tabMateria.Controls.Add(label11);
            tabMateria.Controls.Add(label10);
            tabMateria.Controls.Add(label9);
            tabMateria.Controls.Add(label8);
            tabMateria.Controls.Add(label7);
            tabMateria.Controls.Add(label6);
            tabMateria.Controls.Add(label5);
            tabMateria.Controls.Add(label4);
            tabMateria.Controls.Add(label3);
            tabMateria.Controls.Add(label2);
            tabMateria.Controls.Add(label1);
            tabMateria.Controls.Add(txtIdMateria);
            tabMateria.Controls.Add(cmbMaestro);
            tabMateria.Controls.Add(txtNombreMateria);
            tabMateria.Controls.Add(txtDias);
            tabMateria.Controls.Add(txtHora);
            tabMateria.Controls.Add(txtAula);
            tabMateria.Controls.Add(txtHDCredito);
            tabMateria.Controls.Add(txtSeccion);
            tabMateria.Controls.Add(txtDiasMes);
            tabMateria.Controls.Add(txtCredito);
            tabMateria.Controls.Add(txtTotalCredito);
            tabMateria.Controls.Add(txtInscritos);
            tabMateria.Location = new Point(4, 24);
            tabMateria.Name = "tabMateria";
            tabMateria.Size = new Size(427, 313);
            tabMateria.TabIndex = 1;
            tabMateria.Text = "Nueva Materia";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(196, 20);
            label16.Name = "label16";
            label16.Size = new Size(81, 15);
            label16.TabIndex = 25;
            label16.Text = "Cuatrimestre: ";
            // 
            // cmbCuatrimestre
            // 
            cmbCuatrimestre.FormattingEnabled = true;
            cmbCuatrimestre.Location = new Point(283, 17);
            cmbCuatrimestre.Name = "cmbCuatrimestre";
            cmbCuatrimestre.Size = new Size(107, 23);
            cmbCuatrimestre.TabIndex = 24;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(214, 265);
            label12.Name = "label12";
            label12.Size = new Size(57, 15);
            label12.TabIndex = 23;
            label12.Text = "Inscritos: ";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(214, 230);
            label11.Name = "label11";
            label11.Size = new Size(57, 15);
            label11.TabIndex = 22;
            label11.Text = "Créditos: ";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(214, 195);
            label10.Name = "label10";
            label10.Size = new Size(54, 15);
            label10.TabIndex = 21;
            label10.Text = "Seccíon: ";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(214, 158);
            label9.Name = "label9";
            label9.Size = new Size(37, 15);
            label9.TabIndex = 20;
            label9.Text = "Aula: ";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(14, 265);
            label8.Name = "label8";
            label8.Size = new Size(67, 15);
            label8.TabIndex = 19;
            label8.Text = "Total Cred: ";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 230);
            label7.Name = "label7";
            label7.Size = new Size(59, 15);
            label7.TabIndex = 18;
            label7.Text = "Días/Mes:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 195);
            label6.Name = "label6";
            label6.Size = new Size(63, 15);
            label6.TabIndex = 17;
            label6.Text = "H/D Cred: ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 160);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 16;
            label5.Text = "Hora:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 90);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 15;
            label4.Text = "Materia: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 125);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 14;
            label3.Text = "Días: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 55);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 13;
            label2.Text = "Maestro: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(45, 45, 48);
            label1.Location = new Point(14, 20);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 12;
            label1.Text = "ID Materia:";
            // 
            // txtIdMateria
            // 
            txtIdMateria.Location = new Point(110, 17);
            txtIdMateria.Name = "txtIdMateria";
            txtIdMateria.Size = new Size(80, 23);
            txtIdMateria.TabIndex = 0;
            // 
            // cmbMaestro
            // 
            cmbMaestro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaestro.Location = new Point(110, 52);
            cmbMaestro.Name = "cmbMaestro";
            cmbMaestro.Size = new Size(280, 23);
            cmbMaestro.TabIndex = 1;
            // 
            // txtNombreMateria
            // 
            txtNombreMateria.Location = new Point(110, 87);
            txtNombreMateria.Name = "txtNombreMateria";
            txtNombreMateria.Size = new Size(280, 23);
            txtNombreMateria.TabIndex = 2;
            // 
            // txtDias
            // 
            txtDias.Location = new Point(110, 122);
            txtDias.Name = "txtDias";
            txtDias.Size = new Size(280, 23);
            txtDias.TabIndex = 3;
            // 
            // txtHora
            // 
            txtHora.Location = new Point(110, 157);
            txtHora.Name = "txtHora";
            txtHora.Size = new Size(90, 23);
            txtHora.TabIndex = 4;
            // 
            // txtAula
            // 
            txtAula.Location = new Point(287, 155);
            txtAula.Name = "txtAula";
            txtAula.Size = new Size(103, 23);
            txtAula.TabIndex = 5;
            // 
            // txtHDCredito
            // 
            txtHDCredito.Location = new Point(110, 192);
            txtHDCredito.Name = "txtHDCredito";
            txtHDCredito.Size = new Size(90, 23);
            txtHDCredito.TabIndex = 6;
            // 
            // txtSeccion
            // 
            txtSeccion.Location = new Point(287, 192);
            txtSeccion.Name = "txtSeccion";
            txtSeccion.Size = new Size(103, 23);
            txtSeccion.TabIndex = 7;
            txtSeccion.TextChanged += txtSeccion_TextChanged;
            // 
            // txtDiasMes
            // 
            txtDiasMes.Location = new Point(110, 227);
            txtDiasMes.Name = "txtDiasMes";
            txtDiasMes.Size = new Size(90, 23);
            txtDiasMes.TabIndex = 8;
            // 
            // txtCredito
            // 
            txtCredito.Location = new Point(289, 229);
            txtCredito.Name = "txtCredito";
            txtCredito.Size = new Size(101, 23);
            txtCredito.TabIndex = 9;
            // 
            // txtTotalCredito
            // 
            txtTotalCredito.Location = new Point(110, 262);
            txtTotalCredito.Name = "txtTotalCredito";
            txtTotalCredito.Size = new Size(90, 23);
            txtTotalCredito.TabIndex = 10;
            // 
            // txtInscritos
            // 
            txtInscritos.Location = new Point(289, 262);
            txtInscritos.Name = "txtInscritos";
            txtInscritos.Size = new Size(101, 23);
            txtInscritos.TabIndex = 11;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.Silver;
            btnGuardar.ForeColor = SystemColors.ActiveCaptionText;
            btnGuardar.Location = new Point(230, 385);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 35);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += BtnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Silver;
            btnCancelar.ForeColor = SystemColors.ActiveCaptionText;
            btnCancelar.Location = new Point(340, 385);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 35);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // L1
            // 
            L1.Location = new Point(0, 0);
            L1.Name = "L1";
            L1.Size = new Size(100, 23);
            L1.TabIndex = 0;
            // 
            // L2
            // 
            L2.Location = new Point(0, 0);
            L2.Name = "L2";
            L2.Size = new Size(100, 23);
            L2.TabIndex = 0;
            // 
            // L3
            // 
            L3.Location = new Point(0, 0);
            L3.Name = "L3";
            L3.Size = new Size(100, 23);
            L3.TabIndex = 0;
            // 
            // L4
            // 
            L4.Location = new Point(0, 0);
            L4.Name = "L4";
            L4.Size = new Size(100, 23);
            L4.TabIndex = 0;
            // 
            // L5
            // 
            L5.Location = new Point(0, 0);
            L5.Name = "L5";
            L5.Size = new Size(100, 23);
            L5.TabIndex = 0;
            // 
            // L6
            // 
            L6.Location = new Point(0, 0);
            L6.Name = "L6";
            L6.Size = new Size(100, 23);
            L6.TabIndex = 0;
            // 
            // L7
            // 
            L7.Location = new Point(0, 0);
            L7.Name = "L7";
            L7.Size = new Size(100, 23);
            L7.TabIndex = 0;
            // 
            // L8
            // 
            L8.Location = new Point(0, 0);
            L8.Name = "L8";
            L8.Size = new Size(100, 23);
            L8.TabIndex = 0;
            // 
            // L9
            // 
            L9.Location = new Point(0, 0);
            L9.Name = "L9";
            L9.Size = new Size(100, 23);
            L9.TabIndex = 0;
            // 
            // L10
            // 
            L10.Location = new Point(0, 0);
            L10.Name = "L10";
            L10.Size = new Size(100, 23);
            L10.TabIndex = 0;
            // 
            // L11
            // 
            L11.Location = new Point(0, 0);
            L11.Name = "L11";
            L11.Size = new Size(100, 23);
            L11.TabIndex = 0;
            // 
            // L12
            // 
            L12.Location = new Point(0, 0);
            L12.Name = "L12";
            L12.Size = new Size(100, 23);
            L12.TabIndex = 0;
            // 
            // LC1
            // 
            LC1.Location = new Point(0, 0);
            LC1.Name = "LC1";
            LC1.Size = new Size(100, 23);
            LC1.TabIndex = 0;
            // 
            // LC2
            // 
            LC2.Location = new Point(0, 0);
            LC2.Name = "LC2";
            LC2.Size = new Size(100, 23);
            LC2.TabIndex = 0;
            // 
            // LC3
            // 
            LC3.Location = new Point(0, 0);
            LC3.Name = "LC3";
            LC3.Size = new Size(100, 23);
            LC3.TabIndex = 0;
            // 
            // menuStrip2
            // 
            menuStrip2.BackColor = Color.FromArgb(20, 20, 20);
            menuStrip2.ImageScalingSize = new Size(20, 20);
            menuStrip2.Items.AddRange(new ToolStripItem[] { cerrarToolStripMenuItem2, maximizarToolStripMenuItem, minimizarToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(469, 28);
            menuStrip2.TabIndex = 3;
            menuStrip2.Text = "menuStrip2";
            menuStrip2.MouseDown += toolStrip1_MouseDown;
            // 
            // cerrarToolStripMenuItem2
            // 
            cerrarToolStripMenuItem2.Alignment = ToolStripItemAlignment.Right;
            cerrarToolStripMenuItem2.Image = Properties.Resources.icon_icons__1_;
            cerrarToolStripMenuItem2.Name = "cerrarToolStripMenuItem2";
            cerrarToolStripMenuItem2.Size = new Size(32, 24);
            cerrarToolStripMenuItem2.Click += cerrarToolStripMenuItem_Click;
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
            // FormAgregar
            // 
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(469, 452);
            Controls.Add(menuStrip2);
            Controls.Add(tabControl);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            ForeColor = SystemColors.ButtonHighlight;
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormAgregar";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar";
            Load += FormAgregar_Load;
            MouseDown += toolStrip1_MouseDown;
            tabControl.ResumeLayout(false);
            tabMaestro.ResumeLayout(false);
            tabMaestro.PerformLayout();
            tabCuatrimestre.ResumeLayout(false);
            tabCuatrimestre.PerformLayout();
            tabMateria.ResumeLayout(false);
            tabMateria.PerformLayout();
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void Colocar(Label l, string t, int x, int y, Control p)
        {
            l.Text = t; l.Location = new System.Drawing.Point(x, y); l.AutoSize = true; p.Controls.Add(l);
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMaestro, tabMateria;
        private System.Windows.Forms.TextBox txtNombreMaestro, txtIdMateria, txtNombreMateria, txtDias, txtHora, txtAula, txtSeccion, txtHDCredito, txtDiasMes, txtCredito, txtTotalCredito, txtInscritos;
        private System.Windows.Forms.ComboBox cmbMaestro;
        private System.Windows.Forms.Label lblNombreMaestro, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12;
        private System.Windows.Forms.Button btnGuardar, btnCancelar;
        private System.Windows.Forms.TabPage tabCuatrimestre;
        private System.Windows.Forms.TextBox txtNombreCuatrimestre;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Label LC1, LC2, LC3;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label15;
        private Label label14;
        private Label label13;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem cerrarToolStripMenuItem2;
        private ToolStripMenuItem maximizarToolStripMenuItem;
        private ToolStripMenuItem minimizarToolStripMenuItem;
        private ComboBox cmbCuatrimestre;
        private Label label16;
    }
}
