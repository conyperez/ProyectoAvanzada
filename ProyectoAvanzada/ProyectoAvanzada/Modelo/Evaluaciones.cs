using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    public class Evaluaciones
    {
        private List<string> respuestas;
        private List<string> actividad = new List<string>();
        private LeerArchivo archivo;

        public List<String> TrabajaActividad(int numActividad)
        {
            respuestas = new List<string>();
            actividad = archivo.LeerArchivos(numActividad);     //Se almacena la actividad establecida
            for (int i = 0; i < actividad.Count; i++)
            {
                //Texto
                if (actividad.ElementAt(i).Equals("T1") || actividad.ElementAt(i).Equals("T2") || actividad.ElementAt(i).Equals("T3"))
                {
                    Console.WriteLine("Aqui va el Texto");
                }
                if (actividad.ElementAt(i).Equals("$")) // Enunciado
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("$@")) // Preguntas de seleccion multiple
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
                if (actividad.ElementAt(i).Equals("$#")) // Preguntas de seleccion (mas de una)
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("#"))
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("#\\"))
                {
                    while (actividad.ElementAt(i).Equals("#\\"))
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
                    bool termino = false;
                    while (!termino)
                    {
                        string respuesta = Console.ReadLine();
                        if (respuesta != "*")
                            respuestas.Add(respuesta);
                        else
                            termino = true;
                    }
                }
                if (actividad.ElementAt(i).Equals("|"))  // Preguntas combo box
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("|\\"))
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                    bool termino = false;
                    while (!termino)
                    {
                        string respuesta = Console.ReadLine();
                        if (respuesta != "*")
                            respuestas.Add(respuesta);
                        else
                            termino = true;
                    }
                }
                if (actividad.ElementAt(i).Equals("$%"))  // Terminos pareados
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("%"))
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("%\\"))
                {
                    while (actividad.ElementAt(i).Equals("%\\"))
                    {
                        i++;
                        Console.WriteLine("- " + actividad.ElementAt(i));
                        i++;
                        if (i >= actividad.Count)
                        {
                            break;
                        }
                    }
                    i--;
                }
                if (actividad.ElementAt(i).Equals("%*"))
                {
                    while (actividad.ElementAt(i).Equals("%*"))
                    {
                        i++;
                        Console.WriteLine("+ " + actividad.ElementAt(i));
                        i++;
                        if (i >= actividad.Count)
                        {
                            break;
                        }
                    }
                    i--;
                    respuestas.Add(Console.ReadLine());
                }
                if (actividad.ElementAt(i).Equals("$&"))  // Ordenar las afirmaciones
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("&"))
                {
                    Console.WriteLine(actividad.ElementAt(i + 1));
                }
                if (actividad.ElementAt(i).Equals("&\\"))
                {
                    int cont = 0;
                    while (actividad.ElementAt(i).Equals("&\\"))
                    {
                        i++;
                        cont++;
                        Console.WriteLine("- " + actividad.ElementAt(i));
                        i++;
                        if (i >= actividad.Count)
                        {
                            break;
                        }
                    }
                    i--;
                    Console.WriteLine("CONTADOR: " + cont);
                    for (int j = 0; j < cont; j++)
                    {
                        respuestas.Add(Console.ReadLine());
                    }
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
