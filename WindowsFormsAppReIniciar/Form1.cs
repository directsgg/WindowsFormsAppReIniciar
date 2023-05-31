using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppReIniciar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string consulta = "SELECT * FROM v_ListadodeClientes";
            SqlDataAdapter da = new SqlDataAdapter(consulta, ClassGeneral.cadenaCone);
            DataTable tabla = new DataTable();
            da.Fill(tabla);
            dataGridViewPrincipal.DataSource = tabla;

            //cargar los departamentos
            consulta = "SELECT codigo_departamento AS Codigo, nombre_departamento AS Departamento FROM Departamento";
            da = new SqlDataAdapter(consulta, ClassGeneral.cadenaCone);
            tabla = new DataTable();
            da.Fill(tabla);
            comboBoxDepartamento.DataSource = tabla;
            comboBoxDepartamento.DisplayMember = "Departamento";
            comboBoxDepartamento.ValueMember = "Codigo";
        }

        private void dataGridViewPrincipal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBoxDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int coddepa = Convert.ToInt32(comboBoxDepartamento.SelectedValue);
                string consulta = "SELECT codigo_municipio AS Codigo, nombre_municipio AS Municipio FROM Municipio WHERE codigo_departamento = " + coddepa;
                SqlDataAdapter da = new SqlDataAdapter(consulta, ClassGeneral.cadenaCone);
                DataTable tabla = new DataTable();
                da.Fill(tabla);
                comboBoxMunicipio.DataSource = tabla;
                comboBoxMunicipio.DisplayMember = "Municipio";
                comboBoxMunicipio.ValueMember = "Codigo";
            }
            catch
            {

            }
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            string consulta = "EXEC ABMCliente " + textBoxNit.Text + "," +
                textBoxNombre.Text + "," + textBoxApellido.Text + "," +
                textBoxDireccion.Text + "," + textBoxTelefono.Text + ",'" +
                dateTimePickerFNac.Value.ToString("yyy-MM-dd") + "'," + comboBoxMunicipio.SelectedValue;
            SqlConnection conexion = new SqlConnection(ClassGeneral.cadenaCone);
            SqlCommand comando = new SqlCommand(consulta, conexion);
            conexion.Close();
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Cliente ingresado con exito");
        }
    }
}
