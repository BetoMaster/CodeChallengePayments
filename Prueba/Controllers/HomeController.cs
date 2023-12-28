using Prueba.Models;
using Prueba.Models.TableViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Prueba.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Lista para almacenar los resultados de la consulta
            List<UserTableViewModel> lst = null;

            try
            {
                // Obtener el UserID del usuario actualmente autenticado desde la sesión
                int userId = 0;
                var user = Session["User"] as Users;

                // Verificar si se obtuvo correctamente un objeto Users de la sesión.
                if (user != null)
                {
                    // Asignar el valor del UserID del objeto Users a la variable userId.
                    userId = user.UserID;
                }
                else
                {
                    ViewBag.ErrorMessage = "User not authenticated";
                    return View(lst);
                }

                using (RECIPTSEntities1 db = new RECIPTSEntities1())
                {
                    SqlParameter parameterUserId = new SqlParameter("@UserID", userId);

                    lst = db.Database.SqlQuery<UserTableViewModel>("EXEC GetPaymentsByUserID @UserID", parameterUserId).ToList();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error trying to get payments: " + ex.Message;
            }

            // Retornar la vista con la lista de resultados
            return View(lst);
        }
    }

}