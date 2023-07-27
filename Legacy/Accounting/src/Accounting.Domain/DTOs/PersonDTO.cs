namespace Accounting.DTOs;

public record PersonDTO(Guid Id, string FirstName, string? MiddleName, string LastName, string PhoneNumber)
{
    public PersonDTO Copy() => this with { };
}