namespace Models;

public class FriendsModel
{
    public string? UserEmail {get;set;}
    public string? FriendsList {get;set;}
    public string? FriendRequests {get;set;}
    public List<UserModel>? FriendsObjList {get;set;}
    public List<UserModel>? FriendRequestObjList {get;set;}
    
}