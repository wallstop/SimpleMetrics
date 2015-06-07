using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMetrics.Model.Database
{
    [Table("DatedDuration")]
    public class DatedDuration
    {
        [Key]
        public string TimeStamp { get; set; }

        public string Name { get; set; }

        [Column("Duration")]
        public double DurationValue { get; set; }

        [ForeignKey("Duration")]
        public Duration Duration { get; set; }
    }
}