using System;
using System.Runtime.Serialization;
using System.Threading;

namespace SimpleMetrics.Model.TimeStamped
{
    [DataContract]
    [Serializable]
    public class StampedCount : AbstractTimeStamped
    {
        private readonly ReaderWriterLockSlim lock_;
        [DataMember(Name = "Count")] private double count_;

        public double Count
        {
            get
            {
                lock_.EnterReadLock();
                try
                {
                    return count_;
                }
                finally
                {
                    lock_.ExitReadLock();
                }
            }
        }

        public StampedCount(double amount)
        {
            count_ = amount;
            lock_ = new ReaderWriterLockSlim();
        }

        public void Add(double amount)
        {
            lock_.EnterWriteLock();
            count_ += amount;
            lock_.ExitWriteLock();
        }
    }
}