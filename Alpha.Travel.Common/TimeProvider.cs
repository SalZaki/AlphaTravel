namespace Alpha.Travel.Common
{
    using System;
    using Interfaces;

    public class TimeProvider : ITimeProvider
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}