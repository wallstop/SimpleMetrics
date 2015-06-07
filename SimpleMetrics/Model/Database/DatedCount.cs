using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMetrics.Model.Database
{
    [Table("DatedCount")]
    public class DatedCount
    {
        [Key]
        public string TimeStamp { get; set; }

        public string Name { get; set; }

        [Column("Count")]
        public double CountValue { get; set; }

        [ForeignKey("Name")]
        public Count Count { get; set; }
    }
}