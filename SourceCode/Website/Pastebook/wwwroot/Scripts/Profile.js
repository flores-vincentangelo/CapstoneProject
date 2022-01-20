$(document).ready( () => {

    // Profile picture
    EditProfilePicture();

    // About Me
    EditAboutMe();
});

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
    var modal = document.getElementById("profile-photo-edit-modal");
    window.onclick = function(event) {
        if (event.target == modal) {
            // Close modal
            $('.profile-photo-edit-modal').css("display","none");
        }
    }

    // When the user clicks on the "Save" button,
    $('#photo-save-btn').click(() => {
        console.log("Save photo");
        // Change photo
        // ...write code here...
        
        // Save in database
        var img = document.getElementById('myFile').files;
        console.log(img);
        console.log(img.length);
        if (img.length > 0) {
            getBase64(img[0]);
        }

        // Close modal
        $('.profile-photo-edit-modal').css("display","none");
    });
}

function getBase64(file) {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
      console.log(reader.result);
    };
    reader.onerror = function (error) {
      console.log('Error: ', error);
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
    var about = document.getElementById("form-edit-about");
    $('#about-save-btn').click(() => {
        console.log(about.value);

        // Change "About me" text
        $('#profile-about-text').text(about.value);
        // Save about.value to database
        // ...write code here...

        // show readonly profile
        $('.profile-about-readonly').css("display","flex");
        // hide edit form
        $('.profile-about-edit').css("display","none");
    });
}