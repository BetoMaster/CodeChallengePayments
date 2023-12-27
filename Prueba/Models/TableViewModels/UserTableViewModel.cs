using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba.Models.TableViewModels
{
    public class UserTableViewModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}