<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ASP.login" %>
<!DOCTYPE html>
<html lang="en">
  <head runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="icon" type="image/png" href="assets/images/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="login/vendor/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="login/fonts/font-awesome-4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="login/vendor/animate/animate.css">
    <link rel="stylesheet" type="text/css" href="login/vendor/css-hamburgers/hamburgers.min.css">
    <link rel="stylesheet" type="text/css" href="login/vendor/select2/select2.min.css">
    <link rel="stylesheet" type="text/css" href="login/css/util.css">
    <link rel="stylesheet" type="text/css" href="login/css/main.css">
    <style>
          a:hover {
              color: <%= ConfigurationManager.AppSettings["colorPrincipal"] %>;
            }
          .input100:focus + .focus-input100 + .symbol-input100 {
              color: <%= ConfigurationManager.AppSettings["colorPrincipal"] %>;
            }
          .login100-form-btn {
              background: <%= ConfigurationManager.AppSettings["colorPrincipal"] %>;
            }
          .focus-input100 {
              color: <%= ConfigurationManager.AppSettings["colorPrincipalLight"] %>;
            }
      </style> 
  </head>
  <body style="overflow: hidden;">
    <div class="limiter" style="margin-bottom: -50px;">
      <div class="container-login100" style="background:url(assets/images/wall.jpg) !important; margin-bottom: -50px;">
        <div class="wrap-login100" style="align-items: center;">
          <div class="login100-pic js-tilt">
            <span class="login100-form-title" style="color:white; font-size:large;">
              <p style="font-size: 40px; font-weight: bold; line-height: 1.3;">Bienvenid@</p>
              <p style="font-size: 40px; line-height: 1.3;">
                <asp:Label ID="textoLoginArriba" runat="server" Text=""></asp:Label>
              </p>
              <p style="font-size: 40px; line-height: 1.3; font-weight: bold;">
                <asp:Label ID="textoLoginAbajo" runat="server" Text=""></asp:Label>
              </p>
            </span>
          </div>
          <form class="login100-form" runat="server">
            <div class="login100-pic logoLogin">
              <img src="assets/images/logoLogin.png" style="height:200px !important; width:200px !important;" alt="IMG">
            </div>
            <div class="wrap-input100"">
			    <input ID="user" class="input100" type="text" runat="server" name="user" placeholder="Usuario">
              <span class="focus-input100"></span>
              <span class="symbol-input100">
                <i class="fa fa-user" aria-hidden="true"></i>
              </span>
            </div>
            <div class="wrap-input100">
              <input ID="pass" class="input100" type="password" name="pass" runat="server" placeholder="Contraseña">
              <span class="focus-input100"></span>
              <span class="symbol-input100">
                <i class="fa fa-lock" aria-hidden="true"></i>
              </span>
            </div>
            <div class="container-login100-form-btn">
              <asp:Button ID="Button1" runat="server" CssClass="login100-form-btn" Text="Login" OnClick="loginButton_Click" style="border:0px;" />
            </div>
            <div class="text-center p-t-12">
              <span class="txt1"></span>
              <asp:Label ID="etiqueta" CssClass="txt2" runat="server" Text="" style="color:#ff0000;"></asp:Label>
            </div>
          </form>
        </div>
      </div>
        <center>
            <div class="row text-muted">
                <div class="col-12 text-center">
                  <p class="mb-0">
                    <a class="text-muted" href="https://infonetconsultores.com/" target="_blank">
                      <strong>Infonet Consultores
                      </strong>
                    </a> &copy; 
                    <script>document.write(new Date().getFullYear());
                    </script>
                  </p>
                </div>
              </div>
        </center>
    </div>
    <script src="login/vendor/jquery/jquery-3.2.1.min.js"></script>
    <script src="login/vendor/bootstrap/js/popper.js"></script>
    <script src="login/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="login/vendor/select2/select2.min.js"></script>
    <script src="login/vendor/tilt/tilt.jquery.min.js"></script>
    <script>
        $('.js-tilt').tilt({
            scale: 1.1
        })
    </script>
    <script src="login/js/main.js"></script>
  </body>
</html>