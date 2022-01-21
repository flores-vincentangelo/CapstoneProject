namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail; 
using System.Text;  
using Database;
using Models;

public class RegisterController : Controller
{
    [HttpGet]
    [Route("/register")]
    public IActionResult GetRegisterAction() {
        return View("/Views/Register/Register.cshtml");
    }

    public bool SendVerificationEmail(UserModel user) {
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

    [HttpPost]
    [Route("/register")]
    public IActionResult doPostRegistration()
    {
        var firstName = HttpContext.Request.Form["FirstName"];
        var lastName = HttpContext.Request.Form["LastName"];
        var emailAddress = HttpContext.Request.Form["EmailAddress"];
        var mobileNumber = HttpContext.Request.Form["MobileNumber"];
        var password = HttpContext.Request.Form["Password"];
        var birthday = HttpContext.Request.Form["Birthday"];
        DateTime birthDate = DateTime.Parse(birthday);
        var dateOfBirth = (long)((birthDate.Subtract(new System.DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
        var gender = HttpContext.Request.Form["Gender"];

        var model = new UserModel();

        var isEmailUnique = DbUsers.checkEmailAddress(emailAddress);
        if (isEmailUnique)
        {
            return View("Views/Register/EmailExists.cshtml");
        }

        model.FirstName = firstName;
        model.LastName = lastName;
        model.EmailAddress = emailAddress;
        model.MobileNumber = mobileNumber;
        model.Password = password;
        model.Birthday = dateOfBirth;
        model.Gender = gender;
        model.FullName = (firstName + lastName).ToLower();

        var duplicate = DbUsers.checkFullName(model.FullName);
        if(duplicate == -1)
        {
            duplicate = 0;
        }
        else 
        { 
            duplicate += 1; 
        }

        model.Duplicate = duplicate;
        model.ProfileLink= model.FullName + model.Duplicate;
        DbUsers.InsertUser(model);
        SendVerificationEmail(model);
        return View("Views/Register/RegisteredSuccess.cshtml");
    }
}