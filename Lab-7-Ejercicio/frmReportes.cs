using Lab_7_Ejercicio.lab7DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_7_Ejercicio
{
    public partial class frmReportes : Form
    {
        string tipo, documento;

        private void frmReportes_Load(object sender, EventArgs e)
        {
            tblEstudiantesTableAdapter adaptador = new tblEstudiantesTableAdapter();
            var registros = adaptador.spGetEstudiante(tipo, documento);
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource()
            {

                Value = registros,
                Name = "lab7DataSet"
            });
            this.reportViewer1.RefreshReport();
        }

        public frmReportes(string tipo, string documento)
        {
            InitializeComponent();
            this.tipo = tipo;
            this.documento = documento;
        }
    }
}
