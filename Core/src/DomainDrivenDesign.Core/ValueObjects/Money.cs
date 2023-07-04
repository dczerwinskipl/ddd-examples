using DomainDrivenDesign.Core.BuildingBlocks;

namespace DomainDrivenDesign.Core.ValueObjects;

public class Money : ValueObject
{
    protected int Amount { get; }

    protected Money(int amount)
    {
        Amount = amount;
    }

    public static Money Zero = new(0);
    public static Money FromDecimal(decimal amount) => new((int)Math.Round(amount * 100, 0, MidpointRounding.AwayFromZero));
    public static implicit operator Money(decimal amount) => FromDecimal(amount);
    public static implicit operator decimal(Money money) => money.Amount / 100M;

    public static Money operator +(Money add) => new(add.Amount);
    public static Money operator +(Money current, Money add) => new(current.Amount + add.Amount);
    public static Money operator -(Money sub) => new(sub.Amount);
    public static Money operator -(Money current, Money sub) => new(current.Amount + sub.Amount);


    public static bool operator >(Money current, Money other) => current.Amount > other.Amount;
    public static bool operator >=(Money current, Money other) => current.Amount >= other.Amount;
    public static bool operator <(Money current, Money other) => current.Amount < other.Amount;
    public static bool operator <=(Money current, Money other) => current.Amount <= other.Amount;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }
}
