using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProyectoAvanzada
{
    public class LeerArchivo
    {
        private String NombreCarpeta, NombreModulo;
        private String Direccion;
        private int Cantidad;
        private string[] dirs;
        //Sobrecarga de constructores

        public LeerArchivo(String NombreCarpeta)
        { //Para la prueba de diagnostico
            this.NombreCarpeta = NombreCarpeta;
            this.Direccion = @"Material\Quinto Básico\" + NombreCarpeta;
        }

        public LeerArchivo(String NombreCarpeta, String NombreModulo)
        { //Para los modulos.
            this.NombreCarpeta = NombreCarpeta;
            this.NombreModulo = NombreModulo;
            Direccion = @"Material\Quinto Básico\" + NombreCarpeta + @"\" + NombreModulo;
        }

        //Metodo de leerArchivo
        public void LeerActividades()
        {
            try
            {
                Direccion = Direccion + @"\Actividades";
                NumeroArchivos();

                FileStream stream;
                StreamReader reader;
                for (int i = 0; i < Cantidad; i++)
                {
                    stream = new FileStream(dirs[i], FileMode.Open, FileAccess.Read);
                    reader = new StreamReader(stream);
                    String rd;
                    while (reader.Peek() > -1)
                    {
                        rd = reader.ReadLine();
                        Console.WriteLine(rd);


                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }

        public void LeerPautas()
        {
            try
            {
                Direccion = Direccion + @"\Pautas";

                NumeroArchivos();

                FileStream stream;
                StreamReader reader;
                for (int i = 0; i < Cantidad; i++)
                {
                    stream = new FileStream(dirs[i], FileMode.Open, FileAccess.Read);
                    reader = new StreamReader(stream);
                    String rd;
                    while (reader.Peek() > -1)
                    {
                        rd = reader.ReadLine();
                        Console.WriteLine(rd);


                    }
                }


            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }

        public void NumeroArchivos()
        {
            try
            {
                dirs = Directory.GetFiles(Direccion);
                Cantidad = dirs.Length;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
