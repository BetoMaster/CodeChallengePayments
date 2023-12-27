using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba.Models.TableViewModels
{
    public class PaymentTableViewModel
    {
        public int PaymentID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Date { get; set; }

        public virtual Users Users { get; set; }
    }
}