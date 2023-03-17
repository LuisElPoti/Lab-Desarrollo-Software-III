using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace lab5_Ejercicio
{
    public partial class Service1 : ServiceBase
    {
        SqlConnection cn;
        SqlCommand cm;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            
        }

        protected override void OnStop()
        {
        }

        private void fswMonitor_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            cn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\adame\\source\\repos\\Lab-Desarrollo-Software-III\\lab5-Ejercicio\\Ejercicio5B.mdf;Integrated Security=True");
            cn.Open();
            cm = new SqlCommand("spInsertEvento", cn);
            var transaccion = cn.BeginTransaction();
            try
            {
                cm.Transaction = transaccion;
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@NombreArchivo", e.Name);
                cm.Parameters.AddWithValue("@Operacion", "Cambio");
                cm.Parameters.AddWithValue("@FechaEvento", DateTime.Now);
                cm.ExecuteNonQuery();
                transaccion.Commit();
                log.Info("Archivo modificado > " + e.Name + " > " + DateTime.Now);
                cn.Close();
            }
            catch (Exception a)
            {
                transaccion.Rollback();
                log.Error(a.Message);
                cn.Close();
            }
        }

        private void fswMonitor_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            cn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\adame\\source\\repos\\Lab-Desarrollo-Software-III\\lab5-Ejercicio\\Ejercicio5B.mdf;Integrated Security=True");
            cn.Open();
            cm = new SqlCommand("spInsertEvento", cn);
            var transaccion = cn.BeginTransaction();
            try
            {
                cm.Transaction = transaccion;
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@NombreArchivo", e.Name);
                cm.Parameters.AddWithValue("@Operacion", "Creado");
                cm.Parameters.AddWithValue("@FechaEvento", DateTime.Now);
                cm.ExecuteNonQuery();
                transaccion.Commit();
                log.Info("Archivo Creado > " + e.Name + " > " + DateTime.Now);
                cn.Close();
            }
            catch (Exception a)
            {
                transaccion.Rollback();
                log.Error(a.Message);
                cn.Close();
            }
        }

        private void fswMonitor_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            cn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\adame\\source\\repos\\Lab-Desarrollo-Software-III\\lab5-Ejercicio\\Ejercicio5B.mdf;Integrated Security=True");
            cn.Open();
            cm = new SqlCommand("spInsertEvento", cn);
            var transaccion = cn.BeginTransaction();
            try
            {
                cm.Transaction = transaccion;
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@NombreArchivo", e.Name);
                cm.Parameters.AddWithValue("@Operacion", "Eliminado");
                cm.Parameters.AddWithValue("@FechaEvento", DateTime.Now);
                cm.ExecuteNonQuery();
                transaccion.Commit();
                log.Info("Archivo eliminado > " + e.Name + " > " + DateTime.Now);
                cn.Close();
            }
            catch (Exception a)
            {
                transaccion.Rollback();
                log.Error(a.Message);
                cn.Close();
            }
        }

        private void fswMonitor_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            cn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\adame\\source\\repos\\Lab-Desarrollo-Software-III\\lab5-Ejercicio\\Ejercicio5B.mdf;Integrated Security=True");
            cn.Open();
            cm = new SqlCommand("spInsertEvento", cn);
            var transaccion = cn.BeginTransaction();
            try
            {
                cm.Transaction = transaccion;
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@NombreArchivo", e.Name);
                cm.Parameters.AddWithValue("@Operacion", "Renombrado");
                cm.Parameters.AddWithValue("@FechaEvento", DateTime.Now);
                cm.ExecuteNonQuery();
                transaccion.Commit();
                log.Info("Archivo renombrado > " + e.Name + " > " + DateTime.Now);
                cn.Close();
            }
            catch (Exception a)
            {
                transaccion.Rollback();
                log.Error(a.Message);
                cn.Close();
            }
        }
    }
}
