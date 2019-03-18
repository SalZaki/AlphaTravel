namespace Alpha.Travel.Common
{
    using System;

    public class TimeProvider : ITimeProvider
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}