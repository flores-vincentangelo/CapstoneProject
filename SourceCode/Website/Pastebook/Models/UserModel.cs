namespace Models;

public class UserModel
{
    public int UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? MobileNumber { get; set; }
    public string? Password { get; set; }
    public string? NewPassword { get; set; }
    public long Birthday { get; set; }
    public string? ReadableBirthday { get; set; }
    public string? Gender { get; set; }
    public string? FullName { get; set; }
    public int Duplicate { get; set; }
    public string? ProfileLink { get; set; }
    public string? ProfileName { get; set; }
    public string? About {get;set;}
    public string? Photo {get;set;}
    public string? Cover {get;set;}
}