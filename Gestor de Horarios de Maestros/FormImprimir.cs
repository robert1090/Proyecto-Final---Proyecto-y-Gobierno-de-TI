using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Gestor_de_Horarios_de_Maestros
{
    public partial class FormImprimir : Form
    {
        string connectionString => ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

        public FormImprimir()
        {
            InitializeComponent();
        }

        private void CargarDatosParaReporte(string filtroMaestro = "")
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                MaestroNombre AS 'Docente', 
                                MateriaNombre AS 'Materia', 
                                Seccion AS 'Sección', 
                                DiasImparte AS 'Días', 
                                Hora AS 'Hora', 
                                Aula AS 'Aula'
                             FROM HorariosView WHERE 1=1";

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;

                    if (!string.IsNullOrEmpty(filtroMaestro) && filtroMaestro != "Todo")
                    {
                        query += " AND MaestroNombre LIKE @nombre";
                        cmd.Parameters.AddWithValue("@nombre", "%" + filtroMaestro + "%");
                    }

                    query += " ORDER BY MaestroNombre ASC";
                    cmd.CommandText = query;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar datos para el reporte: " + ex.Message);
            }
        }

        private void LlenarFiltros()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string query = "SELECT DISTINCT MaestroNombre FROM HorariosView ORDER BY MaestroNombre ASC";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Todo");
                    foreach (DataRow row in dt.Rows)
                    {
                        comboBox1.Items.Add(row["MaestroNombre"].ToString());
                    }
                    comboBox1.SelectedIndex = 0;
                }
            }
            catch { }
        }

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
                        pdfDoc.Add(new Paragraph("Reporte del Gestor de Horarios Universitario\n\n"));

                        PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                        pdfTable.WidthPercentage = 100;

                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                            pdfTable.AddCell(cell);
                        }

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

        private void FormImprimir_Load(object sender, EventArgs e)
        {
            LlenarFiltros(); // Importante: Llenamos el combo primero
            CargarDatosParaReporte(""); // Luego cargamos la tabla
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
    }
}