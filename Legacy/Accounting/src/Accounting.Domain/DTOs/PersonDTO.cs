namespace Accounting.DTOs;

public record PersonDTO
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }

    public PersonDTO Copy() => this with { };
}