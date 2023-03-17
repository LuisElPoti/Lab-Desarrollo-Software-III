using System;
using System.Configuration;
using System.Data.SqlClient;

namespace lab2_ejercicio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char valor = 'a';


            while (char.ToLower(valor) != 'n')
            {
                clsPersona persona = new clsPersona();

                Console.WriteLine("Ingrese su Nombre: ");
                persona.Nombres = Console.ReadLine();
                Console.WriteLine("Ingrese su Apellido: ");
                persona.Apellidos = Console.ReadLine();
                Console.WriteLine("Ingrese el tipo de documento: ");
                persona.TipoDocumento = Console.ReadLine();
                Console.WriteLine("Ingrese el documento: ");
                persona.Documento = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Ingrese Fecha de Nacimiento: ");
                persona.FechaNacimiento = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese el Sexo: ");
                persona.Sexo = Console.ReadLine();
                Console.WriteLine("Ingrese el estado: ");
                persona.Estado = Console.ReadLine();
                Console.WriteLine("Ingrese la Nacionalidad: ");
                persona.Nacionalidad = Console.ReadLine();
                Console.WriteLine("Ingrese pais de origen: ");
                persona.PaisOrigen = Console.ReadLine();
                Console.WriteLine("Ingrese la cantidad de novi@s");
                persona.CantidadNovias = int.Parse(Console.ReadLine());

                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cn"].ConnectionString);
                cn.Open();
                Console.WriteLine(cn.State);

                SqlCommand cm = new SqlCommand($"dpInsertPersona", cn);

                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@TipoDocumento", persona.TipoDocumento);
                cm.Parameters.AddWithValue("@Documento", persona.Documento);
                cm.Parameters.AddWithValue("@Nombres", persona.Nombres);
                cm.Parameters.AddWithValue("@Apellidos", persona.Apellidos);
                cm.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento);
                cm.Parameters.AddWithValue("@Sexo", persona.Sexo);
                cm.Parameters.AddWithValue("@Estado", persona.Estado);
                cm.Parameters.AddWithValue("@Nacionalidad", persona.Nacionalidad);
                cm.Parameters.AddWithValue("@PaisOrigen", persona.PaisOrigen);
                cm.Parameters.AddWithValue("@CantidadNovias", persona.CantidadNovias);

                cm.ExecuteNonQuery();
                Console.ReadKey();

                for (int i = 0; i < persona.CantidadNovias; i++)
                {
                    clsNovias novias = new clsNovias();

                    Console.WriteLine("\n-------Datos Pareja #{0}--------\n", i + 1);

                    Console.WriteLine("Ingrese su Nombre: ");
                    novias.Nombres = Console.ReadLine();
                    Console.WriteLine("Ingrese su Apellido: ");
                    novias.Apellidos = Console.ReadLine();
                    Console.WriteLine("Ingrese el tipo de documento: ");
                    novias.TipoDocumento = Console.ReadLine();
                    Console.WriteLine("Ingrese el documento: ");
                    novias.Documento = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Ingrese Fecha de Nacimiento: ");
                    novias.FechaNacimiento = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese el Sexo: ");
                    novias.Sexo = Console.ReadLine();
                    Console.WriteLine("Ingrese el estado: ");
                    novias.Estado = Console.ReadLine();
                    Console.WriteLine("Ingrese la Nacionalidad: ");
                    novias.Nacionalidad = Console.ReadLine();
                    Console.WriteLine("Ingrese pais de origen: ");
                    novias.PaisOrigen = Console.ReadLine();

                    novias.TipoDocumentoNovio = persona.TipoDocumento;
                    novias.DocumentoNovio = persona.Documento;

                    SqlCommand cmm = new SqlCommand($"dpInsertNovias", cn);
                    cmm.CommandType = System.Data.CommandType.StoredProcedure;
                    cmm.Parameters.AddWithValue("@TipoDocumento", novias.TipoDocumento);
                    cmm.Parameters.AddWithValue("@Documento", novias.Documento);
                    cmm.Parameters.AddWithValue("@Nombres", novias.Nombres);
                    cmm.Parameters.AddWithValue("@Apellidos", novias.Apellidos);
                    cmm.Parameters.AddWithValue("@FechaNacimiento", novias.FechaNacimiento);
                    cmm.Parameters.AddWithValue("@Sexo", novias.Sexo);
                    cmm.Parameters.AddWithValue("@Estado", novias.Estado);
                    cmm.Parameters.AddWithValue("@Nacionalidad", novias.Nacionalidad);
                    cmm.Parameters.AddWithValue("@PaisOrigen", novias.PaisOrigen);
                    cmm.Parameters.AddWithValue("@TipoDocumentoNovio", novias.TipoDocumentoNovio);
                    cmm.Parameters.AddWithValue("@DocumentoNovio", novias.DocumentoNovio);

                    cmm.ExecuteNonQuery();
                    Console.ReadKey();

                    Console.WriteLine("Desea agregar otro dato? s/n");
                    valor = char.Parse(Console.ReadLine());
                }
            }

            //conexion base de datos
            //Console.WriteLine(ConfigurationManager.ConnectionStrings["Cn"].ConnectionString);





        }
    }
}
