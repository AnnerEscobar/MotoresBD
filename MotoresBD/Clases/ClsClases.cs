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
        public void Consultas(string consulta, SqlConnection conexion, DataGridView dataGridView1)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand(consulta, conexion);
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            data.Fill(tabla);
            dataGridView1.AutoResizeColumns();
            dataGridView1.DataSource = tabla;
            conexion.Close();

        }
    }
}
