using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada
{
    class Program
    {
        static void Main(string[] args)
        {
            LeerArchivo arch = new LeerArchivo("Módulo2", "MóduloInferir");
            arch.LeerPautas();
        }
    }
}
