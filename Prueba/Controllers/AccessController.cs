using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Prueba.Models;
using Prueba.Models.ViewModels;

namespace Prueba.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Login()
        {
            // Devuelve la vista asociada con esta acción.
            return View();
        }

        // Método ActionResult llamado Enter que se invoca en la acción POST y toma dos parámetros: user y pass.
        public ActionResult Enter(string user, string pass)
        {
            try
            {
                // Intenta realizar la autenticación del usuario utilizando un procedimiento almacenado.
                using (RECIPTSEntities1 db = new RECIPTSEntities1())
                {
                    // Ejecuta un procedimiento almacenado llamado ValidateUser con los parámetros proporcionados.
                    var result = db.Database.SqlQuery<Users>("EXEC ValidateUser @Email, @Password",
                                new SqlParameter("@Email", user),
                                new SqlParameter("@Password", pass)).SingleOrDefault();

                    // Verifica si se encontró un usuario con las credenciales proporcionadas.
                    if (result != null)
                    {
                        // Almacena el objeto Users en la sesión para mantener la autenticación.
                        Session["User"] = result;

                        // Devuelve el código "1" como indicador de autenticación exitosa.
                        return Content("1");
                    }
                    else
                    {
                        // Devuelve un mensaje indicando que el usuario no fue encontrado.
                        return Content("User not found");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: Devuelve un mensaje de error si ocurre alguna excepción durante la autenticación.
                return Content("An error occurred. Please check the details: " + ex.Message);
            }
        }

        // Método ActionResult llamado SignIn que se invoca en la acción GET.
        [HttpGet]
        public ActionResult SignIn()
        {
            // Devuelve la vista asociada con esta acción.
            return View();
        }

        // Método ActionResult llamado SignIn que se invoca en la acción POST y toma un parámetro de modelo (model).
        [HttpPost]
        public ActionResult SignIn(UserViewModel model)
        {
            // Verifica si el modelo pasado como parámetro es válido.
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, devuelve la vista con el modelo para mostrar los errores de validación.
                return View(model);
            }

            // Intenta agregar un nuevo usuario utilizando un procedimiento almacenado.
            using (var db = new RECIPTSEntities1())
            {
                // Define los parámetros para el procedimiento almacenado AddUser.
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Email", model.Email),
                    new SqlParameter("@Password", model.Password)
                };

                // Ejecuta el procedimiento almacenado AddUser con los parámetros proporcionados.
                db.Database.SqlQuery<UserViewModel>("EXEC AddUser @Name, @Email, @Password", parameters).ToList();

                // Guarda los cambios en la base de datos.
                db.SaveChanges();
            }

            // Redirige al usuario a la página de inicio de sesión después de registrar un nuevo usuario.
            return Redirect(Url.Content("~/Access/Login"));
        }
    }
}