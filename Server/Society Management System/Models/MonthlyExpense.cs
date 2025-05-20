using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Society_Management_System.Models
{
    public class MonthlyExpense
    {
        [Key]
        public int Id {  get; set; }
        public int Amount { get; set; }
        public int Month {  get; set; }
        public int Year {  get; set; }
        public string Description {  get; set; }
        public int ExpenseDate {  get; set; }
        public int CategoryId { get; set; }

        
       

    }
}
