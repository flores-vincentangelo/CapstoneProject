namespace Models;

public class PostModel
{
    public string? EmailAddress { get; set; }
    public int PostId { get; set; }
    public long DatePosted { get; set; }
    public string? Caption { get; set; }
    public int PhotoId {get;set;}
    public string? Photo {get;set;}
    public string? Likes {get;set;}
    public string? Comment {get;set;}
    public string? ProfileLink {get;set;}
}