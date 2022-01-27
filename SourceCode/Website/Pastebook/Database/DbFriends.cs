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

    public static void InitializeFriends(string email)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"INSERT INTO Friends (UserEmail) VALUES (@email);";
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();
                    System.Console.WriteLine("Entry added to Friends Table");
            }
        }
    }

    public static FriendsModel GetFriendsData(string email)
    {
        FriendsModel model = new FriendsModel();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT FriendsList, FriendRequests FROM Friends WHERE UserEmail = @email;";
                cmd.Parameters.AddWithValue("@email",email);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    model.UserEmail = email;
                    model.FriendsList = reader.IsDBNull(0) ? null : reader.GetString(0);
                    model.FriendRequests = reader.IsDBNull(1) ? null : reader.GetString(1);
                }
            }
        }
        return model;
    }

    public static void UpdateFriendsListOfUser(string userEmail, string friendsList)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"UPDATE Friends
                    SET FriendsList = @friendslist
                    WHERE UserEmail = @email;";
                cmd.Parameters.AddWithValue("@friendslist", friendsList);
                cmd.Parameters.AddWithValue("@email",userEmail);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateFriendReqsListOfUser(string userEmail, string? friendReqsList)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"UPDATE Friends 
                    SET FriendRequests = @friendrequests 
                    WHERE UserEmail = @email;";
                if(String.IsNullOrEmpty(friendReqsList))
                {
                    cmd.Parameters.AddWithValue("@friendrequests", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@friendrequests", friendReqsList);
                }
                cmd.Parameters.AddWithValue("@email",userEmail);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static string? RemoveEmailFromFriendReqs(string email, string friendReqs)
    {
        
        var _friendReqsArr = friendReqs.Split(",");
        List<string> friendReqsList = new List<string>(_friendReqsArr);
        friendReqsList.Remove(email);
        if(friendReqsList.Count == 0){
            return null;
        }
        else
        {
            return String.Join(",",friendReqsList);
        }
        
    }

    public static string AddEmailtoFriendsList(string emailToAdd, string? friendsListStr)
    {
        if(!String.IsNullOrEmpty(friendsListStr))
        {
            var _friendsListArr = friendsListStr.Split(",");
            List<string> friendsList = new List<string>(_friendsListArr);
            friendsList.Add(emailToAdd);
            return String.Join(",",friendsList);
        }
        else
        {
            return emailToAdd;
        }
        
    }

    public static string AddEmailtoFriendRequestList(string emailToAdd, string? friendReqListStr)
    {
        if(!String.IsNullOrEmpty(friendReqListStr))
        {
            var _friendReqListArr = friendReqListStr.Split(",");
            List<string> friendReqList = new List<string>(_friendReqListArr);
            friendReqList.Add(emailToAdd);
            return String.Join(",",friendReqList);
        }
        else
        {
            return emailToAdd;
        }
        
    }

    public static bool IsInFriendsList(string emailToTest, string? userFriendsList)
    {
        if(String.IsNullOrEmpty(userFriendsList))
        {
            return false;
        }
        else
        {
            return userFriendsList.Contains(emailToTest);
        }
        
    }

    public static bool IsInFriendReqList(string emailToTest, string? userFriendReqList)
    {
        if(String.IsNullOrEmpty(userFriendReqList))
        {
            return false;
        }
        else
        {
            return userFriendReqList.Contains(emailToTest);
        }
        
    }
}