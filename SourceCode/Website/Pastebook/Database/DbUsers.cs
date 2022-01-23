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