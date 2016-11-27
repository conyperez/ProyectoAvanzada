using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    public class Modelo
    {
        private String ResultadoH1 { get; set; }
        private String ResultadoH2 { get; set; }//Almaceno si es logrado o no logrado en cada habiliad.
        private Double Porcent_Act_Diag;
        private Evaluaciones evaluacion;
        private Diagnostico diagnostico;
        private int CantActividad;
        private int H1C = 0, H1I = 0, H2C = 0, H2I = 0;

        public Modelo()
        {

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



            Console.WriteLine("Habilidad 1:" + ResultadoH1);
            Console.WriteLine("Habilidad 2:" + ResultadoH2);
            Porcent_Act_Diag = diagnostico.getPorcentH();
            Console.WriteLine(Porcent_Act_Diag + "%");

            Console.ReadKey();
        }

        public String TrabajoDiagnostico(string rut) //Retorna cual modulo debe realizar
        { 
            string resultado = null; //Aqui devuelve si realiza el modulo 1.1 o 1.2
            List<String> respuestas;
            for (int i = 0; i < CantActividad; i++)//Bucle para realizar todas las actividades.
            {
                respuestas = evaluacion.TrabajaActividad(i); //contiene las respuestas que aplico el alumno
                diagnostico.RevisarActividad(respuestas, i); //revisa esas respuestas con la pauta.
                                                                //Almacenar la cantidad de buenas y malas en cada pregunta.
                H1C = H1C + diagnostico.getH1C();
                H1I = H1I + diagnostico.getH1I();
                H2C = H2C + diagnostico.getH2C();
                H2I = H2I + diagnostico.getH2I();

            }
            //despues de realizar la revision de todas las actividades, determino el nivel de logro.
            ResultadoH1 = diagnostico.determinarNivelLogroHabilidad(H1C, H1I);
            ResultadoH2 = diagnostico.determinarNivelLogroHabilidad(H2C, H2I);
            //ahora determinar cual habilidad enfocar
            if (ResultadoH1.Equals(ResultadoH2))
            { //Si ambos son iguales
                resultado = "MóduloEF";
            }
            if (ResultadoH1.Equals("No Logrado"))
            {
                resultado = "MóduloE";
            }
            if (ResultadoH2.Equals("No Logrado"))
            {
                resultado = "MóduloF";
            }

            return resultado;
        }

        public void TrabajarModulo(string rut)
        {
            Evaluaciones modulo = new Evaluaciones();
            Modulo evaluar = new Modulo();
            LeerArchivo archivos_actividad = new LeerArchivo();
            LeerArchivo archivos_pauta = new LeerArchivo();

            ConexionBD conexion = new ConexionBD();
            string[] moduloRealizado = conexion.SeleccionarModulosRealizdos(rut);
            string mRealizado = moduloRealizado[0];  
            string nivelLogroM = moduloRealizado[1];  

            if (mRealizado == null) // Si no ha hecho ningun modulo
            {
                // SE DEBE OBTENER DE LA BD DE LA TABLA DIAGNOSTICO CUAL MODULO DEBE REALIZAR E, EF o F
                string hacerModulo = "MóduloE";   // Suponiendo que esto es lo que se obtiene de la BD

                archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo1", "Módulo 1.1", hacerModulo);
                archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo1", "Módulo 1.1", hacerModulo);
            }
            else // Ya ha realizado algun modulo
            {
                string seRealiza = evaluar.determinarProgreso(nivelLogroM);       // Si pasa al siguiente modulo, lo repite o realiza remedial
                String[] separarUltimo = mRealizado.Split(' ');                  // Separa el nombre del ultimo modulo realizado: Ej: "Modulo 1.2" -> [Modulo, 1.2]
                double numModulo = Convert.ToDouble(separarUltimo[1]);           // Pasa a double el "1.2"
                String[] num = separarUltimo[1].Split('.');                      // Separa el numero: "1.2" -> [1, 2]
                int numM = Convert.ToInt32(num[0]);                              // Pasa a int "1"
                if (seRealiza == "Siguiente")
                {
                    numM++;
                    archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo" + numM, "Módulo " + numM + ".1");
                    archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo" + numM, "Módulo " + numM + ".1");
                }
                else if (seRealiza == "Repite") // Repite modulo
                {
                    numModulo = numModulo + 0.1;
                    archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo" + numM, "Módulo " + numModulo);
                    archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo" + numM, "Módulo " + numModulo);
                }
                else // Se realiza modulo remedial
                {
                    if (separarUltimo[0].Equals("Remedial")) // El ultimo que realizo fue remedial
                    {
                        numModulo = numModulo + 0.1;
                        archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo" + numM, "Remedial " + numModulo);
                        archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo" + numM, "Remedial " + numModulo);
                    }
                    else // El ultimo fue modulo
                    {
                        archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo" + numM, "Remedial " + numM + ".1");
                        archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo" + numM, "Remedial " + numM + ".1");
                    }
                }
            }
            archivos_actividad.setDireccion(@"\Actividades");
            archivos_pauta.setDireccion(@"\Pautas");

            modulo.setArchivo(archivos_actividad);
            evaluar.setArchivo(archivos_pauta);
            evaluar.setNombreCarpeta("Quinto Básico");

            CantActividad = archivos_actividad.getCantArchivos();
            List<string> logros = new List<string>();
            for (int i = 0; i < CantActividad; i++)    // Se realizan las actividades
            {
                List<string> respuestas = modulo.TrabajaActividad(i);
                logros.Add(evaluar.RevisarActividad(respuestas, i));
            }
            string nivelLogro = evaluar.determinarNivelLogroModulo(logros);
            Console.WriteLine(nivelLogro);
            Console.ReadKey();
        }

    }
}
