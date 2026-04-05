namespace Gestor_de_Horarios_de_Maestros
{
    partial class FormBuscar
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtSeccion;
        private System.Windows.Forms.TextBox txtDia;
        private System.Windows.Forms.TextBox txtCredito;
        private System.Windows.Forms.TextBox txtHora;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtSeccion = new TextBox();
            txtDia = new TextBox();
            txtCredito = new TextBox();
            txtHora = new TextBox();
            lbl1 = new Label();
            lbl2 = new Label();
            lbl3 = new Label();
            lbl4 = new Label();
            btnAceptar = new Button();
            btnCancelar = new Button();
            menuStrip2 = new MenuStrip();
            cerrarToolStripMenuItem4 = new ToolStripMenuItem();
            maximizarToolStripMenuItem = new ToolStripMenuItem();
            minimizarToolStripMenuItem = new ToolStripMenuItem();
            menuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // txtSeccion
            // 
            txtSeccion.Location = new Point(100, 43);
            txtSeccion.Name = "txtSeccion";
            txtSeccion.Size = new Size(150, 23);
            txtSeccion.TabIndex = 0;
            // 
            // txtDia
            // 
            txtDia.Location = new Point(100, 73);
            txtDia.Name = "txtDia";
            txtDia.Size = new Size(150, 23);
            txtDia.TabIndex = 1;
            txtDia.TextChanged += txtDia_TextChanged;
            // 
            // txtCredito
            // 
            txtCredito.Location = new Point(100, 104);
            txtCredito.Name = "txtCredito";
            txtCredito.Size = new Size(150, 23);
            txtCredito.TabIndex = 2;
            txtCredito.TextChanged += txtCredito_TextChanged;
            // 
            // txtHora
            // 
            txtHora.Location = new Point(100, 134);
            txtHora.Name = "txtHora";
            txtHora.Size = new Size(150, 23);
            txtHora.TabIndex = 3;
            // 
            // lbl1
            // 
            lbl1.ForeColor = SystemColors.ButtonHighlight;
            lbl1.Location = new Point(20, 47);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(100, 23);
            lbl1.TabIndex = 4;
            lbl1.Text = "Sección:";
            // 
            // lbl2
            // 
            lbl2.ForeColor = SystemColors.ButtonHighlight;
            lbl2.Location = new Point(20, 73);
            lbl2.Name = "lbl2";
            lbl2.Size = new Size(100, 23);
            lbl2.TabIndex = 5;
            lbl2.Text = "Día:";
            // 
            // lbl3
            // 
            lbl3.ForeColor = SystemColors.ButtonHighlight;
            lbl3.Location = new Point(20, 103);
            lbl3.Name = "lbl3";
            lbl3.Size = new Size(100, 23);
            lbl3.TabIndex = 6;
            lbl3.Text = "Crédito:";
            // 
            // lbl4
            // 
            lbl4.ForeColor = SystemColors.ButtonHighlight;
            lbl4.Location = new Point(20, 134);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(100, 23);
            lbl4.TabIndex = 7;
            lbl4.Text = "Hora:";
            lbl4.Click += lbl4_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.ForeColor = SystemColors.Control;
            btnAceptar.Location = new Point(74, 173);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(75, 38);
            btnAceptar.TabIndex = 8;
            btnAceptar.Text = "Buscar";
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.ForeColor = SystemColors.ButtonHighlight;
            btnCancelar.Location = new Point(175, 173);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 38);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // menuStrip2
            // 
            menuStrip2.BackColor = Color.FromArgb(20, 20, 20);
            menuStrip2.ImageScalingSize = new Size(20, 20);
            menuStrip2.Items.AddRange(new ToolStripItem[] { cerrarToolStripMenuItem4, maximizarToolStripMenuItem, minimizarToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(280, 28);
            menuStrip2.TabIndex = 10;
            menuStrip2.Text = "menuStrip2";
            menuStrip2.MouseDown += toolStrip1_MouseDown;
            // 
            // cerrarToolStripMenuItem4
            // 
            cerrarToolStripMenuItem4.Alignment = ToolStripItemAlignment.Right;
            cerrarToolStripMenuItem4.Image = Properties.Resources.icon_icons__1_;
            cerrarToolStripMenuItem4.Name = "cerrarToolStripMenuItem4";
            cerrarToolStripMenuItem4.Size = new Size(32, 24);
            cerrarToolStripMenuItem4.Click += cerrarToolStripMenuItem4_Click;
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
            // FormBuscar
            // 
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(280, 224);
            Controls.Add(menuStrip2);
            Controls.Add(txtSeccion);
            Controls.Add(txtDia);
            Controls.Add(txtCredito);
            Controls.Add(txtHora);
            Controls.Add(lbl1);
            Controls.Add(lbl2);
            Controls.Add(lbl3);
            Controls.Add(lbl4);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormBuscar";
            StartPosition = FormStartPosition.CenterParent;
            Load += FormBuscar_Load;
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private MenuStrip menuStrip2;
        private ToolStripMenuItem cerrarToolStripMenuItem4;
        private ToolStripMenuItem maximizarToolStripMenuItem;
        private ToolStripMenuItem minimizarToolStripMenuItem;
    }
}
