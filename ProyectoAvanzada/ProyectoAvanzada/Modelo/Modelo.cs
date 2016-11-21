using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    public class Modelo
    {

        public Modelo() {
            //Evaluaciones diagnostico = new Evaluaciones("Quinto Básico", "Módulo1", "Módulo 1.1", "MóduloE", 0);
            Evaluaciones diagnostico = new Evaluaciones("Quinto Básico", "Evaluación Diagnóstico", 0);
            List<string> respuestas = diagnostico.TrabajarActividad();
            //Evaluar evaluar = new Evaluar("Quinto Básico", "Módulo1", "Módulo 1.1", "MóduloE", respuestas, 0);
            Evaluar evaluar = new Evaluar("Quinto Básico", "Evaluación Diagnóstico", respuestas, 0);
            evaluar.RevisarActividad();
            Console.ReadKey();
        }

    }
}
