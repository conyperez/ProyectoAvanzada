﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    public class Modelo
    {

        public Modelo() {
            Evaluaciones diagnostico = new Evaluaciones("Quinto Básico", "Módulo1", "Módulo 1.1", "MóduloE");
            List<string> respuestas = diagnostico.TrabajarActividad();
            Evaluar evaluar = new Evaluar("Quinto Básico", "Módulo1", "Módulo 1.1", "MóduloE", respuestas);
            evaluar.RevisarActividad();
            Console.ReadKey();
        }

    }
}
