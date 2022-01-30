namespace Models;
public class CommentsModel
{
    public UserModel? CommenterUserModel {get;set;}
    public string? CommenterEmail {get;set;}
    public int? PostId {get;set;}
    public int? CommentId {get;set;}
    public string? CommentText {get;set;}
}