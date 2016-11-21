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
        private int numAct;
        private int correcta, incorrecta;

        public Evaluar(String Curso, String NombreCarpeta, List<string> respuestas, int numAct)
        { //Para la prueba de diagnostico
            this.Curso = Curso;
            this.NombreCarpeta = NombreCarpeta;
            this.respuestas = respuestas;
            this.numAct = numAct;
            actividad = new LeerArchivo(Curso, NombreCarpeta);
        }

        public Evaluar(String Curso, String NombreCarpeta, String NombreModulo, String TipoModulo, List<string> respuestas, int numAct)
        { //Para el modulo 1.
            this.Curso = Curso;
            this.NombreCarpeta = NombreCarpeta;
            this.NombreModulo = NombreModulo;
            this.TipoModulo = TipoModulo;
            this.respuestas = respuestas;
            this.numAct = numAct;
            actividad = new LeerArchivo(Curso, NombreCarpeta, NombreModulo, TipoModulo);
        }

        public Evaluar(String Curso, String NombreCarpeta, String NombreModulo, List<string> respuestas, int numAct)
        { //Para los demas modulos
            this.Curso = Curso;
            this.NombreCarpeta = NombreCarpeta;
            this.NombreModulo = NombreModulo;
            this.respuestas = respuestas;
            this.numAct = numAct;
            actividad = new LeerArchivo(Curso, NombreCarpeta, NombreModulo);
        }

        public void RevisarActividad()
        {
            actividad.setDireccion(@"\Pautas");
            int cantidad = actividad.CantidadArchivos();

            //Numero de pauta
            pauta = actividad.LeerArchivos(numAct); 
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

        // Calcula las respuestas correctas e incorrectas que se obtuvo en una actividad del diagnostico
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
               Y CUANDO SE TENGA EL TOTAL SE PUEDE UTILIZAR LO SIGUIENTE: !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!*/
            string resultadoH1 = determinarNivelLogroActividad(H1C, H1I);
            string resultadoH2 = determinarNivelLogroActividad(H2C, H2I);
            Console.WriteLine(resultadoH1 + " | " + resultadoH2);
        }

        public string determinarNivelLogroActividad(int buenas, int malas) // Determina Logrado o No Logrado, sSE DEBERIA GUARDAR EL RESULTADO EN ALGUNA LISTA !!!!!
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

        public string determinarNivelLogroModulo(List<string> resultadoModulo)  // Determina nivel de logro del modulo realizado
        {  // DETERMINAR DE DONDE VA A VENIR ESA LISTA DE RESULTADOMODULO !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            // Se determina cuantos logrados y cuantos no logrados por las actividades hay
            Dictionary<string, int> contador = new Dictionary<string, int>();
            foreach (string item in resultadoModulo)
            {
                if (contador.ContainsKey(item))
                    contador[item]++;
                else
                    contador.Add(item, 1);
            }
            int logrado = 0;
            int nologrado = 0;
            foreach (KeyValuePair<string, int> item in contador)
            {
                if (item.Key.Equals("logrado")) logrado = item.Value;
                if (item.Key.Equals("no logrado")) nologrado = item.Value;
                Console.WriteLine(string.Format("{0} - {1}", item.Key, item.Value));
            }
            Console.WriteLine("Logrado = " + logrado);
            Console.WriteLine("No Logrado = " + nologrado);

            string resultado = null;
            double calcular = (100 * logrado) / (logrado + nologrado);  // Se saca el porcentaje de logro
            // Se determian el nivel de logro en el modulo
            if (calcular >= 0 || calcular <= 25) resultado = "Por Lograr -";
            if (calcular >= 26 || calcular <= 50) resultado = "Por Lograr +";
            if (calcular >= 51 || calcular <= 75) resultado = "Logrado -";
            if (calcular >= 76 || calcular <= 100) resultado = "Logrado +";

            return resultado;
        }

    }
}
