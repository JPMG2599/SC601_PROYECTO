using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SC601_V1.Models
{
    public class Utilitarios
    {
        public static readonly string RutaProductos = AppDomain.CurrentDomain.BaseDirectory + "Imagenes/Productos\\";
        public static readonly string RutaCategorias = AppDomain.CurrentDomain.BaseDirectory + "Imagenes/Categorias\\";

        public bool EnviarCorreo(string correo, string mensaje, string titulo)
        {
            string cuenta = ConfigurationManager.AppSettings["CorreoNotificaciones"].ToString();
            string contrasenna = ConfigurationManager.AppSettings["ContrasennaNotificaciones"].ToString();

            MailMessage message = new MailMessage();
            message.From = new MailAddress(cuenta);
            message.To.Add(new MailAddress(correo));
            message.Subject = titulo;
            message.Body = mensaje;
            message.Priority = MailPriority.Normal;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.Credentials = new System.Net.NetworkCredential(cuenta, contrasenna);
            client.EnableSsl = true;
            client.Send(message);
            return true;
        }
    }
}