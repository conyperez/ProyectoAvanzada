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

        public Evaluar(String Curso, String NombreCarpeta, List<string> respuestas)
        { //Para la prueba de diagnostico
            this.Curso = Curso;
            this.NombreCarpeta = NombreCarpeta;
            this.respuestas = respuestas;
        }

        public Evaluar(String Curso, String NombreCarpeta, String NombreModulo, String TipoModulo, List<string> respuestas)
        { //Para el modulo1.
            this.Curso = Curso;
            this.NombreCarpeta = NombreCarpeta;
            this.NombreModulo = NombreModulo;
            this.TipoModulo = TipoModulo;
            this.respuestas = respuestas;
        }

        public Evaluar(String Curso, String NombreCarpeta, String NombreModulo, List<string> respuestas)
        { //Para los demas modulos
            this.Curso = Curso;
            this.NombreCarpeta = NombreCarpeta;
            this.NombreModulo = NombreModulo;
            this.respuestas = respuestas;
        }

        public void RevisarActividad()
        {
            LeerArchivo actividad = new LeerArchivo(Curso, NombreCarpeta, NombreModulo, TipoModulo);
            actividad.setDireccion(@"\Pautas");

            //Numero de pauta
            pauta = actividad.LeerArchivos(0);  // DEBERIA HABER UNA VARIABLE CON EL NUMERO DE LA ACTIVIDAD!!!!!!!!!!!!!!!!!!
            // Se toma la primera linea donde se encuentran las habilidades de la act
            try
            {
                string habilidades = pauta.ElementAt(0);
                Console.WriteLine(habilidades);
                // Se separan las habilidades por ','
                String[] habilidad = habilidades.Split(',');
                for (int i = 0; i < habilidad.Length; i++)
                {
                    Console.WriteLine(habilidad[i]);
                }

                int correcta = 0;  //Global(?)
                int incorrecta = 0;
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
                            revision.Add("N");
                        }
                        //CalcularDiagnostico();
                    }
                    else // Es Módulo
                    {
                        // Para los otros módulos, se cuentan correctas e incorrectas por la actividad 
                        if (pauta.ElementAt(i + 1).Equals(respuestas.ElementAt(i)))
                        {
                            correcta++;
                            Console.WriteLine("Correcta");
                        }
                        else
                        {
                            incorrecta++;
                            Console.WriteLine("Incorrecta");
                        }
                        //CalcularActividad();
                    }
                }
                //Console.WriteLine("Incorrectas: "+ incorrecta);
                //Console.WriteLine("Correctas: " + correcta);
                for (int i = 0; i < revision.Count; i++)
                {
                    Console.WriteLine(revision.ElementAt(i));
                }
                Console.ReadKey();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Mensaje 1: "+ e.Message);
            }
            catch (NullReferenceException e1)
            {
                Console.WriteLine("Mensaje 2:"+ e1.Message);
            }
            catch (InvalidOperationException e2)
            {
                Console.WriteLine("Mensaje 3:" + e2.Message);
            }
        }

    }
}
