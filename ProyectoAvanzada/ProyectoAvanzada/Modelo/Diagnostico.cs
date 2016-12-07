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
        private int H1C, H1I, H2C, H2I;        // C: correctas ; I:incorrectas

        public Diagnostico() {
            this.H1C = 0;
            this.H1I = 0;
            this.H2C = 0;
            this.H2I = 0;
        }

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
                for (int i = 1; i < pauta.Count; i++)
                { // Aca se cuenta correctas e incorrectas por habilidad
                    if((i-1) == respuestas.Count)
                    {
                        revision.Add("I");
                        Console.WriteLine("Incorrecta");
                    } else
                    {
                        if (pauta.ElementAt(i).Equals(respuestas.ElementAt(i - 1)))
                        {
                            revision.Add("C");
                            Console.WriteLine("Correcta");
                        }
                        else
                        {
                            revision.Add("I");
                            Console.WriteLine("Incorrecta");
                        }
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
                        if (revision.ElementAt(i) == "I") { this.H1I++; }
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
                        if (revision.ElementAt(i) == "I") { this.H2I++; }
                    }
                }
            }
        }

        public string determinarNivelLogroHabilidad(int buenas, int malas) // Determina Logrado o No Logrado de la habilidad
        {
            if (buenas == 0 && malas == 0) { return null; }
            Console.WriteLine("Buenas: " + buenas + "\nMalas: "+ malas);
            porcentaje_actividad = (100 * buenas) / (buenas + malas);
            Console.WriteLine("CALCULO: "+ porcentaje_actividad);
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

        public int getH1C() { return this.H1C; }
        public int getH1I() { return this.H1I; }
        public int getH2C() { return this.H2C; }
        public int getH2I() { return this.H2I; }

        public void setH1C(int value) { this.H1C = value; }
        public void setH1I(int value) { this.H1I = value; }
        public void setH2C(int value) { this.H2C = value; }
        public void setH2I(int value) { this.H2I = value; }
    }
}
