using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripBudgeting.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public int Day { get; set; }

        [ForeignKey("Trip")]
        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
