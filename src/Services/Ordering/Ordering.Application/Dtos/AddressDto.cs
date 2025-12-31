namespace Ordering.Application.Dtos;

public record AddressDto(
    string FristName,
    string LastName,
    string EmailAddress,
    string Addressline,
    string Country,
    string State,
    string ZipCode
    );
