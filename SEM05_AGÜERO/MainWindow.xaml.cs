using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.ComponentModel.Design;

namespace SEM05_AGÜERO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string GetConnectionString()
        {
            return "Data Source=JESUS;Initial Catalog=NeptunoB;User ID=user01;Password=123456";
    
        }
        private void CargarEmpleados_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("USP_ListarEmpleados", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvEmpleados.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void RegistrarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("USP_InsertarEmpleados", conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@IdEmpleado", SqlDbType.Int) { Value = string.IsNullOrEmpty(txtIdEmpleado.Text) ? (object)DBNull.Value : int.Parse(txtIdEmpleado.Text) });
                    command.Parameters.Add(new SqlParameter("@Apellidos", SqlDbType.VarChar, 100) { Value = string.IsNullOrEmpty(txtApellidos.Text) ? (object)DBNull.Value : txtApellidos.Text });
                    command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 100) { Value = string.IsNullOrEmpty(txtNombre.Text) ? (object)DBNull.Value : txtNombre.Text });
                    command.Parameters.Add(new SqlParameter("@Cargo", SqlDbType.VarChar, 100) { Value = string.IsNullOrEmpty(txtCargo.Text) ? (object)DBNull.Value : txtCargo.Text });
                    command.Parameters.Add(new SqlParameter("@Tratamiento", SqlDbType.VarChar, 100) { Value = string.IsNullOrEmpty(txtTratamiento.Text) ? (object)DBNull.Value : txtTratamiento.Text });
                    command.Parameters.Add(new SqlParameter("@FechaNacimiento", SqlDbType.Date) { Value = dpFechaNacimiento.SelectedDate.HasValue ? (object)dpFechaNacimiento.SelectedDate.Value : DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@FechaContratacion", SqlDbType.Date) { Value = dpFechaContratacion.SelectedDate.HasValue ? (object)dpFechaContratacion.SelectedDate.Value : DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 60) { Value = string.IsNullOrEmpty(txtDireccion.Text) ? (object)DBNull.Value : txtDireccion.Text });
                    command.Parameters.Add(new SqlParameter("@Ciudad", SqlDbType.VarChar, 15) { Value = string.IsNullOrEmpty(txtCiudad.Text) ? (object)DBNull.Value : txtCiudad.Text });
                    command.Parameters.Add(new SqlParameter("@Region", SqlDbType.VarChar, 15) { Value = string.IsNullOrEmpty(txtRegion.Text) ? (object)DBNull.Value : txtRegion.Text });
                    command.Parameters.Add(new SqlParameter("@CodPostal", SqlDbType.VarChar, 10) { Value = string.IsNullOrEmpty(txtCodPostal.Text) ? (object)DBNull.Value : txtCodPostal.Text });
                    command.Parameters.Add(new SqlParameter("@Pais", SqlDbType.VarChar, 15) { Value = string.IsNullOrEmpty(txtPais.Text) ? (object)DBNull.Value : txtPais.Text });
                    command.Parameters.Add(new SqlParameter("@TelDomicilio", SqlDbType.VarChar, 24) { Value = string.IsNullOrEmpty(txtTelDomicilio.Text) ? (object)DBNull.Value : txtTelDomicilio.Text });
                    command.Parameters.Add(new SqlParameter("@Extension", SqlDbType.VarChar, 4) { Value = string.IsNullOrEmpty(txtExtension.Text) ? (object)DBNull.Value : txtExtension.Text });
                    command.Parameters.Add(new SqlParameter("@Notas", SqlDbType.Text) { Value = string.IsNullOrEmpty(txtNotas.Text) ? (object)DBNull.Value : txtNotas.Text });
                    command.Parameters.Add(new SqlParameter("@Jefe", SqlDbType.Int) { Value = string.IsNullOrEmpty(txtJefe.Text) ? (object)DBNull.Value : int.Parse(txtJefe.Text) });
                    command.Parameters.Add(new SqlParameter("@SueldoBasico", SqlDbType.Decimal) { Value = string.IsNullOrEmpty(txtSueldoBasico.Text) ? (object)DBNull.Value : decimal.Parse(txtSueldoBasico.Text) });
                    command.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = chkActivo.IsChecked.HasValue ? (object)chkActivo.IsChecked.Value : DBNull.Value });

                    command.ExecuteNonQuery();

                    MessageBox.Show("Empleado registrado exitosamente");

                    txtIdEmpleado.Clear();
                    txtApellidos.Clear();
                    txtNombre.Clear();
                    txtCargo.Clear();
                    txtTratamiento.Clear();
                    dpFechaNacimiento.SelectedDate = null;
                    dpFechaContratacion.SelectedDate = null;
                    txtDireccion.Clear();
                    txtCiudad.Clear();
                    txtRegion.Clear();
                    txtCodPostal.Clear();
                    txtPais.Clear();
                    txtTelDomicilio.Clear();
                    txtExtension.Clear();
                    txtNotas.Clear();
                    txtJefe.Clear();
                    txtSueldoBasico.Clear();
                    chkActivo.IsChecked = false;

                    CargarEmpleados_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void EliminarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("USP_EliminarEmpleado", conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Remove(value: txtIdEmpleado.Text);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Empleado Eliminado Exitosamente");

                    txtIdEmpleado.Clear();


                    EliminarEmpleado_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void ActualizarEmpleado_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
