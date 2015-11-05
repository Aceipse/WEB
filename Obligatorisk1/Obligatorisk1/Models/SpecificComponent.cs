using System.ComponentModel.DataAnnotations;

namespace Obligatorisk1.Models
{
    public class SpecificComponent
    {
        [Key]
        public int Id { get; set; }
        public int ComponentNumber { get; set; }
        public string SerieNr { get; set; }
        public LoanInformation LoanInformation { get; set; }
    }
}