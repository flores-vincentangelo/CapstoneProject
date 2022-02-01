namespace Database;
using System.Data.SqlClient;
using Models;
using Database;

public class DbLikes
{
    private static string? DB_CONNECTION_STRING;

    static DbLikes()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }
  
    public static void UpdateLikesListOfPost(int postId, string? likesList)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = 
                    @"UPDATE Posts
                    SET LikesList = @likesList
                    WHERE PostId = @postId;";
                if(String.IsNullOrEmpty(likesList))
                {
                    command.Parameters.AddWithValue("@likesList", "");
                }
                else
                {
                    command.Parameters.AddWithValue("@likesList", likesList);
                }
                command.Parameters.AddWithValue("@postId", postId);
                command.ExecuteNonQuery();
            }
        }
    }

    public static string AddUserIdtoLikesList(int? userIdToAdd, string? likesListStr)
    {
        if(!String.IsNullOrEmpty(likesListStr))
        {
            var _likesListArr = likesListStr.Split(",");
            List<string> likesList = new List<string>(_likesListArr);
            likesList.Add(userIdToAdd.ToString());
            return String.Join(",",likesList);
        }
        else
        {
            return userIdToAdd.ToString();
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

    public static bool IsUserInLikersList(int userIdToTest, string? likesListStr)
    {
        if(String.IsNullOrEmpty(likesListStr))
        {
            return false;
        }
        else
        {   
            List<string> likesList = new List<string>(likesListStr.Split(','));
            return likesList.Contains(userIdToTest.ToString());
        }
    }

    public static string? RemoveUserIdFromLikesList(int idToRemove, string likes)
    {
        var _likesArr = likes.Split(",");
        List<string> likesList = new List<string>(_likesArr);
        likesList.Remove(idToRemove.ToString());
        if(likesList.Count == 0){
            return null;
        }
        else
        {
            return String.Join(",",likesList);
        }
        
    }
}