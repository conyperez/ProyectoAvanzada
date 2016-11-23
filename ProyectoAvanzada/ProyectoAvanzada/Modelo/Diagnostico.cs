using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    class Diagnostico
    {
        private String NombreCarpeta;
        private List<string> respuestas = new List<string>();
        private List<string> pauta = new List<string>();
        private LeerArchivo actividad;
        private double porcentaje_actividad;
        private int H1C=0, H1I=0, H2C=0, H2I=0; // C: correctas ; I:incorrectas

        public Diagnostico() { }

        public void RevisarActividad(List<String> respuestas, int numAct)
        {
            //Numero de pauta
            pauta = actividad.LeerArchivos(numAct);
            this.respuestas = respuestas;
            try
            {
                // Se toma la primera linea donde se encuentran las habilidades de la act
                string habilidades = pauta.ElementAt(0);
                Console.WriteLine(habilidades);
                // Se separan las habilidades por ','
                String[] habilidad = habilidades.Split(',');

                List<String> revision = new List<string>();
                for (int i = 0; i < respuestas.Count; i++)
                { // Aca se cuenta correctas e incorrectas por habilidad
                    if (pauta.ElementAt(i + 1).Equals(respuestas.ElementAt(i)))
                    {
                        revision.Add("C");
                    }
                    else
                    {
                        revision.Add("I");
                    }
                }
                // Se determina el resultado
                calcularDiagnostico(revision, habilidad);
                Console.ReadKey();
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
        }

        // Calcula las respuestas correctas e incorrectas que se obtuvo en una actividad del diagnostico
        public void calcularDiagnostico(List<string> revision, String[] habilidad)
        {
            for (int i = 0; i < revision.Count; i++)
            {
                if (habilidad[i] == "H1")  // H1 = Extraer información explícita
                {
                    if (revision.ElementAt(i) == "C")
                    {
                        H1C++;
                    }
                    else
                    {
                        if (revision.ElementAt(i) == "I") { H1I++; }
                    }
                }
                else
                { // Es H2 = Análisis de la forma del texto
                    if (revision.ElementAt(i) == "C")
                    {
                        H2C++;
                    }
                    else
                    {
                        if (revision.ElementAt(i) == "I") { H2I++; }
                    }
                }
            }
        }

        public string determinarNivelLogroHabilidad(int buenas, int malas) // Determina Logrado o No Logrado de la habilidad
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

        public void setArchivo(LeerArchivo archivos_actividad)
        {
            this.actividad = archivos_actividad;
        }

        public void setNombreCarpeta(String NombreCarpeta)
        {
            this.NombreCarpeta = NombreCarpeta;
        }

        public double getPorcentH() { return porcentaje_actividad; }

        public int getH1C() { return H1C; }
        public int getH1I() { return H1I; }
        public int getH2C() { return H2C; }
        public int getH2I() { return H2I; }
    }

}
