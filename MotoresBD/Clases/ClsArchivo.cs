using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotoresBD.Clases
{
    class ClsArchivo
    {
        /// <summary> 
        /// Metodo por el cual se carga el archivo a sql server enviandole como parametro del archivo y la ruta de la conexion.
        /// </summary>
        /// <param name="rutaArchivo"></param>
        /// <param name="rutaConexion"></param>
        public void CArgarARchivo(string rutaArchivo, string rutaConexion)
        {
            try
            {
                SqlConnection sqll = new SqlConnection(rutaConexion);
                int contador = 0;
                sqll.Open();
                StreamReader Reader = new StreamReader(rutaArchivo);
                while (!Reader.EndOfStream)
                {
                    var line = Reader.ReadLine();
                    if (contador != 0)
                    {

                        var values = line.Split(';');
                        var sql = "INSERT INTO TbAlumnos VALUES (" + values[0] + ", '" + values[1] + "', " + values[2] + ", " + values[3] + ", " + values[4] + ", '" + values[5] + "')";
                        var cmd = new SqlCommand();
                        cmd.CommandText = sql;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = sqll;
                        cmd.ExecuteNonQuery();
                    }
                    contador++;
                }
                sqll.Close();
                MessageBox.Show(@"ARCHIVOS CARGADOS CORRECTAMENTE A LA DATABASE");
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR AL CARGAR TU ARCHIVO A LA DATABASE");
            }
        }


        /// <summary>
        /// Metodo que Cargando carga el archivo a MySql enviandole como parametro la ruta del archivo y la ruta de conexion a mysql por medio de strings
        /// </summary>
        /// <param name="rutaArchivo"></param>
        /// <param name="rutaConexion"></param>
        public void CArgarARchivoMysql(string rutaArchivo, string rutaConexion)
        {
            try
            {
                MySqlConnection MySql = new MySqlConnection(rutaConexion);
                int contador = 0;
                MySql.Open();
                StreamReader Reader = new StreamReader(rutaArchivo);
                while (!Reader.EndOfStream)
                {
                    var line = Reader.ReadLine();
                    if (contador != 0)
                    {

                        var values = line.Split(';');
                        var sql = "INSERT INTO Alumnos VALUES (" + values[0] + ", '" + values[1] + "', " + values[2] + ", " + values[3] + ", " + values[4] + ", '" + values[5] + "')";
                        var cmd = new MySqlCommand();
                        cmd.CommandText = sql;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = MySql;
                        cmd.ExecuteNonQuery();
                        
                    }
                    contador++;
                }
                MySql.Close();
                MessageBox.Show(@"ARCHIVOS CARGADOS CORRECTAMENTE A LA DATABASE");
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR AL CARGAR TU ARCHIVO A LA DATABASE");
            }
        }

    }
}
