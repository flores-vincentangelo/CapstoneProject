namespace Database;
using System.Data.SqlClient;
using System.Net.Mail; 
using System.Text; 
using Models;

public class DbUsers
{
    private static string? DB_CONNECTION_STRING;

    static DbUsers()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    public static void InsertUser(UserModel user)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText =
                    @"INSERT INTO Users (FirstName, LastName, EmailAddress, MobileNumber, Password, Birthday, Gender, FullName, Duplicate, ProfileLink) 
                    VALUES (@FirstName, @LastName, @EmailAddress, @MobileNumber, @Password, @Birthday, @Gender, @FullName, @Duplicate, @ProfileLink);";
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                cmd.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                cmd.Parameters.AddWithValue("@Password", BCrypt.Net.BCrypt.HashPassword(user.Password));
                cmd.Parameters.AddWithValue("@Birthday", user.Birthday);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Duplicate", user.Duplicate);
                cmd.Parameters.AddWithValue("@ProfileLink", user.ProfileLink);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Form successfully added to Users Table!");
            }
        }
    }

    public static UserModel? GetInformationById(string profileLink)
    {
        UserModel user = new UserModel();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Users WHERE ProfileLink = @ProfileLink;";
                cmd.Parameters.AddWithValue("@ProfileLink", profileLink);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    user.UserId = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);
                    user.EmailAddress = reader.GetString(3);
                    user.MobileNumber = reader.GetString(4);
                    user.Password = reader.GetString(5);
                    user.Birthday = reader.GetInt64(6);
                    user.Gender = reader.GetString(7);
                    user.FullName = reader.GetString(8);
                    user.Duplicate = reader.GetInt32(9);
                    user.ProfileLink = reader.GetString(10);
                }
            }
        }
        return user;
    }

    public static void DeleteUser(string profileLink)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM Users WHERE ProfileLink = @ProfileLink;";
                cmd.Parameters.AddWithValue("@ProfileLink", profileLink);
                cmd.ExecuteNonQuery();
            }

        }
    }

    public static void ModifyInformation(string profileLink, UserModel user)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                if(!String.IsNullOrEmpty(user.FirstName))
                {
                    cmd.CommandText = "UPDATE Users SET FirstName = @FirstName WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@ProfileLink", profileLink);
                    cmd.ExecuteNonQuery();
                }
                if(!String.IsNullOrEmpty(user.LastName))
                {
                    cmd.CommandText = "UPDATE Users SET LastName = @LastName WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@ProfileLink", profileLink);
                    cmd.ExecuteNonQuery();
                }
                if(!String.IsNullOrEmpty(user.EmailAddress))
                {
                    cmd.CommandText = "UPDATE Users SET EmailAddress = @EmailAddress WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                    cmd.Parameters.AddWithValue("@ProfileLink", profileLink);
                    cmd.ExecuteNonQuery();
                }
                if(!String.IsNullOrEmpty(user.MobileNumber))
                {
                    cmd.CommandText = "UPDATE Users SET MobileNumber = @MobileNumber WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                    cmd.Parameters.AddWithValue("@ProfileLink", profileLink);
                    cmd.ExecuteNonQuery();
                }
                if(!String.IsNullOrEmpty(user.Password))
                {
                    cmd.CommandText = "UPDATE Users SET Password = @Password WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Password", BCrypt.Net.BCrypt.HashPassword(user.Password));
                    cmd.Parameters.AddWithValue("@ProfileLink", profileLink);
                    cmd.ExecuteNonQuery();
                }
                if(!String.IsNullOrEmpty(user.Gender))
                {
                    cmd.CommandText = "UPDATE Users SET Gender = @Gender WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Gender", user.Gender);
                    cmd.Parameters.AddWithValue("@ProfileLink", profileLink);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public static bool checkEmailAddress(string email)
    {
        var isUsed = false;
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = $"SELECT EmailAddress FROM Users where EmailAddress = @EmailAddress;";
                cmd.Parameters.AddWithValue("@EmailAddress", email);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
            }
        }
        return isUsed;
    }

    public static int checkFullName(string fullName)
    {
        var maxDuplicate = -1;
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = $"SELECT MAX(Duplicate) FROM Users where FullName = @FullName;";
                cmd.Parameters.AddWithValue("@FullName", fullName);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        if(reader.IsDBNull(0)) return maxDuplicate;
                        else 
                        {
                            maxDuplicate = reader.GetInt32(0);
                        }
                    }
                }
            }
        }
        return maxDuplicate;
    }

    public static bool SendVerificationEmail(UserModel user) {
        string? to = user.EmailAddress;
        string from = "pastebooktest@gmail.com";
        MailMessage message = new MailMessage(from, to);
        var firstName = user.FirstName;
        string mailBody = $@"Welcome {firstName} to Pastebook!";
        message.Subject = "Registration successful!";
        message.Body = mailBody;
        message.BodyEncoding = Encoding.UTF8;
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("pastebooktest", "pasteb00kt3st");
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = basicCredential1;
        try {
            client.Send(message);
            return true;
        }
        catch (Exception e) {
            throw e;
        }
    }
}