using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gestor_de_Horarios_de_Maestros
{
    public partial class Principal : Form
    {

        private bool ModoLocal = false;

        // Propiedad para la conexión (Evaluada cada vez que se usa)
        string connectionString
        {
            get
            {
                string nombreConexion = ModoLocal ? "BaseLocal" : "MiConexion";
                var config = ConfigurationManager.ConnectionStrings[nombreConexion];

                if (config == null)
                {
                    // Fallback por si una de las dos no existe aún en el config
                    return ConfigurationManager.ConnectionStrings["MiConexion"]?.ConnectionString;
                }
                return config.ConnectionString;
            }
        }

        public Principal()
        {
            CargarResolverSQLite();
            InitializeComponent();

            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            Label miLabel = new Label();
            miLabel.Text = "Gestor de Horarios - O&M: La Romana";
            miLabel.ForeColor = Color.White; // Ajusta según tu diseño
            miLabel.BackColor = Color.Transparent;
            label1.Font = new System.Drawing.Font(label1.Font, FontStyle.Bold);
            ToolStripControlHost host = new ToolStripControlHost(miLabel);
            menuStrip2.Items.Add(host);

            // Suscribimos el evento para mejorar el formato de las horas
            this.dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
        }

        private void CargarResolverSQLite()
        {
            string rPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                        Environment.Is64BitProcess ? "x64" : "x86",
                                        "SQLite.Interop.dll");

            if (File.Exists(rPath))
            {
                // Esto le dice a Windows dónde está el "puente" de SQLite exactamente
                LoadLibrary(rPath);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        private IDbConnection CrearConexion()
        {
            if (ModoLocal)
            {
                string dbPath = Path.Combine(Application.StartupPath, "LocalData.db");
                // Quitamos "Version=3" si da problemas, usualmente solo "Data Source=" basta
                return new SQLiteConnection($"Data Source={dbPath};");
            }
            else
            {
                string remoteString = ConfigurationManager.ConnectionStrings["MiConexion"]?.ConnectionString;
                return new MySqlConnection(remoteString);
            }
        }

        private DbDataAdapter CrearAdapter(string query, IDbConnection con)
        {
            if (ModoLocal)
                return new SQLiteDataAdapter(query, (SQLiteConnection)con);
            else
                return new MySqlDataAdapter(query, (MySqlConnection)con);
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            // Intentamos asegurar que la BD local exista siempre
            InicializarBaseDeDatosLocal();

            // Recuperamos el modo guardado (como hicimos en el paso anterior)
            string modoGuardado = Properties.Settings.Default.ModoConexion;
            ModoLocal = (modoGuardado == "Local");

            ConfigurarInterfazSegunModo();
            ActualizarTodo();
        }

        private void ConfigurarInterfazSegunModo()
        {
            localToolStripMenuItem.Checked = ModoLocal;
            localToolStripMenuItem.Text = ModoLocal ? "🌐 Cambiar a Remota" : "💻 Cambiar a Local";
        }

        private void ActualizarTodo()
        {
            try
            {
                CargarComboMaestros();
                CargarComboCuatrimestres();
                CargarGrid();
            }
            catch (MySqlException)
            {
                MessageBox.Show("No se pudo conectar a la base de datos. Configure la conexión en el menú.",
                               "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InicializarBaseDeDatosLocal()
        {
            string dbPath = Path.Combine(Application.StartupPath, "LocalData.db");

            // Si el archivo no existe, SQLite lo crea automáticamente al abrir la conexión
            string connectionStringLocal = $"Data Source={dbPath};Version=3;";

            using (SQLiteConnection con = new SQLiteConnection(connectionStringLocal))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = con.CreateCommand();

                    // 1. Tabla: Maestros
                    // En SQLite, INTEGER PRIMARY KEY ya implica auto-incremento automático
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Maestros (
                    IdMaestro INTEGER PRIMARY KEY, 
                    Nombre TEXT NOT NULL
                  );";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Cuatrimestres (
                    IdCuatrimestre INTEGER PRIMARY KEY,
                    Nombre TEXT NOT NULL,
                    FechaInicio TEXT,
                    FechaFin TEXT
                  );";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Materias (
                    IdMateria INTEGER PRIMARY KEY,
                    Nombre TEXT NOT NULL,
                    DiasImparte TEXT,
                    Hora TEXT,
                    HD_Credito INTEGER,
                    DiasMes INTEGER,
                    TotalCredito INTEGER,
                    Inscritos INTEGER,
                    Aula TEXT,
                    Seccion TEXT,
                    Credito INTEGER,
                    IdMaestro INTEGER,
                    IdCuatrimestre INTEGER,
                    FOREIGN KEY (IdMaestro) REFERENCES Maestros (IdMaestro),
                    FOREIGN KEY (IdCuatrimestre) REFERENCES Cuatrimestres (IdCuatrimestre)
                  );";
                    cmd.ExecuteNonQuery();

                    // 4. Vista: HorariosView (SQLite también soporta vistas)
                    cmd.CommandText = @"CREATE VIEW IF NOT EXISTS HorariosView AS
                                SELECT 
                                    m.Nombre AS MaestroNombre,
                                    mat.IdMateria,
                                    mat.Nombre AS MateriaNombre,
                                    c.Nombre AS Cuatrimestre,
                                    mat.DiasImparte,
                                    mat.Hora,
                                    mat.HD_Credito,
                                    mat.DiasMes,
                                    mat.TotalCredito,
                                    mat.Inscritos,
                                    mat.Aula,
                                    mat.Seccion,
                                    mat.Credito
                                FROM Materias mat
                                INNER JOIN Maestros m ON mat.IdMaestro = m.IdMaestro
                                LEFT JOIN Cuatrimestres c ON mat.IdCuatrimestre = c.IdCuatrimestre;";
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al crear base de datos portátil: " + ex.Message);
                }
            }
        }

        private void CargarComboMaestros()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdMaestro", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));

            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    string query = "SELECT IdMaestro, Nombre FROM Maestros ORDER BY Nombre ASC";
                    IDataAdapter da = CrearAdapter(query, con);
                    if (da is SQLiteDataAdapter sda) sda.Fill(dt);
                    else if (da is MySqlDataAdapter mda) mda.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar maestros: " + ex.Message);
            }

            DataRow filaTodos = dt.NewRow();
            filaTodos["IdMaestro"] = 0;
            filaTodos["Nombre"] = "👥 Todos";
            dt.Rows.InsertAt(filaTodos, 0);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "IdMaestro";
        }

        private void CargarGrid(string nombreMaestro = "", string seccion = "", string dia = "", string credito = "", string hora = "", string cuatrimestre = "")
        {
            try
            {
                using (IDbConnection con = CrearConexion()) // <--- CAMBIO: Genérico
                {
                    string query = @"SELECT MaestroNombre AS 'Docente', 
                             IdMateria AS 'ID', 
                             MateriaNombre AS 'Materia',
                             Cuatrimestre AS 'Cuatrimestre',
                             DiasImparte AS 'Días', 
                             Hora AS 'Hora', 
                             HD_Credito AS 'H/D Credito', 
                             DiasMes AS 'Días Mes', 
                             TotalCredito AS 'Total Credito', 
                             Inscritos AS 'Alum. Inscritos', 
                             Aula AS 'Aula', 
                             Seccion AS 'Sección', 
                             Credito AS 'Créditos' 
                             FROM HorariosView WHERE 1=1";

                    // Usamos el comando según la conexión activa
                    IDbCommand cmd = con.CreateCommand();
                    cmd.Connection = con;

                    if (!string.IsNullOrEmpty(nombreMaestro) && !nombreMaestro.Contains("Todos") && !nombreMaestro.Contains("DataRowView"))
                    {
                        query += " AND MaestroNombre LIKE @nombre";
                        CrearParametro(cmd, "@nombre", "%" + nombreMaestro + "%");
                    }

                    if (!string.IsNullOrEmpty(seccion)) { query += " AND Seccion LIKE @seccion"; CrearParametro(cmd, "@seccion", "%" + seccion + "%"); }
                    if (!string.IsNullOrEmpty(dia)) { query += " AND DiasImparte LIKE @dia"; CrearParametro(cmd, "@dia", "%" + dia + "%"); }
                    if (!string.IsNullOrEmpty(credito)) { query += " AND Credito = @credito"; CrearParametro(cmd, "@credito", credito); }
                    if (!string.IsNullOrEmpty(hora)) { query += " AND Hora LIKE @hora"; CrearParametro(cmd, "@hora", "%" + hora + "%"); }
                    if (!string.IsNullOrEmpty(cuatrimestre) && !cuatrimestre.Contains("Todos") && !cuatrimestre.Contains("DataRowView"))
                    {
                        query += " AND Cuatrimestre LIKE @cuatrimestre";
                        CrearParametro(cmd, "@cuatrimestre", "%" + cuatrimestre + "%");
                    }

                    query += " ORDER BY MaestroNombre ASC";
                    cmd.CommandText = query;

                    DataTable dt = new DataTable();
                    // Usamos DbDataAdapter para evitar el error de conversión de DataTable/DataSet
                    DbDataAdapter da = (DbDataAdapter)CrearAdapter(query, con);

                    // Asignar parámetros al adapter (esto es necesario en algunos proveedores)
                    if (da is MySqlDataAdapter mda) mda.SelectCommand = (MySqlCommand)cmd;
                    else if (da is SQLiteDataAdapter sda) sda.SelectCommand = (SQLiteConnection)con != null ? (SQLiteCommand)cmd : null;

                    da.Fill(dt); // <--- Ya no dará error
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar datos: " + ex.Message); }
        }

        // Método auxiliar para manejar parámetros de forma genérica
        private void CrearParametro(IDbCommand cmd, string nombre, object valor)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = nombre;
            param.Value = valor;
            cmd.Parameters.Add(param);
        }

        // ================= EVENTOS DE CONTROLES =================

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string cuatrimestre = comboBox2.SelectedValue?.ToString() == "0" ? "" : comboBox2.Text;
            CargarGrid(comboBox1.Text, "", "", "", "", cuatrimestre);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Focused && comboBox1.SelectedIndex != -1)
            {
                CargarGrid(comboBox1.Text, "", "", "", "", comboBox2.SelectedValue?.ToString() == "0" ? "" : comboBox2.Text);
            }
        }

        private void CargarComboCuatrimestres()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdCuatrimestre", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));

            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    string query = "SELECT IdCuatrimestre, Nombre FROM Cuatrimestres ORDER BY Nombre ASC";
                    IDataAdapter da = CrearAdapter(query, con);
                    if (da is SQLiteDataAdapter sda) sda.Fill(dt);
                    else if (da is MySqlDataAdapter mda) mda.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar cuatrimestres: " + ex.Message);
            }

            DataRow filaTodos = dt.NewRow();
            filaTodos["IdCuatrimestre"] = 0;
            filaTodos["Nombre"] = "📅 Todos";
            dt.Rows.InsertAt(filaTodos, 0);

            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "IdCuatrimestre";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Focused && comboBox2.SelectedIndex != -1)
            {
                string cuatrimestre = comboBox2.SelectedValue?.ToString() == "0" ? "" : comboBox2.Text;
                string maestro = comboBox1.SelectedValue?.ToString() == "0" ? "" : comboBox1.Text;
                CargarGrid(maestro, "", "", "", "", cuatrimestre);
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
                        using (IDbConnection con = CrearConexion()) // <--- Genérico
                        {
                            con.Open();
                            string query = "DELETE FROM Materias WHERE IdMateria = @id";
                            using (IDbCommand cmd = con.CreateCommand())
                            {
                                cmd.CommandText = query;
                                CrearParametro(cmd, "@id", idMateria);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        ActualizarTodo();
                    }
                    catch (Exception ex) { MessageBox.Show("Error al eliminar: " + ex.Message); }
                }
            }
        }

        private void conexiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ConfigConexion ventana = new ConfigConexion())
            {
                if (ventana.ShowDialog() == DialogResult.OK)
                {
                    // Forzamos modo remoto
                    ModoLocal = false;
                    localToolStripMenuItem.Checked = false;
                    localToolStripMenuItem.Text = "💻 Cambiar a Local";

                    // Guardamos que ahora el modo preferido es Remoto
                    Properties.Settings.Default.ModoConexion = "Remoto";
                    Properties.Settings.Default.Save();

                    ActualizarTodo();
                }
            }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e) => ActualizarTodo();

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormImprimir ventana = new FormImprimir()) { ventana.ShowDialog(); }
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

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Invertimos el modo
            ModoLocal = !ModoLocal;
            localToolStripMenuItem.Checked = ModoLocal;

            // 3. Guardamos la preferencia de forma permanente
            Properties.Settings.Default.ModoConexion = ModoLocal ? "Local" : "Remoto";
            Properties.Settings.Default.Save(); // ¡Importante para que no se olvide!

            if (ModoLocal)
            {
                localToolStripMenuItem.Text = "🌐 Cambiar a Remota";
                MessageBox.Show("Cambiado a Modo LOCAL", "Persistencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                localToolStripMenuItem.Text = "💻 Cambiar a Local";
                MessageBox.Show("Cambiado a Modo REMOTO", "Persistencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ActualizarTodo();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // 1. Obtener los datos de la fila
            var fila = dataGridView1.Rows[e.RowIndex];
            string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string nombre = fila.Cells["Materia"].Value.ToString();
            string IDMateria = fila.Cells["ID"].Value.ToString();

            // 2. Preguntar
            if (MessageBox.Show($"¿Eliminar {IDMateria} - {nombre}?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var con = CrearConexion())
                    {
                        con.Open();
                        var cmd = con.CreateCommand();
                        cmd.CommandText = "DELETE FROM Materias WHERE IdMateria = @id";
                        CrearParametro(cmd, "@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    // 3. AHORA SÍ: Si se borró en la DB, lo quitamos del Grid visualmente
                    dataGridView1.Rows.RemoveAt(e.RowIndex);

                    MessageBox.Show("Eliminado con éxito.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        private void guiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://drive.google.com/file/d/1UNHbvbF-oWdM0VZK9ZTiwZxM3lkRS80N/view?usp=sharing";

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Crucial para que Windows reconozca que es una URL
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el navegador: " + ex.Message);
            }
        }
    }
}
