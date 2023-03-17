using System;

/** 
 * Laboratorio 1 : Ejercicio clase Producto
 * **/

namespace lab1_desarrollo_clase_y_objeto
{

    internal class Program
    {
        static void Main(string[] args)
        {
            clsProductos producto;
            producto = new clsProductos();

            producto.Nombre = "Arroz";
            producto.Precio = 40;
            producto.Categoria = "Cereales";
            producto.Stock = 100;
            Console.WriteLine("---------PROGRAMA 1, PRODUCTOS-----------\n");
            Console.WriteLine("Nombre: {0}\nPrecio: {1}\nCategoria: {2}\nStock: {3}", producto.Nombre, producto.Precio, producto.Categoria, producto.Stock);
            Console.ReadKey();
        }
    }
}
