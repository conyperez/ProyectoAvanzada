using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    public class PruebaDiagnostico
    {
        private List<string> pauta = new List<string>();
        private List<string> respuestas = new List<string>();
        public void TrabajarActividad()
        {
            LeerArchivo archivo = new LeerArchivo("Evaluación Diagnóstico");

            archivo.setDireccion(@"\Actividades");

            int cantidad = archivo.CantidadArchivos();
            //Numero de actividad
            pauta = archivo.LeerActividad(0);

            for (int i = 0; i < pauta.Count; i++)
            {
                //Texto
                if (pauta.ElementAt(i).Equals("T1"))
                {
                    Console.WriteLine("Aqui va el Texto 1");
                }
                //Preguntas de seleccion
                if (pauta.ElementAt(i).Equals("$"))
                {

                    Console.WriteLine(pauta.ElementAt(i + 1));
                    //Console.WriteLine(i);

                    //codigos.Add(actividad.ElementAt(i));

                }
                if (pauta.ElementAt(i).Equals("$@"))
                {
                    //codigos.Add(actividad.ElementAt(i));
                    Console.WriteLine(pauta.ElementAt(i + 1));
                    // Console.WriteLine(i);

                }

                if (pauta.ElementAt(i).Equals("@"))
                {
                    Console.WriteLine(pauta.ElementAt(i + 1));
                }
                if (pauta.ElementAt(i).Equals("@\\"))
                {
                    while (pauta.ElementAt(i).Equals("@\\"))
                    {
                        i++;
                        Console.WriteLine(pauta.ElementAt(i));
                        i++;
                        if (i >= pauta.Count)
                        {
                            break;
                        }
                    }

                    i--;
                    respuestas.Add(Console.ReadLine());
                }

            }
            Console.ReadKey();
        }

        public void RevisarActividad()
        {
            LeerArchivo acvitivdad = new LeerArchivo("Evaluación Diagnóstico");

            acvitivdad.setDireccion(@"\Pautas");

            int cantidad = acvitivdad.CantidadArchivos();

            //Numero de pauta
            pauta = acvitivdad.LeerActividad(0);

            for (int i = 0; i < respuestas.Count; i++)
            {
                if (pauta.ElementAt(i + 1).Equals(respuestas.ElementAt(i)))
                {
                    Console.WriteLine("Correcto!");
                }
                else
                {
                    Console.WriteLine("Incorrecto..");
                }

            }

            Console.ReadKey();
        }

    }
}
