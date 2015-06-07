using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMetrics.Model.Database
{
    [Table("Duration")]
    public class Duration
    {
        [Key]
        public string Name { get; set; }

        [Column("Operation")]
        public string OperationRef { get; set; }

        [ForeignKey("OperationRef")]
        public Operation Operation { get; set; }
    }
}