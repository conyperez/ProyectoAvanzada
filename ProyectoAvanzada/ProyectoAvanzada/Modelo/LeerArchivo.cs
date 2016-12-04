using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProyectoAvanzada
{
        /*Aqui es donde se sacan los archivos para las evaluaciones,pautas,imagenes,etc.*/
    public class LeerArchivo
    {
        private String Direccion;

        public LeerArchivo(String Curso, String NombreCarpeta)//Para la prueba de diagnostico
        {
            Direccion = @"Material\" + Curso + @"\" + NombreCarpeta;
        }

        public LeerArchivo(String Curso, String NombreCarpeta, String NombreModulo, String TipoMoodulo)//Para el Módulo 1.
        {
            Direccion = @"Material\" + Curso + @"\" + NombreCarpeta + @"\" + NombreModulo + @"\" + TipoMoodulo;
        }

        public LeerArchivo(String Curso, String NombreCarpeta, String NombreModulo)//Para los otros modulos
        {
            Direccion = @"Material\" + Curso + @"\" + NombreCarpeta + @"\" + NombreModulo;
        }

        public LeerArchivo() { }

        public List<String> LeerArchivos(int i)
        {
            List<String> archivo = new List<string>();
            try
            {
                String[] dirs = Directory.GetFiles(Direccion); //Cantidad de archivos en la carpeta
                FileStream stream = new FileStream(dirs[i], FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                String rd;
                while (reader.Peek() > -1)
                {
                    rd = reader.ReadLine();
                    archivo.Add(rd);
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
            return archivo;

        }

        public void setDireccion(String url)
        {
            this.Direccion = Direccion + url;
        }

        public int getCantArchivos()
        {
            int cantidad;
            string[] actividad = Directory.GetFiles(Direccion);
            cantidad = actividad.Length;
            return cantidad;
        }

    }
}
