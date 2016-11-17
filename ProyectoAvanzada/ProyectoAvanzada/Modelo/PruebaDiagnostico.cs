using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    public class PruebaDiagnostico
    {
        private List<String> actividad = new List<string>();
        private List<String> codigos = new List<string>();
        public void TrabajarActividad()
        {
            LeerArchivo archivo = new LeerArchivo("Evaluación Diagnóstico");

            archivo.setDireccion(@"\Actividades");

            int cantidad = archivo.CantidadArchivos();

            actividad = archivo.LeerActividad(0);

            for (int i = 0; i < actividad.Count; i++)
            {

                //Console.WriteLine(actividad.ElementAt(i));
                //Texto
                if (actividad.ElementAt(i).Equals("T1"))
                {
                    Console.WriteLine("Aqui va el Texto 1");
                }
                //Preguntas de seleccion
                else if (actividad.ElementAt(i).Equals("$"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                else if (actividad.ElementAt(i).Equals("$@"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                else if (actividad.ElementAt(i).Equals("@"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                else if (actividad.ElementAt(i).Equals("@\\"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                //Enunciado preguntas seleccion(mas de una)
                else if (actividad.ElementAt(i).Equals("$#"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                else if (actividad.ElementAt(i).Equals("#"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                else if (actividad.ElementAt(i).Equals("#\\"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                //Preguntas ComboBox
                else if (actividad.ElementAt(i).Equals("|"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                else if (actividad.ElementAt(i).Equals("|\\"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                //Terminos Pareados
                else if (actividad.ElementAt(i).Equals("$%"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                else if (actividad.ElementAt(i).Equals("%"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                else if (actividad.ElementAt(i).Equals("%\\"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                else if (actividad.ElementAt(i).Equals("%*"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                //Enunciado ordenar
                else if (actividad.ElementAt(i).Equals("$&"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                else if (actividad.ElementAt(i).Equals("&"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                else if (actividad.ElementAt(i).Equals("&\\"))
                {
                    codigos.Add(actividad.ElementAt(i));
                    i++;
                }
                Console.WriteLine(actividad.ElementAt(i)); //Imprime lo que quedo del archivo (las preguntas y respuestas)
            }
            /*for (int j = 0; j < codigos.Count; j++) //imprime los codigos
            {
                Console.WriteLine(codigos.ElementAt(j));
            }*/
            Console.ReadKey();

        }

    }
}
