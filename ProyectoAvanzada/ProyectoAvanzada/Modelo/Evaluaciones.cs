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
        private String Curso, NombreCarpeta, NombreModulo, TipoModulo;
        private LeerArchivo archivo;

        public Evaluaciones(String Curso, String NombreCarpeta)
        { //Para la prueba de diagnostico
            this.Curso = Curso;
            this.NombreCarpeta = NombreCarpeta;
            archivo = new LeerArchivo(Curso, NombreCarpeta);
        }

        public Evaluaciones(String Curso, String NombreCarpeta, String NombreModulo, String TipoModulo)
        { //Para el modulo1.
            this.Curso = Curso;
            this.NombreCarpeta = NombreCarpeta;
            this.NombreModulo = NombreModulo;
            this.TipoModulo = TipoModulo;
            archivo = new LeerArchivo(Curso, NombreCarpeta, NombreModulo, TipoModulo);
        }

        public Evaluaciones(String Curso, String NombreCarpeta, String NombreModulo)
        { //Para los demas modulos
            this.Curso = Curso;
            this.NombreCarpeta = NombreCarpeta;
            this.NombreModulo = NombreModulo;
            archivo = new LeerArchivo(Curso, NombreCarpeta, NombreModulo);
        }

        public List<string> TrabajarActividad()
        {

            archivo.setDireccion(@"\Actividades");

            int cantidad = archivo.CantidadArchivos();
            //Numero de actividad
            actividad = archivo.LeerArchivos(0); //elige la actividad que se va a realizar

            for (int i = 0; i < actividad.Count; i++)
            {
                //Texto
                if (actividad.ElementAt(i).Equals("T1"))
                {
                    Console.WriteLine("Aqui va el Texto 1");
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

            }
            Console.ReadKey();
            return respuestas;
        }
    }
}
