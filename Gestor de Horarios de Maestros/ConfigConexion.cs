using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Gestor_de_Horarios_de_Maestros
{
    public partial class ConfigConexion : Form
    {
        public ConfigConexion()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Construimos la cadena con los datos de los TextBox
            string nuevaCadena = $"Server={txtServer.Text};Database={txtDatabase.Text};Uid={txtUser.Text};Pwd={txtPassword.Text};";

            try
            {
                // 1. Abrimos la configuración del archivo ejecutable
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // 2. Intentamos obtener la conexión "MiConexion"
                var conexionExistente = config.ConnectionStrings.ConnectionStrings["MiConexion"];

                if (conexionExistente != null)
                {
                    // Si ya existe, simplemente actualizamos su valor
                    conexionExistente.ConnectionString = nuevaCadena;
                }
                else
                {
                    // Si NO existe (primera vez), creamos un nuevo objeto de configuración
                    ConnectionStringSettings nuevaConfig = new ConnectionStringSettings();
                    nuevaConfig.Name = "MiConexion";
                    nuevaConfig.ConnectionString = nuevaCadena;
                    nuevaConfig.ProviderName = "MySql.Data.MySqlClient";

                    // Lo agregamos a la sección de cadenas de conexión
                    config.ConnectionStrings.ConnectionStrings.Add(nuevaConfig);
                }

                // 3. Guardamos los cambios de forma permanente en el disco
                config.Save(ConfigurationSaveMode.Modified);

                // 4. Forzamos la recarga en memoria
                ConfigurationManager.RefreshSection("connectionStrings");

                MessageBox.Show("Conexión guardada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Indicamos que el resultado fue exitoso para que el Form Principal sepa que debe refrescar
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la configuración: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDatabase_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
