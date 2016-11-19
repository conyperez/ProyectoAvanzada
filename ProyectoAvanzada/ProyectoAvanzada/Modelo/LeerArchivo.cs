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
        private String[] dirs;
        //Sobrecarga de constructores

        public LeerArchivo(String Curso, String NombreCarpeta)
        { //Para la prueba de diagnostico
            this.NombreCarpeta = NombreCarpeta;
            this.Direccion = @"Material\"+ Curso + @"\" + NombreCarpeta;
        }

        public LeerArchivo(String Curso, String NombreCarpeta, String NombreModulo)
        { //Para los modulos.
            this.NombreCarpeta = NombreCarpeta;
            this.NombreModulo = NombreModulo;
            Direccion = @"Material\"+ Curso + @"\" + NombreCarpeta + @"\" + NombreModulo;
        }

        public List<String> LeerActividad(int i)
        {
            List<String> actividad = new List<string>();
            try
            {
                FileStream stream = new FileStream(dirs[i], FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                String rd;
                while (reader.Peek() > -1)
                {
                    rd = reader.ReadLine();
                    actividad.Add(rd);
                }
            }
            catch (NullReferenceException e1)
            {
                Console.WriteLine(e1.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return actividad;
        }


        public int CantidadArchivos()
        {
            int tamanio = 0;
            try
            {
                this.dirs = Directory.GetFiles(Direccion);
                tamanio = dirs.Length;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            return tamanio;
        }

        public void setDireccion(String url)
        {
            this.Direccion = this.Direccion + url;
        }


    }
}
