using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace lab5_Ejercicio1
{
    public partial class Service1 : ServiceBase
    {

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

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void fswMonitor_Created(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void fswMonitor_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void fswMonitor_Renamed(object sender, System.IO.RenamedEventArgs e)
        {

        }
    }
}
