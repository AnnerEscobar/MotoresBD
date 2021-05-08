using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotoresBD.Clases
{
    class ClsClases
    {
        /// <summary>
        /// Funcion a traves de las cuales se hacen las cosultas enviandole la consulta en la variable string y la conexion y el lugar donde lo mostrara.
        /// </summary>
        /// <param name="Consulta"></param>
        /// <param name="conexion"></param>
        /// <param name="dataGridView1"></param>
        public void Consultas(string Consulta, SqlConnection conexion, DataGridView dataGridView1)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand(Consulta, conexion);
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            data.Fill(tabla);
            dataGridView1.AutoResizeColumns();
            dataGridView1.DataSource = tabla;
            conexion.Close();
        }
    }
}
