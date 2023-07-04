using DomainDrivenDesign.Core.BuildingBlocks;

namespace Accounting.Domain.ValueObjects;

public class SettlementPeriod : ValueObject
{
    protected int Year { get; }
    protected int Month { get; }

    protected SettlementPeriod(int year, int month)
    {
        Year = year;
        Month = month;
    }

    protected SettlementPeriod(DateTime date)
    {
        Year = date.Year;
        Month = date.Month;
    }

    public static implicit operator SettlementPeriod(DateTime date) => new(date);
    public static implicit operator DateTime(SettlementPeriod period) => new(period.Year, period.Month, 1);

    public static implicit operator SettlementPeriod(DateTimeOffset date) => new(date.UtcDateTime);
    public static implicit operator DateTimeOffset(SettlementPeriod period) => new(new DateTime(period.Year, period.Month, 1));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Year;
        yield return Month;
    }

    public SettlementPeriod GetNextPeriod() => new DateTime(Year, Month, 1).AddMonths(1);
    public SettlementPeriod GetPreviousPeriod() => new DateTime(Year, Month, 1).AddMonths(-1);
}
