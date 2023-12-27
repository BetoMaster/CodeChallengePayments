using Prueba.Models;
using Prueba.Models.TableViewModels;
using Prueba.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prueba.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<UserTableViewModel> lst = null;

            try
            {
                using (RECIPTSEntities1 db = new RECIPTSEntities1())
                {
                    lst = db.Database.SqlQuery<UserTableViewModel>("EXEC GetUsers").ToList();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error trying to get users: " + ex.Message;
            }

            return View(lst);
        }
    }

}