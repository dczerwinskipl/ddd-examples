namespace Accounting.Contracts.ValueObjects;

public record PersonalData
{
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
}
