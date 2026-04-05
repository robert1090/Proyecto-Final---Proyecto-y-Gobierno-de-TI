namespace Gestor_de_Horarios_de_Maestros
{
    partial class FormImprimir
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
            comboBox1 = new ComboBox();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            menuStrip2 = new MenuStrip();
            cerrarToolStripMenuItem5 = new ToolStripMenuItem();
            maximizarToolStripMenuItem = new ToolStripMenuItem();
            minimizarToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(79, 39);
            comboBox1.Margin = new Padding(2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(171, 23);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(253, 40);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(78, 20);
            button1.TabIndex = 1;
            button1.Text = "Imprimir Reporte";
            button1.TextAlign = ContentAlignment.TopCenter;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(11, 66);
            dataGridView1.Margin = new Padding(2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(531, 234);
            dataGridView1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(15, 41);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 3;
            label1.Text = "Maestro:";
            // 
            // menuStrip2
            // 
            menuStrip2.BackColor = Color.FromArgb(20, 20, 20);
            menuStrip2.ImageScalingSize = new Size(20, 20);
            menuStrip2.Items.AddRange(new ToolStripItem[] { cerrarToolStripMenuItem5, maximizarToolStripMenuItem, minimizarToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Padding = new Padding(5, 2, 0, 2);
            menuStrip2.Size = new Size(553, 28);
            menuStrip2.TabIndex = 4;
            menuStrip2.Text = "menuStrip2";
            menuStrip2.MouseDown += toolStrip1_MouseDown;
            // 
            // cerrarToolStripMenuItem5
            // 
            cerrarToolStripMenuItem5.Alignment = ToolStripItemAlignment.Right;
            cerrarToolStripMenuItem5.Image = Properties.Resources.icon_icons__1_;
            cerrarToolStripMenuItem5.Name = "cerrarToolStripMenuItem5";
            cerrarToolStripMenuItem5.Size = new Size(32, 24);
            cerrarToolStripMenuItem5.Click += cerrarToolStripMenuItem5_Click;
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
            // FormImprimir
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 36);
            ClientSize = new Size(553, 311);
            Controls.Add(menuStrip2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "FormImprimir";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Imprimir";
            Load += FormImprimir_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private Button button1;
        private DataGridView dataGridView1;
        private Label label1;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem cerrarToolStripMenuItem5;
        private ToolStripMenuItem maximizarToolStripMenuItem;
        private ToolStripMenuItem minimizarToolStripMenuItem;
    }
}