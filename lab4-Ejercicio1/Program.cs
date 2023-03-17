
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_Ejercicio1
{
    internal class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            char a = 's';

            while (a != 'n')
            {
                SqlTransaction transaction = null;
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cn"].ConnectionString);
                cn.Open();

                Console.WriteLine(cn.State);

                SqlCommand cm = new SqlCommand($"dpInsertEvento", cn);

                cm.CommandType = System.Data.CommandType.StoredProcedure;

                Feligreses feligreses = new Feligreses();

                // Conseguir informacion feligreses por consola
          
                Console.WriteLine("Nombres: ");
                feligreses.Nombres = Console.ReadLine();

                Console.WriteLine("Apellidos: ");
                feligreses.Apellidos = Console.ReadLine();

                Console.WriteLine("Tipo Documento: ");
                feligreses.TipoDocumento = int.Parse(Console.ReadLine());

                Console.WriteLine("Documento: ");
                feligreses.Documento = Console.ReadLine();

                Console.WriteLine("Fecha Nacimiento: ");
                feligreses.FechaNacimiento = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Fecha Ultima Confesion: ");
                feligreses.FechaUltimaConfecion = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Fecha Ultima Visita: ");
                feligreses.UltimaVisitaIglesia = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Estado Civil: ");
                feligreses.EstadoCivil = Console.ReadLine();

                Console.WriteLine("Sexo: ");
                feligreses.Sexo = Console.ReadLine();

                Console.WriteLine("Diezma: ");
                feligreses.Diezmo = int.Parse(Console.ReadLine());

                Console.WriteLine("Pertenece a Comunidad: ");

                feligreses.PerteneceAcomunidad = Console.ReadLine();
                Console.WriteLine("----- Insertar Pecado ------");

                // Insertar Pecado
                Eventos evento = new Eventos();

                Console.WriteLine("Tipo de Pecado: ");
                evento.TipoEvento = int.Parse(Console.ReadLine());

                Console.WriteLine("Nota: ");
                evento.Nota = Console.ReadLine();

                Console.WriteLine("Descripcion: ");
                evento.Descripcion = Console.ReadLine();

                cm.Parameters.Clear();
               

                cm.Parameters.AddWithValue("@TipoEvento", evento.TipoEvento);
                cm.Parameters.AddWithValue("@Nota", evento.Nota);
                cm.Parameters.AddWithValue("@Descripcion", evento.Descripcion);
                cm.Parameters.AddWithValue("@TipoDocumento", feligreses.TipoDocumento);
                cm.Parameters.AddWithValue("@Documento", feligreses.Documento);
                
                try
                {
                    transaction = cn.BeginTransaction(); // Inicio de la Transaccion
                    cm.Transaction = transaction;
                    cm.ExecuteNonQuery();

                    cm.Parameters.Clear();
                    cm.CommandText = "ppUpsertFeligreses";
                    cm.Parameters.AddWithValue("@TipoDocumento", feligreses.TipoDocumento);
                    cm.Parameters.AddWithValue("@Documento", feligreses.Documento); 
                    cm.Parameters.AddWithValue("@Nombres", feligreses.Nombres);
                    cm.Parameters.AddWithValue("@Apellidos", feligreses.Apellidos);
                    cm.Parameters.AddWithValue("@FechaNacimiento", feligreses.FechaNacimiento);
                    cm.Parameters.AddWithValue("@FechaUltimaConfecion", feligreses.FechaUltimaConfecion);
                    cm.Parameters.AddWithValue("@EstadoCivil", feligreses.EstadoCivil);
                    cm.Parameters.AddWithValue("@Sexo", feligreses.Sexo);
                    cm.Parameters.AddWithValue("@Diezmo", feligreses.Diezmo);
                    cm.Parameters.AddWithValue("@PerteneceComunidad", feligreses.PerteneceAcomunidad);
                    cm.Parameters.AddWithValue("@UltimaVisitaIglesia", feligreses.UltimaVisitaIglesia);
                    cm.Parameters.AddWithValue("@TipoEvento", evento.TipoEvento);
                    cm.Parameters.AddWithValue("@CantidadPecadosVeniales", feligreses.CantidadPecadosVeniales);
                    cm.Parameters.AddWithValue("@CantidadPecadosMortales", feligreses.CantidadPecadosMortales);
                    cm.Parameters.AddWithValue("@Penitencias", feligreses.Penitencias);

                    cm.ExecuteNonQuery();
                    transaction.Commit();
                    log.Info("Transaccion confirmada");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    log.Error("Transaccion revertida debido a errores");
                    throw;
                }
                Console.WriteLine("Desea continuar? s/n");
                a = char.Parse(Console.ReadLine());
            }
        }
    }
}
