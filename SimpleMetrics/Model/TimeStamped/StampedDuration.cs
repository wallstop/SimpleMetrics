using System;
using System.Runtime.Serialization;
using System.Threading;

namespace SimpleMetrics.Model.TimeStamped
{
    [DataContract]
    [Serializable]
    public class StampedDuration : AbstractTimeStamped
    {
        private readonly ReaderWriterLockSlim lock_;
        [DataMember(Name = "Duration")] private TimeSpan duration_;

        public TimeSpan Duration
        {
            get
            {
                lock_.EnterReadLock();
                try
                {
                    return duration_;
                }
                finally
                {
                    lock_.ExitReadLock();
                }
            }
        }

        public StampedDuration(TimeSpan duration)
        {
            // Should not be able to concurrently access an object during its creation...
            duration_ = duration;
            lock_ = new ReaderWriterLockSlim();
        }

        public void Add(TimeSpan duration)
        {
            lock_.EnterWriteLock();
            try
            {
                duration_ = duration_.Add(duration);
            }
            finally
            {
                lock_.ExitWriteLock();
            }
        }
    }
}