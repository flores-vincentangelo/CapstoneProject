$(document).ready(() => {
    $(".login-container-form-form").submit(function(e){
        e.preventDefault();
        var emailAddress = $("#EmailAddress").val();
        var password = $("#Password").val();

        const userCredentials = {
            EmailAddress : emailAddress,
            Password : password
        }

        addSession(userCredentials);
    });
    //delete cookies
    document.cookie = "email=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
    document.cookie = "sessionId=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
    document.cookie = "profilelink=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
    //delete Local Storage
    localStorage.clear();
});


async function addSession(userCredentials) {
    
    const response = await fetch('/login', {
        method: 'POST',
        headers: {
        'Content-Type': 'application/json',
        },
        body: JSON.stringify(userCredentials)
    });
        if (response.status == 200) {
            document.cookie = 'email=' + userCredentials.EmailAddress;
            const data = await response.json();
            const sessionId = data.id;
            document.cookie = 'sessionId=' +  sessionId;
            window.location.replace("/");
        }
        else {
            alert("Invalid credentials! Try again");
        }
}