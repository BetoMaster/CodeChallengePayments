using Prueba.Models;
using Prueba.Models.TableViewModels;
using Prueba.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Prueba.Controllers
{
    public class PaymentController : Controller
    {
        [HttpPost]
        public ActionResult CreatePayment(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Declaración de la variable userId e inicialización con un valor predeterminado de 0.
            int userId = 0;

            // Intento de obtener el objeto Users de la sesión bajo la clave "User".
            var user = Session["User"] as Users;

            // Verificación de si se obtuvo correctamente un objeto Users de la sesión.
            if (user != null)
            {
                // Asignación del valor del UserID del objeto Users a la variable userId.
                userId = user.UserID;
            }
            else
            {
                // Manejo del caso cuando no se encuentra un usuario en la sesión.
                return Content("User not authenticated");
            }

            using (var db = new RECIPTSEntities1())
            {
                SqlParameter[] parameters ={
                    // Asignación del valor de userId al parámetro @UserID.
                    new SqlParameter("@UserID", userId),

                    // Asignación del valor de la propiedad Description del modelo al parámetro @Description.
                    new SqlParameter("@Description", model.Description),

                    // Asignación del valor de la propiedad Amount del modelo al parámetro @Amount.
                    new SqlParameter("@Amount", model.Amount),

                    // Asignación del valor de la propiedad Date del modelo al parámetro @Date.
                    new SqlParameter("@Date", model.Date)
                };

                // Llamada al procedimiento almacenado AddPayment en la base de datos con los parámetros especificados.
                db.Database.SqlQuery<UserViewModel>("EXEC AddPayment @UserID, @Description, @Amount, @Date", parameters).ToList();

                // Confirmación de los cambios en la base de datos.
                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Payment/ReadPayments"));
        }


        [HttpGet]
        public ActionResult CreatePayment()
        {
            return View();
        }


        public ActionResult ReadPayments()
        {
            // Lista para almacenar los resultados de la consulta
            List<PaymentTableViewModel> lst = null;

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

                    lst = db.Database.SqlQuery<PaymentTableViewModel>("EXEC GetPaymentsByUserID @UserID", parameterUserId).ToList();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error trying to get payments: " + ex.Message;
            }

            // Retornar la vista con la lista de resultados
            return View(lst);
        }

        public ActionResult Update(int id)
        {
            PaymentViewModel model = new PaymentViewModel();
            using (var db = new RECIPTSEntities1())
            {
                
                var oPayment = db.Payments.Find(id);

                if (oPayment != null)
                {
                    model.Description = oPayment.Description;
                    model.Amount = oPayment.Amount;
                    model.Date = oPayment.Date;
                    model.id = oPayment.PaymentID;
                }
                else
                {
                    return Redirect(Url.Content("~/Home/index"));
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new RECIPTSEntities1())
            {
                // Llamar al procedimiento almacenado para actualizar el pago
                db.Database.ExecuteSqlCommand("EXEC UpdatePayment @PaymentID, @Description, @Amount, @Date",
                    new SqlParameter("@PaymentID", model.id),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Amount", model.Amount),
                    new SqlParameter("@Date", model.Date));
                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Payment/ReadPayments"));
        }

        [HttpPost]
        public ActionResult DeleteSelectedPayments(List<int> paymentIDs)
        {
            try
            {
                // Usar un bloque using para garantizar la liberación de recursos
                using (var db = new RECIPTSEntities1())
                {
                    // Iterar sobre cada ID de pago en la lista
                    foreach (var paymentID in paymentIDs)
                    {
                        // Crear un array de parámetros con el ID del pago
                        var parameters = new SqlParameter[]
                        {
                    new SqlParameter("@PaymentID", paymentID)
                        };

                        // Ejecutar el procedimiento almacenado DeletePaymentByID con el ID del pago como parámetro
                        db.Database.ExecuteSqlCommand("EXEC DeletePaymentByID @PaymentID", parameters);
                    }

                    // Devolver una respuesta JSON indicando el éxito y un mensaje
                    return RedirectToAction("ReadPayments");
                }
            }
            catch (Exception ex)
            {
                // Devolver una respuesta JSON indicando el fallo y un mensaje de error
                return Json(new { success = false, message = "Error al intentar eliminar los pagos: " + ex.Message });
            }
        }
    }

    }
