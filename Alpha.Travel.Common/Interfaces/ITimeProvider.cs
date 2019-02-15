namespace Alpha.Travel.Common.Interfaces
{
    using System;

    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}