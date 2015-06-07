using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMetrics.Model.Database
{
    [Table("Operation")]
    public class Operation
    {
        [Key]
        public string Name { get; set; }

        [Column("Application")]
        public string ApplicationRef { get; set; }

        [ForeignKey("ApplicationRef")]
        public Application Application { get; set; }
    }
}