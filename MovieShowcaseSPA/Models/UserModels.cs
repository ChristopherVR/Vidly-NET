namespace MoveShowcaseDDD.Models;
public record UserLoginPost(string Username, string Password);
public record UserDTO(
    string Username,
    string Password,
    string Name,
    string Surname,
    string HomeNumber,
    string PhoneNumber,
    string Address,
    Uri? ImageUrl);

public record UserPreviewPatch(string Name, string Surname, string HomeNumber, string PhoneNumber, string Address, Uri? ImageUrl);
