using System;
using System.Runtime.Serialization;
using System.Threading;

namespace SimpleMetrics.Model.TimeStamped
{
    [DataContract]
    [Serializable]
    public class StampedTimeSpan : AbstractTimeStamped
    {
        private readonly ReaderWriterLockSlim lock_;
        [DataMember(Name = "TimeSpan")] private TimeSpan timeSpan_;

        public TimeSpan TimeSpan
        {
            get
            {
                lock_.EnterReadLock();
                try
                {
                    return timeSpan_;
                }
                finally
                {
                    lock_.ExitReadLock();
                }
            }
        }

        public StampedTimeSpan(TimeSpan duration)
        {
            // Should not be able to concurrently access an object during its creation...
            timeSpan_ = duration;
            lock_ = new ReaderWriterLockSlim();
        }

        public void Add(TimeSpan duration)
        {
            lock_.EnterWriteLock();
            try
            {
                timeSpan_ = timeSpan_.Add(duration);
            }
            finally
            {
                lock_.ExitWriteLock();
            }
        }
    }
}