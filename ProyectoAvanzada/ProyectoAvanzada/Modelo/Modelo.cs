using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    public class Modelo
    {
        private String ResultadoH1, ResultadoH2;
        public Modelo() {
            /*
            Evaluaciones diagnostico = new Evaluaciones();
            Diagnostico evaluar = new Diagnostico();

            LeerArchivo archivos_actividad = new LeerArchivo("Quinto Básico", "Evaluación Diagnóstico");
            LeerArchivo archivos_pauta = new LeerArchivo("Quinto Básico", "Evaluación Diagnóstico");

            archivos_actividad.setDireccion(@"\Actividades");
            archivos_pauta.setDireccion(@"\Pautas");

            diagnostico.setArchivo(archivos_actividad);
            evaluar.setArchivo(archivos_pauta);
            evaluar.setNombreCarpeta("Quinto Básico");

            List<string> respuestas = diagnostico.TrabajaActividad(0);

            evaluar.RevisarActividad(respuestas,0);
            Console.ReadKey();*/

            Evaluaciones modulo = new Evaluaciones();
            Modulo evaluar = new Modulo();

            LeerArchivo archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo1", "Módulo 1.1", "MóduloE");
            LeerArchivo archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo1", "Módulo 1.1", "MóduloE");

            archivos_actividad.setDireccion(@"\Actividades");
            archivos_pauta.setDireccion(@"\Pautas");

            modulo.setArchivo(archivos_actividad);
            evaluar.setArchivo(archivos_pauta);
            evaluar.setNombreCarpeta("Quinto Básico");

            List<string> respuestas = modulo.TrabajaActividad(0);

            evaluar.RevisarActividad(respuestas, 0);
            Console.ReadKey();

        }

    }
}
