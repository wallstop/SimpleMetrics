using System.Data.Entity;

namespace SimpleMetrics.Model.Database
{
    public class MetricsContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Count> Counts { get; set; }
        public DbSet<Duration> Durations { get; set; }
        public DbSet<DatedCount> DatedCounts { get; set; }
        public DbSet<DatedDuration> DatedDurations { get; set; }
    }
}