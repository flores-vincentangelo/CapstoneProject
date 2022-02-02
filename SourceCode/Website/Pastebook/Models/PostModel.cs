namespace Models;

public class PostModel
{
    public int? UserId {get;set;}
    public int? PostId { get; set; }
    public long DatePosted { get; set; }
    public string? Caption { get; set; }
    public int PhotoId {get;set;}
    public string? Photo {get;set;}
    public string? Likes {get;set;}
    public string? Comment {get;set;}
    public string? ProfileLink {get;set;}
    public string? PosterPhoto {get;set;}
    public string? PosterFirstName {get;set;}
    public string? PosterLastName {get;set;}
    public UserModel? Poster {get;set;}
    public string? LikesList {get;set;}
    public string? CommentsList {get; set;}
    public List<CommentsModel>? CommentsListObj {get;set;}
    public List<PostModel>? LikesListObj {get;set;}
    public bool DoesUserLikesAPost {get;set;}
    public bool DoesUserOwnsThePost {get;set;}
    public bool DoesUserOwnProfile {get;set;}
}