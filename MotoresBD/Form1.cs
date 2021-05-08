﻿using MotoresBD.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotoresBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string rutaArchivol = ((@"C:\Users\anner\Desktop\Universidad Mariano Galvez\Tercer Semestre\Programacion 1\datosTexto.csv"));//ruta del archivo
        string rutaConexion = "Data Source = DESKTOP-03L4M4P\\SQLEXPRESS; Initial Catalog = Alumnos; Integrated Security = True";//ruta sql

        string rutaConexionMysql = "Server=localhost;Database=alumnosmysql; Port=3306; Username=root; Password=SoyAgente2341;";//ruta MySql

        ClsArchivo Carga = new ClsArchivo();
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-03L4M4P\\SQLEXPRESS; Initial Catalog = Alumnos; Integrated Security = True");
        ClsClases Consulta = new ClsClases();

        private void button1_Click(object sender, EventArgs e)
        {
            Carga.CArgarARchivo(rutaArchivol, rutaConexion);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Carga.CArgarARchivoMysql(rutaArchivol, rutaConexionMysql);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // BOTON PARA EJECUTAR LA CARGA A ORCLE PENDIETE TODAVIA
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Consulta.Consultas("SELECT * FROM TbAlumnos", conn, dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Consulta.Consultas("SELECT TbAlumnos.Nombre, MAX(TbAlumnos.[Pacial 1]+[Parcial 2]+[Parcial 3]) as [ZONA ACUMULADA], Promedio2 from TbAlumnos group by TbAlumnos.Nombre, TbAlumnos.Promedio2", conn, dataGridView1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Consulta.Consultas("SELECT Nombre,Promedio2 FROM TbAlumnos WHERE Promedio2 = 'Abajo de zona Minina'", conn, dataGridView1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClsEnviarCorreo NuevoCorreo = new ClsEnviarCorreo();
            NuevoCorreo.EnviarCorreo();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string NombreBusqueda = textBox1.Text;
            Consulta.Consultas($"SELECT Nombre, [pacial 1], [Parcial 2],[Parcial 3] FROM TbAlumnos Where Nombre = '{NombreBusqueda}'", conn, dataGridView1);
        }
    }
}
