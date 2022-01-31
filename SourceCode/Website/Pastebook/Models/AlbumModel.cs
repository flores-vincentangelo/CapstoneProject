namespace Models;

public class AlbumModel
{
    public int AlbumId {get;set;}
    public string? UserEmail {get;set;}
    public string? AlbumName {get;set;}
    public long CreatedDate {get;set;}
    public string? PhotosList {get;set;}
    public string? ProfileLink {get;set;}
    public int UserId {get;set;}
    
}