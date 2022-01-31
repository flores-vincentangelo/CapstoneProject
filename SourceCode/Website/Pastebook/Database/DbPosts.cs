namespace Database;
using System.Data.SqlClient;
using Models;
using System.Text.Json;

public class DbPosts
{
    private static string? DB_CONNECTION_STRING;

    static DbPosts()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    public static void InsertPost(PostModel post)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var command = db.CreateCommand())
            {
                command.CommandText =
                    @"INSERT INTO Posts (EmailAddress, DatePosted, Caption, PhotoId, Photo, Likes, Comment, ProfileLink, LikesList, CommentsList) 
                    VALUES (@EmailAddress, @DatePosted, @Caption, @PhotoId, @Photo, @Likes, @Comment, @ProfileLink, @LikesList, @CommentsList);";
                command.Parameters.AddWithValue("@EmailAddress", post.EmailAddress);
                command.Parameters.AddWithValue("@DatePosted", post.DatePosted);
                command.Parameters.AddWithValue("@Caption", post.Caption);
                command.Parameters.AddWithValue("@PhotoId", post.PhotoId);
                command.Parameters.AddWithValue("@Photo", post.Photo);
                command.Parameters.AddWithValue("@Likes", post.Likes);
                command.Parameters.AddWithValue("@Comment", post.Comment);
                command.Parameters.AddWithValue("@ProfileLink", post.ProfileLink);   
                command.Parameters.AddWithValue("@LikesList", post.LikesList); 
                command.Parameters.AddWithValue("@CommentsList", post.CommentsList);             
                command.ExecuteNonQuery();
            }
        }
    }

    public static List<PostModel>? GetAllPostDetails(string profileLink) 
    {
        List<PostModel> postDetails = new List<PostModel>();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {       
                command.CommandText = "SELECT * FROM Posts WHERE ProfileLink = @ProfileLink";
                command.Parameters.AddWithValue("@ProfileLink", profileLink);
                var reader = command.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read())
                {
                    PostModel postDetail = new PostModel();
                    postDetail.EmailAddress = reader.GetString(0);
                    postDetail.PostId = reader.GetInt32(1);
                    postDetail.DatePosted = reader.GetInt64(2);
                    postDetail.Caption = reader.GetString(3);
                    postDetail.PhotoId = reader.GetInt32(4);
                    postDetail.Photo = reader.GetString(5);
                    postDetail.Likes = reader.GetString(6);
                    postDetail.Comment = reader.GetString(7);
                    postDetail.ProfileLink = reader.GetString(8);
                    postDetail.LikesList = reader.GetString(9);
                    postDetail.CommentsList = reader.GetString(10);
                    postDetails.Add(postDetail);
                }
            }
            return postDetails;
        }
    }

    public static List<PostModel>? GetAllPostsByEmail(string userEmail) 
    {
        List<PostModel> postDetails = new List<PostModel>();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {       
                command.CommandText = "SELECT * FROM Posts WHERE EmailAddress = @EmailAddress";
                command.Parameters.AddWithValue("@EmailAddress", userEmail);
                var reader = command.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read())
                {
                    PostModel postDetail = new PostModel();
                    postDetail.EmailAddress = reader.GetString(0);
                    postDetail.PostId = reader.GetInt32(1);
                    postDetail.DatePosted = reader.GetInt64(2);
                    postDetail.Caption = reader.GetString(3);
                    postDetail.PhotoId = reader.GetInt32(4);
                    postDetail.Photo = reader.GetString(5);
                    postDetail.Likes = reader.GetString(6);
                    postDetail.Comment = reader.GetString(7);
                    postDetail.ProfileLink = reader.GetString(8);
                    postDetail.LikesList = reader.GetString(9);
                    postDetail.CommentsList = reader.GetString(10);
                    postDetails.Add(postDetail);
                }
            }
            return postDetails;
        }
    }

    public static PostModel? GetPostById(int id)
    {
        PostModel post = new PostModel();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Posts WHERE PostId = @postid;";
                command.Parameters.AddWithValue("@postid", id);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    post.EmailAddress = reader.GetString(0);
                    post.PostId = reader.GetInt32(1);
                    post.DatePosted = reader.GetInt64(2);
                    post.Caption = reader.GetString(3);
                    post.PhotoId = reader.GetInt32(4);
                    post.Photo = reader.GetString(5);
                    post.Likes = reader.GetString(6);
                    post.Comment = reader.GetString(7);
                    post.ProfileLink = reader.GetString(8); 
                    post.LikesList = reader.GetString(9); 
                    post.CommentsList = reader.GetString(10); 
                }
            }
        }
        return post;
    }    

    public static PostModel? GetPostByProfileLink(string profileLink)
    {
        PostModel post = new PostModel();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Posts WHERE ProfileLink = @ProfileLink";
                command.Parameters.AddWithValue("@ProfileLink", profileLink);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    post.EmailAddress = reader.GetString(0);
                    post.PostId = reader.GetInt32(1);
                    post.DatePosted = reader.GetInt64(2);
                    post.Caption = reader.GetString(3);
                    post.PhotoId = reader.GetInt32(4);
                    post.Photo = reader.GetString(5);
                    post.Likes = reader.GetString(6);
                    post.Comment = reader.GetString(7); 
                    post.ProfileLink = reader.GetString(8); 
                    post.LikesList = reader.GetString(9); 
                    post.CommentsList = reader.GetString(10);
                }
            }
        }
        return post;
    }

    public static PostModel? GetPostByPhotoId(int photoId)
    {
        PostModel post = new PostModel();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Posts WHERE PhotoId = @PhotoId";
                command.Parameters.AddWithValue("@PhotoId", photoId);
                var reader = command.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read())
                {
                    post.EmailAddress = reader.GetString(0);
                    post.PostId = reader.GetInt32(1);
                    post.DatePosted = reader.GetInt64(2);
                    post.Caption = reader.GetString(3);
                    post.PhotoId = reader.GetInt32(4);
                    post.Photo = reader.GetString(5);
                    post.Likes = reader.GetString(6);
                    post.Comment = reader.GetString(7); 
                    post.ProfileLink = reader.GetString(8); 
                    post.LikesList = reader.GetString(9); 
                    post.CommentsList = reader.GetString(10);
                }
            }
        }
        return post;
    }     

    public static void DeletePostByPostId(int postId)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "DELETE FROM Posts WHERE PostId = @PostId";
                command.Parameters.AddWithValue("@PostId", postId);
                command.ExecuteNonQuery();
            }
        }
    }
    public static void ModifyPost(int postId, PostModel post)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                if(!String.IsNullOrEmpty(post.Caption))
                {
                    command.CommandText = "UPDATE Posts SET Caption = @Caption WHERE PostId = @CaptionPostId;";
                    command.Parameters.AddWithValue("@Caption", post.Caption);
                    command.Parameters.AddWithValue("@CaptionPostId", postId);
                    command.ExecuteNonQuery(); 
                }
                if(!String.IsNullOrEmpty(post.Photo))
                {
                    command.CommandText = "UPDATE Posts SET Photo = @Photo WHERE PostId = @PhotoPostId;";
                    command.Parameters.AddWithValue("@Photo", post.Photo);
                    command.Parameters.AddWithValue("@PhotoPostId", postId);
                    command.ExecuteNonQuery();
                }
                System.Console.WriteLine(postId);
            }
        }
    }
    public static void UpdateLikesListOfUser(string emailAddress, string? likesList)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = 
                    @"UPDATE Posts
                    SET Likes = @likes
                    WHERE EmailAddress = @email;";
                if(String.IsNullOrEmpty(likesList))
                {
                    command.Parameters.AddWithValue("@likes", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@likes", likesList);
                }
                command.Parameters.AddWithValue("@email", emailAddress);
                command.ExecuteNonQuery();
            }
        }
    }

    public static string AddEmailtoLikesList(string emailToAdd, string? likesListStr)
    {
        if(!String.IsNullOrEmpty(likesListStr))
        {
            var _likesListArr = likesListStr.Split(",");
            List<string> likesList = new List<string>(_likesListArr);
            likesList.Add(emailToAdd);
            return String.Join(",",likesList);
        }
        else
        {
            return emailToAdd;
        }
        
    }

}