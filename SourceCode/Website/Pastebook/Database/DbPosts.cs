namespace Database;
using System.Data.SqlClient;
using Models;

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
                    @"INSERT INTO Posts (EmailAddress, PostId, DatePosted, Caption, PhotoId, Photo, Like, Comment, PostLink) 
                    VALUES (@EmailAddress, @PostId, @DatePosted, @Caption, @PhotoId, @Photo, @Like, @Comment, @PostLink);";
                command.Parameters.AddWithValue("@EmailAddress", post.EmailAddress);
                command.Parameters.AddWithValue("@PostId", post.PostId);
                command.Parameters.AddWithValue("@DatePosted", post.DatePosted);
                command.Parameters.AddWithValue("@Caption", post.Caption);
                command.Parameters.AddWithValue("@PhotoId", post.PhotoId);
                command.Parameters.AddWithValue("@Photo", post.Photo);
                command.Parameters.AddWithValue("@Like", post.Like);
                command.Parameters.AddWithValue("@Comment", post.Comment);
                command.Parameters.AddWithValue("@PostLink", post.PostLink);                
                command.ExecuteNonQuery();
            }
        }
    }

    public static List<PostModel>? GetAllPostDetails() 
    {
        List<PostModel> postDetails = new List<PostModel>();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {       
                command.CommandText = "SELECT * FROM Posts";
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    PostModel postDetail = new PostModel();
                    postDetail.EmailAddress = reader.GetString(0);
                    postDetail.PostId = reader.GetInt32(1);
                    postDetail.DatePosted = reader.GetInt64(2);
                    postDetail.Caption = reader.GetString(3);
                    postDetail.PhotoId = reader.GetInt32(4);
                    postDetail.Photo = reader.GetString(5);
                    postDetail.Like = reader.GetString(6);
                    postDetail.Comment = reader.GetString(7);
                    postDetail.PostLink = reader.GetString(8);
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
                command.CommandText = "SELECT * FROM Posts WHERE PostId = @PostId";
                command.Parameters.AddWithValue("@PostId", id);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    post.EmailAddress = reader.GetString(0);
                    post.PostId = reader.GetInt32(1);
                    post.DatePosted = reader.GetInt64(2);
                    post.Caption = reader.GetString(3);
                    post.PhotoId = reader.GetInt32(4);
                    post.Photo = reader.GetString(5);
                    post.Like = reader.GetString(6);
                    post.Comment = reader.GetString(7); 
                    post.PostLink = reader.GetString(8);
                }
            }
        }
        return post;
    }    

    public static PostModel? GetPostByEmail(int email)
    {
        PostModel post = new PostModel();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Posts WHERE EmailAddress = @EmailAddress";
                command.Parameters.AddWithValue("@EmailAddress", email);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    post.EmailAddress = reader.GetString(0);
                    post.PostId = reader.GetInt32(1);
                    post.DatePosted = reader.GetInt64(2);
                    post.Caption = reader.GetString(3);
                    post.PhotoId = reader.GetInt32(4);
                    post.Photo = reader.GetString(5);
                    post.Like = reader.GetString(6);
                    post.Comment = reader.GetString(7); 
                    post.PostLink = reader.GetString(8); 
                }
            }
        }
        return post;
    }    

    public static void DeletePostById (int postId)
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

    public static void ModifyPost (string postLink, PostModel post)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                if(!String.IsNullOrEmpty(post.Caption))
                {
                    command.CommandText = "UPDATE Posts SET Caption = @Caption WHERE PostLink = @PostLink;";
                    command.Parameters.AddWithValue("@Caption", post.Caption);
                    command.Parameters.AddWithValue("@PostLink", postLink);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}