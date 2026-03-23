using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Gestor_de_Horarios_de_Maestros
{
    public partial class Principal : Form
    {
        // Propiedad para la conexión (Evaluada cada vez que se usa)
        string connectionString => ConfigurationManager.ConnectionStrings["MiConexion"]?.ConnectionString;

        public Principal()
        {
            InitializeComponent();
            // Suscribimos el evento para mejorar el formato de las horas
            this.dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            ActualizarTodo();
        }

        private void ActualizarTodo()
        {
            try
            {
                CargarComboMaestros();
                CargarGrid();
            }
            catch (MySqlException)
            {
                MessageBox.Show("No se pudo conectar a la base de datos. Configure la conexión en el menú.",
                               "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarComboMaestros()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdMaestro", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));

            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string query = "SELECT IdMaestro, Nombre FROM Maestros ORDER BY Nombre ASC";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                    da.Fill(dt);
                }
            }
            catch { /* Si falla, el DT queda vacío para el "Todos" */ }

            DataRow filaTodos = dt.NewRow();
            filaTodos["IdMaestro"] = 0;
            filaTodos["Nombre"] = "👥 Todos"; // Ajustado para el nuevo diseño
            dt.Rows.InsertAt(filaTodos, 0);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "IdMaestro";
        }

        private void CargarGrid(string nombreMaestro = "", string seccion = "", string dia = "", string credito = "", string hora = "")
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT MaestroNombre AS 'Docente', IdMateria AS 'ID', Nombre AS 'Materia', 
                                     DiasImparte AS 'Días', Hora AS 'Hora', HD_Credito AS 'H/D Credito', 
                                     DiasMes AS 'Días Mes', TotalCredito AS 'Total Credito', 
                                     Inscritos AS 'Alum. Inscritos', Aula AS 'Aula', 
                                     Seccion AS 'Sección', Credito AS 'Créditos' 
                                     FROM HorariosView WHERE 1=1";

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;

                    // Filtro de Maestro (Ignora el emoji y "Todos")
                    if (!string.IsNullOrEmpty(nombreMaestro) && !nombreMaestro.Contains("Todos") && !nombreMaestro.Contains("DataRowView"))
                    {
                        query += " AND MaestroNombre LIKE @nombre";
                        cmd.Parameters.AddWithValue("@nombre", "%" + nombreMaestro + "%");
                    }

                    // Filtros adicionales del PDF
                    if (!string.IsNullOrEmpty(seccion)) { query += " AND Seccion LIKE @seccion"; cmd.Parameters.AddWithValue("@seccion", "%" + seccion + "%"); }
                    if (!string.IsNullOrEmpty(dia)) { query += " AND DiasImparte LIKE @dia"; cmd.Parameters.AddWithValue("@dia", "%" + dia + "%"); }
                    if (!string.IsNullOrEmpty(credito)) { query += " AND Credito = @credito"; cmd.Parameters.AddWithValue("@credito", credito); }
                    if (!string.IsNullOrEmpty(hora)) { query += " AND Hora LIKE @hora"; cmd.Parameters.AddWithValue("@hora", "%" + hora + "%"); }

                    query += " ORDER BY MaestroNombre ASC";
                    cmd.CommandText = query;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (MySqlException ex) { MessageBox.Show("Error de conexión: " + ex.Message); }
        }

        // ================= EVENTOS DE CONTROLES =================

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrid(comboBox1.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Focused && comboBox1.SelectedIndex != -1)
            {
                CargarGrid(comboBox1.Text, "", "", "", "");
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Formateo visual de la columna Hora del PDF
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Hora" && e.Value != null)
            {
                string valor = e.Value.ToString();
                if (valor.Contains("-"))
                {
                    string[] partes = valor.Split('-');
                    e.Value = $"{partes[0].Trim()} - {partes[1].Trim()}";
                    e.FormattingApplied = true;
                }
            }
        }

        // ================= MENÚ SUPERIOR =================

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormAgregar ventana = new FormAgregar()) { ventana.ShowDialog(); }
            ActualizarTodo();
        }

        private void asignarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormAsignar ventana = new FormAsignar())
            {
                if (ventana.ShowDialog() == DialogResult.OK) { ActualizarTodo(); }
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormModificar ventana = new FormModificar()) { ventana.ShowDialog(); }
            ActualizarTodo();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormBuscar fBusqueda = new FormBuscar())
            {
                if (fBusqueda.ShowDialog() == DialogResult.OK)
                {
                    CargarGrid(comboBox1.Text, fBusqueda.Seccion, fBusqueda.Dia, fBusqueda.Credito, fBusqueda.Hora);
                }
            }
        }

        private void removerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int idMateria = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
                string nombreMateria = dataGridView1.CurrentRow.Cells["Materia"].Value.ToString();

                if (MessageBox.Show($"¿Desea remover '{nombreMateria}'?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(connectionString))
                        {
                            con.Open();
                            string query = "DELETE FROM Materias WHERE IdMateria = @id";
                            using (MySqlCommand cmd = new MySqlCommand(query, con))
                            {
                                cmd.Parameters.AddWithValue("@id", idMateria);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        ActualizarTodo();
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                }
            }
        }

        private void conexiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ConfigConexion ventana = new ConfigConexion()) { ventana.ShowDialog(); }
            ActualizarTodo();
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e) => ActualizarTodo();

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormImprimir ventana = new FormImprimir()) { ventana.ShowDialog(); }
        }
    }
}
