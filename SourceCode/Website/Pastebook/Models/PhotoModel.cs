namespace Models;

public class PhotoModel
{
    public int PhototId {get;set;}
    public string? UserEmail {get;set;}
    public string? Photo {get;set;}
    public long UploadDate {get;set;}
    public int AlbumId {get;set;}
    public string? Likes {get;set;}
    public string? Comments {get;set;}
    
}