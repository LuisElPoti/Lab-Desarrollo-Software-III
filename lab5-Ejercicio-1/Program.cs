using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_Ejercicio_1
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

                

                Guerra guerra = new Guerra();

                // Conseguir informacion feligreses por consola

                Console.WriteLine("----- Insertar Guerra ------\n");

                Console.WriteLine("Codigo Guerra: ");
                guerra.CodigoGuerra = Console.ReadLine();

                Console.WriteLine("Pais 1: ");
                guerra.Pais1 = Console.ReadLine();

                Console.WriteLine("Pais 2: ");
                guerra.Pais2 = Console.ReadLine();

                Console.WriteLine("Fecha Inicio: ");
                guerra.FechaInicio = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Fecha Fin: ");
                guerra.FechaFin = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Pais Ganador ");
                guerra.PaisGanador = Console.ReadLine();

                Console.WriteLine("Cantidad Presos: ");
                guerra.CantidadPresos = int.Parse(Console.ReadLine());

                Console.WriteLine("Perdida Financiera: ");
                guerra.PerdidaFinanciera = int.Parse(Console.ReadLine());

                Console.WriteLine("Estado Guerra: ");
                guerra.EstadoGuerra = int.Parse(Console.ReadLine());

                Eventos evento = new Eventos();

                Console.WriteLine("----- Insertar Evento ------\n");

                Console.WriteLine("Tipo Evento: ");
                evento.TipoEvento = int.Parse(Console.ReadLine());

                Console.WriteLine("Descripcion: ");
                evento.Descripcion = Console.ReadLine();

                Console.WriteLine("Fecha Evento: ");
                evento.FechaEvento = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Cantidad Muertos: ");
                evento.CantidadMuertos = int.Parse(Console.ReadLine());

                Console.WriteLine("Cantidad Heridos: ");
                evento.Heridos = int.Parse(Console.ReadLine());

                SqlCommand cm = new SqlCommand($"spInsertEvento", cn);

                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Clear();

                cm.Parameters.AddWithValue("@CodigoGuerra", guerra.CodigoGuerra);
                cm.Parameters.AddWithValue("@TipoEvento", evento.TipoEvento);
                cm.Parameters.AddWithValue("@FechaEvento", evento.FechaEvento);
                cm.Parameters.AddWithValue("@Descripcion", evento.Descripcion);
                cm.Parameters.AddWithValue("@CantidadMuertos", evento.CantidadMuertos);
                cm.Parameters.AddWithValue("@CantidadHeridos", evento.Heridos);


                try
                {
                    transaction = cn.BeginTransaction(); // Inicio de la Transaccion
                    cm.Transaction = transaction;
                    cm.ExecuteNonQuery();
                    cm.Parameters.Clear();

                    cm.CommandText = "spUpsertGuerra";
                    cm.Parameters.AddWithValue("@CodigoGuerra", guerra.CodigoGuerra);
                    cm.Parameters.AddWithValue("@Pais1", guerra.Pais1);
                    cm.Parameters.AddWithValue("@Pais2", guerra.Pais2);
                    cm.Parameters.AddWithValue("@FechaInicio", guerra.FechaInicio);
                    cm.Parameters.AddWithValue("@FechaFin", guerra.FechaFin);
                    cm.Parameters.AddWithValue("@PaisGanador", guerra.PaisGanador);
                    cm.Parameters.AddWithValue("@CantidadMuertos", evento.CantidadMuertos);
                    cm.Parameters.AddWithValue("@CantidadPresos", guerra.CantidadPresos);
                    cm.Parameters.AddWithValue("@PerdidaFinanciera", guerra.PerdidaFinanciera);
                    cm.Parameters.AddWithValue("@EstadoGuerra", guerra.EstadoGuerra);
                    cm.Parameters.AddWithValue("@Heridos", evento.Heridos);

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
