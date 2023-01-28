namespace MovieShowcaseSPA.Models;
public sealed record UserLoginPost(string Username, string Password);
public sealed record UserDTO(
    string Username,
    string Password,
    string Name,
    string Surname,
    string HomeNumber,
    string PhoneNumber,
    string Address,
    Uri? ImageUrl);

public sealed record UserPreviewPatch(string Name, string Surname, string HomeNumber, string PhoneNumber, string Address, Uri? ImageUrl);

public sealed record LoginUser(string Token, string Name, string Surname, string Username, int Id);
