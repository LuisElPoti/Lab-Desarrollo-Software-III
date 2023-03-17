using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_Ejer2_app_infieles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char valor = 'a';
            

            while (char.ToLower(valor) != 'n')
            {
                Infiel infiel = new Infiel();

                Console.WriteLine("-------PROGRAMA INFIELES-----------");

                Console.WriteLine("\nIntroduzca el nombre del afectado: ");
                infiel.NombreAfectado = Console.ReadLine();

                Console.WriteLine("\nIntroduzca el apellido del afectado: ");
                infiel.ApellidoAfectado = Console.ReadLine();

                Console.WriteLine("\nIntroduzca el nombre del infiel: ");
                infiel.NombreInfiel = Console.ReadLine();

                Console.WriteLine("\nIntroduzca el apellido del infiel: ");
                infiel.ApellidoInfiel = Console.ReadLine();

                Console.WriteLine("\nIntroduzca el sexo del afectado: ");
                infiel.SexoAfectado = Console.ReadLine();

                Console.WriteLine("\nIntroduzca el sexo del infiel: ");
                infiel.SexoInfiel = Console.ReadLine();

                Console.WriteLine("\nIntroduzca la fecha del evento: ");
                infiel.FechaEvento = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("\nIntroduzca el estado: ");
                infiel.Estado = Console.ReadLine();

                Console.WriteLine("\nIntroduzca si es su primera vez (si-no): ");
                string a = Console.ReadLine();
                if(a != "si")
                {
                    infiel.PrimeraVez = false;
                }
                else infiel.PrimeraVez= true;
                

           

                SqlConnection cn = new SqlConnection("Data Source=(localdb)\\ProjectModels;Initial Catalog=Infidelidad;Integrated Security=True");
                cn.Open();
                Console.WriteLine(cn.State);
                SqlCommand cm = new SqlCommand($"insert into TblInfieles (NombreAfectado, ApellidoAfectado, NombreInfiel, ApellidoInfiel, SexoAfectado, SexoInfiel, FechaEvento, Estado, PrimeraVez) values(@NombreAfectado, @ApellidoAfectado, @NombreInfiel, @ApellidoInfiel, @SexoAfectado, @SexoInfiel, @FechaEvento, @Estado, @PrimeraVez)", cn);

                cm.Parameters.AddWithValue("@NombreAfectado", infiel.NombreAfectado);
                cm.Parameters.AddWithValue("@ApellidoAfectado", infiel.ApellidoAfectado);
                cm.Parameters.AddWithValue("@NombreInfiel", infiel.NombreInfiel);
                cm.Parameters.AddWithValue("@ApellidoInfiel", infiel.ApellidoInfiel);
                cm.Parameters.AddWithValue("@SexoAfectado", infiel.SexoAfectado);
                cm.Parameters.AddWithValue("@SexoInfiel", infiel.SexoInfiel);
                cm.Parameters.AddWithValue("@FechaEvento", infiel.FechaEvento);
                cm.Parameters.AddWithValue("@Estado", infiel.Estado);
                cm.Parameters.AddWithValue("@PrimeraVez", infiel.PrimeraVez);

                cm.ExecuteNonQuery();

                Console.WriteLine("Desea agregar otro dato? s/n");
                valor = char.Parse( Console.ReadLine());
                
            }


        }
    }
}
