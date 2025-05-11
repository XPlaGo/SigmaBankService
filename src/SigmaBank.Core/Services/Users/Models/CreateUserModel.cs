namespace SigmaBank.Core.Services.Users.Models;

public record CreateUserModel(
    string PhoneNumber,
    string FirstName,
    string LastName,
    int Age);