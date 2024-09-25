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

                    SqlDataReader reader = cmd.ExecuteReader();
                    var empleados = new DataTable();
                    empleados.Load(reader);

                    dgvEmpleados.ItemsSource = empleados.DefaultView;

                    reader.Close();
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

                    command.Parameters.Add(new SqlParameter("@IdEmpleado", SqlDbType.Int)
                    {
                        Value = string.IsNullOrEmpty(txtIdEmpleado2.Text) ? (object)DBNull.Value : int.Parse(txtIdEmpleado2.Text)
                    });

                    command.ExecuteNonQuery();

                    MessageBox.Show("Empleado eliminado exitosamente");

                    txtIdEmpleado2.Clear();

                    CargarEmpleados_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }




        private void ActualizarEmpleado_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtIdEmpleado3.Text) || !int.TryParse(txtIdEmpleado3.Text, out int idEmpleado))
            {
                MessageBox.Show("Por favor, ingrese un ID Empleado válido (solo números).");
                return; 
            }

            string connectionString = GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("USP_ActualizarEmpleado", conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@IdEmpleado", SqlDbType.Int) { Value = idEmpleado });
                    command.Parameters.Add(new SqlParameter("@Apellidos", SqlDbType.VarChar, 100) { Value = string.IsNullOrEmpty(txtApellidos2.Text) ? (object)DBNull.Value : txtApellidos2.Text });
                    command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 100) { Value = string.IsNullOrEmpty(txtNombre2.Text) ? (object)DBNull.Value : txtNombre2.Text });
                    command.Parameters.Add(new SqlParameter("@Cargo", SqlDbType.VarChar, 100) { Value = string.IsNullOrEmpty(txtCargo2.Text) ? (object)DBNull.Value : txtCargo2.Text });
                    command.Parameters.Add(new SqlParameter("@Tratamiento", SqlDbType.VarChar, 100) { Value = string.IsNullOrEmpty(txtTratamiento2.Text) ? (object)DBNull.Value : txtTratamiento2.Text });
                    command.Parameters.Add(new SqlParameter("@FechaNacimiento", SqlDbType.Date) { Value = dpFechaNacimiento3.SelectedDate.HasValue ? (object)dpFechaNacimiento3.SelectedDate.Value : DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@FechaContratacion", SqlDbType.Date) { Value = dpFechaContratacion3.SelectedDate.HasValue ? (object)dpFechaContratacion3.SelectedDate.Value : DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 60) { Value = string.IsNullOrEmpty(txtDireccion3.Text) ? (object)DBNull.Value : txtDireccion3.Text });
                    command.Parameters.Add(new SqlParameter("@Ciudad", SqlDbType.VarChar, 15) { Value = string.IsNullOrEmpty(txtCiudad3.Text) ? (object)DBNull.Value : txtCiudad3.Text });
                    command.Parameters.Add(new SqlParameter("@Region", SqlDbType.VarChar, 15) { Value = string.IsNullOrEmpty(txtRegion3.Text) ? (object)DBNull.Value : txtRegion3.Text });
                    command.Parameters.Add(new SqlParameter("@CodPostal", SqlDbType.VarChar, 10) { Value = string.IsNullOrEmpty(txtCodPostal3.Text) ? (object)DBNull.Value : txtCodPostal3.Text });
                    command.Parameters.Add(new SqlParameter("@Pais", SqlDbType.VarChar, 15) { Value = string.IsNullOrEmpty(txtPais3.Text) ? (object)DBNull.Value : txtPais3.Text });
                    command.Parameters.Add(new SqlParameter("@TelDomicilio", SqlDbType.VarChar, 24) { Value = string.IsNullOrEmpty(txtTelDomicilio3.Text) ? (object)DBNull.Value : txtTelDomicilio3.Text });
                    command.Parameters.Add(new SqlParameter("@Extension", SqlDbType.VarChar, 4) { Value = string.IsNullOrEmpty(txtExtension3.Text) ? (object)DBNull.Value : txtExtension3.Text });
                    command.Parameters.Add(new SqlParameter("@Notas", SqlDbType.Text) { Value = string.IsNullOrEmpty(txtNotas3.Text) ? (object)DBNull.Value : txtNotas3.Text });
                    command.Parameters.Add(new SqlParameter("@Jefe", SqlDbType.Int) { Value = string.IsNullOrEmpty(txtJefe3.Text) ? (object)DBNull.Value : int.Parse(txtJefe3.Text) });
                    command.Parameters.Add(new SqlParameter("@SueldoBasico", SqlDbType.Decimal) { Value = string.IsNullOrEmpty(txtSueldoBasico3.Text) ? (object)DBNull.Value : decimal.Parse(txtSueldoBasico3.Text) });
                    command.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = chkActivo3.IsChecked.HasValue ? (object)chkActivo3.IsChecked.Value : DBNull.Value });

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.RecordsAffected > 0)
                    {
                        MessageBox.Show("Empleado actualizado exitosamente");
                    }
                    else
                    {
                        MessageBox.Show("Empleado no encontrado");
                    }

                    reader.Close();
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

        private void dgvEmpleados_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (dgvEmpleados.SelectedItem is DataRowView selectedRow)
            {

                txtIdEmpleado.Text = selectedRow["IdEmpleado"].ToString();
                txtIdEmpleado2.Text = selectedRow["IdEmpleado"].ToString();
                txtIdEmpleado3.Text = selectedRow["IdEmpleado"].ToString();
                txtApellidos.Text = selectedRow["Apellidos"].ToString();
                txtApellidos2.Text = selectedRow["Apellidos"].ToString();
                txtNombre.Text = selectedRow["Nombre"].ToString();
                txtNombre2.Text = selectedRow["Nombre"].ToString();
                txtCargo.Text = selectedRow["Cargo"].ToString();
                txtCargo2.Text = selectedRow["Cargo"].ToString();
                txtTratamiento.Text = selectedRow["Tratamiento"].ToString();
                txtTratamiento2.Text = selectedRow["Tratamiento"].ToString();
                dpFechaNacimiento.SelectedDate = selectedRow["FechaNacimiento"] is DBNull ? (DateTime?)null : Convert.ToDateTime(selectedRow["FechaNacimiento"]);
                dpFechaNacimiento3.SelectedDate = selectedRow["FechaNacimiento"] is DBNull ? (DateTime?)null : Convert.ToDateTime(selectedRow["FechaNacimiento"]);
                dpFechaContratacion.SelectedDate = selectedRow["FechaContratacion"] is DBNull ? (DateTime?)null : Convert.ToDateTime(selectedRow["FechaContratacion"]);
                dpFechaContratacion3.SelectedDate = selectedRow["FechaContratacion"] is DBNull ? (DateTime?)null : Convert.ToDateTime(selectedRow["FechaContratacion"]);
                txtDireccion.Text = selectedRow["Direccion"].ToString();
                txtDireccion3.Text = selectedRow["Direccion"].ToString();
                txtCiudad.Text = selectedRow["Ciudad"].ToString();
                txtCiudad3.Text = selectedRow["Ciudad"].ToString();
                txtRegion.Text = selectedRow["Region"].ToString();
                txtRegion3.Text = selectedRow["Region"].ToString();
                txtCodPostal.Text = selectedRow["CodPostal"].ToString();
                txtCodPostal3.Text = selectedRow["CodPostal"].ToString();
                txtPais.Text = selectedRow["Pais"].ToString();
                txtPais3.Text = selectedRow["Pais"].ToString();
                txtTelDomicilio.Text = selectedRow["TelDomicilio"].ToString();
                txtTelDomicilio3.Text = selectedRow["TelDomicilio"].ToString();
                txtExtension.Text = selectedRow["Extension"].ToString();
                txtExtension3.Text = selectedRow["Extension"].ToString();
                txtNotas.Text = selectedRow["Notas"].ToString();
                txtNotas3.Text = selectedRow["Notas"].ToString();
                txtJefe.Text = selectedRow["Jefe"].ToString();
                txtJefe3.Text = selectedRow["Jefe"].ToString();
                txtSueldoBasico.Text = selectedRow["SueldoBasico"].ToString();
                txtSueldoBasico3.Text = selectedRow["SueldoBasico"].ToString();
                chkActivo.IsChecked = selectedRow["Activo"] is DBNull ? (bool?)null : Convert.ToBoolean(selectedRow["Activo"]);
                chkActivo3.IsChecked = selectedRow["Activo"] is DBNull ? (bool?)null : Convert.ToBoolean(selectedRow["Activo"]);
            }
        }


    }
}
