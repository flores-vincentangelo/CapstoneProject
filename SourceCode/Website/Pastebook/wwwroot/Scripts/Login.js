$(document).ready(() => {
    $(".login-container-form-form").submit(function(e){
        e.preventDefault();
        var emailAddress = $("#EmailAddress").val();
        var password = $("#Password").val();

        const userCredentials = {
            EmailAddress : emailAddress,
            Password : password
        }

        console.log(userCredentials.EmailAddress);
        console.log(userCredentials.Password);

        addSession(userCredentials);
    });
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
            localStorage.setItem('currentUserStorage', userCredentials.EmailAddress);
            const data = await response.json();
            const sessionId = data.id;
            localStorage.setItem('sessionIdStorage', sessionId);
            window.location.replace("/");
        }
        else {
            alert("Invalid credentials! Try again");
        }
}