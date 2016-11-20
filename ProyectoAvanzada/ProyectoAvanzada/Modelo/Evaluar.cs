using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
     class Evaluar
    {
        private String Curso, NombreCarpeta, NombreModulo, TipoModulo;
        private List<string> respuestas = new List<string>();
        private List<string> pauta = new List<string>();
        private LeerArchivo actividad;
        private int correcta;
        private int incorrecta;

        public Evaluar(String Curso, String NombreCarpeta, List<string> respuestas)
        { //Para la prueba de diagnostico
            this.Curso = Curso;
            this.NombreCarpeta = NombreCarpeta;
            this.respuestas = respuestas;
            actividad = new LeerArchivo(Curso, NombreCarpeta);
        }

        public Evaluar(String Curso, String NombreCarpeta, String NombreModulo, String TipoModulo, List<string> respuestas)
        { //Para el modulo1.
            this.Curso = Curso;
            this.NombreCarpeta = NombreCarpeta;
            this.NombreModulo = NombreModulo;
            this.TipoModulo = TipoModulo;
            this.respuestas = respuestas;
            actividad = new LeerArchivo(Curso, NombreCarpeta, NombreModulo, TipoModulo);
        }

        public Evaluar(String Curso, String NombreCarpeta, String NombreModulo, List<string> respuestas)
        { //Para los demas modulos
            this.Curso = Curso;
            this.NombreCarpeta = NombreCarpeta;
            this.NombreModulo = NombreModulo;
            this.respuestas = respuestas;
            actividad = new LeerArchivo(Curso, NombreCarpeta, NombreModulo);
        }

        public void RevisarActividad()
        {
            actividad.setDireccion(@"\Pautas");
            int cantidad = actividad.CantidadArchivos();

            //Numero de pauta
            pauta = actividad.LeerArchivos(0);  // DEBERIA HABER UNA VARIABLE CON EL NUMERO DE LA ACTIVIDAD!!!!!!!!!!!!!!!!!!
            try {
                string resultado = null;
                // Se toma la primera linea donde se encuentran las habilidades de la act
                string habilidades = pauta.ElementAt(0);
                Console.WriteLine(habilidades);
                // Se separan las habilidades por ','
                String[] habilidad = habilidades.Split(',');

                correcta = 0;
                incorrecta = 0;
                List<String> revision = new List<string>();
                for (int i = 0; i < respuestas.Count; i++)
                {
                    if (NombreCarpeta == "Evaluación Diagnóstico")
                    {  // Aca se cuenta correctas e incorrectas por habilidad
                        if (pauta.ElementAt(i + 1).Equals(respuestas.ElementAt(i)))
                        {
                            revision.Add("C");
                        }
                        else
                        {
                            revision.Add("I");
                        }
                    }
                    else // Es Módulo
                    {
                        // Para los otros módulos, se cuentan correctas e incorrectas por la actividad 
                        if (pauta.ElementAt(i + 1).Equals(respuestas.ElementAt(i)))
                        {
                            correcta++;
                        }
                        else
                        {
                            incorrecta++;
                        }
                    }
                }
                if (NombreCarpeta == "Evaluación Diagnóstico") { // Se ve si es diagnostico o modulo para determinar el resultado
                    calcularDiagnostico(revision, habilidad);
                } else {
                    resultado = determinarNivelLogroActividad(correcta, incorrecta);
                    Console.WriteLine("Resultado: " + resultado);
                }
                Console.ReadKey();
            } catch (ArgumentOutOfRangeException e) {
                Console.WriteLine("Mensaje 1: " + e.Message);
            } catch (NullReferenceException e1) {
                Console.WriteLine("Mensaje 2:" + e1.Message);
            } catch (InvalidOperationException e2) {
                Console.WriteLine("Mensaje 3:" + e2.Message);
            }
        }

        public void calcularDiagnostico(List<string> revision, String[] habilidad)
        {
            int H1C = 0, H1I = 0, H2C = 0, H2I = 0;    // C: correctas ; I:incorrectas
            for(int i=0; i<revision.Count; i++)
            {
                if(habilidad[i] == "H1")  // H1 = Extraer información explícita
                {
                    if(revision.ElementAt(i) == "C") {
                        H1C++;
                    } else {
                        if (revision.ElementAt(i) == "I") { H1I++; }
                    }
                } else { // Es H2 = Análisis de la forma del texto
                    if (revision.ElementAt(i) == "C") {
                        H2C++;
                    } else {
                        if (revision.ElementAt(i) == "I") { H2I++; }
                    }
                }
            }
            /* ESAS VARIABLES SE TIENE QUE GUARDAR EN ALGUN LADO PARA IRLAS SUMANDO 
               PORQUE CON LA SUMA SE SACA EL PORCENTAJE Y SE VE CUAL HABILIDAD SE LE DA MAS ENFASIS
               Y CUANDO SE TENGA EL TOTAL SE PUEDE UTILIZAR LO SIGUIENTE: */
            string resultadoH1 = determinarNivelLogroActividad(H1C, H1I);
            string resultadoH2 = determinarNivelLogroActividad(H2C, H2I);
            Console.WriteLine(resultadoH1 + " | " + resultadoH2);
        }

        public string determinarNivelLogroActividad(int buenas, int malas) // Determina Logrado o No Logrado
        {
            if (buenas == 0 && malas == 0) { return null; }   
            double calcular = (100 * buenas) / (buenas + malas);
            if (calcular > 60)
            {
                return "Logrado";
            }
            else
            {
                return "No Logrado";
            }
        }

        public void determinarNivelLogroModulo(List<string> resultadoModulo)  // Determina nivel de logro del modulo realizado
        {
            // Hay que contar duplicados de logrado y no logrado, con ese resultado se saca el porcentaje
            // y se ve el nivel de logro en el modulo
            // La lista resultadoModulo no se de donde va a venir...
        }

    }
}
