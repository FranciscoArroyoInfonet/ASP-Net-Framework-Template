using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Helpers;

namespace ASP
{
    public partial class login : System.Web.UI.Page
    {

        SqlConnection con = new
        SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = ConfigurationManager.AppSettings["tituloPaginas"];

            textoLoginArriba.Text = ConfigurationManager.AppSettings["textoLoginArriba"];
            textoLoginAbajo.Text = ConfigurationManager.AppSettings["textoLoginAbajo"];

            Session.RemoveAll();
            string p = Request.QueryString["pass"];
            string u = Request.QueryString["user"];
            if (p != null && u != null)
            {
                Session["mobileAPP"] = "1";
                loginMethod(u, p);
            }
            else
            {
                Session["mobileAPP"] = "0";
            }  
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            string p = pass.Value;
            string u = user.Value;
            loginMethod(u, p);
        }

        private void loginMethod(string u, string p)
        {
            bool accesoIncorrecto = false;
            string tipo = "";

            try
            {
                con.Open();
                string qry = "select password, id from tblUsuarios where usuario ='" + u + "' and activo = 1";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    if (Crypto.VerifyHashedPassword(String.Format("{0}", sdr[0]), p))
                    {
                        string idUsuario = String.Format("{0}", sdr[1]);
                        Session["id"] = idUsuario;
                        Session["username"] = u;

                        Session["pagina"] = "dash";
                        Response.Redirect("dash.aspx");
                    }
                    else
                    {
                        tipo = "P";
                        accesoIncorrecto = true;
                    }

                }
                else
                {
                    tipo = "U";
                    accesoIncorrecto = true;
                }

                if (accesoIncorrecto)
                {
                    sdr.Close();
                    SqlCommand cmdInsert = new SqlCommand("INSERT INTO tblLogAccesosIncorrectosWeb ([fechaHora], [usuario], [password], [tipo], [IP]) " +
                    "VALUES (GETDATE(), '" + u + "', '"+p+"', '"+tipo+"', '"+getUserIP()+"')", con);
                    cmdInsert.ExecuteNonQuery();
                    string mobileAPP = Session["mobileAPP"].ToString();
                    if (mobileAPP == "1")
                    {
                        Response.Redirect("errorlogin.aspx");
                    }
                    else
                    {
                        etiqueta.Text = "La combinación de usuario y contraseña no es correcta";
                    }
                }

                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public string getUserIP()
        {
            string strIP = String.Empty;
            HttpRequest httpReq = HttpContext.Current.Request;

            if (httpReq.ServerVariables["HTTP_CLIENT_IP"] != null)
            {
                strIP = httpReq.ServerVariables["HTTP_CLIENT_IP"].ToString();
            }
            else if (httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                strIP = httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if
            (
                (httpReq.UserHostAddress.Length != 0)
                &&
                ((httpReq.UserHostAddress != "::1") || (httpReq.UserHostAddress != "localhost"))
            )
            {
                strIP = httpReq.UserHostAddress;
            }
            else
            {
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    strIP = sr.ReadToEnd();
                }
                int i1 = strIP.IndexOf("Address: ") + 9;
                int i2 = strIP.LastIndexOf("</body>");
                strIP = strIP.Substring(i1, i2 - i1);
            }
            return strIP;
        }
    }

    //CREATE TABLES NECESARIOS PARA PONER EN FUNCIONAMIENTO EN CUALQUIER BASE DE DATOS EL LOGIN DE USUARIOS
    /*
        CREATE TABLE [dbo].[tblUsuarios](
        [id] [int] IDENTITY(1,1) NOT NULL,
        [usuario] [nvarchar](50) NOT NULL,
        [password] [nvarchar](250) NOT NULL,
        [activo] [bit] NOT NULL,
        CONSTRAINT [PK_tblUsuarios] PRIMARY KEY CLUSTERED 
        (
        [id] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY]
        GO
    */

    /*
        CREATE TABLE [dbo].[tblLogAccesosIncorrectosWeb](
        [idLog] [int] IDENTITY(1,1) NOT NULL,
        [usuario] [nvarchar](500) NULL,
        [password] [nvarchar](500) NULL,
        [fechaHora] [datetime] NULL,
        [tipo] [nvarchar](50) NULL,
        [IP] [nvarchar](50) NULL,
        CONSTRAINT [PK_tblLogAccesosIncorrectosWeb] PRIMARY KEY CLUSTERED 
        (
        [idLog] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY]
        GO
    */

    /*
        CREATE TABLE [dbo].[tblLogAccesosWeb](
            [idLog] [int] IDENTITY(1,1) NOT NULL,
            [idUsuario] [int] NULL,
            [fechaHora] [datetime] NULL,
            [IP] [nvarchar](50) NULL,
         CONSTRAINT [PK_tblLogAccesosWeb] PRIMARY KEY CLUSTERED 
        (
            [idLog] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY]
        GO
     */
}