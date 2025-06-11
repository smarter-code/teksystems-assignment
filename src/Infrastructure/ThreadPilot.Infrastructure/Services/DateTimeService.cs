using ThreadPilot.Application.Common.Interfaces;

namespace ThreadPilot.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}