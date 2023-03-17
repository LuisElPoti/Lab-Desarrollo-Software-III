using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_Ejercicio_1
{
    internal class Guerra
    {
        public string CodigoGuerra { get; set; }
        public string Pais1 { get; set; }
        public string Pais2 { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string PaisGanador { get; set; }
        public int CantidadMuertos { get; set; }
        public int CantidadPresos { get; set; }
        public int PerdidaFinanciera { get; set; }
        public int EstadoGuerra { get; set; }
        public int Heridos { get; set; }

    }
}
