using System;
using System.Runtime.Serialization;

namespace SimpleMetrics.Model.TimeStamped
{
    [DataContract]
    [Serializable]
    public abstract class AbstractTimeStamped
    {
        [DataMember(Name = "TimeStamp")] private readonly DateTime timeStamp_;

        public DateTime TimeStamp => timeStamp_;

        protected AbstractTimeStamped()
        {
            timeStamp_ = DateTime.UtcNow;
        }
    }
}