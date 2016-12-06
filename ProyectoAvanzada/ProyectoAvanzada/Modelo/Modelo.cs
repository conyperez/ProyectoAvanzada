using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    public class Modelo
    {
        private string ResultadoH1;         // Se almacena si es logrado o no logrado en cada habiliad.
        private string ResultadoH2;
        private double Porcent_Act_Diag;
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
            Porcent_Act_Diag = diagnostico.getPorcentH();
        }

        //Retorna cual modulo debe realizar
        public String EvaluacionDiagnostico() 
        {
            string resultado = null; 
            List<String> respuestas;
            for (int i = 0; i < CantActividad; i++)             // Bucle para realizar todas las actividades.
            {
                respuestas = evaluacion.TrabajaActividad(i);    // Contiene las respuestas que aplico el alumno
                diagnostico.RevisarActividad(respuestas, i);    // Revisa esas respuestas con la pauta.
                                                                
                H1C = H1C + diagnostico.getH1C();               //Almacenar la cantidad de buenas y malas en cada pregunta.
                H1I = H1I + diagnostico.getH1I();
                H2C = H2C + diagnostico.getH2C();
                H2I = H2I + diagnostico.getH2I();

            }
            // Se determina el nivel de logro para cada habilidad
            ResultadoH1 = diagnostico.determinarNivelLogroHabilidad(H1C, H1I);
            ResultadoH2 = diagnostico.determinarNivelLogroHabilidad(H2C, H2I);
            
            // Se determina a que habilidad se le da mas enfasis
            if (ResultadoH1 == ResultadoH2)
            {
                resultado = "MóduloEF";
            }
            if (ResultadoH1 == "No Logrado")
            {
                resultado = "MóduloE";
            }
            if (ResultadoH2 == "No Logrado")
            {
                resultado = "MóduloF";
            }

            return resultado;
        }

        public void TrabajarModulo(string rut, String codigo, String fecha, String rut_p)
        {
            Evaluaciones modulo = new Evaluaciones();
            Modulo evaluar = new Modulo();
            LeerArchivo archivos_actividad = new LeerArchivo();
            LeerArchivo archivos_pauta = new LeerArchivo();
            String NombreModulo = null;

            ConexionBD conexion = new ConexionBD();
            string[] moduloRealizado = conexion.SeleccionarTodosLosModulosRealizados(rut);
            string mRealizado = moduloRealizado[0];
            string nivelLogroM = moduloRealizado[1];
            conexion.cerrarBD();

            if (mRealizado == null) // Si no ha hecho ningun modulo
            {
                conexion = new ConexionBD();
                string hacerModulo = conexion.resultadoDiagnostico(rut);
                conexion.cerrarBD();

                archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo1", "Módulo 1.1", hacerModulo);
                archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo1", "Módulo 1.1", hacerModulo);
                NombreModulo = hacerModulo;

            }
            else // Ya ha realizado algun modulo
            {
                string seRealiza = evaluar.determinarProgreso(nivelLogroM);       // Si pasa al siguiente modulo, lo repite o realiza remedial
                //Si realizo el diagnostico y luego realizo uno de estos modulos..
                if (mRealizado.Equals("MóduloE") || mRealizado.Equals("MóduloEF") || mRealizado.Equals("MóduloF"))
                {
                    if (seRealiza == "Siguiente")
                    {
                        archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo 2", "Módulo 2.1");
                        archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo 2", "Módulo 2.1");
                        NombreModulo = "Módulo2";

                    }
                    else if (seRealiza == "Repite")
                    {
                        archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo 2", "Módulo 2.2");
                        archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo 2", "Módulo 2.2");
                        NombreModulo = mRealizado;
                    }
                    else if (seRealiza == "Remedial")
                    {
                        archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo 2", "Remedial 1");
                        archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo 2", "Remedial 1");
                        NombreModulo = mRealizado;
                    }
                }

                else
                {
                    String[] separarUltimo = mRealizado.Split(' ');                  // Separa el nombre del ultimo modulo realizado: Ej: "Modulo 1.2" -> [Modulo, 1.2] 
                    double numModulo = Convert.ToDouble(separarUltimo[1]);           // Pasa a double el "1.2"
                    String[] num = separarUltimo[1].Split('.');                      // Separa el numero: "1.2" -> [1, 2]
                    int numM = Convert.ToInt32(num[0]);                              // Pasa a int "1"
                    if (seRealiza == "Siguiente")
                    {
                        numM++;
                        archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo " + numM, "Módulo " + numM + ".1");
                        archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo " + numM, "Módulo " + numM + ".1");
                        NombreModulo = "Módulo " + numM;
                    }
                    else if (seRealiza == "Repite")             // Repite modulo
                    {
                        numModulo = numModulo + 0.1;
                        archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo " + numM, "Módulo " + numModulo);
                        archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo " + numM, "Módulo " + numModulo);
                        NombreModulo = "Módulo " + numM;
                    }
                    else // Se realiza modulo remedial
                    {
                        if (separarUltimo[0].Equals("Remedial")) // El ultimo que realizo fue remedial
                        {
                            numModulo = numModulo + 0.1;
                            archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo " + numM, "Remedial " + numModulo);
                            archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo " + numM, "Remedial " + numModulo);
                            NombreModulo = "Módulo " + numM;
                        }
                        else // El ultimo fue modulo
                        {
                            archivos_actividad = new LeerArchivo("Quinto Básico", "Módulo " + numM, "Remedial " + numM + ".1");
                            archivos_pauta = new LeerArchivo("Quinto Básico", "Módulo " + numM, "Remedial " + numM + ".1");
                            NombreModulo = "Módulo " + numM;
                        }
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
            conexion.cerrarBD();
            conexion = new ConexionBD();
            conexion.InsertarDatosModuloAlumnoAntes(codigo, fecha, NombreModulo, rut_p, rut);
            conexion.cerrarBD();
            for (int i = 0; i < CantActividad; i++)    // Se realizan las actividades
            {
                conexion = new ConexionBD();
                List<string> respuestas = new List<string>(modulo.TrabajaActividad(i));
                logros.Add(evaluar.RevisarActividad(respuestas, i));
                conexion.cerrarBD();
                conexion = new ConexionBD();
                conexion.InsertarActividadesModeloAlumno(codigo, i + 1, logros.ElementAt(i), evaluar.getHabilidades()); //Inserto datos por cada actividad realizada en el modulo
                conexion.cerrarBD();
            }
            for (int j = 0; j < logros.Count; j++)
            {
                Console.WriteLine(logros.ElementAt(j));
            }
            string nivelLogro = evaluar.determinarNivelLogroModulo(logros);
            conexion.cerrarBD();
            conexion = new ConexionBD();
            conexion.InsertarDatosModuloAlumnoDespues(codigo, fecha, NombreModulo, rut_p, rut, nivelLogro);
            Console.ReadKey();
        }

        public String getResultadoH1() { return ResultadoH1; }
        public String getResultadoH2() { return ResultadoH2; }

    }
}
