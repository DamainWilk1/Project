using System;
using Laboratorium_3___App.Models;

public class DateTimeProviderImplementation : IDateTimeProvider
{
    private DateTime fixedDateTime = new DateTime(2023, 12, 6, 12, 0, 0);

    public DateTime GetDateTime()
    {
        return fixedDateTime;
    }
}
