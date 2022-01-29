namespace Models;

public class ProfileModel
{
    public bool DoesUserOwnProfile {get;set;}
    public bool IsUserInFriendsList {get;set;}
    public bool IsUserInFriendReqList {get;set;}
    public bool IsProfileOwnerInFriendReqList {get;set;}
    public UserModel? User {get;set;}
    public AlbumModel? Album {get;set;}
    public PhotoModel? Photo {get;set;}
    public FriendsModel? Friends {get;set;}
    public PostModel? Posts {get;set;}
    public List<AlbumModel>? AlbumList {get;set;} 
    public List<PhotoModel>? PhotoList {get;set;} 
    public List<UserModel>? FriendsList {get;set;} 
    public List<PostModel>? PostsList {get;set;} 
}