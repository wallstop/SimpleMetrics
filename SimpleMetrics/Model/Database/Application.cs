using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMetrics.Model.Database
{
    [Table("Application")]
    public class Application
    {
        [Key]
        public string Name { get; set; }
    }
}