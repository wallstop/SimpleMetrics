using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        [DataMember(Name = "Operation")] private string operation_;
        [DataMember(Name = "TimeSpans")] private ConcurrentDictionary<string, StampedDuration> timeSpans_;

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
            get { return null /* TODO */; }
        }

        public IEnumerable<DatedDuration> Durations
        {
            get { return null /* TODO */; }
        }

        public Metrics()
        {
            counts_ = new ConcurrentDictionary<string, StampedCount>();
            timeSpans_ = new ConcurrentDictionary<string, StampedDuration>();
        }

        public void Dispose()
        {
            // TODO
            throw new NotImplementedException();
        }

        public void AddCount(string name, double amount)
        {
            StampedCount existingCount = counts_.GetOrAdd(name, new StampedCount(0));
            existingCount.Add(amount);
        }

        public void AddTime(string name, TimeSpan duration)
        {
            StampedDuration existingDuration = timeSpans_.GetOrAdd(name, new StampedDuration(TimeSpan.Zero));
            existingDuration.Add(duration);
        }
    }
}