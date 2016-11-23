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
        private Evaluaciones evaluacion;
        private Diagnostico diagnostico;
        private int CantActividad;
        private int H1C = 0, H1I = 0, H2C = 0, H2I = 0;
        public Modelo() {

            evaluacion = new Evaluaciones();
            diagnostico = new Diagnostico();
            

            LeerArchivo archivos_actividad = new LeerArchivo("Quinto Básico", "Evaluación Diagnóstico");
            LeerArchivo archivos_pauta = new LeerArchivo("Quinto Básico", "Evaluación Diagnóstico");

            archivos_actividad.setDireccion(@"\Actividades");
            archivos_pauta.setDireccion(@"\Pautas");

            evaluacion.setArchivo(archivos_actividad);
            diagnostico.setArchivo(archivos_pauta);
            diagnostico.setNombreCarpeta("Evaluación Diagnóstico");

            CantActividad = archivos_actividad.getCantArchivos();

            Console.WriteLine(TrabajoDiagnostico());


            ResultadoH1 = diagnostico.getResultadoH1();
            Console.WriteLine("Habilidad 1:"+ResultadoH1);
            ResultadoH2 = diagnostico.getResultadoH2();
            Console.WriteLine("Habilidad 2:"+ResultadoH2);
            Porcent_Act_Diag = diagnostico.getPorcentH();
            Console.WriteLine(Porcent_Act_Diag+"%");

            Console.ReadKey();
        }

        public String TrabajoDiagnostico() { //Esto va a la BD
            string resultado=null; //Aqui devuelve si realiza el modulo 1.1 o 1.2
            String ResultadoH1, ResultadoH2 = null;
            List<String> respuestas;
            for (int i=0;i< CantActividad;i++)//Bucle para realizar todas las actividades.
            {
                respuestas = evaluacion.TrabajaActividad(i); //contiene las respuestas que aplico el alumno
                diagnostico.RevisarActividad(respuestas,i); //revisa esas respuestas con la pauta.
                //Almacenar la cantidad de buenas y malas en cada pregunta.
                H1C = H1C + diagnostico.getH1C();
                H1I = H1I + diagnostico.getH1I();
                H2C = H2C + diagnostico.getH2C();
                H2I = H2I + diagnostico.getH2I();

            }
            //despues de realizar la revision de todas las actividades, determino el nivel de logro.
            ResultadoH1 = diagnostico.determinarNivelLogroHabilidad(H1C,H1I);
            ResultadoH2 = diagnostico.determinarNivelLogroHabilidad(H2C, H2I);
            //ahora determinar cual habilidad enfocar
            if (ResultadoH1.Equals(ResultadoH2)) { //Si ambos son iguales
                return resultado = "MóduloEF";
            }
            if (ResultadoH1.Equals("No Logrado")) {
                return resultado = "MóduloE";
            }
            if (ResultadoH2.Equals("No Logrado")) {
                return resultado = "MóduloF";
            }

            return resultado;
            
        }

    }
}
