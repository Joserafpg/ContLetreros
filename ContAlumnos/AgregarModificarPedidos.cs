using ContAlumnos.Clases.Estudiantes;
using ContAlumnos.Clases.Inventario;
using ContAlumnos.Clases.Login;
using ContAlumnos.Clases.Ventas;
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

        public string ccodigo, cnombre;

        public Int64 ULTFactura;
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
                if (row.Cells["Total"].Value != null)
                {
                    Total += Convert.ToDouble(row.Cells["Total"].Value);
                }
            }

            // Total ya incluye los valores de la columna Total del DataGridView
            txttotal.Text = Total.ToString();
        }

        void Sumar2()
        {
            const int valor = 144;

            decimal medida, medida2, precio, total;

            // Asegúrate de que los TextBox no estén vacíos
            if (!decimal.TryParse(txtancho.Text, out medida) ||
                !decimal.TryParse(txtlargo.Text, out medida2) ||
                !decimal.TryParse(txtprecio.Text, out precio))
            {
                // Manejar la entrada inválida
                MessageBox.Show("Por favor ingresa valores válidos.");
                return;
            }

            // Calcula el nuevo total basado en las medidas actuales
            total = medida * medida2 / valor * precio;

            // Actualiza el TextBox del total con el nuevo valor calculado
            txttotal.Text = total.ToString();

            // Llama a SumarColumna para recalcular el total del DataGridView y sumar con el nuevo total calculado
            double totalGrid = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    totalGrid += Convert.ToDouble(row.Cells["Total"].Value);
                }
            }

            // Suma el total calculado con el total del DataGridView
            totalGrid += Convert.ToDouble(total);

            // Actualiza el TextBox del total con el nuevo total combinado
            txttotal.Text = totalGrid.ToString();
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
            Sumar2();
        }

        private void txtnumero_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            Sumar2();
        }

        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            if (EditMode)
            {
                /*DatosgetInventario pEstudiantes = new DatosgetInventario();
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
                this.Close();*/
            }


            else
            {
                if (/*!string.IsNullOrEmpty(txtnumero.Text) && !string.IsNullOrEmpty(txtnombre.Text) && !string.IsNullOrEmpty(txtapellido.Text) &&*/ !string.IsNullOrEmpty(txtancho.Text))
                {
                    DatosgetVentas pEstudiantes = new DatosgetVentas();
                    pEstudiantes.ClienteID = Convert.ToInt64(code.Text);
                    pEstudiantes.NombreCliente = txtnombre.Text;
                    pEstudiantes.Empleado = Acceso.Nombre;
                    pEstudiantes.Ancho = Convert.ToDecimal(txtancho.Text);
                    pEstudiantes.Largo = Convert.ToDecimal(txtlargo.Text);
                    pEstudiantes.Precio_material = Convert.ToDecimal(txtprecio.Text);
                    pEstudiantes.FechaPedido = DateTime.Now;
                    pEstudiantes.FechaEntrega = DateTime.Now;
                    pEstudiantes.Total = Convert.ToDouble(txttotal.Text);
                    pEstudiantes.Pagado = false;

                    int Resultado = DatosbaseVentas.Agregar(pEstudiantes);
                    Conn.Open();

                    string query = "SELECT TOP 1 PedidoID FROM Pedidos ORDER BY PedidoID DESC";
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
                            ULTFactura = ultimoIdFactura;
                        }
                    }

                    Conn.Close();

                    // Define las consultas SQL para la tabla DetallePedido
                    SqlCommand agregar = new SqlCommand("INSERT INTO DetallePedido (PedidoID, ProductoID, Producto, DescripciónProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@PedidoID, @ProductoID, @Producto, @DescripciónProducto, @Cantidad, @PrecioUnitario, @Subtotal)", Conn);
                    string verificarQuery = "SELECT Cantidad FROM Inventario WHERE Nombre = @Producto";
                    string actualizarQuery = "UPDATE Inventario SET Cantidad = Cantidad + @Cantidad WHERE Nombre = @Producto";

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

                            // Verificar si el stock es menor que la cantidad
                            using (SqlCommand verificarCmd = new SqlCommand(verificarQuery, Conn))
                            {
                                verificarCmd.Parameters.AddWithValue("@Producto", producto);
                                int stock = Convert.ToInt32(verificarCmd.ExecuteScalar());

                                if (stock < cantidad)
                                {
                                    MessageBox.Show("No hay suficiente stock para el producto " + producto);
                                    return; // Salta a la siguiente iteración del bucle sin ejecutar el código restante
                                }
                            }

                            // Agregar los parámetros al comando
                            agregar.Parameters.Clear();
                            agregar.Parameters.AddWithValue("@PedidoID", idfactura);
                            agregar.Parameters.AddWithValue("@ProductoID", id_producto);
                            agregar.Parameters.AddWithValue("@Producto", producto);
                            agregar.Parameters.AddWithValue("@DescripciónProducto", descripcion);
                            agregar.Parameters.AddWithValue("@Cantidad", cantidad);
                            agregar.Parameters.AddWithValue("@PrecioUnitario", precio);
                            agregar.Parameters.AddWithValue("@Subtotal", total);

                            // Ejecutar el comando para agregar el detalle del pedido
                            agregar.ExecuteNonQuery();

                            // Actualizar los datos de la tabla inventario
                            using (SqlCommand actualizarCmd = new SqlCommand(actualizarQuery, Conn))
                            {
                                actualizarCmd.Parameters.AddWithValue("@Producto", producto);
                                actualizarCmd.Parameters.AddWithValue("@Cantidad", cantidad);
                                actualizarCmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Pedido guardado con éxito");
                        dataGridView1.Rows.Clear();
                        // Limpiar();
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
            }
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            ClienteSelect frm = new ClienteSelect();
            AddOwnedForm(frm);
            frm.ShowDialog();
            code.Text = ccodigo;
            txtnombre.Text = cnombre;
        }
    }
}
