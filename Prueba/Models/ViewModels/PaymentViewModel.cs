using System.ComponentModel.DataAnnotations;


namespace Prueba.Models.ViewModels
{
    public class PaymentViewModel
    {

        public int id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public System.DateTime Date { get; set; }
    }
}