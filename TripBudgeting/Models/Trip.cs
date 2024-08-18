using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripBudgeting.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        public string Destination { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [NotMapped]
        public int Duration => (EndDate - StartDate).Days + 1;

        public decimal InitialBudget { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
