using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MotoresBD.Clases
{
    class ClsEnviarCorreo
    {
        public void EnviarCorreo()
        {


            SqlConnection conn = new SqlConnection("Data Source = DESKTOP-03L4M4P\\SQLEXPRESS; Initial Catalog = Alumnos; Integrated Security = True");


            //Creando la consulta para obtener los correos necesarios
            string Consulta = " Select Correo from TbAlumnos where Promedio2 = 'Abajo de zona Minina'";
            conn.Open();
            SqlCommand comando = new SqlCommand(Consulta, conn);
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            data.Fill(tabla);
            conn.Close();


            //Convirtiendo los correos de datatable a un array
            string[] Datos = new string[tabla.Rows.Count];
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                //- Guardar la Columna Nombre el el Arreglo
                Datos[i] = tabla.Rows[i]["Correo"].ToString();
            }

            //Enviando el correo
            for (int i = 0; i < Datos.Length - 1; i++)
            {
                MailMessage VariableCorreo = new MailMessage();
                VariableCorreo.From = new MailAddress("annercruz@hotmail.es", "Prueeba-UMG", System.Text.Encoding.UTF8);//Correo de salida
                VariableCorreo.To.Add(Datos[i]); //Correo destino?
                VariableCorreo.Subject = "NOTIFICACION IMPORTANTE"; //Asunto
                VariableCorreo.Body = "Este es un correo de prueba enviado desde c#. Por medio del cual se te notifica que no has llegado a zona minima"; //Mensaje del correo
                VariableCorreo.IsBodyHtml = true;
                VariableCorreo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp-mail.outlook.com "; //Host del servidor de correo
                smtp.Port = 587; //Puerto de salida
                smtp.Credentials = new System.Net.NetworkCredential("annercruz@hotmail.es", "62493SoyAgente");//Cuenta de correo
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;//True si el servidor de correo permite ssl
                smtp.Send(VariableCorreo);
            }

        }

    }
}
