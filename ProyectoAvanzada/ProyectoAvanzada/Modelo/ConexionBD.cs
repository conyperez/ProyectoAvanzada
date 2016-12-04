using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAvanzada.Modelo
{
    public class ConexionBD
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        public ConexionBD()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            /*Cada uno coloca su password y su base de datos*/

            builder.Password = "cony123";
            builder.Database = "mclbd";

            conn = new MySqlConnection(builder.ToString());
            conn.Open();


        }

        public void cerrarBD()
        {
            conn.Close();
        }


        //Inserta todos los datos del alumno
        public void InsertarDatosAlumno(String nombre, String apellido_p, String apellido_m, String curso, String rut, String clave, int fecha)
        {
            cmd = new MySqlCommand(String.Format("INSERT INTO alumno (nombre,apellido_p,apellido_m,curso,rut,clave,fecha) value ('" + nombre + "','" + apellido_p + "','" + apellido_m + "','" + curso + "','" + rut + "','" + clave + "'," + fecha + ")"), conn);
            cmd.ExecuteNonQuery();
        }

        public void InsertarDatosProfesor(String nombre, String apellido_p, String apellido_m, String rut, String clave)
        {
            cmd = new MySqlCommand(String.Format("INSERT INTO profesor(nombre,apellido_p,apellido_m,rut,clave) value ('" + nombre + "','" + apellido_p + "','" + apellido_m + "','" + rut + "','" + clave + "')"), conn);
            cmd.ExecuteNonQuery();
        }
        public void InsertarCursoProfesor(int fecha, String rut, String curso)
        {
            cmd = new MySqlCommand(String.Format("INSERT INTO curso_profesor(fecha,rut,curso) VALUE(" + fecha + ",'" + rut + "','" + curso + "')"), conn);
            cmd.ExecuteNonQuery();
        }

        public void InsertarDiagnostico(String codigo, DateTime fecha, String rut_p, String rut_a)
        {
            cmd = new MySqlCommand(String.Format("INSERT INTO diagnostico(codigo,fecha,rut_p,rut_a) VALUE ('" + codigo + "','" + fecha + "','" + rut_p + "','" + rut_a + "')"), conn);
            cmd.ExecuteNonQuery();
        }

        public void InsertarResultadosAlumnoDiagnostico(String codigo, DateTime fecha, String rut_p, String rut_a, int h1_c, int h1_i, int h2_c, int h2_i, String nivel_logro_h1, String nivel_logro_h2, String modulo) //Se insertan datos de diagnostico
        {
            cmd = new MySqlCommand(String.Format("INSERT INTO diagnostico(codigo,fecha,rut_p,rut_a) VALUE (" + codigo + ",'" + fecha + "','" + rut_p + "','" + rut_a + "')"), conn);
            cmd.ExecuteNonQuery();
            cmd = new MySqlCommand(String.Format("INSERT INTO resultado_diag(codigo,h1_c,h1_i,h2_c,h2_i,nivel_logro_h1,nivel_logro_h2,modulo) VALUE (" + codigo + "," + h1_c + "," + h1_i + "," + h2_c + "," + h2_i + ",'" + nivel_logro_h1 + "','" + nivel_logro_h2 + "','" + modulo + ")"), conn);
            cmd.ExecuteNonQuery();
        }
        public void InsertarDatosModuloAlumno(String codigo, String fecha, String Modulo, String rut_p, String rut_a, String nombre_modulo) //Se insertan los resultaos de diagnostico
        {
            cmd = new MySqlCommand(String.Format("INSERT INTO modulo(codigo,fecha,modulo,rut_p,rut_a,nombre_modulo) VALUE('" + codigo + "'," + fecha + ",'" + Modulo + "','" + rut_p + "','" + rut_a + "','" + nombre_modulo + "')"), conn);
            cmd.ExecuteNonQuery();

        }
        public void InsertarNivLogroAlumno(String nivel_logro_modulo)
        {
            cmd = new MySqlCommand(String.Format("INSERT INTO modulo(nivel_logro_modulo) VALUE ('nivel_logro_modulo')"), conn);
            cmd.ExecuteNonQuery();
        }

        public void InsertarActividadesModeloAlumno(String codigo, int num_actividad, String niv_logro_act)
        {
            cmd = new MySqlCommand(String.Format("INSERT INTO resultado_modulo(codigo,num_actividad,niv_logro_act) VALUE('" + codigo + "'," + num_actividad + ",'" + niv_logro_act + "')"), conn);
            cmd.ExecuteNonQuery();
        }

        //Si el alumno esta registrado o no
        public Boolean ComprobarRegistro(String rut, string clave)
        {
            cmd = new MySqlCommand(String.Format("SELECT rut,clave FROM alumno where rut='" + rut + "' and clave='" + clave + "'"), conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader.Read(); ;
        }

        // Si el alumno realizo la evaluacion de diagnostico
        public Boolean diagnosticoRealizado(string rut)
        {
            cmd = new MySqlCommand(String.Format("SELECT codigo FROM alumno, diagnostico WHERE alumno.rut = '" + rut + "' AND alumno.rut = 'diagnostico.rut_a'"), conn);
            MySqlDataReader query = cmd.ExecuteReader();

            return !query.Read();

        }

        // Resultado del diagnostico dado por el alumno
        public string resultadoDiagnostico(string rut)
        {
            cmd = new MySqlCommand(String.Format("SELECT modulo FROM alumno, diagnostico, resultado_diag WHERE alumno.rut = '" + rut + "' AND alumno.rut = diagnostico.rut_a AND diagnostico.codigo = resultado_diag.codigo"), conn);

            MySqlDataReader query = cmd.ExecuteReader();
            String modulo = null;
            while (query.Read())
            {
                modulo = query.GetString(0);
                Console.WriteLine(modulo);
            }

            return modulo;
        }

        // Seleccionar todos los modulos que ha realizado el alumno
        public string[] SeleccionarTodosLosModulosRealizados(string rut)
        {
            cmd = new MySqlCommand(String.Format("SELECT nombre_modulo, nivel_logro_modulo FROM alumno, modulo WHERE alumno.rut = '" + rut + "'  AND alumno.rut = modulo.rut_a"), conn);
            MySqlDataReader query = cmd.ExecuteReader();
            string mRealizado = null;
            string nivelLogro = null;
            while (query.Read())
            {
                mRealizado = (string)query[0];
                nivelLogro = (string)query[1];
            }
            string[] modulo = { mRealizado, nivelLogro };

            return modulo;
        }

        // Seleccionar el nombre, apellidos y curso del alumno
        public string[] SeleccionarAlumno(string rut)
        {
            cmd = new MySqlCommand(String.Format("SELECT nombre, apellido_p, apellido_m, curso FROM alumno WHERE alumno.rut = '" + rut + "'"), conn);
            MySqlDataReader query = cmd.ExecuteReader();
            string nombre = null;
            string curso = null;
            while (query.Read())
            {
                nombre = (string)query[0];
                nombre = nombre + " " + (string)query[1];
                nombre = nombre + " " + (string)query[2];
                curso = (string)query[3];
            }
            string[] alumno = { nombre, curso };
            return alumno;
        }

        // Seleccionar profesor de un alumno en especifico
        public string SeleccionarProfesorDeAlumno(string rut)
        {
            cmd = new MySqlCommand(String.Format("SELECT P.nombre, P.apellido_p, P.apellido_m FROM alumno, profesor as P, curso_profesor WHERE alumno.rut = '" + rut + "' AND alumno.curso = curso_profesor.curso AND curso_profesor.rut = P.rut"), conn);
            MySqlDataReader query = cmd.ExecuteReader();
            string nombre = null;
            while (query.Read())
            {
                nombre = (string)query[0];
                nombre = nombre + " " + (string)query[1];
                nombre = nombre + " " + (string)query[2];
            }
            return nombre;
        }

        // Seleccionar cantidad de respuestas correctas e incorrectas en las habilidades de diagnostico
        public List<Object> SeleccionarResultadosDiagnostico(string rut)
        {
            cmd = new MySqlCommand(String.Format("SELECT h1_c, h1_i, h2_c, h2_i FROM alumno, diagnostico, resultado_diag WHERE alumno.rut = '" + rut + "' AND alumno.rut = diagnostico.rut_a AND diagnostico.codigo = resultado_diag.codigo"), conn);
            MySqlDataReader query = cmd.ExecuteReader();
            List<Object> lista = new List<Object>();
            int resultado = -1;
            query.Read();
            resultado = query.GetInt32(0);
            lista.Add(resultado);
            resultado = query.GetInt32(1);
            lista.Add(resultado);
            resultado = query.GetInt32(2);
            lista.Add(resultado);
            resultado = query.GetInt32(3);
            lista.Add(resultado);
            return lista;
        }

        // Seleccionar nivel de logro de cada habilidad del diagnostico
        public List<string> SeleccionarNivelHabilidad(string rut)
        {
            cmd = new MySqlCommand(String.Format("SELECT nivel_logro_h1, nivel_logro_h2, modulo FROM alumno, diagnostico, resultado_diag WHERE alumno.rut = '" + rut + "' AND alumno.rut = diagnostico.rut_a AND diagnostico.codigo = resultado_diag.codigo"), conn);
            MySqlDataReader query = cmd.ExecuteReader();
            query.Read();
            List<string> resultados = new List<string>();
            resultados.Add((string)query[0]);
            resultados.Add((string)query[1]);
            resultados.Add((string)query[2]);
            return resultados;
        }

        // Seleccionar todas las actividades realizadas en un modulo
        public List<Object> SeleccionarActRealizadasEnModulo(string rut, int codigo)
        {
            cmd = new MySqlCommand(String.Format("SELECT num_actividad FROM alumno, modulo, resultado_modulo WHERE alumno.rut = '" + rut + "' AND alumno.rut = modulo.rut_a AND modulo.codigo = " + codigo + ""), conn);
            MySqlDataReader query = cmd.ExecuteReader();
            List<Object> lista = new List<Object>();
            int resultado = -1;
            while (query.Read())
            {
                resultado = query.GetInt32(0);
                lista.Add(resultado);
            }
            return lista;
        }

        // Seleccionar las habilidades de un modulo
        public List<string> SeleccionarHabilidadesEnModulo(string rut, int codigo)
        {
            cmd = new MySqlCommand(String.Format("SELECT habilidad FROM alumno, modulo, resultado_modulo WHERE alumno.rut = '" + rut + "' AND alumno.rut = modulo.rut_a AND modulo.codigo = " + codigo + ""), conn);
            MySqlDataReader query = cmd.ExecuteReader();
            List<string> lista = new List<string>();
            while (query.Read())
            {
                lista.Add((string)query[0]);
            }
            return lista;
        }

        // Seleccionar todos los nivel de logro de las actividades de un modulo
        public List<string> SeleccionarNivelLogroAct(string rut, int codigo)
        {
            cmd = new MySqlCommand(String.Format("SELECT nivel_logro_act FROM alumno, modulo, resultado_modulo WHERE alumno.rut = '" + rut + "' AND alumno.rut = modulo.rut_a AND modulo.codigo = " + codigo + ""), conn);
            MySqlDataReader query = cmd.ExecuteReader();
            List<string> lista = new List<string>();
            while (query.Read())
            {
                lista.Add((string)query[0]);
            }
            return lista;
        }

        // Seleccionar los codigos de diagnostico para obtener el ultimo
        public int SeleccionarUltimoCodigoD()
        {
            cmd = new MySqlCommand(String.Format("SELECT codigo from diagnostico"), conn);
            MySqlDataReader query = cmd.ExecuteReader();
            string codigo = null;
            while (query.Read())
            {
                codigo = (string)query[0];
            }
            int c = Convert.ToInt32(codigo);
            c++;
            return c;
        }

        // Seleccionar los codigos de dmodulo para obtener el ultimo
        public int SeleccionarUltimoCodigoModulo()
        {
            cmd = new MySqlCommand(String.Format("SELECT codigo from modulo"), conn);
            MySqlDataReader query = cmd.ExecuteReader();
            string codigo = null;
            while (query.Read())
            {
                codigo = (string)query[0];
            }
            int c = Convert.ToInt32(codigo);
            c++;
            return c;
        }
    }
}
