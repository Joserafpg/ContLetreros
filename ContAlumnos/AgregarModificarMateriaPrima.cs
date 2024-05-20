using ContAlumnos.Clases.Inventario;
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
using static System.Net.WebRequestMethods;

namespace ContAlumnos.Clases.Estudiantes
{
    public partial class AgregarModificarMateriaPrima : Form
    {
        public AgregarModificarMateriaPrima()
        {
            InitializeComponent();
        }

        public bool EditMode { get; set; }
        public string ULTFactura;

        public static string computerName = Environment.MachineName;
        public static SqlConnection Conn = new SqlConnection($"Server = {computerName}; database=ContLetreros; Integrated Security=True");

        public void InitializeData(Int64 id, string nombre, string descripcion, string categoria, int cantidad, decimal unidadmedida, decimal costo, DateTime vcompra, DateTime vcaducidad)
        {
            txtcodigo.Text = id.ToString();
            txtnombre.Text = nombre;
            txtdescripcion.Text = descripcion;
            cCategoria.Text = categoria;
            txtcantidad.Text = Convert.ToString(cantidad);
            cUnidadMedida.Text = Convert.ToString(unidadmedida);
            txtcosto.Text = Convert.ToString(costo);
            caducidad.Value = vcaducidad;
            compra.Value = vcompra;
        }

        private int Buscar(string curso, string seccion, string area)
        {
            int numeroMasAlto = 0;
            string query = "SELECT MAX(Numero) AS NumeroMaximo FROM Estudiantes WHERE Curso = @Curso AND Seccion = @Seccion AND Area = @Area";

            using (SqlCommand cmd = new SqlCommand(query, Conn))
            {
                // Agregar parámetros para prevenir la inyección SQL
                cmd.Parameters.AddWithValue("@Curso", curso);
                cmd.Parameters.AddWithValue("@Seccion", seccion);
                cmd.Parameters.AddWithValue("@Area", area);

                Conn.Open();
                // Ejecutar la consulta y obtener el número más alto
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    numeroMasAlto = Convert.ToInt32(result);
                }
                Conn.Close();
            }

            return numeroMasAlto;
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
            txttotal.Text = Total.ToString();

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            if (EditMode)
            {
                DatosgetInventario pEstudiantes = new DatosgetInventario();
                pEstudiantes.Numero = Convert.ToInt64(txtcodigo.Text);
                pEstudiantes.Nombre = txtnombre.Text;
                pEstudiantes.Descripción = txtdescripcion.Text;
                pEstudiantes.Categoria = cCategoria.Text;
                pEstudiantes.Cantidad = Convert.ToInt64(txtcantidad.Text);
                pEstudiantes.UnidadMedida = cUnidadMedida.Text;
                pEstudiantes.CostoUnitario = Convert.ToDecimal(txtcosto.Text);
                pEstudiantes.FechaCaducidad = caducidad.Value;
                pEstudiantes.FechaCompra = compra.Value;

                int Resultado = DatosbaseInventario.Modificar(pEstudiantes);

                if (Resultado > 0)
                {
                    MessageBox.Show("Alumno Modificado con exito", "Alumno modificado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("No se pudo modificar el alumno", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // Indica que la operación fue exitosa
                this.DialogResult = DialogResult.OK;
                this.Close();
            }


            else
            {
                if (/*!string.IsNullOrEmpty(txtnumero.Text) && !string.IsNullOrEmpty(txtnombre.Text) && !string.IsNullOrEmpty(txtapellido.Text) &&*/ !string.IsNullOrEmpty(cUnidadMedida.Text))
                {
                    MateriaPrima pEstudiantes = new MateriaPrima();
                    pEstudiantes.FechaCompra = compra.Value;
                    pEstudiantes.TotalCompra = Convert.ToDecimal(txttotal.Text);

                    int Resultado = DatosbaseInventario.Agregar2(pEstudiantes);
                    Conn.Open();

                    string query = "SELECT TOP 1 CompraID FROM Compras ORDER BY CompraID DESC";
                    using (SqlCommand command = new SqlCommand(query, Conn))
                    {
                        // Obtener el resultado de la consulta
                        object result = command.ExecuteScalar();

                        // Verificar si se obtuvo un resultado válido
                        if (result != null && result != DBNull.Value)
                        {
                            // Convertir el resultado en un entero
                            int ultimoIdFactura = Convert.ToInt32(result);

                            // Mostrar el último Id_Factura en un TextBox
                            ULTFactura = ultimoIdFactura.ToString();
                        }

                        else
                        {
                            // No se encontraron registros en la tabla FacturaTittle
                            // Puedes mostrar un valor predeterminado o dejar el TextBox vacío
                            ULTFactura = "No hay registros";
                        }
                    }

                    Conn.Close();

                    SqlCommand agregar = new SqlCommand("INSERT INTO DetalleCompras VALUES (@CompraID , @ID_MateriaPrima, @MateriaPrima, @Cantidad, @CostoUnitario, @Total)", Conn);
                    string verificarQuery = "SELECT Cantidad FROM Inventario WHERE Nombre = @MateriaPrima";
                    string actualizarQuery = "UPDATE Inventario SET Cantidad = Cantidad + @Cantidad WHERE Nombre = @MateriaPrima";

                    Conn.Open();

                    try
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            // Obtener los valores de la fila actual del DataGridView
                            Int64 idfactura = Convert.ToInt64(ULTFactura);
                            int id_producto = Convert.ToInt32(row.Cells["codigo"].Value);
                            string producto = Convert.ToString(row.Cells["nombre"].Value);
                            string descripcion = Convert.ToString(row.Cells["descripcion"].Value);
                            int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);
                            decimal precio = Convert.ToDecimal(row.Cells["costounitario"].Value);
                            decimal total = Convert.ToDecimal(row.Cells["Total"].Value);

                            // Verificar si el Stock es menor que la Cantidad
                            using (SqlCommand verificarCmd = new SqlCommand(verificarQuery, Conn))
                            {
                                verificarCmd.Parameters.AddWithValue("@MateriaPrima", producto);
                                int stock = Convert.ToInt32(verificarCmd.ExecuteScalar());

                                if (stock < cantidad)
                                {
                                    MessageBox.Show("No hay suficiente stock para el producto " + producto);
                                    return; // Salta a la siguiente iteración del bucle sin ejecutar el código restante
                                }
                            }

                            // Agregar los parámetros al comando
                            agregar.Parameters.Clear();
                            agregar.Parameters.AddWithValue("@CompraID", idfactura);
                            agregar.Parameters.AddWithValue("@ID_MateriaPrima", id_producto);
                            agregar.Parameters.AddWithValue("@MateriaPrima", producto);
                            agregar.Parameters.AddWithValue("@Cantidad", cantidad);
                            agregar.Parameters.AddWithValue("@CostoUnitario", precio);
                            agregar.Parameters.AddWithValue("@Total", total);

                            // Ejecutar el comando para agregar la factura
                            agregar.ExecuteNonQuery();

                            // Actualizar los datos de la tabla productos
                            using (SqlCommand actualizarCmd = new SqlCommand(actualizarQuery, Conn))
                            {
                                actualizarCmd.Parameters.AddWithValue("@MateriaPrima", producto);
                                actualizarCmd.Parameters.AddWithValue("@Cantidad", cantidad);
                                actualizarCmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Facturado con exito");
                        dataGridView1.Rows.Clear();
                        //Limpiar();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar: " + ex.Message);
                    }

                    finally
                    {
                        Conn.Close();
                    }

                    // Indica que la operación fue exitosa
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Por favor llena todo los campos", "Llenar campos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
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
    }
}
