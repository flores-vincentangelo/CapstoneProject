namespace Database;
using System.Data.SqlClient;
using Models;

public class DbFriends
{
    private static string? DB_CONNECTION_STRING;

    static DbFriends()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    public static void InitializeFriends(int userId)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"INSERT INTO Friends (UserId) VALUES (@userid);";
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.ExecuteNonQuery();
                    System.Console.WriteLine("Entry added to Friends Table");
            }
        }
    }
    public static FriendsModel? GetFriendsData(int userId)
    {
        FriendsModel model = new FriendsModel();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT FriendsList, FriendRequests FROM Friends WHERE UserId = @userid;";
                cmd.Parameters.AddWithValue("@userid",userId);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read())
                {
                    model.UserId = userId;
                    model.FriendsList = reader.IsDBNull(0) ? null : reader.GetString(0);
                    model.FriendRequests = reader.IsDBNull(1) ? null : reader.GetString(1);
                }
            }
        }
        return model;
    }
    public static void UpdateFriendsListOfUser(int userId, string friendsList)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"UPDATE Friends
                    SET FriendsList = @friendslist
                    WHERE UserId = @userid;";
                cmd.Parameters.AddWithValue("@friendslist", friendsList);
                cmd.Parameters.AddWithValue("@userid",userId);
                cmd.ExecuteNonQuery();
            }
        }
    }
    public static void UpdateFriendReqsListOfUser(int userId, string? friendReqsList)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"UPDATE Friends 
                    SET FriendRequests = @friendrequests 
                    WHERE UserId = @userid;";
                if(String.IsNullOrEmpty(friendReqsList))
                {
                    cmd.Parameters.AddWithValue("@friendrequests", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@friendrequests", friendReqsList);
                }
                cmd.Parameters.AddWithValue("@userid",userId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static string? RemoveIdFromFriendReqs(int idToRemove, string friendReqs)
    {
        var _friendReqsArr = friendReqs.Split(",");
        List<string> friendReqsList = new List<string>(_friendReqsArr);
        friendReqsList.Remove(idToRemove.ToString());
        if(friendReqsList.Count == 0){
            return null;
        }
        else
        {
            return String.Join(",",friendReqsList);
        }
        
    }

    public static string AddUserIdToFriendsList(int idToAdd, string? friendsListStr)
    {
        if(!String.IsNullOrEmpty(friendsListStr))
        {
            var _friendsListArr = friendsListStr.Split(",");
            List<string> friendsList = new List<string>(_friendsListArr);
            friendsList.Add(idToAdd.ToString());
            return String.Join(",",friendsList);
        }
        else
        {
            return idToAdd.ToString();
        }
        
    }

    public static string AddUserIdToFriendReqList(int idToAdd, string? friendReqListStr)
    {
        if(!String.IsNullOrEmpty(friendReqListStr))
        {
            var _friendReqListArr = friendReqListStr.Split(",");
            List<string> friendReqList = new List<string>(_friendReqListArr);
            friendReqList.Add(idToAdd.ToString());
            return String.Join(",",friendReqList);
        }
        else
        {
            return idToAdd.ToString();
        }
        
    }

    public static bool IsInFriendsList(int userIdToTest, string? friendsListStr)
    {
        if(String.IsNullOrEmpty(friendsListStr))
        {
            return false;
        }
        else
        {
            List<string> friendsList = new List<string>(friendsListStr.Split(','));
            return friendsList.Contains(userIdToTest.ToString());
        }
        
    }

    public static bool IsInFriendReqList(int userIdToTest, string? friendReqListStr)
    {
        if(String.IsNullOrEmpty(friendReqListStr))
        {
            return false;
        }
        else
        {
            List<string> friendReqList = new List<string>(friendReqListStr.Split(','));
            return friendReqList.Contains(userIdToTest.ToString());
        }
        
    }

    public static List<UserModel>? GetListAsUserObj (string? list)
    {
        List<UserModel> userListObj = new List<UserModel>();
        if(String.IsNullOrEmpty(list))
        {
            return null;
        }
        else
        {
            var userIdArr = list.Split(',');
            foreach (string userIdStr in userIdArr)
            {
                int userId = int.Parse(userIdStr);
                UserModel userModel = DbUsers.GetUserById(userId);
                userListObj.Add(userModel);
            }
            return userListObj;
        }
    }
}