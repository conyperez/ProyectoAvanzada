using ProyectoAvanzada.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Controlador
{
    public class Controlador
    {
        private Boolean usuario;
        Modelo.ConexionBD conexion;
        Modelo.Modelo modelo;
        private string rut = "1.040.243-6";                     //Deberia haber una seleccion que te diga el rut del alumno!
        private string clave = "4546";
        string fecha = DateTime.Now.ToString("yyyy-MM-dd");
        String rut_p = "18.759.157-0";                          //Esto se ve con la base de datos.

        public Controlador()
        {
            conexion = new Modelo.ConexionBD();
            modelo = new Modelo.Modelo();
            usuario = conexion.ComprobarRegistro(rut, clave);           //Compruebo si existe el usuario
            conexion.cerrarBD();
            if (usuario)
            { // El usuario ya se registro y puede seguir rindiendo mcl donde quedo
                Console.WriteLine("El alumno esta registrado");
                conexion = new ConexionBD();
                if (!realizarDiagnostico(rut))
                { //Si ya realizo el diagnostico
                    Console.WriteLine("El alumno ya realizo el diagnostico");
                    conexion = new ConexionBD();
                    int generarCodigo = conexion.SeleccionarUltimoCodigoModulo();
                    conexion.cerrarBD();
                    string codigo = Convert.ToString(generarCodigo);
                    modelo.TrabajarModulo(rut, codigo, fecha, rut_p);

                }
                else //Si aun no realiza la evaluacion de diagnostico
                {
                    conexion = new ConexionBD();
                    String rDiagnostico;
                    int codigoGenerado = conexion.SeleccionarUltimoCodigoD();
                    conexion.cerrarBD();
                    Modelo.Diagnostico diagnostico = new Modelo.Diagnostico();
                    rDiagnostico = modelo.EvluacionDiagnostico();
                    string codigo = Convert.ToString(codigoGenerado);
                    conexion = new ConexionBD();
                    conexion.InsertarResultadosAlumnoDiagnostico(codigo, fecha, rut_p, rut, diagnostico.getH1C(), diagnostico.getH1I(), diagnostico.getH2C(), diagnostico.getH2I(), modelo.getResultadoH1(), modelo.getResultadoH2(), rDiagnostico);
                    conexion.cerrarBD();
                }
            }
            else
            {    //En la vista debe haber una opcion para el profesor y ahi recien poder insertar datos del profesor.
                //el usuario debe registrarse y esos datos se deben almacenar en la bd
                Console.WriteLine("El alumno no esta registrado");
                //Esos datos vienen en la vista
                conexion = new ConexionBD();
                conexion.InsertarDatosAlumno("Andres", "Rodriguez", "Rodriguez", "5A", rut, "4546", 2011);

                conexion.InsertarDatosProfesor("Camila", "Opazo", "Reyes", "5.323.234-1", "3243");
                //Aqui deberia ir un if que pregunte si el alumno seguira realizando el software o se saldra de el.
                conexion.InsertarDiagnostico("4546", fecha, "5.323.234-1", rut);   //Ingreso los datos de diagnostico
                String rDiagnostico;

                Modelo.Diagnostico diagnostico = new Modelo.Diagnostico();
                rDiagnostico = modelo.EvluacionDiagnostico();
                conexion.InsertarResultadosAlumnoDiagnostico("4546", fecha, "5.323.234-1", rut, diagnostico.getH1C(), diagnostico.getH1I(), diagnostico.getH2C(), diagnostico.getH2I(), modelo.getResultadoH1(), modelo.getResultadoH2(), rDiagnostico);// Ingreso los resultados de diagnostico
                conexion.cerrarBD();
            }
        }

        public Boolean realizarDiagnostico(string rut_a)
        {
            Boolean realizado = conexion.diagnosticoRealizado(rut_a);
            return realizado;
        }
        public void IngresoDatos(String nombre, String apellido_p, String apellido_m, String curso, String clave, int fecha)
        { //Este procedimiento es cuando el usuario ingresa por primera vez y se deben almacenar los datos a la bd
          //A partir de la clase vista se entregan los datos al controlador y este debe realizar lo siguiente..
          //Las variables son para asumir que la vista entrega estos datos

            conexion.InsertarDatosAlumno(nombre, apellido_p, apellido_m, curso, rut, clave, fecha);
        }
    }
}
