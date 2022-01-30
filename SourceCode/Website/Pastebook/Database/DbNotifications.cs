namespace Database;
using System.Data.SqlClient;
using Models;
using Database;

public class DbNotifications
{
    private static string? DB_CONNECTION_STRING;

    static DbNotifications()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    public static void InitializeNotifications(string email)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"INSERT INTO Notifications (UserEmail) VALUES (@email);";
                cmd.Parameters.AddWithValue("@email",email);
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Entry added to Notifications Table");
            }
        }
    }
    public static void InsertUserIntoFriendReqNotifOfOtherUser(string sentFriendReqEmail, string recieveFriendReqEmail)
    {
        string? friendNotif = GetFriendsColumn(recieveFriendReqEmail);
        string finalList = AddEmailToList(sentFriendReqEmail,friendNotif);
        UpdateFriendsColumn(recieveFriendReqEmail,finalList);
    }
    public static string? GetFriendsColumn(string email)
    {
        string? friendList = null;
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"SELECT FriendRequest
                    FROM Notifications
                    WHERE UserEmail = @email;";
                cmd.Parameters.AddWithValue("@email",email);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    friendList = reader.IsDBNull(0) ? null : reader.GetString(0);
                }
            }
        }
        return friendList;
    }
    public static void UpdateFriendsColumn(string userEmail, string friendsData)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"UPDATE Notifications
                    SET FriendRequest = @friendrequest
                    WHERE UserEmail = @email;";
                cmd.Parameters.AddWithValue("@friendrequest",friendsData);
                cmd.Parameters.AddWithValue("@email",userEmail);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static Dictionary<string,string> GetNotificationsByEmail(string email)
    {
        Dictionary<string, string> notifObj = new Dictionary<string, string>();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"SELECT * FROM Notifications WHERE UserEmail = @email;";
                cmd.Parameters.AddWithValue("@email",email);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    string? friendReqList = reader.IsDBNull(1) ? null : reader.GetString(1);
                    string? LikersList = reader.IsDBNull(2) ? null : reader.GetString(2);
                    string? CommentsList = reader.IsDBNull(3) ? null : reader.GetString(3);
                    notifObj.Add("FriendRequests",friendReqList);
                    notifObj.Add("Likers",LikersList);
                    notifObj.Add("Commenters",CommentsList);
                }
                
            }
        }
        return notifObj;
    }
    
    public static List<UserModel>? GetUserListByEmail(string emailList)
    {
        if(String.IsNullOrEmpty(emailList)) return null;
        string[] emailArr = emailList.Split(',');
        List<UserModel> userList = new List<UserModel>();
        foreach (string email in emailArr)
        {
            UserModel user = DbUsers.GetUserByEmail(email);
            userList.Add(user);
        }
        return userList;
    }
    public static void DeleteNotificationsByEmail(string email)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"UPDATE Notifications
                    SET FriendRequest = @null,
                    LikesOnPost = @null,
                    CommentOnPost = @null
                    WHERE UserEmail = @email;";
                cmd.Parameters.AddWithValue("@null", DBNull.Value);
                cmd.Parameters.AddWithValue("@email",email);
                cmd.ExecuteNonQuery();
            }
        }
    }
    public static string AddEmailToList(string email, string? emailList)
    {
        if(String.IsNullOrEmpty(emailList)){
            return email;
        }
        else
        {
            var emailListArr = emailList.Split(',');
            List<string> emailListList = new List<string>(emailListArr);
            emailListList.Add(email);
            return String.Join(",",emailListList);
        }
    }
}