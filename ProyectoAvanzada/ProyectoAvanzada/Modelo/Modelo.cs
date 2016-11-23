using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    public class Modelo
    {
        private String ResultadoH1=null, ResultadoH2=null; //Almaceno si es logrado o no logrado en cada habiliad.
        private Double Porcent_Act_Diag;
        public Modelo() {

            Evaluaciones evaluacion = new Evaluaciones();
            Diagnostico diagnostico = new Diagnostico();
            

            LeerArchivo archivos_actividad = new LeerArchivo("Quinto Básico", "Evaluación Diagnóstico");
            LeerArchivo archivos_pauta = new LeerArchivo("Quinto Básico", "Evaluación Diagnóstico");

            archivos_actividad.setDireccion(@"\Actividades");
            archivos_pauta.setDireccion(@"\Pautas");

            evaluacion.setArchivo(archivos_actividad);
            diagnostico.setArchivo(archivos_pauta);
            diagnostico.setNombreCarpeta("Evaluación Diagnóstico");

            List<string> respuestas = evaluacion.TrabajaActividad(0);

            diagnostico.RevisarActividad(respuestas,0);


            ResultadoH1 = diagnostico.getResultadoH1();
            Console.WriteLine("Habilidad 1:"+ResultadoH1);
            ResultadoH2 = diagnostico.getResultadoH2();
            Console.WriteLine("Habilidad 2:"+ResultadoH2);
            Porcent_Act_Diag = diagnostico.getPorcentH();
            Console.WriteLine(Porcent_Act_Diag+"%");

            Console.ReadKey();
        }

    }
}
