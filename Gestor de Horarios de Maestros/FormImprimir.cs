using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common; // Necesario para DbDataAdapter
using System.Data.SQLite;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Gestor_de_Horarios_de_Maestros
{
    public partial class FormImprimir : Form
    {
        // Detecta el modo de conexión
        private bool ModoLocal => Properties.Settings.Default.ModoConexion == "Local";

        public FormImprimir()
        {
            InitializeComponent();
        }

        // --- MÉTODOS DE CONEXIÓN HÍBRIDA ---

        private IDbConnection CrearConexion()
        {
            if (ModoLocal)
            {
                string dbPath = Path.Combine(Application.StartupPath, "LocalData.db");
                return new SQLiteConnection($"Data Source={dbPath};");
            }
            else
            {
                var settings = ConfigurationManager.ConnectionStrings["MiConexion"];
                if (settings == null)
                    throw new Exception("No se encontró la cadena de conexión 'MiConexion' en App.config.");

                return new MySqlConnection(settings.ConnectionString);
            }
        }

        private void CrearParametro(IDbCommand cmd, string nombre, object valor)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = nombre;
            param.Value = valor ?? DBNull.Value;
            cmd.Parameters.Add(param);
        }

        // --- LÓGICA DE DATOS ---

        private void CargarDatosParaReporte(string filtroMaestro = "")
        {
            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    string query = @"SELECT 
                                MaestroNombre AS 'Docente', 
                                IdMateria AS 'ID',
                                MateriaNombre AS 'Materia', 
                                Seccion AS 'Sección', 
                                DiasImparte AS 'Días', 
                                Hora AS 'Hora', 
                                Aula AS 'Aula'
                             FROM HorariosView WHERE 1=1";

                    IDbCommand cmd = con.CreateCommand();
                    cmd.Connection = con;

                    if (!string.IsNullOrEmpty(filtroMaestro) && filtroMaestro != "Todo")
                    {
                        query += " AND MaestroNombre LIKE @nombre";
                        CrearParametro(cmd, "@nombre", "%" + filtroMaestro + "%");
                    }

                    query += " ORDER BY MaestroNombre ASC";
                    cmd.CommandText = query;

                    // Adaptador universal para llenar el DataTable
                    DataTable dt = new DataTable();
                    con.Open();
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos para el reporte: " + ex.Message);
            }
        }

        private void LlenarFiltros()
        {
            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT DISTINCT MaestroNombre FROM HorariosView ORDER BY MaestroNombre ASC";

                    DataTable dt = new DataTable();
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }

                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Todo");
                    foreach (DataRow row in dt.Rows)
                    {
                        comboBox1.Items.Add(row["MaestroNombre"].ToString());
                    }
                    comboBox1.SelectedIndex = 0;
                }
            }
            catch (Exception ex) { /* Manejo silencioso o log */ }
        }

        // --- EXPORTACIÓN A PDF ---

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog guardar = new SaveFileDialog();
                guardar.Filter = "Archivo PDF (*.pdf)|*.pdf";
                guardar.FileName = "Reporte_Horarios.pdf";

                if (guardar.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        PdfWriter.GetInstance(pdfDoc, new FileStream(guardar.FileName, FileMode.Create));
                        pdfDoc.Open();

                        // Encabezado
                        var fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                        pdfDoc.Add(new Paragraph("Reporte del Gestor de Horarios Universitario", fuenteTitulo));
                        pdfDoc.Add(new Paragraph($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n"));

                        PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                        pdfTable.WidthPercentage = 100;

                        // Cabeceras de la tabla
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                            cell.BackgroundColor = new BaseColor(240, 240, 240);
                            pdfTable.AddCell(cell);
                        }

                        // Filas de la tabla
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value?.ToString() ?? "");
                                }
                            }
                        }

                        pdfDoc.Add(pdfTable);
                        pdfDoc.Close();
                        MessageBox.Show("¡Reporte guardado exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al crear el PDF: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para imprimir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- EVENTOS DEL FORMULARIO ---

        private void FormImprimir_Load(object sender, EventArgs e)
        {
            LlenarFiltros();
            CargarDatosParaReporte("");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;

            string seleccion = comboBox1.SelectedItem.ToString();
            if (seleccion == "Todo")
            {
                CargarDatosParaReporte("");
            }
            else
            {
                CargarDatosParaReporte(seleccion);
            }
        }

        private void maximizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void minimizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void cerrarToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void MoverVentana()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            // Solo permitimos mover si se hace clic con el botón izquierdo
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

    }
}