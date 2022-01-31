namespace Models;

public class FriendsModel
{
    public int? UserId {get;set;}
    public string? FriendsList {get;set;}
    public string? FriendRequests {get;set;}
    public List<UserModel>? FriendsObjList {get;set;}
    public List<UserModel>? FriendRequestObjList {get;set;}
    public string? ConfirmFriendReqOf {get;set;}
    public string? DeleteFriendReqOf {get;set;}
    
}