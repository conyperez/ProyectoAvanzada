using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    class Modulo
    {
        private String NombreCarpeta;
        private List<string> respuestas = new List<string>();
        private List<string> pauta = new List<string>();
        private LeerArchivo actividad;
        private double porcentaje_actividad;
        private int correcta, incorrecta;
        private string habilidades;


        public Modulo() { }

        public string RevisarActividad(List<String> respuestas, int numAct)
        {
            pauta = actividad.LeerArchivos(numAct);
            this.respuestas = respuestas;
            string resultado = null;
            try
            {
                // Se toma la primera linea donde se encuentran las habilidades de la act
                habilidades = pauta.ElementAt(0);
                Console.WriteLine(habilidades);
                // Se separan las habilidades por ','
                String[] habilidad = habilidades.Split(',');

                correcta = 0; incorrecta = 0;
                List<String> revision = new List<string>();
                for (int i = 0; i < respuestas.Count; i++)
                {
                    if (pauta.ElementAt(i + 1).Equals(respuestas.ElementAt(i)))
                    {
                        correcta++;
                    }
                    else
                    {
                        incorrecta++;
                    }
                }
                resultado = determinarNivelLogroActividad(correcta, incorrecta);
                Console.WriteLine(correcta);
                Console.WriteLine(incorrecta);
                Console.WriteLine("Resultado: " + resultado);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Mensaje 1: " + e.Message);
            }
            catch (NullReferenceException e1)
            {
                Console.WriteLine("Mensaje 2:" + e1.Message);
            }
            catch (InvalidOperationException e2)
            {
                Console.WriteLine("Mensaje 3:" + e2.Message);
            }
            return resultado;
        }

        public string determinarNivelLogroActividad(int buenas, int malas) // Determina Logrado o No Logrado, SE DEBERIA GUARDAR EL RESULTADO EN ALGUNA LISTA !!!!!
        {
            if (buenas == 0 && malas == 0) { return null; }
            porcentaje_actividad = (100 * buenas) / (buenas + malas);
            if (porcentaje_actividad > 60)
            {
                return "Logrado";
            }
            else
            {
                return "No Logrado";
            }
        }

        public string determinarNivelLogroModulo(List<string> resultadoModulo)  // Determina nivel de logro del modulo realizado
        {
            // Se determina cuantos logrados y cuantos no logrados por las actividades hay
            Dictionary<string, int> contador = new Dictionary<string, int>();
            foreach (string item in resultadoModulo)
            {
                try
                {
                    if (contador.ContainsKey(item))
                        contador[item]++;
                    else
                        contador.Add(item, 1);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            int logrado = 0;
            int nologrado = 0;
            foreach (KeyValuePair<string, int> item in contador)
            {
                if (item.Key.Equals("Logrado")) logrado = item.Value;
                if (item.Key.Equals("No Logrado")) nologrado = item.Value;
                Console.WriteLine(string.Format("{0} -> {1}", item.Key, item.Value));
            }

            Console.WriteLine("Logrado = " + logrado);
            Console.WriteLine("No Logrado = " + nologrado);

            string resultado = null;
            if (logrado == 0 && nologrado == 0) return null;
            double calcular = (100 * logrado) / (logrado + nologrado);  // Se saca el porcentaje de logro
            Console.WriteLine("CALCULO: " + calcular);
            // Se determian el nivel de logro en el modulo
            if (calcular >= 0 && calcular <= 25) resultado = "Por Lograr -";
            if (calcular >= 26 && calcular <= 50) resultado = "Por Lograr +";
            if (calcular >= 51 && calcular <= 75) resultado = "Logrado -";
            if (calcular >= 76 && calcular <= 100) resultado = "Logrado +";
            Console.WriteLine("Nivel de Logro Modulo: " + resultado);
            return resultado;
        }

        public string determinarProgreso(string nivelLogro)
        {
            if (nivelLogro.Equals("Por Lograr -") || nivelLogro.Equals("Por Lograr +"))
            {
                return "Repite";
            }
            if (nivelLogro.Equals("Logrado -"))
            {
                return "Remedial";
            }
            if (nivelLogro.Equals("Logrado +"))
            {
                return "Siguiente";
            }
            return null;
        }

        public void setArchivo(LeerArchivo archivos_actividad)
        {
            this.actividad = archivos_actividad;
        }

        public void setNombreCarpeta(String NombreCarpeta)
        {
            this.NombreCarpeta = NombreCarpeta;
        }

        public double getPorcentaje() { return porcentaje_actividad; }

        public string getHabilidades() { return habilidades; }
    }
}
