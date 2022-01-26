namespace Models;

public class ProfileModel
{
    public bool DoesUserOwnProfile {get;set;}
    public UserModel User {get;set;}
    public List<AlbumModel>? AlbumList {get;set;} 
    public List<PhotoModel>? PhotoList {get;set;} 
    public List<FriendsModel>? FriendsList {get;set;} 

}