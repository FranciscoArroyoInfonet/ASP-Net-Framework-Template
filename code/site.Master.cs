using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP
{
    public partial class site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = ConfigurationManager.AppSettings["tituloPaginas"];

            //SI NO ESTÁ LOGGEADO VA AL LOGIN
            try
            {
                //EXISTE LA SESIÓN INICIADA PARA EL USUARIO X
                string username = Session["username"].ToString();
                txtUsuario.Text = username.ToUpper();

                //PONGO LOS ACTIVE OK
                string paginaSession = Session["pagina"].ToString();
                if (paginaSession.Equals("dash"))
                {
                    titulo.Text = "Dashboard";
                    dash.Attributes["class"] = "sidebar-item active"; //SE ACTIVA TODO EL LI COMPLETO
                }
            }
            catch (Exception ex)
            {
                //HA CASCADO PORQUE NO ESTÁ EL ID
                Response.Redirect("login.aspx");
            }
        }

        protected void redirect(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            switch (button.ClientID)
            {
                case "dashLink":
                    Session["pagina"] = "dash";
                    Response.Redirect("dash.aspx");
                    break;
                default:
                    Session.RemoveAll();
                    Response.Redirect("login.aspx");
                    break;
            }
        }

    }
}