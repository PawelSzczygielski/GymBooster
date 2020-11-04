using System;
using GymBooster.Common.Objects;

namespace GymBooster.Api.IntegrationTests.Infrastructure
{
    public class TestDateTimeProvider : IDateTimeProvider
    {
        public TestDateTimeProvider(DateTime currentUtc)
        {
            CurrentUtc = currentUtc;
        }

        public DateTime CurrentUtc { get; }
    }
}