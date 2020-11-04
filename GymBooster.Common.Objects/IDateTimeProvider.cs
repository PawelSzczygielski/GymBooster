using System;

namespace GymBooster.Common.Objects
{
    public interface IDateTimeProvider
    {
        DateTime CurrentUtc { get; }
    }
}
