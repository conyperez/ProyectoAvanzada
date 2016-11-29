using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    public class Evaluaciones
    {
        private List<string> pauta = new List<string>();
        private List<string> respuestas = new List<string>();
        private List<string> actividad = new List<string>();
        private LeerArchivo archivo;

        public List<String> TrabajaActividad(int numActividad)
        {
            List<String> respuestas = new List<string>();

            actividad = archivo.LeerArchivos(numActividad);//Se almacena la actividad establecida
            for (int i = 0; i < actividad.Count; i++)
            {
                //Texto
                if (actividad.ElementAt(i).Equals("T1") || actividad.ElementAt(i).Equals("T2") || actividad.ElementAt(i).Equals("T3"))
                {
                    Console.WriteLine("Aqui va el Texto");
                }
                //Preguntas de seleccion
                if (actividad.ElementAt(i).Equals("$"))
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("$@"))
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("@"))
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("@\\"))
                {
                    while (actividad.ElementAt(i).Equals("@\\"))
                    {
                        i++;
                        Console.WriteLine(actividad.ElementAt(i));
                        i++;
                        if (i >= actividad.Count)
                        {
                            break;
                        }
                    }
                    i--;
                    respuestas.Add(Console.ReadLine());
                }
                if (actividad.ElementAt(i).Equals("$#")) //Preguntas de seleccion (mas de una)
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("#"))
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("#\\"))
                {
                    //Aqui va la pregunta de Seleccion multiple
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("|")) // Pregunta combo box
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("|\\"))
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));   // Tomar encuenta que las opciones a la respuestas estan entre '...-...-...'
                }
                if (actividad.ElementAt(i).Equals("$%"))   // Terminos pareado
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("%")) 
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("%\\"))   // Lado izquierdo de los terminos pareados
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("%*"))    // Lado derecho de los terminos pareados
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("$&"))    // Ordenar elementos
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("&"))
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("&\\"))
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
            }
            Console.ReadKey();
            return respuestas;

        }
        public void setArchivo(LeerArchivo archivos_actividad)
        {
            this.archivo = archivos_actividad;
        }
    }
}
