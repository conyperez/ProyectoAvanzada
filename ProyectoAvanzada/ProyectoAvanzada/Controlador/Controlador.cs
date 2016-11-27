using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Controlador
{
    class Controlador
    {
        Modelo.ConexionBD conexion;
        Modelo.Modelo modelo;
        string rut;

        public Controlador(string rut)
        {
            this.rut = rut;
            conexion = new Modelo.ConexionBD();
            modelo = new Modelo.Modelo();
        }

        public void realizarDiagnostico()
        {
            Boolean realizado = conexion.diagnosticoRealizado(rut);
            if(realizado == true)
            {
                modelo.TrabajoDiagnostico(rut);
            } else
            {
                modelo.TrabajoDiagnostico(rut);
            }
        }
    }
}
