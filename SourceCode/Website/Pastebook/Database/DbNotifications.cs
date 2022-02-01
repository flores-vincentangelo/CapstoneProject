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

    public static void InitializeNotifications(int userId)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"INSERT INTO Notifications (UserId) VALUES (@userid);";
                cmd.Parameters.AddWithValue("@userid",userId);
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Entry added to Notifications Table");
            }
        }
    }
    public static void InsertUserIntoFriendReqNotifOfOtherUser(int sentFriendReqId, int recieveFriendReqId)
    {
        var notifData = GetNotificationsByUserId(recieveFriendReqId);
        string finalList = AddIdToList(sentFriendReqId,notifData["FriendRequests"]);
        UpdateFriendsColumn(recieveFriendReqId,finalList);
    }

    public static void InsertUserIntoLikesNotifOfOtherUser(int sentLikeId, int? recieveLikeId)
    {
        var notifData = GetNotificationsByUserId(recieveLikeId);
        string finalList = AddIdToList(sentLikeId,notifData["Likers"]);
        UpdateLikesColumn(recieveLikeId, finalList);
    }

    public static void InsertUserIntoCommentsNotifOfOtherUser(int? commenterId, int? commentedId)
    {
        var notifData =GetNotificationsByUserId(commentedId);
        string finalList = AddIdToList(commenterId, notifData["Commenters"]);
        UpdateCommentsColumns(commentedId, finalList);
    }

    public static void UpdateFriendsColumn(int userId, string friendsData)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"UPDATE Notifications
                    SET FriendRequest = @friendrequest
                    WHERE UserId = @userId;";
                cmd.Parameters.AddWithValue("@friendrequest",friendsData);
                cmd.Parameters.AddWithValue("@userId",userId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateLikesColumn(int? userId, string likesData)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText =
                    @"UPDATE Notifications
                    SET LikesOnPost = @likes
                    WHERE UserId = @userid;";
                cmd.Parameters.AddWithValue("@likes", likesData);
                cmd.Parameters.AddWithValue("@userid",userId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateCommentsColumns(int? userId, string commentsData)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"UPDATE Notifications
                    SET CommentOnPost = @comment
                    WHERE UserId = @userid;";
                cmd.Parameters.AddWithValue("@comment",commentsData);
                cmd.Parameters.AddWithValue("@userid", userId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static Dictionary<string,string>? GetNotificationsByUserId(int? userId)
    {
        Dictionary<string, string> notifObj = new Dictionary<string, string>();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"SELECT * FROM Notifications WHERE UserId = @userid;";
                cmd.Parameters.AddWithValue("@userid",userId);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
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
    
    public static List<UserModel>? GetUserListByUserId(string userIdListStr)
    {
        if(String.IsNullOrEmpty(userIdListStr)) return null;
        else
        {
            string[] userIdArr = userIdListStr.Split(',');
            List<UserModel> userList = new List<UserModel>();
            foreach (string userIdStr in userIdArr)
            {
                int userId = Int32.Parse(userIdStr);
                UserModel user = DbUsers.GetUserById(userId);
                userList.Add(user);
            }
            return userList;
        }
       
    }
    public static void DeleteNotificationsByUserId(int? userId)
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
                    WHERE UserId = @userid;";
                cmd.Parameters.AddWithValue("@null", DBNull.Value);
                cmd.Parameters.AddWithValue("@userid",userId);
                cmd.ExecuteNonQuery();
            }
        }
    }
    public static string AddIdToList(int? userId, string? idListStr)
    {
        if(String.IsNullOrEmpty(idListStr)){
            return userId.ToString();
        }
        else
        {
            var idListArr = idListStr.Split(',');
            List<string> idList = new List<string>(idListArr);
            idList.Add(userId.ToString());
            return String.Join(",",idList);
        }
    }
}