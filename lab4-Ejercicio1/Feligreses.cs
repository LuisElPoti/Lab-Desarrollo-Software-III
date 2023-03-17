using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_Ejercicio1
{
    internal class Feligreses
    {
        public int TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public DateTime FechaUltimaConfecion { get; set; }

        public string EstadoCivil { get; set; }
        public string Sexo { get; set; }

        public int Diezmo { get; set; }

        public string PerteneceAcomunidad { get; set; }

        public DateTime UltimaVisitaIglesia { get; set; }
        public int CantidadPecadosVeniales { get; set; }
        public int CantidadPecadosMortales { get; set; }
        public int Penitencias { get; set; }

    }
}
