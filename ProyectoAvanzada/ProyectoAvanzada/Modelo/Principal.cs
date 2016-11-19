using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    class Principal
    {
        static void Main(string[] args)
        {

            PruebaDiagnostico diagnostico = new PruebaDiagnostico();
            diagnostico.TrabajarActividad();
            diagnostico.RevisarActividad();
        }
    }
}
