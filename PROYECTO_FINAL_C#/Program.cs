using System;

namespace DonMaxiCarrito
{
    class Producto
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
    }

    class CarritoItem
    {
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Bienvenido a Tienda Don Maxi ===");

            // Inicializar inventario
            List<Producto> inventario = new List<Producto>
            {
                new Producto { Nombre = "Arroz", Precio = 2.50, Stock = 100 },
                new Producto { Nombre = "Leche", Precio = 1.20, Stock = 50 },
                new Producto { Nombre = "Pan", Precio = 0.80, Stock = 200 },
                new Producto { Nombre = "Huevos", Precio = 3.00, Stock = 30 },
                new Producto { Nombre = "Azúcar", Precio = 1.50, Stock = 80 }
            };

            List<CarritoItem> carrito = new List<CarritoItem>();
            double total = 0;

            // ENTRADA: Nombre del cliente
            Console.Write("Ingrese su nombre: ");
            string nombreCliente = (Console.ReadLine());
            while (string.IsNullOrWhiteSpace(nombreCliente))
            {
                Console.Write("Nombre no válido. Ingrese nuevamente: ");
                nombreCliente = Console.ReadLine();
            }

            // PROCESO: Selección de productos
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\nProductos disponibles:");
                for (int i = 0; i < inventario.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {inventario[i].Nombre} - ${inventario[i].Precio} (Stock: {inventario[i].Stock})");
                }

                Console.Write("\nSeleccione número de producto o nombre: ");
                string seleccion = (Console.ReadLine());

                Producto productoSeleccionado = null;
                // Buscar por índice
                if (int.TryParse(seleccion, out int indice) && indice >= 1 && indice <= inventario.Count)
                {
                    productoSeleccionado = inventario[indice - 1];
                }
                else // Buscar por nombre
                {
                    productoSeleccionado = inventario.Find(p => p.Nombre.Equals(seleccion, StringComparison.OrdinalIgnoreCase));
                }

                if (productoSeleccionado != null)
                {
                    Console.Write($"Cantidad de {productoSeleccionado.Nombre}: ");
                    if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0 && cantidad <= productoSeleccionado.Stock)
                    {
                        carrito.Add(new CarritoItem { NombreProducto = productoSeleccionado.Nombre, Cantidad = cantidad });
                        total += productoSeleccionado.Precio * cantidad;
                        productoSeleccionado.Stock -= cantidad; // Actualizar stock
                        Console.WriteLine($"{cantidad} {productoSeleccionado.Nombre} agregado al carrito.");
                    }
                    else
                    {
                        Console.WriteLine("Cantidad no válida o stock insuficiente.");
                    }
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }

                Console.Write("¿Agregar otro producto? (s/n): ");
                string respuesta = (Console.ReadLine());
                if (respuesta.ToLower() != "s")
                    continuar = false;
            }

            // Aplicar descuento
            double descuento = 0;
            if (total > 500)
            {
                descuento = total * 0.10;
                total -= descuento;
                Console.WriteLine($"\nDescuento del 10% aplicado: -${descuento:F2}");
            }

            // Calcular IVA
            double iva = total * 0.16;
            double totalConIVA = total + iva;

            // Mostrar resumen
            Console.WriteLine("\n=== RESUMEN DE COMPRA ===");
            Console.WriteLine($"Subtotal: ${total:F2}");
            Console.WriteLine($"IVA (16%): ${iva:F2}");
            Console.WriteLine($"Total a pagar: ${totalConIVA:F2}");

            // PROCESO DE PAGO
            Console.Write("\nMétodo de pago (efectivo/tarjeta): ");
            string metodoPago = (Console.ReadLine());

            if (metodoPago == "efectivo")
            {
                double montoPagado = 0;
                do
                {
                    Console.Write("Ingrese monto con el que paga: $");
                } while (!double.TryParse(Console.ReadLine(), out montoPagado) || montoPagado < totalConIVA);

                double cambio = montoPagado - totalConIVA;
                Console.WriteLine($"Cambio: ${cambio:F2}");
            }
            else
            {
                Console.WriteLine("Pago con tarjeta procesado exitosamente.");
            }

            // SALIDA: Factura
            Console.WriteLine("\n===== FACTURA DON MAXI =====");
            Console.WriteLine($"Cliente: {nombreCliente}");
            Console.WriteLine("Productos:");
            foreach (var item in carrito)
            {
                Console.WriteLine($"- {item.NombreProducto} x{item.Cantidad}");
            }
            Console.WriteLine($"Total pagado: ${totalConIVA:F2}");
            Console.WriteLine("¡Gracias por su compra!");
            Console.WriteLine("============================");
            Console.WriteLine("Prueba 1 de suma");
            Console.WriteLine("Prueba 5 de RESTA");
        }
    }
}