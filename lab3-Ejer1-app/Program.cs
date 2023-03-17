using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_Ejer1_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char a = 's';

            while (a != 'n')
            {
                SqlTransaction transaction = null;
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cn"].ConnectionString);
                cn.Open();

                Console.WriteLine(cn.State);

                SqlCommand cm = new SqlCommand($"dpInsertFallecidos", cn);

                cm.CommandType = System.Data.CommandType.StoredProcedure;

                clsFallecidos fallecidos = new clsFallecidos();

                Console.WriteLine("Tipo Documento: ");
                fallecidos.TipoDocumento = int.Parse(Console.ReadLine());
                Console.WriteLine("Documento: ");
                fallecidos.Documento = Console.ReadLine();
                Console.WriteLine("Nombre: ");
                fallecidos.Nombres = Console.ReadLine();
                Console.WriteLine("Apellido: ");
                fallecidos.Apellidos = Console.ReadLine();
                Console.WriteLine("FechaNacimiento: ");
                fallecidos.FechaNacimiento = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Sexo: ");
                fallecidos.Sexo = Console.ReadLine();
                Console.WriteLine("Estado: ");
                fallecidos.Estado = Console.ReadLine();
                Console.WriteLine("Pais: ");
                fallecidos.Pais = Console.ReadLine();
                Console.WriteLine("Ciudad: ");
                fallecidos.Ciudad = Console.ReadLine();

                cm.Parameters.Clear();

                cm.Parameters.AddWithValue("@Nombres", fallecidos.Nombres);
                cm.Parameters.AddWithValue("@Apellidos", fallecidos.Apellidos);
                cm.Parameters.AddWithValue("@FechaNacimiento", fallecidos.FechaNacimiento);
                cm.Parameters.AddWithValue("@Sexo", fallecidos.Sexo);
                cm.Parameters.AddWithValue("@TipoDocumento", fallecidos.TipoDocumento);
                cm.Parameters.AddWithValue("@Documento", fallecidos.Documento);
                cm.Parameters.AddWithValue("@Pais", fallecidos.Pais);
                cm.Parameters.AddWithValue("@Ciudad", fallecidos.Ciudad);
                cm.Parameters.AddWithValue("@Estado", fallecidos.Estado);

                
                try
                {
                    transaction = cn.BeginTransaction();
                    cm.Transaction = transaction;
                    cm.ExecuteNonQuery();

                    cm.Parameters.Clear();

                    cm.CommandText = "dpUpsertResumen";

                    cm.Parameters.AddWithValue("@Pais", fallecidos.Pais);
                    cm.Parameters.AddWithValue("@Ciudad", fallecidos.Ciudad);
                    cm.Parameters.AddWithValue("@Sexo", fallecidos.Sexo);

                    cm.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                    
                }
                
                

                Console.WriteLine("Desea continuar? s/n");
                a = char.Parse(Console.ReadLine());
            }

        }
    }
}
