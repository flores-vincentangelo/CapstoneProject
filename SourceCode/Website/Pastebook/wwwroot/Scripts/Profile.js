$(document).ready( () => {
    var modelObj = JSON.parse(model.replace(/&quot;/g,"\""));
    // console.log(modelObj.DoesUserOwnProfile);
    
    if(!modelObj.DoesUserOwnProfile){
        $("#photo-edit-btn").css("display", "none");
        $("#about-edit-btn").css("display", "none");

        $(".profile-edit-firstname-btn").css("display", "none");
        $(".profile-edit-lastname-btn").css("display", "none");
        $(".profile-edit-email-btn").css("display", "none");
        $(".profile-edit-mobile-btn").css("display", "none");
        $(".profile-edit-birthday-btn").css("display", "none");
        $(".profile-edit-gender-btn").css("display", "none");
        $("#profile-password").css("display", "none");
        $(".profile-edit-password-btn").css("display", "none");

        $(".add-album-btn-container").css("display", "none"); // Add new album button
        $(".photos-card-edit-btn").css("display", "none"); // Edit album button
        $(".photos-card-btn-container").css("display", "none"); // Add new photo / delete album button
        $(".photocard-del-btn").css("display", "none"); // Photocard delete button
    }

    // Profile picture
    EditProfilePicture();

    // About Me
    EditAboutMe();

    showProfileExt();
});

async function alertFunction (string){
    alert(string);
}

async function Addfriend(profileLink){
    // alert(profileLink);
    var url = `/friends/request/${profileLink}`
    const response = await fetch(url, {
        method: "PATCH"
    });
    if(response.ok){
        location.reload();
    }
}

function EditProfilePicture() {
    // When the user clicks on the "Change photo" button,
    $('#photo-edit-btn').click(() => {
        // Open modal
        $('.profile-photo-edit-modal').css("display","flex");
    });

    // When the user clicks on the "Cancel" button,
    $('#photo-cancel-btn').click(() => {
        // Close modal
        $('.profile-photo-edit-modal').css("display","none");
    });

    // When the user clicks on the "x",
    $('#profile-photo-close-modal').click(() => {
        // Close modal
        $('.profile-photo-edit-modal').css("display","none");
    });

    // When the user clicks anywhere outside of the modal
    window.onclick = function(event) {
        if (event.target == document.getElementById("profile-photo-edit-modal")) {
            // Close modal
            $('.profile-photo-edit-modal').css("display","none");
        }
    }

    // When the user clicks on the "Choose file" button
    $("#myFile").change(function() {
        if (this.files && this.files[0] && this.files[0].type.includes("image")) { 
            var reader = new FileReader();
            reader.readAsDataURL(this.files[0]);
            reader.onload = imageIsLoaded;
        }
        else {
            alert("This is not valid image file");
        }
    });
    // Change photo
    function imageIsLoaded(e) {
        result = e.target.result;
        $('#profile-photo-modal-img').attr('src', result);
        saveImage(result);
    };

    function saveImage(image) {
        // When the user clicks on the "Save" button,
        $('#photo-save-btn').click((event) => {
            event.preventDefault();
            // Close modal
            $('.profile-photo-edit-modal').css("display","none");
            $('#profile-photo-img').attr('src', image);
            const formData = {
                Photo: image
            }
            var data = JSON.stringify(formData);
            modifyProfile(event, data);
        });
    };
}

function EditAboutMe() {
    // When the user clicks on the "Edit" button,
    $('#about-edit-btn').click(() => {
        // hide readonly profile
        $('.profile-about-readonly').css("display","none");
        // show edit form 
        $('.profile-about-edit').css("display","flex");
    });
    
    // When the user clicks on the "Cancel" button,
    $('#about-cancel-btn').click(() => {
        // show readonly profile
        $('.profile-about-readonly').css("display","flex");
        // hide edit form 
        $('.profile-about-edit').css("display","none");
    });
    
    // When the user clicks on the "Save" button,
    $('#about-save-btn').click((event) => {
        var about = document.getElementById("form-edit-about");
        // Change "About me" text
        $('#profile-about-text').text(about.value);
        // Save about.value to database
        var formData = new FormData(document.getElementById('form-profile-about'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyProfile(event, data);

        // show readonly profile
        $('.profile-about-readonly').css("display","flex");
        // hide edit form
        $('.profile-about-edit').css("display","none");
    });
}

async function modifyProfile(event, jsonData) {
    event.preventDefault();
    // const link = localStorage.getItem('profileLink');
    const link = getCookieValue('profilelink');
    const url = `/${link}`;
    const response = await fetch(url, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: jsonData
    });
    // if(response.status == 200) {
    //     // alert("Profile modified successfully!");
    //     // var userData = await response.json();
    //     // localStorage.setItem('User', JSON.stringify(userData));
    // }
}

// From W3Schools
function getCookieValue(cname) {
    let name = cname + "="; // cookieName=
    // Decode the cookie string, to handle cookies with special characters, e.g. '$'
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for(let i = 0; i <ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1); // cookieName=cookieValue
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length); // cookieValue name
        }
    }
    return "";
}

function showProfileExt() {
    $('#ext-about-btn').click(() => {
        $('#profile-ext-about').css("display", "flex");
        $('#profile-about-readonly').css("display", "flex");
        $('#profile-ext-posts').css("display", "none");
        $('#profile-ext-friends').css("display", "none");
        $('#profile-ext-album').css("display", "none");
        $('#profile-edit-modal').css("display", "none");
    });

    $('#ext-posts-btn').click(() => {
        $('#profile-ext-about').css("display", "none");
        $('#profile-ext-posts').css("display", "flex");
        $('#profile-ext-friends').css("display", "none");
        $('#profile-ext-album').css("display", "none");
        $('#post-modal-container-delete').css("display", "none");
    });

    $('#ext-friends-btn').click(() => {
        $('#profile-ext-about').css("display", "none");
        $('#profile-ext-posts').css("display", "none");
        $('#profile-ext-friends').css("display", "flex");
        $('#profile-ext-album').css("display", "none");
    });

    $('#ext-album-btn').click(() => {
        $('#profile-ext-about').css("display", "none");
        $('#profile-ext-posts').css("display", "none");
        $('#profile-ext-friends').css("display", "none");
        $('#profile-ext-album').css("display", "flex");
    });
}
