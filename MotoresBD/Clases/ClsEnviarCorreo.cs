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

        /// <summary>
        /// Funcion por medio de la cual se envia un correo electronico, extrayendo por medio de una consulta los correos que se necesiten en este caso los de los alumnos que no llegaron azona minima
        /// para notificarles que no tienen derecho a examen
        /// </summary>
        public void EnviarCorreo()
        {

            //Extrayendo los correos hacia un datatable
            SqlConnection sqlconn = new SqlConnection("Data Source = DESKTOP-03L4M4P\\SQLEXPRESS; Initial Catalog = Alumnos; Integrated Security = True");
            string Consulta = " Select Correo from TbAlumnos where Promedio2 = 'Abajo de zona Minina'";
            sqlconn.Open();
            SqlCommand comando = new SqlCommand(Consulta, sqlconn);
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            data.Fill(tabla);
            sqlconn.Close();


            //Extrayendo los correos del datatable y pasandolos a un string
            string[] Correos = new string[tabla.Rows.Count];
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                Correos[i] = tabla.Rows[i]["Correo"].ToString();
            }

            //Metodo para enviar el correo desde mi cuenta de hotmail
            for (int i = 0; i < Correos.Length - 1; i++)
            {
                MailMessage Correo = new MailMessage();
                Correo.From = new MailAddress("annercruz@hotmail.es", "Prueeba-UMG", System.Text.Encoding.UTF8);//Correo de origen
                Correo.To.Add(Correos[i]); //correos de destino
                Correo.Subject = "NOTIFICACION IMPORTANTE"; //Asunto del correo
                Correo.Body = "SE LE NOTIFICA POR ESTE MEDIO QUE USTED NO LLEGO A ZONA MINIMA, DEBE ESFORZARSE EN LA RECUPERACION"; //Mensaje
                Correo.IsBodyHtml = true;
                Correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp-mail.outlook.com "; //Host del servidor de correo
                smtp.Port = 587; //Puerto de salida del correo en outlook
                smtp.Credentials = new System.Net.NetworkCredential("annercruz@hotmail.es", "62493SoyAgente");//Credenciales del ccorreo de origen
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;//True porque el correo si acepta ssl
                smtp.Send(Correo);//enviar correo
            }

        }

    }
}
