using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace ProyectoAvanzada.Modelo
{
    class GenerarReporte
    {
        private Document doc;
        private PdfWriter writer;
        private ConexionBD conexion;
        private string rut;

        public GenerarReporte(string rut)
        {
            this.rut = rut;
            // Se crea el documento con el tamaño de página tradicional
            this.doc = new Document(PageSize.LETTER);
            // Donde se guarda el documento
            this.writer = PdfWriter.GetInstance(doc, new FileStream(@"Reporte.pdf", FileMode.Create));
            this.conexion = new ConexionBD();
            crearReporte();
            datosReporte();

            //Seleccionar si es diagnostico o modulo
            //cuerpoDiagnostico();
            cuerpoModulo();
            cerrarReporte();
        }

        public void crearReporte()
        {
            // Título y el autor (Esto no será visible en el documento)
            doc.AddTitle("Reporte");
            doc.AddCreator("MCL Software");
            // Abrimos el archivo
            doc.Open();
        }

        public void datosReporte()
        {
            // Titulo del documento
            Paragraph titulo = new Paragraph();
            titulo.Alignment = Element.ALIGN_CENTER;
            titulo.Font = new Font(Font.FontFamily.UNDEFINED, 18);
            titulo.Font = FontFactory.GetFont("Calibri", 18);
            titulo.Font.SetStyle(Font.BOLD);
            titulo.Add("Reporte MCL");
            doc.Add(titulo);
            doc.Add(Chunk.NEWLINE);

            string[] alumno = conexion.SeleccionarAlumno(rut);
            conexion.cerrarBD();
            // Nombre alumno
            doc.Add(new Paragraph("Nombre Alumno: " + alumno[0], FontFactory.GetFont("ARIAL", 12, Font.BOLD)));

            // RUT alumno
            doc.Add(new Paragraph("RUT: " + rut, FontFactory.GetFont("ARIAL", 12, Font.BOLD)));

            // Curso alumno
            doc.Add(new Paragraph("Curso: " + alumno[1], FontFactory.GetFont("ARIAL", 12)));

            // Fecha
            DateTime date = DateTime.Now;
            string fecha = date.ToLongDateString();
            doc.Add(new Paragraph("Fecha: "+ fecha, FontFactory.GetFont("ARIAL", 12)));

            conexion = new ConexionBD();
            string nomProfesor = conexion.SeleccionarProfesorDeAlumno(rut);
            conexion.cerrarBD();
            // Profesor del alumno
            doc.Add(new Paragraph("Nombre Profesor: " + nomProfesor, FontFactory.GetFont("ARIAL", 12)));

            doc.Add(new Paragraph("________________________________________________________________________________", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.LIGHT_GRAY)));
        }

        public void cuerpoDiagnostico()
        {
            // Titulo
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Paragraph("Resultados Evaluación de Diagnóstico:", FontFactory.GetFont("ARIAL", 14, Font.BOLD)));
            doc.Add(Chunk.NEWLINE);

            conexion = new ConexionBD();
            List<Object> resultados = conexion.SeleccionarResultadosDiagnostico(rut);
            conexion.cerrarBD();
            // Habilidades
            doc.Add(new Paragraph("     - Habilidad Uno: Extraer Información Explícita.", FontFactory.GetFont("ARIAL", 12, Font.BOLD)));
            // Correctas
            doc.Add(new Paragraph("          - Respuestas Correctas: " + resultados.ElementAt(0), FontFactory.GetFont("ARIAL", 12)));
            // Incorrectas
            doc.Add(new Paragraph("          - Respuestas Incorrectas: " + resultados.ElementAt(1), FontFactory.GetFont("ARIAL", 12)));
            doc.Add(Chunk.NEWLINE);

            // Habilidades
            doc.Add(new Paragraph("     - Habilidad Dos: Análisis de la forma del texto.", FontFactory.GetFont("ARIAL", 12, Font.BOLD)));
            // Correctas
            doc.Add(new Paragraph("          - Respuestas Correctas: " + resultados.ElementAt(2), FontFactory.GetFont("ARIAL", 12)));
            // Incorrectas
            doc.Add(new Paragraph("          - Respuestas Incorrectas: " + resultados.ElementAt(3), FontFactory.GetFont("ARIAL", 12)));

            doc.Add(Chunk.NEWLINE);
            conexion = new ConexionBD();
            List<string> nivel = conexion.SeleccionarNivelHabilidad(rut);
            conexion.cerrarBD();
            // Nivel de logro
            doc.Add(new Paragraph("     - Nivel de logro obtenido en la habilidad Extraer información explícita: " + nivel.ElementAt(0), FontFactory.GetFont("ARIAL", 12, Font.BOLD)));
            // Nivel de logro
            doc.Add(new Paragraph("     - Nivel de logro obtenido en la habilidad Análisis de la forma del texto: " + nivel.ElementAt(1), FontFactory.GetFont("ARIAL", 12, Font.BOLD)));

            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            // Explicacion
            doc.Add(new Paragraph("     ¿Qué representa el Nivel de Logro?\n\n", FontFactory.GetFont("ARIAL", 12, Font.BOLD)));

            // Tabla
            PdfPTable unaTabla = new PdfPTable(2);
            unaTabla.AddCell(new Paragraph("Nivel de Logro", FontFactory.GetFont("ARIAL", 12, Font.BOLD)));
            unaTabla.AddCell(new Paragraph("Descripción", FontFactory.GetFont("ARIAL", 12, Font.BOLD)));

            foreach (PdfPCell celda in unaTabla.Rows[0].GetCells())
            {
                celda.BackgroundColor = BaseColor.LIGHT_GRAY;
                celda.HorizontalAlignment = 1;
                celda.Padding = 3;
            }

            PdfPCell celda1 = new PdfPCell(new Paragraph("Logrado (L)", FontFactory.GetFont("Arial", 12)));
            PdfPCell celda2 = new PdfPCell(new Paragraph("Haz alcanzado el nivel esperado, demostrando dominio en la habilidad.", FontFactory.GetFont("Arial", 12)));
            PdfPCell celda3 = new PdfPCell(new Paragraph("No Logrado (NL)", FontFactory.GetFont("Arial", 12)));
            PdfPCell celda4 = new PdfPCell(new Paragraph("No haz alcanzado el nivel esperado, demostrando que no presentas suficiente aprendizaje en la habilidad.", FontFactory.GetFont("Arial", 12)));

            unaTabla.AddCell(celda1);
            unaTabla.AddCell(celda2);
            unaTabla.AddCell(celda3);
            unaTabla.AddCell(celda4);

            doc.Add(unaTabla);

            doc.Add(Chunk.NEWLINE);
            string habilidad = null;
            if (nivel.ElementAt(2).Equals("Módulo E")) habilidad = "la habilidad: Extraer información explícita.";
            if (nivel.ElementAt(2).Equals("Módulo F")) habilidad = "la habilidad: Análisis de la forma del texto.";
            if (nivel.ElementAt(2).Equals("Módulo EF")) habilidad = "ambas habilidades: Extraer información explícita y Análisis de la forma del texto.";
            doc.Add(new Paragraph("     Según los resultados debes realizar el Módulo Uno enfocado en " + habilidad, FontFactory.GetFont("ARIAL", 12, Font.BOLD)));
        }

        public void cuerpoModulo()
        {
            // Titulo
            conexion = new ConexionBD();
            string[] nombre = conexion.SeleccionarTodosLosModulosRealizados(rut);
            conexion.cerrarBD();
            string modulo = null;
            if (nombre[0].Equals("Módulo E")) modulo = "Uno: Extrayendo información explícita";
            if (nombre[0].Equals("Módulo F")) modulo = "Uno: Analizando la forma del texto";
            if (nombre[0].Equals("Módulo EF")) modulo = "Uno: Extrayendo información explícita y Analizando la forma del texto";
            if (nombre[0].Equals("Módulo 2")) modulo = "Dos: Infiriendo los significados de las palabras";
            if (nombre[0].Equals("Módulo 3")) modulo = "Tres: Extrayendo información implícita";
            if (nombre[0].Equals("Módulo 4")) modulo = "Cuatro: Interpretando lenguaje poético";
            if (nombre[0].Equals("Módulo 5")) modulo = "Cinco";
            if (nombre[0].Equals("Módulo R")) modulo = " Remedial";
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Paragraph("Resultados Módulo "+ modulo +".", FontFactory.GetFont("ARIAL", 14, Font.BOLD)));
            doc.Add(Chunk.NEWLINE);

            // Resultado de las actividades
            int codigo = Convert.ToInt32(nombre[2]);
            conexion = new ConexionBD();
            List<Object> act = conexion.SeleccionarActRealizadasEnModulo(rut, codigo);
            conexion.cerrarBD();
            conexion = new ConexionBD();
            List<string> habilidades = conexion.SeleccionarHabilidadesEnModulo(rut, codigo);
            conexion.cerrarBD();
            conexion = new ConexionBD();
            List<string> nivelLogro = conexion.SeleccionarNivelLogroAct(rut, codigo);
            conexion.cerrarBD();
            doc.Add(new Paragraph("     Nivel de Logo en cada actividad realizada:", FontFactory.GetFont("ARIAL", 12, Font.BOLD)));
            for (int i=0; i<act.Count; i++)
            {
                doc.Add(new Paragraph("          "+ act.ElementAt(i) +". Actividad "+ act.ElementAt(i) + ":", FontFactory.GetFont("ARIAL", 12, Font.BOLD)));
                string habilidad = identificarHabilidad(habilidades.ElementAt(i));
                doc.Add(new Paragraph("              Habilidad: " + habilidad, FontFactory.GetFont("ARIAL", 12)));
                doc.Add(new Paragraph("              Nivel de Logro: " + nivelLogro.ElementAt(i), FontFactory.GetFont("ARIAL", 12)));
            }

            doc.Add(Chunk.NEWLINE);
            doc.Add(new Paragraph("     Resultado final del Módulo " + modulo +": "+ nombre[1], FontFactory.GetFont("ARIAL", 12, Font.BOLD)));
            doc.Add(new Paragraph("     Debes "+ identificarSgtePaso(nombre[1]), FontFactory.GetFont("ARIAL", 12, Font.BOLD)));
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Paragraph("     ¿Qué representa el nivel de logro obtenido en el módulo realizado?\n\n", FontFactory.GetFont("ARIAL", 12, Font.BOLD)));

            // Tabla
            PdfPTable unaTabla = new PdfPTable(2);
            unaTabla.AddCell(new Paragraph("Nivel de Logro", FontFactory.GetFont("ARIAL", 12, Font.BOLD)));
            unaTabla.AddCell(new Paragraph("Descripción", FontFactory.GetFont("ARIAL", 12, Font.BOLD)));

            foreach (PdfPCell celda in unaTabla.Rows[0].GetCells())
            {
                celda.BackgroundColor = BaseColor.LIGHT_GRAY;
                celda.HorizontalAlignment = 1;
                celda.Padding = 3;
            }

            PdfPCell celda1 = new PdfPCell(new Paragraph(nombre[1], FontFactory.GetFont("Arial", 12)));
            PdfPCell celda2 = new PdfPCell(new Paragraph(descripcionNivelLogro(nombre[1]), FontFactory.GetFont("Arial", 12)));
            
            unaTabla.AddCell(celda1);
            unaTabla.AddCell(celda2);
            doc.Add(unaTabla);
        }

        public void cerrarReporte()
        {
            doc.Close();
            writer.Close();
        }

        // Identificar la habilidad que posee la actividad
        public string identificarHabilidad(string habilidad)
        {
            string[] habilidades = habilidad.Split(',');
            string nombre = null;
            string hab = null;
            string nom = null;
            for(int i=0; i<habilidades.Length; i++)
            {
                if (i > 0) nom = nom + ", "; 
                if (habilidades[i].Equals("H1")) nombre = "Extraer información explícita";
                if (habilidades[i].Equals("H2")) nombre = "Análisis de la forma del texto";
                if (habilidades[i].Equals("H3")) nombre = "Inferir significados de las palabras";
                if (habilidades[i].Equals("H4")) nombre = "Extraer información implícita";
                if (habilidades[i].Equals("H5")) nombre = "Interpretar lenguaje poético";
                nom = nom + nombre;
                hab = nom;
            }
            return hab;
        }

        // Idetificar si pasa al sgte modulo, lo repite o realiza modulo remedial
        public string identificarSgtePaso(string nivelLogro)
        {
            string definir = null;
            if (nivelLogro.Equals("Por Lograr -") || nivelLogro.Equals("Por Lograr +")) definir = "repetir el módulo.";
            if (nivelLogro.Equals("Logrado -")) definir = "realizar un Módulo Remedial.";
            if (nivelLogro.Equals("Logrado +")) definir = "realizar el siguiente módulo.";
            return definir;
        }

        public string descripcionNivelLogro(string nivelLogro)
        {
            string descripcion = null;
            if (nivelLogro.Equals("Por Lograr -")) descripcion = "Presentas importantes vacíos en la adquisición de los aprendizajes en las habilidades, considerándose insuficiente.";
            if (nivelLogro.Equals("Por Lograr +")) descripcion = "Aún estas desarrollando las habilidades correspondientes, considerándose básico.";
            if (nivelLogro.Equals("Logrado -")) descripcion = "Haz adquirido el aprendizaje en un buen nivel, considerándose apropiado, pero no suficiente para el desarrollo de las otras habilidades.";
            if (nivelLogro.Equals("Logrado +")) descripcion = "Tu aprendizaje está consolidado, considerándose avanzado para este módulo";
            return descripcion;
        }
    }
}
