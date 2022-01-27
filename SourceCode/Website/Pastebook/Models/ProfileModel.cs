namespace Models;

public class ProfileModel
{
    public bool DoesUserOwnProfile {get;set;}
    public bool IsUserAFriend {get;set;}
    public UserModel? User {get;set;}
    public AlbumModel? Album {get;set;}
    public PhotoModel? Photo {get;set;}
    public FriendsModel? Friends {get;set;}
    public List<AlbumModel>? AlbumList {get;set;} 
    public List<PhotoModel>? PhotoList {get;set;} 
    public List<FriendsModel>? FriendsList {get;set;} 

}