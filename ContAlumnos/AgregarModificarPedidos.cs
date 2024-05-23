using ContAlumnos.Clases.Estudiantes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContAlumnos
{
    public partial class AgregarModificarPedidos : Form
    {
        public AgregarModificarPedidos()
        {
            InitializeComponent();
        }

        public bool EditMode { get; set; }
        public static double medidac;
        public static string computerName = Environment.MachineName;
        public static SqlConnection Conn = new SqlConnection($"Server = {computerName}; database=ContLetreros; Integrated Security=True");

        public void InitializeData(Int64 id, string nombre, string apellido, string cedula, string sexo, DateTime FechaIngreso)
        {
            /*txtnumero.Text = id.ToString();
            txtnombre.Text = nombre;
            txtapellido.Text = apellido;
            txtcedula.Text = cedula;
            cSexo.Text = sexo;
            fecha.Value = FechaIngreso;*/
        }

        private void VerificarAgregarModificarProducto(DatosgetInventario producto)
        {
            bool encontrado = false;

            // Recorrer las filas del DataGridView para buscar el producto
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Obtener el ID del producto en la fila actual
                Int64 id = Convert.ToInt64(row.Cells["codigo"].Value);

                if (id == producto.Numero)
                {
                    // El producto ya está en el DataGridView, modificar la cantidad
                    Int64 cantidadExistente = Convert.ToInt64(row.Cells["cantidad"].Value);
                    Int64 cantidadNueva = cantidadExistente + producto.Cantidad2;
                    row.Cells["cantidad"].Value = cantidadNueva;

                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                // El producto no está en el DataGridView, agregar una nueva fila
                dataGridView1.Rows.Add(producto.Numero, producto.Nombre, producto.Descripción, producto.Categoria, producto.Cantidad2, producto.UnidadMedida, producto.CostoUnitario, producto.FechaCaducidad);
            }
        }

        public void SumarColumna()
        {
            double Total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Total += Convert.ToDouble(row.Cells["Total"].Value);
            }

            Total = Total + medidac;
            txttotal.Text = Total.ToString();

        }

        void Sumar2()
        {
            const int valor = 144;

            decimal medida, medida2, precio, total, total2;

            medida = Convert.ToDecimal(txtancho.Text);
            medida2 = Convert.ToDecimal(txtlargo.Text);
            precio = Convert.ToDecimal(txtprecio.Text);

            total = medida * medida2 / valor * precio;

            total2 = Convert.ToDecimal(txttotal.Text);

            total2 = total2 + total;

            medidac = Convert.ToDouble(total2);

            SumarColumna();
        }

        private DatosgetInventario ObtenerProducto(Int64 idProducto)
        {
            DatosgetInventario producto = null;

            // Cadena de conexión a la base de datos
            string connectionString = $"Data Source={computerName}; Initial Catalog=ContLetreros; Integrated Security=True";

            // Consulta SQL para obtener el producto según su ID
            string query = "SELECT ID_MateriaPrima, Nombre, Descripción, Categoría, UnidadMedida, CostoUnitario, FechaCaducidad FROM Inventario WHERE ID_MateriaPrima = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", idProducto);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Obtener los valores del producto desde el lector de datos
                            Int32 id = reader.GetInt32(0);
                            string nombre = reader.GetString(1);
                            string descripcion = reader.GetString(2);
                            string categoria = reader.GetString(3);
                            string unidad = reader.GetString(4);
                            decimal costo = reader.GetDecimal(5);
                            DateTime fecha = reader.GetDateTime(6);

                            // Crear un objeto Producto con los valores obtenidos
                            producto = new DatosgetInventario
                            {
                                Numero = id,
                                Nombre = nombre,
                                Descripción = descripcion,
                                Categoria = categoria,
                                UnidadMedida = unidad,
                                CostoUnitario = costo,
                                FechaCaducidad = fecha,
                            };
                        }
                    }
                }
                Conn.Close();
            }

            return producto;

        }

        void dtagregar()
        {
            if (string.IsNullOrEmpty(txtcodigo.Text))
            {
                MessageBox.Show("Debe colocar el ID del producto");
                return;
            }

            Int64 idProducto = Convert.ToInt64(txtcodigo.Text);

            // Llamar a un método para obtener el producto completo según su ID
            DatosgetInventario producto = ObtenerProducto(idProducto);

            if (producto != null)
            {
                // Verificar si el producto ya está en el DataGridView y realizar la acción correspondiente
                VerificarAgregarModificarProducto(producto);

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Obtener los valores de las columnas "Columna1" y "Columna2"
                    double valor1 = Convert.ToDouble(row.Cells["costounitario"].Value);
                    int valor2 = Convert.ToInt32(row.Cells["cantidad"].Value);
                    // Realizar la multiplicación
                    double resultado = valor1 * valor2;

                    // Asignar el resultado a la columna "Resultado" de la fila actual
                    row.Cells["Total"].Value = resultado;
                    SumarColumna();
                }
            }
            else
            {
                // No se encontró un producto con el ID especificado, mostrar un mensaje de error o realizar alguna otra acción apropiada
                MessageBox.Show("No se encontró ningún producto con el ID especificado.");
            }


            txtcodigo.Clear();
        }

        private void txtnombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtapellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            dtagregar();
        }

        private void txtnumero_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            Sumar2();
        }
    }
}
