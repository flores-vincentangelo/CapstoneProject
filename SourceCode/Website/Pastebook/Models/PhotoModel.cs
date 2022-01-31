namespace Models;

public class PhotoModel
{
    public int PhotoId {get;set;}
    public string? UserEmail {get;set;}
    public string? Photo {get;set;}
    public long UploadDate {get;set;}
    public int AlbumId {get;set;}
    public int UserId {get;set;}
    public int PostId {get;set;}
    public string? ProfileLink {get;set;}
    public string? Likes {get;set;}
    public string? Comments {get;set;}
    
}