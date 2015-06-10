using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SimpleMetrics.Model.Database;
using SimpleMetrics.Model.TimeStamped;

namespace SimpleMetrics.Model
{
    [DataContract]
    [Serializable]
    public class Metrics : IDisposable
    {
        [DataMember(Name = "Application")] private string application_;
        [DataMember(Name = "Counts")] private ConcurrentDictionary<string, StampedCount> counts_;
        [DataMember(Name = "TimeSpans")] private ConcurrentDictionary<string, StampedDuration> durations_;
        [DataMember(Name = "Operation")] private string operation_;

        public string Operation
        {
            get { return operation_; }
            set { operation_ = value; }
        }

        public string Application
        {
            get { return application_; }
            set { application_ = value; }
        }

        public IEnumerable<DatedCount> Counts
        {
            get
            {
                Application application = new Application {Name = Application};
                Operation operation = new Operation
                {
                    Name = Operation,
                    Application = application,
                    ApplicationRef = Application
                };
                List<DatedCount> counts = new List<DatedCount>(counts_.Count);
                counts.AddRange(from count in counts_
                    let dbCount = new Count {Name = count.Key, Operation = operation, OperationRef = operation.Name}
                    select new DatedCount
                    {
                        Count = dbCount,
                        CountValue = count.Value.Count,
                        Name = dbCount.Name,
                        TimeStamp = FormatTimeStamp(count.Value.TimeStamp)
                    });

                return counts;
            }
        }

        public IEnumerable<DatedDuration> Durations
        {
            get
            {
                Application application = new Application {Name = Application};
                Operation operation = new Operation
                {
                    Name = Operation,
                    Application = application,
                    ApplicationRef = Application
                };
                List<DatedDuration> durations = new List<DatedDuration>(durations_.Count);
                durations.AddRange(from duration in durations_
                    let dbDuration =
                        new Duration {Name = duration.Key, Operation = operation, OperationRef = operation.Name}
                    select new DatedDuration
                    {
                        Duration = dbDuration,
                        DurationValue = duration.Value.Duration.TotalMilliseconds,
                        Name = dbDuration.Name,
                        TimeStamp = FormatTimeStamp(duration.Value.TimeStamp)
                    });

                return durations;
            }
        }

        public Metrics()
        {
            counts_ = new ConcurrentDictionary<string, StampedCount>();
            durations_ = new ConcurrentDictionary<string, StampedDuration>();
        }

        public void Dispose()
        {
            // TODO
            throw new NotImplementedException();
        }

        public static string FormatTimeStamp(DateTime instant)
        {
            return instant.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public void AddCount(string name, double amount)
        {
            StampedCount existingCount = counts_.GetOrAdd(name, new StampedCount(0));
            existingCount.Add(amount);
        }

        public void AddTime(string name, TimeSpan duration)
        {
            StampedDuration existingDuration = durations_.GetOrAdd(name, new StampedDuration(TimeSpan.Zero));
            existingDuration.Add(duration);
        }
    }
}