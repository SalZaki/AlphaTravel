namespace Alpha.Travel.Common
{
    using System;

    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}