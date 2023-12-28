using System.Web.Mvc;

namespace Prueba.Controllers
{
    public class LogOutController : Controller
    {
        public ActionResult LogOut()
        {
            // Limpia la sesión eliminando el objeto almacenado bajo la clave "User".
            Session["User"] = null;

            // Redirige al método "Login" del controlador "Access".
            return RedirectToAction("Login", "Access");
        }
    }
}
